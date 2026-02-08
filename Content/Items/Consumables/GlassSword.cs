using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Consumables;

public class GlassSword : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 36;
        Item.value = 150;
        Item.rare = ItemRarityID.White;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Shatter;
        Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
        Item.maxStack = Item.CommonMaxStack;
        Item.damage = 20;
        Item.knockBack = 4.0f;
        Item.consumable = true;
        Item.ChangePlayerDirectionOnShoot = true;
        Item.buffType = BuffID.Bleeding;
        Item.buffTime = 300;
        Item.useTurn = true;
    }
    public float LobotometerCost = 7f;
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
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Makes you bleed when swung. It's shattering in your hand, what did you expect?"));
        tooltips.Add(new TooltipLine(Mod, "Tooltip#1", "Uses 7 Lobotometer"));
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Glass, 10);
        recipe.AddTile(TileID.GlassKiln);
        recipe.Register();
    }
}