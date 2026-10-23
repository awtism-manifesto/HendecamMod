using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HendecamMod.Networking;

// many parts of this system are from terraria overhaul. credits to Mirsario!
public class NetSystem : ModSystem
{
    public static NetSystem Instance => ModContent.GetInstance<NetSystem>();

    private static readonly List<HendecamPacket> packets = [];

    private static readonly Dictionary<Type, HendecamPacket> packetsByType = [];

    public override void Load()
    {
        // registers the packets into the packets list and the dictionary
        foreach (var type in Mod.Code.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(HendecamPacket))))
        {
            var instance = (HendecamPacket)RuntimeHelpers.GetUninitializedObject(type);
            instance.ID = packets.Count;
            packetsByType[type] = instance;

            packets.Add(instance);

            ContentInstance.Register(instance);
        }
    }
    public override void Unload()
    {
        packets?.Clear();
        packetsByType?.Clear();
    }
    public static HendecamPacket GetPacket(byte id)
        => packets[id];

    public static HendecamPacket GetPacket(Type type)
        => packetsByType[type];

    public static T GetPacket<T>() where T : HendecamPacket
        => ModContent.GetInstance<T>();
    public static void SendPacket<T>(T packet, int ignoreClient = -1, int toClient = -1) where T : HendecamPacket
    {
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            return;
        }

        ModPacket packetToSend = HendecamMod.Instance.GetPacket();

        packetToSend.Write((byte)packet.ID);
        packet.WriteAndDispose(packetToSend);

        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            packetToSend.Send();
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            // Send to all clients except ignoreClient
            packetToSend.Send(ignoreClient: ignoreClient, toClient: toClient);
        }
    }
    internal static void HandlePacket(BinaryReader reader, int sender)
    {
        byte packetId = reader.ReadByte();

        if (packetId > packets.Count)
        {
            return;
        }

        var packet = packets[packetId];

        packet.Read(reader, sender);
    }
}