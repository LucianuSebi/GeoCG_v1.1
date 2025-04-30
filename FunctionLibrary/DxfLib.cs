using CGEntity;
using netDxf;
using netDxf.Entities;
using netDxf.Tables;
using static netDxf.Entities.HatchBoundaryPath;

namespace FunctionLibrary
{
    public class DxfLib
    {

        public static List<Text> TextFromDxf(string Layer, DxfDocument dxf)
        {
            List<Text> text = new List<Text>();
            foreach (Text textEntity in dxf.Entities.Texts)
                if (textEntity.Layer != null && textEntity.Layer.Name.Equals(Layer, StringComparison.OrdinalIgnoreCase))
                    text.Add(textEntity);
            return text;
        }

        public static List<Text> TextRangeSort(List<Text> lista,int start, int end)
        {
            List<Text> listaAux = new List<Text>();
            foreach (Text numar in lista)
                if (int.TryParse(numar.Value, out int idValue) && idValue >= start && idValue <= end)
                    listaAux.Add(numar);
            return listaAux;
        }

        public static List<Polyline2D> PolylineFromDxf(string Layer, DxfDocument dxf)
        {
            List<Polyline2D> polylines = new List<Polyline2D>();
            foreach (Polyline2D polylineEntity in dxf.Entities.Polylines2D)
                if (polylineEntity.IsClosed && polylineEntity.Layer.Name == Layer)
                    polylines.Add(polylineEntity);
            return polylines;
        }

        public static List<Polyline2D> FindParent(Vertex child, List<Polyline2D> polylines)
        {
            List<Polyline2D> parentPolylines = new List<Polyline2D>();
            foreach (Polyline2D polyline in polylines)
                if (IsInsidePolygon(child, polyline))
                    parentPolylines.Add(polyline);
            return parentPolylines;
        }

        public static List<Polyline2D> FindParent(Polyline2D child, List<Polyline2D> polylines)
        {
            List<Polyline2D> parentPolylines = new List<Polyline2D>();
            foreach (Polyline2D polyline in polylines)
                if (IsInsidePolygon(child, polyline))
                    parentPolylines.Add(polyline);
            return parentPolylines;
        }

        public static List<Polyline2D> FindChild(Polyline2D parent, List<Polyline2D> polylines)
        {
            List<Polyline2D> childPolylines = new List<Polyline2D>();
            foreach (Polyline2D polyline in polylines)
                if (IsInsidePolygon(polyline, parent))
                    childPolylines.Add(polyline);
            return childPolylines;
        }

        public static List<Text> FindChild(Polyline2D parent, List<Text> texts)
        {
            List<Text> childTexts = new List<Text>();
            foreach (Text text in texts)
                if (IsInsidePolygon(new Vertex(text.Position.X, text.Position.Y), parent))
                    childTexts.Add(text);
            return childTexts;
        }

        public static void SaveChildren(List<Text> texts, Polyline2D parent, DxfDocument dxf)
        {
            foreach (Text text in texts)
                if (IsInsidePolygon(new Vertex(text.Position.X, text.Position.Y), parent))
                    dxf.Entities.Add((EntityObject)text.Clone());
        }

        public static void SaveChildren(List<Polyline2D> polylines, Polyline2D parent, DxfDocument dxf)
        {
            foreach (Polyline2D polyline in polylines)
                if(IsInsidePolygon(polyline, parent))
                    dxf.Entities.Add((EntityObject)polyline.Clone()); 
        }

        public static bool IsInsidePolygon(Polyline2D child, Polyline2D parent)
        {

            foreach (var vertex in child.Vertexes)
                if (!IsInsidePolygon(new Vertex(vertex.Position.X, vertex.Position.Y), parent))
                    return false;
            return true;
        }

        public static bool IsInsidePolygon(Vertex point, Polyline2D polyline)
        {
            List<Vertex> polygon = new List<Vertex>();
            foreach (var vertex in polyline.Vertexes)
                polygon.Add(new Vertex(vertex.Position.X, vertex.Position.Y));

            int n = polygon.Count;
            bool inside = false;
            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                if (((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) &&
                    (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) /
                    (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    inside = !inside;
                }
            }
            return inside;
        }

        public static void SetViewportCenter(string filePath, Polyline2D polyline)
        {
            double x = polyline.Vertexes[0].Position.X;
            double y = polyline.Vertexes[0].Position.Y;

            DxfDocument dxf = DxfDocument.Load(filePath);
            VPort activeVPort = dxf.VPorts["*Active"];
            activeVPort.ViewCenter = new Vector2(x / 2, y / 2);
            activeVPort.ViewHeight = 500;
            activeVPort.ViewCenter = new Vector2(x / 2, y / 2);
            activeVPort.ViewTarget = new Vector3(x / 2, y / 2, 0);
            activeVPort.ViewDirection = new Vector3(0, 0, 1);
            dxf.Save(filePath);
        }
    }
}
