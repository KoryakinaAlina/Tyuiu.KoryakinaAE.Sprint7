using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tyuiu.KoryakinaAE.Sprint7.Project.V12
{
    public partial class FormAbout : Form
    {
            public FormAbout()
        {
            InitializeComponent();
            LoadPhoto();
        }

        private void LoadPhoto()
        {
            try
            {
                string path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Icons",
                    "bell.png");

                if (File.Exists(path))
                {
                    pictureBoxPhoto_KAE.Image?.Dispose();
                    pictureBoxPhoto_KAE.Image = Image.FromFile(path);
                }
                else
                {
                    pictureBoxPhoto_KAE.BackColor = Color.LightGray;
                }
            }
            catch
            {
                pictureBoxPhoto_KAE.BackColor = Color.LightGray;
            }
        }

    }
}
       