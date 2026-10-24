using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Multiclass;

public class BeetleBomb : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 40; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = GetInstance<RangedStupidDamage>();
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.width = 13;
        Item.height = 13;
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
        Item.knockBack = 7f;
        Item.value = 3400;
        Item.rare = ItemRarityID.Lime;
        Item.shoot = ProjectileType<BeetleBombProj>(); // The projectile that weapons fire when using this item as ammunition.
        Item.shootSpeed = 14.25f; // The speed of the projectile.

        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        {
            Item.DamageType = DamageClass.Throwing;
        }
    }
    public float LobotometerCost = 9f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe(20);
        recipe.AddIngredient(ItemID.Beenade, 20);
        recipe.AddIngredient(ItemID.ChlorophyteBar);
        recipe.AddIngredient(ItemID.BeetleHusk, 2);

        recipe.Register();
    }
}