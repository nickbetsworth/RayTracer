using RayTracer.Data;

namespace RayTracer.Collision;

using Point3 = Vector3;

public record IntersectionResult(IIntersectable Object, Point3 Point, Vector3 Normal, double T, bool FrontFace = true);