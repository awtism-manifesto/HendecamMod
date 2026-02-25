using HendecamMod.Common.Systems;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using System.Collections.Generic;

namespace HendecamMod.Content.Items;

// This is a basic item template.
// Please see tModLoader's ExampleMod for every other example:
// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
public class VeryBigStick : ModItem
{
    // The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.bitsnbobs.hjson' file.
    public override void SetDefaults()
    {
        Item.damage = 25;
        Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
        Item.scale = 1.5f;
        Item.width = 34;
        Item.height = 34;
        Item.useTime = 17;
        Item.useAnimation = 17;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 6;
        Item.value = Item.buyPrice(silver: 20);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.useTurn = true;
    }
    public float LobotometerCost = 2f;
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
        var line = new TooltipLine(Mod, "Face", "Uses 2 Lobotometer");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'Very confusing, it's not a stick'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);


    }
    public override void AddRecipes()
    {
        // With Ebonwood
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WoodenSword);
        recipe.AddIngredient(ItemID.EbonwoodSword);
        recipe.AddIngredient(ItemID.BorealWoodSword);
        recipe.AddIngredient(ItemID.PalmWoodSword);
        recipe.AddIngredient(ItemID.RichMahoganySword);
        recipe.AddIngredient<PoorMahoganySword>();
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        // With Shadewood
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WoodenSword);
        recipe.AddIngredient(ItemID.ShadewoodSword);
        recipe.AddIngredient(ItemID.BorealWoodSword);
        recipe.AddIngredient(ItemID.PalmWoodSword);
        recipe.AddIngredient(ItemID.RichMahoganySword);
        recipe.AddIngredient<PoorMahoganySword>();
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        
            target.AddBuff(ModContent.BuffType<Splinters>(), 360);
        
    }
}