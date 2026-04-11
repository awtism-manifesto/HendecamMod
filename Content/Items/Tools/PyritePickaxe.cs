using System.Collections.Generic;

namespace HendecamMod.Content.Items.Tools;

public class PyritePickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 13;
        Item.DamageType = DamageClass.Melee;
        Item.width = 35;
        Item.height = 35;
        Item.useTime = 10;
        Item.useAnimation = 19;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 4;
        Item.useTurn = true;
        Item.value = 162000;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;

        Item.pick = 64; // How strong the pickaxe is, see https://terraria.wiki.gg/wiki/Pickaxe_power for a list of common values
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
       
        for (int i = 0; i < 4; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.IchorTorch);
            dust.noGravity = true;
            dust.velocity *= 3.5f;
            dust.scale *= 0.65f;
        }
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<PyriteBar>(15);
        recipe.AddTile(TileID.Anvils);

        recipe.Register();
    }
}