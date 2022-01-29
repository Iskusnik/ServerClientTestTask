using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace ServerApp
{
    public partial class FormServer : Form
    {
       
        Server Server;
        //Треды для обработки запросов
        //ServerWaiting - принимает клиентов и записывает их запросы в очередь
        //PaliJobWaiting - смотрит в очередь запросов и обрабатывает их
        Thread ServerWaiting, PaliJobWaiting;
        public FormServer()
        {
            InitializeComponent();
        }


        private void buttonStartServer_Click(object sender, EventArgs e)
        {
            Server = new Server((int)numericUpDownN.Value);

            ServerWaiting = new Thread(StartWaiting);
            ServerWaiting.Start();

            PaliJobWaiting = new Thread(StartIsPaliJob);
            PaliJobWaiting.Start();
            
            buttonStartServer.Enabled = false;
        }




        #region Работа в другом потоке
        public enum ResultMeaning : int
        {
            NoResult,
            True,
            False,
            InProgress
        }
        const string IPadress = "127.0.0.3";

        /// <summary>
        /// Меняем текст из другого потока
        /// </summary>
        /// <param name="value"></param>
        public void AppendTextBoxWithSplit(string value)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(AppendTextBoxWithSplit), new object[] { value });
                return;
            }
            //Убираем разделители
            string[] valueSplit = value.Split("$$$")[0].Split("_^_");
            value = valueSplit[0] + ". " + valueSplit[1] + ": " + valueSplit[2];
            richTextBoxResults.Text += value + "\n";
        }
        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            richTextBoxResults.Text += value + "\n";
        }

        /// <summary>
        /// Отправка результата клиенту
        /// </summary>
        /// <param name="client">TCP клиент которому надо отправить сообщение</param>
        /// <param name="result">Результат проверки на палиндром</param>
        private void ReturnResult(NetworkStream stream, ResultMeaning result, string request)
        {
            string msg = request + "_^_";
            try
            {
                switch (result)
                {
                    case ResultMeaning.NoResult:
                        msg += "-Ошибка. Очередь заполнена.";
                        break;
                    case ResultMeaning.True:
                        msg += "True. Палиндром";
                        break;
                    case ResultMeaning.False:
                        msg += "False. Не палиндром";
                        break;
                    case ResultMeaning.InProgress:
                        msg += "+Запрос принят";
                        break;
                }
                msg += "$$$";

                //Выводим результат на сервере
                AppendTextBoxWithSplit("Отправлено: " + msg + "\n");
                //Преобразуем сообщение в массив байтов
                byte[] data = Encoding.UTF8.GetBytes(msg);
                //Отправляем сообщение
                
                stream.Write(data, 0, data.Length);
            }
            catch (System.ObjectDisposedException)
            {
                AppendTextBox("Потеряна связь с клиентом");
            }
        }

        /// <summary>
        /// Запуск принятия запросов
        /// </summary>
        public void StartWaiting()
        {
            IPAddress addressIP = IPAddress.Parse(IPadress);
            TcpListener listener = new TcpListener(addressIP, 8888);
            listener.Start();
            AppendTextBox("Сервер ожидает запросы");
            List<Thread> clientThreads = new List<Thread>();

            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Thread newClient = new Thread(ClientWork);
                    clientThreads.Add(newClient);
                    newClient.Start(client);
                }
                catch (Exception n)
                {
                    AppendTextBox(n.Message);
                }
            }
        }

        /// <summary>
        /// Работа с отдельным клиентом
        /// </summary>
        public void ClientWork(object clientData)
        {
            TcpClient client = (TcpClient)clientData;
            NetworkStream stream = client.GetStream();
            while (true)
            {
                try
                {
                    
                    byte[] data = new byte[200];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;

                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);



                    string encodedStr = builder.ToString();
                    AppendTextBox(encodedStr);
                    string[] isPalis = encodedStr.Split("$$$");

                    for (int i = 0; i < isPalis.Length - 1; i++)
                    {
                        if (Server.RequestQue.Count >= Server.N)
                        {
                            ReturnResult(stream, ResultMeaning.NoResult, isPalis[i]);
                            AppendTextBox("Очередь заполнена. Повторите отправку");
                            //Добавляем ожидание освобождения рабочей очереди
                            Thread.Sleep(500);
                        }
                        else
                        {
                            Server.RequestQue.Enqueue(isPalis[i]);
                            Server.ClientQue.Enqueue(client);
                            Server.StreamQue.Enqueue(stream);
                            ReturnResult(stream, ResultMeaning.InProgress, isPalis[i]);
                        }
                    }
                }
                catch (Exception n)
                {
                    AppendTextBox(n.Message);
                }
            }
        }

        /// <summary>
        /// Запуск обработки запросов - проверка на свойство палиндрома.
        /// </summary>
        public void StartIsPaliJob()
        {
            while (true)
            {
                if (Server.RequestQue.Count > 0)
                {
                    string request = Server.RequestQue.Dequeue();
                    TcpClient client = Server.ClientQue.Dequeue();
                    NetworkStream stream = Server.StreamQue.Dequeue();

                    ResultMeaning result;

                    string[] requestSplit = request.Split("_^_");

                    if (Server.IsPalindrom(requestSplit[1]))
                        result = ResultMeaning.True;
                    else
                        result = ResultMeaning.False;

                    ReturnResult(stream, result, request);
                }
            }
        }
        #endregion
    }
}
