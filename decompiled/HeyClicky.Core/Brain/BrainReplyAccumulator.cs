using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Brain
{
  // Token: 0x02000200 RID: 512
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BrainReplyAccumulator : IBrainStreamSink
  {
    // Token: 0x1700069E RID: 1694
    // (get) Token: 0x06001DDB RID: 7643 RVA: 0x003E9B20 File Offset: 0x003E9B20
    public BrainReply Reply { get; } = new BrainReply();

    // Token: 0x1700069F RID: 1695
    // (get) Token: 0x06001DDC RID: 7644 RVA: 0x003E9B28 File Offset: 0x003E9B28
    public string RawText
    {
      get
      {
        return this.rawText.ToString();
      }
    }

    // Token: 0x170006A0 RID: 1696
    // (get) Token: 0x06001DDD RID: 7645 RVA: 0x003E9B38 File Offset: 0x003E9B38
    // (set) Token: 0x06001DDE RID: 7646 RVA: 0x003E9B40 File Offset: 0x003E9B40
    [Nullable(2)]
    public Action OnFirstToken
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170006A1 RID: 1697
    // (get) Token: 0x06001DDF RID: 7647 RVA: 0x003E9B4C File Offset: 0x003E9B4C
    // (set) Token: 0x06001DE0 RID: 7648 RVA: 0x003E9B54 File Offset: 0x003E9B54
    public bool SpeechOnly { get; set; }

    // Token: 0x170006A2 RID: 1698
    // (get) Token: 0x06001DE1 RID: 7649 RVA: 0x003E9B60 File Offset: 0x003E9B60
    // (set) Token: 0x06001DE2 RID: 7650 RVA: 0x003E9B68 File Offset: 0x003E9B68
    [Nullable(new byte[] { 2, 1 })]
    public Action<string> OnDriveLeaseId
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x06001DE3 RID: 7651 RVA: 0x003E9B74 File Offset: 0x003E9B74
    [NullableContext(2)]
    public BrainReplyAccumulator([Nullable(new byte[] { 2, 1 })] Action<string> onCodeUpdate = null, [Nullable(new byte[] { 2, 1 })] Action<string> onStatus = null, [Nullable(new byte[] { 2, 1 })] Action<BrainToolCall> onToolCall = null, Func<long> clockMilliseconds = null)
    {
      this.onCodeUpdate = onCodeUpdate;
      this.onStatus = onStatus;
      this.onToolCall = onToolCall;
      Func<long> func = clockMilliseconds;
      if (clockMilliseconds == null && (func = BrainReplyAccumulator.<>c.<>9__26_0) == null)
      {
        func = (BrainReplyAccumulator.<>c.<>9__26_0 = () => Environment.TickCount64);
      }
      this.clock = func;
      this.startedAtTicks = this.clock();
    }

    // Token: 0x06001DE4 RID: 7652 RVA: 0x003E9C10 File Offset: 0x003E9C10
    [NullableContext(0)]
    public void OnText(ReadOnlySpan<char> text)
    {
      if (this.Reply.FirstTokenMilliseconds == null)
      {
        this.Reply.FirstTokenMilliseconds = new int?((int)(this.clock() - this.startedAtTicks));
        Action onFirstToken = this.OnFirstToken;
        if (onFirstToken != null)
        {
          onFirstToken();
        }
      }
      this.rawText.Append(text);
      if (this.SpeechOnly)
      {
        return;
      }
      if (this.clock() - this.lastReportAtTicks >= 60L)
      {
        this.ReportCodeIfItGrew();
        return;
      }
      this.codeReportIsPending = true;
    }

    // Token: 0x06001DE5 RID: 7653 RVA: 0x003E9CA4 File Offset: 0x003E9CA4
    public void OnEvent(string type, JsonElement root)
    {
      if (this.codeReportIsPending)
      {
        this.ReportCodeIfItGrew();
      }
      if (type != null)
      {
        switch (type.Length)
        {
        case 4:
        {
          char c2 = type[0];
          if (c2 != 'd')
          {
            if (c2 != 'm')
            {
              return;
            }
            if (!(type == "meta"))
            {
              return;
            }
            this.Reply.ModelIdentifier = BrainReplyAccumulator.ReadString(root, "model");
            this.Reply.UserLayer = BrainReplyAccumulator.ReadString(root, "user_layer");
            this.Reply.UserLayerSignature = BrainReplyAccumulator.ReadString(root, "user_layer_signature");
            JsonElement jsonElement;
            if (root.TryGetProperty("guided_max_rounds", out jsonElement) && jsonElement.ValueKind == JsonValueKind.Number)
            {
              this.Reply.GuidedMaxRounds = new int?(jsonElement.GetInt32());
            }
            string text = BrainReplyAccumulator.ReadString(root, "drive_lease_id");
            if (text == null || text.Length <= 0)
            {
              return;
            }
            this.Reply.DriveLeaseId = text;
            Action<string> onDriveLeaseId = this.OnDriveLeaseId;
            if (onDriveLeaseId == null)
            {
              return;
            }
            onDriveLeaseId(text);
            return;
          }
          else
          {
            if (!(type == "done"))
            {
              return;
            }
            this.Reply.SawDone = true;
            JsonElement jsonElement2;
            if (root.TryGetProperty("cost", out jsonElement2) && jsonElement2.ValueKind == JsonValueKind.Number)
            {
              this.Reply.CostUsd = new double?(jsonElement2.GetDouble());
            }
            this.Reply.LastInputTokens = BrainReplyAccumulator.ReadInt(root, "last_input");
            this.Reply.InputTokens = BrainReplyAccumulator.ReadInt(root, "input");
            JsonElement jsonElement3;
            this.Reply.KeepsReasoning = root.TryGetProperty("keep_reasoning", out jsonElement3) && jsonElement3.ValueKind == JsonValueKind.True;
            this.Reply.TurnToken = BrainReplyAccumulator.ReadString(root, "turn_token");
            JsonElement jsonElement4;
            this.Reply.MessageRecorded = root.TryGetProperty("message_recorded", out jsonElement4) && jsonElement4.ValueKind == JsonValueKind.True;
            JsonElement jsonElement5;
            this.Reply.MessageRefunded = root.TryGetProperty("message_refunded", out jsonElement5) && jsonElement5.ValueKind == JsonValueKind.True;
            this.Reply.ScreenshotFile = BrainReplyAccumulator.ReadString(root, "screenshot_file");
            this.Reply.DriveMessagesRefunded = Math.Min(2, Math.Max(0, BrainReplyAccumulator.ReadInt(root, "drive_messages_refunded")));
            this.Reply.Usage = JsonNode.Parse(root.GetRawText(), null, default(JsonDocumentOptions)) as JsonObject;
            JsonObject usage = this.Reply.Usage;
            if (usage == null)
            {
              return;
            }
            usage.Remove("type");
            return;
          }
          break;
        }
        case 5:
        {
          char c2 = type[0];
          if (c2 != 'e')
          {
            if (c2 != 'i')
            {
              return;
            }
            if (!(type == "items"))
            {
              return;
            }
            JsonElement jsonElement6;
            if (!root.TryGetProperty("items", out jsonElement6) || jsonElement6.ValueKind != JsonValueKind.Array)
            {
              return;
            }
            using (JsonElement.ArrayEnumerator enumerator = jsonElement6.EnumerateArray().GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                JsonElement jsonElement7 = enumerator.Current;
                JsonNode jsonNode = JsonNode.Parse(jsonElement7.GetRawText(), null, default(JsonDocumentOptions));
                if (jsonNode != null)
                {
                  this.Reply.Items.Add(jsonNode);
                }
              }
              return;
            }
          }
          else
          {
            if (!(type == "error"))
            {
              return;
            }
            this.Reply.Error = BrainReplyAccumulator.ReadString(root, "message") ?? "unknown error";
            this.Reply.ErrorCode = BrainReplyAccumulator.ReadString(root, "code");
            return;
          }
          break;
        }
        case 6:
        {
          if (!(type == "status"))
          {
            return;
          }
          string text2 = BrainReplyAccumulator.ReadString(root, "kind");
          if (text2 == null)
          {
            return;
          }
          Action<string> action = this.onStatus;
          if (action == null)
          {
            return;
          }
          action(text2);
          return;
        }
        case 7:
        case 8:
        case 11:
        case 12:
          return;
        case 9:
        {
          if (!(type == "tool_call"))
          {
            return;
          }
          JsonElement jsonElement8;
          if (!root.TryGetProperty("call", out jsonElement8))
          {
            return;
          }
          BrainToolCall brainToolCall = BrainToolCall.FromEvent(jsonElement8);
          if (brainToolCall == null)
          {
            return;
          }
          Action<BrainToolCall> action2 = this.onToolCall;
          if (action2 == null)
          {
            return;
          }
          action2(brainToolCall);
          return;
        }
        case 10:
        {
          if (!(type == "tool_calls"))
          {
            return;
          }
          BrainReply reply = this.Reply;
          JsonElement jsonElement9;
          List<BrainToolCall> list;
          if (!root.TryGetProperty("calls", out jsonElement9) || jsonElement9.ValueKind != JsonValueKind.Array)
          {
            list = new List<BrainToolCall>();
          }
          else
          {
            IEnumerable<JsonElement> enumerable = jsonElement9.EnumerateArray();
            Func<JsonElement, BrainToolCall> func;
            if ((func = BrainReplyAccumulator.<>O.<0>__FromEvent) == null)
            {
              func = (BrainReplyAccumulator.<>O.<0>__FromEvent = new Func<JsonElement, BrainToolCall>(BrainToolCall.FromEvent));
            }
            list = (from c in enumerable.Select<JsonElement, BrainToolCall>(func)
              where c != null
              select (c)).ToList<BrainToolCall>();
          }
          reply.ToolCalls = list;
          return;
        }
        case 13:
          if (!(type == "search_record"))
          {
            return;
          }
          break;
        case 14:
        {
          if (!(type == "computer_calls"))
          {
            return;
          }
          BrainReply reply2 = this.Reply;
          JsonElement jsonElement10;
          List<BrainComputerCall> list2;
          if (!root.TryGetProperty("calls", out jsonElement10) || jsonElement10.ValueKind != JsonValueKind.Array)
          {
            list2 = new List<BrainComputerCall>();
          }
          else
          {
            IEnumerable<JsonElement> enumerable2 = jsonElement10.EnumerateArray();
            Func<JsonElement, BrainComputerCall> func2;
            if ((func2 = BrainReplyAccumulator.<>O.<1>__FromEvent) == null)
            {
              func2 = (BrainReplyAccumulator.<>O.<1>__FromEvent = new Func<JsonElement, BrainComputerCall>(BrainComputerCall.FromEvent));
            }
            list2 = (from c in enumerable2.Select<JsonElement, BrainComputerCall>(func2)
              where c != null
              select (c)).ToList<BrainComputerCall>();
          }
          reply2.ComputerCalls = list2;
          return;
        }
        default:
          return;
        }
        string text3 = BrainReplyAccumulator.ReadString(root, "text");
        if (text3 != null && text3.Length > 0)
        {
          this.Reply.SearchRecords.Add(text3);
          return;
        }
      }
    }

    // Token: 0x06001DE6 RID: 7654 RVA: 0x003EA2A4 File Offset: 0x003EA2A4
    public void Finish()
    {
      string text = this.rawText.ToString();
      if (this.SpeechOnly && !text.Contains("```", StringComparison.Ordinal))
      {
        string text2 = text.Trim();
        if (text2.Length > 0)
        {
          this.Reply.SpokenSentence = text2;
          this.Reply.Code = BrainReplyAccumulator.SayLine((text2.Length > 300) ? text2.Substring(0, 300) : text2);
          this.lastReportedCode = this.Reply.Code;
          Action<string> action = this.onCodeUpdate;
          if (action != null)
          {
            action(this.Reply.Code);
          }
        }
        this.Reply.TotalMilliseconds = (int)(this.clock() - this.startedAtTicks);
        return;
      }
      string text3 = ProgramText.WithoutLinks(ProgramText.ProgramCode(text, true) ?? "");
      this.Reply.Code = text3;
      if (text3.Length > 0 && text3.Length > this.lastReportedCode.Length)
      {
        this.lastReportedCode = text3;
        Action<string> action2 = this.onCodeUpdate;
        if (action2 != null)
        {
          action2(text3);
        }
      }
      this.Reply.TotalMilliseconds = (int)(this.clock() - this.startedAtTicks);
    }

    // Token: 0x06001DE7 RID: 7655 RVA: 0x003EA3E4 File Offset: 0x003EA3E4
    private void ReportCodeIfItGrew()
    {
      this.codeReportIsPending = false;
      string text = ProgramText.ProgramCode(this.rawText.ToString(), false);
      if (text == null || text.Length <= this.lastReportedCode.Length)
      {
        return;
      }
      this.lastReportedCode = text;
      this.lastReportAtTicks = this.clock();
      Action<string> action = this.onCodeUpdate;
      if (action == null)
      {
        return;
      }
      action(ProgramText.WithoutLinks(text));
    }

    // Token: 0x06001DE8 RID: 7656 RVA: 0x003EA450 File Offset: 0x003EA450
    public static string SayLine(string sentence)
    {
      return "say(" + JsonSerializer.Serialize<string>(sentence, new JsonSerializerOptions
      {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
      }) + ");";
    }

    // Token: 0x06001DE9 RID: 7657 RVA: 0x003EA478 File Offset: 0x003EA478
    [return: Nullable(2)]
    private static string ReadString(JsonElement root, string name)
    {
      JsonElement jsonElement;
      if (!root.TryGetProperty(name, out jsonElement) || jsonElement.ValueKind != JsonValueKind.String)
      {
        return null;
      }
      return jsonElement.GetString();
    }

    // Token: 0x06001DEA RID: 7658 RVA: 0x003EA4A4 File Offset: 0x003EA4A4
    private static int ReadInt(JsonElement root, string name)
    {
      JsonElement jsonElement;
      int num;
      if (!root.TryGetProperty(name, out jsonElement) || jsonElement.ValueKind != JsonValueKind.Number || !jsonElement.TryGetInt32(out num))
      {
        return 0;
      }
      return num;
    }

    // Token: 0x04000CCD RID: 3277
    private readonly StringBuilder rawText = new StringBuilder(4096);

    // Token: 0x04000CCE RID: 3278
    private readonly long startedAtTicks;

    // Token: 0x04000CCF RID: 3279
    [Nullable(new byte[] { 2, 1 })]
    private readonly Action<string> onCodeUpdate;

    // Token: 0x04000CD0 RID: 3280
    [Nullable(new byte[] { 2, 1 })]
    private readonly Action<string> onStatus;

    // Token: 0x04000CD1 RID: 3281
    [Nullable(new byte[] { 2, 1 })]
    private readonly Action<BrainToolCall> onToolCall;

    // Token: 0x04000CD2 RID: 3282
    private readonly Func<long> clock;

    // Token: 0x04000CD3 RID: 3283
    private string lastReportedCode = "";

    // Token: 0x04000CD4 RID: 3284
    private long lastReportAtTicks = -4611686018427387904L;

    // Token: 0x04000CD5 RID: 3285
    private bool codeReportIsPending;

    // Token: 0x02000657 RID: 1623
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040024F2 RID: 9458
      [Nullable(0)]
      public static Func<JsonElement, BrainToolCall> <0>__FromEvent;

      // Token: 0x040024F3 RID: 9459
      [Nullable(0)]
      public static Func<JsonElement, BrainComputerCall> <1>__FromEvent;
    }
  }
}
