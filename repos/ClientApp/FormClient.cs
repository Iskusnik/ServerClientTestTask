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

        public FormClient()
        {
            InitializeComponent();
        }

        private void buttonSendReq_Click(object sender, EventArgs e)
        {
            Client = new Client(textBoxFilesPath.Text);
            string[] filesContent = Client.GetStrings();
            for (int i = 0; i < filesContent.Length; i++)
            {
                richTextBoxClientResult.Text += (i + 1).ToString() + ". " + filesContent[i] + "\n";
                
                ClientWaiting = new Thread(SendRequests);//Task.Run(() => Server.StartWaiting());
                ClientWaiting.Start();
            }
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
        public void SendRequests(object form)
        {
            FormClient formClient = (FormClient)form;
            string serverAdress = "127.0.0.1";
            TcpClient tcpClient = new TcpClient(serverAdress, 8888);
            try
            {
                for (int i = 0; i < Client.InnerFiles.Length; i++)
                {
                    bool noRes = true;
                    while (noRes)
                    {
                        NetworkStream stream = tcpClient.GetStream();
                        // преобразуем сообщение в массив байтов
                        byte[] data = Encoding.Unicode.GetBytes(Client.InnerFiles[i]);
                        // отправка сообщения
                        stream.Write(data, 0, data.Length);

                        string result = Client.InnerFiles[i] + " " + GetAnswer(tcpClient, ref noRes);

                        AppendTextBox(result);
                    }
                }
            }
            catch (Exception e)
            {
                AppendTextBox(e.Message);
            }
            finally
            {
                tcpClient.Close();
            }
        }


        /// <summary>
        /// Ожидание ответа
        /// </summary>
        /// <returns></returns>
        public string GetAnswer(TcpClient tcpClient, ref bool haveRes)
        {
            NetworkStream stream = tcpClient.GetStream();

            byte[] data = new byte[200];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            try
            {
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);

                string res = builder.ToString();
                if (res.Length > 0 && (res[0] == 'F' || res[0] == 'T'))
                    haveRes = true;
                else
                    haveRes = false;
                return res;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
