using System.Collections.Generic;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Dusts;

namespace HendecamMod.Content.Items.Tools;

public class AstatineAnnihilator : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
    }

    public override void SetDefaults()
    {
        Item.damage = 125;
        Item.DamageType = DamageClass.Melee;
        Item.width = 50;
        Item.height = 50;
        Item.useTime = 4;
        Item.useAnimation = 15;
        Item.scale = 1.67f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 10;
        Item.useTurn = true;
        Item.value = Item.buyPrice(gold: 70); // Buy this item for one gold - change gold to any coin and change the value to any number <= 100
        Item.rare = ItemRarityID.Red;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost = 5;
        Item.pick = 245;

        Item.axe = 43;
        Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }

    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 2;
            Item.useAnimation = 2;
            Item.pick = 0;

            Item.axe = 0;
            Item.hammer = 185;
        }
        else
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 4;
            Item.useAnimation = 15;
            Item.pick = 245;
            Item.hammer = 0;
            Item.axe = 43;
        }

        return base.CanUseItem(player);
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Left click for pickaxe and axe functionality");
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Right click for hammer functionality")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
        line = new TooltipLine(Mod, "Face", "Matter? more like doesn't matter!")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(ModContent.BuffType<RadPoisoning3>(), 300);

        for (int i = 0; i < 6; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, ModContent.DustType<AstatineDust>());
            dust.noGravity = true;
            dust.velocity *= 5.5f;
            dust.scale *= 0.9f;
        }
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();

        recipe.AddIngredient<AstatineBar>(25);
        recipe.AddIngredient<ThePurifier>();

        recipe.AddTile(TileID.LunarCraftingStation);

        recipe.Register();
    }
}