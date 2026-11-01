using HendecamMod.Common.Utilities;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories;

public class UltraboostedWrench : ModItem
{
    public override void SetStaticDefaults()
    {
        // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(2, 31));
        ItemID.Sets.AnimatesAsSoul[Type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation
    }
    public override void SetDefaults()
    {
        Item.width = 45;
        Item.height = 30;
        Item.accessory = true;
        Item.rare = ItemRarityID.Red;
        Item.value = 9599000;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<OverclockedWrench>();
        recipe.AddIngredient<FissionDrive>();

        recipe.AddIngredient(ItemID.FragmentStardust, 10);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }
    public override void UpdateVanity(Player player)
    {
        if (Main.rand.NextBool(4))
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
            if (Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(player.position, player.width, player.height, DustID.Electric,
                    player.velocity.X * Main.rand.NextFloat(-1.75f, 2.67f), player.velocity.Y * Main.rand.NextFloat(-1.75f, 2.67f), 70, default, 0.82f);
                Main.dust[dust].noGravity = true;
            }
        }

        player.GetHCPlayer().ultraBoosted = true; 
    }
}