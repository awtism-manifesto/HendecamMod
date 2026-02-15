using System.Collections.Generic;
using HendecamMod.Common.Systems;
using HendecamMod.Content.NPCs.Bosses;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Localization;

namespace HendecamMod.Content.Items.Consumables;

public class ApacheElfShipSummon : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        ItemID.Sets.SortingPriorityBossSpawns[Type] = 13;
    }

    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 30;
        Item.maxStack = 1;
        Item.rare = ItemRarityID.Blue;
        Item.useAnimation = 30;
        Item.useTime = 30;
        Item.useStyle = ItemUseStyleID.HoldUp;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Show the commander of the elves that their commrades perished in vein");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Summons Alpine, pilot of the Apache Elf Ship")
        {
            OverrideColor = new Color(155, 0, 0)
        };
        tooltips.Add(line);
    }

    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossSpawners;
    }

    public override bool CanUseItem(Player player)
    {
        return !NPC.AnyNPCs(ModContent.NPCType<ApacheElfShip>());
    }

    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            SoundEngine.PlaySound(SoundID.AbigailUpgrade, player.position);
            int type = ModContent.NPCType<ApacheElfShip>();
            if (BossDownedSystem.downedApacheElfShip)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Well, I'm not one to turn down a rematch."), new Color(185, 105, 105));
            }
            else
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Target found, firing at will."), new Color(185, 105, 105));
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, type);
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
            }
        }

        return true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HallowedBar, 5);
        recipe.AddIngredient<CandyHeart>(15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        if (ModLoader.TryGetMod("SOTS", out Mod SOTSMerica) && SOTSMerica.TryFind("HelicopterParts", out ModItem HelicopterParts))
        {
            recipe.AddIngredient(HelicopterParts.Type);
        }
    }
}