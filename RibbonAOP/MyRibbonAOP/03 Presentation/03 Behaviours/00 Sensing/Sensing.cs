using LinFu.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonAOP.Presentation
{

    public class Sensing : IInvokeWrapper
    {
        // after surface's onSizeChanged called
        protected virtual void onSizeChanged(InvocationInfo info)
        { 
        }

        #region AOP framework
        public Sensing()
        {
    
        }
 
        public void AfterInvoke(InvocationInfo info, object returnValue) {}
        public void BeforeInvoke(InvocationInfo info) {}


        public object DoInvoke(InvocationInfo info)
        {
            if (info.TargetMethod.Name == "onSizeChanged")
            {
                onSizeChanged(info);
            }            
            return null;
        }



        #endregion
    }
}