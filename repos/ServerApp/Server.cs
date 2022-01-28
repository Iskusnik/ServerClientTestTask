using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;

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

        /// <summary>
        /// Список потоков связанных с клиентами
        /// </summary>
        public Queue<NetworkStream> StreamQue;
        public Server(int N)
        {
            this.N = N;
            RequestQue = new Queue<string>();
            ClientQue = new Queue<TcpClient>();
            StreamQue = new Queue<NetworkStream>();
        }
       

        /// <summary>
        /// Проверка строки <paramref name="s"/> на свойство палиндрома
        /// </summary>
        /// <param name="s">Проверяемая строка</param>
        /// <returns></returns>
        public bool IsPalindrom(string s)
        {
            Thread.Sleep(2000);

            bool result = true;
            int len = s.Length;

            for (int i = 0; i < len / 2; i++)
                result = result && (s[i] == s[len - i - 1]);
            
            return result;
        }      
    }
}
