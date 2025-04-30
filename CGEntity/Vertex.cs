using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGEntity
{
    public class Vertex
    {
        public int pointId=0;
        public int pointNo=0;
        public double X, Y;
        private static int counter = 0;
        public Vertex(int pointNo, double X, double Y) {
            this.pointNo = pointNo;
            this.pointId = ++counter;
            this.X = X;
            this.Y = Y;
        }
        public Vertex(double X, double Y)
        {;
            this.X = X;
            this.Y = Y;
        }
    }
}
