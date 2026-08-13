using System;

namespace Server.Mobiles;

public static class ShardEventSink
{
    public static event Action<PlayerMobile> PlayerLogin;
    public static event Action<PlayerMobile> PlayerDeath;
    public static event Action<PlayerMobile> PlayerDeleted;
    public static event Action<BaseCreature> CreatureDeath;

    internal static void InvokePlayerLogin(PlayerMobile player) => PlayerLogin?.Invoke(player);
    internal static void InvokePlayerDeath(PlayerMobile player) => PlayerDeath?.Invoke(player);
    internal static void InvokePlayerDeleted(PlayerMobile player) => PlayerDeleted?.Invoke(player);
    internal static void InvokeCreatureDeath(BaseCreature creature) => CreatureDeath?.Invoke(creature);
}
