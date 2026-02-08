using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;

/// <summary>
///     Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
///     See Source code for Star Wrath projectile to see how it passes through tiles.
///     For a detailed sword guide see <see cref="ExampleSword" />
/// </summary>
public class DvdLogo : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.autoReuse = true;

        Item.DamageType = ModContent.GetInstance<StupidDamage>();
        Item.damage = 155;
        Item.knockBack = 6;
        Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
        Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).

        Item.value = Item.buyPrice(gold: 20);
        Item.rare = ItemRarityID.Cyan;
        Item.UseSound = SoundID.Item1;

        Item.shoot = ModContent.ProjectileType<DvdBlue>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 11.5f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
    public float LobotometerCost = 12f;
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
        var line = new TooltipLine(Mod, "Face", "Throws dvd logos that intermittently explode and randomly change direction");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Uses 12 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        if (ModLoader.TryGetMod("Fargowiltas", out Mod FargoMerica2) && FargoMerica2.TryFind("Cyborg", out ModItem Cyborg))
        {
            recipe = CreateRecipe();

            recipe.AddIngredient(Cyborg.Type);
            recipe.Register();
        }
    }
}