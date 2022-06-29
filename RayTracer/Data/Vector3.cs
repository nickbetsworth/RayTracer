using System.Runtime.CompilerServices;

namespace RayTracer.Data;

public class Vector3
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Vector3()
    {
    }

    public Vector3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Length() => Math.Sqrt(LengthSquared());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double LengthSquared() => Dot(this, this);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 operator *(Vector3 a, double t)
    {
        return new Vector3(a.X * t, a.Y * t, a.Z * t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 operator *(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 operator /(Vector3 a, double t)
    {
        return a * (1 / t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 operator -(Vector3 vec)
    {
        return new Vector3(-vec.X, -vec.Y, -vec.Z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 operator -(Vector3 a, Vector3 b)
    {
        return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Normalize(Vector3 vec)
    {
        return vec / vec.Length();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Dot(Vector3 a, Vector3 b)
    {
        return a.X * b.X +
               a.Y * b.Y +
               a.Z * b.Z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Cross(Vector3 a, Vector3 b)
    {
        return new Vector3(
            a.Y * b.Z - a.Z * b.Y,
            a.Z * b.X - a.X * b.Z,
            a.X * b.Y - a.Y * b.X);
    }

    public static Vector3 Reflect(Vector3 vec, Vector3 norm)
    {
        return vec - norm * 2.0 * Dot(vec, norm);
    }
    
    public static Vector3 Refract(Vector3 uv, Vector3 n, double refractiveIndex)
    {
        var cosTheta = Math.Min(Vector3.Dot(-uv, n), 1.0);
        var rayOutPerp = (uv + n * cosTheta) * refractiveIndex;
        var rayOutParallel = n * -Math.Sqrt(Math.Abs(1.0 - rayOutPerp.LengthSquared()));
        
        return rayOutPerp + rayOutParallel;
    }
    
    public static bool NearZero(Vector3 vec)
    {
        const double eps = 1e-8;
        return Math.Abs(vec.X) < eps && Math.Abs(vec.Y) < eps && Math.Abs(vec.Z) < eps;
    }
    
    public override string ToString()
    {
        return $"({X} {Y} {Z})";
    }
}