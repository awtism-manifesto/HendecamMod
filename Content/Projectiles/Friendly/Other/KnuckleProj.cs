using HendecamMod.Content.DamageClasses;
using Terraria.Enums;
namespace HendecamMod.Content.Projectiles.Friendly.Other;
public class KnuckleProj : ModProjectile
    {
    public const int FadeInDuration = 9;
    public const int FadeOutDuration = 9;
    public const int TotalDuration = 23;
    public float CollisionWidth => 12f * Projectile.scale;
    public int Timer
        {
        get => (int)Projectile.ai[0];
        set => Projectile.ai[0] = value;
        }
    public override void SetStaticDefaults()
        {
        Main.projFrames[Projectile.type] = 4;
        }
    public override void SetDefaults()
        {
        Projectile.Size = new Vector2(25);
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.light = 0.1f;
        Projectile.tileCollide = false;
        Projectile.width = 25;
        Projectile.height = 25;
        Projectile.scale = 2f;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 6;
        Projectile.DamageType = ModContent.GetInstance<UnarmedDamage>();
        Projectile.ownerHitCheck = true;
        Projectile.extraUpdates = 1;
        Projectile.timeLeft = 360;
        Projectile.hide = true;
        Projectile.GetGlobalProjectile<UnarmedGlobal>().UnarmedWeapon = true;
        Projectile.GetGlobalProjectile<KnuckleGlobalProjectile>().KnuckleWeapon = true;
        }
    public override Color? GetAlpha(Color lightColor)
        {
        return Color.White;
        }
    public override void AI()
        {
        int frameSpeed = 6;
        Projectile.frameCounter++;
        if (Projectile.frameCounter >= frameSpeed)
            {
            Projectile.frameCounter = 0;
            Projectile.frame++;

            if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                Projectile.frame = 0;
                }
            }
        Player player = Main.player[Projectile.owner];
        Timer += 1;
        if (Timer >= TotalDuration)
            {
            Projectile.Kill();
            return;
            }
        player.heldProj = Projectile.whoAmI;
        Projectile.Opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, clamped: true) * Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, clamped: true);
        Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
        Projectile.Center = playerCenter + Projectile.velocity * (Timer - 1f);
        Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();
        if (player.direction == -1)
            {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 * Projectile.spriteDirection + 3.2f;
            }
        else
            {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 * Projectile.spriteDirection;
            }
        SetVisualOffsets();
        }
    private void SetVisualOffsets()
        {
        const int HalfSpriteWidth = 16 / 2;
        const int HalfSpriteHeight = 32 / 2;
        int HalfProjWidth = Projectile.width / 2;
        int HalfProjHeight = Projectile.height / 2;
        DrawOriginOffsetX = 0;
        DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
        DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);
        }
    public override bool ShouldUpdatePosition()
        {
        return false;
        }
    public override void CutTiles()
        {
        DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
        Vector2 start = Projectile.Center;
        Vector2 end = start + Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 10f;
        Utils.PlotTileLine(start, end, CollisionWidth, DelegateMethods.CutTiles);
        }
    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
        Player player = Main.player[Projectile.owner];
        Vector2 start = player.Center;
        if (player.direction == -1) // Dynamically change to make extreme close range hits more consistent (autism manifesto comment)
            {
            start = player.Right;
            }
        else
            {
            start = player.Left;
            }
        Vector2 end = start + Projectile.velocity * 4500f; // HEY RIVER!!!! HITBOX NOT MATCHING VISUALS???? FUCK WITH THIS NUMBER, IT'LL FIX IT!
        float collisionPoint = 0f;
        return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, CollisionWidth, ref collisionPoint);
        }
    }