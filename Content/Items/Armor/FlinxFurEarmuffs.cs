using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Items.Armor;

	[AutoloadEquip(EquipType.Head)]
	public class FlinxFurEarmuffs : ModItem
	{
		public override void SetDefaults()
		{
			Item.defense = 1;
			Item.rare = ItemRarityID.Blue;
		}
		public override void UpdateEquip(Player player)
    {
        player.maxTurrets += 1;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
        var line = new TooltipLine(Mod, "Face", "+1 sentry slot");
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
    public override void SetStaticDefaults(){
			ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.FlinxFur, 4);
			recipe.AddTile(TileID.Loom);
			recipe.Register();
		}
    public static readonly int AdditiveSummonDamageBonus = 8;
    public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "8% increased summon damage";
        player.GetDamage(DamageClass.Summon) += AdditiveSummonDamageBonus / 109f;
    }
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.FlinxFurCoat && legs.type == ModContent.ItemType<FlinxFurSlippers>();
		}
	}