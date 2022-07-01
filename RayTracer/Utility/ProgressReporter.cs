namespace RayTracer.Utility;

/// <summary>
/// A CLI progress reporter which conditionally reports up to N progress updates
/// </summary>
/// <remarks>
/// This class provides a CLI reporter which prints a progress bar and progress percentage to console.
/// 
/// The reporter will only print at most {maxReports} updates to console.
/// 
/// Progress updates will be evenly spaced from the provided {start} -> {end}, assuming updates are provided
/// in single units.
///
/// It is possible to print all progress reports to console, with no limit on intervals, by setting {maxReports} to
/// {int.MaxValue}
/// </remarks>
public class ProgressReporter
{
    private const int ProgressBarLength = 40;

    private readonly TextWriter _out;
    private readonly int _start;
    private readonly int _end;
    private readonly int _interval;
    private int _nextReport;

    public ProgressReporter(TextWriter @out, int start, int end, int maxReports)
    {
        _out = @out;
        _start = start;
        _end = end;
        _interval = CalculateReportInterval(start, end, maxReports);
        _nextReport = _start + _interval;
    }
    
    public void Update(int value)
    {
        if (!ShouldReport(value))
        {
            return;
        }
        
        var percentage = (double) (value - _start) / (_end - _start);
        ReportProgress(percentage);
        _nextReport += _interval;
    }

    private bool ShouldReport(int value)
    {
        return value >= _nextReport;
    }

    private void ReportProgress(double percentage)
    {
        var numCharsComplete = (int)(percentage * ProgressBarLength);
        
        _out.WriteLine(
            $"[{new string('=', numCharsComplete)}{new string('-', ProgressBarLength-numCharsComplete)}] " + 
            $"{(int)(percentage * 100)}%");
    }

    private static int CalculateReportInterval(int start, int end, int maxReports)
    {
        return (end - start) / maxReports;
    }
}