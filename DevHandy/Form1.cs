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
using NAudio.Wave;
using System.IO;

namespace DevHandy
{
    public partial class Form1 : Form
    {
        public string SelectedFilePath { get; set; }
        private string inputFile = string.Empty;
        private string outputFile = string.Empty;


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

        private void ConvertToMp3()
        {
            using (var reader = new AudioFileReader(inputFile))
            {
                MediaFoundationEncoder.EncodeToMp3(reader, outputFile);
            }
        }

        private void ConvertToWav()
        {
            using (var reader = new AudioFileReader(inputFile))
            {
                WaveFileWriter.CreateWaveFile(outputFile, reader);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //audio load
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
          //  openFileDialog.Filter = "Audio Files|*.wav|All Files|*.*"; // Limit input to WAV files for simplicity

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                inputFile = openFileDialog.FileName;
                label5.Text = $"Input File: {inputFile}";
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(inputFile))
            {
                MessageBox.Show("Please select an input audio file.");
                return;
            }

            string selectedFormat = comboBox2.Text; // Get the selected output format from the ComboBox

            string fileExtension = Path.GetExtension(inputFile).ToLower();

            if (fileExtension == ".wav" && comboBox2.Text == ".wav")
            {
                MessageBox.Show("Woah buddy do not convert a wav to wav.");
                return;
            }
            else if (fileExtension == ".mp3" && comboBox2.Text == ".mp3")
            {
                MessageBox.Show("Woah buddy do not convert a mp3 to mp3.");
                return;
            }

            if (string.IsNullOrEmpty(selectedFormat))
            {
                MessageBox.Show("Please select an output format.");
                return;
            }

            outputFile = System.IO.Path.ChangeExtension(inputFile, selectedFormat);

            if (selectedFormat == ".mp3")
            {
                ConvertToMp3();
            }
            else if (selectedFormat == ".wav")
            {
                ConvertToWav();
            }

            MessageBox.Show($"Conversion completed. Output file: {outputFile}");
        }
    

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
