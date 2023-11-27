/// <summary>
/// Switch.cs
/// <summary>
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
    [Description("当用户单击它时切换开关状态。")]
    public partial class Switch : CheckBox
    {
        private bool _enabled = true;

        public Switch()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
        }

        public new bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                base.Enabled = value;
                this.Cursor = value ? Cursors.Default : Cursors.No;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // 绘制控件的背景
            OnPaintBackground(pe);

            // 设置与控件关联的窗口区域
            var outline = RoundControl.GetRoundedRectOutline(this.ClientRectangle, this.Height / 2);
            this.Region = new Region(outline);

            var diameter = ClientSize.Height - Padding.Vertical;

            // 不要使用 using 语句，因为 pe 会在 OnPaint() 方法执行完后被自动释放，所以如果使用了 using 语句会导致 OnPaint() 方法执行完毕后自动释放时尝试释放一个已经被释放的对象
            var g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            // 绘制边框、底色及文本
            var roundPath = RoundControl.GetRoundedRectPath(this.ClientRectangle, this.Height / 2, 1);

            using (var onBrush = new SolidBrush(Enabled ? Color.FromArgb(33, 150, 243) : Color.FromArgb(140, 197, 255)))
            using (var offBrush = new SolidBrush(Enabled ? Color.FromArgb(204, 204, 204) : Color.FromArgb(234, 236, 240)))
            using (var thumbBrush = new SolidBrush(Color.White))
            {
                g.FillPath(Checked ? onBrush : offBrush, roundPath);
                Cursor = Enabled ? Cursors.Hand : Cursors.No;

                var rect = new Rectangle(
                Padding.Left,
                Padding.Top,
                ClientSize.Width - Padding.Horizontal,
                ClientSize.Height - Padding.Vertical);

                rect.X = Checked ? ClientSize.Width - diameter - Padding.Right : Padding.Left;
                rect.Width = diameter;
                g.FillEllipse(thumbBrush, rect);

                var rect2 = new Rectangle(
                Padding.Left,
                Padding.Top,
                ClientSize.Width - Padding.Horizontal,
                ClientSize.Height - Padding.Vertical);
                g.DrawString(Text, Font, new SolidBrush(this.ForeColor), rect2, StringFormatProcessing.GetStringFormat(this.TextAlign));
            }
                //g.DrawPath(new Pen(Color.FromArgb(204, 204, 204), 2), roundPath);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            this.Invalidate();
            base.OnMouseDown(mevent);
        }
    }
}
