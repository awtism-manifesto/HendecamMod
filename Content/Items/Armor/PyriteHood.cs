using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class PyriteHood : ModItem
	{
		public static readonly int AdditiveMagicDamageBonus = 16;
		public override void SetDefaults()
		{
			Item.defense = 3;
			Item.rare = ItemRarityID.Blue;
            Item.value = 54000;
        }
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient<PyriteBar>(10);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
        public static readonly int MaxManaIncrease = 70;
        public static readonly int MagicCritBonus = 7;
        public override void UpdateEquip(Player player)
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

            
            player.GetCritChance(DamageClass.Magic) += MagicCritBonus;
            player.statManaMax2 += MaxManaIncrease;
           
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "+70 max mana and +7% magic crit chance");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);




        }
        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "15% increased magic damage";
			player.GetDamage(DamageClass.Magic) += AdditiveMagicDamageBonus / 116f;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<PyriteChestguard>() && legs.type == ModContent.ItemType<PyriteLegPlating>();
		}
	}
}