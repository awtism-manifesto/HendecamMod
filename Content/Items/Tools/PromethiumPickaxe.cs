using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Tools;

public class PromethiumPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 252;
        Item.DamageType = DamageClass.Melee;
        Item.width = 38;
        Item.height = 38;
        Item.useTime = 1;
        Item.useAnimation = 9;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 11;
        Item.scale = 1.67f;
        Item.useTurn = true;
        Item.value = Item.buyPrice(gold: 295);
        Item.rare = ItemRarityID.Purple;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.pick = 260;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
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
        target.AddBuff(ModContent.BuffType<RadPoisoning4>(), 400);
        for (int i = 0; i < 4; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, ModContent.DustType<PromethiumDust>());
            dust.noGravity = true;
            dust.velocity *= 6.25f;
            dust.scale *= 0.9f;
        }
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<PromethiumBar>(36);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
    }
}