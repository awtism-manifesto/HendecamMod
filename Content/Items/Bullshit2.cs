using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items;

public class Bullshit2 : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.autoReuse = true;
        Item.scale = 1.15f;
        Item.DamageType = ModContent.GetInstance<OmniDamage>();
        Item.damage = 55;
        Item.knockBack = 7.5f;
        Item.mana = 6;
        Item.ArmorPenetration = 10;
        Item.value = Item.buyPrice(gold: 135);
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item8;
        Item.shoot = ModContent.ProjectileType<GalaxyProj>(); 
        Item.shootSpeed = 12.75f;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Shoots weakly explosive, shattering galaxy pearls with 16 summon tag damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "-Dedicated Item-")
        {
            OverrideColor = new Color(252, 141, 204)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Bullshit1>();
        recipe.AddIngredient(ItemID.TruffleWorm);
        recipe.AddIngredient(ItemID.RainbowSlimeBanner);
        recipe.AddIngredient(ItemID.GalaxyPearl);
        recipe.AddTile(TileID.LihzahrdFurnace);
        recipe.Register();
    }
}