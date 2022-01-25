using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private int N
        {
            get;
            set;
        }

        private Queue<string> RequestQue;

        public Server(int N)
        {
            this.N = N;
            RequestQue = new Queue<string>();
        }

        /// <summary>
        /// Проверка строки <paramref name="s"/> на свойство палиндрома
        /// </summary>
        /// <param name="s">Проверяемая строка</param>
        /// <returns></returns>
        public bool IsPalindrom(string s)
        {
            bool result = true;
            int len = s.Length;

            for (int i = 0; i < len / 2; i++)
                result = result && (s[i] == s[len - i - 1]);

            return result;
        }

        private void ReturnResult(string clientAdress, bool result)
        {

        }

        public void StartWaiting()
        {
            //Запуск TcpListener
            //Запуск обработчика
            try
            {
                //Пытаемся добавить в очередь
                //Если очередь заполнена - throw Exception "Очередь заполнена, повторите запрос позже"
            }
            catch(Exception n)
            {
                //Написать, что очередь заполнена
            }
        }
    }
}
