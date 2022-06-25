using RayTracer.Data;

namespace RayTracer.SceneConfiguration;

using Point3 = Vector3;

public class CameraConfiguration
{
    public double AspectRatio { get; set; } = 16.0 / 9.0;
    public double ViewportHeight { get; set; } = 2.0;
    public double ViewportWidth => AspectRatio * ViewportHeight;
    public double FocalLength { get; set; } = 1.0;

    public Point3 Origin { get; set; } = new (0, 0, 0);
    
    public Vector3 Horizontal => new(ViewportWidth, 0, 0);
    public Vector3 Vertical => new(0, ViewportHeight, 0);
    public Point3 LowerLeftCorner => Origin - (Horizontal / 2) - (Vertical / 2) - new Vector3(0, 0, FocalLength);
}