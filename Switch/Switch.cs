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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsControlLibraryMadeByXJY
{
    [Description("当用户单击它时切换开关状态。")]
    public partial class Switch : CheckBox
    {
        private Color switchOnColor = Color.FromArgb(33, 150, 243);
        private Color switchOffColor = Color.FromArgb(204, 204, 204);
        private int switchOnX;
        private int switchOffX;
        private Color transitionColor;
        private double stepSizeR;
        private double stepSizeG;
        private double stepSizeB;
        private int transitionX;
        private double stepSizeX;
        private int frameRate = 90;
        private int frameNum = 24;
        private int counter = 0;
        private Timer frameTimer;

        public Switch()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            InitializeComponent();

            this.FrameRateChanged += new EventHandler(this.OnFrameRateChanged);
            this.SizeChanged += new EventHandler(this.OnAnimationConditionChanged);
            this.PaddingChanged += new EventHandler(this.OnAnimationConditionChanged);
            this.SwitchOnColorChanged += new EventHandler(this.OnAnimationConditionChanged);
            this.SwitchOffColorChanged += new EventHandler(this.OnAnimationConditionChanged);
            this.FrameNumChanged += new EventHandler(this.OnAnimationConditionChanged);
            this.CheckedChanged += new EventHandler(this.OnAnimationConditionChanged);

            // 初始化还未得到初始化的成员变量
            switchOnX = ClientSize.Width - (ClientSize.Height - Padding.Vertical) - Padding.Right;
            switchOffX = Padding.Left;
            transitionX = Checked ? switchOnX : switchOffX;
            transitionColor = Checked ? SwitchOnColor : SwitchOffColor;
            stepSizeR = Checked ? (SwitchOffColor.R - SwitchOnColor.R) / (double)FrameNum : (SwitchOnColor.R - SwitchOffColor.R) / (double)FrameNum;
            stepSizeG = Checked ? (SwitchOffColor.G - SwitchOnColor.G) / (double)FrameNum : (SwitchOnColor.G - SwitchOffColor.G) / (double)FrameNum;
            stepSizeB = Checked ? (SwitchOffColor.B - SwitchOnColor.B) / (double)FrameNum : (SwitchOnColor.B - SwitchOffColor.B) / (double)FrameNum;
            stepSizeX = (ClientSize.Width - (ClientSize.Height - Padding.Vertical) - Padding.Horizontal) / (double)FrameNum;

            // 初始化定时器
            frameTimer = new Timer() { Interval = 1000 / FrameRate };
            frameTimer.Tick += new EventHandler(this.timer_Tick);
        }

        /// <summary>在 <see cref="SwitchOnColor"/> 属性更改后发生</summary>
        [Category("属性已更改"), Description("在控件上更改 SwitchOnColor 属性的值时引发的事件。")]
        public event EventHandler SwitchOnColorChanged;

        /// <summary>获取或设置开关打开时的背景色</summary>
        [Category("扩展属性"), Description("开关打开时背景色")]
        public Color SwitchOnColor
        {
            get => switchOnColor;
            set
            {
                if (switchOnColor != value)
                {
                    switchOnColor = value;
                    this.Invalidate();
                    SwitchOnColorChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>在 <see cref="SwitchOffColor"/> 属性更改后发生</summary>
        [Category("属性已更改"), Description("在控件上更改 SwitchOffColor 属性的值时引发的事件。")]
        public event EventHandler SwitchOffColorChanged;

        /// <summary>获取或设置开关关闭时的背景色</summary>
        [Category("扩展属性"), Description("开关关闭时背景色")]
        public Color SwitchOffColor
        {
            get => switchOffColor;
            set
            {
                if (switchOffColor != value)
                {
                    switchOffColor = value;
                    this.Invalidate();
                    SwitchOffColorChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>在 <see cref="FrameRate"/> 属性更改后发生</summary>
        [Category("属性已更改"), Description("在控件上更改 FrameRate 属性的值时引发的事件。")]
        public event EventHandler FrameRateChanged;

        /// <summary>获取或设置过渡动画的帧率</summary>
        [Category("扩展属性"), Description("过渡动画帧率")]
        public int FrameRate {
            get => frameRate;
            set
            {
                if (frameRate != value)
                {
                    frameRate = value;
                    FrameRateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>在 <see cref="FrameNum"/> 属性更改后发生</summary>
        [Category("属性已更改"), Description("在控件上更改 FrameNum 属性的值时引发的事件。")]
        public event EventHandler FrameNumChanged;

        /// <summary>获取或设置完成过渡动画需要的帧数量</summary>
        [Category("扩展属性"), Description("过渡动画帧数量")]
        public int FrameNum {
            get => frameNum;
            set{
                if (frameNum != value)
                {
                    frameNum = value;
                    FrameNumChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // 绘制控件的背景
            OnPaintBackground(pe);

            // 设置与控件关联的窗口区域
            var outline = RoundControl.GetRoundedRectOutline(this.ClientRectangle, this.Height / 2);
            this.Region = new Region(outline);

            // 不要使用 using 语句，因为 pe 会在 OnPaint() 方法执行完后被自动释放，所以如果使用了 using 语句会导致 OnPaint() 方法执行完毕后自动释放时尝试释放一个已经被释放的对象
            var g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            // 绘制底色、及文本
            var roundPath = RoundControl.GetRoundedRectPath(this.ClientRectangle, this.Height / 2, 1);

            using (var onBrush = new SolidBrush(Enabled ? transitionColor : Color.FromArgb(140, 197, 255)))
            using (var offBrush = new SolidBrush(Enabled ? transitionColor : Color.FromArgb(234, 236, 240)))
            using (var thumbBrush = new SolidBrush(Color.White))
            {
                g.FillPath(Checked ? onBrush : offBrush, roundPath);

                var rect = new Rectangle(
                    transitionX,
                    Padding.Top,
                    ClientSize.Height - Padding.Vertical,
                    ClientSize.Height - Padding.Vertical);

                g.FillEllipse(thumbBrush, rect);

                if (Text != string.Empty)
                {
                    var textRect = new Rectangle(
                        Padding.Left,
                        Padding.Top,
                        ClientSize.Width - Padding.Horizontal,
                        ClientSize.Height - Padding.Vertical);
                    g.DrawString(Text, Font, new SolidBrush(this.ForeColor), textRect, StringFormatProcessing.GetStringFormat(this.TextAlign));
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            this.Invalidate();
            base.OnMouseDown(mevent);
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            // 开始播放动画
            frameTimer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            counter++;

            if (counter == FrameNum)
            {
                counter = 0;
                frameTimer.Stop();
                transitionColor = Checked ? SwitchOnColor : SwitchOffColor;
                transitionX = Checked ? switchOnX : switchOffX;
            }
            else
            {
                int tempR = (int)Math.Round(stepSizeR * counter);
                int tempG = (int)Math.Round(stepSizeG * counter);
                int tempB = (int)Math.Round(stepSizeB * counter);
                int tempX = (int)Math.Round(stepSizeX * counter);
                transitionColor = Color.FromArgb(
                    Math.Max(0, Math.Min(255, Checked ? SwitchOffColor.R + tempR : SwitchOnColor.R - tempR)),
                    Math.Max(0, Math.Min(255, Checked ? SwitchOffColor.G + tempG : SwitchOnColor.G - tempG)),
                    Math.Max(0, Math.Min(255, Checked ? SwitchOffColor.B + tempB : SwitchOnColor.B - tempB)));
                transitionX = Math.Max(Padding.Left, Math.Min(switchOnX, Checked ? switchOffX + tempX : (switchOnX) - tempX));
            }

            // 重绘控件
            this.Invalidate();
        }

        /// <summary>
        /// 过渡动画帧率改变处理函数。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnFrameRateChanged(object sender, EventArgs e)
        {
            // 改变动画帧率意味着需要重新设置定时器的时间间隔
            frameTimer.Interval = 1000 / FrameRate;
        }

        /// <summary>
        /// 动画条件改变处理函数。引起动画条件改变的源头通常有很多，例如：<br/>
        /// <example> 
        /// 1. 控件的 <see cref="Control.Size"/> 发生了变化；<br/>
        /// 2. 控件的 <see cref="Control.Padding"/> 发生了变化；<br/>
        /// 3. <see cref="SwitchOffColor"/> 或者 <see cref="SwitchOnColor"/> 属性发生了变化；<br/>
        /// 4. <see cref="FrameNum"/> 属性发生了变化；<br/>
        /// 5. <see cref="CheckBox.Checked"/> 属性发生了变化；<br/>
        /// </example>
        /// </summary>
        /// <param name="e"></param>
        protected void OnAnimationConditionChanged(object sender, EventArgs e)
        {
            switchOnX = ClientSize.Width - (ClientSize.Height - Padding.Vertical) - Padding.Right;
            switchOffX = Padding.Left;

            transitionColor = Checked ? SwitchOnColor : SwitchOffColor;
            stepSizeR = (SwitchOnColor.R - SwitchOffColor.R) / (double)FrameNum;
            stepSizeG = (SwitchOnColor.G - SwitchOffColor.G) / (double)FrameNum;
            stepSizeB = (SwitchOnColor.B - SwitchOffColor.B) / (double)FrameNum;

            transitionX = Checked ? switchOnX : switchOffX;
            stepSizeX = (ClientSize.Width - (ClientSize.Height - Padding.Vertical) - Padding.Horizontal) / (double)FrameNum;
        }
    }
}
