global using Microsoft.Xna.Framework;
global using System;
global using Terraria;
global using Terraria.ID;
global using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace HendecamMod;

// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
public class HendecamMod : Mod
{
    public HendecamMod()
    {
        MusicSkipsVolumeRemap = true;
    }

    public override void Load()
    {
        // Loading shader
        LoadFilterShader("LobotomyScreen", "Assets/Shaders/Screen/LobotomyScreen", EffectPriority.Medium);
    }

    void LoadFilterShader(string name, string path, EffectPriority priority)
    {
        Asset<Effect> filter = Assets.Request<Effect>(path);
        // Prefixing shader with "ModName:" as convention
        Filters.Scene[$"HendecamMod:{name}"] = new Filter(new ScreenShaderData(filter, name), priority);
    }
}