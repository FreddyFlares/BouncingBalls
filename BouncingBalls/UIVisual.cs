using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BouncingBalls
{
    class UIVisual : FrameworkElement
    {
        DrawingVisual vis;

        // Using a DependencyProperty as the backing store for Fill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillProperty =
            Shape.FillProperty.AddOwner(typeof(UIVisual), new PropertyMetadata(Brushes.Black));
        public SolidColorBrush Fill
        {
            get { return (SolidColorBrush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public UIVisual()
        {
            Loaded += UIVisual_Loaded;
        }

        private void UIVisual_Loaded(object sender, RoutedEventArgs e)
        {
            vis = new DrawingVisual();
            using (DrawingContext dc = vis.RenderOpen())
            {
                dc.DrawEllipse(Fill, null, new Point(Width / 2, Height / 2), Width / 2, Height / 2);
            }
            AddVisualChild(vis);
            AddLogicalChild(vis);
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return vis;
        }
    }
}
