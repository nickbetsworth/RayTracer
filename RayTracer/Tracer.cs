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
        foreach (var item in _scene.Objects)
        {
            if (item.Intersect(ray, 0, 0) is not null)
            {
                return new Color(0, 1.0, 0);
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