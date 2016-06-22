using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageGrid
{

    public partial class Form1 : Form
    {

        
        public Form1()
        {
            InitializeComponent();
             
        
        }

     

        private void Form1_Load(object sender, EventArgs e)
        {
            int width = 300, height = 300;
            Bitmap histOriginal = new Bitmap(width, height);
            Bitmap histResult = new Bitmap(width, height);

            Bitmap originalImage = new Bitmap(@"C:\Users\Александр\Pictures\Unprocessed Images\"
                                               +@"boland_img78.gif", false);
            Bitmap resultImage = originalImage;

            Histogram hist = new Histogram(originalImage);

            histOriginal = hist.GetHistogram();

            Histogram result = new Histogram(hist.GetNormalizedImage(originalImage));
            histResult = result.GetHistogram();
            pictureBox1.Image = originalImage;
            pictureBox2.Image = histOriginal;
      }
    }

    public class Histogram
    {
        public int HistWidth { get; set; }
        public int HistHeight { get; set; }
        public Bitmap Image { get; set; }

        private Bitmap Hist { get; set; }

    
        public Histogram(Bitmap image)
        {
            HistHeight = image.Height;
            HistWidth = image.Width;
            this.Image = image;
            this.Hist = new Bitmap(image, 256, 256);

           
        }

        public Bitmap GetNormalizedImage(Bitmap img)
        {
            Bitmap normImg = new Bitmap(img, img.Height, img.Width);

            Byte[] pixelIntensityArray = new Byte[256];

            //calculating number of pixels with intensity value of i( from 0 to 255) 
            //and recording this number to i-element of pixelIntensityArray 

            for (int i = 0; i < img.Height - 1; i++)
            {
                for (int j = 0; j < img.Width - 1; j++)
                {
                    Color pixel = img.GetPixel(j, i);
                    pixelIntensityArray[pixel.R]++;
                }
            }

            Byte[] normalizedPixelIntensityArray = new Byte[256];
            Byte sum = 0;
            for (int i = 0; i < pixelIntensityArray.Length; i++)
            {
                if (i == 0)
                {
                    sum = 0;
                }
                else
                {
                    sum = normalizedPixelIntensityArray[i - 1];
                }
                normalizedPixelIntensityArray[i] = (Byte)(255 * (double)pixelIntensityArray[i] / (double)(img.Width * img.Height));
                normalizedPixelIntensityArray[i] += sum;
                if (i == 36) 
                {
                    MessageBox.Show("Ok");
                };
            }

            for (int i = 0; i < normImg.Height - 1; i++)
            {
                for (int j = 0; j < normImg.Width - 1; j++)
                {
                    Color tempPix = normImg.GetPixel(j, i);
                    Byte pixIntensity = tempPix.R;

                    byte r = normalizedPixelIntensityArray[pixIntensity];
                    byte g = r;
                    byte b = r;

                    
                    normImg.SetPixel(j, i, Color.FromArgb(255, r, g, b));
                }
            }

            return normImg;
        }
        public Bitmap GetHistogram()
        {
                 Byte[] pixelIntensityArray = new Byte[256];

                 //calculating number of pixels with intensity value of i( from 0 to 255) 
                 //and recording this number to i-element of pixelIntensityArray 

                 for (int i = 0; i < Image.Height - 1; i++)
                 {
                     for (int j = 0; j < Image.Width - 1; j++)
                     {
                         Color pixel = Image.GetPixel(j, i);
                         pixelIntensityArray[pixel.R]++;
                     }
                 }

          
            //making histogram picture from  Bitmap image
            for (int i = 0; i < 255; i++ )
            {
                for (int j = 0; j < 255; j++ )
                {
                    if (j < 255-pixelIntensityArray[i] )
                    {
                        Hist.SetPixel(i, j, Color.White);
                    }
                    else
                    {
                        Hist.SetPixel(i, j, Color.Black);
                    }
                }
            }
            return Hist;
        }
    }
}
