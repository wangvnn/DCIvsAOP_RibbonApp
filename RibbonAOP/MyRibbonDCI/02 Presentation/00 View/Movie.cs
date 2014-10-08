using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonDCI.Presentation
{
    public class Movie : PeriodicUITask
    {
        public Movie(Surface v)
            :base(20)
	    {
		    screen = v;
	    }

	    protected override void OnTick()
	    {
		    screen.Invalidate();
	    }

	    private Surface screen;
    }
}
