using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Weapons;

public class FlamingKnife : ModItem
{
    public override void SetDefaults()
    {

        // Common Properties
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(silver: 5);
        Item.maxStack = 9999;

        // Use Properties
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.consumable = true;

        // Weapon Properties			
        Item.damage = 16;
        Item.knockBack = 5f;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.DamageType = DamageClass.Throwing;
        Item.shootSpeed = 12f;
        Item.shoot = ModContent.ProjectileType<FlamingKnifeProjectile>();
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Explodes every time it hits an enemy");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(100);
        recipe.AddIngredient<FireDiamond>(1);
        recipe.AddIngredient(ItemID.HellstoneBar, 1);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}
