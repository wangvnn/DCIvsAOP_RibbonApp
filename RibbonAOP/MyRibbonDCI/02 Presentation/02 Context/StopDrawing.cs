using MyRibbonDCI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyRibbonDCI.Presentation
{
    public class StopDrawing
    {        
        public StopDrawing(int shapeId, Surface onTouchScreen, Point lastPoint){
            LastPointToAdd = lastPoint;
            BeingDrawnShapeId = shapeId;
            BeingDrawnShape = new FindBeingDrawnShape(BeingDrawnShapeId, onTouchScreen).Find();
            TouchRecorder = new StopTrack(BeingDrawnShape, LastPointToAdd);
            ShapeTracker = onTouchScreen.Controller;
        }

        // Methodless
        role BeingDrawnShapeId : int {}
        role BeingDrawnShape : Track {}
        role LastPointToAdd : Point {}

        // Main entry point of Context --> Action in Filming
        interaction void DoIt() {            
            // Film Director ask first actor to act
            TouchRecorder.Shutdown();
        }
    
        // first actor has entry
        role TouchRecorder : StopTrack {
            entry void Shutdown() {
                // do his stuff
                TouchRecorder.Stop();
                // ask 2nd actor do his stuff
                ShapeTracker.Forget(BeingDrawnShapeId);
            }            
        }
        // 2nd actor
        role ShapeTracker : RibbonApp {
            void Forget(int shapeId) {
                // finish his stuff -> The end
                ShapeTracker.TrackDrawingShape(shapeId, null);
            }
        }
    }
}
