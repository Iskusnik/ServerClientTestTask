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
        Thread ServerWaiting, PaliJobWaiting;
        public FormServer()
        {
            InitializeComponent();
        }

        #region Удалить/временное
        private void button1_Click(object sender, EventArgs e)
        {
            ServerWaiting = new Thread(IsPaliTask);
            ServerWaiting.Start();
            
        }

        private void IsPaliTask()
        {
            Server server = new Server(1);
            AppendTextBox("1" + "\n" + server.IsPalindrom("1").ToString() + "\n");
            AppendTextBox("121" + "\n" + server.IsPalindrom("1").ToString() + "\n");
            AppendTextBox("123" + "\n" + server.IsPalindrom("1").ToString() + "\n");
        }
        #endregion

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
        private enum ResultMeaning : int
        {
            NoResult,
            True,
            False
        }
        const string IPadress = "127.0.0.1";

        /// <summary>
        /// Меняем текст из другого потока
        /// </summary>
        /// <param name="value"></param>
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
        private void ReturnResult(TcpClient client, ResultMeaning result)
        {

            string msg = "";
            try
            {
                NetworkStream stream = client.GetStream();

                switch (result)
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
                //Выводим результат на сервере
                AppendTextBox(client.ToString() + msg);
                //Преобразуем сообщение в массив байтов
                byte[] data = Encoding.Unicode.GetBytes(msg);
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
            try
            {
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();

                    if (Server.RequestQue.Count > Server.N)
                        throw new Exception("Очередь заполнена. Повторите отправку");
                    else
                    {
                        byte[] data = new byte[200];
                        int bytes = stream.Read(data, 0, data.Length);
                        string isPali = Encoding.UTF8.GetString(data, 0, bytes);
                        Server.RequestQue.Enqueue(isPali);
                        Server.ClientQue.Enqueue(client);
                    }

                    stream.Close();
                    client.Close();
                }
            }
            catch (Exception n)
            {
                AppendTextBox(n.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
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
                    ResultMeaning result;

                    if (Server.IsPalindrom(request))
                        result = ResultMeaning.True;
                    else
                        result = ResultMeaning.False;

                    ReturnResult(client, result);
                }
            }
        }
        #endregion
    }
}
