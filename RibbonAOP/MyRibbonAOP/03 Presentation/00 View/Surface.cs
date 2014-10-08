using MyRibbonAOP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Westwind.Utilities.Dynamic;

namespace MyRibbonAOP.Presentation
{
    public class Surface :  Expando, View
    {
        public Surface(InkCanvas renderer)
        {
            this.renderer = renderer;
            setup();
        }

        #region Simulate android View: Drawing and Touching

        public void invalidate()
        {
            self.onDraw(renderer);
        }

        void View.setOnTouchListener(OnTouchListener listener)
        {
            this.touchListener = listener;
        }
        public virtual void onClear()
        {
            renderer.Strokes.Clear();
        }

        public void TouchStart(int id, int x, int y)
        {
            touchListener.onTouch(this, new MotionEvent(MotionEventAction.ACTION_POINTER_DOWN, id, x, y));
        }
        public void TouchMoving(int id, int x, int y)
        {
            touchListener.onTouch(this, new MotionEvent(MotionEventAction.ACTION_MOVE, id, x, y));
        }
        public void TouchEnd(int id, int x, int y)
        {
            touchListener.onTouch(this, new MotionEvent(MotionEventAction.ACTION_POINTER_UP, id, x, y));
        }
        
        private OnTouchListener touchListener = null;
        public InkCanvas renderer { get; private set; }
        #endregion

        #region Dirty Trick for AOP
        static int IdCounter = 0;
        public int Id { get; private set; }
        public virtual void init()
        {
        }
        public virtual void onDraw(InkCanvas renderer)
        {

        }
        private Surface self { get { return ((Surface)this["self"]);  } }
        private void setup()
        {
            this["self"] = this;
            this.Id = IdCounter++;
        }
        // need to add these for OAP to work
        public virtual void onSizeChanged()
        {
        }

        // dirty trick to trigger APO, after set(tracks)
        public virtual void afterSetTrack()
        {
        }
        // dirty trick to trigger APO, after constructor, add movie
        public virtual void initRender()
        {
        }

        #endregion
    }
}
