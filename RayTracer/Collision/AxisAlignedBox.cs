using RayTracer.Data;
using RayTracer.Materials;

namespace RayTracer.Collision;

using Point3 = Vector3;

public class AxisAlignedBox : IIntersectable
{
    public Point3 Min { get; }
    public Point3 Max { get; }
    public Material Material { get; }
    
    public AxisAlignedBox(Vector3 min, Vector3 max, Material material)
    {
        Min = min;
        Max = max;
        Material = material;
    }

    public IntersectionResult? Intersect(Ray ray, double tMin, double tMax)
    {
        var localTMin = (Min.X - ray.Origin.X) / ray.Direction.X;
        var localTMax = (Max.X - ray.Origin.X) / ray.Direction.X;
        var normalMin = new Vector3(-1, 0, 0);
        var normalMax = new Vector3(1, 0, 0);
        
        if (localTMin > localTMax) 
            (localTMin, localTMax, normalMin, normalMax) = (localTMax, localTMin, normalMax, normalMin);

        var tYMin = (Min.Y - ray.Origin.Y) / ray.Direction.Y;
        var tYMax = (Max.Y - ray.Origin.Y) / ray.Direction.Y;
        var tYNormalMin = new Vector3(0, -1, 0);
        var tYNormalMax = new Vector3(0, 1, 0);
        
        if (tYMin > tYMax) 
            (tYMin, tYMax, tYNormalMin, tYNormalMax) = (tYMax, tYMin, tYNormalMax, tYNormalMin);

        if (localTMin > tYMax || localTMax < tYMin) 
            return null;

        if (tYMin > localTMin)
        {
            localTMin = tYMin;
            normalMin = tYNormalMin;
        }

        if (tYMax < localTMax)
        {
            localTMax = tYMax;
            normalMax = tYNormalMax;
        } 
        
        var tZMin = (Min.Z - ray.Origin.Z) / ray.Direction.Z;
        var tZMax = (Max.Z - ray.Origin.Z) / ray.Direction.Z;
        var tZNormalMin = new Vector3(0, 0, -1);
        var tZNormalMax = new Vector3(0, 0, 1);
        
        if (tZMin > tZMax)
            (tZMin, tZMax, tZNormalMin, tZNormalMax) = (tZMax, tZMin, tZNormalMax, tZNormalMin);

        if (tZMin > localTMax || tZMax < localTMin)
            return null;

        if (tZMin > localTMin)
        {
            localTMin = tZMin;
            normalMin = tZNormalMin;
        }

        if (tZMax < localTMax)
        {
            localTMax = tZMax;
            normalMax = tZNormalMax;
        }

        if (tMax < localTMin || tMin > localTMax || (tMin > localTMin && tMax < localTMax))
            return null;

        // We intersect with the outside of the box
        if (tMin < localTMin)
        {
            return new IntersectionResult(
                this, ray.At(localTMin), normalMin, localTMin, true);
        }

        // The ray must originate inside the box and intersect with an internal face, reverse any normals
        return new IntersectionResult(
            this, ray.At(localTMax), -normalMax, localTMax, false);
    }
    
}