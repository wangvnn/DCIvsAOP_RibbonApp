using LinFu.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Westwind.Utilities.Dynamic;

namespace MyRibbonAOP.Presentation
{
    public class Rendering : IInvokeWrapper
    {
        private void afterSurfaceConstructor()
        {
            _surface.movie = new Movie((Surface)_surface);
            _surface.movie.play();   
        }

        private void afterSetTrackSet()
        {
            _surface.ribbons = new RibbonSet(_surface.tracks);
            #region AOP Tricks
            Aging_RibbonSet interceptor = new Aging_RibbonSet(_surface.ribbons);
            ProxyFactory factory = new ProxyFactory();
            RibbonSet ribbonSetProxy = factory.CreateProxy<RibbonSet>(interceptor);
            ((Expando)_surface.ribbons)["self"] = ribbonSetProxy;
            #endregion
        }

        private void onDraw()
        {
            if (_surface.ribbons != null)
                _surface.ribbons.displayOn(_surface.renderer);
        }

        private void onClear()
        {
            if (_surface.ribbons != null)
                _surface.ribbons.clear();
        }


        #region AOP Tricks
        protected readonly dynamic _surface;
        public Rendering(dynamic target)
        {
            _surface = target;
        }

        public void AfterInvoke(InvocationInfo info, object returnValue) { }
        public void BeforeInvoke(InvocationInfo info) { }


        public object DoInvoke(InvocationInfo info)
        {
            if (info.TargetMethod.Name == "afterSetTrack")
            {
                afterSetTrackSet();               
            }
            else if (info.TargetMethod.Name == "initRender")
            {
                afterSurfaceConstructor();
            }

            else if (info.TargetMethod.Name == "onDraw")
            {
                onDraw();
            }
            else if (info.TargetMethod.Name == "onClear")
            {
                onClear();
                info.TargetMethod.Invoke(_surface, info.Arguments);
            }     
            return null;
        }



        #endregion
    }
}
