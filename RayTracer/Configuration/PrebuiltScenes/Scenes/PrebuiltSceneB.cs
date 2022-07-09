using RayTracer.Collision;
using RayTracer.Data;
using RayTracer.Materials;

namespace RayTracer.Configuration.PrebuiltScenes.Scenes;

using Point3 = Vector3;
using Color = Vector3;

public class PrebuiltSceneB : PrebuiltScene
{
    public PrebuiltSceneB()
    {
        Add(new Sphere(new Point3(0.0, -1000, 0), 1000, new LambertianMaterial(new Color(0.1, 0.1, 0.1))));
        Add(new AxisAlignedBox(new Point3(-2, 0, -2), new Point3(2, 4,  2), new MetalMaterial(new Color(0.7, 0.7, 0.7), 0.15)));

        var random = new Random(22);
        // Create n random spheres
        for (var i = -10; i < 10; i+=2)
        {
            for (var j = -10; j < 10; j+=2)
            {
                // Don't render any spheres near the box in the centre of the scene
                if (i is > -3 and < 4 && j is > -3 and < 4) continue;
                
                var radius = 0.5;
                var origin = new Point3(
                    i + random.NextDouble() - 0.5,
                    radius,
                    j + random.NextDouble() - 0.5);

                var chooseMaterial = random.NextDouble();
                Material material = chooseMaterial switch
                {
                    < 0.8 => new LambertianMaterial(GetRandomDiffuseColour(random) * GetRandomDiffuseColour(random) * GetRandomDiffuseColour(random)),
                    < 0.95 => new MetalMaterial(GetRandomMetalColour(random)),
                    _ => new DielectricMaterial(random.NextDouble() + 1.5)
                };

                Add(new Sphere(origin, radius, material));
            }
        }
    }

    public override Camera GetSuggestedCamera()
    {
        return new Camera(
            new Point3(8, 3, 5),
            new Point3(0, -1, 0),
            new Vector3(0, 1, 0),
            45,
            16.0 / 9.0);
    }

    private static Color GetRandomDiffuseColour(Random random)
    {
        return new Color(
            random.NextDouble(),
            random.NextDouble(),
            random.NextDouble());
    }
    
    private static Color GetRandomMetalColour(Random random)
    {
        var shade = 0.5 + random.NextDouble() * 0.5;
        return new Color(shade, shade, shade);
    }
}