using RayTracer.Collision;
using RayTracer.Data;
using RayTracer.Utility;

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
        var refractionRatio = intersection.FrontFace ? (1.0 / RefractiveIndex) : RefractiveIndex;

        var unitDirection = Vector3.Normalize(rayIn.Direction);
        var cosTheta = Math.Min(Vector3.Dot(-unitDirection, intersection.Normal), 1.0);
        var sinTheta = Math.Sqrt(1.0 - cosTheta * cosTheta);
        var cannotRefract = refractionRatio * sinTheta > 1.0;

        var direction = cannotRefract || Reflectance(cosTheta, refractionRatio) > RandomProvider.Random.NextDouble()
            ? Vector3.Reflect(unitDirection, intersection.Normal)
            : Vector3.Refract(unitDirection, intersection.Normal, refractionRatio);
        
        return new ScatterResult(
            new Color(1.0, 1.0, 1.0),
            new Ray(intersection.Point, direction));
    }

    private static double Reflectance(double cosine, double refractionRatio) {
        // Use Schlick's approximation for reflectance.
        var r0 = (1-refractionRatio) / (1+refractionRatio);
        r0 = r0*r0;
        return r0 + (1-r0)*Math.Pow((1 - cosine),5);
    }
}