using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000B7 RID: 183
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentNotification : IEquatable<AgentNotification>
  {
    // Token: 0x06000C03 RID: 3075 RVA: 0x0037BDF4 File Offset: 0x0037BDF4
    public AgentNotification(string Id, string SourceType, [Nullable(2)] string SourceId, [Nullable(2)] string RunId, string Title, string Summary, AgentNotificationSeverity Severity, [Nullable(2)] string ReadAt, string CreatedAt)
    {
      this.Id = Id;
      this.SourceType = SourceType;
      this.SourceId = SourceId;
      this.RunId = RunId;
      this.Title = Title;
      this.Summary = Summary;
      this.Severity = Severity;
      this.ReadAt = ReadAt;
      this.CreatedAt = CreatedAt;
      base..ctor();
    }

    // Token: 0x1700029C RID: 668
    // (get) Token: 0x06000C04 RID: 3076 RVA: 0x0037BE4C File Offset: 0x0037BE4C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentNotification);
      }
    }

    // Token: 0x1700029D RID: 669
    // (get) Token: 0x06000C05 RID: 3077 RVA: 0x0037BE58 File Offset: 0x0037BE58
    // (set) Token: 0x06000C06 RID: 3078 RVA: 0x0037BE60 File Offset: 0x0037BE60
    public string Id { get; set; }

    // Token: 0x1700029E RID: 670
    // (get) Token: 0x06000C07 RID: 3079 RVA: 0x0037BE6C File Offset: 0x0037BE6C
    // (set) Token: 0x06000C08 RID: 3080 RVA: 0x0037BE74 File Offset: 0x0037BE74
    public string SourceType { get; set; }

    // Token: 0x1700029F RID: 671
    // (get) Token: 0x06000C09 RID: 3081 RVA: 0x0037BE80 File Offset: 0x0037BE80
    // (set) Token: 0x06000C0A RID: 3082 RVA: 0x0037BE88 File Offset: 0x0037BE88
    [Nullable(2)]
    public string SourceId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170002A0 RID: 672
    // (get) Token: 0x06000C0B RID: 3083 RVA: 0x0037BE94 File Offset: 0x0037BE94
    // (set) Token: 0x06000C0C RID: 3084 RVA: 0x0037BE9C File Offset: 0x0037BE9C
    [Nullable(2)]
    public string RunId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170002A1 RID: 673
    // (get) Token: 0x06000C0D RID: 3085 RVA: 0x0037BEA8 File Offset: 0x0037BEA8
    // (set) Token: 0x06000C0E RID: 3086 RVA: 0x0037BEB0 File Offset: 0x0037BEB0
    public string Title { get; set; }

    // Token: 0x170002A2 RID: 674
    // (get) Token: 0x06000C0F RID: 3087 RVA: 0x0037BEBC File Offset: 0x0037BEBC
    // (set) Token: 0x06000C10 RID: 3088 RVA: 0x0037BEC4 File Offset: 0x0037BEC4
    public string Summary { get; set; }

    // Token: 0x170002A3 RID: 675
    // (get) Token: 0x06000C11 RID: 3089 RVA: 0x0037BED0 File Offset: 0x0037BED0
    // (set) Token: 0x06000C12 RID: 3090 RVA: 0x0037BED8 File Offset: 0x0037BED8
    public AgentNotificationSeverity Severity { get; set; }

    // Token: 0x170002A4 RID: 676
    // (get) Token: 0x06000C13 RID: 3091 RVA: 0x0037BEE4 File Offset: 0x0037BEE4
    // (set) Token: 0x06000C14 RID: 3092 RVA: 0x0037BEEC File Offset: 0x0037BEEC
    [Nullable(2)]
    public string ReadAt
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170002A5 RID: 677
    // (get) Token: 0x06000C15 RID: 3093 RVA: 0x0037BEF8 File Offset: 0x0037BEF8
    // (set) Token: 0x06000C16 RID: 3094 RVA: 0x0037BF00 File Offset: 0x0037BF00
    public string CreatedAt { get; set; }

    // Token: 0x170002A6 RID: 678
    // (get) Token: 0x06000C17 RID: 3095 RVA: 0x0037BF0C File Offset: 0x0037BF0C
    public DateTimeOffset? CreatedAtDate
    {
      get
      {
        return AgentNotification.ParseTimestamp(this.CreatedAt);
      }
    }

    // Token: 0x170002A7 RID: 679
    // (get) Token: 0x06000C18 RID: 3096 RVA: 0x0037BF1C File Offset: 0x0037BF1C
    public string DisplayDateTime
    {
      get
      {
        DateTimeOffset? createdAtDate = this.CreatedAtDate;
        if (createdAtDate != null)
        {
          return createdAtDate.GetValueOrDefault().ToLocalTime().ToString("MMM d, yyyy 'at' h:mm tt", CultureInfo.GetCultureInfo("en-US"));
        }
        return "";
      }
    }

    // Token: 0x06000C19 RID: 3097 RVA: 0x0037BF68 File Offset: 0x0037BF68
    public static DateTimeOffset? ParseTimestamp(string raw)
    {
      string text = raw.Trim();
      DateTimeOffset dateTimeOffset;
      if (DateTimeOffset.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTimeOffset))
      {
        return new DateTimeOffset?(dateTimeOffset);
      }
      string text2 = text.Replace(' ', 'T');
      if (text2.Length >= 3)
      {
        string text3 = text2;
        if (text3[text3.Length - 3] != '+')
        {
          string text4 = text2;
          if (text4[text4.Length - 3] != '-')
          {
            goto IL_0063;
          }
        }
        text2 += ":00";
      }
      IL_0063:
      if (!DateTimeOffset.TryParse(text2, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTimeOffset))
      {
        return null;
      }
      return new DateTimeOffset?(dateTimeOffset);
    }

    // Token: 0x06000C1A RID: 3098 RVA: 0x0037BFFC File Offset: 0x0037BFFC
    public static IReadOnlyList<AgentNotification> ParseList(byte[] bytes)
    {
      List<AgentNotification> list = new List<AgentNotification>();
      JsonNode jsonNode = JsonNode.Parse(bytes, null, default(JsonDocumentOptions));
      JsonArray jsonArray = ((jsonNode != null) ? jsonNode["notifications"] : null) as JsonArray;
      if (jsonArray == null)
      {
        return list;
      }
      foreach (JsonObject jsonObject in jsonArray.OfType<JsonObject>())
      {
        AgentNotification.<>c__DisplayClass44_0 CS$<>8__locals1;
        CS$<>8__locals1.n = jsonObject;
        string text = AgentNotification.<ParseList>g__Str|44_0("id", ref CS$<>8__locals1);
        if (text != null)
        {
          string text2 = AgentNotification.<ParseList>g__Str|44_0("severity", ref CS$<>8__locals1);
          AgentNotificationSeverity agentNotificationSeverity;
          if (!(text2 == "success"))
          {
            if (!(text2 == "warning"))
            {
              if (!(text2 == "error"))
              {
                agentNotificationSeverity = AgentNotificationSeverity.Info;
              }
              else
              {
                agentNotificationSeverity = AgentNotificationSeverity.Error;
              }
            }
            else
            {
              agentNotificationSeverity = AgentNotificationSeverity.Warning;
            }
          }
          else
          {
            agentNotificationSeverity = AgentNotificationSeverity.Success;
          }
          AgentNotificationSeverity agentNotificationSeverity2 = agentNotificationSeverity;
          list.Add(new AgentNotification(text, AgentNotification.<ParseList>g__Str|44_0("source_type", ref CS$<>8__locals1) ?? "", AgentNotification.<ParseList>g__Str|44_0("source_id", ref CS$<>8__locals1), AgentNotification.<ParseList>g__Str|44_0("run_id", ref CS$<>8__locals1), AgentNotification.<ParseList>g__Str|44_0("title", ref CS$<>8__locals1) ?? "", AgentNotification.<ParseList>g__Str|44_0("summary", ref CS$<>8__locals1) ?? "", agentNotificationSeverity2, AgentNotification.<ParseList>g__Str|44_0("read_at", ref CS$<>8__locals1), AgentNotification.<ParseList>g__Str|44_0("created_at", ref CS$<>8__locals1) ?? ""));
        }
      }
      return list;
    }

    // Token: 0x06000C1B RID: 3099 RVA: 0x0037C18C File Offset: 0x0037C18C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentNotification");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000C1C RID: 3100 RVA: 0x0037C1D8 File Offset: 0x0037C1D8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", SourceType = ");
      builder.Append(this.SourceType);
      builder.Append(", SourceId = ");
      builder.Append(this.SourceId);
      builder.Append(", RunId = ");
      builder.Append(this.RunId);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      builder.Append(", Severity = ");
      builder.Append(this.Severity.ToString());
      builder.Append(", ReadAt = ");
      builder.Append(this.ReadAt);
      builder.Append(", CreatedAt = ");
      builder.Append(this.CreatedAt);
      builder.Append(", CreatedAtDate = ");
      builder.Append(this.CreatedAtDate.ToString());
      builder.Append(", DisplayDateTime = ");
      builder.Append(this.DisplayDateTime);
      return true;
    }

    // Token: 0x06000C1D RID: 3101 RVA: 0x0037C31C File Offset: 0x0037C31C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentNotification left, AgentNotification right)
    {
      return !(left == right);
    }

    // Token: 0x06000C1E RID: 3102 RVA: 0x0037C328 File Offset: 0x0037C328
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentNotification left, AgentNotification right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000C1F RID: 3103 RVA: 0x0037C33C File Offset: 0x0037C33C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SourceType>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SourceId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RunId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Summary>k__BackingField)) * -1521134295 + EqualityComparer<AgentNotificationSeverity>.Default.GetHashCode(this.<Severity>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ReadAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CreatedAt>k__BackingField);
    }

    // Token: 0x06000C20 RID: 3104 RVA: 0x0037C428 File Offset: 0x0037C428
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentNotification);
    }

    // Token: 0x06000C21 RID: 3105 RVA: 0x0037C438 File Offset: 0x0037C438
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentNotification other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SourceType>k__BackingField, other.<SourceType>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SourceId>k__BackingField, other.<SourceId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RunId>k__BackingField, other.<RunId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Summary>k__BackingField, other.<Summary>k__BackingField) && EqualityComparer<AgentNotificationSeverity>.Default.Equals(this.<Severity>k__BackingField, other.<Severity>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ReadAt>k__BackingField, other.<ReadAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CreatedAt>k__BackingField, other.<CreatedAt>k__BackingField));
    }

    // Token: 0x06000C23 RID: 3107 RVA: 0x0037C554 File Offset: 0x0037C554
    [CompilerGenerated]
    private AgentNotification(AgentNotification original)
    {
      this.Id = original.<Id>k__BackingField;
      this.SourceType = original.<SourceType>k__BackingField;
      this.SourceId = original.<SourceId>k__BackingField;
      this.RunId = original.<RunId>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Summary = original.<Summary>k__BackingField;
      this.Severity = original.<Severity>k__BackingField;
      this.ReadAt = original.<ReadAt>k__BackingField;
      this.CreatedAt = original.<CreatedAt>k__BackingField;
    }

    // Token: 0x06000C24 RID: 3108 RVA: 0x0037C5D4 File Offset: 0x0037C5D4
    [CompilerGenerated]
    public void Deconstruct(out string Id, out string SourceType, [Nullable(2)] out string SourceId, [Nullable(2)] out string RunId, out string Title, out string Summary, out AgentNotificationSeverity Severity, [Nullable(2)] out string ReadAt, out string CreatedAt)
    {
      Id = this.Id;
      SourceType = this.SourceType;
      SourceId = this.SourceId;
      RunId = this.RunId;
      Title = this.Title;
      Summary = this.Summary;
      Severity = this.Severity;
      ReadAt = this.ReadAt;
      CreatedAt = this.CreatedAt;
    }

    // Token: 0x06000C25 RID: 3109 RVA: 0x0037C630 File Offset: 0x0037C630
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <ParseList>g__Str|44_0(string k, ref AgentNotification.<>c__DisplayClass44_0 A_1)
    {
      JsonValue jsonValue = A_1.n[k] as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text))
      {
        return null;
      }
      return text;
    }
  }
}
