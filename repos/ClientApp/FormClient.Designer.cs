
namespace ClientApp
{
    partial class FormClient
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.richTextBoxClientResult = new System.Windows.Forms.RichTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.textBoxFilesRout = new System.Windows.Forms.TextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.buttonOpenFileDialog = new System.Windows.Forms.Button();
            this.buttonSendReq = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.richTextBoxClientResult);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(678, 373);
            this.splitContainer1.SplitterDistance = 344;
            this.splitContainer1.TabIndex = 0;
            // 
            // richTextBoxClientResult
            // 
            this.richTextBoxClientResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxClientResult.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxClientResult.Name = "richTextBoxClientResult";
            this.richTextBoxClientResult.ReadOnly = true;
            this.richTextBoxClientResult.Size = new System.Drawing.Size(678, 344);
            this.richTextBoxClientResult.TabIndex = 0;
            this.richTextBoxClientResult.Text = "";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.textBoxFilesRout);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(678, 25);
            this.splitContainer2.SplitterDistance = 438;
            this.splitContainer2.TabIndex = 0;
            // 
            // textBoxFilesRout
            // 
            this.textBoxFilesRout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFilesRout.Location = new System.Drawing.Point(0, 0);
            this.textBoxFilesRout.Name = "textBoxFilesRout";
            this.textBoxFilesRout.ReadOnly = true;
            this.textBoxFilesRout.Size = new System.Drawing.Size(438, 23);
            this.textBoxFilesRout.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.buttonOpenFileDialog);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.buttonSendReq);
            this.splitContainer3.Size = new System.Drawing.Size(236, 25);
            this.splitContainer3.SplitterDistance = 78;
            this.splitContainer3.TabIndex = 0;
            // 
            // buttonOpenFileDialog
            // 
            this.buttonOpenFileDialog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOpenFileDialog.Location = new System.Drawing.Point(0, 0);
            this.buttonOpenFileDialog.Name = "buttonOpenFileDialog";
            this.buttonOpenFileDialog.Size = new System.Drawing.Size(78, 25);
            this.buttonOpenFileDialog.TabIndex = 4;
            this.buttonOpenFileDialog.Text = "Выбрать файл";
            this.buttonOpenFileDialog.UseVisualStyleBackColor = true;
            // 
            // buttonSendReq
            // 
            this.buttonSendReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSendReq.Location = new System.Drawing.Point(0, 0);
            this.buttonSendReq.Name = "buttonSendReq";
            this.buttonSendReq.Size = new System.Drawing.Size(154, 25);
            this.buttonSendReq.TabIndex = 2;
            this.buttonSendReq.Text = "Отправить";
            this.buttonSendReq.UseVisualStyleBackColor = true;
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 373);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormClient";
            this.Text = "Клиент:";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox richTextBoxClientResult;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox textBoxFilesRout;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button buttonOpenFileDialog;
        private System.Windows.Forms.Button buttonSendReq;
    }
}

