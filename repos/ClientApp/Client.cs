using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ClientApp
{
    /// <summary>
    /// Клиент, создающий запросы к серверу на основе текстовых файлов, 
    /// хранящихся в FolderRout
    /// </summary>
    class Client
    {
        /// <summary>
        /// Подсчёт клиентов
        /// </summary>
        static private int ClientCount = 0;

        /// <summary>
        /// Путь к папке с текстовыми файлами
        /// </summary>
        private readonly string FolderPath;

        /// <summary>
        /// Содержимое файлов в папке
        /// </summary>
        private string[] InnerFiles;

        public Client(string FolderPath)
        {
            this.FolderPath = FolderPath;
            ClientCount++;
        }



        /// <summary>
        /// Отправка запроса на проверку файлов из папки
        /// </summary>
        /// <param name="serverAdress">Путь/адресс к серверу</param>
        public string[] GetStrings()
        {
            string[] names = Directory.GetFiles(FolderPath, "*.txt");
            InnerFiles = new string[names.Length];

            for (int i = 0; i < names.Length; i++)
                InnerFiles[i] = File.ReadAllText(names[i]);

            return InnerFiles;
        }


        /// <summary>
        /// Отправка запроса на проверку файлов
        /// </summary>
        /// <param name="serverAdress">Путь/адресс к серверу</param>
        public void SendRequests(object form)
        {
            FormClient formClient = (FormClient)form;
            string serverAdress = "127.0.0.1";
            TcpClient client = new TcpClient(serverAdress, 8888);

            NetworkStream stream = client.GetStream();


            for (int i = 0; i < InnerFiles.Length; i++)
            {
                bool noRes = true;
                while (noRes)
                {
                    // преобразуем сообщение в массив байтов
                    byte[] data = Encoding.Unicode.GetBytes(InnerFiles[i]);
                    // отправка сообщения
                    stream.Write(data, 0, data.Length);

                    string result = InnerFiles[i] + " " + GetAnswer(serverAdress, ref noRes);

                    formClient.AppendTextBox(result);
                }
            }
        }

        
        /// <summary>
        /// Ожидание ответа
        /// </summary>
        /// <returns></returns>
        public string GetAnswer(string serverAdress, ref bool haveRes)
        {
            TcpClient client = new TcpClient(serverAdress, 8888);

            NetworkStream stream = client.GetStream();
            // получаем ответ
            byte[] data = new byte[200]; // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);

            string res = builder.ToString();
            if (res[0] == 'F' || res[0] == 'T')
                haveRes = true;
            else
                haveRes = false;
            return res;
        }
    }
}
