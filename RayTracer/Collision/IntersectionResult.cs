using RayTracer.Data;

namespace RayTracer.Collision;

using Point3 = Vector3;

public class IntersectionResult
{
    public Point3 Point { get; set; }
    public Vector3 Normal { get; set; }
    public double T { get; set; }
    
    public IntersectionResult(Vector3 point, Vector3 normal, double t)
    {
        Point = point;
        Normal = normal;
        T = t;
    }
}