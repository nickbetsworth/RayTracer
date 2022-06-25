using RayTracer.Data;

namespace RayTracer.Utility;

using Point3 = Vector3;
public static class VectorUtils
{
    public static Vector3 RandomUnitVector()
    {
        return Vector3.Normalize(new Vector3(
            RandomProvider.Random.NextDouble(),
            RandomProvider.Random.NextDouble(),
            RandomProvider.Random.NextDouble()));
    }
}