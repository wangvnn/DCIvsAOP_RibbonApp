using MyRibbonDCI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyRibbonDCI.Presentation
{
    public class KeepDrawing
    {        
        public KeepDrawing(int shapeId, Surface onTouchScreen, Point newPoint){
            NewPointToAdd = newPoint;
            BeingDrawnShapeId = shapeId;
            TouchScreen = onTouchScreen;
        }
        interaction void Draw() {
            BeingDrawnShape = new FindBeingDrawnShape(BeingDrawnShapeId, TouchScreen).Find();
            if (BeingDrawnShape != null)
                new UpdateTrack(BeingDrawnShape, NewPointToAdd).Update();
        }

        role BeingDrawnShapeId : int {}
        role BeingDrawnShape : Track {}
        role NewPointToAdd : Point {}
        role TouchScreen : Surface  {}
    }
}
