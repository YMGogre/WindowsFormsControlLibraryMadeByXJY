using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsControlLibraryMadeByXJY
{
    public partial class RoundButton : Button
    {
        private int rgnRadius = 15;

        public RoundButton()
        {
            InitializeComponent();
        }

        [Category("扩展属性"), Description("圆角半径")]
        public int RgnRadius { 
            get => rgnRadius;
            set
            {
                rgnRadius = value;
                this.Invalidate();
            } 
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            // 不要使用 using 语句，因为 pe 会在 OnPaint() 方法执行完后被自动释放，所以如果使用了 using 语句会导致 OnPaint() 方法执行完毕后自动释放时尝试释放一个已经被释放的对象
            var g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            var roundPath = RoundControl.GetRoundedRectPath(this.ClientRectangle, RgnRadius);
            //绘制界面轮廓，参考 <https://learn.microsoft.com/zh-cn/dotnet/api/system.drawing.drawing2d.graphicspath.widen?redirectedfrom=MSDN&view=dotnet-plat-ext-6.0>
            g.DrawPath(new Pen(base.FlatAppearance.BorderColor, base.FlatAppearance.BorderSize), roundPath);
            this.Region = new Region(roundPath);

            //Pen widenPen = new Pen(Color.LightGray, 1);
            //Matrix widenMatrix = new Matrix();
            //widenMatrix.Translate(-1, -1);
            //roundPath.Widen(widenPen, widenMatrix, 5.0f);
            //g.FillPath(new SolidBrush(Color.FromArgb(255, 173, 173, 173)), roundPath);
        }

        protected override void OnSizeChanged(EventArgs pevent)
        {
            base.OnSizeChanged(pevent);
            this.Invalidate();
        }
    }
}
