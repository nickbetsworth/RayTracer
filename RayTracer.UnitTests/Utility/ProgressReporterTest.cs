using RayTracer.Utility;

namespace RayTracer.UnitTests.Utility;

public class ProgressReporterTest
{
    [Fact]
    public void CanReportProgressOnAllUpdates()
    {
        var writer = new StringWriter();
        var reporter = new ProgressReporter(writer, 1, 3, int.MaxValue);
        
        reporter.Update(1);
        reporter.Update(2);
        reporter.Update(3);
        
        Assert.Equal(3, GetUpdateCount(writer));
    }

    [Fact]
    public void CanReportProgressOnSubsetOfUpdates()
    {
        var writer = new StringWriter();
        var reporter = new ProgressReporter(writer, 1, 10, 2);
        
        reporter.Update(1);
        Assert.Equal(0, GetUpdateCount(writer));
        
        reporter.Update(4);
        Assert.Equal(0, GetUpdateCount(writer));

        reporter.Update(5);
        Assert.Equal(1, GetUpdateCount(writer));
        
        reporter.Update(10);
        Assert.Equal(2, GetUpdateCount(writer));
    }

    [Fact]
    public void ReportsProgressCorrectly()
    {
        var writer = new StringWriter();
        var reporter = new ProgressReporter(writer, 1, 3, int.MaxValue);
        
        reporter.Update(1);
        Assert.Contains("0%", GetReportByIndex(writer, 0));
        reporter.Update(2);
        Assert.Contains("50%", GetReportByIndex(writer, 1));
        reporter.Update(3);
        Assert.Contains("100%", GetReportByIndex(writer, 2));
    }
    
    private static int GetUpdateCount(StringWriter writer)
    {
        return writer.ToString().Count(c => c == '\n');
    }

    private static string GetReportByIndex(StringWriter writer, int reportNo)
    {
        return writer.ToString().Split(new [] { "\r\n", "\n" }, StringSplitOptions.None)[reportNo];
    }
}