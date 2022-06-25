using RayTracer.Collision;

namespace RayTracer.Configuration;

public class Scene
{
    public IList<IIntersectable> Objects { get; }

    public Scene()
    {
        Objects = new List<IIntersectable>();
    }

    public Scene(IList<IIntersectable> objects)
    {
        Objects = objects;
    }
    
    public void Add(IIntersectable item)
    {
        Objects.Add(item);
    }
}