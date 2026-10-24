//use this to update localization

/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

Console.Write("Mod folder path: ");
string modRoot = (args.FirstOrDefault() ?? Console.ReadLine() ?? "").Trim('"', ' ');

string hjsonPath = Directory.GetFiles(modRoot, "*Items.hjson", SearchOption.AllDirectories)[0];
string hjson = File.ReadAllText(hjsonPath);

foreach (string file in Directory.GetFiles(modRoot, "*.cs", SearchOption.AllDirectories))
{
    string code = File.ReadAllText(file);
    if (!code.Contains("namespace HendecamMod.Content.Items"))
        continue;

    Match classMatch = Regex.Match(code, @"class\s+(\w+)\s*:\s*ModItem");
    Match methodMatch = Regex.Match(code, @"public\s+override\s+void\s+ModifyTooltips\s*\(\s*List<TooltipLine>\s+\w+\s*\)");
    if (!classMatch.Success || !methodMatch.Success)
        continue;

    string itemName = classMatch.Groups[1].Value;

    int bodyStart = code.IndexOf('{', methodMatch.Index);
    int end = bodyStart;
    int depth = 1;
    while (depth > 0 && ++end < code.Length)
    {
        if (code[end] == '{') depth++;
        else if (code[end] == '}') depth--;
    }

    string methodBody = code.Substring(bodyStart, end - bodyStart + 1);

    List<string> lines = Regex.Matches(methodBody, @"new\s+TooltipLine\s*\(\s*Mod\s*,\s*""[^""]*""\s*,\s*""([^""]*)""\s*\)")
        .Select(m => m.Groups[1].Value)
        .Where(s => !string.IsNullOrWhiteSpace(s))
        .ToList();

    code = code.Remove(methodMatch.Index, end - methodMatch.Index + 1);
    File.WriteAllText(file, code);

    if (lines.Count > 0)
    {
        string newTooltip = lines.Count == 1 && !lines[0].StartsWith("'")
            ? lines[0]
            : $"\n\t'''\n\t{string.Join("\n\t", lines)}\n\t'''";

        hjson = Regex.Replace(
            hjson,
            $@"({itemName}:\s*\{{[^}}]*?\bTooltip:\s*)""""",
            m => m.Groups[1].Value + newTooltip
        );

        Console.WriteLine($"Updated {itemName}");
    }
}

File.WriteAllText(hjsonPath, hjson);
Console.WriteLine("Done!");*/