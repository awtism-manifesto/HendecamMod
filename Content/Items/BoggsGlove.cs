using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;

namespace HendecamMod.Content.Items;

public class BoggsGlove : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 62; // Hitbox width of the item.
        Item.height = 32; // Hitbox height of the item.
        Item.scale = 1.2f;
        Item.rare = ItemRarityID.Cyan; // The color that the item's name will be in-game.
        Item.value = 595000;
        Item.useTime = 8; // The item's use time in ticks (60 ticks == 1 second.)
        Item.useAnimation = 8; // The length of the item's use animation in ticks (60 ticks == 1 second.)
        Item.useStyle = ItemUseStyleID.Swing; // How you use the item (swinging, holding out, etc.)
        Item.autoReuse = true;
        Item.noUseGraphic = true;
        Item.UseSound = SoundID.Item1;
        Item.DamageType = ModContent.GetInstance<StupidDamage>(); // Sets the damage type to ranged.
        Item.damage = 105; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
        Item.knockBack = 3f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
        Item.noMelee = true; // So the item's animation doesn't do damage.

        // Gun Properties
        // For some reason, all the guns in the vanilla source have this.
        Item.shootSpeed = 31.95f; // The speed of the projectile (measured in pixels per frame.)

        Item.shoot = ProjectileID.Ale;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Rapidly throws ale at your target");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'Dedicated to the drinking legend Wade Boggs'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        if (ModLoader.TryGetMod("Fargowiltas", out Mod FargoMerica2) && FargoMerica2.TryFind("Tavernkeep", out ModItem Tavernkeep))
        {
            recipe = CreateRecipe();
            recipe.AddIngredient(Tavernkeep.Type);
            recipe.AddIngredient(ItemID.BossBagBetsy);
            recipe.Register();
        }
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-28f, -3f);
    }
}