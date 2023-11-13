using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsControlLibraryMadeByXJY;

namespace TestWindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Task.Run(ChangeIndicatorColor);
        }

        private void ChangeIndicatorColor()
        {
            var values = (IndicatorColors[])Enum.GetValues(typeof(IndicatorColors));
            int index = 0;
            while (true)
            {
                Thread.Sleep(1000);
                this.indicatorLight1.CurrColor = values[index];
                if (index < values.Length - 1) index++;
                else index = 0;
            }
        }
    }
}
