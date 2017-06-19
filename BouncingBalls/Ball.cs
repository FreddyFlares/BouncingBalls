using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BouncingBalls
{
    class Ball : ViewModelBase
    {
        private double x;
        public double X { get { return x; } set { x = value; OnPropertyChanged(); } }

        private double y;
        public double Y { get { return y; } set { y = value; OnPropertyChanged(); } }

        private double radius;
        public double Radius { get { return radius; } set { radius = value; calculateMass(); OnPropertyChanged(); } }

        private Color colour;
        public Color Colour { get { return colour; } set { colour = value; OnPropertyChanged(); } }

        private double xVel;
        public double XVel { get { return xVel; } set { xVel = value; } }

        private double yVel;
        public double YVel { get { return yVel; } set { yVel = value; } }

        private double mass;
        public double Mass { get { return mass; } private set { mass = value; } }

        void calculateMass()
        {
            Mass = radius * radius;
        }

        public void Collide(Ball b)
        {
            if (IsCollidingWith(b))
            {
                double nx = b.x - x;
                double ny = b.y - y;
                double d = (double)Math.Sqrt(nx * nx + ny * ny);
                nx = nx / d;
                ny = ny / d;
                double a1 = xVel * nx + yVel * ny;
                double a2 = b.xVel * nx + b.yVel * ny;
                double p = 2 * (a1 - a2) / (mass + b.mass);
                xVel = xVel - p * nx * b.mass;
                yVel = yVel - p * ny * b.mass;
                b.xVel = b.xVel + p * nx * mass;
                b.yVel = b.yVel + p * ny * mass;
            }
        }

        private bool IsCollidingWith(Ball b)
        {
            double r = radius + b.radius;
            // Fast check
            if (Math.Abs(x - b.x) >= r || Math.Abs(y - b.y) >= r) return false;
            // Slow check
            double d = (x - b.x) * (x - b.x) + (y - b.y) * (y - b.y);
            return (d < r * r && IsMovingTowards(b));
        }

        private bool IsMovingTowards(Ball b)
        {
            return (b.x - x) * (xVel - b.xVel) + (b.y - y) * (yVel - b.yVel) > 0;
        }

    }
}
