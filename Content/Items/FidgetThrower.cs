using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;

public class FidgetThrower : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 23;
        Item.useAnimation = 23;
        Item.autoReuse = true;
        Item.DamageType = ModContent.GetInstance<RangedStupidDamage>();
        Item.damage = 45;
        Item.knockBack = 4f;

        Item.noMelee = true;
        Item.value = 250000;
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item99;
        Item.scale = 1f;

        Item.shoot = ModContent.ProjectileType<FidgetSpinner1>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 17.75f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }
    public float LobotometerCost = 5f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Shoots piercing fidget spinners that inflict random debuffs");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Uses 5 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-2f, -3f);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<KingslayerBar>(9);
        recipe.AddIngredient(ItemID.HellstoneBar, 9);
        recipe.AddIngredient(ItemID.JungleSpores, 9);
        recipe.AddIngredient(ItemID.IllegalGunParts);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}