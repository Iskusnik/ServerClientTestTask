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

namespace ClientApp
{
    public partial class FormClient : Form
    {
        Client Client;
        Thread ClientWaiting;

        public enum ResultMeaning : int
        {
            NoResult,
            True,
            False,
            InProgress
        }
        public FormClient()
        {
            InitializeComponent();
        }

        private void buttonSendReq_Click(object sender, EventArgs e)
        {
            Client = new Client(textBoxFilesPath.Text);
            Client.GetStrings();
            for (int i = 0; i < Client.InnerFiles.Length; i++)
                richTextBoxClientResult.Text += i.ToString() + ". " + Client.InnerFiles[i] + "\n";
            

            ClientWaiting = new Thread(SendRequests);//Task.Run(() => Server.StartWaiting());
            
            ClientWaiting.Start();
        }

        private void buttonOpenFileDialog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fileDialog = new FolderBrowserDialog();
            fileDialog.ShowDialog();
            textBoxFilesPath.Text = fileDialog.SelectedPath;
        }

        /// <summary>
        /// Меняем текст из другого потока
        /// </summary>
        /// <param name="value"></param>
        public void AppendTextBoxWithSplit(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBoxWithSplit), new object[] { value });
                return;
            }
            //Убираем разделители
            string[] valueSplit = value.Split("_^_");
            value = valueSplit[0] + ". " + valueSplit[1] + ": "+ valueSplit[2];
            richTextBoxClientResult.Text +=  value + "\n";
        }
        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            richTextBoxClientResult.Text += value + "\n";
        }





        
        /// <summary>
        /// Отправка запроса на проверку файлов
        /// </summary>
        /// <param name="serverAdress">Путь/адресс к серверу</param>
        public void SendRequests()
        {
            string serverAdress = "127.0.0.3";
            TcpClient tcpClient = new TcpClient(serverAdress, 8888);
            tcpClient.ReceiveTimeout = 5000;
            bool threadNeeded = true;

            NetworkStream stream = tcpClient.GetStream();
            try
            {
                while (threadNeeded)
                {
                    RepeatedSend(stream);
                    RepeatedGet(stream);

                    foreach (ResultMeaning meaning in Client.FileInWork)
                        threadNeeded = threadNeeded &&
                            (meaning != ResultMeaning.True || meaning != ResultMeaning.False);
                }
            }
            catch (Exception e)
            {
                AppendTextBox(e.Message);
            }
        }
        

        /// <summary>
        /// Метод для обхода отправки всех файлов
        /// </summary>
        /// <param name="stream"></param>
        public void RepeatedSend(NetworkStream stream)
        {
            //отправляем для каждого файла запрос
            for (int i = 0; i < Client.InnerFiles.Length; i++)
            {
                //Если запрос не принят, то пытаемся его отправить
                if (Client.FileInWork[i] == ResultMeaning.NoResult)
                {
                    // преобразуем сообщение в массив байтов
                    // сообщение:
                    // i - индекс в массиве имеющихся строк
                    // _^_ - разделитель
                    //
                    // $$$ - конец строки
                    byte[] data = Encoding.UTF8.GetBytes(i.ToString() + "_^_" + Client.InnerFiles[i] + "$$$");
                    // отправка сообщения
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        /// <summary>
        /// Метод для обхода получения сообщений
        /// </summary>
        /// <param name="tcpClient"></param>
        public void RepeatedGet(NetworkStream stream)
        {
            // обрабатываем ответы от сервера
            string[] results = GetAnswer(stream).Split("$$$");
            for (int i = 0; i < results.Length - 1; i++)
            {
                int innerFileId = -1;
                ResultMeaning tempRes = ResultMeaning.NoResult;
                string res = TranslateAnswer(results[i], ref tempRes, ref innerFileId);
                if (innerFileId != -1)
                {
                    Client.FileInWork[innerFileId] = tempRes;
                    AppendTextBoxWithSplit(results[i]);
                }
            }
        }

        /// <summary>
        /// Ожидание ответа
        /// </summary>
        /// <returns></returns>
        public string GetAnswer(NetworkStream stream)
        {
            byte[] data = new byte[2000];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            try
            {
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                } while (stream.DataAvailable);


                string builderRes = builder.ToString();
                return builderRes;
                
            }
            catch(Exception e)
            {
                AppendTextBox(e.Message);
                return e.Message;
            }
        }

        public string TranslateAnswer(string answer, ref ResultMeaning inWork, ref int innerFileId)
        {
            inWork = ResultMeaning.NoResult;

            string[] builderResSplit = answer.Split("_^_");
            innerFileId = int.Parse(builderResSplit[0]);
            string res = builderResSplit[2];
            if (res.Length > 0)
            {
                if (res[0] == '-')
                    inWork = ResultMeaning.NoResult;
                if (res[0] == '+')
                    inWork = ResultMeaning.InProgress;
                if (res[0] == 'T')
                    inWork = ResultMeaning.True;
                if (res[0] == 'F')
                    inWork = ResultMeaning.False;
            }
            return res;
        }
    }
}
