using HendecamMod.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Tools;

public class AncientCobaltWaraxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 21;
        Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 7;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 6;
        Item.value = 10000;
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.scale = 1.15f;
        Item.axe = 22;
        Item.hammer = 75;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        Item.useTurn = true;
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(10))
        {
        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<AncientCobaltBar>(12);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}