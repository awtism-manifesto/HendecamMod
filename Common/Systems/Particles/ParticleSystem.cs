using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using HendecamMod.Common.Systems.Particles;

namespace HendecamMod.Common.Systems.Particles;

public class ParticleSystem : ModSystem
{
    public const int MAX_PARTICLES = 50000;
    /// <summary>
    /// Contains all particles in the world, active or inactive.<br></br>
    /// Active particles are always at the beginning of the array.
    /// </summary>
    public static Particle[] Particles { get; private set; } = new Particle[MAX_PARTICLES];

    /// <summary>
    /// Keeps track of how many active particles there are.
    /// </summary>
    public static int ActiveParticleCount { get; private set; } = 0;

    /// <summary>
    /// Maps ParticleID values to ParticleType instances.
    /// </summary>
    public static List<ParticleType> TypesByID { get; private set; } = new List<ParticleType>();

    /// <summary>
    /// Index of the next particle to be replaced in case of the cap being reached.
    /// </summary>
    static int ReplacementIndex = 0;

    public override void Load()
    {
        On_Main.DrawDust += On_Main_DrawDust;
    }

    private void On_Main_DrawDust(On_Main.orig_DrawDust orig, Main self)
    {
        orig(self);
        DrawParticles();
    }

    public override void SetStaticDefaults()
    {
        InitializeTypesByID();
        InitializeParticles();
    }

    public override void ClearWorld()
    {
        ActiveParticleCount = 0;
        InitializeParticles();
    }

    /// <summary>
    /// Adds the ParticleType to the list of particle types.
    /// </summary>
    /// <param name="typeInstance"></param>
    /// <returns>The new ParticleID of the added particle type.</returns>
    public static int RegisterParticle(ParticleType typeInstance)
    {
        int particleID = TypesByID.Count;
        TypesByID.Add(typeInstance);
        return particleID;
    }

    /// <summary>
    /// Registers all particle types and calls load on them (for example, to load textures).
    /// </summary>
    public void InitializeTypesByID()
    {
        ParticleID.RegisterParticles();

        for (int i = 0; i < ParticleID.Count; i++)
        {
            TypesByID[i].Load(Mod);
        }
    }

    /// <summary>
    /// Populates the particles list with empty particles.
    /// </summary>
    public static void InitializeParticles()
    {
        for (int i = 0; i < MAX_PARTICLES; i++)
        {
            Particles[i] = new Particle();
        }
    }

    public static void BeginDefaultParticleSpriteBatch()
    {
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.Default, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
    }

    /// <summary>
    /// Draws all active particles.<br></br>
    /// See ParticleSystem.BeginDefaultParticleSpriteBatch() to view spriteBatch state.
    /// </summary>
    public static void DrawParticles()
    {
        BeginDefaultParticleSpriteBatch();
        for (int i = 0; i < ActiveParticleCount; i++)
        {
            TypesByID[Particles[i].type].Draw(Particles[i]);
        }
        Main.spriteBatch.End();
    }

    public override void PostUpdateDusts()
    {
        UpdateParticles();
    }

    /// <summary>
    /// Calls ParticleType.Update() on all active particles,
    /// adjusts their position by their velocity,
    /// increases their timers and kills particles that shouldDie.
    /// </summary>
    public static void UpdateParticles()
    {
        for (int i = 0; i < ActiveParticleCount; i++)
        {
            ref Particle particle = ref Particles[i];
            TypesByID[particle.type].Update(ref particle);
            particle.position += particle.velocity;
            particle.timer++;
            if (particle.shouldDie)
            {
                KillParticle(i);
                i--;
            }
        }
    }
    /// <summary>
    /// Spawns a new particle into the world. Returns a reference to the newly spawned particle.
    /// </summary>
    /// <param name="type">The ParticleID of the particle.</param>
    /// <param name="position">World position of the particle.</param>
    /// <param name="velocity"></param>
    /// <param name="color">Color to draw the particle in. Default is Color.White.<br></br>
    /// Change the opacity param if you only want to change visibility.</param>
    /// <param name="opacity">Opacity of the particle.</param>
    /// <param name="scale"></param>
    /// <returns>A reference to the newly spawned particle.</returns>
    public static ref Particle SpawnParticle(int type, Vector2 position, Vector2 velocity, Color color = default, float opacity = 1f, float scale = 1f)
    {
        if (color == default)
        {
            color = Color.White;
        }

        // Replacing "old" particles
        if (ActiveParticleCount >= MAX_PARTICLES)
        {
            ref Particle particle1 = ref Particles[ReplacementIndex];
            particle1.active = true;
            particle1.type = type;
            particle1.position = position;
            particle1.velocity = velocity;
            particle1.color = color;
            particle1.opacity = opacity;
            particle1.scale = scale;
            particle1.shouldDie = false;
            particle1.timer = 0;
            TypesByID[particle1.type].OnSpawn(ref particle1);
            ReplacementIndex++;
            if (ReplacementIndex >= MAX_PARTICLES)
            {
                ReplacementIndex = 0;
            }
            return ref particle1;
        }
        ReplacementIndex = 0;

        ref Particle particle = ref Particles[ActiveParticleCount];
        particle.active = true;
        particle.type = type;
        particle.position = position;
        particle.velocity = velocity;
        particle.color = color;
        particle.opacity = opacity;
        particle.scale = scale;
        particle.shouldDie = false;
        particle.timer = 0;
        TypesByID[particle.type].OnSpawn(ref particle);
        ActiveParticleCount++;
        return ref particle;
    }

    /// <summary>
    /// Kills the particle at index. 
    /// Switches the spots of that particle and the last active particle to make sure all active particles are next to each other in the array.
    /// </summary>
    /// <param name="index"></param>
    public static void KillParticle(int index)
    {
        if (ActiveParticleCount == 1)
        {
            Particles[index].active = false;
        }
        else
        {
            int lastActiveParticleIDX = ActiveParticleCount - 1;
            Particles[index] = Particles[lastActiveParticleIDX];
            Particles[lastActiveParticleIDX].active = false;
        }
        ActiveParticleCount--;
    }
}
