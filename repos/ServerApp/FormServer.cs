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

namespace ServerApp
{
    public partial class FormServer : Form
    {
        Server Server;
        Thread ServerWaiting;
        public FormServer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Server server = new Server(1, this);
            richTextBoxResults.Text += "12" + "\n" + server.IsPalindrom("12").ToString() + "\n";
            //richTextBoxResults.Text += "1" + "\n" + server.IsPalindrom("1").ToString() + "\n";
            //richTextBoxResults.Text += "121" + "\n" + server.IsPalindrom("121").ToString() + "\n";

        }

        private void buttonStartServer_Click(object sender, EventArgs e)
        {
            Server = new Server((int)numericUpDownN.Value, this);
            ServerWaiting = new Thread(Server.StartWaiting);//Task.Run(() => Server.StartWaiting());
            ServerWaiting.Start();

            buttonStartServer.Enabled = false;
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
            richTextBoxResults.Text += value;
            //TODO: проверить
        }

        private delegate void NameCallBack(string varText);
        public void UpdateTextBox(string input)
        {
            if (InvokeRequired)
            {
                richTextBoxResults.BeginInvoke(new NameCallBack(UpdateTextBox), new object[] { input });
            }
            else
            {
                //richTextBoxResults.Text += input;
                richTextBoxResults.Text = richTextBoxResults.Text + Environment.NewLine + input;
                this.Update();
            }
        }
    }
}
