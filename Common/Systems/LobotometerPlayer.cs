using HendecamMod.Common.Systems.Players;
using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Accessories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace HendecamMod.Common.Systems
{
    public class LobotometerPlayer : ModPlayer
    {
        public float Current;
        public float BaseMax = 50f;
        public float Max;

        // This is the PERMANENT bonus from consumed items (character-specific)
        public float PermanentBonus = 0f;

        // This is the TEMPORARY bonus from equipment/buffs
        public float TemporaryBonus = 0f;

        // Total MaxBonus = PermanentBonus + TemporaryBonus
        public float MaxBonus => PermanentBonus + TemporaryBonus;

        public float BaseDecayRate = 40f;
        public float DecayRateMultiplier = 1f;
        public int ticksSinceLastUse;
        public float decayPerTick = 0.66f;
        public int decayPerSecond = 40;
        public float MaxScalingFactor = 0.1f;

        public override void ResetEffects()
        {
            // Reset temporary bonuses each frame
            TemporaryBonus = 0f;
            DecayRateMultiplier = 1f;
        }

        public override void PostUpdate()
        {
            // Recalculate Max AFTER all temporary bonuses have been applied
            Max = BaseMax + MaxBonus;

            // Clamp current to the new max (in case max decreased)
            if (Current > Max)
                Current = Max;

            // Calculate decay rate based on current Max
            if (Max == 200 && DecayRateMultiplier == 1f)
            {
                decayPerTick = 0.66f;
            }
            else
            {
                float basePerTick = (BaseDecayRate * DecayRateMultiplier) / 60f;
                float maxScaling = (Max * MaxScalingFactor) / 60f;
                decayPerTick = basePerTick + maxScaling;
                decayPerSecond = (int)(decayPerTick * 60);
            }

            if (Player.dead)
            {
                Current = 0;
            }
        }

        public override void PreUpdate()
        {
            Max = BaseMax + MaxBonus;

            if (Current > Max)
                Current = Max;

            ticksSinceLastUse++;

            if (ticksSinceLastUse > 120 && Current > 0f && !Player.GetModPlayer<BaseSpike>().Spiked)
            {
                float basePerTick = (BaseDecayRate * DecayRateMultiplier) / 60f;
                float maxScaling = (Max * MaxScalingFactor) / 60f;
                decayPerTick = basePerTick + maxScaling;

                Current -= decayPerTick;
                if (Current < 0f)
                    Current = 0f;
            }
        }

        // CHARACTER-SPECIFIC SAVE DATA
        public override void SaveData(TagCompound tag)
        {
            if (PermanentBonus > 0f)
                tag["LoboPermanentBonus"] = PermanentBonus;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("LoboPermanentBonus"))
                PermanentBonus = tag.GetFloat("LoboPermanentBonus");
            else
                PermanentBonus = 0f;
        }

        public bool CanIncreasePermanent(int amount)
        {
            float maxTotalBonus = 250f;
            return PermanentBonus + amount <= maxTotalBonus;
        }

        public void IncreasePermanent(int amount)
        {
            if (CanIncreasePermanent(amount))
            {
                PermanentBonus += amount;
                Current += amount;
                if (Current > Max)
                    Current = Max;
            }
        }

        public void AddTemporaryBonus(float bonus)
        {
            TemporaryBonus += bonus;
        }

        public float LobotometerShaderStrength = 0.5f;
        public float BaseLobotometerShaderStrength = 0.5f;
        public float LocalCurrent = 0f;
        public float LocalMax = 1f;

        private static readonly string[] AllLobotomyFilters = new[]
                {
            "HendecamMod:LobotomyScreen",
            "HendecamMod:LobotomyWave",
            "HendecamMod:LobotomyInsane"
        };

        private Vector3 insaneRandomSeed = new Vector3(1f, 1f, 0f);
        private float insaneRandomProgress = 0f;
        private bool wasLobotomyActive = false;

        public override void PostUpdateMiscEffects()
        {
            if (!Main.dedServ)
            {
                if (Player != Main.LocalPlayer)
                    return;

                var clientConfig = GetInstance<HendecamClientConfig>();
                float configIntensity = clientConfig.LobotometerShaderIntensity;

                string activeFilter = clientConfig.LobotomyShaderMode switch
                {
                    LobotomyMode.Wave => "HendecamMod:LobotomyWave",
                    LobotomyMode.Insane => "HendecamMod:LobotomyInsane",
                    _ => "HendecamMod:LobotomyScreen"
                };

                // Deactivate any unselected shaders if the user switches mode in config
                foreach (string filterName in AllLobotomyFilters)
                {
                    if (filterName != activeFilter && Filters.Scene[filterName] != null && Filters.Scene[filterName].IsActive())
                    {
                        Filters.Scene.Deactivate(filterName);
                    }
                }

                // Deactivate active shader when dead, no current, or slider is at 0
                if (Current <= 0 || Player.dead || configIntensity <= 0f)
                {
                    wasLobotomyActive = false;

                    if (Filters.Scene[activeFilter] != null && Filters.Scene[activeFilter].IsActive())
                    {
                        Filters.Scene.Deactivate(activeFilter);
                    }
                    return;
                }

                // Roll a new random glass crack layout whenever the meter starts up from 0
                if (!wasLobotomyActive)
                {
                    wasLobotomyActive = true;

                    float scaleX = Main.rand.NextFloat(0.75f, 1.35f) * (Main.rand.NextBool() ? 1f : -1f);
                    float scaleY = Main.rand.NextFloat(0.75f, 1.35f) * (Main.rand.NextBool() ? 1f : -1f);
                    float offset = Main.rand.NextFloat(-0.22f, 0.22f);

                    insaneRandomSeed = new Vector3(scaleX, scaleY, offset);
                    insaneRandomProgress = Main.rand.NextFloat(0f, 1f);
                }

                // Activate the selected shader if not already active
                if (Filters.Scene[activeFilter] != null && !Filters.Scene[activeFilter].IsActive())
                {
                    Filters.Scene.Activate(activeFilter);
                }

                // Calculate intensity modifiers
                float intensityMultiplier = configIntensity;

                if (Player.GetModPlayer<HendecamPlayer>().Lucid)
                {
                    intensityMultiplier *= 0.425f;
                }

                if (Player.GetModPlayer<HendecamPlayer>().TermLucid)
                {
                    intensityMultiplier *= 0.175f;
                }

                if (Main.masterMode && Player.HasBuff(BuffID.Rabies))
                {
                    intensityMultiplier *= 1.225f;
                }

                LobotometerShaderStrength = BaseLobotometerShaderStrength * intensityMultiplier;

                if (Main.zenithWorld)
                {
                    LobotometerShaderStrength *= 1.8f;
                }

                if (Filters.Scene[activeFilter] != null)
                {
                    ScreenShaderData shader = Filters.Scene[activeFilter].GetShader();
                    shader.UseIntensity((Current / Max) * LobotometerShaderStrength);

                    if (clientConfig.LobotomyShaderMode == LobotomyMode.Insane)
                    {
                        shader.UseColor(insaneRandomSeed);
                        shader.UseProgress(insaneRandomProgress);
                    }
                }
            }
        }

        public void AddLobotometer(float amount = 5f)
        {
            Current += amount;
            if (Current > Max)
                Current = Max;

            ticksSinceLastUse = 0;
        }
    }

    [Autoload(Side = ModSide.Client)]
    public class LobotometerUISystem : ModSystem
    {
        private Texture2D frame;
        private bool textureLoaded = false;

        public override void Load()
        {
            try
            {
                frame = Request<Texture2D>("HendecamMod/Content/Effects/Lobotometer", AssetRequestMode.ImmediateLoad).Value;

                if (frame != null && !frame.IsDisposed)
                {
                    textureLoaded = true;
                }

                Asset<Effect> lobotomyScreenShader = Request<Effect>("HendecamMod/Assets/Shaders/Screen/LobotomyScreen", AssetRequestMode.ImmediateLoad);
                Asset<Effect> lobotomyWaveShader = Request<Effect>("HendecamMod/Assets/Shaders/Screen/LobotomyWave", AssetRequestMode.ImmediateLoad);

                Filters.Scene["HendecamMod:LobotomyScreen"] = new Filter(new ScreenShaderData(lobotomyScreenShader, "LobotomyScreen"), EffectPriority.Medium);
                Filters.Scene["HendecamMod:LobotomyWave"] = new Filter(new ScreenShaderData(lobotomyWaveShader, "LobotomyWave"), EffectPriority.Medium);
            }
            catch (Exception ex)
            {
                Mod.Logger.Error($"Error loading Lobotometer assets: {ex.Message}");
            }
        }

        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            if (!textureLoaded) return;

            Player player = Main.LocalPlayer;
            if (player == null || !player.active) return;

            var lobo = player.GetModPlayer<LobotometerPlayer>();
            if (lobo == null || lobo.Current <= 0f) return;

            try
            {
                Vector2 uiPos = (player.Center - Main.screenPosition) / Main.UIScale;
                uiPos += new Vector2(0f, 61f);

                Rectangle screenRect = new Rectangle(0, 0, Main.screenWidth, Main.screenHeight);
                if (!screenRect.Contains((int)uiPos.X, (int)uiPos.Y))
                    return;

                Vector2 origin = new Vector2(frame.Width / 2f, frame.Height / 2f);

                spriteBatch.Draw(frame, uiPos, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);

                int border = 4;
                int fillWidth = frame.Width - (border * 6);
                int fillHeight = frame.Height - (border * 2);

                float fillPercent = MathHelper.Clamp(lobo.Current / lobo.Max, 0f, 1f);
                int currentWidth = (int)(fillWidth * fillPercent * 0.9f);

                if (currentWidth > 0)
                {
                    Rectangle fillRect = new Rectangle(
                        (int)(uiPos.X - origin.X + border + 16),
                        (int)(uiPos.Y - origin.Y + border),
                        currentWidth,
                        fillHeight
                    );

                    spriteBatch.Draw(
                        TextureAssets.MagicPixel.Value,
                        fillRect,
                        new Color(250, 140, 80) * 0.5f
                    );
                }
            }
            catch (Exception e)
            {
                Mod.Logger.Error(e);
            }
        }
    }
}