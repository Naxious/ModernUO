using Server;
using Server.Items;
using Server.Mobiles;
using Xunit;

namespace UOContent.Tests;

[Collection("Sequential UOContent Tests")]
public class BandageTests
{
    [Fact]
    public void BeginHeal_DoesNotReplaceAnActiveBandageContext()
    {
        var healer = new PlayerMobile(World.NewMobile);
        healer.DefaultMobileInit();
        healer.RawStr = 100;
        healer.Hits = 1;

        BandageContext first = null;

        try
        {
            first = BandageContext.BeginHeal(healer, healer);

            Assert.NotNull(first);
            Assert.Null(BandageContext.BeginHeal(healer, healer));
            Assert.Same(first, BandageContext.GetContext(healer));
        }
        finally
        {
            first?.StopHeal();
            healer.Delete();
        }
    }
}
