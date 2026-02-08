using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;

public class BeetleBomb : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Item.damage = 40; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
        Item.DamageType = ModContent.GetInstance<RangedStupidDamage>();
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
        Item.shoot = ModContent.ProjectileType<BeetleBombProj>(); // The projectile that weapons fire when using this item as ammunition.
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
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Throws heavy beetle bombs that explode into many beetles");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "The faster the bomb is moving when it explodes, the faster the beetles fly")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Does not damage tiles")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Uses 9 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica))
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Hendecam Mod Cross-Mod (Thorium): Now deals Throwing damage") { OverrideColor = Color.LightSeaGreen });
        }

       
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