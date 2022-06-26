using RayTracer.Data;

namespace RayTracer.Materials;

using Color = Vector3;

public record ScatterResult(Color Attenuation, Ray Ray);