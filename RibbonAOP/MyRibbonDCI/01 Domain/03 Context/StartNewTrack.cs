using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyRibbonDCI.Domain;

namespace MyRibbonDCI.Domain
{
    // Encapsulate algo to start a new track
    public class StartNewTrack
    {        
        public StartNewTrack(Track theTrack, TrackSet inTheTrackSet, Point atPoint){
            TheTrackSet = inTheTrackSet;
            TheTrack = theTrack;
            PointToStart = atPoint;
        }

        role PointToStart : Point {}
        interaction void Start() {
            TheTrack.Start(PointToStart);
        }

        role TheTrack : Track {        
            entry void Start(Point atPoint) {
                TheTrack.Add(atPoint);
                TheTrackSet.StartTrack(TheTrack);
            }
        }

        role TheTrackSet : TrackSet {
            void StartTrack(Track track) {  
                TheTrackSet.AddTrack(track);
            }
        }
    }
}
