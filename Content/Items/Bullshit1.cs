using System.Collections.Generic;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Projectiles;

namespace HendecamMod.Content.Items;

public class Bullshit1 : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.autoReuse = true;
        Item.scale = 1.1f;
        Item.DamageType = ModContent.GetInstance<OmniDamage>();
        Item.damage = 35;
        Item.knockBack = 4.5f;
        Item.mana = 5;
        Item.ArmorPenetration = 5;
        Item.value = Item.buyPrice(gold: 67);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item8;

        Item.shoot = ModContent.ProjectileType<PearlProj>();
        Item.shootSpeed = 9.25f;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Shoots homing pink pearls with 9 summon tag damage");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "-Dedicated Item-")
        {
            OverrideColor = new Color(252, 141, 204)
        };
        tooltips.Add(line);
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MetalDetector);
        recipe.AddIngredient(ItemID.GoldLadyBug);
        recipe.AddIngredient(ItemID.NypmhBanner);
        recipe.AddIngredient(ItemID.PinkPearl);
        recipe.AddTile(TileID.Hellforge);
        recipe.Register();
        if (ModLoader.TryGetMod("SpiritReforged", out Mod SpiritMerica) && SpiritMerica.TryFind("GoldGarItem", out ModItem GoldGarItem))
        {
            recipe.AddIngredient(GoldGarItem.Type);
        }
    }
}