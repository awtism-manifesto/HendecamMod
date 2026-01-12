
using HendecamMod.Content.Buffs;
using HendecamMod.Content.DamageClasses;
using HendecamMod.Content.Dusts;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items.Accessories
{
    public class RadioactiveAura : ModItem
    {
        // By declaring these here, changing the values will alter the effect, and the tooltip

        public static readonly int AdditiveDamageBonus = 6;
        public static readonly int AttackSpeedBonus = 3;
        public static readonly int CritBonus = 6;


        // Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AdditiveDamageBonus);

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = 230000;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Enemies are much less likely to target you");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "6% increased damage and critical strike chance, and 3% increased attack speed")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "Your attacks now inflict Radiation Poisoning")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);


        }
        public override void AddRecipes()
        {
            Recipe 


            
                recipe = CreateRecipe();
               
                recipe.AddIngredient<PlutoniumBar>(12);
                
                recipe.AddIngredient(ItemID.PutridScent);
                recipe.AddTile(TileID.TinkerersWorkbench);
                recipe.Register();

            

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // GetDamage returns a reference to the specified damage class' damage StatModifier.
            // Since it doesn't return a value, but a reference to it, you can freely modify it with mathematics operators (+, -, *, /, etc.).
            // StatModifier is a structure that separately holds float additive and multiplicative modifiers, as well as base damage and flat damage.
            // When StatModifier is applied to a value, its additive modifiers are applied before multiplicative ones.
            // Base damage is added directly to the weapon's base damage and is affected by damage bonuses, while flat damage is applied after all other calculations.
            // In this case, we're doing a number of things:
            // - Adding 25% damage, additively. This is the typical "X% damage increase" that accessories use, use this one.
            // - Adding 12% damage, multiplicatively. This effect is almost never used in Terraria, typically you want to use the additive multiplier above. It is extremely hard to correctly balance the game with multiplicative bonuses.
            // - Adding 4 base damage.
            // - Adding 5 flat damage.
            // Since we're using DamageClass.Generic, these bonuses apply to ALL damage the player deals.
            player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 106f;
            player.GetAttackSpeed(DamageClass.Generic) += AttackSpeedBonus / 103f;
            player.GetCritChance(DamageClass.Generic) += CritBonus;
            player.GetModPlayer<Rad2Apply>().rad2Effect = true;
            player.aggro += -660;
        }
    }
    public class Rad2Apply : ModPlayer
    {
        public bool rad2Effect; public override void ResetEffects()
        {
            rad2Effect = false;
        }

        public override void PostUpdateRunSpeeds()
        {
            if (Player.GetModPlayer<Rad2Apply>().rad2Effect == false)
            {
                return;
            }
           
            if (Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<PlutoniumDust>(),
                    Player.velocity.X * Main.rand.NextFloat(-1.2f, 2.33f), Player.velocity.Y * Main.rand.NextFloat(-1.2f, 2.33f), 70, default, 0.82f);
                Main.dust[dust].noGravity = true;


            }
           

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.GetModPlayer<Rad2Apply>().rad2Effect == false)
            {
                return;
            }
            else
            {
               
                target.AddBuff(ModContent.BuffType<RadPoisoning2>(), 240);
              
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.GetModPlayer<Rad2Apply>().rad2Effect == false)
            {
                return;
            }
            else
            {
               
                target.AddBuff(ModContent.BuffType<RadPoisoning2>(), 240);
               
            }
        }
    }


}
