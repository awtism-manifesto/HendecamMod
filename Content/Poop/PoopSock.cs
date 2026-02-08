using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Poop;

public class PoopSock : ModItem
{
    public override void SetDefaults()
    {
        // This method quickly sets the whip's properties.
        // Mouse over to see its parameters.
        Item.DefaultToWhip(ModContent.ProjectileType<SockPoop>(), 20, 2, 7);
        Item.rare = ItemRarityID.White;
        Item.damage = 11;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.knockBack = 2;
        Item.width = 14;
        Item.height = 14;
        Item.value = 1;
        Item.DamageType = ModContent.GetInstance<SummonStupidDamage>();
    }
    public float LobotometerCost = 3f;
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
        var line = new TooltipLine(Mod, "Face", "3 summon tag damage");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Makes the user stinky")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Uses 3 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Represents the average Summoner player (until whipstacking gets removed in 1.4.5)")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

        
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        player.AddBuff(BuffID.Stinky, 61);
        return true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PoopBlock, 4);
        recipe.AddIngredient(ItemID.Silk, 4);
        recipe.Register();
    }

    // Makes the whip receive melee prefixes
    public override bool MeleePrefix()
    {
        return true;
    }
}