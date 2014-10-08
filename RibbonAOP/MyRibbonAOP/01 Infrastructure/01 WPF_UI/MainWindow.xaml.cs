using LinFu.DynamicProxy;
using MyRibbonAOP.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyRibbonAOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region AOP Tricks
        private Surface surfaceTouching1 = null;
        private Surface surfaceRendering1 = null;
        private Surface surfaceSimulating1 = null;
        private Surface surface1 = null;

        private Surface surfaceTouching2 = null;
        private Surface surfaceRendering2 = null;
        private Surface surfaceSimulating2 = null;
        private Surface surface2 = null;

        private void SetupSurface(InkCanvas renderer, ref Surface surface, ref Surface surfaceTouching, ref Surface surfaceRendering, ref Surface surfaceSimulating)
        {
            surface = new Surface(renderer);
            ProxyFactory factory = new ProxyFactory();

            Tracking_Surface interceptorTracking_Surface = new Tracking_Surface(surface);
            var surfaceProxyTemp = factory.CreateProxy<Surface>(interceptorTracking_Surface);
            surfaceProxyTemp.init();

            Rendering interceptorRendering = new Rendering(surface);
            surfaceRendering = factory.CreateProxy<Surface>(interceptorRendering);
            surfaceRendering.initRender();
            surfaceRendering.afterSetTrack();
            surface["self"] = surfaceRendering;

            Touching interceptorTouching = new Touching(surface);
            surfaceTouching = factory.CreateProxy<Surface>(interceptorTouching);

            Simulating interceptorSimulating = new Simulating(surface);
            surfaceSimulating = factory.CreateProxy<Surface>(interceptorSimulating);  
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            // like any touch screen of any device
            // each touch pad has: touch screen to detech touches and rendering screen to draw stuff
            SetupSurface(renderer1, ref surface1, ref surfaceTouching1, ref surfaceRendering1, ref surfaceSimulating1);
            SetupSurface(renderer2, ref surface2, ref surfaceTouching2, ref surfaceRendering2, ref surfaceSimulating2);

            touchArea1.LayoutUpdated += WhenLayoutUpdated1;
            touchArea2.LayoutUpdated += WhenLayoutUpdated2;            
        }

        #region Touch Simulation 
        private bool triggerLayoutUpdated1 = false;
        public void WhenLayoutUpdated1(object sender, EventArgs e)
        {
            if (!triggerLayoutUpdated1)
            {
                surfaceTouching1.onSizeChanged();
                surfaceSimulating1.onSizeChanged();
                triggerLayoutUpdated1 = true;
            }
        }

        private bool triggerLayoutUpdated2 = false;
        public void WhenLayoutUpdated2(object sender, EventArgs e)
        {
            if (!triggerLayoutUpdated2)
            {
                surfaceTouching2.onSizeChanged();
                surfaceSimulating2.onSizeChanged();
                triggerLayoutUpdated2 = true;
            }
        }
        private void surface1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(touchArea1);
            surface1.TouchStart(0, (int)p.X, (int)p.Y);
        }

        private void surface1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(touchArea1);
            surface1.TouchEnd(0, (int)p.X, (int)p.Y);
        }

        private void surface1_MouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(touchArea1);

            surface1.TouchMoving(0, (int)p.X-5, (int)p.Y-5);
            surface1.TouchMoving(1, (int)p.X+5, (int)p.Y+5);

        }

        private void surface1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(touchArea1);
            surface1.TouchStart(1, (int)p.X, (int)p.Y);
        }

        private void surface1_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(touchArea1);
            surface1.TouchEnd(1, (int)p.X, (int)p.Y);
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            surfaceRendering1.onClear();
            surfaceRendering2.onClear();
        }

        private void surface2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(touchArea2);
            surface2.TouchStart(0, (int)p.X, (int)p.Y);

        }

        private void surface2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(touchArea2);
            surface2.TouchEnd(0, (int)p.X, (int)p.Y);
        }

        private void surface2_MouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(touchArea2);

            surface2.TouchMoving(0, (int)p.X - 5, (int)p.Y - 5);
            surface2.TouchMoving(1, (int)p.X + 5, (int)p.Y + 5);

        }

        private void surface2_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(touchArea2);
            surface2.TouchStart(1, (int)p.X, (int)p.Y);
        }

        private void surface2_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(touchArea2);
            surface2.TouchEnd(1, (int)p.X, (int)p.Y);
        }
        #endregion
    }
}
