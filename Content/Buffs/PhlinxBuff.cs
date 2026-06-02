using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;

namespace HendecamMod.Content.Buffs;



public class PhlinxBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
        Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
    }

    public override void Update(Player player, ref int buffIndex)
    {
        // If the minions exist reset the buff time, otherwise remove the buff from the player
        if (player.ownedProjectileCounts[ProjectileType<Phlinx>()] > 0)
        {
            player.buffTime[buffIndex] = 18000;
        }
        else
        {
            player.DelBuff(buffIndex);
            buffIndex--;
        }
    }
}