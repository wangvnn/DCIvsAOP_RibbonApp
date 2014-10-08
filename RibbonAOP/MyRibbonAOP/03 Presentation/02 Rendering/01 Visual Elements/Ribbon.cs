using MyRibbonAOP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;

namespace MyRibbonAOP.Presentation
{
    public class Ribbon
    {
        public Ribbon(Track t, DrawingAttributes p)
	    {
		    paint = p;
		    path = new StylusPointCollection();

		    foreach( Point pt in t )
		    {
                path.Add(new StylusPoint(pt.X+1, pt.Y+1));
		    }
	    }

	    public void displayOn(InkCanvas c)
	    {
            if (path.Count > 0)
            {
                var st = new Stroke(path);
                st.DrawingAttributes = paint;
                c.Strokes.Add(st);
            }
	    }

        private readonly DrawingAttributes paint;
        private readonly StylusPointCollection path;
    }
}
