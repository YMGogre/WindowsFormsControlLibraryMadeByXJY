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
    public partial class WatermarkTextBox : TextBox
    {
        private readonly Label lblwaterText = new Label();

        public WatermarkTextBox()
        {
            InitializeComponent();
            lblwaterText.BorderStyle = BorderStyle.None;
            lblwaterText.Enabled = false;
            lblwaterText.Parent = this;
            lblwaterText.BackColor = Color.Transparent;
            lblwaterText.AutoSize = true;
            lblwaterText.Left = 2;
            lblwaterText.FlatStyle = FlatStyle.System;
            lblwaterText.Font = this.Font;
            lblwaterText.TextAlign = ContentAlignment.BottomLeft;
            Controls.Add(lblwaterText);
        }

        public override string Text
        {
            set
            {
                lblwaterText.Visible = value == string.Empty;
                base.Text = value;
            }
            get
            {
                return base.Text;
            }
        }

        /// <summary>
        /// 重写"控件上的 Size 属性值更改"事件处理方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            if (Multiline && (ScrollBars == ScrollBars.Vertical || ScrollBars == ScrollBars.Both))
                lblwaterText.Width = Width - 20;
            else
                lblwaterText.Width = Width;
            //lblwaterText.Height = Height - 2;
            lblwaterText.Top = (Height - lblwaterText.Height) / 2;
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// 重写"文本改变"事件处理方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            lblwaterText.Visible = base.Text == string.Empty;
            base.OnTextChanged(e);
        }

        /// <summary>
        /// 重写"鼠标指针在控件上方并按下鼠标按钮"事件处理方法
        /// </summary>
        /// <param name="e"></param>
        //protected override void OnMouseDown(MouseEventArgs e)
        //{
        //    if(e.Button == MouseButtons.Left)
        //    {
        //        lblwaterText.Visible = false;
        //        base.OnMouseDown(e);
        //    }
        //}

        /// <summary>
        /// 重写"鼠标离开控件的可见部分"事件处理方法
        /// </summary>
        /// <param name="e"></param>
        //protected override void OnMouseLeave(EventArgs e)
        //{
        //    lblwaterText.Visible = base.Text == string.Empty;
        //    base.OnMouseLeave(e);
        //}

        /// <summary>
        /// 重写"控件成为该窗体的活动控件"事件处理方法
        /// </summary>
        /// <param name="e"></param>
        //protected override void OnEnter(EventArgs e)
        //{
        //    lblwaterText.Visible = false;
        //    base.OnEnter(e);
        //}

        /// <summary>
        /// 重写"控件不再是窗体的活动控件"事件处理方法
        /// </summary>
        /// <param name="e"></param>
        //protected override void OnLeave(EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(base.Text))
        //        lblwaterText.Visible = true;
        //    base.OnLeave(e);
        //}

        [Category("扩展属性"), Description("显示的水印文字提示信息")]
        public string WaterText
        {
            get { return lblwaterText.Text; }
            set { lblwaterText.Text = value; }
        }

        [Category("扩展属性"), Description("水印文字的左上角相对于文本框左上角的坐标")]
        public Point WaterMarkLocation
        {
            get { return lblwaterText.Location; }
            set { lblwaterText.Location = value; }
        }

        [Category("扩展属性"), Description("水印文字字体")]
        public Font WaterMarkFont
        {
            get { return lblwaterText.Font; }
            set { lblwaterText.Font = value; }
        }
    }
}
