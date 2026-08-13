using System.IO;
using Server;
using Server.Misc;
using Xunit;

namespace UOContent.Tests;

[Collection("Sequential UOContent Tests")]
public class CrashGuardTests
{
    [Fact]
    public void CrashReportsAreStoredUnderWritableLogsDirectory()
    {
        Assert.Equal(
            Path.Combine(Core.BaseDirectory, "Logs", "Crashes"),
            CrashGuard.CrashReportDirectory
        );
    }
}
