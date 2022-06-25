﻿using RayTracer.Data;

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

        var discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
        {
            return null;
        }

        var t = (-b - Math.Sqrt(discriminant)) / (2.0 * a);

        // Todo: early return?
        if (t < tMin || t > tMax)
        {
            
        }

        var intersectionPoint = ray.At(t);
        var norm = Vector3.Normalize(intersectionPoint - Origin);
        
        return new IntersectionResult(intersectionPoint, norm, t);
    }
}