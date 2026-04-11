using HendecamMod.Common.Systems;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items.QuadswordProjectiles;
using System.Collections.Generic;

namespace HendecamMod.Content.Items.Weapons.Multiclass;

public class LeadQuadsword : ModItem
{
    // Here is an example of blacklisting certain modifiers. Remove this section for standard vanilla behavior.
    // In this example, we are blacklisting the ones that reduce damage of a melee weapon.
    // Make sure that your item can even receive these prefixes (check the vanilla wiki on prefixes).
    private static readonly int[] unwantedPrefixes = new[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Shameful, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 24;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.UseSound = SoundID.Item1;
        Item.damage = 15;
        Item.DamageType = GetInstance<MeleeStupidDamage>();
        Item.knockBack = 5f;
        Item.crit = 4;
        Item.rare = ItemRarityID.White;
        Item.value = Item.buyPrice(copper: 480);

        Item.shoot = ProjectileType<LeadQuadswordProj>();
        Item.shootSpeed = 16.5f;
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
        recipe.AddIngredient(ItemID.LeadShortsword, 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();


    }
}