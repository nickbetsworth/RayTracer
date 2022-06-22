namespace RayTracer;

public class Image
{
    public int Width { get; }
    public int Height { get; }
    
    private readonly byte[] _r;
    private readonly byte[] _g;
    private readonly byte[] _b;
    
    public Image(int width, int height)
    {
        Width = width;
        Height = height;
        
        _r = new byte[width * height];
        _g = new byte[width * height];
        _b = new byte[width * height];
    }

    public void SetPixel(int x, int y, Color color)
    {
        if (x < 0 || x >= Width) throw new ArgumentOutOfRangeException("x");
        if (y < 0 || y >= Height) throw new ArgumentOutOfRangeException("y");
        _r[y * Width + x] = color.R;
        _g[y * Width + x] = color.G;
        _b[y * Width + x] = color.B;
    }

    public Color GetPixel(int x, int y)
    {
        return new Color(
            _r[y * Width + x],
            _g[y * Width + x],
            _b[y * Width + x]);
    }
}