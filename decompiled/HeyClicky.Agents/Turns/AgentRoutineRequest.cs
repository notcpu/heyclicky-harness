using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Agents.Turns
{
  // Token: 0x0200001A RID: 26
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentRoutineRequest : IEquatable<AgentRoutineRequest>
  {
    // Token: 0x06000344 RID: 836 RVA: 0x000E4C78 File Offset: 0x000E4C78
    public AgentRoutineRequest(string Action, string Title, string Task, int EveryMinutes, string NewTitle)
    {
      this.Action = Action;
      this.Title = Title;
      this.Task = Task;
      this.EveryMinutes = EveryMinutes;
      this.NewTitle = NewTitle;
      base..ctor();
    }

    // Token: 0x170000E6 RID: 230
    // (get) Token: 0x06000345 RID: 837 RVA: 0x000E4CA8 File Offset: 0x000E4CA8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentRoutineRequest);
      }
    }

    // Token: 0x170000E7 RID: 231
    // (get) Token: 0x06000346 RID: 838 RVA: 0x000E4CB4 File Offset: 0x000E4CB4
    // (set) Token: 0x06000347 RID: 839 RVA: 0x000E4CBC File Offset: 0x000E4CBC
    public string Action { get; set; }

    // Token: 0x170000E8 RID: 232
    // (get) Token: 0x06000348 RID: 840 RVA: 0x000E4CC8 File Offset: 0x000E4CC8
    // (set) Token: 0x06000349 RID: 841 RVA: 0x000E4CD0 File Offset: 0x000E4CD0
    public string Title { get; set; }

    // Token: 0x170000E9 RID: 233
    // (get) Token: 0x0600034A RID: 842 RVA: 0x000E4CDC File Offset: 0x000E4CDC
    // (set) Token: 0x0600034B RID: 843 RVA: 0x000E4CE4 File Offset: 0x000E4CE4
    public string Task { get; set; }

    // Token: 0x170000EA RID: 234
    // (get) Token: 0x0600034C RID: 844 RVA: 0x000E4CF0 File Offset: 0x000E4CF0
    // (set) Token: 0x0600034D RID: 845 RVA: 0x000E4CF8 File Offset: 0x000E4CF8
    public int EveryMinutes { get; set; }

    // Token: 0x170000EB RID: 235
    // (get) Token: 0x0600034E RID: 846 RVA: 0x000E4D04 File Offset: 0x000E4D04
    // (set) Token: 0x0600034F RID: 847 RVA: 0x000E4D0C File Offset: 0x000E4D0C
    public string NewTitle { get; set; }

    // Token: 0x06000350 RID: 848 RVA: 0x000E4D18 File Offset: 0x000E4D18
    [return: Nullable(2)]
    public static AgentRoutineRequest Parse(string rawBlock)
    {
      string text = rawBlock.Trim();
      int num = text.IndexOf('{');
      int num2 = text.LastIndexOf('}');
      if (num < 0 || num2 <= num)
      {
        return null;
      }
      JsonObject jsonObject;
      try
      {
        string text2 = text;
        int num3 = num;
        jsonObject = JsonNode.Parse(text2.Substring(num3, num2 + 1 - num3), null, default(JsonDocumentOptions)) as JsonObject;
      }
      catch (JsonException)
      {
        return null;
      }
      if (jsonObject == null)
      {
        return null;
      }
      JsonNode jsonNode = jsonObject["action"];
      string text3 = (((jsonNode != null) ? jsonNode.ToString() : null) ?? "create").Trim().ToLowerInvariant();
      if (!(text3 == "create") && !(text3 == "update") && !(text3 == "pause") && !(text3 == "resume") && !(text3 == "delete"))
      {
        return null;
      }
      JsonNode jsonNode2 = jsonObject["title"];
      string text4;
      if (jsonNode2 == null)
      {
        text4 = null;
      }
      else
      {
        string text5 = jsonNode2.ToString();
        text4 = ((text5 != null) ? text5.Trim() : null);
      }
      string text6 = text4 ?? "";
      JsonNode jsonNode3 = jsonObject["task"];
      string text7;
      if (jsonNode3 == null)
      {
        text7 = null;
      }
      else
      {
        string text8 = jsonNode3.ToString();
        text7 = ((text8 != null) ? text8.Trim() : null);
      }
      string text9 = text7 ?? "";
      JsonNode jsonNode4 = jsonObject["new_title"];
      string text10;
      if (jsonNode4 == null)
      {
        text10 = null;
      }
      else
      {
        string text11 = jsonNode4.ToString();
        text10 = ((text11 != null) ? text11.Trim() : null);
      }
      string text12 = text10 ?? "";
      int num4 = 0;
      JsonValue jsonValue = jsonObject["every_minutes"] as JsonValue;
      if (jsonValue != null)
      {
        int num5;
        double num6;
        string text13;
        int num7;
        if (jsonValue.TryGetValue<int>(out num5))
        {
          num4 = num5;
        }
        else if (jsonValue.TryGetValue<double>(out num6))
        {
          num4 = (int)Math.Round(num6);
        }
        else if (jsonValue.TryGetValue<string>(out text13) && int.TryParse((text13 != null) ? text13.Trim() : null, out num7))
        {
          num4 = num7;
        }
      }
      bool flag;
      if (!(text3 == "create"))
      {
        if (!(text3 == "update"))
        {
          flag = text6.Length > 0 || text9.Length > 0;
        }
        else
        {
          flag = text6.Length > 0 && (text9.Length > 0 || num4 > 0 || text12.Length > 0);
        }
      }
      else
      {
        flag = text9.Length > 0 && num4 > 0;
      }
      if (!flag)
      {
        return null;
      }
      return new AgentRoutineRequest(text3, text6, text9, num4, text12);
    }

    // Token: 0x06000351 RID: 849 RVA: 0x000E4F9C File Offset: 0x000E4F9C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentRoutineRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000352 RID: 850 RVA: 0x000E4FE8 File Offset: 0x000E4FE8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Action = ");
      builder.Append(this.Action);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Task = ");
      builder.Append(this.Task);
      builder.Append(", EveryMinutes = ");
      builder.Append(this.EveryMinutes.ToString());
      builder.Append(", NewTitle = ");
      builder.Append(this.NewTitle);
      return true;
    }

    // Token: 0x06000353 RID: 851 RVA: 0x000E5088 File Offset: 0x000E5088
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentRoutineRequest left, AgentRoutineRequest right)
    {
      return !(left == right);
    }

    // Token: 0x06000354 RID: 852 RVA: 0x000E5094 File Offset: 0x000E5094
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentRoutineRequest left, AgentRoutineRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000355 RID: 853 RVA: 0x000E50A8 File Offset: 0x000E50A8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Action>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Task>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<EveryMinutes>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NewTitle>k__BackingField);
    }

    // Token: 0x06000356 RID: 854 RVA: 0x000E5138 File Offset: 0x000E5138
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentRoutineRequest);
    }

    // Token: 0x06000357 RID: 855 RVA: 0x000E5148 File Offset: 0x000E5148
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentRoutineRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Action>k__BackingField, other.<Action>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Task>k__BackingField, other.<Task>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<EveryMinutes>k__BackingField, other.<EveryMinutes>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NewTitle>k__BackingField, other.<NewTitle>k__BackingField));
    }

    // Token: 0x06000359 RID: 857 RVA: 0x000E51F8 File Offset: 0x000E51F8
    [CompilerGenerated]
    private AgentRoutineRequest(AgentRoutineRequest original)
    {
      this.Action = original.<Action>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Task = original.<Task>k__BackingField;
      this.EveryMinutes = original.<EveryMinutes>k__BackingField;
      this.NewTitle = original.<NewTitle>k__BackingField;
    }

    // Token: 0x0600035A RID: 858 RVA: 0x000E5248 File Offset: 0x000E5248
    [CompilerGenerated]
    public void Deconstruct(out string Action, out string Title, out string Task, out int EveryMinutes, out string NewTitle)
    {
      Action = this.Action;
      Title = this.Title;
      Task = this.Task;
      EveryMinutes = this.EveryMinutes;
      NewTitle = this.NewTitle;
    }
  }
}
