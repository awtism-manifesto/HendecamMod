using log4net.Core;
using Microsoft.Xna.Framework;
using HendecamMod.Content.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Global
{
    // if you have to read through these unhinged ahh public classes and youre not Autism Manifesto, i apologize.
    public class CobaltPickaxeBuff : GlobalItem
    {

        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.CobaltPickaxe;
        }

        public override void SetDefaults(Item item)
        {
            item.pick = 115;
        }
    }
}