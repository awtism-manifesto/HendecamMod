using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Multiclass;

public class AlpinePlushieGlitterbomb : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 21;
        Item.useTime = 21;
        Item.damage = 149;
        Item.knockBack = 7.5f;
        Item.width = 40;
        Item.height = 40;
        Item.shootSpeed = 15.5f;
        Item.ArmorPenetration = 10;
        Item.scale = 1f;
        Item.noUseGraphic = true;
        Item.UseSound = SoundID.Item1;
        Item.rare = ItemRarityID.Cyan;
        Item.value = 595000;
        Item.DamageType = GetInstance<SummonStupidDamage>();
        Item.shoot = ProjectileType<AlpineGlitterbomb>();
        Item.noMelee = true; // This is set the sword itself doesn't deal damage (only the projectile does).
        Item.shootsEveryUse = true; // This makes sure Player.ItemAnimationJustStarted is set when swinging.
        Item.autoReuse = true;
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
    }}