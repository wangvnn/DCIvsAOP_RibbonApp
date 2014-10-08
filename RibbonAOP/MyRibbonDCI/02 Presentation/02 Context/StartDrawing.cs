using MyRibbonDCI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyRibbonDCI.Presentation
{
    public class StartDrawing
    {        
        public StartDrawing(Track newShape, Surface onTouchScreen, Point atPoint){
            NewShape = newShape;
            Shapes = onTouchScreen.Controller.ShapesModel;
            PointToStart = atPoint;
            TouchRecorder = new StartNewTrack(NewShape, Shapes, PointToStart);
            ShapeTracker = onTouchScreen.Controller;
        }
        interaction void DoIt() {
            ShapeTracker.Remember(NewShape);
        }

        role NewShape : Track {}
        role PointToStart : Point {}
        role Shapes : TrackSet {}

        role ShapeTracker : RibbonApp 
        {
           entry void Remember(Track shape) {
                ShapeTracker.TrackDrawingShape(shape.Id, shape);
                TouchRecorder.StartUp();
            }
        }

        role TouchRecorder : StartNewTrack {
            void StartUp() {
                TouchRecorder.Start();
            }            
        }
    }
}
