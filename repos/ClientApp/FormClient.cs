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

namespace ClientApp
{
    public partial class FormClient : Form
    {

        Thread ClientWaiting;

        public FormClient()
        {
            InitializeComponent();
        }

        private void buttonSendReq_Click(object sender, EventArgs e)
        {
            Client client = new Client(textBoxFilesPath.Text);
            string[] filesContent = client.GetStrings();
            for (int i = 0; i < filesContent.Length; i++)
            {
                richTextBoxClientResult.Text += (i + 1).ToString() + ". " + filesContent[i] + "\n";
                
                ClientWaiting = new Thread(client.SendRequests);//Task.Run(() => Server.StartWaiting());
                ClientWaiting.Start(this);
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
            richTextBoxClientResult.Text += value;
        }
    }
}
