using RayTracer.Collision;
using RayTracer.Data;
using RayTracer.Utility;

namespace RayTracer.Materials;

using Color = Vector3;

public class MetalMaterial : Material
{
    public Color Albedo { get; }
    public double Fuzz { get; }
    
    public MetalMaterial(Vector3 albedo, double fuzz = 0.0)
    {
        Albedo = albedo;
        Fuzz = fuzz < 1 ? fuzz : 1; // Bound the fuzz to unit length
    }
    
    public override ScatterResult? Scatter(Ray rayIn, IntersectionResult intersection)
    {
        var reflectionDir = Vector3.Reflect(Vector3.Normalize(rayIn.Direction), intersection.Normal) + VectorUtils.RandomUnitVector() * Fuzz;
        if (Vector3.Dot(reflectionDir, intersection.Normal) <= 0)
        {
            return null;
        }
        
        var ray = new Ray(intersection.Point, reflectionDir);
        return new ScatterResult(Albedo, ray);
    }
}