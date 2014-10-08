using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MyRibbonDCI.Presentation
{
    public abstract class  PeriodicUITask 
    {
        public void Play()
	    { 
            _DispatcherTimer.Start();
	    }

        public PeriodicUITask()
        { }

	    protected PeriodicUITask( int rateMs )
	    {
		    _DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            _DispatcherTimer.Interval =  TimeSpan.FromMilliseconds(rateMs);
            _DispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
 	    }
	    private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            OnTick();
        }
	    protected abstract void OnTick();

	    private DispatcherTimer _DispatcherTimer ;

    }
}
