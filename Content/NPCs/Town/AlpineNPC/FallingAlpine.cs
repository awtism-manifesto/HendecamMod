using HendecamMod.Common.Systems;
using HendecamMod.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.NPCs.Town.AlpineNPC;

public class FallingAlpine : ModNPC
{
    public static readonly Asset<Texture2D> ropeTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Bosses/Dartship_Rope");
    public static readonly Asset<Texture2D> parachuteTexture = ModContent.Request<Texture2D>("HendecamMod/Content/NPCs/Town/AlpineNPC/AlpineParachute");

    public enum FallState
    {
        Falling = 0,
        Landed = 1,
        Talked = 2
    }

    public FallState State
    {
        get => (FallState)NPC.ai[0];
        set => NPC.ai[0] = (float)value;
    }

    public ref float Timer => ref NPC.ai[1];
    public ref float RetractProgress => ref NPC.ai[2];

    public VerletChain LeftCord;
    public VerletChain RightCord;
    public Vector2 CanopyPos = Vector2.Zero;
    public float CanopyRotation;
    public float CanopyScale = 1f;

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = 4;
        NPCID.Sets.NoTownNPCHappiness[Type] = true;
        NPCID.Sets.MPAllowedEnemies[Type] = true;
        NPCID.Sets.NPCBestiaryDrawModifiers hideModifier = new() { Hide = true };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, hideModifier);
    }

    public override void SetDefaults()
    {
        NPC.friendly = true;
        NPC.aiStyle = -1;
        NPC.lifeMax = 250;
        NPC.defense = 20;
        NPC.dontTakeDamage = true;
        NPC.dontTakeDamageFromHostiles = true;
        NPC.noTileCollide = false;
        NPC.noGravity = false;
        NPC.width = 30;
        NPC.height = 50;
    }

    public override bool CanChat() => State != FallState.Falling && RetractProgress >= 1f;

    public override string GetChat()
    {
        if (State == FallState.Landed)
        {
            State = FallState.Talked;
            Timer = 0f;
            NPC.netUpdate = true;
        }
        return "hook up w localization later remember this.";
    }

    public override void SetChatButtons(ref string button, ref string button2)
    {
        button = "hook up w localization";
    }

    public override void OnChatButtonClicked(bool firstButton, ref string shopName)
    {
        if (firstButton)
        {
            TransformIntoAlpine();
        }
    }

    private void TransformIntoAlpine()
    {
        CleanupCords();
        AlpineNPCRespawnSystem.unlockedAlpineSpawn = true;
        if (Main.netMode != NetmodeID.MultiplayerClient)
        {
            NPC.Transform(ModContent.NPCType<Alpine>());
        }
    }

    public override void OnSpawn(IEntitySource source)
    {
        Player player = Main.player[Player.FindClosest(NPC.Center, 3000, 3000)];
        if (NPC.Center.X < player.Center.X)
        {
            NPC.spriteDirection = -1;
            NPC.velocity = new Vector2(-8.5f, -9.5f);
        }
        else
        {
            NPC.spriteDirection = 1;
            NPC.velocity = new Vector2(8.5f, -9.5f);
        }

        State = FallState.Falling;
        Timer = 0f;
        RetractProgress = 0f;
        CanopyScale = 1f;
        CanopyPos = NPC.Center + new Vector2(0f, -44f);
    }

    private void UpdateParachuteVerlet()
    {
        if (Main.dedServ) return;

        if (State == FallState.Falling)
        {
            Vector2 targetCanopy = NPC.Center + new Vector2(-NPC.velocity.X * 2.5f, -46f);
            CanopyPos = Vector2.Lerp(CanopyPos, targetCanopy, 0.25f);
            CanopyRotation = MathHelper.Lerp(CanopyRotation, NPC.velocity.X * 0.06f, 0.15f);
            CanopyScale = 1f;
        }
        else
        {
            Vector2 retractTarget = NPC.Center + new Vector2(0f, 4f);
            CanopyPos = Vector2.Lerp(CanopyPos, retractTarget, 0.22f);
            CanopyRotation = MathHelper.Lerp(CanopyRotation, 0f, 0.2f);
            CanopyScale = MathHelper.Clamp(1f - RetractProgress, 0f, 1f);
        }

        Vector2 leftRim = CanopyPos + new Vector2(-16f * CanopyScale, 4f * CanopyScale).RotatedBy(CanopyRotation);
        Vector2 rightRim = CanopyPos + new Vector2(16f * CanopyScale, 4f * CanopyScale).RotatedBy(CanopyRotation);
        Vector2 leftHand = NPC.Center + new Vector2(-7f * CanopyScale, -2f).RotatedBy(NPC.rotation);
        Vector2 rightHand = NPC.Center + new Vector2(7f * CanopyScale, -2f).RotatedBy(NPC.rotation);

        LeftCord ??= PhysicsMethods.CreateVerletChain(6, 7f, leftRim, leftHand, pinStart: true, pinEnd: true);
        RightCord ??= PhysicsMethods.CreateVerletChain(6, 7f, rightRim, rightHand, pinStart: true, pinEnd: true);

        UpdateSingleCord(LeftCord, leftRim, leftHand);
        UpdateSingleCord(RightCord, rightRim, rightHand);
    }

    private static void UpdateSingleCord(VerletChain cord, Vector2 start, Vector2 end)
    {
        if (cord?.allSticks == null) return;

        float segLen = Math.Max(0.5f, Vector2.Distance(start, end) / cord.allSticks.Count);
        foreach (VerletStick stick in cord.allSticks)
        {
            stick.length = segLen;
            if (!stick.pointA.isVerletPinned)
            {
                Vector2 vel = (stick.pointA.pos - stick.pointA.oldPos) * 0.85f;
                stick.pointA.oldPos = stick.pointA.pos;
                stick.pointA.pos += vel + new Vector2(0f, PhysicsMethods.gravity * 0.4f);
            }
        }
        cord.Update(start, end);
    }

    private void CleanupCords()
    {
        LeftCord?.Kill();
        LeftCord = null;
        RightCord?.Kill();
        RightCord = null;
    }

    private void UpdateParachuteRetraction()
    {
        if (RetractProgress < 1f)
        {
            RetractProgress = MathHelper.Clamp(RetractProgress + 0.04f, 0f, 1f);
            UpdateParachuteVerlet();

            if (RetractProgress >= 1f)
            {
                CleanupCords();
            }
        }
    }

    public override void AI()
    {
        DrawOffsetY = -2;

        switch (State)
        {
            case FallState.Falling:
                float sway = MathF.Sin(Main.GlobalTimeWrappedHourly * 4f) * 1.6f;
                float dragLerp = (NPC.velocity.Y < 0f) ? 0.015f : 0.035f;
                NPC.velocity.X = MathHelper.Lerp(NPC.velocity.X, sway, dragLerp);

                if (NPC.velocity.Y > 2.4f)
                {
                    NPC.velocity.Y = 2.4f;
                }
                NPC.rotation = MathHelper.Clamp(NPC.velocity.X * 0.05f, -0.35f, 0.35f);

                UpdateParachuteVerlet();

                if (NPC.velocity.Y == 0f)
                {
                    SoundEngine.PlaySound(SoundID.Item10, NPC.Center);
                    NPC.velocity.X = 0f;
                    NPC.rotation = 0f;
                    State = FallState.Landed;
                    Timer = 0f;
                    RetractProgress = 0f;

                    Gore.NewGoreDirect(NPC.GetSource_FromThis(), NPC.Bottom, new Vector2(3f, 0f), Main.rand.Next(11, 14), Scale: 0.7f);
                    Gore.NewGoreDirect(NPC.GetSource_FromThis(), NPC.Bottom, new Vector2(-3f, 0f), Main.rand.Next(11, 14), Scale: 0.7f);
                    NPC.netUpdate = true;
                }
                break;

            case FallState.Landed:
                NPC.velocity.X = 0f;
                NPC.rotation = 0f;
                UpdateParachuteRetraction();

                if (RetractProgress >= 1f)
                {
                    FaceClosestPlayer(out Player player);
                    if (player != null && NPC.Distance(player.Center) >= 1600f)
                    {
                        TransformIntoAlpine();
                    }
                }
                break;

            case FallState.Talked:
                NPC.velocity.X = 0f;
                NPC.rotation = 0f;
                Timer++;
                UpdateParachuteRetraction();

                FaceClosestPlayer(out Player talkedPlayer);
                if (Timer >= 60f || (talkedPlayer != null && NPC.Distance(talkedPlayer.Center) >= 1200f))
                {
                    TransformIntoAlpine();
                }
                break;
        }
    }

    private void FaceClosestPlayer(out Player closestPlayer)
    {
        int p = Player.FindClosest(NPC.Center, 3000, 3000);
        if (p >= 0 && p < Main.maxPlayers && Main.player[p].active)
        {
            closestPlayer = Main.player[p];
            NPC.spriteDirection = (NPC.Center.X < closestPlayer.Center.X) ? 1 : -1;
        }
        else
        {
            closestPlayer = null;
        }
    }

    public override void FindFrame(int frameHeight)
    {
        NPC.frame.Y = State switch
        {
            FallState.Falling => 0,
            FallState.Landed => (RetractProgress < 1f) ? frameHeight : frameHeight * 2,
            FallState.Talked => frameHeight * 3,
            _ => 0
        };
    }

    public override void OnKill()
    {
        CleanupCords();
    }

    private void DrawCord(SpriteBatch spriteBatch, VerletChain cord, Vector2 screenPos)
    {
        if (cord?.allSticks == null) return;

        Texture2D rTex = ropeTexture.Value;
        Vector2 rOrigin = rTex.Size() / 2f;

        foreach (VerletStick stick in cord.allSticks)
        {
            Vector2 diff = stick.pointB.pos - stick.pointA.pos;
            float dist = diff.Length();
            if (dist <= 0.1f) continue;

            Vector2 center = Vector2.Lerp(stick.pointA.pos, stick.pointB.pos, 0.5f);
            float angle = diff.ToRotation() - MathHelper.PiOver2;
            Color ropeLight = Lighting.GetColor(center.ToTileCoordinates());
            Vector2 scale = new(0.75f * CanopyScale, (dist + 2f) / rTex.Height);

            spriteBatch.Draw(
                rTex,
                center - screenPos,
                null,
                ropeLight * CanopyScale,
                angle,
                rOrigin,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        if (State == FallState.Falling || RetractProgress < 1f)
        {
            DrawCord(spriteBatch, LeftCord, screenPos);
            DrawCord(spriteBatch, RightCord, screenPos);

            Texture2D canopyTex = parachuteTexture.Value;
            Vector2 canopyOrigin = canopyTex.Size() / 2f;
            spriteBatch.Draw(
                canopyTex,
                CanopyPos - screenPos,
                null,
                drawColor * CanopyScale,
                CanopyRotation,
                canopyOrigin,
                NPC.scale * CanopyScale,
                SpriteEffects.None,
                0f
            );
        }

        Texture2D npcTex = TextureAssets.Npc[Type].Value;
        SpriteEffects effects = (NPC.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        Vector2 origin = NPC.frame.Size() / 2f;

        spriteBatch.Draw(
            npcTex,
            NPC.Center - screenPos,
            NPC.frame,
            drawColor,
            NPC.rotation,
            origin,
            NPC.scale,
            effects,
            0f
        );

        return false;
    }
}