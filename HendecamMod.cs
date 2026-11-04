global using Microsoft.Xna.Framework;
global using System;
global using Terraria;
global using Terraria.ID;
global using Terraria.ModLoader;
global using static Terraria.ModLoader.ModContent;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Networking.Packets;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.IO;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace HendecamMod;

// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
public class HendecamMod : Mod
{
    public static HendecamMod Instance;
    public HendecamMod() => Instance = this;
    public const string BlankTexture = "HendecamMod/Assets/Textures/Misc/BlankTexture";

    public override void Load()
    {
        if (!Main.dedServ)
        {
            Asset<Effect> lobotomyScreenShader = Request<Effect>("HendecamMod/Assets/Shaders/Screen/LobotomyScreen", AssetRequestMode.ImmediateLoad);
            Asset<Effect> lobotomyWaveShader = Request<Effect>("HendecamMod/Assets/Shaders/Screen/LobotomyWave", AssetRequestMode.ImmediateLoad);
            Asset<Effect> lobotomyInsaneShader = Request<Effect>("HendecamMod/Assets/Shaders/Screen/LobotomyInsane", AssetRequestMode.ImmediateLoad);

            Filters.Scene["HendecamMod:LobotomyScreen"] = new Filter(new ScreenShaderData(lobotomyScreenShader, "LobotomyScreen"), EffectPriority.Medium);
            Filters.Scene["HendecamMod:LobotomyWave"] = new Filter(new ScreenShaderData(lobotomyWaveShader, "LobotomyWave"), EffectPriority.Medium);
            Filters.Scene["HendecamMod:LobotomyInsane"] = new Filter(new ScreenShaderData(lobotomyInsaneShader, "LobotomyInsane"), EffectPriority.Medium);
        }

    }
    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {

        CardGameManager.Instance.HandlePacket(reader, whoAmI);
    }
    void LoadFilterShader(string name, string path, EffectPriority priority)
    {
        Asset<Effect> filter = Assets.Request<Effect>(path);
        // Prefixing shader with "ModName:" as convention
        Filters.Scene[$"HendecamMod:{name}"] = new Filter(new ScreenShaderData(filter, name), priority);
    }

   
}