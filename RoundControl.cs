using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsControlLibraryMadeByXJY
{
    public class RoundOK
    {
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, Boolean bRedraw);
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Ansi)]
        public static extern int DeleteObject(IntPtr hObject);
    }

    public class RoundControl
    {
        public static void SetButtonRoundRectRgn(Button button, int rgnRadius)
        {
            IntPtr hRgn = (IntPtr)0;
            hRgn = RoundOK.CreateRoundRectRgn(0, 0, button.Width, button.Height, rgnRadius, rgnRadius);
            RoundOK.SetWindowRgn(button.Handle, hRgn, true);
            RoundOK.DeleteObject(hRgn);
        }

        /// <summary>
        /// 获取由控件的矩形轮廓转换而来的圆角矩形轮廓路径
        /// </summary>
        /// <param name="rect">控件矩形</param>
        /// <param name="radius">圆角半径</param>
        /// <returns></returns>
        public static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int SideLength = radius;    //定义圆弧方形容器边长为圆的半径
            Rectangle arcRect = new Rectangle(rect.Location, new Size(SideLength, SideLength));
            GraphicsPath path = new GraphicsPath();

            // 左上角
            path.AddArc(arcRect, 180, 90);

            // 右上角
            arcRect.X = rect.Right - SideLength;
            path.AddArc(arcRect, 270, 90);

            // 右下角
            arcRect.Y = rect.Bottom - SideLength;
            path.AddArc(arcRect, 0, 90);

            // 左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线
            return path;
        }
    }
}
