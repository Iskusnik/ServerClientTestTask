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
        public static FormServer form;
        private enum ResultMeaning:int
        { 
            NoResult,
            True,
            False
        }

        /// <summary>
        /// Максимальное число запросов в очереди
        /// </summary>
        private int N
        {
            get;
            set;
        }

        /// <summary>
        /// Очередь запросов
        /// </summary>
        private Queue<string> RequestQue;

        /// <summary>
        /// Список клиентов по отношению к запросам
        /// </summary>
        private Queue<TcpClient> ClientQue;

        private IPAddress Address;



        public Server(int N, FormServer frmServer)
        {
            this.N = N;
            RequestQue = new Queue<string>();
            ClientQue = new Queue<TcpClient>();
            Address = IPAddress.Parse("127.0.0.1");
            form = frmServer;
        }
        public Server(int N, string Address)
        {
            this.N = N;
            RequestQue = new Queue<string>();
            this.Address = IPAddress.Parse(Address);
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

            ShowMessage(s + "CALLED FROM SERVER.CS \n");
            
            return result;
        }

        /// <summary>
        /// Отправка результата клиенту
        /// </summary>
        /// <param name="client">TCP клиент которому надо отправить сообщение</param>
        /// <param name="result">Результат проверки на палиндром</param>
        private void ReturnResult(TcpClient client, ResultMeaning result)
        {
            string msg = "";
            switch(result)
            {
                case ResultMeaning.NoResult:
                    msg = "Ошибка. Очередь заполнена";
                    break;
                case ResultMeaning.True:
                    msg = "True. Палиндром";
                    break;
                case ResultMeaning.False:
                    msg = "False. Не палиндром";
                    break;
            }
            //TODO:дописать отправку клиенту
        }

        /// <summary>
        /// Запуск сервера обработки запросов
        /// </summary>
        public void StartWaiting()
        {
            TcpListener listener = new TcpListener(Address, 8888);
            listener.Start();
            
            //Запуск обработки
            Task paliJob = Task.Run(() => StartIsPaliJob());

            while (true) 
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                try
                {
                    byte[] data = new byte[200];
                    int bytes = stream.Read(data, 0, data.Length);
                    string isPali = Encoding.UTF8.GetString(data, 0, bytes);
                    
                    
                    if (RequestQue.Count > N)
                        throw new Exception("Очередь заполнена. Повторите отправку");
                    else
                    {
                        RequestQue.Enqueue(isPali);
                        ClientQue.Enqueue(client);
                    }


                    // закрываем поток
                    stream.Close();
                    // закрываем подключение
                    client.Close();
                }
                catch (Exception n)
                {
                    ReturnResult(client, ResultMeaning.NoResult); 
                    ShowMessage(n.Message);
                }
            }
            
        }

        public void StartIsPaliJob()
        {
            //TODO:дописать асинхронную обработку запросов
            while(true)
            {
                if (RequestQue.Count > 0)
                {
                    string request = RequestQue.Dequeue();
                    TcpClient client = ClientQue.Dequeue();
                    ResultMeaning result;

                    if (IsPalindrom(request))
                        result = ResultMeaning.True;
                    else
                        result = ResultMeaning.False;

                    ReturnResult(client, result);
                }
            }
        }
        public void ShowMessage(string msg)
        {
            form.UpdateTextBox(msg);
        }
    }
}
