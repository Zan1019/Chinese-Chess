using System;
using System.Collections.Generic;
using System.Text;

namespace XQ2
{
    class Point
    {
        private int X;
        private int Y;



        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public int getX()
        {
            return X;
        }

        public int getY()
        {
            return Y;
        }

        public void setX(int X)
        {
            this.X = X;
        }

        public void setY(int Y)
        {
            this.Y = Y;
        }

        public void OutPut()
        {
            Console.WriteLine("(" + this.X + "," + this.Y + ")");
        }

        
    }
}
