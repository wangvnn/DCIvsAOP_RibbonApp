using MyRibbonAOP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using Westwind.Utilities.Dynamic;

namespace MyRibbonAOP.Presentation
{
    public class RibbonSet : Expando
    {
        public RibbonSet(TrackSet ts)
	    {
		    tracks = ts;
            setup(); // todo AOP Tricks
	    }

	    public void displayOn(InkCanvas c)
	    {
            self.refreshRibbons();
            c.Strokes.Clear();
		    foreach( Ribbon r in ribbons )
		    {
			    r.displayOn(c);
		    }
	    }

        public void clear()
        {
            this.tracks.clear();
            self.refreshRibbons(); // AOP Tricks
        }

	    protected virtual DrawingAttributes colorForTrack(Track t)
	    {
            // ignore Coloring aspect, use simple hardcoded pallete
            int colorId = t.Id % 3;
            var colorR = Color.FromRgb(255,0,0);
            var colorB = Color.FromRgb(0,0,255);
            var colorG = Color.FromRgb(0,255, 0);
            var colors = new []  { colorR, colorG, colorB };
            return ThickPaint.make(colors[colorId]);
	    }

	    public virtual void refreshRibbons()
	    {
		    ribbons = new LinkedList<Ribbon>();
		    foreach( Track t in tracks )
			    makeRibbon(t);
	    }

	    private void makeRibbon(Track t)
	    {
		    Ribbon r = new Ribbon(t, colorForTrack(t));
		    ribbons.AddLast(r);
	    }

	    private LinkedList<Ribbon> ribbons;
	    public TrackSet tracks {get; private set; }

        #region AOP Tricks
        private void setup()
        {
            this["self"] = this;
        }
        private RibbonSet self { get { return (RibbonSet)this["self"];  } }
        #endregion
    }
}
