using System.Collections.Generic;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Items.Tools;

public class PlutoniumHamaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 70;
        Item.DamageType = DamageClass.Melee;
        Item.width = 50;
        Item.height = 50;
        Item.useTime = 9;
        Item.useAnimation = 19;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 8.5f;

        Item.value = 190000; // Buy this item for one gold - change gold to any coin and change the value to any number <= 100
        Item.rare = ItemRarityID.LightPurple;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.hammer = 115;
        Item.axe = 29;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
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

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(ModContent.BuffType<RadPoisoning2>(), 245);
        for (int i = 0; i < 5; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, ModContent.DustType<PlutoniumDust>());
            dust.noGravity = true;
            dust.velocity *= 4.5f;
            dust.scale *= 0.76f;
        }
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<PlutoniumBar>(15);
        recipe.AddTile(TileID.MythrilAnvil);

        recipe.Register();
    }
}