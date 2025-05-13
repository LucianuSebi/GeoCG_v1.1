using CGEntity;
using netDxf;
using netDxf.Entities;
using netDxf.Tables;
using static netDxf.Entities.HatchBoundaryPath;
using Polyline = CGEntity.Polyline;

namespace FunctionLibrary
{
    public class DxfToCGLib
    {

        public static Polyline ToCGPolyline(Polyline2D polyline2d)
        {
            List<Vertex> vertices = new List<Vertex>();
            foreach(var vertex in polyline2d.Vertexes)
            {
                vertices.Add(new Vertex(vertex.Position.X, vertex.Position.Y, true));
            }
            return new Polyline(vertices);
        }

    }
}
