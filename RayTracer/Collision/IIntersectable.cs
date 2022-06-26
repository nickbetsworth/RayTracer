using RayTracer.Data;
using RayTracer.Materials;

namespace RayTracer.Collision;

public interface IIntersectable
{
    public Material Material { get; }
    public IntersectionResult? Intersect(Ray ray, double tMin, double tMax);
}