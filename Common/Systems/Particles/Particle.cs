namespace HendecamMod.Common.Systems.Particles;

public struct Particle
{
    public const byte DATA_LENGTH = 4;
    /// <summary>
    /// The ParticleID of the particle.
    /// </summary>
    public int type = -1;

    /// <summary>
    /// Whether the particle is active or not. Inactive particles are not updated and contain junk data.
    /// </summary>
    public bool active;

    /// <summary>
    /// If true, the particle will be killed during the next update.
    /// </summary>
    public bool shouldDie;

    /// <summary>
    /// Timer that increases by 1 every update while the particle is active.
    /// </summary>
    public int timer;

    /// <summary>
    /// World position of the particle.
    /// </summary>
    public Vector2 position;

    /// <summary>
    /// Velocity that is added to the particle's position every update;
    /// </summary>
    public Vector2 velocity;

    /// <summary>
    /// Particle's rotation in radians.
    /// </summary>
    public float rotation;

    /// <summary>
    /// Particle's scale. To scale the sprite differently along different axes, you'll need to manually draw it.
    /// </summary>
    public float scale;

    /// <summary>
    /// The color the particle is drawn with. If you only need to modify the opacity, use the opacity field instead.
    /// </summary>
    public Color color;

    /// <summary>
    /// The opacity of the sprite, 0f is invisible, 1f is fully visible.
    /// </summary>
    public float opacity;

    /// <summary>
    /// Rectangle that depicts which frame to draw from a spritesheet.<br></br>
    /// If null, the whole texture will be drawn (useful if the texture only has 1 sprite).<br></br>
    /// Use ParticleType.Texture.Frame() to easily get a rectangle from a spritesheet.
    /// </summary>
    public Rectangle? frame;

    /// <summary>
    /// Contains 4 floats that can be used for various purposes depending on particle type.
    /// </summary>
    public float[] data;

    // You probably don't need to touch this.
    public Particle()
    {
        type = -1;
        active = false;
        shouldDie = false;
        timer = 0;
        position = Vector2.Zero;
        velocity = Vector2.Zero;
        rotation = 0f;
        scale = 1f;
        color = Color.White;
        opacity = 1f;
        data = new float[DATA_LENGTH];
    }
}
