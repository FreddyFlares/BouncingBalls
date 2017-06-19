using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BouncingBalls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;

        public MainWindow()
        {
            DataContextChanged += MainWindow_DataContextChanged;
            InitializeComponent();
        }

        void MainWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            viewModel = (ViewModel)e.NewValue;
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement ell = (FrameworkElement)sender;
            viewModel.RemoveBallCommand.Execute(ell.DataContext);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.NrBallschangedCommand.Execute(sldNrBalls.Value);
            sldNrBalls.ValueChanged += sldNrBalls_ValueChanged;
        }

        void sldNrBalls_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            viewModel.NrBallschangedCommand.Execute(e.NewValue);
        }
    }
}
