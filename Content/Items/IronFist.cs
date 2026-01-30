using System.Collections.Generic;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items;

public class IronFist : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 33;
        Item.knockBack = 9.5f;
        Item.useStyle = ItemUseStyleID.Rapier; // Makes the player do the proper arm motion
        Item.useAnimation = 21;
        Item.useTime = 21;
        Item.width = 32;
        Item.height = 32;
        Item.UseSound = SoundID.Item60;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.autoReuse = false;
        Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
        Item.noMelee = true; // The projectile will do the damage and not the item

        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 3);

        Item.shoot = ModContent.ProjectileType<IronFistProj>(); // The projectile is what makes a shortsword work
        Item.shootSpeed = 2.15f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Allows the player to punch so hard it releases a piercing shockwave");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
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
        recipe.AddIngredient(ItemID.FeralClaws);
        recipe.AddIngredient(ItemID.IronBar, 35);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}