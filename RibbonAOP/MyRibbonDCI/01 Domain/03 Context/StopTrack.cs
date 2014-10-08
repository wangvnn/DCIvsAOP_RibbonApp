using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyRibbonDCI.Domain;

namespace MyRibbonDCI.Domain
{
    // Encapsulate algo to start a new track
    public class StopTrack
    {        
        public StopTrack(Track theTrack,  Point atPoint){
            TheTrack = theTrack;
            PointToStop = atPoint;
        }
        interaction void Stop() {
            TheTrack.Stop(PointToStop);
        }

        role PointToStop : Point {}

        role TheTrack : Track {        
            entry void Stop(Point pointToStop) {
                TheTrack.Add(pointToStop);
            }
        }
    }
}
