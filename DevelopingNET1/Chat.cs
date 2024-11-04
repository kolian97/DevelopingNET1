using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace DevelopingNET1
{
    internal class Chat
    {

        public static void Server()
        {
            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
            UdpClient ucl = new UdpClient(12345);
            Console.WriteLine("The server is waiting for the client");
            while (true)
            {
                try
                {
                    byte[] buffer = ucl.Receive(ref localEP);
                    string str1 = Encoding.UTF8.GetString(buffer);

                    Message? somemessage = Message.FromJson(str1);
                    if (somemessage != null)
                    {
                        Console.WriteLine(somemessage.ToString());

                        Message newmessage = new Message("server", "Сообщение получено");
                        string js = newmessage.ToJson();
                        byte[] bytes = Encoding.UTF8.GetBytes(js);
                        ucl.Send(bytes, localEP);
                    }
                    else { Console.WriteLine("Некорректное сообщение"); }
                }
                catch(Exception e) { Console.WriteLine(e.Message); }
            }
        }
        public static void Client(string nik)
        {
            IPEndPoint localEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
            UdpClient ucl = new UdpClient();
            while (true)
            {
                Console.WriteLine("Введите сообщение");
                string text = Console.ReadLine();
                if(string.IsNullOrEmpty(text))
                {
                    break;
                }
                Message newmessage = new Message(nik, text);
                string js = newmessage.ToJson();
                byte[] bytes = Encoding.UTF8.GetBytes(js);
                ucl.Send(bytes, localEP);

                byte[] buffer = ucl.Receive(ref localEP);
                string str1 = Encoding.UTF8.GetString(buffer);
                Message? somemessage = Message.FromJson(str1);
                Console.WriteLine(somemessage);
            }
        }

    }
}
