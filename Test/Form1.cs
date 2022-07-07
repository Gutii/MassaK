using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
//using MassaK;

namespace Test
{
    public partial class Form1 : Form
    {
        ASHK.MassaK.MassaKScale massaKScale1;
        string pathOperators = System.IO.Directory.GetCurrentDirectory() + "\\ReadOperators.io";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                massaKScale1 = new ASHK.MassaK.MassaKScale("10.10.19.101", 5001, "Test");
                //massaKScale1.OpenConect();
            }
            catch(Exception ex)
            {
                //memoEdit1.Text = ex.Message + ex.HelpLink + ex.StackTrace;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {                
                var Weight = massaKScale1.GetWeight.Invoke();
                //memoEdit1.Text = Weight.ToString();
            }
            catch (Exception ex)
            {
                //memoEdit1.Text = ex.Message + ex.HelpLink + ex.StackTrace;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var oper = massaKScale1.GetOperator();
            var a = oper.Unload();
            string g = oper.ParseOperators(a);
            //System.IO.File.WriteAllText(pathOperators, g);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var oper = massaKScale1.GetOperator();
            string g = System.IO.File.ReadAllText(pathOperators);
            oper.Clear();
            oper.Download(g);
            //try
            //{
            //    oper.Download("asdag asdg ; asdgasd asgd asg; asdgasdg; asdgasdg;");
            //}
            //catch(Exception ex)
            //{

            //}
        }
    }
}
