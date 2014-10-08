using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRibbonDCI.Domain;
using MyRibbonDCI.Presentation;

namespace MyRibbonDCI
{
    public class RibbonApp : Form
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new RibbonApp());
        }
        public Surface TouchSurface {get; private set; }
        public TrackSet ShapesModel { get; private set; }

        private static readonly int MAX_OPEN_TRACKS = 20;
        private Track[] BeingDrawnShapes = new Track[MAX_OPEN_TRACKS];

        public RibbonApp()
        {
            // Creation logic
            ShapesModel = new TrackSet(); 
            TouchSurface = new Surface(this);

            // Wire up UIs
            SetupOtherUI();           
        }

        public void TrackDrawingShape(int index, MyRibbonDCI.Domain.Track beingDrewTrack)
        {
            BeingDrawnShapes[index] = beingDrewTrack;
        }

        public Track GetBeingDrawnShape(int shapeId)
        {
            return BeingDrawnShapes[shapeId];
        }

        private void clearButton_Click(object sender, System.EventArgs e)
        {
            //TouchSurface.Clear();
        }

        #region Setup UI

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RibbonForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "RibbonApp";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.GroupBox groupBox1;

        private void SetupOtherUI()
        {
            this.clearButton = new System.Windows.Forms.Button();

            this.groupBox1 = new System.Windows.Forms.GroupBox();

            // Clear Button 
            this.clearButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.clearButton.Location = new System.Drawing.Point(592, 504);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);

            // GroupBox 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right);
            this.groupBox1.Location = new System.Drawing.Point(16, 24);
            this.groupBox1.Size = new System.Drawing.Size(664, 50);
            this.groupBox1.Text = "Use left/right mouse as 1st/2nd touches";

            // Set up how the form should be displayed and add the controls to the form. 
            this.ClientSize = new System.Drawing.Size(696, 534);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { 
                                    this.clearButton,this.TouchSurface, this.groupBox1});
            this.Text = "Ribbon DCI Style";
        }

        #endregion
    }
}