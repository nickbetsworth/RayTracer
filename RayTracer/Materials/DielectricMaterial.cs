using RayTracer.Collision;
using RayTracer.Data;

namespace RayTracer.Materials;

using Color = Vector3;

class DielectricMaterial : Material
{
    public double RefractiveIndex { get; }
    
    public DielectricMaterial(double refractiveIndex)
    {
        RefractiveIndex = refractiveIndex;
    }

    public override ScatterResult? Scatter(Ray rayIn, IntersectionResult intersection)
    {
        var refractiveIndex = intersection.FrontFace ? (1.0 / RefractiveIndex) : RefractiveIndex;
        var refracted = Refract(
            Vector3.Normalize(rayIn.Direction),
            intersection.Normal,
            refractiveIndex);
        
        return new ScatterResult(
            new Color(1.0, 1.0, 1.0),
            new Ray(intersection.Point, refracted));
    }

    private static Vector3 Refract(Vector3 uv, Vector3 n, double refractiveIndex)
    {
        var cosTheta = Math.Min(Vector3.Dot(-uv, n), 1.0);
        var rayOutPerp = (uv + n * cosTheta) * refractiveIndex;
        var rayOutParallel = n * -Math.Sqrt(Math.Abs(1.0 - rayOutPerp.LengthSquared()));
        
        return rayOutPerp + rayOutParallel;
    }
}