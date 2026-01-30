using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Rarities;

namespace HendecamMod.Content.Items;

/// <summary>
///     Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
///     See Source code for Star Wrath projectile to see how it passes through tiles.
///     For a detailed sword guide see <see cref="ExampleSword" />
/// </summary>
public class TheFuckingSun : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 33;
        Item.height = 33;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 66;
        Item.useAnimation = 66;
        Item.autoReuse = true;

        Item.DamageType = ModContent.GetInstance<StupidDamage>();
        Item.damage = 11111;
        Item.knockBack = 333;
        Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
        Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).
        Item.ArmorPenetration = 999;
        Item.value = Item.buyPrice(gold: 99999);
        Item.rare = ModContent.RarityType<Seizure2>();
        Item.UseSound = SoundID.Item1;

        Item.shoot = ModContent.ProjectileType<TheSun>(); // ID of the projectiles the sword will shoot
        Item.shootSpeed = 12.5f; // Speed of the projectiles the sword will shoot

        // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
        // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

        // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
        // Item.ChangePlayerDirectionOnShoot = false;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Literally throws the fucking sun at your enemy");
        tooltips.Add(line);
        // Here we will hide all tooltips whose title end with ':RemoveMe'
        // One like that is added at the start of this method
        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }

        // Another method of hiding can be done if you want to hide just one line.
        // tooltips.FirstOrDefault(x => x.Mod == "ExampleMod" && x.Name == "Verbose:RemoveMe")?.Hide();
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("AshesofAnnihilation", out ModItem AshesofAnnihilation)
                                                                  && CalMerica.TryFind("AuricBar", out ModItem AuricBar) && CalMerica.TryFind("YharonSoulFragment", out ModItem YharonSoulFragment))
        {
            recipe = CreateRecipe();

            recipe.AddIngredient<TheMoon>();
            recipe.AddIngredient<FissionDrive>();
            recipe.AddIngredient(AshesofAnnihilation.Type, 5);
            recipe.AddIngredient(AuricBar.Type, 5);
            recipe.AddIngredient(YharonSoulFragment.Type, 10);
            recipe.AddIngredient(ItemID.FragmentSolar, 25);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        else
        {
            recipe = CreateRecipe();

            recipe.AddIngredient<TheMoon>();
            recipe.AddIngredient<FissionDrive>(99);
            recipe.AddIngredient(ItemID.FragmentSolar, 999);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}