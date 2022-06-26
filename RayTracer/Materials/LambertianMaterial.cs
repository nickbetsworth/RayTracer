using RayTracer.Collision;
using RayTracer.Data;
using RayTracer.Utility;

namespace RayTracer.Materials;

using Color = Vector3;

public class LambertianMaterial : Material
{
    public Color Albedo { get; }
    
    public LambertianMaterial(Vector3 albedo)
    {
        Albedo = albedo;
    }

    public override ScatterResult? Scatter(Ray ray, IntersectionResult intersection)
    {
        var diffuseDirection = intersection.Normal + VectorUtils.RandomUnitVector();

        // Prevent subtle bug where intersection normal and random vector are exactly opposite
        if (Vector3.NearZero(diffuseDirection))
        {
            diffuseDirection = intersection.Normal;
        }
        
        return new ScatterResult(Albedo, new Ray(intersection.Point, diffuseDirection));
    }
}