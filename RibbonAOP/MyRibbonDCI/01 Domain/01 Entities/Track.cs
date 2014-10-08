using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonDCI.Domain
{
    public class Track : IEnumerable
    {
        public Track(int id)
	    {
            Id = id;
		    _points = new LinkedList<Point>();
		    _lasPoint = Point.INFINITY;
	    }

	    public void Add(Point p)
	    {
		    if(p.ManhattanDistance(_lasPoint) >= MIN_DELTA )
		    {
			    _points.AddLast(p);
			    _lasPoint = p;
		    }
	    }


        public void Clear()
        {
            _points.Clear();
            _lasPoint = Point.INFINITY;
        }

        public void Remove(Point p)
        {
            _points.Remove(p);
        }

        public int Count { get { return _points.Count;  } }
	    private static readonly float MIN_DELTA = 4;

	    private Point _lasPoint;
	    private LinkedList<Point> _points;

        public int Id  {get; private set; }

        #region enumerable

        // IEnumerable Member
        public IEnumerator GetEnumerator()
        {
            foreach (object o in _points)
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
