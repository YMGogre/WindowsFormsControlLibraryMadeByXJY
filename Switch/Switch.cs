﻿/// <summary>
/// Switch.cs
/// <summary>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsControlLibraryMadeByXJY
{
    public partial class Switch : UserControl
    {
        public Switch()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;
            this.Size = new Size(87, 27);
        }
        bool isCheck = false;

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked
        {
            set { isCheck = value; this.Invalidate(); }
            get { return isCheck; }
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bitMapOn = null;
            Bitmap bitMapOff = null;
            bitMapOn = Properties.Resources.SwitchON;
            bitMapOff = Properties.Resources.SwitchOFF;
            Graphics g = e.Graphics;
            Rectangle rec = new Rectangle(0, 0, this.Size.Width, this.Size.Height);

            if (isCheck)
            {
                g.DrawImage(bitMapOn, rec);
            }
            else
            {
                g.DrawImage(bitMapOff, rec);
            }
        }
        private void Switch_Click(object sender, EventArgs e)
        {
            isCheck = !isCheck;
            this.Invalidate();
        }
    }
}
