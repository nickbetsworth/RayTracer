using RayTracer.Collision;
using RayTracer.Data;
using RayTracer.Materials;

namespace RayTracer.UnitTests.Fakes;

public class NoopMaterial : Material
{
    public override ScatterResult? Scatter(Ray rayIn, IntersectionResult intersection)
    {
        return null;
    }
}