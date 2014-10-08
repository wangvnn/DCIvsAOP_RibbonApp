using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonAOP.Presentation
{
    public class Movie : PeriodicUITask
    {
        public Movie(Surface v)
            :base(20)
	    {
		    screen = v;
	    }

	    protected override void onTick()
	    {
		    screen.invalidate();
	    }

	    private Surface screen;
    }
}
