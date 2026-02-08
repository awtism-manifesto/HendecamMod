using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;

public class Bullshit4 : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 64;
        Item.height = 64;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 27;
        Item.useAnimation = 27;
        Item.autoReuse = true;
        Item.scale = 1.25f;
        Item.DamageType = ModContent.GetInstance<OmniDamage>();
        Item.damage = 175;
        Item.knockBack = 15.5f;
        Item.mana = 11;
        Item.ArmorPenetration = 50;
        Item.value = Item.buyPrice(gold: 550);
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item8;
        Item.shoot = ModContent.ProjectileType<MoonProjFunny>();
        Item.shootSpeed = 15.95f;
    }
    public float LobotometerCost = 11f;
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
        var line = new TooltipLine(Mod, "Face", "Shoots moons that cause a chain reaction of bullshit upon hitting an enemy");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Uses 11 Lobotometer")
        {
            OverrideColor = new Color(252, 141, 204)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "-Developer Item-")
        {
            OverrideColor = new Color(252, 141, 204)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<Bullshit3>();
        recipe.AddIngredient(ItemID.GoldWaterStrider);
        recipe.AddIngredient(ItemID.CrabBanner);
        recipe.AddIngredient<TheMoon>();
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();

        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica) && ThorMerica.TryFind("GoldLobster", out ModItem GoldLobster) && ThorMerica.TryFind("TheOmegaCore", out ModItem TheOmegaCore))
        {
            recipe.AddIngredient(GoldLobster.Type);
            recipe.AddIngredient(TheOmegaCore.Type);
        }
    }
}