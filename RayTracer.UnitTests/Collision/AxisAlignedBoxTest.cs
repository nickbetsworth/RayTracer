using RayTracer.Collision;
using RayTracer.Data;
using RayTracer.Materials;
using RayTracer.UnitTests.Fakes;

namespace RayTracer.UnitTests.Collision;

using Point3 = Vector3;

public class AxisAlignedBoxTest
{
    private readonly AxisAlignedBox _box;
    
    public AxisAlignedBoxTest()
    {
        _box = new AxisAlignedBox(
            new Vector3(0, 0, 0),
            new Vector3(2, 2, 2),
            new NoopMaterial());
    }
    
    [Fact]
    
    /*
     * @--->
     * 
     * |  |
     * |  |  
     * |  |
     */
    public void RayMissesBox()
    {
        var ray = new Ray(new Point3(-50, 0, 0), new Vector3(0, 1, 0));
        Assert.Null(_box.Intersect(ray, 0, double.MaxValue));
    }

    /*
     * |  |
     * |  |  @--->
     * |  |
     */
    [Fact]
    public void IntersectionBeforeTMin()
    {
        var ray = new Ray(new Point3(3, 1, 1), new Vector3(1, 0, 0));
        Assert.Null(_box.Intersect(ray, 0, double.MaxValue));
        
        ray = new Ray(new Point3(-1, 1, 1), new Vector3(-1, 0, 0));
        Assert.Null(_box.Intersect(ray, 0, double.MaxValue));
    }

    /*
     *        |  |
     * @--->  |  |  
     *        |  |
     */
    [Fact]
    public void IntersectionAfterTMax()
    {
        var ray = new Ray(new Point3(-2, 1, 1), new Vector3(1, 0, 0));
        Assert.Null(_box.Intersect(ray, 0, 1));
    }
    
    /*
     * |       |
     * | @---> |  
     * |       |
     */
    [Fact]
    public void BoxEncompassesEntireConstrainedRay()
    {
        var ray = new Ray(new Point3(1, 1, 1), new Vector3(0.5, 0.5, 0.5));
        Assert.Null(_box.Intersect(ray, 0, 1));
    }

    /*
     *    |    |
     * @--|->  |  
     *    |    |
     */
    [Fact]
    public void Intersection_A1()
    {
        var ray = new Ray(new Point3(-1, 1, 1), new Vector3(2, 0, 0));
        var intersection = _box.Intersect(ray, 0, 1);
        
        Assert.NotNull(intersection);
        Assert.Equal(0.5, intersection?.T);
        Assert.Equal(new Point3(0, 1, 1), intersection?.Point);
        Assert.Equal(new Vector3(-1, 0, 0), intersection?.Normal);
        Assert.True(intersection?.FrontFace);
    }
    
    /*
     * |    |
     * |  <-|--@ 
     * |    |
     */
    [Fact]
    public void Intersection_A2()
    {
        var ray = new Ray(new Point3(3, 1, 1), new Vector3(-2, 0, 0));
        var intersection = _box.Intersect(ray, 0, 1);
        
        Assert.NotNull(intersection);
        Assert.Equal(0.5, intersection?.T);
        Assert.Equal(new Point3(2, 1, 1), intersection?.Point);
        Assert.Equal(new Vector3(1, 0, 0), intersection?.Normal);
        Assert.True(intersection?.FrontFace);
    }
    
    /*
     * |     |
     * |  @--|->  
     * |     |
     */
    [Fact]
    public void Intersection_B1()
    {
        var ray = new Ray(new Point3(1, 1, 1), new Vector3(0, 2, 0));
        var intersection = _box.Intersect(ray, 0, 1);
        
        Assert.NotNull(intersection);
        Assert.Equal(0.5, intersection?.T);
        Assert.Equal(new Point3(1, 2, 1), intersection?.Point);
        Assert.Equal(new Vector3(0, -1, 0), intersection?.Normal);
        Assert.False(intersection?.FrontFace);
    }
    
    /*
     *   |     |
     * <-|--@  |  
     *   |     |
     */
    [Fact]
    public void Intersection_B2()
    {
        var ray = new Ray(new Point3(1, 1, 1), new Vector3(0, 2, 0));
        var intersection = _box.Intersect(ray, 0, 1);
        
        Assert.NotNull(intersection);
        Assert.Equal(0.5, intersection?.T);
        Assert.Equal(new Point3(1, 2, 1), intersection?.Point);
        Assert.Equal(new Vector3(0, -1, 0), intersection?.Normal);
        Assert.False(intersection?.FrontFace);
    }
    
    /*
     *    |    |
     * @--|----|-->  
     *    |    |
     */
    [Fact]
    public void Intersection_C1()
    {
        var ray = new Ray(new Point3(1, 1, -1), new Vector3(0, 0, 1));
        var intersection = _box.Intersect(ray, 0, double.MaxValue);
        
        Assert.NotNull(intersection);
        Assert.Equal(1, intersection?.T);
        Assert.Equal(new Point3(1, 1, 0), intersection?.Point);
        Assert.Equal(new Vector3(0, 0, -1), intersection?.Normal);
        Assert.True(intersection?.FrontFace);
    }
    
    /*
     *    |    |
     * <--|----|--@  
     *    |    |
     */
    [Fact]
    public void Intersection_C2()
    {
        var ray = new Ray(new Point3(1, 1, 3), new Vector3(0, 0, -1));
        var intersection = _box.Intersect(ray, 0, double.MaxValue);
        
        Assert.NotNull(intersection);
        Assert.Equal(1, intersection?.T);
        Assert.Equal(new Point3(1, 1, 2), intersection?.Point);
        Assert.Equal(new Vector3(0, 0, 1), intersection?.Normal);
        Assert.True(intersection?.FrontFace);
    }
}