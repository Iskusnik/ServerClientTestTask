using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class FormClient : Form
    {
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
                //отправка запроса и результат
            }
        }

        private void buttonOpenFileDialog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fileDialog = new FolderBrowserDialog();
            fileDialog.ShowDialog();
            textBoxFilesPath.Text = fileDialog.SelectedPath;
        }
    }
}
