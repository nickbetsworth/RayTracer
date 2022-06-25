using RayTracer.Data;

namespace RayTracer.Collision;

using Point3 = Vector3;

public class Sphere : IIntersectable
{
    public Point3 Origin { get; set; }
    public double Radius { get; set; }

    public Sphere(Vector3 origin, double radius)
    {
        Origin = origin;
        Radius = radius;
    }

    public IntersectionResult? Intersect(Ray ray, double tMin, double tMax)
    {
        var sphereToRay = ray.Origin - Origin;
        var a = Vector3.Dot(ray.Direction, ray.Direction);
        var b = 2.0 * Vector3.Dot(sphereToRay, ray.Direction);
        var c = Vector3.Dot(sphereToRay, sphereToRay) - Radius * Radius;

        // Todo: ignored simplification in #6.2. Left as is to maintain readability
        var discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
        {
            return null;
        }

        var sqrt = Math.Sqrt(discriminant);
        var t = (-b - sqrt) / (2.0 * a);

        if (t < tMin || t > tMax)
        {
            t = (-b + sqrt) / (2.0 * a);
            if (t < tMin || t > tMax)
            {
                return null;
            }
        }

        var intersectionPoint = ray.At(t);
        var norm = (intersectionPoint - Origin) / Radius;
        var frontFaceIntersection = Vector3.Dot(ray.Direction, norm) < 0.0;
        
        return new IntersectionResult(intersectionPoint, frontFaceIntersection ? norm : -norm, t, frontFaceIntersection);
    }
}