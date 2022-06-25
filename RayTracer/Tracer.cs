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
        // var objectColor = new Color(0.0, 0.25, 0.85);
        foreach (var item in _scene.Objects)
        {
            var intersection = item.Intersect(ray, 0, 0); 
            if (intersection is not null)
            {
                // return objectColor * ((Vector3.Dot(Vector3.Normalize(ray.Direction), intersection.Normal) - 1) * -0.5);
                return new Color(
                    intersection.Normal.X + 1.0,
                    intersection.Normal.Y + 1.0,
                    intersection.Normal.Z + 1.0) * 0.5;
            }
        }

        return Default(ray);
    }

    private static Color Default(Ray ray)
    {
        var normalizedDirection = Vector3.Normalize(ray.Direction);
        var t = 0.5 * (normalizedDirection.Y + 1.0);
        return new Color(1, 1, 1) * (1 - t) + new Color(0.5, 0.7, 1.0) * t;
    }
}