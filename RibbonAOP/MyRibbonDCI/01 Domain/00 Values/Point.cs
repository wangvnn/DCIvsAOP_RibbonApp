using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonDCI.Domain
{
    public class Point 
    {
        public Point(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        public float ManhattanDistance(Point p)
         {
             return Math.Abs(p.X- X) + Math.Abs(p.Y - Y);
         }
         public static readonly Point INFINITY =  new Point(float.MaxValue, float.MaxValue);

         public float X { get; private set; }
         public float Y { get; private set; }
    }
}
