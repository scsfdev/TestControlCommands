using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestControlCommands
{
    class Program
    {
        static SerialPort sr = new SerialPort();
        static void Main(string[] args)
        {
            sr.PortName = "COM10";
            sr.BaudRate = 115200;
            sr.DataReceived += Sr_DataReceived;
            Console.WriteLine("Type in AT20Q Control Commands only. Invalid commands will trigger error beep on AT20Q");
            Console.WriteLine("Type in Q to exit.");
            sr.Open();
            string ctrlCmd = "";
            while (ctrlCmd != "Q")
            {
                ctrlCmd = Console.ReadLine();
                if (ctrlCmd.ToUpper() == "Q")
                    break;

                sr.Write(ctrlCmd + "\r");
            }

            sr.Close();
        }

        private static void Sr_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine(sr.ReadExisting());
        }
    }
}
