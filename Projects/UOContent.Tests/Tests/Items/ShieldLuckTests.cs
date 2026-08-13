using Server;
using Server.Items;
using Server.Mobiles;
using Xunit;

namespace UOContent.Tests;

[Collection("Sequential UOContent Tests")]
public class ShieldLuckTests
{
    [Fact]
    public void ResettingLuckOnAnotherPlayersEquippedShieldIsSafe()
    {
        var player = new PlayerMobile(World.NewMobile);
        player.DefaultMobileInit();
        var shield = new MetalShield();

        try
        {
            player.AddItem(shield);
            shield.Attributes.Luck = 1000;

            Assert.Equal(1000, shield.Attributes.Luck);
            Assert.Equal(1000, player.Luck);

            shield.Attributes.Luck = 0;

            Assert.Equal(0, shield.Attributes.Luck);
            Assert.Equal(0, player.Luck);
            Assert.Same(player, shield.Parent);
        }
        finally
        {
            player.Delete();
        }
    }
}
