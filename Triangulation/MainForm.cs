using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Triangulation
{
    public partial class MainForm : Form
    {
        private DrawingTriangulation dT;
        private Triangulation triangulation;

        public MainForm()
        {
            InitializeComponent();
        }

        private void OnLoadMainForm(object sender, EventArgs e)
        {

        }

        private void OnClickButtonDraw(object sender, EventArgs e)
        {
            triangulation = new Triangulation((int)numUpDown_Nodes.Value);
            dT = new DrawingTriangulation(pictureBox_Triangulation, 0, 0, 1, 1);
            dT.Clear();
            dT.DrawTriangulation(triangulation.nodes, triangulation.triangles);
        }
    }
}