using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace CG
{
    public partial class CreateM2C : Form
    {
      
        string modDir = "";
        char[] delimiters = new char[] { (char)0x5C };
        public CreateM2C()
        {
            InitializeComponent();
        }

        void makeDarkTheme()
        {
            this.BackColor = Color.FromArgb(37, 37, 38);
            this.groupBox1.BackColor = Color.FromArgb(62, 62, 66);
            this.groupBox1.ForeColor = Color.White;
            label1.BackColor = Color.FromArgb(62, 62, 66);
            label1.ForeColor = Color.White;
            textBox1.BackColor = Color.FromArgb(62, 62, 66);
            textBox1.ForeColor = Color.White;
            button1.BackColor = Color.FromArgb(62, 62, 66);
            button1.ForeColor = Color.White;
            button2.BackColor = Color.FromArgb(62, 62, 66);
            button2.ForeColor = Color.White;
        }

        void makeNormalTheme()
        {
            this.BackColor = SystemColors.Control;
            this.groupBox1.BackColor = SystemColors.Control;
            this.groupBox1.ForeColor = Color.Black;
            label1.BackColor = SystemColors.Control;
            label1.ForeColor = Color.Black;
            textBox1.BackColor = SystemColors.Control;
            textBox1.ForeColor = Color.Black;
            button1.BackColor = SystemColors.Control;
            button1.ForeColor = Color.Black;
            button2.BackColor = SystemColors.Control;
            button2.ForeColor = Color.Black;
        }

        private void CreateM2C_Load(object sender, EventArgs e)
        {
            if (SharedSettings.darkTheme)
                makeDarkTheme();
            else
                makeNormalTheme();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ( modDir != "")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ZipFile.CreateFromDirectory(modDir, saveFileDialog1.FileName, CompressionLevel.Fastest, true);
                    MessageBox.Show("M2C created successfully");
                }
                  
            }
            else
            {
                MessageBox.Show("Set Mod Directory first");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //cpkTarget = openFileDialog1.SafeFileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string TargetCPK;
                modDir = folderBrowserDialog1.SelectedPath;
            
                string[] modSplit = (modDir).Split(delimiters);
                TargetCPK = (modDir).Split(delimiters)[modSplit.Length - 1] + ".cpk";

                textBox1.Text = TargetCPK;
            }
        }
    }
}
