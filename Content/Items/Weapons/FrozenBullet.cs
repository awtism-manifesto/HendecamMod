using System.Collections.Generic;
using HendecamMod.Content.Projectiles.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Weapons;

public class FrozenBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 8;
        Item.DamageType = DamageClass.Ranged;

        Item.width = 16;
        Item.height = 16;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.knockBack = 0.5f;
        Item.value = 14;

        Item.shoot = ModContent.ProjectileType<FrozenBulletProj>();
        Item.shootSpeed = 3.5f;
        Item.ammo = AmmoID.Bullet;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Shots inflict Frostburn for 3 seconds");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(50);
        recipe.AddIngredient(ItemID.MusketBall, 50);
        recipe.AddIngredient(ItemID.IceTorch);
        recipe.Register();
    }
}