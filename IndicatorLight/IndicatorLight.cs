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
    /// <summary>
    /// 指示灯颜色枚举
    /// </summary>
    public enum IndicatorColors
    {
        /// <summary>灰色</summary>
        Gray = 0xC8C9CC,
        /// <summary>蓝色</summary>
        Blue = 0x409EFF,
        /// <summary>绿色</summary>
        Green = 0x67C23A,
        /// <summary>黄色</summary>
        Yellow = 0xE6A23C,
        /// <summary>红色</summary>
        Red = 0xF56C6C
    }
    /// <summary>
    /// 指示灯状态枚举，每种状态对应一种 <see cref="IndicatorColors"/> 颜色
    /// </summary>
    public enum IndicatorState
    {
        /// <summary>空状态，UI 表现为灰色</summary>
        Empty = IndicatorColors.Gray,
        /// <summary>信息状态，UI 表现为蓝色</summary>
        Info = IndicatorColors.Blue,
        /// <summary>成功状态，UI 表现为绿色</summary>
        Success = IndicatorColors.Green,
        /// <summary>警告状态，UI 表现为黄色</summary>
        Warn = IndicatorColors.Yellow,
        /// <summary>错误状态，UI 表现为红色</summary>
        Error = IndicatorColors.Red,
    };

    public partial class IndicatorLight : UserControl
    {
        /// <summary>指示灯的当前颜色，默认为灰色</summary>
        private IndicatorColors currColor = IndicatorColors.Gray;
        /// <summary>指示灯显示的文本，默认为空</summary>
        private string indicatorText = "";
        /// <summary>指示灯是否可点击，默认不可点击</summary>
        private bool clickable = false;

        /// <summary>
        /// 基于一个 32 位的无符号 ARGB 值创建 <see cref="Color"/> 结构。
        /// </summary>
        /// <param name="argb">用于指定 32 位无符号 ARGB 值的值</param>
        /// <returns>此方法创建的 <see cref="Color"/> 结构。</returns>
        public static Color FromUargb(uint argb)
        {
            int a = (int)((argb & 0xFF000000) >> 24);
            int r = (int)((argb & 0x00FF0000) >> 16);
            int g = (int)((argb & 0x0000FF00) >> 8);
            int b = (int)(argb & 0x000000FF);
            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// 基于一个 32 位的 RGB 值创建 <see cref="Color"/> 结构（不包含 α 通道值）。
        /// </summary>
        /// <param name="rgb">用于指定 32 位 RGB 值的值</param>
        /// <returns>此方法创建的 <see cref="Color"/> 结构。</returns>
        public static Color FromRgb(int rgb)
        {
            int r = (rgb & 0x00FF0000) >> 16;
            int g = (rgb & 0x0000FF00) >> 8;
            int b = rgb & 0x000000FF;
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// 当 <see cref="CurrColor"/> 属性的值更改时发生。
        /// </summary>
        [Category("属性已更改"), Description("在控件的 CurrColor 属性值更改时引发的事件。"), Browsable(true)]
        public event EventHandler CurrColorChanged;

        /// <summary>
        /// 当 <see cref="CurrState"/> 属性的值更改时发生。
        /// </summary>
        [Category("属性已更改"), Description("在控件的 CurrState 属性值更改时引发的事件。"), Browsable(true)]
        public event EventHandler CurrStateChanged;

        /// <summary>
        /// 当 <see cref="IndicatorText"/> 属性的值更改时发生。
        /// </summary>
        [Category("属性已更改"), Description("在控件的 IndicatorText 属性值更改时引发的事件。"), Browsable(true)]
        public event EventHandler IndicatorTextChanged;

        /// <summary>
        /// 当 <see cref="Clickable"/> 属性的值更改时发生。
        /// </summary>
        [Category("属性已更改"), Description("在控件的 Clickable 属性值更改时引发的事件。"), Browsable(true)]
        public event EventHandler ClickableChanged;

        /// <summary>
        /// 获取或设置指示灯显示的颜色
        /// </summary>
        [Category("Indicator"), Description("指示灯的当前颜色"), Browsable(true)]
        public IndicatorColors CurrColor 
        { 
            get => currColor;
            set
            {
                if (currColor != value)
                {
                    currColor = value;
                    CurrColorChanged?.Invoke(this, EventArgs.Empty);
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置指示灯的状态，UI 表现为修改颜色
        /// </summary>
        [Category("Indicator"), Description("指示灯的当前状态"), Browsable(true)]
        public IndicatorState CurrState
        {
            get => (IndicatorState)currColor;
            set
            {
                if (CurrState != value)
                {
                    CurrColor = (IndicatorColors)value;
                    CurrStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 获取或设置指示灯显示的文本
        /// </summary>
        [Category("Indicator"), Description("指示灯显示的文本"), Browsable(true)]
        public string IndicatorText
        {
            get => indicatorText;
            set
            {
                if (indicatorText != value)
                {
                    indicatorText = value;
                    IndicatorTextChanged?.Invoke(this, EventArgs.Empty);
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置指示灯是否可以被点击
        /// </summary>
        [Category("Indicator"), Description("指示灯是否可以被点击"), Browsable(true)]
        public bool Clickable 
        { 
            get => clickable;
            set
            {
                if (clickable != value)
                {
                    clickable = value;
                    this.Cursor = this.Clickable ? Cursors.Hand : Cursors.Default;
                    ClickableChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 重置信号灯状态为空
        /// </summary>
        public void ResetState()
        {
            this.CurrState = IndicatorState.Empty;
        }

        public IndicatorLight()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.Size = new Size(50, 50);
        }

        /// <summary>
        /// 重写绘制方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawEllipse(new Pen(new SolidBrush(Color.FromArgb(0xF4, 0xF4, 0xF5)),2), new Rectangle(4, 4, this.Width - 8, this.Height - 8));
            g.FillEllipse(new SolidBrush(FromRgb((int)CurrColor)), new Rectangle(4, 4, this.Width - 8, this.Height - 8));           
            TextRenderer.DrawText(g, IndicatorText, this.Font, new Rectangle(4, 4, this.Width - 8, this.Height - 8), SystemColors.InfoText);
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(new Rectangle(4, 4, this.Width - 8, this.Height - 8));
            this.Region = new Region(path);
        }


        /// <summary>
        /// 处理指示灯的SizeChanged事件中针对只调整单边大小的情况。
        /// </summary>
        /// <param name="sender">事件的来源</param>
        /// <param name="e"><see cref="EventArgs"/> 包含事件数据的实例</param>
        void UCSignalLamp_SizeChanged(object sender, EventArgs e)
        {
            this.Height = this.Width;
        }
    }
}
