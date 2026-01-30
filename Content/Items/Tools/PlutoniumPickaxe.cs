using System.Collections.Generic;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Items.Tools;

public class PlutoniumPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 51;
        Item.DamageType = DamageClass.Melee;
        Item.width = 35;
        Item.height = 35;
        Item.useTime = 5;
        Item.useAnimation = 14;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 6;

        Item.value = 190000; // Buy this item for one gold - change gold to any coin and change the value to any number <= 100
        Item.rare = ItemRarityID.LightPurple;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.useTurn = true;

        Item.pick = 205; // How strong the pickaxe is, see https://terraria.wiki.gg/wiki/Pickaxe_power for a list of common values
        Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Can mine Chlorophyte");
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

        recipe.AddIngredient<PlutoniumBar>(21);
        recipe.AddTile(TileID.MythrilAnvil);

        recipe.Register();
    }
}