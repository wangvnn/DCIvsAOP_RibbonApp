using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Media;

namespace MyRibbonAOP.Presentation
{
    public class ThickPaint
    {
        public static DrawingAttributes make(Color color)
        {
            DrawingAttributes p = new DrawingAttributes();
            p.Color = color;
            p.StylusTip = StylusTip.Ellipse;
            p.Width = 3;
            p.Height = 3;
            return p;
        }
    }
}
