using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsControlLibraryMadeByXJY
{
    internal class ColorProcessing
    {
        /// <summary>
        /// 获取指定颜色的浅色版本
        /// </summary>
        /// <param name="color">要获取浅色版本的 <see cref="Color"/></param>
        /// <param name="lightenAmount">浅色程度，请指定一个介于 0 到 1 之间的值</param>
        /// <returns><paramref name="color"/> 的浅色版本 <see cref="Color"/> 对象</returns>
        public static Color LightenColor(Color color, double lightenAmount)
        {
            // 通过增加 RGB 值的亮度使颜色变浅
            double red = color.R * (1 + lightenAmount);
            double green = color.G * (1 + lightenAmount);
            double blue = color.B * (1 + lightenAmount);

            // 确保 RGB 值在 0 到 255 之间
            red = Math.Min(255, red);
            green = Math.Min(255, green);
            blue = Math.Min(255, blue);

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }
    }
}
