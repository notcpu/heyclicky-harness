using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000042 RID: 66
  [NullableContext(2)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class ClickyJobUserStep : IEquatable<ClickyJobUserStep>
  {
    // Token: 0x17000193 RID: 403
    // (get) Token: 0x060005F7 RID: 1527 RVA: 0x000F3BA0 File Offset: 0x000F3BA0
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ClickyJobUserStep);
      }
    }

    // Token: 0x17000194 RID: 404
    // (get) Token: 0x060005F8 RID: 1528 RVA: 0x000F3BAC File Offset: 0x000F3BAC
    // (set) Token: 0x060005F9 RID: 1529 RVA: 0x000F3BB4 File Offset: 0x000F3BB4
    [Nullable(1)]
    [RequiredMember]
    public string Step
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000195 RID: 405
    // (get) Token: 0x060005FA RID: 1530 RVA: 0x000F3BC0 File Offset: 0x000F3BC0
    // (set) Token: 0x060005FB RID: 1531 RVA: 0x000F3BC8 File Offset: 0x000F3BC8
    public string Page { get; set; }

    // Token: 0x17000196 RID: 406
    // (get) Token: 0x060005FC RID: 1532 RVA: 0x000F3BD4 File Offset: 0x000F3BD4
    // (set) Token: 0x060005FD RID: 1533 RVA: 0x000F3BDC File Offset: 0x000F3BDC
    public string DoneWhenUrlContains { get; set; }

    // Token: 0x17000197 RID: 407
    // (get) Token: 0x060005FE RID: 1534 RVA: 0x000F3BE8 File Offset: 0x000F3BE8
    // (set) Token: 0x060005FF RID: 1535 RVA: 0x000F3BF0 File Offset: 0x000F3BF0
    public DateTimeOffset AskedAt { get; set; }

    // Token: 0x06000600 RID: 1536 RVA: 0x000F3BFC File Offset: 0x000F3BFC
    [NullableContext(1)]
    [return: Nullable(2)]
    public static ClickyJobUserStep Parse(string rawBlock, DateTimeOffset? askedAt = null)
    {
      string text = rawBlock.Trim();
      if (text.Length == 0)
      {
        return null;
      }
      DateTimeOffset dateTimeOffset = askedAt ?? DateTimeOffset.Now;
      ClickyJobUserStep.<>c__DisplayClass18_0 CS$<>8__locals1;
      CS$<>8__locals1.o = null;
      try
      {
        CS$<>8__locals1.o = JsonNode.Parse(text, null, default(JsonDocumentOptions)) as JsonObject;
      }
      catch (JsonException)
      {
      }
      if (CS$<>8__locals1.o == null)
      {
        return new ClickyJobUserStep
        {
          Step = ((text.Length > 160) ? text.Substring(0, 160) : text),
          AskedAt = dateTimeOffset
        };
      }
      string text2 = ClickyJobUserStep.<Parse>g__Text|18_0("step", 160, ref CS$<>8__locals1);
      if (text2 == null)
      {
        return null;
      }
      return new ClickyJobUserStep
      {
        Step = text2,
        Page = ClickyJobUserStep.<Parse>g__Text|18_0("page", 600, ref CS$<>8__locals1),
        DoneWhenUrlContains = ClickyJobUserStep.<Parse>g__Text|18_0("done_when_url_contains", 200, ref CS$<>8__locals1),
        AskedAt = dateTimeOffset
      };
    }

    // Token: 0x06000601 RID: 1537 RVA: 0x000F3D0C File Offset: 0x000F3D0C
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyJobUserStep");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000602 RID: 1538 RVA: 0x000F3D58 File Offset: 0x000F3D58
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Step = ");
      builder.Append(this.Step);
      builder.Append(", Page = ");
      builder.Append(this.Page);
      builder.Append(", DoneWhenUrlContains = ");
      builder.Append(this.DoneWhenUrlContains);
      builder.Append(", AskedAt = ");
      builder.Append(this.AskedAt.ToString());
      return true;
    }

    // Token: 0x06000603 RID: 1539 RVA: 0x000F3DE0 File Offset: 0x000F3DE0
    [CompilerGenerated]
    public static bool operator !=(ClickyJobUserStep left, ClickyJobUserStep right)
    {
      return !(left == right);
    }

    // Token: 0x06000604 RID: 1540 RVA: 0x000F3DEC File Offset: 0x000F3DEC
    [CompilerGenerated]
    public static bool operator ==(ClickyJobUserStep left, ClickyJobUserStep right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000605 RID: 1541 RVA: 0x000F3E00 File Offset: 0x000F3E00
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Step>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Page>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DoneWhenUrlContains>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<AskedAt>k__BackingField);
    }

    // Token: 0x06000606 RID: 1542 RVA: 0x000F3E7C File Offset: 0x000F3E7C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyJobUserStep);
    }

    // Token: 0x06000607 RID: 1543 RVA: 0x000F3E8C File Offset: 0x000F3E8C
    [CompilerGenerated]
    public bool Equals(ClickyJobUserStep other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Step>k__BackingField, other.<Step>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Page>k__BackingField, other.<Page>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DoneWhenUrlContains>k__BackingField, other.<DoneWhenUrlContains>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<AskedAt>k__BackingField, other.<AskedAt>k__BackingField));
    }

    // Token: 0x06000609 RID: 1545 RVA: 0x000F3F20 File Offset: 0x000F3F20
    [CompilerGenerated]
    [SetsRequiredMembers]
    private ClickyJobUserStep([Nullable(1)] ClickyJobUserStep original)
    {
      this.Step = original.<Step>k__BackingField;
      this.Page = original.<Page>k__BackingField;
      this.DoneWhenUrlContains = original.<DoneWhenUrlContains>k__BackingField;
      this.AskedAt = original.<AskedAt>k__BackingField;
    }

    // Token: 0x0600060A RID: 1546 RVA: 0x000F3F58 File Offset: 0x000F3F58
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public ClickyJobUserStep()
    {
    }

    // Token: 0x0600060B RID: 1547 RVA: 0x000F3F60 File Offset: 0x000F3F60
    [NullableContext(1)]
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <Parse>g__Text|18_0(string key, int limit, ref ClickyJobUserStep.<>c__DisplayClass18_0 A_2)
    {
      JsonValue jsonValue = A_2.o[key] as JsonValue;
      string text2;
      string text = ((jsonValue != null && jsonValue.TryGetValue<string>(out text2)) ? ((text2 != null) ? text2.Trim() : null) : null);
      if (string.IsNullOrEmpty(text))
      {
        return null;
      }
      if (text.Length <= limit)
      {
        return text;
      }
      return text.Substring(0, limit);
    }

    // Token: 0x0400023D RID: 573
    [Nullable(1)]
    public const string BriefSection = "When the next step is one only the user can do (sign, pay, log in, approve something), get everything ready first (open the page in their Chrome, fill in what you can), then end your turn telling them in one line what to do, and append <WAIT_FOR>{\"step\": \"Sign the lease in DocuSign\", \"page\": \"https://…\", \"done_when_url_contains\": \"/signing/complete\"}</WAIT_FOR>. Give done_when_url_contains only when you know the address they land on once it's done; HeyClicky then continues this work by itself when their browser gets there. Don't wait or poll for it yourself. (A step you could do yourself is never a WAIT_FOR: on a website you do it in the browser; in a Mac app, its own scripting commands or, for its window, the mac_step tool.)";
  }
}
