using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyRibbonDCI.Domain;

namespace MyRibbonDCI.Domain
{
    // Encapsulate algo to start a new track
    public class UpdateTrack
    {        
        public UpdateTrack(Track theTrack,  Point addPoint){
            TheTrack = theTrack;
            PointToBeAdded = addPoint;
        }
        interaction void Update() {
            TheTrack.AddPoint(PointToBeAdded);
        }

        role PointToBeAdded : Point {}

        role TheTrack : Track {        
            entry void AddPoint(Point pointToBeAdded) {
                TheTrack.Add(pointToBeAdded);
            }
        }
    }
}
