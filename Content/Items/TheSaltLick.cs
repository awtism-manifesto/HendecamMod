using System.Collections.Generic;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items;

public class TheSaltLick : ModItem
{
    private static readonly int[] unwantedPrefixes = new[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Shameful, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

    public override void SetStaticDefaults()
    {
        ItemID.Sets.Yoyo[Item.type] = true; // Used to increase the gamepad range when using Strings.
        ItemID.Sets.GamepadExtraRange[Item.type] = 11; // Increases the gamepad range. Some vanilla values: 4 (Wood), 10 (Valor), 13 (Yelets), 18 (The Eye of Cthulhu), 21 (Terrarian).
        ItemID.Sets.GamepadSmartQuickReach[Item.type] = true; // Unused, but weapons that require aiming on the screen are in this set.
    }

    public override void SetDefaults()
    {
        Item.width = 24; 
        Item.height = 24;
        Item.useStyle = ItemUseStyleID.Shoot; 
        Item.useTime = 25; 
        Item.useAnimation = 25; 
        Item.noMelee = true; 
        Item.noUseGraphic = true; 
        Item.UseSound = SoundID.Item1; 
        Item.damage = 23; 
        Item.DamageType = DamageClass.MeleeNoSpeed; 
        Item.knockBack = 2.5f; 
        Item.channel = true; 
        Item.rare = ItemRarityID.Green; 
        Item.value = 105000; 

        Item.shoot = ModContent.ProjectileType<SaltYoyo>(); 
        Item.shootSpeed = 16f; 		
    }

    public override bool AllowPrefix(int pre)
    {
        // return false to make the game reroll the prefix.

        // DON'T DO THIS BY ITSELF:
        // return false;
        // This will get the game stuck because it will try to reroll every time. Instead, make it have a chance to return true.

        if (Array.IndexOf(unwantedPrefixes, pre) > -1)
        {
            return false;
        }

        // Don't reroll
        return true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Salt flakes off with every hit")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<RockSalt>(35);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}