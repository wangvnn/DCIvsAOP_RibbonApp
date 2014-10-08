using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonAOP
{
    public interface OnTouchListener
    {
        void onTouch(View v, MotionEvent me);
    }

    public interface View
    {
        void setOnTouchListener(OnTouchListener listener);
    }
    public enum MotionEventAction
    {
        ACTION_DOWN,
        ACTION_POINTER_DOWN,
        ACTION_MOVE,
        ACTION_UP,
        ACTION_POINTER_UP,
        ACTION_CANCEL
    }

    public class MotionEvent
    {

        public MotionEvent(MotionEventAction index, int id, int x, int y)
        {
            Action = index;
            PointerId = id;
            X = x;
            Y = y;
        }
        public MotionEventAction Action { get; private set; }
        public int PointerId { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
    }
}
