using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adolfo_Ponte_viewer
{
    public partial class fullScreen : Form
    {
        Form1 form1 = new Form1();
        public static int cont;
        List<String> imagem = new List<string>(); 
        public int exbirFullScreen(List<String> imagens, int index)
        {
            try
            {
                
                cont = index;
                pictureBox1.Image.Dispose();
                pictureBox1.Dispose();
                
            }
            catch { }
            imagem.AddRange(imagens);

            InitializeComponent();
            pictureBox1.ClientSize = new Size(fullScreen.ActiveForm.Width, fullScreen.ActiveForm.Height);
            pictureBox1.Image = mostrarImagem(imagens[index]);
            return cont;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void fullScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Space)
            {
                //pictureBox1.Image.Dispose();
                //fullScreen.ActiveForm.Dispose();
                fullScreen.ActiveForm.Close();

                
            }
            if (e.KeyCode == Keys.F7)
            {
                //pictureBox1.Image.Dispose();
                //fullScreen.ActiveForm.Dispose();
                fullScreen.ActiveForm.Close();


            }
            else if (e.KeyCode == Keys.Right)
            {
                pictureBox1.Image.Dispose();
                try
                {
                    
                    cont++;
                    pictureBox1.Image = pictureBox1.Image = mostrarImagem(imagem[cont]);
                }
                catch {
                    cont=0;
                    pictureBox1.Image = pictureBox1.Image = mostrarImagem(imagem[cont]);
                }
            }
            else if (e.KeyCode == Keys.Left)
            {

                pictureBox1.Image.Dispose();
                try
                {
                    cont--;
                    pictureBox1.Image = pictureBox1.Image = mostrarImagem(imagem[cont]);
                }
                catch
                {
                    cont=imagem.Count-1;
                    pictureBox1.Image = pictureBox1.Image = mostrarImagem(imagem[cont]);
                }


            }

        }
        public Image mostrarImagem(String s)
        {
            double altura = 0.0;
            double largura = 0.0;
            double aspectRatio = 0.0;
            int px = 0;
            Bitmap img = new Bitmap(s);
            Bitmap pic = new Bitmap(fullScreen.ActiveForm.Width, fullScreen.ActiveForm.Height);
            
            if (img.Width > img.Height)
            {
                
                aspectRatio = (double)img.Width / img.Height;
                altura = fullScreen.ActiveForm.Height;
                largura = (double)altura * aspectRatio;
                px = (int)(fullScreen.ActiveForm.Width - (int)largura) / 2;
                
                
            }
            else if (img.Width < img.Height)
            {
                
                aspectRatio = (double)img.Height / img.Width;
                altura = fullScreen.ActiveForm.Height;
                largura = (double)altura / aspectRatio;
                px = (int)(fullScreen.ActiveForm.Width - (int)largura) / 2;
            }

            
            using (Graphics g = Graphics.FromImage(pic))
            {
                g.DrawImage(img, new Rectangle(px, 0, (int)largura, (int)altura));
            }
            

            img.Dispose();
            return pic;
        }
    }
}
