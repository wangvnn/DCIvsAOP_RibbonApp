using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonAOP.Presentation
{
    public interface TouchPad
    {
        void onTouchDown(int pointerId, int x, int y);
        void onTouchMoving(int pointerId, int x, int y);
        void onTouchUp(int pointerId);

        void setView(View v);
    }
}
