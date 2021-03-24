using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMap_WinForms_
{
    public partial class Form1 : Form
    {
        private Map map;
        public Form1()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            var path = Directory.GetCurrentDirectory() + @"\Map.txt";
            map = new Map(path);
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            map.DrawMap(panel.CreateGraphics(), panel.Width, panel.Height);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            map.DrawMap(panel.CreateGraphics(), panel.Width, panel.Height);
        }
    }
}
