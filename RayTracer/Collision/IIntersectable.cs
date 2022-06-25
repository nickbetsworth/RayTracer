using RayTracer.Data;

namespace RayTracer.Collision;

public interface IIntersectable
{
    public IntersectionResult? Intersect(Ray ray, double tMin, double tMax);
}