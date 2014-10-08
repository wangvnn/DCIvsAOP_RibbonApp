using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonAOP.Domain
{
    public class Track : IEnumerable
    {
        public Track()
	    {
		    points = new LinkedList<Point>();
		    lastPoint = Point.INFINITY;
	    }

	    public void addPoint(Point p)
	    {
		    if( !closed && p.manhattanDistance(lastPoint) >= MIN_DELTA )
		    {
			    points.AddLast(p);
			    lastPoint = p;
		    }
	    }

	    public bool isClosed()
	    {
		    return closed;
	    }

	    public void close()
	    {
		    closed = true;
	    }

        public void clear()
        {
            points.Clear();
            lastPoint = Point.INFINITY;
            closed = false;
        }

        public void remove(Point p)
        {
            points.Remove(p);
        }

        public int Count { get { return points.Count;  } }
	    private static readonly float MIN_DELTA = 4;

	    private Point lastPoint;
	    private LinkedList<Point> points;
	    private bool closed = false;

        // ignore coloring aspect, using simple Id
        public int Id = 0;

        #region enumerable

        // IEnumerable Member
        public IEnumerator GetEnumerator()
        {
            foreach (object o in points)
            {
                // Lets check for end of list (its bad code since we used arrays)
                if (o == null)
                {
                    break;
                }

                // Return the current element and then on next function call 
                // resume from next element rather than starting all over again;
                yield return o;
            }
        }
        #endregion
    }
}
