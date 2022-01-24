using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerApp
{
    public partial class FormServer : Form
    {
        public FormServer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Server server = new Server(1);
            richTextBoxResults.Text += "123456456123" + "\n" + server.IsPalindrom("123456456123").ToString() + "\n";
            richTextBoxResults.Text += "1a'a1" + "\n" + server.IsPalindrom("1a'a1").ToString() + "\n";
            richTextBoxResults.Text += "123321" + "\n" + server.IsPalindrom("123321").ToString() + "\n";

        }
    }
}
