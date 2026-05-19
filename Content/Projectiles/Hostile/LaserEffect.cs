using HendecamMod.Common.Systems.Assets;
using HendecamMod.Common.Utils;
using System.IO;
using System.Linq;

namespace HendecamMod.Content.Projectiles.Hostile;

public class LaserEffect : ModProjectile
{
    public override string Texture => HendecamTextures.Empty100TexPath;

    int AITimer = 0;
    ref float Duration => ref Projectile.ai[0];
    ref float LaserWidth => ref Projectile.ai[1];

    public Color LaserColor { get; set; } = Color.White;
    public Entity EntityToFollow { get; set; } = null;
    int entityType = -1;
    int entityID = -1;

    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(LaserColor.R);
        writer.Write(LaserColor.G);
        writer.Write(LaserColor.B);
        writer.Write(LaserColor.A);
        writer.Write(entityType);
        writer.Write(entityID);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        LaserColor = new Color(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        entityType = reader.ReadInt32();
        entityID = reader.ReadInt32();
        if (entityID < 0) return;
        switch (entityType)
        {
            case 1:
                EntityToFollow = Main.player[entityID];
                break;
            case 2:
                EntityToFollow = Main.projectile.FirstOrDefault(p => p.identity == entityID, null);
                break;
            case 3:
                EntityToFollow = Main.npc[entityID];
                break;
        }
    }

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        ProjectileID.Sets.DrawScreenCheckFluff[Type] = 6000;
        Main.projFrames[Type] = 1;
    }

    public override void SetDefaults()
    {
        Projectile.width = 8;
        Projectile.height = 8;
        Projectile.hostile = false;
        Projectile.friendly = false;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.timeLeft = 9999;
    }

    Vector2 savedVelocity = Vector2.Zero;
    public override void AI()
    {
        if (AITimer == 0)
        {
            if (EntityToFollow != null)
            {
                if (EntityToFollow is Player)
                {
                    entityType = 1;
                    entityID = EntityToFollow.whoAmI;
                }
                else if (EntityToFollow is Projectile proj)
                {
                    entityType = 2;
                    entityID = proj.identity;
                }
                else if (EntityToFollow is NPC)
                {
                    entityType = 3;
                    entityID = EntityToFollow.whoAmI;
                }
            }
            savedVelocity = Projectile.velocity;
            Projectile.netUpdate = true;
        }
        if (EntityToFollow != null)
        {
            Projectile.Center = EntityToFollow.Center;
        }
        if (LaserWidth == 0) LaserWidth = 1;
        Projectile.velocity = Vector2.Zero;
        if (AITimer > Duration) Projectile.Kill();
        AITimer++;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        if (savedVelocity == Vector2.Zero)
        {
            return false;
        }
        LemonUtils.DrawLaser(Projectile.Center, Projectile.Center + savedVelocity, LaserWidth, LaserColor);
        return false;
    }
}
