using LinFu.DynamicProxy;
using MyRibbonAOP.Presentation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbonAOP.Domain
{
    public class TrackSet : IEnumerable
    {
        public TrackSet(int maxOpenTracks)
	    {
		    tracks = new LinkedList<Track>();
		    openTracks = new List<Track>() ;
            for (int i = 0; i < maxOpenTracks; ++i)
                openTracks.Add(null);
		}

	    public void startNewTrack(int trackId, float x, float y)
	    {
		    Track t = new Track();
            t.Id = trackId;
		    tracks.AddLast(t);
            dynamic p = new Point(x, y);

            #region AOP Tricks
            Aging_Point interceptor = new Aging_Point(p);
            ProxyFactory factory = new ProxyFactory();
            var point = factory.CreateProxy<Point>(interceptor);
            point.init(); 
            #endregion

            t.addPoint(p);

            if (trackId < openTracks.Count)
            {
                Track prev = openTracks[trackId];
                if (prev != null)
                    prev.close();
            }
		    openTracks[trackId] = t;
	    }

	    public void addPoint(int trackId, float x, float y)
	    {
            if (trackId < openTracks.Count)
            {
                Track t = openTracks[trackId];
                if (t != null)
                {
                    dynamic p = new Point(x, y);
                    #region AOP Tricks
                    Aging_Point interceptor = new Aging_Point(p);
                    ProxyFactory factory = new ProxyFactory();
                    var point = factory.CreateProxy<Point>(interceptor);
                    point.init();
                    #endregion
                    t.addPoint(p);
                }
            }
	    }

	    public void endTrack(int trackId)
	    {
		    Track t = openTracks[trackId];
		    if( t != null )
             t.close();
	    }

        public void clear()
        {
            foreach (var t in tracks)
            {
                t.clear();
            }
            tracks.Clear();

            for (var i=0; i < openTracks.Count; ++i)
            {
                openTracks[i] = null;
            }
        }

        public void remove(Track t)
        {
            tracks.Remove(t);
            openTracks[t.Id] = null;
        }

        private LinkedList<Track> tracks;
	    private List<Track> openTracks;

        #region IEnumerable Member
        public IEnumerator GetEnumerator()
        {
            foreach (object o in tracks)
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
