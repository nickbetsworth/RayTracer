// See https://aka.ms/new-console-template for more information

using RayTracer;

if (args.Length == 0)
{
    throw new ArgumentNullException("args[0]", "Expected to receive output filepath as argument");
}

var outputPath = args[0];

var image = new Image(255, 255);

for (var y = 0; y < image.Height; y++)
{
    for (var x = 0; x < image.Width; x++)
    {
        image.SetPixel(x, y, new Color(50, (byte)x, (byte)y));
    }
}

image.ToPpm(outputPath);