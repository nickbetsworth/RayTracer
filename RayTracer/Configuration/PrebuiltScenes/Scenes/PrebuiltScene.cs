namespace RayTracer.Configuration.PrebuiltScenes.Scenes;

public abstract class PrebuiltScene : Scene
{
    public abstract Camera GetSuggestedCamera();
}