using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Tiles.Furniture;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;

public class Bullshit3 : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.autoReuse = true;
        Item.scale = 1.2f;
        Item.DamageType = ModContent.GetInstance<OmniDamage>();
        Item.damage = 125;
        Item.knockBack = 10.5f;
        Item.mana = 8;
        Item.ArmorPenetration = 25;
        Item.value = Item.buyPrice(gold: 205);
        Item.rare = ItemRarityID.Cyan;
        Item.UseSound = SoundID.Item8;
        Item.shoot = ModContent.ProjectileType<EmblemProj>();
        Item.shootSpeed = 14.25f;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Shoots dangerously explosive radiation emblems with 21 summon tag damage");
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
        recipe.AddIngredient<Bullshit2>();
        recipe.AddIngredient(ItemID.EmpressButterfly);
        recipe.AddIngredient(ItemID.PaladinBanner);
        recipe.AddIngredient<RadioactiveEmblem>();
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
        if (ModLoader.TryGetMod("ThoriumMod", out Mod ThorMerica) && ThorMerica.TryFind("GoldDuck", out ModItem GoldDuck))
        {
            recipe.AddIngredient(GoldDuck.Type);
        }

        if (ModLoader.TryGetMod("CalamityMod", out Mod CalMerica) && CalMerica.TryFind("CoreofCalamity", out ModItem CoreofCalamity))
        {
            recipe.AddIngredient(CoreofCalamity.Type);
        }
    }
}