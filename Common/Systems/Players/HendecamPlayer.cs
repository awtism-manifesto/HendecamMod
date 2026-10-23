using HendecamMod.Common.Utils;
using HendecamMod.Networking;
using HendecamMod.Networking.Packets;
using Microsoft.Xna.Framework;
using MonoMod.Core.Platforms;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace HendecamMod.Common.Systems.Players;

public class HendecamPlayer : ModPlayer
{
    public int shakeDuration;
    public float shakeIntensityX;
    public float shakeIntensityY;

    public float recoilFront = 0f;
    public float recoilBack = 0f;

    public Vector2 MousePosition { get; set; }
    private Vector2 mousePosPrev;

    public void Screenshake(int dur, Vector2 power, Vector2 anchor)
    {
        if (anchor != Vector2.Zero)
        {
            power *= Terraria.Utils.GetLerpValue(2000f, 0f, anchor.Distance(Player.Center), true);
        }
        shakeDuration = dur;
        shakeIntensityX = power.X;
        shakeIntensityY = power.Y;
    }
    public override void ResetEffects()
    {
        if (shakeDuration > 0)
            shakeDuration--;
        if (recoilFront > 0f)
            recoilFront -= 0.02f;
        if (recoilBack > 0f)
            recoilBack -= 0.02f;
    }
    public override void UpdateDead()
    {
        if (shakeDuration > 0)
            shakeDuration--;
    }
    public override void UpdateEquips()
    {
    }
    public override void ProcessTriggers(TriggersSet triggersSet)
    {
    }

    public override void PostUpdateEquips()
    {
        base.PostUpdateEquips();
    }
    public override void ModifyScreenPosition()
    {
        if (shakeDuration > 0)
        {
            Main.screenPosition += Main.rand.NextVector2Circular(shakeIntensityX, shakeIntensityY);
        }
    }
    public void UpdateMouse()
    {
        if (Player.IsLocalPlayer())
        {
            mousePosPrev = MousePosition;
            MousePosition = Main.MouseWorld;
            if (Main.netMode == NetmodeID.MultiplayerClient && mousePosPrev != MousePosition)
            {
                NetSystem.SendPacket(new MousePositionPacket(Player));
            }
        }
    }
    public override void PreUpdate()
    {
        UpdateMouse();
    }
}