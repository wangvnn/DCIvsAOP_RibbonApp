using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonAOP.Presentation
{
    public class SimulatedPad : PeriodicUITask, TouchPad
    {
        public SimulatedPad(int w, int h)
            :base(100)
	    {
            setup();
		    this.w = w;
		    this.h = h;
		    r = Math.Min(w, h) / 3;

		    first = true;
		    play();
	    }
        
	    protected override void onTick()
	    {
            int x = (int)(w / 2 + r * Math.Sin(a / 180 * Math.PI));
            int y = (int)(h / 2 + r * Math.Cos(a / 180 * Math.PI));

		    if( first )
		    {
			    first = false;
                self.onTouchDown(SIMULATED_POINTER_INDEX, x, y);
		    }
		    else
		    {
                self.onTouchMoving(SIMULATED_POINTER_INDEX, x, y);
		    }
		    a = (a + 8) % 360;
	    }

	    protected static readonly int SIMULATED_POINTER_INDEX = 17;
	    private bool first;
	    private float w;
	    private float h;
	    private float r;
	    private float a;

        #region Dirty tricks for AOP to work

        private void setup()
        {
            this["self"] = this; // dirty trick to deal with dynamic property
        }
        private SimulatedPad self { get { return ((SimulatedPad)this["self"]); } }

        public virtual void setView(View v)
        {
        }

        public virtual void onTouchDown(int pointerId, int x, int y)
        {
        }

        public virtual void onTouchMoving(int pointerId, int x, int y)
        {
        }

        public virtual void onTouchUp(int pointerId)
        {
        }
        #endregion
    }
}
