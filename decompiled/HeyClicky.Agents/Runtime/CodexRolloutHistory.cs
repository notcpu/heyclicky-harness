using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Agents.Runtime
{
  // Token: 0x02000022 RID: 34
  [NullableContext(1)]
  [Nullable(0)]
  internal static class CodexRolloutHistory
  {
    // Token: 0x060003D8 RID: 984 RVA: 0x000E73E4 File Offset: 0x000E73E4
    private static string SessionsDirectory(string codexHome)
    {
      return Path.Combine(codexHome, "sessions");
    }

    // Token: 0x060003D9 RID: 985 RVA: 0x000E73F4 File Offset: 0x000E73F4
    [return: TupleElementNames(new string[] { "ThreadId", "File" })]
    [return: Nullable(new byte[] { 1, 0, 1, 1 })]
    private static IEnumerable<ValueTuple<string, FileInfo>> RolloutFiles(string codexHome)
    {
      string text = CodexRolloutHistory.SessionsDirectory(codexHome);
      if (!Directory.Exists(text))
      {
        return Array.Empty<ValueTuple<string, FileInfo>>();
      }
      IEnumerable<ValueTuple<string, FileInfo>> enumerable;
      try
      {
        enumerable = (from f in new DirectoryInfo(text).EnumerateFiles("rollout-*.jsonl", SearchOption.AllDirectories)
          select new ValueTuple<string, FileInfo>(CodexRolloutHistory.ThreadIdFromFileName(f.Name), f) into x
          where x.Item1 != null
          orderby x.Item2.LastWriteTimeUtc descending
          select new ValueTuple<string, FileInfo>(x.Item1, x.Item2)).ToList<ValueTuple<string, FileInfo>>();
      }
      catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
      {
        Log.Warn("Agents", "Codex sessions unreadable: " + ex.Message);
        enumerable = Array.Empty<ValueTuple<string, FileInfo>>();
      }
      return enumerable;
    }

    // Token: 0x060003DA RID: 986 RVA: 0x000E7520 File Offset: 0x000E7520
    [return: Nullable(2)]
    internal static string ThreadIdFromFileName(string fileName)
    {
      if (!fileName.EndsWith(".jsonl", StringComparison.OrdinalIgnoreCase))
      {
        return null;
      }
      int num = ".jsonl".Length;
      string text = fileName.Substring(0, fileName.Length - num);
      if (text.Length < 36)
      {
        return null;
      }
      string text2 = text;
      num = text2.Length;
      int num2 = num - 36;
      string text3 = text2.Substring(num2, num - num2);
      Guid guid;
      if (!Guid.TryParse(text3, out guid))
      {
        return null;
      }
      return text3;
    }

    // Token: 0x060003DB RID: 987 RVA: 0x000E7590 File Offset: 0x000E7590
    [return: Nullable(2)]
    private static string RolloutPath(string codexHome, string threadId)
    {
      string text = CodexRolloutHistory.SessionsDirectory(codexHome);
      if (!Directory.Exists(text) || threadId.Length == 0)
      {
        return null;
      }
      string text2;
      try
      {
        text2 = Directory.EnumerateFiles(text, "rollout-*-" + threadId + ".jsonl", SearchOption.AllDirectories).FirstOrDefault<string>();
      }
      catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
      {
        text2 = null;
      }
      return text2;
    }

    // Token: 0x060003DC RID: 988 RVA: 0x000E7618 File Offset: 0x000E7618
    public static IReadOnlyList<CodexRolloutHistory.RolloutThread> List(string codexHome)
    {
      List<CodexRolloutHistory.RolloutThread> list = new List<CodexRolloutHistory.RolloutThread>();
      foreach (ValueTuple<string, FileInfo> valueTuple in CodexRolloutHistory.RolloutFiles(codexHome))
      {
        string item = valueTuple.Item1;
        FileInfo item2 = valueTuple.Item2;
        DateTimeOffset? dateTimeOffset = null;
        string text = null;
        string text2 = null;
        bool flag = false;
        int num = 0;
        foreach (string text3 in CodexRolloutHistory.Lines(item2.FullName))
        {
          if (++num > 400)
          {
            break;
          }
          if (text3.Contains("session_meta", StringComparison.Ordinal) || text3.Contains("\"message\"", StringComparison.Ordinal))
          {
            JsonObject jsonObject = CodexRolloutHistory.Parse(text3);
            if (jsonObject != null)
            {
              JsonObject jsonObject2 = jsonObject["payload"] as JsonObject;
              JsonNode jsonNode = jsonObject["type"];
              if (((jsonNode != null) ? jsonNode.ToString() : null) == "session_meta" && jsonObject2 != null)
              {
                DateTimeOffset? dateTimeOffset2 = dateTimeOffset;
                if (dateTimeOffset2 == null)
                {
                  DateTimeOffset? dateTimeOffset3 = CodexRolloutHistory.Timestamp(jsonObject2["timestamp"]);
                  dateTimeOffset = ((dateTimeOffset3 != null) ? dateTimeOffset3 : CodexRolloutHistory.Timestamp(jsonObject["timestamp"]));
                }
                if (text == null)
                {
                  JsonNode jsonNode2 = jsonObject2["cwd"];
                  text = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
                }
                bool flag2 = flag;
                JsonObject jsonObject3 = jsonObject2["source"] as JsonObject;
                flag = flag2 | (((jsonObject3 != null) ? jsonObject3["subagent"] : null) != null);
              }
              else
              {
                string text4 = CodexRolloutHistory.UserText(jsonObject);
                if (text4 != null)
                {
                  text2 = text4;
                  break;
                }
              }
            }
          }
        }
        if (!flag)
        {
          list.Add(new CodexRolloutHistory.RolloutThread(item, new DateTimeOffset?(dateTimeOffset ?? new DateTimeOffset(item2.CreationTimeUtc)), new DateTimeOffset(item2.LastWriteTimeUtc), text, text2 ?? ""));
        }
      }
      return list;
    }

    // Token: 0x060003DD RID: 989 RVA: 0x000E785C File Offset: 0x000E785C
    public unsafe static IReadOnlyList<CodexRolloutHistory.RolloutTurn> ReadTurns(string codexHome, string threadId)
    {
      string text = CodexRolloutHistory.RolloutPath(codexHome, threadId);
      if (text == null)
      {
        return Array.Empty<CodexRolloutHistory.RolloutTurn>();
      }
      CodexRolloutHistory.<>c__DisplayClass8_0 CS$<>8__locals1;
      CS$<>8__locals1.turns = new List<CodexRolloutHistory.RolloutTurn>();
      CS$<>8__locals1.users = null;
      CS$<>8__locals1.progress = null;
      CS$<>8__locals1.replies = null;
      CS$<>8__locals1.startedAt = null;
      CS$<>8__locals1.finishedAt = null;
      foreach (string text2 in CodexRolloutHistory.Lines(text))
      {
        if (text2.Contains("\"message\"", StringComparison.Ordinal))
        {
          JsonObject jsonObject = CodexRolloutHistory.Parse(text2);
          if (jsonObject != null)
          {
            DateTimeOffset? dateTimeOffset = CodexRolloutHistory.Timestamp(jsonObject["timestamp"]);
            string text3 = CodexRolloutHistory.UserText(jsonObject);
            if (text3 != null)
            {
              CodexRolloutHistory.<ReadTurns>g__Close|8_0(ref CS$<>8__locals1);
              int num = 1;
              List<string> list = new List<string>(num);
              CollectionsMarshal.SetCount<string>(list, num);
              Span<string> span = CollectionsMarshal.AsSpan<string>(list);
              int num2 = 0;
              *span[num2] = text3;
              CS$<>8__locals1.users = list;
              CS$<>8__locals1.progress = new List<string>();
              CS$<>8__locals1.replies = new List<string>();
              CS$<>8__locals1.startedAt = dateTimeOffset;
              CS$<>8__locals1.finishedAt = dateTimeOffset;
            }
            else if (CS$<>8__locals1.users != null)
            {
              string text4 = CodexRolloutHistory.AssistantText(jsonObject);
              if (text4 != null)
              {
                if (CodexRolloutHistory.IsCommentary(jsonObject))
                {
                  CS$<>8__locals1.progress.Add(text4);
                }
                else
                {
                  CS$<>8__locals1.replies.Add(text4);
                }
                if (dateTimeOffset != null)
                {
                  CS$<>8__locals1.finishedAt = dateTimeOffset;
                }
              }
            }
          }
        }
      }
      CodexRolloutHistory.<ReadTurns>g__Close|8_0(ref CS$<>8__locals1);
      return CS$<>8__locals1.turns;
    }

    // Token: 0x060003DE RID: 990 RVA: 0x000E7A00 File Offset: 0x000E7A00
    [return: Nullable(2)]
    private static string UserText(JsonObject entry)
    {
      JsonObject jsonObject = CodexRolloutHistory.MessagePayload(entry, "user");
      if (jsonObject == null)
      {
        return null;
      }
      string text = CodexRolloutHistory.ContentText(jsonObject).Trim();
      if (text.Length == 0 || CodexRolloutHistory.IsScaffold(text))
      {
        return null;
      }
      return text;
    }

    // Token: 0x060003DF RID: 991 RVA: 0x000E7A40 File Offset: 0x000E7A40
    [return: Nullable(2)]
    private static string AssistantText(JsonObject entry)
    {
      JsonObject jsonObject = CodexRolloutHistory.MessagePayload(entry, "assistant");
      if (jsonObject == null)
      {
        return null;
      }
      string text = CodexRolloutHistory.ContentText(jsonObject).Trim();
      if (text.Length != 0)
      {
        return text;
      }
      return null;
    }

    // Token: 0x060003E0 RID: 992 RVA: 0x000E7A78 File Offset: 0x000E7A78
    private static bool IsCommentary(JsonObject entry)
    {
      JsonObject jsonObject = entry["payload"] as JsonObject;
      string text;
      if (jsonObject == null)
      {
        text = null;
      }
      else
      {
        JsonNode jsonNode = jsonObject["phase"];
        text = ((jsonNode != null) ? jsonNode.ToString() : null);
      }
      return text == "commentary";
    }

    // Token: 0x060003E1 RID: 993 RVA: 0x000E7AB4 File Offset: 0x000E7AB4
    [return: Nullable(2)]
    private static JsonObject MessagePayload(JsonObject entry, string role)
    {
      JsonNode jsonNode = entry["type"];
      if (((jsonNode != null) ? jsonNode.ToString() : null) == "response_item")
      {
        JsonObject jsonObject = entry["payload"] as JsonObject;
        if (jsonObject != null)
        {
          JsonNode jsonNode2 = jsonObject["type"];
          if (((jsonNode2 != null) ? jsonNode2.ToString() : null) == "message")
          {
            JsonNode jsonNode3 = jsonObject["role"];
            if (((jsonNode3 != null) ? jsonNode3.ToString() : null) == role)
            {
              return jsonObject;
            }
          }
        }
      }
      return null;
    }

    // Token: 0x060003E2 RID: 994 RVA: 0x000E7B40 File Offset: 0x000E7B40
    private static string ContentText(JsonObject payload)
    {
      string text = "\n";
      JsonArray jsonArray;
      if ((jsonArray = payload["content"] as JsonArray) == null)
      {
        jsonArray = new JsonArray(null);
      }
      return string.Join(text, jsonArray.OfType<JsonObject>().Select<JsonObject, string>(delegate(JsonObject part)
      {
        JsonNode jsonNode = part["text"];
        if (jsonNode == null)
        {
          return null;
        }
        return jsonNode.ToString();
      }).OfType<string>());
    }

    // Token: 0x060003E3 RID: 995 RVA: 0x000E7BA8 File Offset: 0x000E7BA8
    internal static bool IsScaffold(string text)
    {
      return text.StartsWith("# AGENTS.md instructions", StringComparison.Ordinal) || text.StartsWith("<environment_context>", StringComparison.Ordinal) || text.StartsWith("<user_instructions>", StringComparison.Ordinal);
    }

    // Token: 0x060003E4 RID: 996 RVA: 0x000E7BD4 File Offset: 0x000E7BD4
    private static IEnumerable<string> Lines(string path)
    {
      CodexRolloutHistory.<Lines>d__15 <Lines>d__ = new CodexRolloutHistory.<Lines>d__15(-2);
      <Lines>d__.<>3__path = path;
      return <Lines>d__;
    }

    // Token: 0x060003E5 RID: 997 RVA: 0x000E7BE4 File Offset: 0x000E7BE4
    [return: Nullable(2)]
    private static StreamReader Open(string path)
    {
      StreamReader streamReader;
      try
      {
        streamReader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete));
      }
      catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
      {
        Log.Warn("Agents", "Codex rollout unreadable (" + Path.GetFileName(path) + "): " + ex.Message);
        streamReader = null;
      }
      return streamReader;
    }

    // Token: 0x060003E6 RID: 998 RVA: 0x000E7C68 File Offset: 0x000E7C68
    [return: Nullable(2)]
    private static string ReadLine(StreamReader reader)
    {
      string text;
      try
      {
        text = reader.ReadLine();
      }
      catch (IOException)
      {
        text = null;
      }
      return text;
    }

    // Token: 0x060003E7 RID: 999 RVA: 0x000E7C94 File Offset: 0x000E7C94
    [return: Nullable(2)]
    private static JsonObject Parse(string line)
    {
      JsonObject jsonObject;
      try
      {
        jsonObject = JsonNode.Parse(line, null, default(JsonDocumentOptions)) as JsonObject;
      }
      catch (JsonException)
      {
        jsonObject = null;
      }
      return jsonObject;
    }

    // Token: 0x060003E8 RID: 1000 RVA: 0x000E7CD8 File Offset: 0x000E7CD8
    [NullableContext(2)]
    private static DateTimeOffset? Timestamp(JsonNode node)
    {
      JsonValue jsonValue = node as JsonValue;
      string text;
      DateTimeOffset dateTimeOffset;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text) || !DateTimeOffset.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTimeOffset))
      {
        return null;
      }
      return new DateTimeOffset?(dateTimeOffset);
    }

    // Token: 0x060003E9 RID: 1001 RVA: 0x000E7D1C File Offset: 0x000E7D1C
    [CompilerGenerated]
    internal static void <ReadTurns>g__Close|8_0(ref CodexRolloutHistory.<>c__DisplayClass8_0 A_0)
    {
      if (A_0.users != null)
      {
        A_0.turns.Add(new CodexRolloutHistory.RolloutTurn(A_0.users, A_0.progress, A_0.replies, A_0.startedAt, A_0.finishedAt));
      }
    }

    // Token: 0x04000166 RID: 358
    private const int ListScanLineLimit = 400;

    // Token: 0x02000101 RID: 257
    [Nullable(0)]
    public sealed class RolloutThread : IEquatable<CodexRolloutHistory.RolloutThread>
    {
      // Token: 0x06000B67 RID: 2919 RVA: 0x0010EC24 File Offset: 0x0010EC24
      public RolloutThread(string ThreadId, DateTimeOffset? CreatedAt, DateTimeOffset UpdatedAt, [Nullable(2)] string Cwd, string FirstUserMessage)
      {
        this.ThreadId = ThreadId;
        this.CreatedAt = CreatedAt;
        this.UpdatedAt = UpdatedAt;
        this.Cwd = Cwd;
        this.FirstUserMessage = FirstUserMessage;
        base..ctor();
      }

      // Token: 0x1700028F RID: 655
      // (get) Token: 0x06000B68 RID: 2920 RVA: 0x0010EC54 File Offset: 0x0010EC54
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(CodexRolloutHistory.RolloutThread);
        }
      }

      // Token: 0x17000290 RID: 656
      // (get) Token: 0x06000B69 RID: 2921 RVA: 0x0010EC60 File Offset: 0x0010EC60
      // (set) Token: 0x06000B6A RID: 2922 RVA: 0x0010EC68 File Offset: 0x0010EC68
      public string ThreadId { get; set; }

      // Token: 0x17000291 RID: 657
      // (get) Token: 0x06000B6B RID: 2923 RVA: 0x0010EC74 File Offset: 0x0010EC74
      // (set) Token: 0x06000B6C RID: 2924 RVA: 0x0010EC7C File Offset: 0x0010EC7C
      public DateTimeOffset? CreatedAt { get; set; }

      // Token: 0x17000292 RID: 658
      // (get) Token: 0x06000B6D RID: 2925 RVA: 0x0010EC88 File Offset: 0x0010EC88
      // (set) Token: 0x06000B6E RID: 2926 RVA: 0x0010EC90 File Offset: 0x0010EC90
      public DateTimeOffset UpdatedAt { get; set; }

      // Token: 0x17000293 RID: 659
      // (get) Token: 0x06000B6F RID: 2927 RVA: 0x0010EC9C File Offset: 0x0010EC9C
      // (set) Token: 0x06000B70 RID: 2928 RVA: 0x0010ECA4 File Offset: 0x0010ECA4
      [Nullable(2)]
      public string Cwd
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x17000294 RID: 660
      // (get) Token: 0x06000B71 RID: 2929 RVA: 0x0010ECB0 File Offset: 0x0010ECB0
      // (set) Token: 0x06000B72 RID: 2930 RVA: 0x0010ECB8 File Offset: 0x0010ECB8
      public string FirstUserMessage { get; set; }

      // Token: 0x06000B73 RID: 2931 RVA: 0x0010ECC4 File Offset: 0x0010ECC4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("RolloutThread");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000B74 RID: 2932 RVA: 0x0010ED10 File Offset: 0x0010ED10
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("ThreadId = ");
        builder.Append(this.ThreadId);
        builder.Append(", CreatedAt = ");
        builder.Append(this.CreatedAt.ToString());
        builder.Append(", UpdatedAt = ");
        builder.Append(this.UpdatedAt.ToString());
        builder.Append(", Cwd = ");
        builder.Append(this.Cwd);
        builder.Append(", FirstUserMessage = ");
        builder.Append(this.FirstUserMessage);
        return true;
      }

      // Token: 0x06000B75 RID: 2933 RVA: 0x0010EDBC File Offset: 0x0010EDBC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(CodexRolloutHistory.RolloutThread left, CodexRolloutHistory.RolloutThread right)
      {
        return !(left == right);
      }

      // Token: 0x06000B76 RID: 2934 RVA: 0x0010EDC8 File Offset: 0x0010EDC8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(CodexRolloutHistory.RolloutThread left, CodexRolloutHistory.RolloutThread right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000B77 RID: 2935 RVA: 0x0010EDDC File Offset: 0x0010EDDC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<CreatedAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<UpdatedAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Cwd>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FirstUserMessage>k__BackingField);
      }

      // Token: 0x06000B78 RID: 2936 RVA: 0x0010EE6C File Offset: 0x0010EE6C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as CodexRolloutHistory.RolloutThread);
      }

      // Token: 0x06000B79 RID: 2937 RVA: 0x0010EE7C File Offset: 0x0010EE7C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(CodexRolloutHistory.RolloutThread other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<CreatedAt>k__BackingField, other.<CreatedAt>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<UpdatedAt>k__BackingField, other.<UpdatedAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Cwd>k__BackingField, other.<Cwd>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FirstUserMessage>k__BackingField, other.<FirstUserMessage>k__BackingField));
      }

      // Token: 0x06000B7B RID: 2939 RVA: 0x0010EF2C File Offset: 0x0010EF2C
      [CompilerGenerated]
      private RolloutThread(CodexRolloutHistory.RolloutThread original)
      {
        this.ThreadId = original.<ThreadId>k__BackingField;
        this.CreatedAt = original.<CreatedAt>k__BackingField;
        this.UpdatedAt = original.<UpdatedAt>k__BackingField;
        this.Cwd = original.<Cwd>k__BackingField;
        this.FirstUserMessage = original.<FirstUserMessage>k__BackingField;
      }

      // Token: 0x06000B7C RID: 2940 RVA: 0x0010EF7C File Offset: 0x0010EF7C
      [CompilerGenerated]
      public void Deconstruct(out string ThreadId, out DateTimeOffset? CreatedAt, out DateTimeOffset UpdatedAt, [Nullable(2)] out string Cwd, out string FirstUserMessage)
      {
        ThreadId = this.ThreadId;
        CreatedAt = this.CreatedAt;
        UpdatedAt = this.UpdatedAt;
        Cwd = this.Cwd;
        FirstUserMessage = this.FirstUserMessage;
      }
    }

    // Token: 0x02000102 RID: 258
    [Nullable(0)]
    public sealed class RolloutTurn : IEquatable<CodexRolloutHistory.RolloutTurn>
    {
      // Token: 0x06000B7D RID: 2941 RVA: 0x0010EFB0 File Offset: 0x0010EFB0
      public RolloutTurn(IReadOnlyList<string> UserMessages, IReadOnlyList<string> ProgressLines, IReadOnlyList<string> Replies, DateTimeOffset? StartedAt, DateTimeOffset? FinishedAt)
      {
        this.UserMessages = UserMessages;
        this.ProgressLines = ProgressLines;
        this.Replies = Replies;
        this.StartedAt = StartedAt;
        this.FinishedAt = FinishedAt;
        base..ctor();
      }

      // Token: 0x17000295 RID: 661
      // (get) Token: 0x06000B7E RID: 2942 RVA: 0x0010EFE0 File Offset: 0x0010EFE0
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(CodexRolloutHistory.RolloutTurn);
        }
      }

      // Token: 0x17000296 RID: 662
      // (get) Token: 0x06000B7F RID: 2943 RVA: 0x0010EFEC File Offset: 0x0010EFEC
      // (set) Token: 0x06000B80 RID: 2944 RVA: 0x0010EFF4 File Offset: 0x0010EFF4
      public IReadOnlyList<string> UserMessages { get; set; }

      // Token: 0x17000297 RID: 663
      // (get) Token: 0x06000B81 RID: 2945 RVA: 0x0010F000 File Offset: 0x0010F000
      // (set) Token: 0x06000B82 RID: 2946 RVA: 0x0010F008 File Offset: 0x0010F008
      public IReadOnlyList<string> ProgressLines { get; set; }

      // Token: 0x17000298 RID: 664
      // (get) Token: 0x06000B83 RID: 2947 RVA: 0x0010F014 File Offset: 0x0010F014
      // (set) Token: 0x06000B84 RID: 2948 RVA: 0x0010F01C File Offset: 0x0010F01C
      public IReadOnlyList<string> Replies { get; set; }

      // Token: 0x17000299 RID: 665
      // (get) Token: 0x06000B85 RID: 2949 RVA: 0x0010F028 File Offset: 0x0010F028
      // (set) Token: 0x06000B86 RID: 2950 RVA: 0x0010F030 File Offset: 0x0010F030
      public DateTimeOffset? StartedAt { get; set; }

      // Token: 0x1700029A RID: 666
      // (get) Token: 0x06000B87 RID: 2951 RVA: 0x0010F03C File Offset: 0x0010F03C
      // (set) Token: 0x06000B88 RID: 2952 RVA: 0x0010F044 File Offset: 0x0010F044
      public DateTimeOffset? FinishedAt { get; set; }

      // Token: 0x06000B89 RID: 2953 RVA: 0x0010F050 File Offset: 0x0010F050
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("RolloutTurn");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000B8A RID: 2954 RVA: 0x0010F09C File Offset: 0x0010F09C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("UserMessages = ");
        builder.Append(this.UserMessages);
        builder.Append(", ProgressLines = ");
        builder.Append(this.ProgressLines);
        builder.Append(", Replies = ");
        builder.Append(this.Replies);
        builder.Append(", StartedAt = ");
        builder.Append(this.StartedAt.ToString());
        builder.Append(", FinishedAt = ");
        builder.Append(this.FinishedAt.ToString());
        return true;
      }

      // Token: 0x06000B8B RID: 2955 RVA: 0x0010F148 File Offset: 0x0010F148
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(CodexRolloutHistory.RolloutTurn left, CodexRolloutHistory.RolloutTurn right)
      {
        return !(left == right);
      }

      // Token: 0x06000B8C RID: 2956 RVA: 0x0010F154 File Offset: 0x0010F154
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(CodexRolloutHistory.RolloutTurn left, CodexRolloutHistory.RolloutTurn right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000B8D RID: 2957 RVA: 0x0010F168 File Offset: 0x0010F168
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<UserMessages>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ProgressLines>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Replies>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<StartedAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<FinishedAt>k__BackingField);
      }

      // Token: 0x06000B8E RID: 2958 RVA: 0x0010F1F8 File Offset: 0x0010F1F8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as CodexRolloutHistory.RolloutTurn);
      }

      // Token: 0x06000B8F RID: 2959 RVA: 0x0010F208 File Offset: 0x0010F208
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(CodexRolloutHistory.RolloutTurn other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<UserMessages>k__BackingField, other.<UserMessages>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ProgressLines>k__BackingField, other.<ProgressLines>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Replies>k__BackingField, other.<Replies>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<StartedAt>k__BackingField, other.<StartedAt>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<FinishedAt>k__BackingField, other.<FinishedAt>k__BackingField));
      }

      // Token: 0x06000B91 RID: 2961 RVA: 0x0010F2B8 File Offset: 0x0010F2B8
      [CompilerGenerated]
      private RolloutTurn(CodexRolloutHistory.RolloutTurn original)
      {
        this.UserMessages = original.<UserMessages>k__BackingField;
        this.ProgressLines = original.<ProgressLines>k__BackingField;
        this.Replies = original.<Replies>k__BackingField;
        this.StartedAt = original.<StartedAt>k__BackingField;
        this.FinishedAt = original.<FinishedAt>k__BackingField;
      }

      // Token: 0x06000B92 RID: 2962 RVA: 0x0010F308 File Offset: 0x0010F308
      [CompilerGenerated]
      public void Deconstruct(out IReadOnlyList<string> UserMessages, out IReadOnlyList<string> ProgressLines, out IReadOnlyList<string> Replies, out DateTimeOffset? StartedAt, out DateTimeOffset? FinishedAt)
      {
        UserMessages = this.UserMessages;
        ProgressLines = this.ProgressLines;
        Replies = this.Replies;
        StartedAt = this.StartedAt;
        FinishedAt = this.FinishedAt;
      }
    }
  }
}
