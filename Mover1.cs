/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class Mover2
{
    private class ExtractedClass
    {
        public string ClassName = "";
        public string BaseType = "";
        public int StartIndex;
        public int EndIndex;
        public List<string> Fields = new List<string>();
        public List<string> FieldNames = new List<string>();
        public Dictionary<string, string> Methods = new Dictionary<string, string>();
        public int? DotMultiplier;
    }

    public static void Run(string modSourcesRoot)
    {
        string buffsDir = Path.Combine(modSourcesRoot, "Content", "Buffs");
        string globalNpcPath = Path.Combine(modSourcesRoot, "Content", "NPCs", "HendecamGlobalNPC.cs");
        string playerPath = Path.Combine(modSourcesRoot, "Common", "Systems", "Players", "HendecamPlayer.cs");
        string updatePlayerPath = Path.Combine(modSourcesRoot, "Common", "Systems", "Players", "UpdatePlayer.cs");

        if (!Directory.Exists(buffsDir) || !File.Exists(globalNpcPath) || !File.Exists(playerPath) || !File.Exists(updatePlayerPath))
        {
            Console.WriteLine("ERROR: Could not find Content/Buffs or target GlobalNPC/Player files. Make sure you are in the HendecamMod root folder.");
            return;
        }

        string parentDir = Directory.GetParent(modSourcesRoot).FullName;
        string backupDir = Path.Combine(parentDir, "HendecamMod_Backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        CopyDirectory(modSourcesRoot, backupDir);
        Console.WriteLine("Created backup at: " + backupDir);

        string[] buffFiles = Directory.GetFiles(buffsDir, "*.cs", SearchOption.AllDirectories);
        List<ExtractedClass> extractedClasses = new List<ExtractedClass>();

        foreach (string file in buffFiles)
        {
            string code = File.ReadAllText(file);
            List<ExtractedClass> found = ExtractClassesFromFile(code);
            if (found.Count > 0)
            {
                extractedClasses.AddRange(found);
                StringBuilder modifiedCode = new StringBuilder(code);
                foreach (ExtractedClass cls in found.OrderByDescending(c => c.StartIndex))
                {
                    modifiedCode.Remove(cls.StartIndex, cls.EndIndex - cls.StartIndex);
                }
                File.WriteAllText(file, modifiedCode.ToString());
            }
        }

        List<ExtractedClass> npcClasses = extractedClasses.Where(c => c.BaseType == "GlobalNPC").ToList();
        if (npcClasses.Count > 0)
        {
            string globalNpcCode = File.ReadAllText(globalNpcPath);
            globalNpcCode = MergeIntoGlobalNPC(globalNpcCode, npcClasses);
            File.WriteAllText(globalNpcPath, globalNpcCode);
        }

        List<ExtractedClass> playerClasses = extractedClasses.Where(c => c.BaseType == "ModPlayer").ToList();
        if (playerClasses.Count > 0)
        {
            string playerCode = File.ReadAllText(playerPath);
            string updatePlayerCode = File.ReadAllText(updatePlayerPath);
            MergeIntoHendecamPlayer(ref playerCode, ref updatePlayerCode, playerClasses);
            File.WriteAllText(playerPath, playerCode);
            File.WriteAllText(updatePlayerPath, updatePlayerCode);
        }

        string[] allCsFiles = Directory.GetFiles(modSourcesRoot, "*.cs", SearchOption.AllDirectories);
        HashSet<string> npcClassNames = new HashSet<string>(npcClasses.Select(c => c.ClassName));
        HashSet<string> playerClassNames = new HashSet<string>(playerClasses.Select(c => c.ClassName));

        foreach (string file in allCsFiles)
        {
            string content = File.ReadAllText(file);
            string original = content;
            bool usedGlobalNpc = false;
            bool usedHendecamPlayer = false;

            foreach (string oldNpc in npcClassNames)
            {
                string pattern = @"\bGetGlobalNPC\s*<\s*" + Regex.Escape(oldNpc) + @"\s*>";
                if (Regex.IsMatch(content, pattern))
                {
                    content = Regex.Replace(content, pattern, "GetGlobalNPC<HendecamGlobalNPC>");
                    usedGlobalNpc = true;
                }
            }

            foreach (string oldPlayer in playerClassNames)
            {
                string pattern = @"\bGetModPlayer\s*<\s*" + Regex.Escape(oldPlayer) + @"\s*>";
                if (Regex.IsMatch(content, pattern))
                {
                    content = Regex.Replace(content, pattern, "GetModPlayer<HendecamPlayer>");
                    usedHendecamPlayer = true;
                }
            }

            if (usedGlobalNpc && !content.Contains("using HendecamMod.Content.NPCs;"))
                content = "using HendecamMod.Content.NPCs;\r\n" + content;

            if (usedHendecamPlayer && !content.Contains("using HendecamMod.Common.Systems.Players;"))
                content = "using HendecamMod.Common.Systems.Players;\r\n" + content;

            if (content != original)
                File.WriteAllText(file, content);
        }

        Console.WriteLine(String.Format("Done! Migrated {0} GlobalNPC and {1} ModPlayer classes.", npcClasses.Count, playerClasses.Count));
    }

    private static List<ExtractedClass> ExtractClassesFromFile(string code)
    {
        List<ExtractedClass> results = new List<ExtractedClass>();
        Regex classRegex = new Regex(@"public\s+class\s+(\w+)\s*:\s*(ModPlayer|GlobalNPC)\b");

        foreach (Match match in classRegex.Matches(code))
        {
            string className = match.Groups[1].Value;
            string baseType = match.Groups[2].Value;
            if (className == "HendecamPlayer" || className == "HendecamGlobalNPC") continue;

            int openBrace = code.IndexOf('{', match.Index);
            if (openBrace == -1) continue;
            int closeBrace = FindMatchingBrace(code, openBrace);
            if (closeBrace == -1) continue;

            string classBody = code.Substring(openBrace + 1, closeBrace - openBrace - 1);
            ExtractedClass extracted = new ExtractedClass
            {
                ClassName = className,
                BaseType = baseType,
                StartIndex = match.Index,
                EndIndex = closeBrace + 1
            };

            Regex fieldRegex = new Regex(@"^\s*(public|private|internal)?\s*(bool|int|float|double|Vector2)\s+(\w+)\s*(=\s*[^;]+)?;", RegexOptions.Multiline);
            foreach (Match fieldMatch in fieldRegex.Matches(classBody))
            {
                string fieldName = fieldMatch.Groups[3].Value;
                if (fieldName == "InstancePerEntity") continue;
                extracted.FieldNames.Add(fieldName);
                extracted.Fields.Add(fieldMatch.Value.Trim());
            }

            Regex methodRegex = new Regex(@"(?:public\s+override|private|void)\s+(?:void|bool|Color\?)\s+(\w+)\s*\([^)]*\)");
            foreach (Match methodMatch in methodRegex.Matches(classBody))
            {
                string methodName = methodMatch.Groups[1].Value;
                int mOpen = classBody.IndexOf('{', methodMatch.Index);
                if (mOpen == -1) continue;
                int mClose = FindMatchingBrace(classBody, mOpen);
                if (mClose == -1) continue;

                string methodBody = classBody.Substring(mOpen + 1, mClose - mOpen - 1).Trim();
                if (methodName == "DOTDebuff")
                {
                    Match multMatch = Regex.Match(methodBody, @"damagePerSecond\s*\*\s*(\d+)");
                    int mult;
                    if (multMatch.Success && int.TryParse(multMatch.Groups[1].Value, out mult))
                        extracted.DotMultiplier = mult;
                }
                else
                {
                    extracted.Methods[methodName] = methodBody;
                }
            }

            if (extracted.BaseType == "GlobalNPC" && extracted.Methods.ContainsKey("UpdateLifeRegen"))
            {
                int mult = extracted.DotMultiplier.HasValue ? extracted.DotMultiplier.Value : 7;
                extracted.Methods["UpdateLifeRegen"] = Regex.Replace(
                    extracted.Methods["UpdateLifeRegen"],
                    @"DOTDebuff\s*\(\s*npc\s*,\s*([^,]+?)\s*,\s*ref\s+damage\s*\)",
                    String.Format("DOTDebuff(npc, $1, {0}, ref damage)", mult)
                );
            }

            results.Add(extracted);
        }
        return results;
    }

    private static string MergeIntoGlobalNPC(string globalNpcCode, List<ExtractedClass> npcClasses)
    {
        foreach (ExtractedClass cls in npcClasses)
        {
            for (int i = 0; i < cls.Fields.Count; i++)
            {
                string fieldName = cls.FieldNames[i];
                if (!Regex.IsMatch(globalNpcCode, @"\b" + Regex.Escape(fieldName) + @"\b"))
                    globalNpcCode = InsertAfterAnchor(globalNpcCode, "public override bool InstancePerEntity => true;", "    " + cls.Fields[i]);
            }
            foreach (KeyValuePair<string, string> kvp in cls.Methods)
                globalNpcCode = AppendToMethodBody(globalNpcCode, kvp.Key, kvp.Value);
        }
        return globalNpcCode;
    }

    private static void MergeIntoHendecamPlayer(ref string playerCode, ref string updatePlayerCode, List<ExtractedClass> playerClasses)
    {
        foreach (ExtractedClass cls in playerClasses)
        {
            for (int i = 0; i < cls.Fields.Count; i++)
            {
                string fieldName = cls.FieldNames[i];
                if (!Regex.IsMatch(playerCode, @"\b" + Regex.Escape(fieldName) + @"\b"))
                    playerCode = InsertAfterAnchor(playerCode, "//buff/debuff", "    " + cls.Fields[i]);
            }
            foreach (KeyValuePair<string, string> kvp in cls.Methods)
            {
                if (kvp.Key == "UpdateDead") continue;
                if (kvp.Key == "ResetEffects")
                    playerCode = PrependToMethodBody(playerCode, "ResetEffects", kvp.Value);
                else if (ContainsMethod(updatePlayerCode, kvp.Key))
                    updatePlayerCode = AppendToMethodBody(updatePlayerCode, kvp.Key, kvp.Value);
                else if (ContainsMethod(playerCode, kvp.Key))
                    playerCode = AppendToMethodBody(playerCode, kvp.Key, kvp.Value);
                else
                    updatePlayerCode = AddNewOverrideToClass(updatePlayerCode, kvp.Key, kvp.Value);
            }
        }
    }

    private static bool ContainsMethod(string code, string methodName)
    {
        return Regex.IsMatch(code, @"public\s+override\s+\w+\s+" + Regex.Escape(methodName) + @"\s*\(");
    }

    private static string InsertAfterAnchor(string code, string anchor, string lineToInsert)
    {
        int idx = code.IndexOf(anchor, StringComparison.Ordinal);
        if (idx == -1) return code;
        int lineEnd = code.IndexOf('\n', idx);
        if (lineEnd == -1) return code;
        return code.Insert(lineEnd + 1, lineToInsert + "\r\n");
    }

    private static string PrependToMethodBody(string code, string methodName, string snippet)
    {
        if (String.IsNullOrWhiteSpace(snippet)) return code;
        Match match = Regex.Match(code, @"public\s+override\s+\w+\s+" + Regex.Escape(methodName) + @"\s*\([^)]*\)");
        if (!match.Success) return code;
        int openBrace = code.IndexOf('{', match.Index);
        int closeBrace = FindMatchingBrace(code, openBrace);
        string existing = code.Substring(openBrace + 1, closeBrace - openBrace - 1);
        if (existing.Contains(snippet.Trim())) return code;
        return code.Insert(openBrace + 1, "\r\n        " + snippet.Trim());
    }

    private static string AppendToMethodBody(string code, string methodName, string snippet)
    {
        if (String.IsNullOrWhiteSpace(snippet)) return code;
        Match match = Regex.Match(code, @"public\s+override\s+\w+\s+" + Regex.Escape(methodName) + @"\s*\([^)]*\)");
        if (!match.Success) return code;
        int openBrace = code.IndexOf('{', match.Index);
        int closeBrace = FindMatchingBrace(code, openBrace);
        string existing = code.Substring(openBrace + 1, closeBrace - openBrace - 1);
        if (existing.Contains(snippet.Trim())) return code;
        return code.Insert(closeBrace, "    " + snippet.Trim() + "\r\n    ");
    }

    private static string AddNewOverrideToClass(string code, string methodName, string body)
    {
        int classOpen = code.IndexOf("class HendecamPlayer", StringComparison.Ordinal);
        if (classOpen == -1) return code;
        int braceOpen = code.IndexOf('{', classOpen);
        int braceClose = FindMatchingBrace(code, braceOpen);
        string sig = methodName == "DrawEffects"
            ? "public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)"
            : String.Format("public override void {0}()", methodName);
        return code.Insert(braceClose, String.Format("\r\n        {0}\r\n        {{\r\n            {1}\r\n        }}\r\n", sig, body));
    }

    private static int FindMatchingBrace(string text, int openBraceIndex)
    {
        int depth = 0;
        for (int i = openBraceIndex; i < text.Length; i++)
        {
            if (text[i] == '{') depth++;
            else if (text[i] == '}') { depth--; if (depth == 0) return i; }
        }
        return -1;
    }

    private static void CopyDirectory(string sourceDir, string destinationDir)
    {
        Directory.CreateDirectory(destinationDir);
        foreach (string file in Directory.GetFiles(sourceDir))
            File.Copy(file, Path.Combine(destinationDir, Path.GetFileName(file)), true);
        foreach (string subDir in Directory.GetDirectories(sourceDir))
        {
            string dirName = Path.GetFileName(subDir);
            if (dirName == "bin" || dirName == "obj" || dirName == ".vs" || dirName == ".git") continue;
            CopyDirectory(subDir, Path.Combine(destinationDir, dirName));
        }
    }
}
*/