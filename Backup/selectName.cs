using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TimingMadShoken
{
    public partial class selectName : Form
    {
        public class Info
        {
            public String serie;
            public String type;
            public int num;
            public String chanson;
            public String fullname;
            public String language;
            public String name;
            public String artiste;


            public Info(String fullName)
            {
                serie = "";
            type = "";
            num = 0;
            chanson = "";
            fullname = "";
            language = "";
            name = "";
            artiste = "";
                String[] splitted = fullName.Split(new Char[] {'.'});
                String cleanName = splitted[0];
                String[] parts = cleanName.Split(new Char[] { '-' });
                if (parts.Length >= 3)
                {
                    int pos = 0;
                    if (parts.Length == 4)
                    {
                        this.language = parts[pos++].Trim();
                    }
                    else
                    {
                        this.language = "";
                    }
                    this.serie = parts[pos++].Trim();
                    String typenum = parts[pos++].Trim();
                    if (typenum.Length >= 3)
                    {
                        this.type = typenum.Substring(0, 2);
                        short numvalue;
                        Int16.TryParse(typenum.Substring(2, typenum.Length - 2), out numvalue);
                        this.num = numvalue;
                    }
                    else
                    {
                        this.type = typenum;
                        this.num = 0;
                    }
                    this.chanson = parts[pos++].Trim();
                }
                generateName();
            }
            public Info(String serieArg, String typeArg, int numArg, String chansonArg, String languageArg, String artisteArg)
            {
                this.serie = serieArg;
                this.type = typeArg;
                this.num = numArg;
                this.chanson = chansonArg;
                this.language = languageArg;
                this.artiste = artisteArg;
                generateName();
            }

            private void generateName()
            {
                this.name = (serie.Length > 0 ? serie + " - " : "")
                               + (type.Length > 0 ? type : "")
                               + (num > 0 ? num.ToString() : "")
                               + ((type.Length > 0 || num > 0) ? " - " : "")
                               + (chanson.Length > 0 ? chanson : "");

                this.fullname = (language.Length > 0 ? language + " - " : "") + name;

            }
        }

        public enum Emode { NOUVEAU_TIME, CHANGE_TIME_NAME };
        private Emode mode = Emode.NOUVEAU_TIME;

        public selectName()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num = 0;
            int.TryParse(this.numerocomboBox2.Text, out num);

            Info info = new Info(this.serietextBox1.Text, this.typecomboBox1.Text
                , num
                , chansontextBox1.Text, this.languagecomboBox2.Text, this.artistetextBox.Text);


            switch (this.mode)
            {
                case Emode.CHANGE_TIME_NAME:
                    ((TimingMadShoken.mainForm)Owner).setTimeName(info);
                    break;
                case Emode.NOUVEAU_TIME:
                    ((TimingMadShoken.mainForm)Owner).nouveauTime(info);
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Info info = new Info("", "", 0, "", "", "");
            switch (this.mode)
            {

                case Emode.CHANGE_TIME_NAME:
                    ((TimingMadShoken.mainForm)Owner).setTimeName(info);
                    break;
                case Emode.NOUVEAU_TIME:
                    ((TimingMadShoken.mainForm)Owner).nouveauTime(info);
                    break;
            }


        }

        private void cancelbutton3_Click(object sender, EventArgs e)
        {
            ((TimingMadShoken.mainForm)Owner).cancelNouveauTime();

        }

        internal void setNouveauTimeMode()
        {
            this.mode = Emode.NOUVEAU_TIME;
        }

        internal void setChangeTimeName()
        {
            this.mode = Emode.CHANGE_TIME_NAME;
        }

        private void selectName_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Enabled = true;
        }

        private void selectName_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chansontextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        internal void setInfoTime(Info info)
        {
            this.serietextBox1.Text = info.serie;
            this.chansontextBox1.Text = info.chanson;
            this.languagecomboBox2.Text = info.language;
            this.numerocomboBox2.Text = info.num.ToString();
            this.typecomboBox1.Text = info.type;
        }
    }
}