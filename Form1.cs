using System;
using System.Windows.Forms;
using Ionic.Zip;
using System.IO;
using ZipFile = Ionic.Zip.ZipFile;
using System.Diagnostics;

namespace JuiceZIP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"C:/ZIP");
            Directory.CreateDirectory(@"C:/ZIPSource");
            Directory.CreateDirectory(@"C:/JuiceZIP");
        }

        private void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string extractPath = @"C:\JuiceZIP";

            using (ZipFile zip = ZipFile.Read(textBox1.Text))
            {
                // Önce eski dosyaları temizle
                if (Directory.Exists(extractPath))
                {
                    foreach (string file in Directory.GetFiles(extractPath))
                        File.Delete(file);

                    foreach (string dir in Directory.GetDirectories(extractPath))
                        Directory.Delete(dir, true);
                }

                Directory.CreateDirectory(extractPath);

                foreach (ZipEntry entry in zip)
                {
                    string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FileName));

                    // Dizin kaçışını engelle
                    if (!destinationPath.StartsWith(extractPath, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show($"Dizin kaçışı tespit edildi: {entry.FileName}", "JuiceZIP");
                        continue; // Bu entry atlanacak
                    }

                    entry.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
                }

                Process.Start(extractPath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string extractPath = @"C:\JuiceZIP";

            using (ZipFile zip = ZipFile.Read(textBox1.Text))
            {
                if (Directory.Exists(extractPath))
                    Directory.Delete(extractPath, true);

                Directory.CreateDirectory(extractPath);

                foreach (ZipEntry entry in zip)
                {
                    string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FileName));

                    if (!destinationPath.StartsWith(extractPath, StringComparison.OrdinalIgnoreCase))
                        continue;

                    entry.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(@"C:\ZIPSource");
                zip.Save(@"C:/ZIP/JuiceZIP.zip");
                Process.Start("C:/ZIP");
                MessageBox.Show("Zip oluşturuldu!", "JuiceZIP");
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void oluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string extractPath = @"C:\JuiceZIP";
            if (Directory.Exists(extractPath))
                Directory.Delete(extractPath, true);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }
    }
}
