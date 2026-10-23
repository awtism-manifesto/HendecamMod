using System;
using System.IO;

namespace HendecamMod.Networking;
//from the mirs to the que
public abstract class HendecamPacket : IDisposable // once again, most of this is from overhaul. credits to Mirsario
{
    public int ID { get; internal set; }
    protected BinaryWriter Writer { get; private set; }

    private MemoryStream stream;
    protected HendecamPacket()
    {
        ID = NetSystem.GetPacket(GetType()).ID;
        Writer = new BinaryWriter(stream = new MemoryStream());
    }
    public abstract void Read(BinaryReader reader, int sender);
    public void WriteAndDispose(BinaryWriter writer)
    {
        writer.Write(stream.ToArray());

        Dispose();
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);

        Writer?.Dispose();
        stream?.Dispose();

        Writer = null!;
        stream = null!;
    }
}