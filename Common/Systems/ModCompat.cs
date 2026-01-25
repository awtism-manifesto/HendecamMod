using System;
using System.Collections.Generic;
using HendecamMod.Content.Items.Consumables;
using HendecamMod.Content.NPCs.Bosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace HendecamMod.Common.Systems;

public class ApacheElfShipEntry : ModSystem
    {
    public override void PostSetupContent()
        {
        DoBossChecklistIntegration();
        }
    private void DoBossChecklistIntegration()
        {
        if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
            {
            return;
            }
        if (bossChecklistMod.Version < new Version(1, 6))
            {
            return;
            }
        string publicName = "ApacheElfShip";
        float weight = 13.54f;
        Func<bool> downed = () => ApacheElfShipDown.downedApacheElfShip;
        int bossType = ModContent.NPCType<ApacheElfShip>();
        int spawnItem = ModContent.ItemType<ApacheElfShipSummon>();
        List<int> collectibles = new List<int>()
            {
            //ModContent.ItemType<Content.Items.Placeable.Furniture.MinionBossRelic>(),
            //ModContent.ItemType<Content.Pets.MinionBossPet.MinionBossPetItem>(),
            //ModContent.ItemType<Content.Items.Placeable.Furniture.MinionBossTrophy>(),
            //ModContent.ItemType<Content.Items.Armor.Vanity.MinionBossMask>()
            };
        var customPortrait = (SpriteBatch sb, Rectangle rect, Color color) =>
        {
            Texture2D texture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/ApacheElfShipEntry").Value;
            Vector2 centered = new Vector2(rect.X + rect.Width / 2 - texture.Width / 2, rect.Y + rect.Height / 2 - texture.Height / 2);
            sb.Draw(texture, centered, color);
        };
        bossChecklistMod.Call(
            "LogBoss",
            Mod,
            publicName,
            weight,
            downed,
            bossType,
            new Dictionary<string, object>()
                {
                ["spawnItems"] = spawnItem,
                ["collectibles"] = collectibles,
                ["customPortrait"] = customPortrait,
                }
        );
        }
    }
public class HeadOfCthulhuEntry : ModSystem
    {
    public override void PostSetupContent()
        {
        DoBossChecklistIntegration();
        }
    private void DoBossChecklistIntegration()
        {
        if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
            {
            return;
            }
        if (bossChecklistMod.Version < new Version(1, 6))
            {
            return;
            }
        string publicName = "HeadOfCthulhu";
        float weight = 6.5f;
        Func<bool> downed = () => HeadOfCthulhuDown.downedHeadOfCthulhu;
        int bossType = ModContent.NPCType<HeadOfCthulhu>();
        int spawnItem = ModContent.ItemType<HeadOfCthulhuSummon>();
        List<int> collectibles = new List<int>()
            {
            //ModContent.ItemType<Content.Items.Placeable.Furniture.MinionBossRelic>(),
            //ModContent.ItemType<Content.Pets.MinionBossPet.MinionBossPetItem>(),
            //ModContent.ItemType<Content.Items.Placeable.Furniture.MinionBossTrophy>(),
            //ModContent.ItemType<Content.Items.Armor.Vanity.MinionBossMask>()
            };
        var customPortrait = (SpriteBatch sb, Rectangle rect, Color color) =>
        {
            Texture2D texture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/HeadOfCthulhuEntry").Value;
            Vector2 centered = new Vector2(rect.X + rect.Width / 2 - texture.Width / 2, rect.Y + rect.Height / 2 - texture.Height / 2);
            sb.Draw(texture, centered, color);
        };
        bossChecklistMod.Call(
            "LogBoss",
            Mod,
            publicName,
            weight,
            downed,
            bossType,
            new Dictionary<string, object>()
                {
                ["spawnItems"] = spawnItem,
                ["collectibles"] = collectibles,
                ["customPortrait"] = customPortrait,
                }
        );
        }
    }
public class PromethiumPlasmoidEntry : ModSystem
    {
    public override void PostSetupContent()
        {
        DoBossChecklistIntegration();
        }
    private void DoBossChecklistIntegration()
        {
        if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
            {
            return;
            }
        if (bossChecklistMod.Version < new Version(1, 6))
            {
            return;
            }
        string publicName = "PromethiumPlasmoid";
        float weight = 19f;
        Func<bool> downed = () => PromethiumPlasmoidDown.downedPromethiumPlasmoid;
        int bossType = ModContent.NPCType<PromethiumPlasmoid>();
        int spawnItem = ModContent.ItemType<AstatineGlassSword>();
        List<int> collectibles = new List<int>()
            {
            //ModContent.ItemType<Content.Items.Placeable.Furniture.MinionBossRelic>(),
            //ModContent.ItemType<Content.Pets.MinionBossPet.MinionBossPetItem>(),
            //ModContent.ItemType<Content.Items.Placeable.Furniture.MinionBossTrophy>(),
            //ModContent.ItemType<Content.Items.Armor.Vanity.MinionBossMask>()
            };
        var customPortrait = (SpriteBatch sb, Rectangle rect, Color color) =>
        {
            Texture2D texture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/PromethiumPlasmoidEntry").Value;
            Vector2 centered = new Vector2(rect.X + rect.Width / 2 - texture.Width / 2, rect.Y + rect.Height / 2 - texture.Height / 2);
            sb.Draw(texture, centered, color);
        };
        bossChecklistMod.Call(
            "LogBoss",
            Mod,
            publicName,
            weight,
            downed,
            bossType,
            new Dictionary<string, object>()
                {
                ["spawnItems"] = spawnItem,
                ["collectibles"] = collectibles,
                ["customPortrait"] = customPortrait,
                }
        );
        }
    }
