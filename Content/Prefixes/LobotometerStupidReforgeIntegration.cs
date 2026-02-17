using HendecamMod.Common.Systems;
using System.Collections.Generic;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace HendecamMod.Content.Prefixes
{
    public class LobotometerStupidReforgeIntegration : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Check if item has the Lobotomized prefix
            if (item.prefix == ModContent.PrefixType<Lobotomized>())
            {
                // Get the actual prefix instance to access Power property
                if (PrefixLoader.GetPrefix(item.prefix) is Lobotomized loboPrefix)
                {
                    // Find where to insert the tooltips
                    int insertIndex = -1;

                    // Option 1: Look for the specific "Prefix..." tooltip lines
                    for (int i = 0; i < tooltips.Count; i++)
                    {
                        var line = tooltips[i];

                        // Vanilla prefix lines have names like "PrefixDamage", "PrefixSpeed", etc.
                        if (line.Name.StartsWith("Prefix") && line.Mod == "Terraria")
                        {
                            insertIndex = i + 1;
                        }
                    }

                    // Option 2: If we can't find vanilla prefix lines, insert before tooltips like "Material" or "SetBonus"
                    if (insertIndex == -1)
                    {
                        for (int i = 0; i < tooltips.Count; i++)
                        {
                            var line = tooltips[i];
                            if (line.Name == "Material" || line.Name == "SetBonus" ||
                                line.Name == "Consumable" || line.Name == "Quest")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Option 3: Default to near the end
                    if (insertIndex == -1)
                    {
                        insertIndex = tooltips.Count;
                        for (int i = tooltips.Count - 1; i >= 0; i--)
                        {
                            if (tooltips[i].Name == "Price" || tooltips[i].Name == "SpecialPrice")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Create tooltips with exact vanilla colors
                    // Good modifiers: new Color(120, 190, 120) - light green
                    // Bad modifiers: new Color(190, 120, 120) - light red

                    TooltipLine maxBonusLine = new TooltipLine(Mod, "PrefixLobotomyMax",
                        $"+{loboPrefix.Power * 50f:F0} max Lobotometer")
                    {
                        OverrideColor = new Color(120, 190, 120) // Light green like good prefixes
                    };

                    TooltipLine decayBonusLine = new TooltipLine(Mod, "PrefixLobotomyDecay",
                        $"+{loboPrefix.Power * 0.33f:P0} Lobotometer decay rate")
                    {
                        OverrideColor = new Color(120, 190, 120) // Orange, or use (190, 120, 120) for red
                    };

                    // Insert the tooltips
                    tooltips.Insert(insertIndex, decayBonusLine);
                    tooltips.Insert(insertIndex, maxBonusLine);
                }
            }
            if (item.prefix == ModContent.PrefixType<Serious>())
            {
                // Get the actual prefix instance to access Power property
                if (PrefixLoader.GetPrefix(item.prefix) is Serious loboPrefix)
                {
                    // Find where to insert the tooltips
                    int insertIndex = -1;

                    // Option 1: Look for the specific "Prefix..." tooltip lines
                    for (int i = 0; i < tooltips.Count; i++)
                    {
                        var line = tooltips[i];

                        // Vanilla prefix lines have names like "PrefixDamage", "PrefixSpeed", etc.
                        if (line.Name.StartsWith("Prefix") && line.Mod == "Terraria")
                        {
                            insertIndex = i + 1;
                        }
                    }

                    // Option 2: If we can't find vanilla prefix lines, insert before tooltips like "Material" or "SetBonus"
                    if (insertIndex == -1)
                    {
                        for (int i = 0; i < tooltips.Count; i++)
                        {
                            var line = tooltips[i];
                            if (line.Name == "Material" || line.Name == "SetBonus" ||
                                line.Name == "Consumable" || line.Name == "Quest")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Option 3: Default to near the end
                    if (insertIndex == -1)
                    {
                        insertIndex = tooltips.Count;
                        for (int i = tooltips.Count - 1; i >= 0; i--)
                        {
                            if (tooltips[i].Name == "Price" || tooltips[i].Name == "SpecialPrice")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Create tooltips with exact vanilla colors
                    // Good modifiers: new Color(120, 190, 120) - light green
                    // Bad modifiers: new Color(190, 120, 120) - light red

                    

                    TooltipLine decayBonusLine = new TooltipLine(Mod, "PrefixLobotomyDecay",
                        $"{loboPrefix.Power * -0.25f:P0} Lobotometer decay rate")
                    {
                        OverrideColor = new Color(190, 120, 120) // Orange, or use (190, 120, 120) for red
                    };

                    // Insert the tooltips
                    tooltips.Insert(insertIndex, decayBonusLine);
                   
                }
            }
            if (item.prefix == ModContent.PrefixType<Clunky>())
            {
                // Get the actual prefix instance to access Power property
                if (PrefixLoader.GetPrefix(item.prefix) is Clunky loboPrefix)
                {
                    // Find where to insert the tooltips
                    int insertIndex = -1;

                    // Option 1: Look for the specific "Prefix..." tooltip lines
                    for (int i = 0; i < tooltips.Count; i++)
                    {
                        var line = tooltips[i];

                        // Vanilla prefix lines have names like "PrefixDamage", "PrefixSpeed", etc.
                        if (line.Name.StartsWith("Prefix") && line.Mod == "Terraria")
                        {
                            insertIndex = i + 1;
                        }
                    }

                    // Option 2: If we can't find vanilla prefix lines, insert before tooltips like "Material" or "SetBonus"
                    if (insertIndex == -1)
                    {
                        for (int i = 0; i < tooltips.Count; i++)
                        {
                            var line = tooltips[i];
                            if (line.Name == "Material" || line.Name == "SetBonus" ||
                                line.Name == "Consumable" || line.Name == "Quest")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Option 3: Default to near the end
                    if (insertIndex == -1)
                    {
                        insertIndex = tooltips.Count;
                        for (int i = tooltips.Count - 1; i >= 0; i--)
                        {
                            if (tooltips[i].Name == "Price" || tooltips[i].Name == "SpecialPrice")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Create tooltips with exact vanilla colors
                    // Good modifiers: new Color(120, 190, 120) - light green
                    // Bad modifiers: new Color(190, 120, 120) - light red



                    TooltipLine decayBonusLine = new TooltipLine(Mod, "PrefixLobotomyDecay",
                        $"{loboPrefix.Power * -0.45f:P0} Lobotometer decay rate")
                    {
                        OverrideColor = new Color(190, 120, 120) // Orange, or use (190, 120, 120) for red
                    };

                    // Insert the tooltips
                    tooltips.Insert(insertIndex, decayBonusLine);

                }
            }
            // Check if item has the Lobotomized prefix
            if (item.prefix == ModContent.PrefixType<Sigma>())
            {
                // Get the actual prefix instance to access Power property
                if (PrefixLoader.GetPrefix(item.prefix) is Sigma loboPrefix)
                {
                    // Find where to insert the tooltips
                    int insertIndex = -1;

                    // Option 1: Look for the specific "Prefix..." tooltip lines
                    for (int i = 0; i < tooltips.Count; i++)
                    {
                        var line = tooltips[i];

                        // Vanilla prefix lines have names like "PrefixDamage", "PrefixSpeed", etc.
                        if (line.Name.StartsWith("Prefix") && line.Mod == "Terraria")
                        {
                            insertIndex = i + 1;
                        }
                    }

                    // Option 2: If we can't find vanilla prefix lines, insert before tooltips like "Material" or "SetBonus"
                    if (insertIndex == -1)
                    {
                        for (int i = 0; i < tooltips.Count; i++)
                        {
                            var line = tooltips[i];
                            if (line.Name == "Material" || line.Name == "SetBonus" ||
                                line.Name == "Consumable" || line.Name == "Quest")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Option 3: Default to near the end
                    if (insertIndex == -1)
                    {
                        insertIndex = tooltips.Count;
                        for (int i = tooltips.Count - 1; i >= 0; i--)
                        {
                            if (tooltips[i].Name == "Price" || tooltips[i].Name == "SpecialPrice")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Create tooltips with exact vanilla colors
                    // Good modifiers: new Color(120, 190, 120) - light green
                    // Bad modifiers: new Color(190, 120, 120) - light red

                    TooltipLine maxBonusLine = new TooltipLine(Mod, "PrefixLobotomyMax",
                        $"+{loboPrefix.Power * 25f:F0} max Lobotometer")
                    {
                        OverrideColor = new Color(120, 190, 120) // Light green like good prefixes
                    };

                   

                    // Insert the tooltips
                    
                    tooltips.Insert(insertIndex, maxBonusLine);
                }
            }
            if (item.prefix == ModContent.PrefixType<Brainy>())
            {
                // Get the actual prefix instance to access Power property
                if (PrefixLoader.GetPrefix(item.prefix) is Brainy loboPrefix)
                {
                    // Find where to insert the tooltips
                    int insertIndex = -1;

                    // Option 1: Look for the specific "Prefix..." tooltip lines
                    for (int i = 0; i < tooltips.Count; i++)
                    {
                        var line = tooltips[i];

                        // Vanilla prefix lines have names like "PrefixDamage", "PrefixSpeed", etc.
                        if (line.Name.StartsWith("Prefix") && line.Mod == "Terraria")
                        {
                            insertIndex = i + 1;
                        }
                    }

                    // Option 2: If we can't find vanilla prefix lines, insert before tooltips like "Material" or "SetBonus"
                    if (insertIndex == -1)
                    {
                        for (int i = 0; i < tooltips.Count; i++)
                        {
                            var line = tooltips[i];
                            if (line.Name == "Material" || line.Name == "SetBonus" ||
                                line.Name == "Consumable" || line.Name == "Quest")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Option 3: Default to near the end
                    if (insertIndex == -1)
                    {
                        insertIndex = tooltips.Count;
                        for (int i = tooltips.Count - 1; i >= 0; i--)
                        {
                            if (tooltips[i].Name == "Price" || tooltips[i].Name == "SpecialPrice")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Create tooltips with exact vanilla colors
                    // Good modifiers: new Color(120, 190, 120) - light green
                    // Bad modifiers: new Color(190, 120, 120) - light red

                    TooltipLine maxBonusLine = new TooltipLine(Mod, "PrefixLobotomyMax",
                        $"+{loboPrefix.Power * 25f:F0} max Lobotometer")
                    {
                        OverrideColor = new Color(120, 190, 120) // Light green like good prefixes
                    };



                    // Insert the tooltips

                    tooltips.Insert(insertIndex, maxBonusLine);
                }
            }
            if (item.prefix == ModContent.PrefixType<Tiktokified>())
            {
                // Get the actual prefix instance to access Power property
                if (PrefixLoader.GetPrefix(item.prefix) is Tiktokified loboPrefix)
                {
                    // Find where to insert the tooltips
                    int insertIndex = -1;

                    // Option 1: Look for the specific "Prefix..." tooltip lines
                    for (int i = 0; i < tooltips.Count; i++)
                    {
                        var line = tooltips[i];

                        // Vanilla prefix lines have names like "PrefixDamage", "PrefixSpeed", etc.
                        if (line.Name.StartsWith("Prefix") && line.Mod == "Terraria")
                        {
                            insertIndex = i + 1;
                        }
                    }

                    // Option 2: If we can't find vanilla prefix lines, insert before tooltips like "Material" or "SetBonus"
                    if (insertIndex == -1)
                    {
                        for (int i = 0; i < tooltips.Count; i++)
                        {
                            var line = tooltips[i];
                            if (line.Name == "Material" || line.Name == "SetBonus" ||
                                line.Name == "Consumable" || line.Name == "Quest")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Option 3: Default to near the end
                    if (insertIndex == -1)
                    {
                        insertIndex = tooltips.Count;
                        for (int i = tooltips.Count - 1; i >= 0; i--)
                        {
                            if (tooltips[i].Name == "Price" || tooltips[i].Name == "SpecialPrice")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Create tooltips with exact vanilla colors
                    // Good modifiers: new Color(120, 190, 120) - light green
                    // Bad modifiers: new Color(190, 120, 120) - light red

                    

                    TooltipLine decayBonusLine = new TooltipLine(Mod, "PrefixLobotomyDecay",
                        $"+{loboPrefix.Power * 0.67f:P0} Lobotometer decay rate")
                    {
                        OverrideColor = new Color(120, 190, 120) // Orange, or use (190, 120, 120) for red
                    };

                    // Insert the tooltips
                    tooltips.Insert(insertIndex, decayBonusLine);
                    
                }
            }
            if (item.prefix == ModContent.PrefixType<Brainrotted>())
            {
                // Get the actual prefix instance to access Power property
                if (PrefixLoader.GetPrefix(item.prefix) is Brainrotted loboPrefix)
                {
                    // Find where to insert the tooltips
                    int insertIndex = -1;

                    // Option 1: Look for the specific "Prefix..." tooltip lines
                    for (int i = 0; i < tooltips.Count; i++)
                    {
                        var line = tooltips[i];

                        // Vanilla prefix lines have names like "PrefixDamage", "PrefixSpeed", etc.
                        if (line.Name.StartsWith("Prefix") && line.Mod == "Terraria")
                        {
                            insertIndex = i + 1;
                        }
                    }

                    // Option 2: If we can't find vanilla prefix lines, insert before tooltips like "Material" or "SetBonus"
                    if (insertIndex == -1)
                    {
                        for (int i = 0; i < tooltips.Count; i++)
                        {
                            var line = tooltips[i];
                            if (line.Name == "Material" || line.Name == "SetBonus" ||
                                line.Name == "Consumable" || line.Name == "Quest")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Option 3: Default to near the end
                    if (insertIndex == -1)
                    {
                        insertIndex = tooltips.Count;
                        for (int i = tooltips.Count - 1; i >= 0; i--)
                        {
                            if (tooltips[i].Name == "Price" || tooltips[i].Name == "SpecialPrice")
                            {
                                insertIndex = i;
                                break;
                            }
                        }
                    }

                    // Create tooltips with exact vanilla colors
                    // Good modifiers: new Color(120, 190, 120) - light green
                    // Bad modifiers: new Color(190, 120, 120) - light red



                    TooltipLine decayBonusLine = new TooltipLine(Mod, "PrefixLobotomyDecay",
                        $"+{loboPrefix.Power * 0.5f:P0} Lobotometer decay rate")
                    {
                        OverrideColor = new Color(120, 190, 120) // Orange, or use (190, 120, 120) for red
                    };

                    // Insert the tooltips
                    tooltips.Insert(insertIndex, decayBonusLine);

                }
            }
        }

        public override void HoldItem(Item item, Player player)
        {
            if (item.prefix == ModContent.PrefixType<Lobotomized>())
            {
                var loboPlayer = player.GetModPlayer<LobotometerPlayer>();

                if (PrefixLoader.GetPrefix(item.prefix) is Lobotomized loboPrefix)
                {
                   
                    loboPlayer.MaxBonus += 50f;

                   
                    loboPlayer.DecayRateMultiplier *= 1.33f;
                }
            }
            if (item.prefix == ModContent.PrefixType<Tiktokified>())
            {
                var loboPlayer = player.GetModPlayer<LobotometerPlayer>();

                if (PrefixLoader.GetPrefix(item.prefix) is Tiktokified loboPrefix)
                {
                    

                   
                    loboPlayer.DecayRateMultiplier *= 1.67f;
                }
            }
            if (item.prefix == ModContent.PrefixType<Brainrotted>())
            {
                var loboPlayer = player.GetModPlayer<LobotometerPlayer>();

                if (PrefixLoader.GetPrefix(item.prefix) is Brainrotted loboPrefix)
                {



                    loboPlayer.DecayRateMultiplier *= 1.5f;
                }
            }
            if (item.prefix == ModContent.PrefixType<Serious>())
            {
                var loboPlayer = player.GetModPlayer<LobotometerPlayer>();

                if (PrefixLoader.GetPrefix(item.prefix) is Serious loboPrefix)
                {


                   
                    loboPlayer.DecayRateMultiplier *= 0.75f;
                }
            }
            if (item.prefix == ModContent.PrefixType<Clunky>())
            {
                var loboPlayer = player.GetModPlayer<LobotometerPlayer>();

                if (PrefixLoader.GetPrefix(item.prefix) is Clunky loboPrefix)
                {


                   
                    loboPlayer.DecayRateMultiplier *= 0.5f;
                }
            }
            if (item.prefix == ModContent.PrefixType<Sigma>())
            {
                var loboPlayer = player.GetModPlayer<LobotometerPlayer>();

                if (PrefixLoader.GetPrefix(item.prefix) is Sigma loboPrefix)
                {
                    loboPlayer.MaxBonus += 25f;

                   
                }
            }

            if (item.prefix == ModContent.PrefixType<Brainy>())
            {
                var loboPlayer = player.GetModPlayer<LobotometerPlayer>();

                if (PrefixLoader.GetPrefix(item.prefix) is Brainy loboPrefix)
                {
                    loboPlayer.MaxBonus += 100f;


                }
            }
        }
    }
}