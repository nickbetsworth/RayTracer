using RayTracer.Collision;
using RayTracer.Data;
using RayTracer.SceneConfiguration;

namespace RayTracer;

using Color = Vector3;
public class Tracer
{
    private readonly Scene _scene;

    public Tracer(Scene scene)
    {
        _scene = scene;
    }

    public Color Trace(Ray ray)
    {
        IntersectionResult? closest = null;
        foreach (var item in _scene.Objects)
        {
            var intersection = item.Intersect(ray, 0, closest?.T ?? double.MaxValue); 
            if (intersection is not null)
            {
                closest = intersection;
            }
        }

        if (closest is null)
        {
            return Default(ray);
        }
        
        // var objectColor = new Color(0.0, 0.25, 0.85);
        // return objectColor * ((Vector3.Dot(Vector3.Normalize(ray.Direction), intersection.Normal) - 1) * -0.5);
        return new Color(
            closest.Normal.X + 1.0,
            closest.Normal.Y + 1.0,
            closest.Normal.Z + 1.0) * 0.5;

    }

    private static Color Default(Ray ray)
    {
        var normalizedDirection = Vector3.Normalize(ray.Direction);
        var t = 0.5 * (normalizedDirection.Y + 1.0);
        return new Color(1, 1, 1) * (1 - t) + new Color(0.5, 0.7, 1.0) * t;
    }
}