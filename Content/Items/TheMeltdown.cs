using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items;

public class TheMeltdown : ModItem
{


    public override void SetDefaults()
    {
        // This method quickly sets the whip's properties.
        // Mouse over to see its parameters.
        Item.DefaultToWhip(ModContent.ProjectileType<AstaWhip>(), 33, 9, 4.25f);
        Item.rare = ItemRarityID.Red;
        Item.damage = 499;
        Item.useTime = 45;
        Item.useAnimation = 45;
        Item.knockBack = 15;
        Item.ArmorPenetration = 35;
        Item.width = 14;
        Item.height = 14;
        Item.value = 22555555;
        Item.DamageType = ModContent.GetInstance<MeleeSummonDamage>();
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Causes an instant explosion upon hitting an enemy");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'I'm a strong independent summoner player, i don't need my minions to commit war crimes for me!'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);



        // Here we will hide all tooltips whose title end with ':RemoveMe'
        // One like that is added at the start of this method
        foreach (var l in tooltips)
        {
            if (l.Name.EndsWith(":RemoveMe"))
            {
                l.Hide();
            }
        }

        // Another method of hiding can be done if you want to hide just one line.
        // tooltips.FirstOrDefault(x => x.Mod == "ExampleMod" && x.Name == "Verbose:RemoveMe")?.Hide();
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();




        recipe.AddIngredient(ItemID.FireWhip);
        recipe.AddIngredient<Items.ChainReaction>();
        recipe.AddIngredient<FissionDrive>();
        recipe.AddTile(TileID.LunarCraftingStation);

        recipe.Register();

    }

    // Makes the whip receive melee prefixes
    public override bool MeleePrefix()
    {
        return true;
    }
}