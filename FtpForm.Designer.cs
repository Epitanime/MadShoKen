namespace TimingMadShoken
{
    partial class FtpForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        


        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.passTextBox = new System.Windows.Forms.TextBox();
            this.EnvoyerButton = new System.Windows.Forms.Button();
            this.ftpRichTextBox = new System.Windows.Forms.RichTextBox();
            this.fermerButton = new System.Windows.Forms.Button();
            this.serveurTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.reptextBox = new System.Windows.Forms.TextBox();
            this.transferedCapt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(29, 161);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(199, 13);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Identifiant";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(102, 35);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(126, 20);
            this.idTextBox.TabIndex = 3;
            this.idTextBox.TextChanged += new System.EventHandler(this.idTextBox_TextChanged);
            // 
            // passTextBox
            // 
            this.passTextBox.Location = new System.Drawing.Point(102, 62);
            this.passTextBox.Name = "passTextBox";
            this.passTextBox.Size = new System.Drawing.Size(126, 20);
            this.passTextBox.TabIndex = 4;
            this.passTextBox.UseSystemPasswordChar = true;
            this.passTextBox.TextChanged += new System.EventHandler(this.passTextBox_TextChanged);
            // 
            // EnvoyerButton
            // 
            this.EnvoyerButton.Location = new System.Drawing.Point(29, 127);
            this.EnvoyerButton.Name = "EnvoyerButton";
            this.EnvoyerButton.Size = new System.Drawing.Size(75, 23);
            this.EnvoyerButton.TabIndex = 5;
            this.EnvoyerButton.Text = "Envoyer";
            this.EnvoyerButton.UseVisualStyleBackColor = true;
            this.EnvoyerButton.Click += new System.EventHandler(this.EnvoyerButton_Click);
            // 
            // ftpRichTextBox
            // 
            this.ftpRichTextBox.Location = new System.Drawing.Point(29, 216);
            this.ftpRichTextBox.Name = "ftpRichTextBox";
            this.ftpRichTextBox.Size = new System.Drawing.Size(199, 107);
            this.ftpRichTextBox.TabIndex = 6;
            this.ftpRichTextBox.Text = "";
            // 
            // fermerButton
            // 
            this.fermerButton.Location = new System.Drawing.Point(153, 127);
            this.fermerButton.Name = "fermerButton";
            this.fermerButton.Size = new System.Drawing.Size(75, 23);
            this.fermerButton.TabIndex = 6;
            this.fermerButton.Text = "Fermer";
            this.fermerButton.UseVisualStyleBackColor = true;
            this.fermerButton.Click += new System.EventHandler(this.fermerButton_Click);
            // 
            // serveurTextBox
            // 
            this.serveurTextBox.Enabled = false;
            this.serveurTextBox.Location = new System.Drawing.Point(102, 9);
            this.serveurTextBox.Name = "serveurTextBox";
            this.serveurTextBox.Size = new System.Drawing.Size(126, 20);
            this.serveurTextBox.TabIndex = 1;
            this.serveurTextBox.Text = "ftp.epitanime.net";
            this.serveurTextBox.TextChanged += new System.EventHandler(this.serveurTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Serveur";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(26, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Repertoire";
            this.label4.Visible = false;
            // 
            // reptextBox
            // 
            this.reptextBox.Enabled = false;
            this.reptextBox.Location = new System.Drawing.Point(102, 101);
            this.reptextBox.Name = "reptextBox";
            this.reptextBox.Size = new System.Drawing.Size(126, 20);
            this.reptextBox.TabIndex = 2;
            this.reptextBox.Visible = false;
            // 
            // transferedCapt
            // 
            this.transferedCapt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transferedCapt.Location = new System.Drawing.Point(29, 177);
            this.transferedCapt.Name = "transferedCapt";
            this.transferedCapt.Size = new System.Drawing.Size(199, 21);
            this.transferedCapt.TabIndex = 10;
            this.transferedCapt.Text = "0";
            this.transferedCapt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.transferedCapt.Click += new System.EventHandler(this.label5_Click);
            // 
            // FtpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 335);
            this.Controls.Add(this.transferedCapt);
            this.Controls.Add(this.reptextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.serveurTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fermerButton);
            this.Controls.Add(this.ftpRichTextBox);
            this.Controls.Add(this.EnvoyerButton);
            this.Controls.Add(this.passTextBox);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Name = "FtpForm";
            this.Text = "Envoi FTP";
            this.Load += new System.EventHandler(this.FtpForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox passTextBox;
        private System.Windows.Forms.Button EnvoyerButton;
        private System.Windows.Forms.RichTextBox ftpRichTextBox;
        private System.Windows.Forms.Button fermerButton;
        private System.Windows.Forms.TextBox serveurTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox reptextBox;
        private System.Windows.Forms.Label transferedCapt;
    }
}