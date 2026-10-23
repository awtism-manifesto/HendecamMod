using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace HendecamMod.Content.Global
{
    public class LawnmowerGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        private static Player activeMowingPlayer;

        private bool isFlattening;
        private int flattenTimer;
        private int frozenFrameY;
        private int flattenDirection = 1;

        private static int DropDuration = 8;
        private static int ShrinkDuration = 18;
        private int TotalDuration = DropDuration + ShrinkDuration;

        private static readonly HashSet<int> ExtraZombieIDs = new()
        {
            NPCID.MaggotZombie,
            NPCID.ArmedTorchZombie,
            NPCID.BloodZombie,
            NPCID.ZombieMerman,
            NPCID.Eyezor,
            NPCID.ZombieElf,
            NPCID.ZombieElfBeard,
            NPCID.ZombieElfGirl
        };

        public override void Load()
        {
            On_Player.MowTheLawn += OnMowTheLawn;
        }

        public override void Unload()
        {
            On_Player.MowTheLawn -= OnMowTheLawn;
            activeMowingPlayer = null;
        }

        private static void OnMowTheLawn(On_Player.orig_MowTheLawn orig, Player self)
        {
            activeMowingPlayer = self;
            try
            {
                orig(self);
            }
            finally
            {
                activeMowingPlayer = null;
            }
        }

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return lateInstantiation && IsZombieVariant(entity);
        }

        private static bool IsZombieId(int npcType)
        {
            return npcType > 0 &&
                   ((npcType < NPCID.Sets.Zombies.Length && NPCID.Sets.Zombies[npcType]) ||
                    ExtraZombieIDs.Contains(npcType));
        }

        private static bool IsZombieVariant(NPC npc)
        {
            if (IsZombieId(npc.type))
                return true;

            if (npc.ModNPC is { } modNpc && (IsZombieId(modNpc.AIType) || IsZombieId(modNpc.AnimationType)))
                return true;

            int zombieBannerItem = Item.NPCtoBanner(NPCID.Zombie);
            return zombieBannerItem > 0 && Item.NPCtoBanner(npc.BannerID()) == zombieBannerItem;
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (activeMowingPlayer is null)
                return;

            modifiers.SourceDamage *= 10f;
            modifiers.Knockback *= 0f;

            if (!isFlattening)
            {
                isFlattening = true;
                flattenTimer = 0;
                frozenFrameY = npc.frame.Y;
                flattenDirection = activeMowingPlayer.direction != 0 ? activeMowingPlayer.direction : 1;
                npc.netUpdate = true;
            }
        }
       
        public override bool CheckDead(NPC npc)
        {
            if (isFlattening && flattenTimer < TotalDuration)
            {
                npc.life = 1;
                npc.dontTakeDamage = true;
                return false;
            }

            return true;
        }

        public override bool PreAI(NPC npc)
        {
            if (!isFlattening)
                return true;

            flattenTimer++;
            npc.dontTakeDamage = true;
            npc.velocity.X = 0f;
            npc.noGravity = false;
            npc.noTileCollide = false;

            if (flattenTimer >= TotalDuration)
            {
                npc.dontTakeDamage = false;
                npc.life = 0;
                npc.HitEffect(0, 10.0);
                npc.checkDead();
            }

            return false;
        }

        public override void FindFrame(NPC npc, int frameHeight)
        {
            if (isFlattening)
            {
                npc.frame.Y = frozenFrameY;
                npc.frameCounter = 0;
            }
        }

        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
        {
            return !isFlattening;
        }

        public override bool CanHitNPC(NPC npc, NPC target)
        {
            return !isFlattening;
        }

        public override bool? DrawHealthBar(NPC npc, byte bhPosition, ref float scale, ref Vector2 position)
        {
            return isFlattening ? false : null;
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (!isFlattening)
                return true;

            Texture2D texture = TextureAssets.Npc[npc.type].Value;
            Rectangle sourceRect = npc.frame;

            float dropProgress = Utils.GetLerpValue(0f, DropDuration, flattenTimer, clamped: true);
            float rotation = flattenDirection * MathHelper.PiOver2 * dropProgress;

            float shrinkProgress = Utils.GetLerpValue(DropDuration, TotalDuration, flattenTimer, clamped: true);
            float verticalScale = MathHelper.Lerp(1f, 0.1f, shrinkProgress);
            float horizontalScale = MathHelper.Lerp(1f, 0.35f, dropProgress);
            Vector2 scale = new Vector2(horizontalScale, verticalScale) * npc.scale;

            Vector2 origin = sourceRect.Size() * 0.5f;
            float groundOffset = MathHelper.Lerp(0f, (npc.height * 0.5f) - 4f, dropProgress);
            Vector2 drawPos = npc.Center - screenPos + new Vector2(0f, groundOffset + npc.gfxOffY);

            SpriteEffects effects = npc.spriteDirection == -1
                ? SpriteEffects.None
                : SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(
                texture,
                drawPos,
                sourceRect,
                npc.GetAlpha(drawColor),
                rotation,
                origin,
                scale,
                effects,
                0f
            );

            return false;
        }

        public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            bitWriter.WriteBit(isFlattening);
            if (isFlattening)
            {
                binaryWriter.Write((byte)flattenTimer);
                binaryWriter.Write((short)frozenFrameY);
                binaryWriter.Write((sbyte)flattenDirection);
            }
        }

        public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
        {
            isFlattening = bitReader.ReadBit();
            if (isFlattening)
            {
                flattenTimer = binaryReader.ReadByte();
                frozenFrameY = binaryReader.ReadInt16();
                flattenDirection = binaryReader.ReadSByte();
            }
        }
    }
}