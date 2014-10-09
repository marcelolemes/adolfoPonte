using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ps = Photoshop;
namespace Adolfo_Ponte_viewer
{
    public partial class Form1 : Form
    {
        public static List<String> imagens = new List<String>();
        int cont;
        int unidade;
        double altura;
        double aspectRatio;
        double largura;
        String nomePasta;
        String nomeAlbum;
        List<String> parcial = new List<String>();
        List<ListViewItem> remover = new List<ListViewItem>();
        String nomeRemovida;
        public static int i;
        Bitmap pasta = new Bitmap("pasta.ico");
        ImageList pastas = new ImageList();
        ImageList imageList1 = new ImageList();
        public Form1()
        {
            InitializeComponent();

            //treeView1.ExpandAll();
            this.WindowState = FormWindowState.Maximized;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {

        }


        private void carregarMiniaturas_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                cont = 0;
                imageList1.Images.Clear();
                listView1.Items.Clear();
            }
            catch { }

            unidade = 100;

            Parallel.For(0, imagens.Count, delegate(int count)
               {
                   if (carregarMiniaturas.CancellationPending)
                   {
                       e.Cancel = true;
                   }
                   else
                   {
                       try
                       {
                           Bitmap img = new Bitmap(imagens[count]);
                           Bitmap imgFinal = new Bitmap(unidade, unidade);
                           double p_x;
                           double p_y;

                           if (img.Width > img.Height)
                           {
                               aspectRatio = (double)img.Width / img.Height;
                               largura = unidade;
                               altura = (double)unidade / aspectRatio;
                               p_y = (double)(unidade - altura) / 2;
                               using (Graphics g = Graphics.FromImage(imgFinal))
                               {
                                   g.DrawImage(img, new Rectangle(0, (int)p_y, (int)(largura), (int)(altura)));
                               }
                           }

                           if (img.Width < img.Height)
                           {
                               aspectRatio = (double)img.Height / img.Width;
                               largura = (double)unidade / aspectRatio;
                               altura = unidade;
                               p_x = (double)((unidade - largura) / 2);
                               using (Graphics g = Graphics.FromImage(imgFinal))
                               {
                                   g.DrawImage(img, new Rectangle((int)p_x, 0, (int)(largura), (int)(altura)));
                               }
                           }
                           if (listView1.InvokeRequired)
                               Parallel.Invoke(() =>
                               {
                                   listView1.Invoke(new MethodInvoker(delegate
                               {
                                   imageList1.Images.Add(imgFinal);

                               }));
                               });
                           else
                               imageList1.Images.Add(imgFinal);
                           img.Dispose();

                           ListViewItem item = new ListViewItem(); //Nomes trocados

                           item.ImageIndex = count;
                           item.Text = count.ToString();// Path.GetFileNameWithoutExtension(imagens[count]);
                           //   item.Name = imagens[count];
                           if (listView1.InvokeRequired)
                               Parallel.Invoke(() =>
                               {
                                   listView1.Invoke(new MethodInvoker(delegate
                                   {
                                       listView1.Items.Add(item);

                                   }));
                               });
                           else
                               listView1.Items.Add(item);


                           carregarMiniaturas.ReportProgress(100 / (imagens.Count / count));
                       }
                       catch { }

                   }

                   if (listView1.InvokeRequired)
                       Parallel.Invoke(() =>
                       {
                           listView1.Invoke(new MethodInvoker(delegate
                           {
                               imageList1.ImageSize = new Size(unidade, unidade);
                               imageList1.ColorDepth = ColorDepth.Depth24Bit;
                               listView1.LargeImageList = imageList1;

                           }));
                       });

                   else
                   {
                       imageList1.ImageSize = new Size(unidade, unidade);
                       imageList1.ColorDepth = ColorDepth.Depth24Bit;
                       listView1.LargeImageList = imageList1;
                   }

               });




            /*    Parallel.For(0,imagens.Count-1, delegate (int count)
                {
                    if (carregarMiniaturas.CancellationPending)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        try
                        {
                            Bitmap img = new Bitmap(imagens[count]);
                            Bitmap imgFinal = new Bitmap(unidade, unidade);
                            double p_x;
                            double p_y;

                            if (img.Width > img.Height)
                            {
                                aspectRatio = (double)img.Width / img.Height;
                                largura = unidade;
                                altura = (double)unidade / aspectRatio;
                                p_y = (double)(unidade - altura) / 2;
                                using (Graphics g = Graphics.FromImage(imgFinal))
                                {
                                    g.DrawImage(img, new Rectangle(0, (int)p_y, (int)(largura), (int)(altura)));
                                }
                            }

                            if (img.Width < img.Height)
                            {
                                aspectRatio = (double)img.Height / img.Width;
                                largura = (double)unidade / aspectRatio;
                                altura = unidade;
                                p_x = (double)((unidade - largura) / 2);
                                using (Graphics g = Graphics.FromImage(imgFinal))
                                {
                                    g.DrawImage(img, new Rectangle((int)p_x, 0, (int)(largura), (int)(altura)));
                                }
                            }
                            if (listView1.InvokeRequired)
                                Parallel.Invoke(() =>
                                {
                                    listView1.Invoke(new MethodInvoker(delegate
                                {
                                    imageList1.Images.Add(imgFinal);
                                
                                }));
                                });
                            else
                                imageList1.Images.Add(imgFinal);
                            img.Dispose();

                            ListViewItem item = new ListViewItem(); //Nomes trocados

                            item.ImageIndex = count;
                            item.Text = count.ToString();// Path.GetFileNameWithoutExtension(imagens[count]);
                         //   item.Name = imagens[count];
                            if (listView1.InvokeRequired)
                                Parallel.Invoke(() =>
                                {
                                    listView1.Invoke(new MethodInvoker(delegate
                                    {
                                        listView1.Items.Add(item);

                                    }));
                                });
                            else
                                listView1.Items.Add(item);


                            carregarMiniaturas.ReportProgress(100 / (imagens.Count / count));
                        }
                        catch { }

                    }

                    if (listView1.InvokeRequired)
                        Parallel.Invoke(() =>
                        {
                            listView1.Invoke(new MethodInvoker(delegate
                            {
                                imageList1.ImageSize = new Size(unidade, unidade);
                                imageList1.ColorDepth = ColorDepth.Depth24Bit;
                                listView1.LargeImageList = imageList1;

                            }));
                        });

                    else
                    {
                        imageList1.ImageSize = new Size(unidade, unidade);
                        imageList1.ColorDepth = ColorDepth.Depth24Bit;
                        listView1.LargeImageList = imageList1;
                    }

                    */




            /*
            foreach (String s in imagens)
            {
                if (carregarMiniaturas.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    try
                    {
                        Bitmap img = new Bitmap(s);
                        Bitmap imgFinal = new Bitmap(unidade, unidade);
                        double p_x;
                        double p_y;

                        if (img.Width > img.Height)
                        {
                            aspectRatio = (double)img.Width / img.Height;
                            largura = unidade;
                            altura = (double)unidade / aspectRatio;
                            p_y = (double)(unidade - altura) / 2;
                            using (Graphics g = Graphics.FromImage(imgFinal))
                            {
                                g.DrawImage(img, new Rectangle(0, (int)p_y, (int)(largura), (int)(altura)));
                            }
                        }

                        if (img.Width < img.Height)
                        {
                            aspectRatio = (double)img.Height / img.Width;
                            largura = (double)unidade / aspectRatio;
                            altura = unidade;
                            p_x = (double)((unidade - largura) / 2);
                            using (Graphics g = Graphics.FromImage(imgFinal))
                            {
                                g.DrawImage(img, new Rectangle((int)p_x, 0, (int)(largura), (int)(altura)));
                            }
                        }
                        if (listView1.InvokeRequired)
                            listView1.Invoke(new MethodInvoker(delegate
                            {
                                imageList1.Images.Add(imgFinal);

                            }));
                        else
                            imageList1.Images.Add(imgFinal);
                        img.Dispose();

                        ListViewItem item = new ListViewItem();
                        item.ImageIndex = cont;
                        item.Text = Path.GetFileNameWithoutExtension(imagens[cont]);
                        item.Name = imagens[cont];

                        if (listView1.InvokeRequired)
                            listView1.Invoke(new MethodInvoker(delegate
                            {
                                listView1.Items.Add(item);

                            }));
                        else
                            listView1.Items.Add(item);
                        cont++;

                        carregarMiniaturas.ReportProgress(100 / (imagens.Count / cont));
                    }
                    catch { }

                }
                if (listView1.InvokeRequired)
                    listView1.Invoke(new MethodInvoker(delegate
                    {
                        imageList1.ImageSize = new Size(unidade, unidade);
                        imageList1.ColorDepth = ColorDepth.Depth24Bit;
                        listView1.LargeImageList = imageList1;

                    }));
                else
                {
                    imageList1.ImageSize = new Size(unidade, unidade);
                    imageList1.ColorDepth = ColorDepth.Depth24Bit;
                    listView1.LargeImageList = imageList1;
                }
              
        });


          */



        }

        private void carregarMiniaturas_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage < 100)
                label1.Text = "Carregando...";
            else
                label1.Text = "" + imagens.Count + " imagens";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            ListView lvTemp = (ListView)sender;



            nomeRemovida = "";
            if (e.KeyCode == Keys.Space)
            {

                if (lvTemp.SelectedItems.Count > 0)
                {

                    fullScreen full = new fullScreen();
                    full = new fullScreen();
                    full.WindowState = FormWindowState.Maximized;
                    full.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    full.Show();
                    full.exbirFullScreen(imagens, lvTemp.SelectedItems[0].ImageIndex);

                }
            }

            else if (e.KeyCode == Keys.Return)
            {
                if (lvTemp.SelectedItems.Count > 0)
                {
                    try
                    {
                        ps.ApplicationClass app = new ps.ApplicationClass();
                        app.Open(lvTemp.SelectedItems[0].Name);
                        System.Diagnostics.Process.Start("Photoshop.exe");
                        this.WindowState = FormWindowState.Minimized;
                    }
                    catch { }
                }
                //System.Diagnostics.Process.Start("Photoshop.exe", lvTemp.Items[i].Name);
            }
            else if (e.KeyCode == Keys.F7)
            {
                removerFotos(lvTemp);
            }


        }

        private void pathTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void pathTxt_DragEnter(object sender, DragEventArgs e)
        {
            Array a = (Array)e.Data.GetData(DataFormats.FileDrop);

            if (carregarMiniaturas.IsBusy != true)
            {
                try
                {
                    imagens.Clear();
                    imageList1.Images.Clear();
                    listView1.Items.Clear();
                }
                catch { }

                pathTxt.Text = a.GetValue(0).ToString();
                imagens.AddRange(Directory.GetFiles(pathTxt.Text, "*.jpg"));
                carregarMiniaturas.RunWorkerAsync();
            }
        }

        private void pathTxt_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Return & pathTxt.Text.Length > 5)
            {
                try
                {
                    label2.Text = pathTxt.Text;
                    pathTxt.Visible = false;
                    label2.Visible = true;
                    imagens.Clear();
                    imageList1.Images.Clear();
                    listView1.Items.Clear();
                }
                catch { }
                if (carregarMiniaturas.IsBusy != true)
                {

                    imagens.AddRange(Directory.GetFiles(pathTxt.Text, "*.jpg"));
                    refreshBtn_Click(sender, e);
                    if (treeView1.Nodes.Count > 0)
                    { }
                    else
                    {
                        PopularArvore.RunWorkerAsync();
                    }

                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            pathTxt.Visible = true;
        }

        private void pathTxt_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = true;
            pathTxt.Visible = false;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {

        }

        private void PopulateTreeView()
        {
            TreeNode rootNode;



            DirectoryInfo info = new DirectoryInfo(Path.GetPathRoot(pathTxt.Text));

            if (info.Exists)
            {
                // MessageBox.Show(Path.GetPathRoot(pathTxt.Text));
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                rootNode.Name = info.FullName;
                if (treeView1.Nodes.Contains(rootNode))
                {

                }
                else
                {
                    GetDirectories(info.GetDirectories(), rootNode);

                    if (treeView1.InvokeRequired)
                        treeView1.Invoke(new MethodInvoker(delegate
                        {
                            treeView1.Nodes.Add(rootNode);
                        }));
                    else
                        treeView1.Nodes.Add(rootNode);
                }

            }


        }

        private void GetDirectories(DirectoryInfo[] subDirs,
            TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            /*     foreach (DirectoryInfo subDir in subDirs)
                 {
                     aNode = new TreeNode(subDir.Name, 0, 0);
                     aNode.Tag = subDir;
                     aNode.Name = subDir.FullName;
                     //aNode.ImageKey = "folder";
                     try
                     {
                         subSubDirs = subDir.GetDirectories();
                         if (subSubDirs.Length != 0)
                         {
                             GetDirectories(subSubDirs, aNode);
                         }
                         if (treeView1.InvokeRequired)
                             treeView1.Invoke(new MethodInvoker(delegate
                             {
                                 nodeToAddTo.Nodes.Add(aNode);
                             }));
                         else
                         nodeToAddTo.Nodes.Add(aNode);
                     }
                     catch
                     {

                     }

                 }*/

            if (treeView1.InvokeRequired)
                treeView1.Invoke(new MethodInvoker(delegate
                {
                    pastas.Images.Add(pasta);
                    treeView1.ImageList = pastas;
                }));
            else
            {
                pastas.Images.Add(pasta);
                treeView1.ImageList = pastas;
            }
            Parallel.ForEach(subDirs, subDir =>
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                aNode.Name = subDir.FullName;
                aNode.ImageIndex = 0;

                try
                {
                    subSubDirs = subDir.GetDirectories();
                    if (subSubDirs.Length != 0)
                    {
                        GetDirectories(subSubDirs, aNode);
                    }
                    if (treeView1.InvokeRequired)
                        treeView1.Invoke(new MethodInvoker(delegate
                        {
                            nodeToAddTo.Nodes.Add(aNode);
                        }));
                    else
                        nodeToAddTo.Nodes.Add(aNode);
                }
                catch
                {

                }

            });
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {

        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (carregarMiniaturas.IsBusy)
            {
                carregarMiniaturas.CancelAsync();

            }

            TreeNode mySelectedNode = treeView1.GetNodeAt(e.X, e.Y);

            if (mySelectedNode != null)
            {
                if (carregarMiniaturas.IsBusy != true)
                {
                    pathTxt.Text = mySelectedNode.Name;

                    try
                    {
                        pathTxt.Text = Path.GetFullPath(pathTxt.Text);
                        label2.Text = pathTxt.Text;
                        pathTxt.Visible = false;
                        label2.Visible = true;
                        imagens.Clear();
                        imageList1.Images.Clear();
                        listView1.Items.Clear();
                    }
                    catch { }
                    imagens.AddRange(Directory.GetFiles(mySelectedNode.Name, "*.jpg"));
                    if (imagens.Count == 0)
                    {
                        label1.Text = "0 imagens";
                    }
                    carregarMiniaturas.RunWorkerAsync();

                }
            }
        }

        public void removerFotos(ListView lvTemp)
        {
            if (carregarMiniaturas.IsBusy == !true)
            {
                if (lvTemp.SelectedItems.Count > 0)
                {
                    remover.Clear();
                    foreach (ListViewItem x in lvTemp.SelectedItems)
                    {
                        remover.Add(x);
                    }

                    foreach (ListViewItem t in remover)
                    {
                        if (parcial.Count > 0)
                            parcial.Clear();
                        nomePasta = Path.GetDirectoryName(t.Name);
                        //  MessageBox.Show(nomePasta);
                        parcial.AddRange(nomePasta.Split('\\'));
                        nomeAlbum = parcial[parcial.Count - 1];
                        parcial.Remove(nomeAlbum);
                        nomeRemovida = "";
                        foreach (String s in parcial)
                        {
                            nomeRemovida = nomeRemovida + s + "\\";
                        }
                        nomeRemovida = nomeRemovida + "Removidas" + "\\" + nomeAlbum;
                        try
                        {
                            if (!Directory.Exists(nomeRemovida))
                                Directory.CreateDirectory(nomeRemovida);
                            File.Move(t.Name, nomeRemovida + "\\" + t.Text + ".jpg");
                        }
                        catch
                        {
                        }
                        listView1.LargeImageList.Images.RemoveAt(t.ImageIndex);
                        listView1.Items.RemoveAt(t.Index);
                        imagens.Remove(t.Name);
                        Application.DoEvents();
                        label1.Text = "" + imagens.Count + " imagens";
                    }

                }

            }
        }



        private void PopularArvore_DoWork(object sender, DoWorkEventArgs e)
        {
            PopulateTreeView();

            if (PopularArvore.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void carregarMiniaturas_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (carregarMiniaturas.IsBusy != true)
                {
                    imagens.Clear();
                    imageList1.Images.Clear();
                    listView1.Items.Clear();
                    imagens.AddRange(Directory.GetFiles(pathTxt.Text, "*.jpg"));
                    carregarMiniaturas.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("BackgroundWork em uso");
                }
            }
            catch { }





        }

        private void btnTree_Click(object sender, EventArgs e)
        {
            PopularArvore.CancelAsync();
        }


    }
}
