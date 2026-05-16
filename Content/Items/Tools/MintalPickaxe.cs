using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Tools;

public class MintalPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 36;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 7;
        Item.useAnimation = 19;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.1f;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 32;
        Item.knockBack = 6;
        Item.ChangePlayerDirectionOnShoot = false;
        Item.pick = 145;
        Item.tileBoost = 2;

        Item.value = 205000;
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item1;

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }

  

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

      
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<MintalBar>(20);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}