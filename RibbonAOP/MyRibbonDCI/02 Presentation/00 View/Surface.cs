using MyRibbonDCI.Domain;
using MyRibbonDCI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyRibbonDCI.Presentation
{
    public class Surface : Panel
    {
        private static readonly int FIRST_TOUCH_INDEX = 0;
        private static readonly int SECOND_TOUCH_INDEX = 0;

        private Movie _Renderer =null;

        // sub view1
        public RibbonSet Shapes { get; private set; }

        // controller
        public RibbonApp Controller { get; private set; }

        public Surface(RibbonApp controller)
        {
            Controller = controller;
            _Renderer = new Movie(this);
            _Renderer.Play();
            Shapes = new RibbonSet(Controller.ShapesModel);
            this.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Location = new System.Drawing.Point(16, 60);
            this.Size = new System.Drawing.Size(664, 320);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TouchUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnDraw);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TouchMoving);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TouchDown);
        }

        private int GetShapeId(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) return FIRST_TOUCH_INDEX;
            if (e.Button == System.Windows.Forms.MouseButtons.Right) return SECOND_TOUCH_INDEX;
            return 0;           
        }

        private void TouchDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            new StartDrawing(new Track(GetShapeId(e)), this, new MyRibbonDCI.Domain.Point(e.X, e.Y)).DoIt();
            this.Focus();
        }

        private void TouchMoving(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            new KeepDrawing(GetShapeId(e), this, new MyRibbonDCI.Domain.Point(e.X, e.Y)).Draw();
        }

        private void TouchUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            new StopDrawing(GetShapeId(e), this, new MyRibbonDCI.Domain.Point(e.X, e.Y)).DoIt();
        }

        private void OnDraw(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Shapes.DisplayOn(e.Graphics);
        }

        public void Clear()
        {
            Invalidate();
        }
    }
}