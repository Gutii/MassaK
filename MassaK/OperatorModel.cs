using System;
using System.Collections.Generic;
using System.Text;

namespace ASHK.MassaK
{
    public class OperatorModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PassCode { get; set; }
        public string Code { get; set; }

        public OperatorModel() { }

        public OperatorModel(int id, string name, int passcode, string code)
        {
            ID = id;
            Name = name;
            PassCode = passcode;
            Code = code;
        }

    }
}
