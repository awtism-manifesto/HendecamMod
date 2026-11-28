using HendecamMod.Content.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Content.Projectiles;

public class BabyOilMonster : ModProjectile
{
    private ref float State => ref Projectile.ai[0];
    private ref float SlashTimer => ref Projectile.ai[1];
    private ref float WalkAttemptTimer => ref Projectile.localAI[0];

    private int airTicks;

    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 15;
        Main.projPet[Projectile.type] = true;

        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.netImportant = true;
        Projectile.width = 26;
        Projectile.height = 36;
        Projectile.timeLeft = 18000;
        Projectile.friendly = true;
        Projectile.ignoreWater = true;
        Projectile.minion = true;
        Projectile.minionSlots = 1f;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.penetrate = -1;
        Projectile.tileCollide = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 20;
    }

    public override bool? CanCutTiles() => false;

    public override bool MinionContactDamage() => true;

    public override bool OnTileCollide(Vector2 oldVelocity) => false;

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        SlashTimer = 16f;

        if (Main.rand.NextBool(5))
        {
            target.AddBuff(BuffID.Oiled, 240);
        }

        if (Projectile.owner == Main.myPlayer && target.HasBuff(BuffID.Oiled))
        {
            Vector2 spawnPos = Projectile.Center - new Vector2(Main.rand.Next(-1, 2), 2);
            Vector2 dir = spawnPos.DirectionTo(Projectile.Center);
            if (dir == Vector2.Zero)
                dir = Vector2.UnitY;

            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                spawnPos,
                dir,
                ModContent.ProjectileType<OilBoomSmall>(),
                (int)(Projectile.damage * 1.25f),
                Projectile.knockBack,
                Projectile.owner
            );
        }
    }

    public override void AI()
    {
        Player owner = Main.player[Projectile.owner];
        if (!CheckActive(owner))
            return;

        if (SlashTimer > 0f)
            SlashTimer--;

        if (Vector2.Distance(Projectile.Center, owner.Center) > 2000f)
        {
            Projectile.Center = owner.Center;
            Projectile.velocity = Vector2.Zero;
            State = 0f;
            WalkAttemptTimer = 0f;
            Projectile.netUpdate = true;
        }

        SeparateFromOtherMinions();

        bool onGround;
        if (State == 1f)
        {
            onGround = false;
            FlyToOwner(owner);
        }
        else
        {
            onGround = GroundBehavior(owner);
        }

        if (onGround)
            airTicks = 0;
        else
            airTicks++;

        UpdateAnimation(onGround);
    }

    private void FlyToOwner(Player owner)
    {
        Projectile.tileCollide = false;
        WalkAttemptTimer = 0f;

        Vector2 idlePos = owner.Center + new Vector2(-owner.direction * (44f + Projectile.minionPos * 26f), -42f);
        Vector2 toIdle = idlePos - Projectile.Center;
        float dist = toIdle.Length();

        float speed = MathF.Max(12f, owner.velocity.Length() + 4f);
        if (dist > 20f)
        {
            toIdle.Normalize();
            Projectile.velocity = (Projectile.velocity * 19f + toIdle * speed) / 20f;
        }
        else
        {
            Projectile.velocity *= 0.9f;
        }

        if (MathF.Abs(Projectile.velocity.X) > 0.2f)
            Projectile.direction = Projectile.velocity.X > 0f ? 1 : -1;
        else
            Projectile.direction = owner.direction;

        Projectile.spriteDirection = Projectile.direction;
        Projectile.rotation = Projectile.velocity.X * 0.04f;

        bool nearOwnerAbove = dist < 95f || (MathF.Abs(Projectile.Center.X - owner.Center.X) < 110f && Projectile.Bottom.Y <= owner.Bottom.Y + 8f && Projectile.Center.Y >= owner.Center.Y - 120f);
        bool canDrop = nearOwnerAbove
            && owner.velocity.Y == 0f
            && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height);

        if (canDrop)
        {
            State = 0f;
            Projectile.tileCollide = true;
            Projectile.rotation = 0f;
            Projectile.velocity.X *= 0.4f;
            if (Projectile.velocity.Y < 0.5f)
                Projectile.velocity.Y = 0.5f;
            airTicks = 10;
            Projectile.netUpdate = true;
        }
    }

    private bool GroundBehavior(Player owner)
    {
        Projectile.tileCollide = true;
        Projectile.rotation = 0f;

        bool onGround = Projectile.velocity.Y == 0f;

        NPC target = FindTarget(owner);

        float distToOwner = Vector2.Distance(Projectile.Center, owner.Center);
        float verticalToOwner = MathF.Abs(Projectile.Center.Y - owner.Center.Y);

        if (target != null)
        {
            WalkAttemptTimer = 0f;
            if (distToOwner > 900f)
            {
                State = 1f;
                Projectile.netUpdate = true;
                return false;
            }
        }
        else
        {
            bool tooFar = distToOwner > 500f || (owner.velocity.Y == 0f && verticalToOwner > 260f);
            if (tooFar)
            {
                WalkAttemptTimer++;
                if (distToOwner > 750f || WalkAttemptTimer >= 45f)
                {
                    State = 1f;
                    WalkAttemptTimer = 0f;
                    Projectile.netUpdate = true;
                    return false;
                }
            }
            else
            {
                WalkAttemptTimer = MathF.Max(0f, WalkAttemptTimer - 2f);
            }
        }

        if (target != null)
        {
            float dx = target.Center.X - Projectile.Center.X;
            float dy = target.Center.Y - Projectile.Center.Y;
            float distToTarget = Vector2.Distance(Projectile.Center, target.Center);

            if (distToTarget < 78f)
                SlashTimer = MathF.Max(SlashTimer, 14f);

            int moveDir = dx >= 0f ? 1 : -1;
            Projectile.direction = moveDir;

            if (MathF.Abs(dx) > 12f)
                Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X + moveDir * 0.4f, -6.5f, 6.5f);
            else
                Projectile.velocity.X *= 0.85f;

            if (onGround)
            {
                bool blockedAhead = IsWallAhead(moveDir);
                bool targetAbove = target.Bottom.Y < Projectile.Top.Y - 12f && MathF.Abs(dx) < 180f;
                bool gapAhead = IsGapAhead(moveDir) && dy < 40f;

                if (blockedAhead || targetAbove || gapAhead)
                {
                    Projectile.velocity.Y = dy < -120f ? -9.8f : -7.8f;
                    onGround = false;
                }
            }
        }
        else
        {
            float idleX = owner.Center.X - owner.direction * (48f + Projectile.minionPos * 30f);
            float dx = idleX - Projectile.Center.X;

            if (MathF.Abs(dx) > 24f)
            {
                int moveDir = dx >= 0f ? 1 : -1;
                Projectile.direction = moveDir;

                float maxSpeed = MathF.Abs(dx) > 160f ? 6.5f : 4.5f;
                Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X + moveDir * 0.35f, -maxSpeed, maxSpeed);

                if (onGround && (IsWallAhead(moveDir) || (IsGapAhead(moveDir) && owner.Bottom.Y <= Projectile.Bottom.Y + 32f)))
                {
                    Projectile.velocity.Y = -8.2f;
                    onGround = false;
                }
            }
            else
            {
                Projectile.velocity.X *= 0.8f;
                if (MathF.Abs(Projectile.velocity.X) < 0.1f)
                    Projectile.velocity.X = 0f;

                Projectile.direction = owner.direction;
            }
        }

        Projectile.spriteDirection = Projectile.direction;

        Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY);

        Projectile.velocity.Y += 0.4f;
        if (Projectile.velocity.Y > 10f)
            Projectile.velocity.Y = 10f;

        if (StandOnPlatform(owner, target))
            onGround = true;

        return onGround;
    }

    private bool StandOnPlatform(Player owner, NPC target)
    {
        if (Projectile.velocity.Y < 0f)
            return false;

        float desiredBottomY = target != null ? target.Bottom.Y : owner.Bottom.Y;
        if (desiredBottomY > Projectile.Bottom.Y + 16f)
            return false;

        int startX = (int)(Projectile.Left.X / 16f);
        int endX = (int)(Projectile.Right.X / 16f);
        int footY = (int)((Projectile.Bottom.Y + Projectile.velocity.Y) / 16f);

        for (int x = startX; x <= endX; x++)
        {
            if (!WorldGen.InWorld(x, footY, 1))
                continue;

            Tile tile = Main.tile[x, footY];
            if (tile.HasUnactuatedTile && Main.tileSolidTop[tile.TileType] && !Main.tileSolid[tile.TileType])
            {
                float tileTopY = footY * 16f;
                if (Projectile.Bottom.Y <= tileTopY + 8f && Projectile.Bottom.Y + Projectile.velocity.Y >= tileTopY)
                {
                    Projectile.position.Y = tileTopY - Projectile.height;
                    Projectile.velocity.Y = 0f;
                    return true;
                }
            }
        }

        return false;
    }

    private void UpdateAnimation(bool onGround)
    {
        if (State == 1f)
        {
            if (Projectile.frame < 10 || Projectile.frame > 13)
                Projectile.frame = 10;

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = Projectile.frame >= 13 ? 10 : Projectile.frame + 1;
            }
            return;
        }

        bool moving = MathF.Abs(Projectile.velocity.X) > 0.25f;

        if (SlashTimer > 0f)
        {
            int start = moving ? 7 : 4;
            int end = moving ? 9 : 6;

            if (Projectile.frame < start || Projectile.frame > end)
                Projectile.frame = start;

            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = Projectile.frame >= end ? start : Projectile.frame + 1;
            }
            return;
        }

        if (!onGround && (airTicks >= 2 || Projectile.velocity.Y > 0.9f || Projectile.velocity.Y < -0.5f))
        {
            Projectile.frame = 14;
            Projectile.frameCounter = 0;
            return;
        }

        if (moving)
        {
            if (Projectile.frame > 3)
                Projectile.frame = 0;

            int walkDelay = MathF.Abs(Projectile.velocity.X) > 3.5f ? 4 : 6;
            if (++Projectile.frameCounter >= walkDelay)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 4;
            }
        }
        else
        {
            Projectile.frame = 0;
            Projectile.frameCounter = 0;
        }
    }

    private NPC FindTarget(Player owner)
    {
        if (owner.HasMinionAttackTargetNPC)
        {
            NPC forced = Main.npc[owner.MinionAttackTargetNPC];
            if (forced.CanBeChasedBy(this) && Vector2.Distance(owner.Center, forced.Center) < 900f)
                return forced;
        }

        NPC best = null;
        float bestDist = 700f;

        for (int i = 0; i < Main.maxNPCs; i++)
        {
            NPC npc = Main.npc[i];
            if (!npc.CanBeChasedBy(this))
                continue;

            float distToOwner = Vector2.Distance(owner.Center, npc.Center);
            if (distToOwner > 1000f)
                continue;

            float dist = Vector2.Distance(Projectile.Center, npc.Center);
            if (dist < bestDist && (dist < 200f || Collision.CanHitLine(Projectile.Center, 1, 1, npc.Center, 1, 1)))
            {
                bestDist = dist;
                best = npc;
            }
        }

        return best;
    }

    private void SeparateFromOtherMinions()
    {
        for (int i = 0; i < Main.maxProjectiles; i++)
        {
            Projectile other = Main.projectile[i];
            if (i == Projectile.whoAmI || !other.active || other.owner != Projectile.owner || other.type != Projectile.type)
                continue;

            if (MathF.Abs(Projectile.Center.X - other.Center.X) < 20f && MathF.Abs(Projectile.Center.Y - other.Center.Y) < 20f)
            {
                Projectile.velocity.X += Projectile.Center.X < other.Center.X ? -0.15f : 0.15f;
            }
        }
    }

    private bool IsWallAhead(int dir)
    {
        Vector2 checkPos = dir > 0
            ? new Vector2(Projectile.Right.X, Projectile.Top.Y + 2f)
            : new Vector2(Projectile.Left.X - 8f, Projectile.Top.Y + 2f);

        return Collision.SolidCollision(checkPos, 8, Projectile.height - 20);
    }

    private bool IsGapAhead(int dir)
    {
        Vector2 footCheck = new(Projectile.Center.X + dir * (Projectile.width * 0.5f + 8f) - 4f, Projectile.Bottom.Y + 2f);
        return Collision.TileCollision(footCheck, new Vector2(0f, 24f), 8, 8, fallThrough: false).Y >= 24f;
    }

    private bool CheckActive(Player owner)
    {
        if (owner.dead || !owner.active)
        {
            owner.ClearBuff(ModContent.BuffType<OilMonsterBuff>());
            Projectile.Kill();
            return false;
        }

        if (owner.HasBuff(ModContent.BuffType<OilMonsterBuff>()))
        {
            Projectile.timeLeft = 2;
            return true;
        }

        Projectile.Kill();
        return false;
    }
}