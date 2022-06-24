namespace RayTracer.Data;

using Color = Vector3;

public class Image
{
    public int Width { get; }
    public int Height { get; }
    
    private readonly Color[] _data;
    
    public Image(int width, int height)
    {
        Width = width;
        Height = height;
        
        _data = new Color[width * height];
    }

    public void SetPixel(int x, int y, Color color)
    {
        if (x < 0 || x >= Width) throw new ArgumentOutOfRangeException("x");
        if (y < 0 || y >= Height) throw new ArgumentOutOfRangeException("y");
        
        _data[y * Width + x] = color;
    }

    public Color GetPixel(int x, int y)
    {
        return _data[y * Width + x];
    }
}