using HendecamMod.Content.Buffs;
using HendecamMod.Content.Items.Materials;
using HendecamMod.Content.Items.Weapons.Summon;
using HendecamMod.Content.Projectiles;
using HendecamMod.Content.Projectiles.Items;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;

namespace HendecamMod.Content.Items.Weapons.Melee;

public class KusariOfFlamingFundo : ModItem
{
    
    private static readonly int[] unwantedPrefixes = new[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Shameful, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

    public override void SetStaticDefaults()
    {
        // These are all related to gamepad controls and don't seem to affect anything else
        ItemID.Sets.Yoyo[Item.type] = true; // Used to increase the gamepad range when using Strings.
        ItemID.Sets.GamepadExtraRange[Item.type] = 11; // Increases the gamepad range. Some vanilla values: 4 (Wood), 10 (Valor), 13 (Yelets), 18 (The Eye of Cthulhu), 21 (Terrarian).
        ItemID.Sets.GamepadSmartQuickReach[Item.type] = true; // Unused, but weapons that require aiming on the screen are in this set.
    }

    public override void SetDefaults()
    {
        Item.width = 24; // The width of the item's hitbox.
        Item.height = 24; // The height of the item's hitbox.

        Item.useStyle = ItemUseStyleID.Shoot; // The way the item is used (e.g. swinging, throwing, etc.)
        Item.useTime = 25; // All vanilla yoyos have a useTime of 25.
        Item.useAnimation = 25; // All vanilla yoyos have a useAnimation of 25.
        Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
        Item.noUseGraphic = true; // Makes the item invisible while using it (the projectile is the visible part).
        Item.UseSound = SoundID.Item1; // The sound that will play when the item is used.

        Item.damage = 115; 
        Item.DamageType = DamageClass.MeleeNoSpeed; 
        Item.knockBack = 6.67f; // The amount of knockback the item inflicts.

        Item.channel = true; // Set to true for items that require the attack button to be held out (e.g. yoyos and magic missile weapons)
        Item.rare = ItemRarityID.Yellow; // The item's rarity. This changes the color of the item's name.
        Item.value = Item.sellPrice(gold: 22, silver: 25);

        Item.shoot = ProjectileType<KusariProj>(); // Which projectile this item will shoot. We set this to our corresponding projectile.
        Item.shootSpeed = 16f; // The velocity of the shot projectile.			
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var line = new TooltipLine(Mod, "Face", "Right click before throwing out the weapon to start having a really fun time... for 15 seconds");
        tooltips.Add(line);

        line = new TooltipLine(Mod, "Face", "'COWABUNGA!!!'")
        {
            OverrideColor = new Color(255, 255, 255)
        };
        tooltips.Add(line);
    }
    public override void HoldItem(Player player)
    {
        var CDPlayer = player.GetModPlayer<FunCD>();
       
          
        foreach (Projectile proj in Main.ActiveProjectiles)
        {
            if (proj.owner == player.whoAmI && proj.type == ModContent.ProjectileType<KusariProj>() && CDPlayer.FunTimeActive == true)
            {
                player.endurance = 1f - 0.75f * (1f - player.endurance);

            }
        }
    }
    public override bool AltFunctionUse(Player player)
    {
        bool TurboOrCD = true;

        for (int i = 0; i < Player.MaxBuffs; i++)
        {
            int buffType = player.buffType[i];

            if (buffType == BuffType<FunTimeCooldown>())
            {
                TurboOrCD = false;
            }
        }

        if (!TurboOrCD)
        {

            CombatText.NewText(player.Hitbox, Color.Red, "On Cooldown!");

        }

        return TurboOrCD;
    }
    public override bool AllowPrefix(int pre)
    {
        // return false to make the game reroll the prefix.

        // DON'T DO THIS BY ITSELF:
        // return false;
        // This will get the game stuck because it will try to reroll every time. Instead, make it have a chance to return true.

        if (Array.IndexOf(unwantedPrefixes, pre) > -1)
        {
            // IndexOf returns a positive index of the element you search for. If not found, it's less than 0.
            // Here we check if the selected prefix is positive (it was found).
            // If so, we found a prefix that we don't want. Reroll.
            return false;
        }

        // Don't reroll
        return true;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {

        var CDPlayer = player.GetModPlayer<FunCD>();
        if (player.altFunctionUse == 2)
        {
            





            player.AddBuff(BuffType<FunTimeCooldown>(), 2400);
            CDPlayer.FunTimeCooldown = 2400;

            SoundEngine.PlaySound(new SoundStyle($"{nameof(HendecamMod)}/Assets/Sounds/JetEngine")
            {
                Volume = 1f,
                PitchVariance = 0f,
                MaxInstances = 1,
            });
            CDPlayer.FunTimeActive = true;

            return false;
        }
        else
        {

           

            return true;
        }
    }
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.HelFire);
        recipe.AddIngredient(ItemID.Ectoplasm, 15);
        recipe.AddIngredient<FireDiamond>(50);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();

       
    }
}
public class FunCD : ModPlayer
{
    public bool FunTimeActive { get; set; } = false;
    public int FunTimeCooldown { get; set; } = 0;

    public override void ResetEffects()
    {
        FunTimeCooldown--;
        if (FunTimeCooldown < 1499)
        {
            FunTimeActive = false;
        }


    }
}