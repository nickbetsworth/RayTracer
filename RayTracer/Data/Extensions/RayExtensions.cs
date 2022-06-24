namespace RayTracer.Data.Extensions;

using Color = Vector3;

public static class RayExtensions
{
    public static Color ToColor(this Ray ray)
    {
        var normalizedDirection = Vector3.Normalize(ray.Direction);
        var t = 0.5 * (normalizedDirection.Y + 1.0);
        return new Color(1, 1, 1) * (1 - t) + new Color(0.5, 0.7, 1.0) * t;
    }
}