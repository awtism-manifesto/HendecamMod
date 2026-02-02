using System.Collections.Generic;

namespace HendecamMod.Content.Items.Accessories;

public class EmpoweredManaCrystal : ModItem
{
    public static readonly int MaxManaIncrease = 50;
    public static readonly int MagicCritBonus = 5;

    public override void SetDefaults()
    {
        // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

        // Common Properties
        Item.width = 32; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
        Item.value = 35000;
        Item.maxStack = 1;
        Item.accessory = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += MaxManaIncrease;
        player.GetCritChance(damageClass: DamageClass.Magic) += MagicCritBonus;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "A mana crystal infused with extra fallen stars");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Increases Mana by 50 and Magic damage by 5% when equipped")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);

       
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.ManaCrystal);
        recipe.AddIngredient(ItemID.FallenStar, 4);

        recipe.Register();
    }
}