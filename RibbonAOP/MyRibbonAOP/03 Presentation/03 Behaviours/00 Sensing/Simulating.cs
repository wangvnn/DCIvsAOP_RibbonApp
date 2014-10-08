using LinFu.DynamicProxy;
using MyRibbonAOP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Westwind.Utilities.Dynamic;

namespace MyRibbonAOP.Presentation
{
    public class Simulating : Sensing
    {
        // after surface's onSizeChanged called
        protected override void onSizeChanged(InvocationInfo info)
        {
            dynamic simulatedpad = new SimulatedPad(200, 200); // create
            #region AOP Tricks   
            Tracking_TouchPad interceptor = new Tracking_TouchPad(simulatedpad);
            ProxyFactory factory = new ProxyFactory();
            SimulatedPad simulatedPad = factory.CreateProxy<SimulatedPad>(interceptor);
            ((Expando)simulatedpad)["self"] = simulatedPad;
            #endregion                
            simulatedPad.setView(_surface);
        }

        #region AOP Tricks
        protected readonly Surface _surface;
        public Simulating(Surface target)
        {
            _surface = target;
        }
        #endregion
    }
}
