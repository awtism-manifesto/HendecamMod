using HendecamMod.Content.DamageClasses;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Buffs
{
    public class BigShotCooldown : ModBuff
    {

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
           


        }

    }
}