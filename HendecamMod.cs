global using Microsoft.Xna.Framework;
global using System;
global using Terraria;
global using Terraria.ID;
global using Terraria.ModLoader;

namespace HendecamMod;

// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
public class HendecamMod : Mod
{
    public NeoParacosm()
    {
        MusicSkipsVolumeRemap = true;
    }
}