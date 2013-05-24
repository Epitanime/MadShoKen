using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TimingMadShoken
{
    public partial class FtpForm : Form
    {
        

        

        public enum EMode
        {
            DOWNLOAD, UPLOAD
        }

        FtpTime ftpTime = null;
        private EMode mode; 

        public FtpForm()
        {
            InitializeComponent();
            mode = EMode.UPLOAD;
        }

        public void setMode(EMode modeS)
        {
            mode = modeS;
        }

        public void setInfo(ParoleManager paroleManagerS, String videoFilePathS, String folderForReceiveS)
        {
            /*if (ftpTime != null)
            {
                ftpTime.onMessage -= new System.EventHandler(this.displayFtpMessage);
                ftpTime.onUpload -= new System.EventHandler(this.displayUploadStep);
                ftpTime.onFinish -= new System.EventHandler(this.displayFinish);
            }*/

            ftpTime = null;
            ftpTime = new FtpTime(paroleManagerS, videoFilePathS, folderForReceiveS);
            /*ftpTime.onMessage += new System.EventHandler(this.displayFtpMessage);
            ftpTime.onUpload += new System.EventHandler(this.displayUploadStep);
            ftpTime.onFinish += new System.EventHandler(this.displayFinish);*/
        }

        private void EnvoyerButton_Click(object sender, EventArgs e)
        {
            this.ftpRichTextBox.Clear();
            ftpTime.init(this.reptextBox.Text, this.serveurTextBox.Text, this.idTextBox.Text, this.passTextBox.Text);
            Thread th = null;
            if (mode == EMode.UPLOAD)
            {
                th = new Thread(new ThreadStart(this.ftpTime.sendTime));
            }
            if (mode == EMode.DOWNLOAD)
            {
                th = new Thread(new ThreadStart(this.ftpTime.getime));
            }

            if (th != null)
            {
                th.Start();
                while (th.IsAlive)
                {
                    this.progressBar1.Visible = true;

                    if (ftpTime.max > ftpTime.min)
                    {
                        this.progressBar1.Maximum = ftpTime.max;
                        this.progressBar1.Value = ftpTime.min;
                    }
                    this.transferedCapt.Text = ftpTime.min.ToString();
                    this.ftpRichTextBox.Text = ftpTime.message;
                    this.ftpRichTextBox.Refresh();
                    this.progressBar1.Refresh();
                    Application.DoEvents();
                }
                this.progressBar1.Visible = false;
                if (ftpTime.max > ftpTime.min)
                {
                    this.progressBar1.Maximum = ftpTime.max;
                    this.progressBar1.Value = ftpTime.min;
                }
                this.transferedCapt.Text = ftpTime.min.ToString();
                this.ftpRichTextBox.Text = ftpTime.message;
                this.ftpRichTextBox.Refresh();
                this.progressBar1.Refresh();
                Application.DoEvents();
            }
        }

        /*private void displayFtpMessage(object sender, EventArgs e)
        {

            this.ftpRichTextBox.AppendText(((FtpTime.FtpEventArgs) e).message);
            this.ftpRichTextBox.AppendText(Environment.NewLine);
            this.ftpRichTextBox.Refresh();
        }

        private void displayFinish(object sender, EventArgs e)
        {
            this.progressBar1.Visible = false;
        }
        
        private void displayUploadStep(object sender, EventArgs e)
        {
            this.progressBar1.Visible = true;
            this.progressBar1.Maximum = ((FtpTime.FtpUploadArgs)e).max;
            this.progressBar1.Value = ((FtpTime.FtpUploadArgs)e).pos;
            this.progressBar1.Refresh();
        }
        */
        private void fermerButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FtpForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void passTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        
    }
}