using HendecamMod.Content.Items.Accessories.VapeDyes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static HendecamMod.Content.Items.Accessories.VapeDyes.Red40VapeDye;

namespace HendecamMod.Content.Global;

public class VapeMark : GlobalProjectile
{
    public bool VapeProj;
    public Color MixedDyeColor = Color.White; // Default color
    private bool hasInitializedColor = false;
    public float DustScale = 1.5f; 

    public override bool InstancePerEntity => true;

    public override void AI(Projectile projectile)
    {
        if (!VapeProj) return;

        // Initialize color once when projectile spawns
        if (!hasInitializedColor)
        {
            MixedDyeColor = GetPlayerDyeColor(Main.player[projectile.owner]);
            hasInitializedColor = true;
        }

        // Spawn colored dust each tick
        SpawnVapeDust(projectile);
    }

    private Color GetPlayerDyeColor(Player player)
    {
        var dyePlayer = player.GetModPlayer<VapeDyePlayer>();
        var activeDyes = dyePlayer.GetActiveDyes(); 

        if (activeDyes.Count == 0)
            return Color.White;

        if (activeDyes.Count == 1)
            return activeDyes[0].Color;

        return MixColors(activeDyes);
    }

    private Color MixColors(List<VapeDye> dyes, bool additive = true)
    {
        if (dyes.Count == 0) return Color.White;
        if (dyes.Count == 1) return dyes[0].Color;

        if (additive)
        {
            // Weighted additive mixing
            Vector3 total = Vector3.Zero;
            float totalWeight = 0f;

            foreach (var dye in dyes)
            {
                float weight = dye.MixWeight;
                total.X += (dye.Color.R / 255f) * weight;
                total.Y += (dye.Color.G / 255f) * weight;
                total.Z += (dye.Color.B / 255f) * weight;
                totalWeight += weight;
            }

            // Normalize by total weight
            return new Color(
                MathHelper.Clamp(total.X / totalWeight, 0, 1),
                MathHelper.Clamp(total.Y / totalWeight, 0, 1),
                MathHelper.Clamp(total.Z / totalWeight, 0, 1)
            );
        }
        else
        {
            // Weighted subtractive mixing (more complex - using weighted averages for CMY)
            float totalCyan = 0f, totalMagenta = 0f, totalYellow = 0f;
            float totalWeight = 0f;

            foreach (var dye in dyes)
            {
                float weight = dye.MixWeight;

                // Convert RGB to CMY
                float c = 1f - (dye.Color.R / 255f);
                float m = 1f - (dye.Color.G / 255f);
                float y = 1f - (dye.Color.B / 255f);

                // Weighted accumulation
                totalCyan += c * weight;
                totalMagenta += m * weight;
                totalYellow += y * weight;
                totalWeight += weight;
            }

            // Weighted average
            float avgCyan = totalCyan / totalWeight;
            float avgMagenta = totalMagenta / totalWeight;
            float avgYellow = totalYellow / totalWeight;

            // Convert back to RGB
            return new Color(
                (int)((1f - avgCyan) * 255),
                (int)((1f - avgMagenta) * 255),
                (int)((1f - avgYellow) * 255)
            );
        }
    }
    private void SpawnVapeDust(Projectile projectile)
    {
        // Your existing dust spawning logic, but using MixedDyeColor
        for (int i = 0; i < 2; i++) // Spawn multiple dust for better effect
        {
            float posOffsetX = Main.rand.NextFloat(-5f, 5f);
            float posOffsetY = Main.rand.NextFloat(-5f, 5f);

            Dust fire2Dust = Dust.NewDustDirect(
                new Vector2(
                    projectile.position.X + 1f + posOffsetX,
                    projectile.position.Y + 1f + posOffsetY
                ) - projectile.velocity * 0.1f,
                projectile.width - 10,
                projectile.height - 10,
                DustID.Smoke,
                0f, 0f, 166,
                MixedDyeColor,
                DustScale
                
            );

            fire2Dust.fadeIn = 0.2f + Main.rand.Next(4) * 0.1f;
            fire2Dust.noGravity = true;
            fire2Dust.velocity *= 1.33f;
        }
    }
}