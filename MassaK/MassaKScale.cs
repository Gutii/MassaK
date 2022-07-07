using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ASHK.MassaK
{
    public class MassaKScale
    {
        public Device Device { get; private set; }
        public bool Connect { get; private set; } = false;
        public delegate Int32? ResultWeight();
        public ResultWeight GetWeight { get; private set; }
        public TerminalMassaK.DriverClass DriverClass { get; private set; }

        public MassaKScale(string IP, int Port, string Name)
        {
            DriverClass = new TerminalMassaK.DriverClass();
            CreateScalesList(Directory.GetCurrentDirectory());
            CreateScalesList(@"C:\Windows\System32");
            Device = new Device(IP, Port, Name);
            GetWeight += ReadWeight;
            GetError();
        }

        public void CreateScalesList(string Path)
        {
            FileInfo fileInfo = new FileInfo(Path+@"\ScalesList.csv");
            try
            {
                if (!fileInfo.Exists)
                {
                    var stream = fileInfo.Create();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        /// <summary>
        /// Без использовании OpenConect, CloseConect() каждый запрос к весам будет сопровождаться установкой/разрывом соединения
        /// внутри самого запроса, что увеличивает время выполнения, но избавляет от необходимости вручную устанавли-вать и разрывать соединение.
        /// </summary>
        /// <returns></returns>
        public bool OpenConect()
        {
            if (Device.TerminalMassaK.OpenConnection() == 0) Connect = true;
            GetError();
            return Connect;
        }
        public bool CloseConect()
        {
            if (Device.TerminalMassaK.CloseConnection() == 0) Connect = false;
            GetError();
            return Connect;
        }
        /// <summary>
        /// Читает вес с весов в граммах. Если масса не стабилизирована, меньше или ровна нулю - возвращает null
        /// </summary>
        /// <returns></returns>
        private Int32? ReadWeight()
        {
            lock (this)
            {
                Int32? result = null;           
                try
                {
                    //if (!Connect) OpenConect();
                    Device.TerminalMassaK.GetWeight(); // может возникнуть разрыв соединения при чтении, ошибка такая: Socket Error # 10054 Connection reset by peer.
                    if (Device.TerminalMassaK.Stable == 1 & Device.TerminalMassaK.Weight > 0) // 0 - масса не стабилизирована, 1 - масса стабилизирована
                        result = Device.TerminalMassaK.Weight;
                    GetError();
                }
                catch (Exception ex)
                {
                    //Device.GetDevice.CloseConnection();
                    Connect = false;
                    throw ex;
                }
                return result;
            }
        }
        public void GetError()
        {
            ErrorCode errorCode = (ErrorCode)DriverClass.ResultCode;
            if (!errorCode.Equals(ErrorCode.Success) && !errorCode.Equals(ErrorCode.NoRecords))
            {                
                Exception ex = new Exception(DriverClass.ResultDescription);
                ex.HelpLink = ((int)errorCode).ToString();
                //Device.TerminalMassaK.CloseConnection();
                throw ex;
            }
                
        }
        /// <summary>
        /// Вызывает встроенную утилиту из драйвера.
        /// Используется для отладки и просмотра.
        /// </summary>
        public void ShowProperty()
        {
            Device.TerminalMassaK.ShowProperties();            
        }
            
        public Operator GetOperator()
        {
            return new Operator(this);
        }

    }
}
