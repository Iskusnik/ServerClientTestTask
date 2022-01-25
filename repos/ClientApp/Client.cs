using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        /// Отправка запроса на проверку одного файла
        /// </summary>
        /// <param name="serverAdress">Путь/адресс к серверу</param>
        /// <returns>Результат запроса: является или нет палиндромом или ошибка</returns>
        public string SendRequest(string serverAdress)
        {
            return null;
        }

        
        /// <summary>
        /// Ожидание ответа
        /// </summary>
        /// <returns></returns>
        public string GetAnswer()
        {
            return null;
        }
    }
}
