using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ServerApp
{
    /// <summary>
    /// Класс обработки запросов
    /// </summary>
    public class Server
    {

        /// <summary>
        /// Максимальное число запросов в очереди
        /// </summary>
        public int N
        {
            get;
            set;
        }

        /// <summary>
        /// Очередь запросов
        /// </summary>
        public Queue<string> RequestQue;

        /// <summary>
        /// Список клиентов по отношению к запросам
        /// </summary>
        public Queue<TcpClient> ClientQue;

        public Server(int N)
        {
            this.N = N;
            RequestQue = new Queue<string>();
            ClientQue = new Queue<TcpClient>();
        }
       

        /// <summary>
        /// Проверка строки <paramref name="s"/> на свойство палиндрома
        /// </summary>
        /// <param name="s">Проверяемая строка</param>
        /// <returns></returns>
        public bool IsPalindrom(string s)
        {
            Task.Delay(2000).Wait();

            bool result = true;
            int len = s.Length;

            for (int i = 0; i < len / 2; i++)
                result = result && (s[i] == s[len - i - 1]);
            
            return result;
        }      
    }
}
