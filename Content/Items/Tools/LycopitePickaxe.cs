using System.Collections.Generic;
using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Items.Tools;

public class LycopitePickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 27;
        Item.DamageType = DamageClass.Melee;
        Item.width = 35;
        Item.height = 35;
        Item.useTime = 8;
        Item.useAnimation = 14;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 6;

        Item.value = 69000;
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost = 1;
        Item.pick = 95;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Can mine Hellstone");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 2; i++)
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, ModContent.DustType<LycopiteDust>());
            dust.noGravity = true;
            dust.velocity *= 2.5f;
            dust.scale *= 0.66f;
        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<LycopiteBar>(17);
        recipe.AddTile(TileID.Anvils);

        recipe.Register();
    }
}