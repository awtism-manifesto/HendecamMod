using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons;

public class CopperQuadsword : ModItem
{
    // Here is an example of blacklisting certain modifiers. Remove this section for standard vanilla behavior.
    // In this example, we are blacklisting the ones that reduce damage of a melee weapon.
    // Make sure that your item can even receive these prefixes (check the vanilla wiki on prefixes).
    private static readonly int[] unwantedPrefixes = new[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Shameful, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

    public override void SetDefaults()
    {
        Item.width = 24; // The width of the item's hitbox.
        Item.height = 24; // The height of the item's hitbox.

        Item.useStyle = ItemUseStyleID.Shoot; // The way the item is used (e.g. swinging, throwing, etc.)
        Item.useTime = 31; // All vanilla yoyos have a useTime of 25.
        Item.useAnimation = 31; // All vanilla yoyos have a useAnimation of 25.
        Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
        Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).
        Item.UseSound = SoundID.Item1; // The sound that will play when the item is used.

        Item.damage = 14; // The amount of damage the item does to an enemy or player.
        Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
        Item.knockBack = 5f; // The amount of knockback the item inflicts.
        Item.crit = 4; // The percent chance for the weapon to deal a critical strike. Defaults to 4.

        Item.rare = ItemRarityID.White; // The item's rarity. This changes the color of the item's name.
        Item.value = Item.buyPrice(copper: 480); // The amount of money that the item is can be bought for.

        Item.shoot = ModContent.ProjectileType<CopperQuadswordProjectile>(); // Which projectile this item will shoot. We set this to our corresponding projectile.
        Item.shootSpeed = 16.5f; // The velocity of the shot projectile.			
    }
    public float LobotometerCost = 4f;
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
        var line = new TooltipLine(Mod, "Face", "4 swords are better than 1");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Uses 4 Lobotometer")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override bool AllowPrefix(int pre)
    {
        // return false to make the game reroll the prefix.

        // DON'T DO THIS BY ITSELF:
        // return false;
        // This will get the game stuck because it will try to reroll every time. Instead, make it have a chance to return true.

        if (Array.IndexOf(unwantedPrefixes, pre) > -1)
        {
            // IndexOf returns a positive index of the element you search for. If not found, it's less than 0.
            // Here we check if the selected prefix is positive (it was found).
            // If so, we found a prefix that we don't want. Reroll.
            return false;
        }

        // Don't reroll
        return true;
    }

    public override void AddRecipes()
    {
        Recipe 

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CopperShortsword, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
           
        
    }
}