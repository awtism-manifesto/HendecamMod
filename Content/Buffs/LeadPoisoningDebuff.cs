using HendecamMod.Common.Systems.Players;
namespace HendecamMod.Content.Buffs;

// This class serves as an example of a debuff that causes constant loss of life
// See ExampleLifeRegenDebuffPlayer.UpdateBadLifeRegen at the end of the file for more information
public class LeadPoisoningDebuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true; // Is it a debuff?
        Main.pvpBuff[Type] = true; // Players can give other players buffs, which are listed as pvpBuff
        Main.buffNoSave[Type] = false; // Causes this buff not to persist when exiting and rejoining the world
        BuffID.Sets.LongerExpertDebuff[Type] = true; // If this buff is a debuff, setting this to true will make this buff last twice as long on players in expert mode
    }

    // Allows you to make this buff give certain effects to the given player
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<HendecamPlayer>().lifeRegenDebuff = true;
    }
}

