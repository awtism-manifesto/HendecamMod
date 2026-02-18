using System.Collections.Generic;
using HendecamMod.Content.NPCs.Bosses;
using HendecamMod.Content.Tiles.Furniture;
using Terraria.Audio;

namespace HendecamMod.Content.Items.Consumables;

public class EyesOfArchangelsSummon : ModItem
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
        var line = new TooltipLine(Mod, "Face", "Laugh in the face of religion and curse out the lord by name");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "Summons the Eyes of the Archangels, the everwatching eyes of god himself")
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
        return !NPC.AnyNPCs(ModContent.NPCType<EyesOfRaphael>()) || !NPC.AnyNPCs(ModContent.NPCType<EyesOfMichael>()) || !NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>());
        }

    public override bool? UseItem(Player player)
        {
        if (player.whoAmI == Main.myPlayer)
            {
            SoundEngine.PlaySound(SoundID.AbigailUpgrade, player.position);
            int type = ModContent.NPCType<EyesOfGabriel>();
            if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                if (!NPC.AnyNPCs(ModContent.NPCType<EyesOfMichael>()))
                    {
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<EyesOfMichael>());
                    }
                if (!NPC.AnyNPCs(ModContent.NPCType<EyesOfGabriel>()))
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<EyesOfGabriel>());
                    {
                    }
                if (!NPC.AnyNPCs(ModContent.NPCType<EyesOfRaphael>()))
                    {
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<EyesOfRaphael>());
                    }
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
        recipe.AddIngredient(ItemID.LunarBar, 15);
        recipe.AddIngredient<FissionDrive>(1);
        recipe.AddTile<CultistCyclotronPlaced>();
        recipe.Register();
        }
    }