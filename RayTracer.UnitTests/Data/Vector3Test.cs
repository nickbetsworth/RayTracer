using RayTracer.Data;

namespace RayTracer.UnitTests.Data;

public class Vector3Test
{
    [Fact]
    public void TestAdditionOperator()
    {
        var vecA = new Vector3(1, 2, 3);
        var vecB = new Vector3(10, 100, 1000);

        var vecC = vecA + vecB;
        Assert.Equal(new Vector3(11, 102, 1003), vecC);

    }
    
    [Fact]
    public void TestAdditionOperatorCommutative()
    {
        var vecA = new Vector3(1, 2, 3);
        var vecB = new Vector3(10, 100, 1000);

        Assert.Equal(vecA + vecB, vecB + vecA);
    }
    
    [Fact]
    public void TestSubtractionOperator()
    {
        var vecA = new Vector3(1, 2, 3);
        var vecB = new Vector3(11, 102, 1003);

        var vecC = vecB - vecA;
        Assert.Equal(new Vector3(10, 100, 1000), vecC);
    }
    
    [Fact]
    public void TestMultiplicationOperator()
    {
        var vecA = new Vector3(1, 2, 3);
        var vecB = new Vector3(10, 20, 30);

        var vecC = vecB * vecA;
        Assert.Equal(new Vector3(10, 40, 90), vecC);
    }
    
    [Fact]
    public void TestMultiplicationOperatorCommutative()
    {
        var vecA = new Vector3(1, 2, 3);
        var vecB = new Vector3(10, 20, 30);

        Assert.Equal(vecA * vecB, vecB * vecA);
    }

    [Fact]
    public void TestNegationOperator()
    {
        var vec = new Vector3(1, 2, 3);
        Assert.Equal(new Vector3(-1, -2, -3), -vec);
    }

    [Fact]
    public void TestLengthSquared()
    {
        var vec = new Vector3(1, 2, 3);
        Assert.Equal(14, vec.LengthSquared());
    }
    
    [Fact]
    public void TestLength()
    {
        // 6 + 16 + 144 = 169
        // Sqrt(169) = 13
        var vec = new Vector3(3, 4, 12);
        Assert.Equal(13, vec.Length());
    }
    
    [Fact]
    public void TestDotProduct()
    {
        var vecA = new Vector3(1, 2, 3);
        var vecB = new Vector3(3, 2, 1);
        
        Assert.Equal(10, Vector3.Dot(vecA, vecB));
    }

    
    [Fact]
    public void TestCrossProduct()
    {
        var vecA = new Vector3(1, 0, 0);
        var vecB = new Vector3(0, 1, 0);
        
        Assert.Equal(new Vector3(0, 0, 1), Vector3.Cross(vecA, vecB));
        Assert.Equal(new Vector3(0, 0, -1), Vector3.Cross(vecB, vecA));
    }
    
    [Fact]
    public void TestCrossProductParallel()
    {
        var vecA = new Vector3(1, 1, 0);
        var vecB = new Vector3(1, 1, 0);
        
        Assert.Equal(new Vector3(), Vector3.Cross(vecA, vecB));
    }

    [Fact]
    public void TestNormalize()
    {
        Assert.Equal(new Vector3(1, 0, 0), Vector3.Normalize(new Vector3(100, 0, 0)));
        
        Assert.Equal(
            new Vector3(0.5773502691896258, 0.5773502691896258, 0.5773502691896258),
            Vector3.Normalize(new Vector3(100, 100, 100)));
    }

    [Fact]
    public void TestReflect()
    {
        var vec = new Vector3(-1, 0, 0);
        var norm = new Vector3(1, 1, 1);
        Assert.Equal(new Vector3(1, 2, 2), Vector3.Reflect(vec, norm));
    }
}