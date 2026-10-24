using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using HendecamMod.Content.Items.Weapons.Magic;

namespace HendecamMod.Content.Global;
public class DeathPlayer : ModPlayer
{
    public override void PreUpdate()
    {
        var config = ModContent.GetInstance<HendecamExperimentalConfig>();

        int chance = config.DeathChance;

        if (!config.DeathEnabled)
        {
            return;
        }

        if (Main.rand.NextBool(chance * 60))
        {
            Player.AddBuff(ModContent.BuffType<OtherworldPoisoning>(), 120);


        }
    }
}