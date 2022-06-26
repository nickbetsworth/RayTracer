using RayTracer.Collision;
using RayTracer.Data;

namespace RayTracer.Materials;

using Color = Vector3;

public class MetalMaterial : Material
{
    public Color Albedo { get; set; }
    
    public MetalMaterial(Vector3 albedo)
    {
        Albedo = albedo;
    }
    
    public override ScatterResult? Scatter(Ray rayIn, IntersectionResult intersection)
    {
        var reflectionDir = Vector3.Reflect(rayIn.Direction, intersection.Normal);
        if (Vector3.Dot(reflectionDir, intersection.Normal) <= 0)
        {
            return null;
        }
        
        var ray = new Ray(intersection.Point, reflectionDir);
        return new ScatterResult(Albedo, ray);
    }
}