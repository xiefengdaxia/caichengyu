using asprise_ocr_api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
namespace CaiChengYu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
        }

        private void btnOcrRead_Click(object sender, EventArgs e)
        {

            
           //textResults.Text = Marshal.PtrToStringAnsi(OCR(textImage.Text, -1));

            AspriseOCR.SetUp();
            AspriseOCR ocr = new AspriseOCR();
            ocr.StartEngine("eng", AspriseOCR.SPEED_FASTEST);

            string s = ocr.Recognize(textImage.Text, -1, -1, -1, -1, -1,
           AspriseOCR.RECOGNIZE_TYPE_ALL, AspriseOCR.OUTPUT_FORMAT_PLAINTEXT);
           textResults.Text=s;
            // process more images here ...

            ocr.StopEngine();
        }

        private void btnChooseImg_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            DialogResult dr = openFileDialog1.ShowDialog(this);


            textImage.Text = openFileDialog1.FileName;
            if (dr == DialogResult.OK)
            {

                picSource.Image = Image.FromFile(openFileDialog1.FileName);

            }


        }
    }
}
