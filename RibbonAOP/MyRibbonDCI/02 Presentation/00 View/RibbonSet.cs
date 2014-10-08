using MyRibbonDCI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyRibbonDCI.Presentation
{
    public class RibbonSet
    {
        public RibbonSet(TrackSet ts)
        {
            Tracks = ts;  
        }

        public void DisplayOn(System.Drawing.Graphics g)
        {
            refreshRibbons();
            foreach (Ribbon r in _Ribbons)
            {
                r.DisplayOn(g);
            }
        }

        public void Clear()
        {
            this.Tracks.Clear();
        }

        public void refreshRibbons()
        {
            if (Tracks == null) return;

            _Ribbons = new LinkedList<Ribbon>();
            foreach (Track t in Tracks)
                makeRibbon(t);
        }

        private void makeRibbon(Track t)
        {
            Ribbon r = new Ribbon(t);
            _Ribbons.AddLast(r);
        }

        private LinkedList<Ribbon> _Ribbons = new LinkedList<Ribbon>();
        public TrackSet Tracks { get; private set; }
    }
}
