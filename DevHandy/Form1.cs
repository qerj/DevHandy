using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace DevHandy
{
    public partial class Form1 : Form
    {
        public string SelectedFilePath { get; set; }

        public Form1()
        {
            InitializeComponent();
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    SelectedFilePath = openFileDialog.FileName;

                    // Ensure the extra "\" after the first "\"
                    SelectedFilePath = SelectedFilePath.Replace("\\", "\\\\");

                    // Update the label to display the selected file path
                    label1.Text = SelectedFilePath;
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(SelectedFilePath == null)
            {
                MessageBox.Show("SelectedFilePath is null. Please select a file!");
                return;
            }
            Mat jpgImage = Cv2.ImRead(SelectedFilePath);
            if(jpgImage != null)
            {
                Cv2.ImWrite(SelectedFilePath + "_output" + comboBox1.Text, jpgImage);
                if (comboBox1.Text != string.Empty)
                {
                    MessageBox.Show("Image succesfully saved!");
                }
                else
                {
                    MessageBox.Show("The convert type was not selected. Please select your conversion type!");
                }
            }
            else {
                MessageBox.Show("Jpg image is null!");
            }

            //conversion part

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
