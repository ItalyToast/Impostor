using Hazel;
using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllBytes("server 2 client.bin");

            var reader = MessageReader.Get(data);

            MessageReader msg;
            while ((msg = reader.ReadMessage()) != null)
            {
                Console.WriteLine($"New msg: {msg.Length} Tag: {msg.Tag}");
            }
        }
    }
}
