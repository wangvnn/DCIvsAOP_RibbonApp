using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyRibbonDCI.Domain;

namespace MyRibbonDCI.Presentation
{
    public class FindBeingDrawnShape
    {        
        public FindBeingDrawnShape(int shapeId, Surface onTouchScreen){
            BeingDrawnShapeId = shapeId;
            ShapeTracker = onTouchScreen.Controller;
        }
        interaction Track Find() {
            return ShapeTracker.FindBeingDrawnTrack();
        }

        role BeingDrawnShapeId : int {}
        role ShapeTracker : RibbonApp  {
            entry Track FindBeingDrawnTrack() {
                return ShapeTracker.GetBeingDrawnShape(BeingDrawnShapeId);
            }
        }

    }
}
