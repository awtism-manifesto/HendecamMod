using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Rarities;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;

public class TheFuckingSun : ModItem
{
    public float LobotometerCost = 36f;
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
        Item.shoot = ModContent.ProjectileType<TheSun>();
        Item.shootSpeed = 12.5f;
        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica))
        {
            Item.useTime = 55;
            Item.useAnimation = 55;
        }
    }
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Literally throws the fucking sun at your enemy");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Uses 36 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

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
            recipe.AddTile<CultistCyclotronPlaced>();
            recipe.Register();
        }
        else
        {
            recipe = CreateRecipe();
            recipe.AddIngredient<TheMoon>();
            recipe.AddIngredient<FissionDrive>(99);
            recipe.AddIngredient(ItemID.FragmentSolar, 999);
            recipe.AddTile<CultistCyclotronPlaced>();
            recipe.Register();
        }
    }
}