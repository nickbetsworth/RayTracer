using RayTracer.Collision;
using RayTracer.Data;
using RayTracer.Materials;

namespace RayTracer.Configuration.PrebuiltScenes.Scenes;

using Point3 = Vector3;
using Color = Vector3;
public class PrebuiltSceneA : PrebuiltScene
{
    public PrebuiltSceneA()
    {
        var materialMetal = new MetalMaterial(new Color(0.5, 0.5, 0.5));
        var materialGlass = new DielectricMaterial(1.5);
        var materialGround = new LambertianMaterial(new Color(0.1, 0.1, 0.1));
        var materialDielectricDiamond = new DielectricMaterial(2.4);
        
        Add(new Sphere(new Point3(1.0, 0.0, -1.0), 0.5, materialMetal));
        Add(new Sphere(new Point3(-1.0, 0.0, -1.0), -0.45, materialGlass));
        Add(new Sphere(new Point3(-1.0, 0.0, -1.0), 0.5, materialGlass));
        Add(new AxisAlignedBox(new Point3(-0.25, -0.5, -1.25), new Point3(0.25, 0.5,  -0.75), materialDielectricDiamond));
        Add(new Sphere(new Point3(0.0, -100.5, -1.0), 100, materialGround));
    }

    public override Camera GetSuggestedCamera()
    {
        return new Camera(
            new Point3(-3, 0.75, 1),
            new Point3(-0.25, 0.25, -1),
            new Vector3(0, 1, 0),
            35,
            16.0 / 9.0);
    }
}