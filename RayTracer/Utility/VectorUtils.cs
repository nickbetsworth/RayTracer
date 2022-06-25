using RayTracer.Data;

namespace RayTracer.Utility;

using Point3 = Vector3;
public static class VectorUtils
{
    public static Point3 RandomPointInUnitSphere()
    {
        const int maxAttempts = 100;

        for (var i = 0; i < maxAttempts; i++)
        {
            var pt = new Point3(
                RandomProvider.Random.NextDouble(),
                RandomProvider.Random.NextDouble(),
                RandomProvider.Random.NextDouble());

            if (pt.LengthSquared() < 1.0)
            {
                return pt;
            }
        }

        return new Point3(
            RandomProvider.Random.NextDouble(),
            RandomProvider.Random.NextDouble(),
            RandomProvider.Random.NextDouble());
    }
}