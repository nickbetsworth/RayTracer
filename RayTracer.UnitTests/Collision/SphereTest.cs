using RayTracer.Collision;
using RayTracer.Data;
using RayTracer.UnitTests.Fakes;

namespace RayTracer.UnitTests.Collision;

using Point3 = Vector3;

public class SphereTest
{
    private readonly Sphere _sphere;

    public SphereTest()
    {
        _sphere = new Sphere(new Point3(0, 0, 0), 1, new NoopMaterial());
    }

    /*
     * @------------------>
     *        =  =     
     *     =        =  
     *    =          = 
     *    =          = 
     *     =        =  
     *        =  = 
     */
    [Fact]
    public void RayMissesSphere()
    {
        var ray = new Ray(new Point3(-5, 2, 0), new Vector3(1, 0, 0));
        Assert.Null(_sphere.Intersect(ray, 0, double.MaxValue));
    }

    /*
    *        =  =     
    *     =        =  
    *    =          =  @--->
    *    =          = 
    *     =        =  
    *        =  = 
    */
    [Fact]
    public void IntersectionBeforeTMin()
    {
        var ray = new Ray(new Point3(2, 0, 0), new Vector3(1, 0, 0));
        Assert.Null(_sphere.Intersect(ray, 0, double.MaxValue));
    }
    
    /*
     *            =  =     
     *         =        =  
     * @--->  =          = 
     *        =          = 
     *         =        =  
     *            =  = 
     */
    [Fact]
    public void IntersectionAfterTMax()
    {
        var ray = new Ray(new Point3(-2.5, 0, 0), new Vector3(1, 0, 0));
        Assert.Null(_sphere.Intersect(ray, 0, 1));
    }

    /*
     *            =  =     
     *         =        =  
     *        =   @--->  = 
     *        =          = 
     *         =        =  
     *            =  = 
     */
    [Fact]
    public void SphereEncompassesRay()
    {
        var ray = new Ray(new Point3(-0.5, 0, 0), new Vector3(1, 0, 0));
        Assert.Null(_sphere.Intersect(ray, 0, 1));
    }

    /*
     *        =  =     
     *     =        =  
     *    =     @----=-->
     *    =          = 
     *     =        =  
     *        =  = 
     */
    [Fact]
    public void Intersection_A()
    {
        var ray = new Ray(new Point3(0, 0, 0), new Vector3(1, 0, 0));
        var intersection = _sphere.Intersect(ray, 0, 2);
        
        Assert.NotNull(intersection);
        Assert.Equal(1, intersection?.T);
        Assert.Equal(new Point3(1, 0, 0), intersection?.Point);
        Assert.Equal(new Vector3(-1, 0, 0), intersection?.Normal);
        Assert.False(intersection?.FrontFace);
    }
    
    /*
     *        =  =     
     *     =        =  
     * @--=----->    =
     *    =          = 
     *     =        =  
     *        =  = 
     */
    [Fact]
    public void Intersection_B()
    {
        var ray = new Ray(new Point3(-2, 0, 0), new Vector3(1, 0, 0));
        var intersection = _sphere.Intersect(ray, 0, 2);
        
        Assert.NotNull(intersection);
        Assert.Equal(1, intersection?.T);
        Assert.Equal(new Point3(-1, 0, 0), intersection?.Point);
        Assert.Equal(new Vector3(-1, 0, 0), intersection?.Normal);
        Assert.True(intersection?.FrontFace);
    }

    /*
     *        =  =     
     *     =        =  
     * @--=----------=-->
     *    =          =
     *     =        =  
     *        =  = 
     */
    [Fact]
    public void Intersection_C()
    {
        var ray = new Ray(new Point3(-2, 0, 0), new Vector3(1, 0, 0));
        var intersection = _sphere.Intersect(ray, 0, double.MaxValue);
        
        Assert.NotNull(intersection);
        Assert.Equal(1, intersection?.T);
        Assert.Equal(new Point3(-1, 0, 0), intersection?.Point);
        Assert.Equal(new Vector3(-1, 0, 0), intersection?.Normal);
        Assert.True(intersection?.FrontFace);
    }
}