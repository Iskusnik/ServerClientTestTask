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
    public class Client
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
        public string[] InnerFiles;

        /// <summary>
        /// Находится ли файл в обработке на сервере или необходима повторная отправка
        /// </summary>
        public FormClient.ResultMeaning[] FileInWork;

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
            FileInWork = new FormClient.ResultMeaning[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                InnerFiles[i] = File.ReadAllText(names[i]);
                FileInWork[i] = FormClient.ResultMeaning.NoResult;
            }
            return InnerFiles;
        }

    }
}
