using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200012A RID: 298
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkActivity : IEquatable<ClickyInkActivity>
  {
    // Token: 0x06001368 RID: 4968 RVA: 0x003A68CC File Offset: 0x003A68CC
    public ClickyInkActivity(double At, string Kind, string Text)
    {
      this.At = At;
      this.Kind = Kind;
      this.Text = Text;
      base..ctor();
    }

    // Token: 0x1700048B RID: 1163
    // (get) Token: 0x06001369 RID: 4969 RVA: 0x003A68EC File Offset: 0x003A68EC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkActivity);
      }
    }

    // Token: 0x1700048C RID: 1164
    // (get) Token: 0x0600136A RID: 4970 RVA: 0x003A68F8 File Offset: 0x003A68F8
    // (set) Token: 0x0600136B RID: 4971 RVA: 0x003A6900 File Offset: 0x003A6900
    public double At { get; set; }

    // Token: 0x1700048D RID: 1165
    // (get) Token: 0x0600136C RID: 4972 RVA: 0x003A690C File Offset: 0x003A690C
    // (set) Token: 0x0600136D RID: 4973 RVA: 0x003A6914 File Offset: 0x003A6914
    public string Kind { get; set; }

    // Token: 0x1700048E RID: 1166
    // (get) Token: 0x0600136E RID: 4974 RVA: 0x003A6920 File Offset: 0x003A6920
    // (set) Token: 0x0600136F RID: 4975 RVA: 0x003A6928 File Offset: 0x003A6928
    public string Text { get; set; }

    // Token: 0x06001370 RID: 4976 RVA: 0x003A6934 File Offset: 0x003A6934
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkActivity");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001371 RID: 4977 RVA: 0x003A6980 File Offset: 0x003A6980
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("At = ");
      builder.Append(this.At.ToString());
      builder.Append(", Kind = ");
      builder.Append(this.Kind);
      builder.Append(", Text = ");
      builder.Append(this.Text);
      return true;
    }

    // Token: 0x06001372 RID: 4978 RVA: 0x003A69EC File Offset: 0x003A69EC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkActivity left, ClickyInkActivity right)
    {
      return !(left == right);
    }

    // Token: 0x06001373 RID: 4979 RVA: 0x003A69F8 File Offset: 0x003A69F8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkActivity left, ClickyInkActivity right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001374 RID: 4980 RVA: 0x003A6A0C File Offset: 0x003A6A0C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<At>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
    }

    // Token: 0x06001375 RID: 4981 RVA: 0x003A6A70 File Offset: 0x003A6A70
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkActivity);
    }

    // Token: 0x06001376 RID: 4982 RVA: 0x003A6A80 File Offset: 0x003A6A80
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkActivity other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<double>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
    }

    // Token: 0x06001378 RID: 4984 RVA: 0x003A6AFC File Offset: 0x003A6AFC
    [CompilerGenerated]
    private ClickyInkActivity(ClickyInkActivity original)
    {
      this.At = original.<At>k__BackingField;
      this.Kind = original.<Kind>k__BackingField;
      this.Text = original.<Text>k__BackingField;
    }

    // Token: 0x06001379 RID: 4985 RVA: 0x003A6B28 File Offset: 0x003A6B28
    [CompilerGenerated]
    public void Deconstruct(out double At, out string Kind, out string Text)
    {
      At = this.At;
      Kind = this.Kind;
      Text = this.Text;
    }
  }
}
