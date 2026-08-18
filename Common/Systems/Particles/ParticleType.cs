using HendecamMod.Common.Systems.Particles;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;

namespace HendecamMod.Common.Systems.Particles;

public abstract class ParticleType : ILoadable
{
    /// <summary>
    /// The particle's texture.
    /// </summary>
    public Asset<Texture2D> Texture { get; protected set; }

    /// <summary>
    /// Kills the particle.
    /// </summary>
    /// <param name="particle"></param>
    public void Kill(ref Particle particle)
    {
        particle.shouldDie = true;
    }

    /// <summary>
    /// Draws the particle.
    /// Override this if you want custom drawing.
    /// </summary>
    /// <param name="particle"></param>
    public virtual void Draw(Particle particle)
    {
        Main.spriteBatch.Draw(
            Texture.Value,
            particle.position - Main.screenPosition,
            particle.frame,
            particle.color * particle.opacity,
            particle.rotation,
            particle.frame == null ? Texture.Size() * 0.5f : particle.frame.Value.Size() * 0.5f,
            particle.scale,
            SpriteEffects.None,
            0
            );
    }

    /// <summary>
    /// Called when a particle is spawned.
    /// </summary>
    /// <param name="particle"></param>
    public virtual void OnSpawn(ref Particle particle)
    {

    }

    /// <summary>
    /// Called after dusts have been updated.
    /// </summary>
    /// <param name="particle"></param>
    public virtual void Update(ref Particle particle)
    {

    }

    public void Load(Mod mod)
    {
        string path = $"{GetType().Namespace}/{GetType().Name}".Replace(".", "/");
        Texture = Request<Texture2D>(path);
    }

    public void Unload()
    {
        //Texture.Dispose();
    }
}
