using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGEntity
{
    public class Polyline
    {
        public List<Vertex> Vertices = new List<Vertex>();
        public int Area = 0;

        public Polyline(){}

        public Polyline(List<Vertex> Vertices)
        {
            this.Vertices = Vertices;
            Area = CalculateArea();
        }
        public  Polyline(Polyline polyline)
        {
            this.Vertices = polyline.Vertices;
            Area = CalculateArea();
        }
        protected int CalculateArea()
        {
            if (Vertices == null || Vertices.Count < 3)
                return 0;

            var uniqueVertices = Vertices.GroupBy(v => new { v.X, v.Y }).Select(g => g.First()).ToList();
            if (uniqueVertices.Count < 3)
                return 0;

            int n = uniqueVertices.Count;
            decimal centerX = uniqueVertices.Sum(v => (decimal)v.X) / n;
            decimal centerY = uniqueVertices.Sum(v => (decimal)v.Y) / n;

            var orderedVertices = uniqueVertices.OrderBy(v => Math.Atan2((double)((decimal)v.Y - centerY), (double)((decimal)v.X - centerX))).ToList();

            decimal area = 0M;
            n = orderedVertices.Count;
            for (int i = 0; i < n; i++)
            {
                int j = (i + 1) % n;
                decimal x1 = (decimal)orderedVertices[i].X;
                decimal y1 = (decimal)orderedVertices[i].Y;
                decimal x2 = (decimal)orderedVertices[j].X;
                decimal y2 = (decimal)orderedVertices[j].Y;
                area += (x1 * y2) - (x2 * y1);
            }
            return (int)Math.Round((double)(Math.Abs(area) / 2M));
        }


    }
}
