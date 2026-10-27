using HendecamMod.Common.Systems;
using HendecamMod.Content.Items;
using HendecamMod.Content.Items.Accessories;
using HendecamMod.Content.Items.Weapons.Ranger;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HendecamMod.Content.Items
{
    // This class showcases a "pickup". Also known as a power-up.
    // Pickup refers to items that don't enter then inventory when picked up, but rather have some other effect when obtained.
    // Pickups usually provide resources to the player, such as hearts providing life or stars providing mana. Nebula armor boosters are another example.
    // This example drops from enemies when Example Resource is low, similar to how hearts and stars only drop if the player is lacking health or mana.
    // See ExampleResourcePickupGlobalNPC for the item drop code.
    public class BrainMatter : ModItem
    {


        public override LocalizedText Tooltip => LocalizedText.Empty;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatShouldNotBeInInventory[Type] = true;
            ItemID.Sets.IgnoresEncumberingStone[Type] = true;
            ItemID.Sets.IsAPickup[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.height = 12;
            Item.width = 12;
        }
        public override bool OnPickup(Player player)
        {
            // When the item is picked up, heal the player's ExampleResource stat and spawn and sync the corresponding CombatText
            var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
            loboPlayer.Current -= 67;

            // We need to play this ourselves since we are returning false meaning it won't play automatically.
            SoundEngine.PlaySound(SoundID.Grab, player.Center);

            // We return false to prevent the item from going into the players inventory.
            return false;
        }



    }
    public class BrainMatterExists : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void OnKill(NPC npc)
        {

            Player player = Main.player[npc.lastInteraction];
            if (player != null && player.active)
            {
                var loboPlayer = player.GetModPlayer<LobotometerPlayer>();
                if (loboPlayer.Current >= 1 && Main.rand.NextBool(4))
                {
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemType<BrainMatter>());
                }
            }
        }
    }
}
