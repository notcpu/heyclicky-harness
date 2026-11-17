using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001B6 RID: 438
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkCardSubject : IEquatable<ClickyInkCardSubject>
  {
    // Token: 0x0600172D RID: 5933 RVA: 0x005BF8A0 File Offset: 0x005BF8A0
    public ClickyInkCardSubject(string Id, ClickyInkCardSubject.SubjectKind Kind, bool IsRunning, ClickyInkCardSubject.SubjectPhase Phase, ClickyThingStatus Status, ClickyPaperStock Stock)
    {
      this.Id = Id;
      this.Kind = Kind;
      this.IsRunning = IsRunning;
      this.Phase = Phase;
      this.Status = Status;
      this.Stock = Stock;
      base..ctor();
    }

    // Token: 0x1700041B RID: 1051
    // (get) Token: 0x0600172E RID: 5934 RVA: 0x005BF8D8 File Offset: 0x005BF8D8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkCardSubject);
      }
    }

    // Token: 0x1700041C RID: 1052
    // (get) Token: 0x0600172F RID: 5935 RVA: 0x005BF8E4 File Offset: 0x005BF8E4
    // (set) Token: 0x06001730 RID: 5936 RVA: 0x005BF8EC File Offset: 0x005BF8EC
    public string Id { get; set; }

    // Token: 0x1700041D RID: 1053
    // (get) Token: 0x06001731 RID: 5937 RVA: 0x005BF8F8 File Offset: 0x005BF8F8
    // (set) Token: 0x06001732 RID: 5938 RVA: 0x005BF900 File Offset: 0x005BF900
    public ClickyInkCardSubject.SubjectKind Kind { get; set; }

    // Token: 0x1700041E RID: 1054
    // (get) Token: 0x06001733 RID: 5939 RVA: 0x005BF90C File Offset: 0x005BF90C
    // (set) Token: 0x06001734 RID: 5940 RVA: 0x005BF914 File Offset: 0x005BF914
    public bool IsRunning { get; set; }

    // Token: 0x1700041F RID: 1055
    // (get) Token: 0x06001735 RID: 5941 RVA: 0x005BF920 File Offset: 0x005BF920
    // (set) Token: 0x06001736 RID: 5942 RVA: 0x005BF928 File Offset: 0x005BF928
    public ClickyInkCardSubject.SubjectPhase Phase { get; set; }

    // Token: 0x17000420 RID: 1056
    // (get) Token: 0x06001737 RID: 5943 RVA: 0x005BF934 File Offset: 0x005BF934
    // (set) Token: 0x06001738 RID: 5944 RVA: 0x005BF93C File Offset: 0x005BF93C
    public ClickyThingStatus Status { get; set; }

    // Token: 0x17000421 RID: 1057
    // (get) Token: 0x06001739 RID: 5945 RVA: 0x005BF948 File Offset: 0x005BF948
    // (set) Token: 0x0600173A RID: 5946 RVA: 0x005BF950 File Offset: 0x005BF950
    public ClickyPaperStock Stock { get; set; }

    // Token: 0x0600173B RID: 5947 RVA: 0x005BF95C File Offset: 0x005BF95C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkCardSubject");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600173C RID: 5948 RVA: 0x005BF9A8 File Offset: 0x005BF9A8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", IsRunning = ");
      builder.Append(this.IsRunning.ToString());
      builder.Append(", Phase = ");
      builder.Append(this.Phase.ToString());
      builder.Append(", Status = ");
      builder.Append(this.Status.ToString());
      builder.Append(", Stock = ");
      builder.Append(this.Stock.ToString());
      return true;
    }

    // Token: 0x0600173D RID: 5949 RVA: 0x005BFA98 File Offset: 0x005BFA98
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkCardSubject left, ClickyInkCardSubject right)
    {
      return !(left == right);
    }

    // Token: 0x0600173E RID: 5950 RVA: 0x005BFAA4 File Offset: 0x005BFAA4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkCardSubject left, ClickyInkCardSubject right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600173F RID: 5951 RVA: 0x005BFAB8 File Offset: 0x005BFAB8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkCardSubject.SubjectKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsRunning>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkCardSubject.SubjectPhase>.Default.GetHashCode(this.<Phase>k__BackingField)) * -1521134295 + EqualityComparer<ClickyThingStatus>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<ClickyPaperStock>.Default.GetHashCode(this.<Stock>k__BackingField);
    }

    // Token: 0x06001740 RID: 5952 RVA: 0x005BFB60 File Offset: 0x005BFB60
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkCardSubject);
    }

    // Token: 0x06001741 RID: 5953 RVA: 0x005BFB70 File Offset: 0x005BFB70
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkCardSubject other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<ClickyInkCardSubject.SubjectKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsRunning>k__BackingField, other.<IsRunning>k__BackingField) && EqualityComparer<ClickyInkCardSubject.SubjectPhase>.Default.Equals(this.<Phase>k__BackingField, other.<Phase>k__BackingField) && EqualityComparer<ClickyThingStatus>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<ClickyPaperStock>.Default.Equals(this.<Stock>k__BackingField, other.<Stock>k__BackingField));
    }

    // Token: 0x06001743 RID: 5955 RVA: 0x005BFC3C File Offset: 0x005BFC3C
    [CompilerGenerated]
    private ClickyInkCardSubject(ClickyInkCardSubject original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Kind = original.<Kind>k__BackingField;
      this.IsRunning = original.<IsRunning>k__BackingField;
      this.Phase = original.<Phase>k__BackingField;
      this.Status = original.<Status>k__BackingField;
      this.Stock = original.<Stock>k__BackingField;
    }

    // Token: 0x06001744 RID: 5956 RVA: 0x005BFC98 File Offset: 0x005BFC98
    [CompilerGenerated]
    public void Deconstruct(out string Id, out ClickyInkCardSubject.SubjectKind Kind, out bool IsRunning, out ClickyInkCardSubject.SubjectPhase Phase, out ClickyThingStatus Status, out ClickyPaperStock Stock)
    {
      Id = this.Id;
      Kind = this.Kind;
      IsRunning = this.IsRunning;
      Phase = this.Phase;
      Status = this.Status;
      Stock = this.Stock;
    }

    // Token: 0x02000771 RID: 1905
    [NullableContext(0)]
    public enum SubjectKind
    {
      // Token: 0x040020A7 RID: 8359
      Job,
      // Token: 0x040020A8 RID: 8360
      Kept
    }

    // Token: 0x02000772 RID: 1906
    [NullableContext(0)]
    public enum SubjectPhase
    {
      // Token: 0x040020AA RID: 8362
      Working,
      // Token: 0x040020AB RID: 8363
      NeedsYou,
      // Token: 0x040020AC RID: 8364
      Failed,
      // Token: 0x040020AD RID: 8365
      Done
    }
  }
}
