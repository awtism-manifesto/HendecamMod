using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Placeables;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Consumables;

public class UraniumGlassSword : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.sellPrice(silver: 10);
        Item.rare = ItemRarityID.Green;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Shatter;
        Item.DamageType = GetInstance<MeleeStupidDamage>();
        Item.maxStack = Item.CommonMaxStack;
        Item.damage = 60;
        Item.knockBack = 6.7f;
        Item.consumable = true;
        Item.ChangePlayerDirectionOnShoot = true;
        Item.buffType = BuffID.Bleeding;
        Item.buffType = BuffType<RadPoisoning>();
        Item.buffTime = 300;
        Item.useTurn = true;
    }
    public float LobotometerCost = 3.5f;
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            player.GetModPlayer<LobotometerPlayer>()
                  .AddLobotometer(LobotometerCost);
        }
        return base.UseItem(player);
    }
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffType<RadPoisoning>(), 300);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<UraniumGlass>(10);
        recipe.AddTile(TileID.GlassKiln);
        recipe.Register();
    }
}