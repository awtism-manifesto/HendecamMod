using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Tools;

public class MintalHamaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 19;
        Item.DamageType = DamageClass.Melee;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 12;
        Item.useAnimation = 34;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 6;
        Item.value = 10000;
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.useTurn = true;

        Item.axe = 25;
        Item.hammer = 80;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
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
        recipe.AddIngredient<Placeables.MintalBar>(18);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}