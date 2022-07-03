using RayTracer.Data;

namespace RayTracer.Configuration;

using Point3 = Vector3;

public class Camera
{
    public Camera(Point3 lookFrom, Point3 lookAt, Vector3 vup, double verticalFovDeg, double aspectRatio)
    {
        AspectRatio = aspectRatio;
        Origin = lookFrom;
        
        var theta = DegreesToRadians(verticalFovDeg);
        var h = Math.Tan(theta / 2.0);
        ViewportHeight = 2.0 * h;
        
        var w = Vector3.Normalize(lookFrom - lookAt);
        var u = Vector3.Normalize(Vector3.Cross(vup, w));
        var v = Vector3.Cross(w, u);

        Horizontal = u * ViewportWidth;
        Vertical = v * ViewportHeight;
        LowerLeftCorner = Origin - (Horizontal / 2) - (Vertical / 2) - w;
    }

    public double AspectRatio { get; }
    public double ViewportHeight { get; }
    public double ViewportWidth => AspectRatio * ViewportHeight;

    public Point3 Origin { get; }

    public Vector3 Horizontal { get; }
    public Vector3 Vertical { get; }
    public Point3 LowerLeftCorner { get; }

    private static double DegreesToRadians(double deg)
    {
        return deg * (Math.PI / 180);
    }
}