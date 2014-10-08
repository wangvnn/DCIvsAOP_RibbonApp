using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRibbonAOP;
using System.Windows;
using System.Dynamic;
using Westwind.Utilities.Dynamic;

namespace MyRibbonAOP.Presentation
{
    public class TouchView : Expando, TouchPad, OnTouchListener
    {
        public TouchView()
        {
            setup();
        }

        public virtual void setView(View v)
        {
           v.setOnTouchListener(this);
        }

        public virtual void onTouch(View v, MotionEvent me)
        {
            MotionEventAction action = me.Action;
            int ptrId = me.PointerId;
 
            switch (action)
            {
                case MotionEventAction.ACTION_DOWN:
                case MotionEventAction.ACTION_POINTER_DOWN:
                    {
                        self.onTouchDown(ptrId, me.X, me.Y);
                        break;
                    }
                case MotionEventAction.ACTION_MOVE:
                    {
                        self.onTouchMoving(ptrId, me.X, me.Y);
                        break;
                    }
                case MotionEventAction.ACTION_UP:
                case MotionEventAction.ACTION_POINTER_UP:
                case MotionEventAction.ACTION_CANCEL:
                    {
                        self.onTouchUp(ptrId);
                        break;
                    }                    
            }
        }

        #region Dirty tricks for AOP to work

        private void setup()
        {
            this["self"] = this; // dirty trick to deal with dynamic property
         }
        // need to add these for OAP to work
        public virtual void onTouchDown(int pointerId, int x, int y)
        {
        }

        public virtual void onTouchMoving(int pointerId, int x, int y)
        {
        }

        public virtual void onTouchUp(int pointerId)
        {
        }

        private TouchView self { get { return ((TouchView)this["self"]); } }
        #endregion
    }
}
