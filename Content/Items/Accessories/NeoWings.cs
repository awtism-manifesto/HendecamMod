using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Wings)]
public class NeoWings : ModItem
{
    public override void SetStaticDefaults()
    {
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(155, 8.33f, 1.55f);
        ItemID.Sets.ShimmerTransformToItem[Type] = ItemType<TheSpamCannon>();
    }

    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 20;
        Item.value = 950000;
        Item.rare = ItemRarityID.Yellow;
        Item.accessory = true;
        Item.defense = 5;
    }
   
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
       

        var line = new TooltipLine(Mod, "Face", "Allows flight and slow fall");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "WITNESS ME THIS TIME")
        {
            OverrideColor = new Color(Main.rand.Next(155, 255), Main.rand.Next(155, 255), Main.rand.Next(155, 255))
        };
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "CUT THESE [SILLY STRINGS]")
        {
            OverrideColor = new Color(Main.rand.Next(155, 255), Main.rand.Next(155, 255), Main.rand.Next(155, 255))
        };
        tooltips.Add(line);
    }

    public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
        ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
        ascentWhenFalling = 1.025f; 
        ascentWhenRising = 0.215f; 
        maxCanAscendMultiplier = 1.125f;
        maxAscentMultiplier = 3.25f;
        constantAscend = 0.145f;
    }
}

public class NeoWingsPlayer : ModPlayer
{
    public bool HasNeoWings;

    public override void ResetEffects()
    {
        HasNeoWings = false;
    }

    public override void UpdateEquips()
    {
       
        for (int i = 3; i < 8 + Player.extraAccessorySlots; i++)
        {
            Item item = Player.armor[i];
            if (item != null && !item.IsAir && item.type == ModContent.ItemType<NeoWings>())
            {
                HasNeoWings = true;
                break;
            }
        }
    }
}


public class NeoWingsStringLayer : PlayerDrawLayer
{
    private Asset<Texture2D> _neoPixel;

    public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.Wings);

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
        Player player = drawInfo.drawPlayer;
        if (player.whoAmI != Main.myPlayer)
            return false;

        var modPlayer = player.GetModPlayer<NeoWingsPlayer>();
        return modPlayer.HasNeoWings && !player.dead;
    }

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        if (drawInfo.shadow != 0f) return;
        Player player = drawInfo.drawPlayer;
        var modPlayer = player.GetModPlayer<NeoWingsPlayer>();
        if (!modPlayer.HasNeoWings) return;
        _neoPixel ??= ModContent.Request<Texture2D>("HendecamMod/Assets/Textures/Misc/NeoPixel");
        if (_neoPixel is not { IsLoaded: true, Value: Texture2D texture }) return;

        int dir = player.direction; // 1 = right, -1 = left

        // Fine-tune this by eye in-game for left-facing only.
        // Positive shifts right, negative shifts left, in screen pixels.
        const float leftFacingCorrectionX = 18f;

        Vector2 wingAnchor = player.position + new Vector2(5f * dir, 2f) - Main.screenPosition;
        float correction = dir == -1 ? leftFacingCorrectionX : 0f;
        float topOfScreenY = 0f;

        float[] stringXOffsets = { -22f, -33f, -28f, - 17f, -11f,-6f, 0f, 6f, 11f, 16f, 22f };

        foreach (float xOffset in stringXOffsets)
        {
            float screenX = wingAnchor.X + xOffset * dir + correction;
            float distanceToTop = wingAnchor.Y - topOfScreenY;
            if (distanceToTop <= 0) continue;

            float widthScale = 1f / texture.Width;
            float heightScale = distanceToTop / texture.Height;

            drawInfo.DrawDataCache.Add(new DrawData(
                texture,
                new Vector2(screenX, topOfScreenY),
                null,
                Color.White,
                0f,
                Vector2.Zero,
                new Vector2(widthScale, heightScale),
                SpriteEffects.None,
                0
            ));
        }
    }
}