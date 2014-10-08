using LinFu.DynamicProxy;
using MyRibbonAOP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonAOP.Presentation
{
    public class Touching : Sensing
    {
        // after surface's onSizeChanged called
        protected override void onSizeChanged(InvocationInfo info)
        {
            TouchView touchview = new TouchView(); // create
            #region AOP Tricks   
            Tracking_TouchPad interceptor = new Tracking_TouchPad(touchview);
            ProxyFactory factory = new ProxyFactory();
            TouchView touchView = factory.CreateProxy<TouchView>(interceptor);
            touchview["self"] = touchView;
            #endregion
            touchView.setView(_surface); // hook up with surface            
        }

        #region AOP Tricks
        protected readonly Surface _surface;
        public Touching(Surface target)
        {
            _surface = target;
        }
        #endregion
    }
}
