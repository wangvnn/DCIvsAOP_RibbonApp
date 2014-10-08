using MyRibbonDCI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MyRibbonDCI.Presentation
{
    public class Ribbon
    {
        public Ribbon(Track t)
        {
            _Path = new System.Drawing.Drawing2D.GraphicsPath();

            foreach (Point pt in t)
            {
                System.Drawing.Point touchPos = new System.Drawing.Point((int)pt.X, (int)pt.Y);
                _Path.AddLine(touchPos, touchPos);
            }
        }

        public void DisplayOn(System.Drawing.Graphics g)
        {
            g.DrawPath(System.Drawing.Pens.DarkRed, _Path);
        }

        private System.Drawing.Drawing2D.GraphicsPath _Path;
    }
}
