using Microsoft.Xna.Framework.Graphics;

namespace HendecamMod.Common.Systems.Particles.Renderers;

public abstract class ParticleRenderer : ModSystem
{
    public const int MAX_PARTICLES = 50000;
    public Particle[] Particles { get; private set; } = new Particle[MAX_PARTICLES];
    public int ActiveParticleCount { get; private set; }
    private int replacementIndex;

    public override void SetStaticDefaults() => InitializeParticles();

    public override void ClearWorld()
    {
        ActiveParticleCount = 0;
        InitializeParticles();
    }

    public void InitializeParticles()
    {
        for (int i = 0; i < MAX_PARTICLES; i++)
            Particles[i] = new Particle();
    }

    public static void BeginDefaultParticleSpriteBatch()
    {
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.Default, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
    }

    public virtual void DrawParticles()
    {
        BeginDefaultParticleSpriteBatch();
        for (int i = 0; i < ActiveParticleCount; i++)
            ParticleSystem.TypesByID[Particles[i].type].Draw(Particles[i]);
        Main.spriteBatch.End();
    }

    public override void PostUpdateDusts() => UpdateParticles();

    public void UpdateParticles()
    {
        for (int i = 0; i < ActiveParticleCount; i++)
        {
            ref Particle particle = ref Particles[i];
            ParticleSystem.TypesByID[particle.type].Update(ref particle);
            particle.position += particle.velocity;
            particle.timer++;
            if (particle.shouldDie)
            {
                KillParticle(i);
                i--;
            }
        }
    }

    public ref Particle SpawnParticle(int type, Vector2 position, Vector2 velocity, Color color = default, float opacity = 1f, float scale = 1f, float data0 = 0f, float data1 = 0f, float data2 = 0f, float data3 = 0f)
    {
        if (color == default)
            color = Color.White;

        if (ActiveParticleCount >= MAX_PARTICLES)
        {
            ref Particle replacement = ref Particles[replacementIndex];
            InitializeParticle(ref replacement, type, position, velocity, color, opacity, scale, data0, data1, data2, data3);
            replacementIndex = (replacementIndex + 1) % MAX_PARTICLES;
            return ref replacement;
        }

        replacementIndex = 0;
        ref Particle particle = ref Particles[ActiveParticleCount];
        InitializeParticle(ref particle, type, position, velocity, color, opacity, scale, data0, data1, data2, data3);
        ActiveParticleCount++;
        return ref particle;
    }

    private static void InitializeParticle(ref Particle particle, int type, Vector2 position, Vector2 velocity, Color color, float opacity, float scale, float data0, float data1, float data2, float data3)
    {
        particle.active = true;
        particle.type = type;
        particle.position = position;
        particle.velocity = velocity;
        particle.color = color;
        particle.opacity = opacity;
        particle.scale = scale;
        particle.shouldDie = false;
        particle.timer = 0;
        particle.frame = null;
        particle.rotation = 0f;
        particle.data = new float[Particle.DATA_LENGTH] { data0, data1, data2, data3 };
        ParticleSystem.TypesByID[type].OnSpawn(ref particle);
    }

    public void KillParticle(int index)
    {
        if (ActiveParticleCount == 1)
            Particles[index].active = false;
        else
        {
            int lastActive = ActiveParticleCount - 1;
            Particles[index] = Particles[lastActive];
            Particles[lastActive].active = false;
        }
        ActiveParticleCount--;
    }
}
