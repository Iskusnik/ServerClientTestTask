using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private readonly string FolderRout;

        public Client(string FolderRout)
        {
            this.FolderRout = FolderRout;
            ClientCount++;
        }



        /// <summary>
        /// Отправка запроса на проверку файлов из папки
        /// </summary>
        /// <param name="serverAdress">Путь/адресс к серверу</param>
        public void ClientJob(string serverAdress)
        {

        }


        /// <summary>
        /// Отправка запроса на проверку одного файла
        /// </summary>
        /// <param name="serverAdress">Путь/адресс к серверу</param>
        public void SendRequest(string serverAdress)
        {

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
