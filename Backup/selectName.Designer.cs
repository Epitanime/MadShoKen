namespace TimingMadShoken
{
    partial class selectName
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
            Owner.Enabled = true;
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
            this.typecomboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chansontextBox1 = new System.Windows.Forms.TextBox();
            this.serietextBox1 = new System.Windows.Forms.TextBox();
            this.numerocomboBox2 = new System.Windows.Forms.ComboBox();
            this.languagecomboBox2 = new System.Windows.Forms.ComboBox();
            this.createbutton1 = new System.Windows.Forms.Button();
            this.nonamebutton2 = new System.Windows.Forms.Button();
            this.cancelbutton3 = new System.Windows.Forms.Button();
            this.artistetextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // typecomboBox1
            // 
            this.typecomboBox1.FormattingEnabled = true;
            this.typecomboBox1.Items.AddRange(new object[] {
            "OP",
            "ED",
            "IN",
            "OT"});
            this.typecomboBox1.Location = new System.Drawing.Point(143, 69);
            this.typecomboBox1.Name = "typecomboBox1";
            this.typecomboBox1.Size = new System.Drawing.Size(121, 21);
            this.typecomboBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nom artiste";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nom série";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Numéro";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Langue";
            // 
            // chansontextBox1
            // 
            this.chansontextBox1.Location = new System.Drawing.Point(143, 131);
            this.chansontextBox1.Name = "chansontextBox1";
            this.chansontextBox1.Size = new System.Drawing.Size(121, 20);
            this.chansontextBox1.TabIndex = 4;
            this.chansontextBox1.TextChanged += new System.EventHandler(this.chansontextBox1_TextChanged);
            // 
            // serietextBox1
            // 
            this.serietextBox1.Location = new System.Drawing.Point(144, 38);
            this.serietextBox1.Name = "serietextBox1";
            this.serietextBox1.Size = new System.Drawing.Size(121, 20);
            this.serietextBox1.TabIndex = 1;
            // 
            // numerocomboBox2
            // 
            this.numerocomboBox2.FormattingEnabled = true;
            this.numerocomboBox2.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.numerocomboBox2.Location = new System.Drawing.Point(143, 100);
            this.numerocomboBox2.Name = "numerocomboBox2";
            this.numerocomboBox2.Size = new System.Drawing.Size(121, 21);
            this.numerocomboBox2.TabIndex = 3;
            // 
            // languagecomboBox2
            // 
            this.languagecomboBox2.FormattingEnabled = true;
            this.languagecomboBox2.Items.AddRange(new object[] {
            "RUS",
            "FR",
            "ANG",
            "JAP"});
            this.languagecomboBox2.Location = new System.Drawing.Point(143, 162);
            this.languagecomboBox2.Name = "languagecomboBox2";
            this.languagecomboBox2.Size = new System.Drawing.Size(121, 21);
            this.languagecomboBox2.TabIndex = 5;
            // 
            // createbutton1
            // 
            this.createbutton1.Location = new System.Drawing.Point(35, 231);
            this.createbutton1.Name = "createbutton1";
            this.createbutton1.Size = new System.Drawing.Size(50, 23);
            this.createbutton1.TabIndex = 7;
            this.createbutton1.Text = "Créer";
            this.createbutton1.UseVisualStyleBackColor = true;
            this.createbutton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nonamebutton2
            // 
            this.nonamebutton2.Location = new System.Drawing.Point(101, 231);
            this.nonamebutton2.Name = "nonamebutton2";
            this.nonamebutton2.Size = new System.Drawing.Size(75, 23);
            this.nonamebutton2.TabIndex = 8;
            this.nonamebutton2.Text = "Pas de nom";
            this.nonamebutton2.UseVisualStyleBackColor = true;
            this.nonamebutton2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cancelbutton3
            // 
            this.cancelbutton3.Location = new System.Drawing.Point(189, 231);
            this.cancelbutton3.Name = "cancelbutton3";
            this.cancelbutton3.Size = new System.Drawing.Size(75, 23);
            this.cancelbutton3.TabIndex = 9;
            this.cancelbutton3.Text = "Annuler";
            this.cancelbutton3.UseVisualStyleBackColor = true;
            this.cancelbutton3.Click += new System.EventHandler(this.cancelbutton3_Click);
            // 
            // artistetextBox
            // 
            this.artistetextBox.Location = new System.Drawing.Point(143, 195);
            this.artistetextBox.Name = "artistetextBox";
            this.artistetextBox.Size = new System.Drawing.Size(121, 20);
            this.artistetextBox.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Nom chanson";
            // 
            // selectName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.artistetextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cancelbutton3);
            this.Controls.Add(this.nonamebutton2);
            this.Controls.Add(this.createbutton1);
            this.Controls.Add(this.languagecomboBox2);
            this.Controls.Add(this.numerocomboBox2);
            this.Controls.Add(this.serietextBox1);
            this.Controls.Add(this.chansontextBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.typecomboBox1);
            this.Name = "selectName";
            this.Text = "Selection du nom";
            this.Load += new System.EventHandler(this.selectName_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.selectName_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox typecomboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox chansontextBox1;
        private System.Windows.Forms.TextBox serietextBox1;
        private System.Windows.Forms.ComboBox numerocomboBox2;
        private System.Windows.Forms.ComboBox languagecomboBox2;
        private System.Windows.Forms.Button createbutton1;
        private System.Windows.Forms.Button nonamebutton2;
        private System.Windows.Forms.Button cancelbutton3;
        private System.Windows.Forms.TextBox artistetextBox;
        private System.Windows.Forms.Label label6;
    }
}