using MRG.Controls.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AsyncDialog
{
    public partial class Form1 : Form
    {
        private LoadingCircle barberPole = new LoadingCircle();
        private FlickerFreePanel2 asyncPanel = new FlickerFreePanel2();
        private SolidBrush fillBrush = new SolidBrush(Color.FromArgb(225, SystemColors.Window));
        private Bitmap background = null;
        public Form1()
        {
            InitializeComponent();
           
        }

        private sealed class FlickerFreePanel2 : Control
        {
            public FlickerFreePanel2()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
                Visible = false;
            }

        }//class

        private void Form1_Load(object sender, EventArgs e)
        {
            barberPole.BackColor = Color.Transparent;
            barberPole.Dock = DockStyle.Fill;
            barberPole.StylePreset = LoadingCircle.StylePresets.Custom;
            barberPole.SpokeThickness = 8;
            barberPole.InnerCircleRadius = 30;
            barberPole.OuterCircleRadius = 35;
            barberPole.NumberSpoke = 100;
            barberPole.Color = SystemColors.ControlDark;
            barberPole.RotationSpeed = 35;

            asyncPanel.Dock = DockStyle.Fill;
            asyncPanel.Controls.Add(barberPole);
            Controls.Add(asyncPanel);

            asyncPanel.BringToFront();

            ResumeLayout(false);

            asyncPanel.Paint += new PaintEventHandler(OnPaintAsyncPanel);

            asyncPanel.Visible = barberPole.Active = true;
        }
        private void OnPaintAsyncPanel(object sender, PaintEventArgs e)
        {
            if (background != null)
                //
                // Paint background image...
                e.Graphics.DrawImageUnscaled(background, 0, 0, background.Width, background.Height);
            else
                //
                // No background available (BeginAsyncOperation was probably called during the subclass constructor)...
                // So just fill with the normal back colour instead...
                e.Graphics.FillRectangle(fillBrush, 0, 0, asyncPanel.Width, asyncPanel.Height);
        }
    }
    
}
