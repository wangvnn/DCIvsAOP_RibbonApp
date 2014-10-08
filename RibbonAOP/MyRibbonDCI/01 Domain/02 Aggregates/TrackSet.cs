using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonDCI.Domain
{
    public class TrackSet : IEnumerable
    {
        public TrackSet()
	    {
		    _tracks = new LinkedList<Track>();		   
		}	 

        public void Clear()
        {
            _tracks.Clear();
        }

        public void AddTrack(Track t)
        {
            _tracks.AddLast(t);
        }

        public void Remove(Track t)
        {
            _tracks.Remove(t);
        }

        private LinkedList<Track> _tracks;

        #region IEnumerable Member
        public IEnumerator GetEnumerator()
        {
            foreach (object o in _tracks)
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
