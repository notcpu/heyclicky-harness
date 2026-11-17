using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001FC RID: 508
  public sealed class BrainSseParser : IDisposable
  {
    // Token: 0x06001D78 RID: 7544 RVA: 0x003E8D6C File Offset: 0x003E8D6C
    public void Feed(ReadOnlySpan<byte> chunk, [Nullable(1)] IBrainStreamSink sink)
    {
      while (!chunk.IsEmpty)
      {
        int num = chunk.IndexOf(10);
        if (num < 0)
        {
          this.Append(chunk);
          return;
        }
        if (this.lineLength == 0)
        {
          this.DispatchLine(chunk.Slice(0, num), sink);
        }
        else
        {
          this.Append(chunk.Slice(0, num));
          this.DispatchLine(this.lineBuffer.AsSpan<byte>(0, this.lineLength), sink);
          this.lineLength = 0;
        }
        ref ReadOnlySpan<byte> ptr = ref chunk;
        int num2 = num + 1;
        chunk = ptr.Slice(num2, ptr.Length - num2);
      }
    }

    // Token: 0x06001D79 RID: 7545 RVA: 0x003E8E04 File Offset: 0x003E8E04
    [NullableContext(1)]
    public void Complete(IBrainStreamSink sink)
    {
      if (this.lineLength == 0)
      {
        return;
      }
      this.DispatchLine(this.lineBuffer.AsSpan<byte>(0, this.lineLength), sink);
      this.lineLength = 0;
    }

    // Token: 0x06001D7A RID: 7546 RVA: 0x003E8E34 File Offset: 0x003E8E34
    private void Append(ReadOnlySpan<byte> bytes)
    {
      if (this.lineLength + bytes.Length > this.lineBuffer.Length)
      {
        byte[] array = ArrayPool<byte>.Shared.Rent(Math.Max(this.lineBuffer.Length * 2, this.lineLength + bytes.Length));
        this.lineBuffer.AsSpan<byte>(0, this.lineLength).CopyTo(array);
        ArrayPool<byte>.Shared.Return(this.lineBuffer, false);
        this.lineBuffer = array;
      }
      bytes.CopyTo(this.lineBuffer.AsSpan<byte>(this.lineLength));
      this.lineLength += bytes.Length;
    }

    // Token: 0x06001D7B RID: 7547 RVA: 0x003E8EE8 File Offset: 0x003E8EE8
    private unsafe void DispatchLine(ReadOnlySpan<byte> line, [Nullable(1)] IBrainStreamSink sink)
    {
      ref ReadOnlySpan<byte> ptr;
      if (!line.IsEmpty && *line[line.Length - 1] == 13)
      {
        ptr = ref line;
        line = ptr.Slice(0, ptr.Length - 1);
      }
      if (!line.StartsWith<byte>(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.E771B296D32D5D9E1B2A69781D5081EE0FFD0728DAF4A488EE176022134F94E7), 5)))
      {
        return;
      }
      ptr = ref line;
      ReadOnlySpan<byte> readOnlySpan = ptr.Slice(5, ptr.Length - 5).TrimStart(32);
      if (readOnlySpan.IsEmpty)
      {
        return;
      }
      if (this.TryDispatchText(readOnlySpan, sink))
      {
        return;
      }
      try
      {
        using (JsonDocument jsonDocument = JsonDocument.Parse(readOnlySpan.ToArray(), default(JsonDocumentOptions)))
        {
          JsonElement rootElement = jsonDocument.RootElement;
          JsonElement jsonElement;
          if (rootElement.ValueKind == JsonValueKind.Object && rootElement.TryGetProperty("type", out jsonElement) && jsonElement.ValueKind == JsonValueKind.String)
          {
            sink.OnEvent(jsonElement.GetString(), rootElement);
          }
        }
      }
      catch (JsonException ex)
      {
        Log.Warn("Brain", "unreadable stream event skipped: " + ex.Message);
      }
    }

    // Token: 0x06001D7C RID: 7548 RVA: 0x003E900C File Offset: 0x003E900C
    private unsafe bool TryDispatchText(ReadOnlySpan<byte> json, [Nullable(1)] IBrainStreamSink sink)
    {
      if (!json.StartsWith<byte>(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.18E1329BDC66DC17B5DEABDB40085DC09E1186B4A564490A4E05AEAFDA90EE19), 14)))
      {
        return false;
      }
      Utf8JsonReader utf8JsonReader = new Utf8JsonReader(json, default(JsonReaderOptions));
      bool flag;
      try
      {
        if (!utf8JsonReader.Read() || utf8JsonReader.TokenType != JsonTokenType.StartObject)
        {
          flag = false;
        }
        else
        {
          bool flag2 = false;
          while (utf8JsonReader.Read() && utf8JsonReader.TokenType == JsonTokenType.PropertyName)
          {
            bool flag3 = utf8JsonReader.ValueTextEquals(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.619BE496336510A6BA6D293CF53B436B7EB7C09DD209989264C663420E7DBCDE), 4));
            if (!utf8JsonReader.Read())
            {
              return false;
            }
            if (flag3 && utf8JsonReader.TokenType == JsonTokenType.String)
            {
              int num = (utf8JsonReader.HasValueSequence ? ((int)utf8JsonReader.ValueSequence.Length) : utf8JsonReader.ValueSpan.Length);
              if (num > this.textBuffer.Length)
              {
                ArrayPool<char>.Shared.Return(this.textBuffer, false);
                this.textBuffer = ArrayPool<char>.Shared.Rent(num);
              }
              int num2 = utf8JsonReader.CopyString(this.textBuffer);
              sink.OnText(this.textBuffer.AsSpan<char>(0, num2));
              flag2 = true;
            }
            else
            {
              utf8JsonReader.Skip();
            }
          }
          if (!flag2)
          {
            sink.OnText(ReadOnlySpan<char>.Empty);
          }
          flag = true;
        }
      }
      catch (JsonException)
      {
        flag = false;
      }
      return flag;
    }

    // Token: 0x06001D7D RID: 7549 RVA: 0x003E9178 File Offset: 0x003E9178
    public void Dispose()
    {
      if (this.lineBuffer.Length != 0)
      {
        ArrayPool<byte>.Shared.Return(this.lineBuffer, false);
      }
      if (this.textBuffer.Length != 0)
      {
        ArrayPool<char>.Shared.Return(this.textBuffer, false);
      }
      this.lineBuffer = Array.Empty<byte>();
      this.textBuffer = Array.Empty<char>();
    }

    // Token: 0x04000CA9 RID: 3241
    [Nullable(1)]
    private byte[] lineBuffer = ArrayPool<byte>.Shared.Rent(16384);

    // Token: 0x04000CAA RID: 3242
    private int lineLength;

    // Token: 0x04000CAB RID: 3243
    [Nullable(1)]
    private char[] textBuffer = ArrayPool<char>.Shared.Rent(4096);
  }
}
