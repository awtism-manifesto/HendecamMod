using HendecamMod.Common.Utilities;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories;

public class OverclockedWrench : ModItem
{
    public override void SetStaticDefaults()
    {
        // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(3, 26));
        ItemID.Sets.AnimatesAsSoul[Type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation


      


    }
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Lime;
        Item.value = 1499000;
    }

    public override void UpdateVanity(Player player)
    {
        if (Main.rand.NextBool(8))
        {
            int dust = Dust.NewDust(player.position, player.width, player.height, DustID.Electric,
                player.velocity.X * Main.rand.NextFloat(-1.75f, 2.67f), player.velocity.Y * Main.rand.NextFloat(-1.75f, 2.67f), 70, default, 0.82f);
            Main.dust[dust].noGravity = true;
        }
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!hideVisual)
        {
            if (Main.rand.NextBool(8))
            {
                int dust = Dust.NewDust(player.position, player.width, player.height, DustID.Electric,
                    player.velocity.X * Main.rand.NextFloat(-1.75f, 2.67f), player.velocity.Y * Main.rand.NextFloat(-1.75f, 2.67f), 70, default, 0.82f);
                Main.dust[dust].noGravity = true;
            }
        }

        player.GetHCPlayer().overClocked = true;
    }
}