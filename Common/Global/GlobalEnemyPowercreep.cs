using System;
using System.Linq;
using System.Collections.Generic;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using HendecamMod.Content.NPCs.Bosses;
using HendecamMod.Common.Systems;


namespace HendecamMod.Common.Global
{
    public class GlobalEnemyPowercreep : GlobalNPC // MASSIVE shoutout to the Fargo's devs, this code was adapted from Fargo's Mutant boss and enemy health config
    {
        internal static int[] Bosses = [ // Here just in case we want to change modifiers for bosses specifically
            NPCID.KingSlime,
            NPCID.EyeofCthulhu,
            
            NPCID.BrainofCthulhu,
            NPCID.QueenBee,
            NPCID.SkeletronHead,
            NPCID.QueenSlimeBoss,
            NPCID.TheDestroyer,
            NPCID.SkeletronPrime,
            NPCID.Retinazer,
            NPCID.Spazmatism,
            NPCID.Plantera,
            NPCID.Golem,
            NPCID.DukeFishron,
            NPCID.HallowBoss,
            NPCID.CultistBoss,
            NPCID.MoonLordCore,
            NPCID.MartianSaucerCore,
            NPCID.Pumpking,
            NPCID.IceQueen,
            NPCID.DD2Betsy,
            NPCID.DD2OgreT3,
            NPCID.IceGolem,
            NPCID.SandElemental,
            NPCID.Paladin,
            NPCID.Everscream,
            NPCID.MourningWood,
            NPCID.SantaNK1,
            NPCID.HeadlessHorseman,
            NPCID.PirateShip,
            ModContent.NPCType<ApacheElfShip>(),
            ModContent.NPCType<HeadOfCthulhu>(),
            ModContent.NPCType<PromethiumPlasmoid>(),
        ];
        public bool FirstFrame = true;
        public override bool InstancePerEntity => true;
        public override bool PreAI(NPC npc)
        {
            if (FirstFrame)
            {
                FirstFrame = false;
                float lifeFraction = npc.GetLifePercent();
                if (!Main.expertMode)
                {
                    if (NPC.downedBoss2 && !Main.hardMode && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.05f);

                    }

                    if (Main.hardMode && !NPC.downedPlantBoss && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.11f);

                    }
                    if (!NPC.downedMoonlord && NPC.downedPlantBoss && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.22f);

                    }
                    if (NPC.downedMoonlord && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.35f);

                    }
                }
                if (Main.expertMode)
                {
                    if (NPC.downedBoss2 && !Main.hardMode && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.1f);

                    }

                    if (Main.hardMode && !NPC.downedPlantBoss && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.2f);

                    }
                    if (!NPC.downedMoonlord && NPC.downedPlantBoss && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.33f);

                    }
                    if (NPC.downedMoonlord && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5f);

                    }
                }
                if (Main.masterMode) 
                {
                    if (NPC.downedBoss2 && !Main.hardMode && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.15f);

                    }

                    if (Main.hardMode && !NPC.downedPlantBoss && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.25f);

                    }
                    if (!NPC.downedMoonlord && NPC.downedPlantBoss && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.4f);

                    }
                    if (NPC.downedMoonlord && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.66f);

                    }
                }
                if (Main.getGoodWorld)
                {
                    if (NPC.downedBoss2 && !Main.hardMode && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.2f);

                    }

                    if (Main.hardMode && !NPC.downedPlantBoss && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.33f);

                    }
                    if (!NPC.downedMoonlord && NPC.downedPlantBoss && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.5f);

                    }
                    if (NPC.downedMoonlord && !npc.townNPC && !npc.CountsAsACritter && npc.life > 10)
                    {



                        npc.lifeMax = (int)Math.Round(npc.lifeMax * 1.75f);

                    }
                }
                npc.life = (int)Math.Round(npc.lifeMax * lifeFraction);
            }
            


            return true;




        }
    }
}