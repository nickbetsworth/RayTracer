using RayTracer.Data;

namespace RayTracer.Configuration;

using Point3 = Vector3;

public class Camera
{
    public Camera(double verticalFovDeg, double aspectRatio)
    {
        AspectRatio = aspectRatio;
        
        var theta = DegreesToRadians(verticalFovDeg);
        var h = Math.Tan(theta / 2.0);
        ViewportHeight = 2.0 * h;
    }

    public double AspectRatio { get; }
    public double ViewportHeight { get; }
    public double ViewportWidth => AspectRatio * ViewportHeight;
    public double FocalLength { get; set; } = 1.0;

    public Point3 Origin { get; set; } = new(0, 0, 0);

    public Vector3 Horizontal => new(ViewportWidth, 0, 0);
    public Vector3 Vertical => new(0, ViewportHeight, 0);
    public Point3 LowerLeftCorner => Origin - (Horizontal / 2) - (Vertical / 2) - new Vector3(0, 0, FocalLength);

    private static double DegreesToRadians(double deg)
    {
        return deg * (Math.PI / 180);
    }
}