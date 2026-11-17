using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000078 RID: 120
  public sealed class GeminiMouthTurns
  {
    // Token: 0x170001A2 RID: 418
    // (get) Token: 0x0600073B RID: 1851 RVA: 0x003628C8 File Offset: 0x003628C8
    public int OpenTurns
    {
      get
      {
        object obj = this.gate;
        int count;
        lock (obj)
        {
          count = this.turns.Count;
        }
        return count;
      }
    }

    // Token: 0x0600073C RID: 1852 RVA: 0x00362910 File Offset: 0x00362910
    public void Reset()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.turns.Clear();
      }
    }

    // Token: 0x0600073D RID: 1853 RVA: 0x00362958 File Offset: 0x00362958
    public void LineSent()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.turns.Add(new GeminiMouthTurns.Turn());
      }
    }

    // Token: 0x0600073E RID: 1854 RVA: 0x003629A4 File Offset: 0x003629A4
    public bool CancelLineInProgress()
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        GeminiMouthTurns.Turn turn = this.turns.FirstOrDefault<GeminiMouthTurns.Turn>((GeminiMouthTurns.Turn t) => !t.Discarded && !t.GenerationComplete);
        if (turn == null)
        {
          flag2 = false;
        }
        else
        {
          turn.Discarded = true;
          flag2 = true;
        }
      }
      return flag2;
    }

    // Token: 0x0600073F RID: 1855 RVA: 0x00362A1C File Offset: 0x00362A1C
    [return: Nullable(1)]
    public List<GeminiMouthEvent> Events(ReadOnlySpan<byte> json)
    {
      List<GeminiMouthEvent> list = new List<GeminiMouthEvent>();
      List<GeminiMouthEvent> list2;
      using (JsonDocument jsonDocument = JsonDocument.Parse(json.ToArray(), default(JsonDocumentOptions)))
      {
        JsonElement rootElement = jsonDocument.RootElement;
        if (rootElement.ValueKind != JsonValueKind.Object)
        {
          list2 = list;
        }
        else
        {
          JsonElement jsonElement;
          if (rootElement.TryGetProperty("setupComplete", out jsonElement))
          {
            list.Add(new GeminiMouthEvent(GeminiMouthEventKind.SetupComplete, null, null));
          }
          if (rootElement.TryGetProperty("goAway", out jsonElement))
          {
            list.Add(new GeminiMouthEvent(GeminiMouthEventKind.GoAway, null, null));
          }
          JsonElement jsonElement2;
          if (rootElement.TryGetProperty("serverContent", out jsonElement2) && jsonElement2.ValueKind == JsonValueKind.Object)
          {
            object obj = this.gate;
            lock (obj)
            {
              this.ServerContentEvents(jsonElement2, list);
            }
          }
          JsonElement jsonElement3;
          if (rootElement.TryGetProperty("usageMetadata", out jsonElement3) && jsonElement3.ValueKind == JsonValueKind.Object)
          {
            list.Add(new GeminiMouthEvent(GeminiMouthEventKind.Usage, null, jsonElement3.GetRawText()));
          }
          list2 = list;
        }
      }
      return list2;
    }

    // Token: 0x06000740 RID: 1856 RVA: 0x00362B40 File Offset: 0x00362B40
    [NullableContext(1)]
    private void ServerContentEvents(JsonElement content, List<GeminiMouthEvent> events)
    {
      if (this.turns.Count == 0)
      {
        return;
      }
      GeminiMouthTurns.Turn turn = this.turns[0];
      JsonElement jsonElement;
      if (content.TryGetProperty("interrupted", out jsonElement) && jsonElement.ValueKind == JsonValueKind.True)
      {
        if (!turn.Discarded && !turn.GenerationComplete)
        {
          events.Add(new GeminiMouthEvent(GeminiMouthEventKind.LineDone, null, null));
          turn.GenerationComplete = true;
        }
        turn.Discarded = true;
      }
      JsonElement jsonElement2;
      JsonElement jsonElement3;
      if (!turn.Discarded && content.TryGetProperty("modelTurn", out jsonElement2) && jsonElement2.TryGetProperty("parts", out jsonElement3) && jsonElement3.ValueKind == JsonValueKind.Array)
      {
        foreach (JsonElement jsonElement4 in jsonElement3.EnumerateArray())
        {
          JsonElement jsonElement5;
          JsonElement jsonElement6;
          if (jsonElement4.TryGetProperty("inlineData", out jsonElement5) && jsonElement5.TryGetProperty("data", out jsonElement6) && jsonElement6.ValueKind == JsonValueKind.String)
          {
            string @string = jsonElement6.GetString();
            if (@string != null && @string.Length > 0)
            {
              events.Add(new GeminiMouthEvent(GeminiMouthEventKind.Audio, @string, null));
            }
          }
        }
      }
      JsonElement jsonElement7;
      if (content.TryGetProperty("generationComplete", out jsonElement7) && jsonElement7.ValueKind == JsonValueKind.True)
      {
        if (!turn.Discarded && !turn.GenerationComplete)
        {
          events.Add(new GeminiMouthEvent(GeminiMouthEventKind.LineDone, null, null));
        }
        turn.GenerationComplete = true;
      }
      JsonElement jsonElement8;
      if (content.TryGetProperty("turnComplete", out jsonElement8) && jsonElement8.ValueKind == JsonValueKind.True)
      {
        if (!turn.Discarded && !turn.GenerationComplete)
        {
          events.Add(new GeminiMouthEvent(GeminiMouthEventKind.LineDone, null, null));
        }
        this.turns.RemoveAt(0);
      }
    }

    // Token: 0x06000741 RID: 1857 RVA: 0x00362D08 File Offset: 0x00362D08
    [NullableContext(1)]
    public static byte[] SpeakRequestBody(string voice, string line, IReadOnlyList<string> languages)
    {
      ArrayBufferWriter<byte> arrayBufferWriter = new ArrayBufferWriter<byte>(line.Length * 2 + 128);
      using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(arrayBufferWriter, default(JsonWriterOptions)))
      {
        utf8JsonWriter.WriteStartObject();
        utf8JsonWriter.WriteString("voice", voice);
        utf8JsonWriter.WriteString("text", line);
        if (languages.Count > 0)
        {
          utf8JsonWriter.WriteStartArray("languages");
          foreach (string text in languages)
          {
            utf8JsonWriter.WriteStringValue(text);
          }
          utf8JsonWriter.WriteEndArray();
        }
        utf8JsonWriter.WriteEndObject();
      }
      return arrayBufferWriter.WrittenSpan.ToArray();
    }

    // Token: 0x06000742 RID: 1858 RVA: 0x00362DDC File Offset: 0x00362DDC
    public unsafe static bool IsLineEnd(ReadOnlySpan<byte> json)
    {
      return json.IndexOf<byte>(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.C2F613A7B8B343584325213EAF1500CAC90BED186F9BF0E1E6CFF614D46C66C8), 14)) >= 0 && json.IndexOf<byte>(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.516867B874BE94B8596F4A4BD0235701E1C27AFA736376B77A7A684627D492FC), 11)) < 0;
    }

    // Token: 0x06000743 RID: 1859 RVA: 0x00362E0C File Offset: 0x00362E0C
    public unsafe static bool CarriesAudio(ReadOnlySpan<byte> json)
    {
      return json.IndexOf<byte>(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.DABF24C86F53A6538AF98D4DE04397D7496DD311CD3709CC313B4F89736AE7DF), 12)) >= 0;
    }

    // Token: 0x06000744 RID: 1860 RVA: 0x00362E28 File Offset: 0x00362E28
    [return: Nullable(2)]
    public unsafe static string ErrorMessage(ReadOnlySpan<byte> json)
    {
      if (json.IndexOf<byte>(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.49D3880C6788CB249B318E9B977D6F22D93051CF7692B26F3C6B89F87EAA1CD6), 7)) < 0)
      {
        return null;
      }
      string text;
      try
      {
        using (JsonDocument jsonDocument = JsonDocument.Parse(json.ToArray(), default(JsonDocumentOptions)))
        {
          JsonElement jsonElement;
          JsonElement jsonElement2;
          text = (jsonDocument.RootElement.TryGetProperty("error", out jsonElement) ? (((jsonElement.ValueKind == JsonValueKind.Object && jsonElement.TryGetProperty("message", out jsonElement2)) ? jsonElement2.GetString() : null) ?? "the Worker couldn't speak the line") : null);
        }
      }
      catch (JsonException)
      {
        text = null;
      }
      return text;
    }

    // Token: 0x0400035A RID: 858
    [Nullable(1)]
    private readonly object gate = new object();

    // Token: 0x0400035B RID: 859
    [Nullable(1)]
    private readonly List<GeminiMouthTurns.Turn> turns = new List<GeminiMouthTurns.Turn>();

    // Token: 0x0400035C RID: 860
    [Nullable(1)]
    public static readonly byte[] LineEndMessage = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.018380DEDB4167542AE04C681E68CEA19C5E0E9F1B457A82F54C4C929AACDE1C), 65).ToArray();

    // Token: 0x02000324 RID: 804
    private sealed class Turn
    {
      // Token: 0x0400164C RID: 5708
      public bool Discarded;

      // Token: 0x0400164D RID: 5709
      public bool GenerationComplete;
    }
  }
}
