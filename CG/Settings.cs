using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CG
{
    public partial class Settings : Form
    {
        private MainForm mainform;
        public Settings(MainForm mf)
        {
            this.mainform = mf;
            InitializeComponent();
        }

        void makeDarkTheme()
        {
            this.BackColor = Color.FromArgb(37, 37, 38);
       
            this.groupBox1.BackColor = Color.FromArgb(62, 62, 66);
            this.groupBox2.BackColor = Color.FromArgb(62, 62, 66);
            this.groupBox3.BackColor = Color.FromArgb(62, 62, 66);
            this.groupBox4.BackColor = Color.FromArgb(62, 62, 66);
            this.button1.BackColor = Color.FromArgb(62, 62, 66);
            this.button2.BackColor = Color.FromArgb(62, 62, 66);
            this.button3.BackColor = Color.FromArgb(62, 62, 66);
      
            this.groupBox1.ForeColor = Color.White;
            this.groupBox2.ForeColor = Color.White;
            this.groupBox3.ForeColor = Color.White;
            this.groupBox4.ForeColor = Color.White;
            this.button2.ForeColor = Color.White;
        }
        void makeNormalTheme()
        {
            this.BackColor = SystemColors.Control;
          
            this.groupBox1.BackColor = SystemColors.Control;
            this.groupBox2.BackColor = SystemColors.Control;
            this.groupBox3.BackColor = SystemColors.Control;
            this.groupBox4.BackColor = SystemColors.Control;
            this.button1.BackColor = SystemColors.Control;
            this.button2.BackColor = SystemColors.Control;
            this.button3.BackColor = SystemColors.Control;
            this.groupBox1.ForeColor = Color.Black;
            this.groupBox2.ForeColor = Color.Black;
            this.groupBox3.ForeColor = Color.Black;
            this.groupBox4.ForeColor = Color.Black;
            this.button2.ForeColor = Color.Black;
        }
        private void Settings_Load(object sender, EventArgs e)
        {
            label2.Text = SharedSettings.extractionPath;
            checkBox2.Checked = SharedSettings.matchCase;
            checkBox3.Checked = SharedSettings.fullSearch;
            radioButton1.Checked = SharedSettings.viewType == 0;
            radioButton2.Checked = SharedSettings.viewType ==1;
            checkBox1.Checked = SharedSettings.encType == 1;
            checkBox4.Checked = SharedSettings.darkTheme;

            if (SharedSettings.darkTheme)
                makeDarkTheme();
            else
                makeNormalTheme();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                SharedSettings.extractionPath = folderBrowserDialog1.SelectedPath;
                //label2.Text = SharedSettings.extractionPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainform.HideRevert();
            
            string settingFilePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"/" + "Settings";
            bool mc = checkBox2.Checked;
            bool fs = checkBox3.Checked;
            bool dt = checkBox4.Checked;
          
            short vt = -1;
            short et = -1;


            if (radioButton1.Checked)
                vt = 0;
            else if (radioButton2.Checked)
                vt = 1;



            if (checkBox1.Checked)
                et = 1;

           // SharedSettings.extractionPath = label2.Text;
            SharedSettings.matchCase = mc;
            SharedSettings.fullSearch = fs;
            SharedSettings.viewType = vt;
            SharedSettings.encType = et;
            SharedSettings.darkTheme = dt;

            File.WriteAllLines(settingFilePath, new string[] {
                SharedSettings.extractionPath,
                SharedSettings.CPKDirPath,
                SharedSettings.matchCase.ToString(),
                SharedSettings.fullSearch.ToString(),
                SharedSettings.viewType.ToString(),
                SharedSettings.encType.ToString(),
                SharedSettings.darkTheme.ToString(),
            }
                );
            if (vt == 0)
                mainform.LargeIconViewToolStripMenuItemClick();
            else if (vt == 1)
                mainform.SmallIconViewToolStripMenuItemClick();

            if (dt)
                mainform.makeDarkTheme();
            else
                mainform.makeNormalTheme();
            this.Close();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked){
                makeDarkTheme();
                
            }
             
            else{
                makeNormalTheme();
             
            }
               
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                SharedSettings.CPKDirPath = folderBrowserDialog1.SelectedPath;
                //label2.Text = SharedSettings.extractionPath;
            }
        }

     
    }
}
