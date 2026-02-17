using System.Collections.Generic;
using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Items.Tools;

public class LycopiteHamaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 33;
        Item.DamageType = DamageClass.Melee;
        Item.width = 60;
        Item.height = 60;
        Item.useTime = 11;
        Item.useAnimation = 20;
        Item.scale = 1.2f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 7.75f;
        Item.useTurn = true;

        Item.value = 178900;
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost = 1;

        Item.hammer = (int)79.999f;
        Item.axe = (int)19.4f;
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
        for (int i = 0; i < 3; i++)
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, ModContent.DustType<LycopiteDust>());
            dust.noGravity = true;
            dust.velocity *= 3.5f;
            dust.scale *= 0.75f;
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