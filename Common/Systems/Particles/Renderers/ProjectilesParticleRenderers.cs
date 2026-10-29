namespace HendecamMod.Common.Systems.Particles.Renderers;

public class BeforeProjectilesParticleRenderer : ParticleRenderer
{
    public override void Load() => On_Main.DrawProjectiles += On_Main_DrawProjectiles;

    private void On_Main_DrawProjectiles(On_Main.orig_DrawProjectiles orig, Main self)
    {
        DrawParticles();
        orig(self);
    }
}

public class AfterProjectilesParticleRenderer : ParticleRenderer
{
    public override void Load() => On_Main.DrawProjectiles += On_Main_DrawProjectiles;

    private void On_Main_DrawProjectiles(On_Main.orig_DrawProjectiles orig, Main self)
    {
        orig(self);
        DrawParticles();
    }
}
