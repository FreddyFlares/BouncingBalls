using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace BouncingBalls
{
    class ViewModel : ViewModelBase
    {
        static Random rng = new Random();
        private double areaWidth;
        public double AreaWidth
        {
            get { return areaWidth; }
            set { areaWidth = value; OnPropertyChanged(); }
        }
        private double areaHeight;
        public double AreaHeight
        {
            get { return areaHeight; }
            set { areaHeight = value; OnPropertyChanged(); }
        }

        private double kineticEnergy;
        public double KineticEnergy { get { return kineticEnergy; } set { kineticEnergy = value; OnPropertyChanged("KineticEnergy"); } }

        private ObservableCollection<Ball> balls = new ObservableCollection<Ball>();
        public ObservableCollection<Ball> Balls { get { return balls; } set { balls = value; OnPropertyChanged("Balls"); } }
        private readonly ICommand initCommand;
        public ICommand InitCommand { get { return initCommand; } }
        private readonly ICommand removeBallCommand;
        public ICommand RemoveBallCommand { get { return removeBallCommand; } }
        private readonly ICommand nrBallsChangedCommand;
        public ICommand NrBallschangedCommand { get { return nrBallsChangedCommand; } }

        DispatcherTimer timer;

        public ViewModel()
        {
            //CompositionTarget.Rendering += CompositionTarget_Rendering;
            timer = new DispatcherTimer(DispatcherPriority.Render);
            timer.Interval = TimeSpan.FromSeconds(1/60.0);
            timer.Tick += Timer_Tick;
            timer.Start();
            initCommand = new DelegateCommand(init);
            removeBallCommand = new DelegateCommand(removeBall);
            nrBallsChangedCommand = new DelegateCommand(nrBallsChanged);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            update();
        }

        void init(object o)
        {
        }

        void removeBall(object o)
        {
            Ball ball = o as Ball;
            if (o != null)
                balls.Remove(ball);
        }

        void nrBallsChanged(object o)
        {
            int newNrBalls = Convert.ToInt32(o);
            while (balls.Count < newNrBalls)
                addRandomBall();
            while (balls.Count > newNrBalls)
                balls.RemoveAt(0);
        }

        void addRandomBall()
        {
            Ball ball = new Ball();
            ball.X = AreaWidth / 2;
            ball.Y = AreaHeight / 2;
            ball.Radius = 6 + rng.Next(10);
            ball.XVel = (rng.Next(10001) - 5000) / 2000d;
            ball.YVel = (rng.Next(10001) - 5000) / 2000d;
            if (rng.Next(2) == 0) ball.XVel = -ball.XVel;
            if (rng.Next(2) == 0) ball.YVel = -ball.YVel;
            byte r = (byte)rng.Next(256);
            byte g = (byte)rng.Next(256);
            byte b = (byte)rng.Next(256);
            ball.Colour = Color.FromArgb(255, r, g, b);
            balls.Add(ball);
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            update();
        }

        void update()
        {
            CheckCollisions();
            double k = 0;
            foreach (Ball b in balls)
            {
                if (b.XVel < 0 && b.X - b.Radius < 0) b.XVel = -b.XVel;
                if (b.YVel < 0 && b.Y - b.Radius < 0) b.YVel = -b.YVel;
                if (b.XVel > 0 && b.X + b.Radius >= AreaWidth) b.XVel = -b.XVel;
                if (b.YVel > 0 && b.Y + b.Radius >= AreaHeight) b.YVel = -b.YVel;
                b.X += b.XVel;
                b.Y += b.YVel;
                k += 0.5 * b.Mass * (b.XVel * b.XVel + b.YVel * b.YVel);
            }
            KineticEnergy = k;
        }

        public void CheckCollisions()
        {
            for (int i = 0; i < Balls.Count - 1; i++)
            {
                for (int j = i; j < Balls.Count; j++)
                {
                    balls[i].Collide(balls[j]);
                }
            }
        }
    }
}
