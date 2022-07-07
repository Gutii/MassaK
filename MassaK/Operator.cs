using System;
using System.Collections.Generic;
using System.Text;

namespace ASHK.MassaK
{
    public class Operator
    {
        private TerminalMassaK.Operator_Class Operator_ { get; set; }
        private MassaKScale _MassaKScale { get; set; }
        internal Operator(MassaKScale massaKScale)
        {
            _MassaKScale = massaKScale;
            Operator_ = new TerminalMassaK.Operator_Class();            
        }

        public int GetLastID()
        {
            _MassaKScale.Device.TerminalMassaK.ReadOperator();
            Operator_.GetFirst();
            int id = 0;
            id = Operator_.ID;
            while (Operator_.GetNext() != -12) // -12 в справочнике больше нет записей
            {
                id = Operator_.ID;
            }
            return id;

        }

        public void Download(string parseStr)
        {            
            _MassaKScale.Device.TerminalMassaK.WriteType = 0; //0 – Загрузка данных в терминал; 1 – Корректировка данных в терминал
            CollectOperators(parseStr);
            _MassaKScale.GetError();
        }

        public void Clear()
        {
            _MassaKScale.Device.TerminalMassaK.DeleteOperator();
        }

        public List<OperatorModel> Unload()
        {
            _MassaKScale.Device.TerminalMassaK.ReadOperator();
            List<OperatorModel> operators = new List<OperatorModel>();
            Operator_.GetFirst();
            operators.Add(new OperatorModel(Operator_.ID, Operator_.Name, Operator_.Passcode, Operator_.Code));
            while (Operator_.GetNext() != -12) // -12 в справочнике больше нет записей
            {
                operators.Add(new OperatorModel(Operator_.ID, Operator_.Name, Operator_.Passcode, Operator_.Code));
            }

            return operators;
        }

        public void CollectOperators(string collectOperators)
        {
            string[] liststr = collectOperators.Split('\n');
            int id = GetLastID()+1;
            foreach (string s in liststr)
            {
                string[] listoper = s.Split(';');
                if (listoper.Length >= 3)
                {
                    Operator_.Clear();
                    Operator_.ID = id;
                    Operator_.Code = listoper[0].Trim();
                    Operator_.Name = listoper[1];
                    int res = 0;
                    if (!int.TryParse(listoper[2], out res))
                    {
                        throw new Exception("Is not parse to name:" + listoper[1] + " and passcode: " + listoper[2]);
                    }
                    Operator_.Passcode = res;
                    if (Operator_.Add() != 0)
                    {
                        throw new Exception("Неверные данные");
                    }
                    id++;
                    _MassaKScale.Device.TerminalMassaK.WriteOperator();
                }
            }

        }

        public string ParseOperators(List<OperatorModel> operatorModels)
        {
            string str = string.Empty;
            foreach (var b in operatorModels)
            {
                str = str + b.Code + ";" + b.Name + ";" + b.PassCode + ";" + "\r\n";
            }
            try
            {
                str = str + "ERROR:";
                _MassaKScale.GetError();
            }
            catch (Exception ex)
            {
                str = str + ex.Message;
            }

            return str;
        }
    }
}
