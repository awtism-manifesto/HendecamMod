using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Multiclass;

public class WhippetWhip : ModItem
{
    public override void SetDefaults()
    {
        // This method quickly sets the whip's properties.
        // Mouse over to see its parameters.
        Item.DefaultToWhip(ProjectileType<WhippetProj>(), 20, 2, 3.75f);
        Item.rare = ItemRarityID.Green;
        Item.damage = 21;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.knockBack = 3;
        Item.width = 14;
        Item.height = 14;
        Item.value = Item.buyPrice(silver: 600);
        Item.DamageType = GetInstance<SummonStupidDamage>();
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
    // Makes the whip receive melee prefixes
    public override bool MeleePrefix()
    {
        return true;
    }
}