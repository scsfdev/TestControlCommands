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
            try
            {
                Console.WriteLine("Type in AT20Q/AT30Q Control Commands only. Invalid commands will trigger error beep on Scanner");
                Console.WriteLine(".....");
                Console.Write("Key in your COM Port number: ");
                string comPort = Console.ReadLine();

                comPort = comPort.ToUpper();
                comPort = comPort.Replace(" ","").Replace("COM:", "").Replace("COM", "").Replace("CO", "").Replace("C", "");

                if (comPort != "")
                {
                    int iPort = Convert.ToInt32(comPort);
                    Console.WriteLine("Type in Q to exit.");

                    sr.PortName = "COM" + iPort;
                    sr.BaudRate = 115200;
                    sr.DataReceived += Sr_DataReceived;

                    sr.Open();
                    string ctrlCmd = "";
                    while (ctrlCmd != "Q")
                    {
                        ctrlCmd = Console.ReadLine();
                        if (ctrlCmd.ToUpper() == "Q")
                            break;

                        sr.Write(ctrlCmd.ToUpper().Trim() + "\r");
                    }

                    sr.Close();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid COM Port number!");
            }
          
        }

        private static void Sr_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine(sr.ReadExisting());
        }
    }
}
