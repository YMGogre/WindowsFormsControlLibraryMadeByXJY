using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsControlLibraryMadeByXJY
{
    public partial class Selector : UserControl
    {
        /// <summary>
        /// 使用静态列表存储被选中的项
        /// </summary>
        private static List<Selector> s_selectedItems = new List<Selector>();
        /// <summary>
        /// 获取选中项的编号
        /// </summary>
        public int Number { get; private set; }
        /// <summary>
        /// 获取当前控件的选中状态
        /// </summary>
        public bool IsSelected { get; private set; }

        /// <summary>
        /// 选择当前项
        /// </summary>
        public void SelectThis()
        {
            s_selectedItems.Add(this);
            IsSelected = true;
            UpdateItemNumbers();
            Invalidate();
        }

        /// <summary>
        /// 取消选择当前项
        /// </summary>
        public void DeselectThis()
        {
            s_selectedItems.Remove(this);
            IsSelected = false;
            UpdateItemNumbers();
            Invalidate();
        }

        /// <summary>
        /// 更新选择项的编号
        /// </summary>
        private void UpdateItemNumbers()
        {
            for(int i = 0; i < s_selectedItems.Count; i++)
            {
                s_selectedItems[i].Number = i + 1;
                s_selectedItems[i].Invalidate();
            }
        }
        public Selector()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.Cursor = Cursors.Hand;
            this.Size = new Size(25, 25);
        }

        /// <summary>
        /// 重绘控件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (IsSelected)
            {
                g.DrawEllipse(new Pen(new SolidBrush(Color.FromArgb(255, 2, 152, 249)), 2), new Rectangle(4, 4, this.Width - 8, this.Height - 8));
                g.FillEllipse(new SolidBrush(Color.FromArgb(255, 2, 152, 249)), new Rectangle(4, 4, this.Width - 8, this.Height - 8));
                TextRenderer.DrawText(g, Number.ToString(), this.Font, new Rectangle(5, 3, this.Width - 8, this.Height - 8), Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            else
            {
                g.DrawEllipse(new Pen(new SolidBrush(Color.White), 2), new Rectangle(4, 4, this.Width - 8, this.Height - 8));
                g.FillEllipse(new SolidBrush(Color.FromArgb(128, 178, 178, 178)), new Rectangle(4, 4, this.Width - 8, this.Height - 8));
            }
        }

        /// <summary>
        /// 当自身被点击时，修改自身的选中状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selector_Click(object sender, EventArgs e)
        {
            if(IsSelected)
            {
                DeselectThis();
            }
            else
            {
                SelectThis();
            }
        }

        /// <summary>
        /// 针对只调整单边大小的情况处理 SizeChanged 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selector_SizeChanged(object sender, EventArgs e)
        {
            this.Height = this.Width;
        }
    }
}
