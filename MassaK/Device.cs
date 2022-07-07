using System;

namespace ASHK.MassaK
{
    public class Device
    {
        public int? ID { get; private set; } = null;
        public string IP { get; private set; } = null;
        public int Port { get; private set; } = 0;
        public string Name { get; private set; } = null;
        
        public TerminalMassaK.Device TerminalMassaK { get; set; } = null;
        public Device(string IP, int Port, string Name)
        {
           
            this.IP = IP;
            this.Port = Port;
            this.Name = Name;
            GetDevice();
        }
        /// <summary>
        /// Все методы применяются только к терминалам, включенным в список устройств.
        /// </summary>
        public void GetDevice()
        {
            try
            {
                TerminalMassaK = new TerminalMassaK.Device();
            }
            catch { }
            TerminalMassaK.Connection = IP + ":" + Port;
            TerminalMassaK.Name = Name;
            try
            {
                TerminalMassaK.Add(); //Все методы применяются только к терминалам, включенным в список устройств.
                TerminalMassaK.Get();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this.ID = TerminalMassaK.ID;
            
        }

    }
}
