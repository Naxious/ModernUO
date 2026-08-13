using Server;
using Server.Items;
using Server.Mobiles;
using Xunit;

namespace UOContent.Tests;

[Collection("Sequential UOContent Tests")]
public class VendorPaymentTests
{
    [Fact]
    public void PurchaseFallsBackToBankForAmountsBelowLegacyThreshold()
    {
        var buyer = CreateBuyer(backpackGold: 0, bankGold: 14_000);

        try
        {
            Assert.True(BaseVendor.TryPayForPurchase(buyer, 100, out var fromBank));
            Assert.True(fromBank);
            Assert.Equal(13_900, Banker.GetBalance(buyer));
        }
        finally
        {
            buyer.Delete();
        }
    }

    [Fact]
    public void PurchasePrefersBackpackWhenItCanCoverTheFullAmount()
    {
        var buyer = CreateBuyer(backpackGold: 200, bankGold: 14_000);

        try
        {
            Assert.True(BaseVendor.TryPayForPurchase(buyer, 100, out var fromBank));
            Assert.False(fromBank);
            Assert.Equal(100, buyer.Backpack.GetAmount(typeof(Gold)));
            Assert.Equal(14_000, Banker.GetBalance(buyer));
        }
        finally
        {
            buyer.Delete();
        }
    }

    [Fact]
    public void BankFallbackDoesNotPartiallyConsumeBackpackGold()
    {
        var buyer = CreateBuyer(backpackGold: 50, bankGold: 14_000);

        try
        {
            Assert.True(BaseVendor.TryPayForPurchase(buyer, 100, out var fromBank));
            Assert.True(fromBank);
            Assert.Equal(50, buyer.Backpack.GetAmount(typeof(Gold)));
            Assert.Equal(13_900, Banker.GetBalance(buyer));
        }
        finally
        {
            buyer.Delete();
        }
    }

    private static PlayerMobile CreateBuyer(int backpackGold, int bankGold)
    {
        var buyer = new PlayerMobile(World.NewMobile);
        buyer.DefaultMobileInit();
        buyer.AddItem(new Backpack());

        if (backpackGold > 0)
        {
            buyer.Backpack.DropItem(new Gold(backpackGold));
        }

        if (bankGold > 0)
        {
            buyer.BankBox.DropItem(new Gold(bankGold));
        }

        return buyer;
    }
}
