using RayTracer.Collision;
using RayTracer.Data;

namespace RayTracer.Materials;

public abstract class Material
{
    public abstract ScatterResult? Scatter(Ray rayIn, IntersectionResult intersection);
}