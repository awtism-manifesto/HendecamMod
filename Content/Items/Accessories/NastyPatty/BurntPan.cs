using HendecamMod.Common.Systems.Players;
using HendecamMod.Common.Utilities;
using HendecamMod.Content.Buffs;
using System.Collections.Generic;
using Terraria.DataStructures;
using static HendecamMod.Content.Items.Accessories.NastyPatty.NastyPattyAccessory;

namespace HendecamMod.Content.Items.Accessories.NastyPatty;

public class BurntPan : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.value = Item.sellPrice(silver: 1000);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item178;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 100;
        Item.knockBack = 40.0f;
        Item.ChangePlayerDirectionOnShoot = true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        player.lifeRegen += (int)(8f);
        return true;
    }

    public override void UpdateEquip(Player player)
    {
        var target = player.GetHCPlayer();
        target.burntPan = true;
        player.lifeRegen += 6;

    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient<PacifistsMark>();
        recipe.AddIngredient<HotWax>();
        recipe.AddTile(TileID.TinkerersWorkbench);
        recipe.Register();
    }
}