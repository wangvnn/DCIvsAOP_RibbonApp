using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Westwind.Utilities.Dynamic;

namespace MyRibbonAOP.Presentation
{
    public abstract class  PeriodicUITask : Expando
    {
        public void play()
	    { 
            dispatcherTimer.Start();
	    }

        public PeriodicUITask()
        { }

	    protected PeriodicUITask( int rateMs )
	    {
		    dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Interval =  TimeSpan.FromMilliseconds(rateMs);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
 	    }
	    private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            onTick();
        }
	    protected abstract void onTick();

	    private DispatcherTimer dispatcherTimer ;

    }
}
