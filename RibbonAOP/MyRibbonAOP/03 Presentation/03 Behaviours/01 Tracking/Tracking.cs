using LinFu.DynamicProxy;
using MyRibbonAOP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonAOP.Presentation
{
    public class Tracking_Surface : IInvokeWrapper
    {
        private void afterConstructorCall()
        {
            _surface.tracks = new TrackSet(20);
        }

        #region AOP Tricks
        protected readonly dynamic _surface;
        public Tracking_Surface(dynamic target)
        {
            _surface = target;
        }

        public void AfterInvoke(InvocationInfo info, object returnValue) { }
        public void BeforeInvoke(InvocationInfo info) { }

        public object DoInvoke(InvocationInfo info)
        {
            if (info.TargetMethod.Name == "init")
            {
                info.TargetMethod.Invoke(_surface, info.Arguments);
                afterConstructorCall();
            }

            return null;
        }


        #endregion
    }

    public class Tracking_TouchPad : IInvokeWrapper
    {
        // when a touchpad-derived object is created within a surface context,
        // put the surface in the touchpad
        private void afterDerivedConstructorCall(Surface surface)
        {
            _touchPad.surface = surface;
        }
        // handle onTouch event in touchpad-derived objec
        private void onTouchDown(int pointerId, float x, float y)
        {
            _touchPad.surface.tracks.startNewTrack(pointerId, x, y);
        }
        private void onTouchMoving(int pointerId, float x, float y)
        {
            _touchPad.surface.tracks.addPoint(pointerId, x, y);
        }
        private void onTouchUp(int pointerId)
        {
            _touchPad.surface.tracks.endTrack(pointerId);
        }   

        #region AOP Trick
        protected readonly dynamic _touchPad;
        public Tracking_TouchPad(dynamic target)
        {
            _touchPad = target;
        }      

        public void AfterInvoke(InvocationInfo info, object returnValue) { }
        public void BeforeInvoke(InvocationInfo info) { }

        public object DoInvoke(InvocationInfo info)
        {
            if (info.TargetMethod.Name == "setView")
            {
                // invoked from touchpad.setView(v)
                info.TargetMethod.Invoke(_touchPad, info.Arguments);
                afterDerivedConstructorCall((Surface)info.Arguments[0]); 
            }
            else if (info.TargetMethod.Name == "onTouchDown")
            {
                onTouchDown((int)info.Arguments[0], (int)info.Arguments[1], (int)info.Arguments[2]);
            }
            else if (info.TargetMethod.Name == "onTouchMoving")
            {
                onTouchMoving((int)info.Arguments[0], (int)info.Arguments[1], (int)info.Arguments[2]);
            }
            else if (info.TargetMethod.Name == "onTouchUp")
            {
                onTouchUp((int)info.Arguments[0]);
            }
            return null;
        }



        #endregion
    }
}
