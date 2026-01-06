
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Accessories.Rampart
{
    //[AutoloadEquip(EquipType.Beard)]
    public class RampartAccessory : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.defense = 32;
            Item.value = Item.sellPrice(silver: 32000);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "Face", "Grants immunity to every vanilla debuff, aswell as Knockback and fire blocks");
            tooltips.Add(line);
        }
        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.ManaSickness] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Suffocation] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.VortexDebuff] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.OgreSpit] = true;
            player.buffImmune[BuffID.NeutralHunger] = true;
            player.buffImmune[BuffID.Hunger] = true;
            player.buffImmune[BuffID.Starving] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.ShadowFlame] = true;
            player.buffImmune[BuffID.OnFire3] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Frostburn2] = true;
            player.buffImmune[BuffID.Obstructed] = true;
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Rabies] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Dazed] = true;
            player.fireWalk = true;
            player.noKnockback = true;
            player.buffImmune[BuffID.BoneJavelin] = true;
            player.buffImmune[BuffID.BloodButcherer] = true;
            player.buffImmune[BuffID.Midas] = true;
            player.buffImmune[BuffID.DryadsWardDebuff] = true;
            player.buffImmune[BuffID.BetsysCurse] = true;
            player.buffImmune[BuffID.Oiled] = true;
            player.buffImmune[BuffID.Daybreak] = true;
            player.buffImmune[BuffID.StardustMinionBleed] = true;
            player.buffImmune[BuffID.Slimed] = true;
            player.buffImmune[BuffID.GelBalloonBuff] = true;
            player.buffImmune[BuffID.Wet] = true;
            player.buffImmune[BuffID.Lovestruck] = true;
            player.buffImmune[BuffID.Stinky] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Webbed] = true;
            player.buffImmune[BuffID.BrainOfConfusionBuff] = true;
            player.buffImmune[BuffID.Tipsy] = true;
            player.buffImmune[BuffID.WaterCandle] = true;
            player.buffImmune[BuffID.ChaosState] = true;
            player.buffImmune[BuffID.PotionSickness] = true;
            player.buffImmune[BuffID.NoBuilding] = true;
            player.buffImmune[BuffID.WindPushed] = true;
            player.buffImmune[BuffID.MoonLeech] = true;
            player.buffImmune[BuffID.TheTongue] = true;
            player.buffImmune[BuffID.Shimmer] = true;
            player.buffImmune[BuffID.ShadowCandle] = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AnkhShield, 1);
            recipe.AddIngredient<GodsPaintThinner>(1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}