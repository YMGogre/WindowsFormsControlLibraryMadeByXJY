using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsControlLibraryMadeByXJY
{
    public partial class RoundButton : Button
    {
        /// <summary>圆角半径，默认为 15 像素</summary>
        private int rgnRadius = 15;
        /// <summary>圆角按钮轮廓颜色，默认为蓝色</summary>
        private Color roundBorderColor = Color.FromArgb(64, 158, 255);
        /// <summary>圆角按钮的边框的大小（以像素为单位），默认为 1</summary>
        private int roundBorderSize = 1;
        /// <summary>圆角按钮背景色，默认为浅蓝色</summary>
        private Color roundBackColor = Color.FromArgb(236, 245, 255);
        /// <summary>圆角按钮按下时背景色，默认为深蓝色</summary>
        private Color roundButtonPressedColor = Color.FromArgb(58, 142, 230);
        /// <summary>表征鼠标指针是否在控件区域内部</summary>
        private bool isMouseInside = false;
        /// <summary>表征按钮是否被按下</summary>
        private bool isMouseDown = false;

        public RoundButton()
        {
            InitializeComponent();
        }

        /// <summary>获取或设置圆角半径，默认为 15 像素</summary>
        [Category("扩展属性"), Description("圆角半径")]
        public int RgnRadius { 
            get => rgnRadius;
            set
            {
                rgnRadius = value;
                this.Invalidate();
            } 
        }

        /// <summary>获取或设置圆角按钮轮廓颜色，默认为蓝色</summary>
        [Category("扩展属性"), Description("圆角按钮轮廓颜色")]
        public Color RoundBorderColor 
        {
            get => roundBorderColor;
            set
            {
                roundBorderColor = value;
                this.Invalidate();
            }
        }

        /// <summary>获取或设置一个值，该值指定圆角按钮周围的边框的大小（以像素为单位），默认为 1</summary>
        [Category("扩展属性"), Description("圆角按钮边框的大小")]
        public int RoundBorderSize
        {
            get => roundBorderSize;
            set
            {
                roundBorderSize = value;
                this.Invalidate();
            }
        }

        /// <summary>获取或设置圆角按钮背景颜色，默认为浅蓝色</summary>
        [Category("扩展属性"), Description("圆角按钮背景颜色")]
        public Color RoundBackColor
        {
            get => roundBackColor;
            set
            {
                roundBackColor = value;
                this.Invalidate();
            }
        }

        /// <summary>获取或设置圆角按钮按下时背景色，默认为深蓝色</summary>
        [Category("扩展属性"), Description("圆角按钮按下时背景色")]
        public Color RoundButtonPressedColor
        {
            get => roundButtonPressedColor;
            set
            {
                roundButtonPressedColor = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // 设置与控件关联的窗口区域
            var outline = RoundControl.GetRoundedRectOutline(this.ClientRectangle, RgnRadius);
            this.Region = new Region(outline);

            // 不要使用 using 语句，因为 pe 会在 OnPaint() 方法执行完后被自动释放，所以如果使用了 using 语句会导致 OnPaint() 方法执行完毕后自动释放时尝试释放一个已经被释放的对象
            var g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            // 设置透明背景色（并非真正透明，而是跟随父控件的背景色）
            var backColor = Parent.BackColor;
            var regionColor = Color.FromArgb(255, backColor);
            g.FillRegion(new SolidBrush(regionColor), this.Region);

            // 绘制边框、底色及文本
            var roundPath = RoundControl.GetRoundedRectPath(this.ClientRectangle, RgnRadius, RoundBorderSize);
            if (isMouseInside)
            {
                g.FillPath(new SolidBrush(RoundBorderColor), roundPath);
                g.DrawString(Text, Font, new SolidBrush(Color.White), pe.ClipRectangle, GetStringFormat());
            }
            else
            {
                g.DrawPath(new Pen(RoundBorderColor, RoundBorderSize), roundPath);
                g.FillPath(new SolidBrush(RoundBackColor), roundPath);
                g.DrawString(Text, Font, new SolidBrush(this.ForeColor), pe.ClipRectangle, GetStringFormat());
            }
            if (isMouseDown)
            {
                g.FillPath(new SolidBrush(RoundButtonPressedColor), roundPath);
                g.DrawString(Text, Font, new SolidBrush(Color.White), pe.ClipRectangle, GetStringFormat());
            }

            // 处理控件无效时的绘制（绘制为灰色）
            if (!Enabled)
            {
                g.DrawPath(new Pen(Color.FromArgb(188, 190, 194), RoundBorderSize), roundPath);
                g.FillPath(new SolidBrush(Color.FromArgb(244, 244, 245)), roundPath);
                g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(188, 190, 194)), pe.ClipRectangle, GetStringFormat());
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            isMouseInside = true;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isMouseInside = false;
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            isMouseDown = true;
            this.Invalidate();
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            isMouseDown = false;
            this.Invalidate();
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            var pt = this.PointToClient(Cursor.Position);
            if (!this.Region.IsVisible(pt))
            {
                // 鼠标已经移出了圆角按钮控件范围，释放按钮
                isMouseDown = false;
                this.Invalidate();
            }
        }

        protected override void OnSizeChanged(EventArgs pevent)
        {
            base.OnSizeChanged(pevent);
            this.Invalidate();
        }

        /// <summary>
        /// 根据 <see cref="ButtonBase.TextAlign"/> 属性设置文本对齐方式
        /// </summary>
        /// <returns>文本布局信息 <see cref="StringFormat"/> 对象</returns>
        private StringFormat GetStringFormat()
        {
            StringFormat sf = new StringFormat();
            switch (this.TextAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    sf.Alignment = StringAlignment.Far;
                    break;
            }
            switch (this.TextAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    break;
            }
            return sf;
        }
    }
}
