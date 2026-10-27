/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

Console.Write("Mod folder path: ");
string modRoot = (args.FirstOrDefault() ?? Console.ReadLine() ?? "").Trim('"', ' ');

string accDir = Path.Combine(modRoot, "Content", "Items", "Accessories");
string accPlayerPath = Directory.GetFiles(modRoot, "AccessoryEffectsPlayer.cs", SearchOption.AllDirectories)[0];
string mainPlayerPath = Directory.GetFiles(modRoot, "HendecamPlayer.cs", SearchOption.AllDirectories)[0];
string updatePlayerPath = Directory.GetFiles(modRoot, "UpdatePlayer.cs", SearchOption.AllDirectories)[0];

var accessories = new Dictionary<string, AccData>();
var usedBoolNames = new HashSet<string>();
var extraUsings = new HashSet<string>
{
    "using static Terraria.ModLoader.ModContent;"
};

foreach (string file in Directory.GetFiles(accDir, "*.cs", SearchOption.AllDirectories))
{
    string code = File.ReadAllText(file);
    Match classMatch = Regex.Match(code, @"class\s+(\w+)\s*:\s*ModItem");
    Match methodMatch = Regex.Match(code, @"public\s+override\s+void\s+UpdateAccessory\s*\(\s*Player\s+(\w+)\s*,\s*bool\s+\w+\s*\)");
    if (!classMatch.Success || !methodMatch.Success)
        continue;

    string className = classMatch.Groups[1].Value;
    string playerParam = methodMatch.Groups[1].Value;

    int openBrace = code.IndexOf('{', methodMatch.Index);
    int closeBrace = FindClosingBrace(code, openBrace);
    if (openBrace == -1 || closeBrace == -1)
        continue;

    foreach (Match u in Regex.Matches(code, @"(?m)^using\s+[^\r\n]+;"))
        extraUsings.Add(u.Value.Trim());

    string rawBody = code.Substring(openBrace + 1, closeBrace - openBrace - 1);
    string convertedBody = Regex.Replace(rawBody, $@"\b{Regex.Escape(playerParam)}\b", "Player")
                                .Replace("hideVisual", "false");

    var ingredients = Regex.Matches(code, @"AddIngredient\s*(?:<\s*(\w+)\s*>|\(\s*(?:ModContent\.)?ItemType\s*<\s*(\w+)\s*>\s*\(\s*\))")
        .Select(m => m.Groups[1].Success ? m.Groups[1].Value : m.Groups[2].Value)
        .Distinct()
        .ToList();

    accessories[className] = new AccData
    {
        ClassName = className,
        BoolName = ToTwoWordBool(className, usedBoolNames),
        FilePath = file,
        FullCode = code,
        MethodOpenBrace = openBrace,
        MethodCloseBrace = closeBrace,
        Statements = SplitStatements(convertedBody),
        RawIngredients = ingredients
    };
}

foreach (var acc in accessories.Values)
{
    acc.Children = acc.RawIngredients.Where(accessories.ContainsKey).ToList();
}

// Strip duplicate statements from parent accessories that already exist in their crafting tree descendants
foreach (var acc in accessories.Values)
{
    var descendants = GetAllDescendants(acc.ClassName, accessories);
    if (descendants.Count == 0)
        continue;

    var descendantStatements = descendants
        .SelectMany(d => accessories[d].Statements)
        .Select(Normalize)
        .ToHashSet();

    acc.Statements.RemoveAll(s =>
        descendantStatements.Contains(Normalize(s)) ||
        descendants.Any(d => s.Contains($".{accessories[d].BoolName} = true")));
}

// Update each accessory .cs file to set its HendecamPlayer bool
foreach (var acc in accessories.Values)
{
    string newBody = $"\n        player.GetModPlayer<HendecamPlayer>().{acc.BoolName} = true;\n    ";
    string updatedCode = acc.FullCode.Remove(acc.MethodOpenBrace + 1, acc.MethodCloseBrace - acc.MethodOpenBrace - 1)
                                     .Insert(acc.MethodOpenBrace + 1, newBody);

    if (!updatedCode.Contains("using HendecamMod.Common.Systems.Players;"))
        updatedCode = "using HendecamMod.Common.Systems.Players;\n" + updatedCode;

    File.WriteAllText(acc.FilePath, updatedCode);
}

var sortedForCascade = TopologicalSortParentsFirst(accessories);

var sb = new StringBuilder();
foreach (string u in extraUsings.OrderBy(x => x))
    sb.AppendLine(u);

sb.AppendLine("\nnamespace HendecamMod.Common.Systems.Players;\n");
sb.AppendLine("public partial class HendecamPlayer");
sb.AppendLine("{");

foreach (var acc in accessories.Values.OrderBy(a => a.BoolName))
    sb.AppendLine($"    public bool {acc.BoolName};");

sb.AppendLine("\n    public void ResetAccessoryEffects()");
sb.AppendLine("    {");
foreach (var acc in accessories.Values.OrderBy(a => a.BoolName))
    sb.AppendLine($"        {acc.BoolName} = false;");
sb.AppendLine("    }");

sb.AppendLine("\n    public void UpdateAccessoryEffects()");
sb.AppendLine("    {");

foreach (var acc in sortedForCascade.Where(a => a.Children.Count > 0))
{
    sb.AppendLine($"        if ({acc.BoolName})");
    sb.AppendLine("        {");
    foreach (string child in acc.Children)
        sb.AppendLine($"            {accessories[child].BoolName} = true;");
    sb.AppendLine("        }");
}

foreach (var acc in accessories.Values.OrderBy(a => a.BoolName))
{
    if (acc.Statements.Count == 0)
        continue;

    sb.AppendLine($"        if ({acc.BoolName})");
    sb.AppendLine("        {");
    foreach (string stmt in acc.Statements)
        sb.AppendLine($"            {stmt}");
    sb.AppendLine("        }");
}

sb.AppendLine("    }");
sb.AppendLine("}");

File.WriteAllText(accPlayerPath, sb.ToString());

string mainPlayerCode = File.ReadAllText(mainPlayerPath);
if (!mainPlayerCode.Contains("ResetAccessoryEffects();"))
{
    mainPlayerCode = Regex.Replace(
        mainPlayerCode,
        @"(public\s+override\s+void\s+ResetEffects\s*\(\s*\)\s*\{)",
        "$1\n        ResetAccessoryEffects();"
    );
    File.WriteAllText(mainPlayerPath, mainPlayerCode);
}

string updatePlayerCode = File.ReadAllText(updatePlayerPath);
if (!updatePlayerCode.Contains("UpdateAccessoryEffects();"))
{
    updatePlayerCode = Regex.Replace(
        updatePlayerCode,
        @"(public\s+override\s+void\s+UpdateEquips\s*\(\s*\)\s*\{)",
        "$1\n            UpdateAccessoryEffects();"
    );
    File.WriteAllText(updatePlayerPath, updatePlayerCode);
}

Console.WriteLine($"\nMigrated {accessories.Count} accessories into AccessoryEffectsPlayer.cs!");

static string ToTwoWordBool(string name, HashSet<string> used)
{
    if (name.EndsWith("Accessory") && name.Length > "Accessory".Length)
        name = name[..^"Accessory".Length];
    if (name.StartsWith("The") && name.Length > 3 && char.IsUpper(name[3]))
        name = name[3..];

    var stopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Of", "And", "In", "Among", "A", "To" };

    var words = Regex.Matches(name, @"[A-Z]+(?=[A-Z][a-z]|\d|$)|[A-Z][a-z0-9]*")
        .Select(m => m.Value)
        .Where(w => !stopWords.Contains(w))
        .ToList();

    if (words.Count == 0)
        words.Add(name);

    string FormatWord(string w, bool first) =>
        first ? w.ToLowerInvariant() : char.ToUpperInvariant(w[0]) + w[1..].ToLowerInvariant();

    string candidate;
    if (words.Count == 1)
    {
        candidate = FormatWord(words[0], true);
    }
    else
    {
        // Default 2-word rule: First word + Last word (e.g., CarbonDioxideBalloon -> carbonBalloon)
        candidate = FormatWord(words[0], true) + FormatWord(words[^1], false);

        // Fallback if First + Last is already taken: First word + Second word
        if (used.Contains(candidate) && words.Count > 2)
            candidate = FormatWord(words[0], true) + FormatWord(words[1], false);
    }

    string finalName = candidate;
    int counter = 2;
    while (!used.Add(finalName))
        finalName = candidate + counter++;

    return finalName;
}

static int FindClosingBrace(string text, int openIndex)
{
    if (openIndex == -1) return -1;
    int depth = 0;
    for (int i = openIndex; i < text.Length; i++)
    {
        if (text[i] == '{') depth++;
        else if (text[i] == '}' && --depth == 0) return i;
    }
    return -1;
}

static List<string> SplitStatements(string body)
{
    body = Regex.Replace(body, @"//[^\r\n]*", "");
    var list = new List<string>();
    var current = new StringBuilder();
    int depth = 0;

    foreach (char c in body)
    {
        current.Append(c);
        if (c == '{') depth++;
        else if (c == '}')
        {
            depth--;
            if (depth == 0)
            {
                string s = current.ToString().Trim();
                if (!string.IsNullOrWhiteSpace(s)) list.Add(s);
                current.Clear();
            }
        }
        else if (c == ';' && depth == 0)
        {
            string s = current.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(s)) list.Add(s);
            current.Clear();
        }
    }
    return list;
}

static string Normalize(string s) => Regex.Replace(s, @"\s+", "");

static HashSet<string> GetAllDescendants(string className, Dictionary<string, AccData> map)
{
    var result = new HashSet<string>();
    var queue = new Queue<string>(map[className].Children);
    while (queue.Count > 0)
    {
        string curr = queue.Dequeue();
        if (result.Add(curr) && map.TryGetValue(curr, out var childData))
        {
            foreach (string grandChild in childData.Children)
                queue.Enqueue(grandChild);
        }
    }
    return result;
}

static List<AccData> TopologicalSortParentsFirst(Dictionary<string, AccData> map)
{
    var visited = new HashSet<string>();
    var order = new List<AccData>();

    void Visit(string name)
    {
        if (!visited.Add(name) || !map.TryGetValue(name, out var data))
            return;
        foreach (string child in data.Children)
            Visit(child);
        order.Add(data);
    }

    foreach (string key in map.Keys)
        Visit(key);

    order.Reverse();
    return order;
}

class AccData
{
    public string ClassName = "";
    public string BoolName = "";
    public string FilePath = "";
    public string FullCode = "";
    public int MethodOpenBrace;
    public int MethodCloseBrace;
    public List<string> Statements = new();
    public List<string> RawIngredients = new();
    public List<string> Children = new();
}*/