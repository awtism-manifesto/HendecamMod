
using HendecamMod.Content.DamageClasses;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Poop;

public class PoopPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 6;
        Item.DamageType = ModContent.GetInstance<MeleeStupidDamage>();
        Item.width = 30;
        Item.height = 30;
        Item.useTime = 11;
        Item.useAnimation = 19;
        Item.scale = 1f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 2;

        Item.value = Item.buyPrice(copper: 70); // Buy this item for one gold - change gold to any coin and change the value to any number <= 100

        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost = -1;
        Item.pick = 45;

        Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "Makes the user stinky");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {

        player.AddBuff(BuffID.Stinky, 61);


        return true;
    }
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        // Inflict the OnFire debuff for 1 second onto any NPC/Monster that this hits.
        // 60 frames = 1 second
        target.AddBuff(BuffID.Stinky, 900);
        target.AddBuff(BuffID.Poisoned, 180);
        for (int i = 0; i < 7; i++) // Creates a splash of dust around the position the projectile dies.
        {
            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Poop);
            dust.noGravity = true;
            dust.velocity *= 7.5f;
            dust.scale *= 1.25f;
        }

    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddRecipeGroup("Wood", 4);
        recipe.AddIngredient(ItemID.PoopBlock, 8);

        recipe.Register();
    }

}