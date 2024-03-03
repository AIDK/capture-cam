using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Windows.Forms;

namespace CameraDemo
{
    public partial class Form1 : Form
    {
        private Capture _capture;
        private bool _streaming;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
                pictureBox1.Image = pictureBox2.Image;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!_streaming)
            {
                Application.Idle += streaming;
                button2.Text = @"End Streaming";
            }
            else
            {
                Application.Idle -= streaming;
                pictureBox2.Image = null;
                button2.Text = @"Start Streaming";
            }

            _streaming = !_streaming;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
                return;

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = @"JPG|*.jpg";
                dialog.Title = @"Save an Image File";
                dialog.FileName = string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), ".jpg");
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(dialog.FileName);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _streaming = false;
            _capture = new Capture();
        }

        private void streaming(object sender, EventArgs e)
        {
            var img = _capture.QueryFrame().ToImage<Bgr, byte>();
            var bmp = img.Bitmap;
            pictureBox2.Image = bmp;
        }
    }
}