using System.Runtime.CompilerServices;

namespace RayTracer.Data;

using Point3 = Vector3;

public class Ray
{
    public Point3 Origin { get; set; }
    public Vector3 Direction { get; set; }

    public Ray(Vector3 origin, Vector3 direction)
    {
        Origin = origin;
        Direction = direction;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Point3 At(double t)
    {
        return Origin + Direction * t;
    }
}