using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200025B RID: 603
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkEnvironment : IEquatable<ClickyInkEnvironment>
  {
    // Token: 0x1700048D RID: 1165
    // (get) Token: 0x06001A5A RID: 6746 RVA: 0x005E1DF0 File Offset: 0x005E1DF0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkEnvironment);
      }
    }

    // Token: 0x1700048E RID: 1166
    // (get) Token: 0x06001A5B RID: 6747 RVA: 0x005E1DFC File Offset: 0x005E1DFC
    // (set) Token: 0x06001A5C RID: 6748 RVA: 0x005E1E04 File Offset: 0x005E1E04
    public double ContentWidth { get; set; }

    // Token: 0x1700048F RID: 1167
    // (get) Token: 0x06001A5D RID: 6749 RVA: 0x005E1E10 File Offset: 0x005E1E10
    // (set) Token: 0x06001A5E RID: 6750 RVA: 0x005E1E18 File Offset: 0x005E1E18
    public bool IsWindowPreview { get; set; }

    // Token: 0x17000490 RID: 1168
    // (get) Token: 0x06001A5F RID: 6751 RVA: 0x005E1E24 File Offset: 0x005E1E24
    // (set) Token: 0x06001A60 RID: 6752 RVA: 0x005E1E2C File Offset: 0x005E1E2C
    public bool IsGlancePreview { get; set; }

    // Token: 0x17000491 RID: 1169
    // (get) Token: 0x06001A61 RID: 6753 RVA: 0x005E1E38 File Offset: 0x005E1E38
    public static ClickyInkEnvironment Current
    {
      get
      {
        return ClickyInkEnvironment.current ?? ClickyInkEnvironment.Default;
      }
    }

    // Token: 0x06001A62 RID: 6754 RVA: 0x005E1E48 File Offset: 0x005E1E48
    public static IDisposable Use(ClickyInkEnvironment environment)
    {
      ClickyInkEnvironment clickyInkEnvironment = ClickyInkEnvironment.current;
      ClickyInkEnvironment.current = environment;
      return new ClickyInkEnvironment.Restore(clickyInkEnvironment);
    }

    // Token: 0x06001A63 RID: 6755 RVA: 0x005E1E5C File Offset: 0x005E1E5C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkEnvironment");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001A64 RID: 6756 RVA: 0x005E1EA8 File Offset: 0x005E1EA8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ContentWidth = ");
      builder.Append(this.ContentWidth.ToString());
      builder.Append(", IsWindowPreview = ");
      builder.Append(this.IsWindowPreview.ToString());
      builder.Append(", IsGlancePreview = ");
      builder.Append(this.IsGlancePreview.ToString());
      return true;
    }

    // Token: 0x06001A65 RID: 6757 RVA: 0x005E1F30 File Offset: 0x005E1F30
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkEnvironment left, ClickyInkEnvironment right)
    {
      return !(left == right);
    }

    // Token: 0x06001A66 RID: 6758 RVA: 0x005E1F3C File Offset: 0x005E1F3C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkEnvironment left, ClickyInkEnvironment right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001A67 RID: 6759 RVA: 0x005E1F50 File Offset: 0x005E1F50
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<ContentWidth>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsWindowPreview>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsGlancePreview>k__BackingField);
    }

    // Token: 0x06001A68 RID: 6760 RVA: 0x005E1FB4 File Offset: 0x005E1FB4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkEnvironment);
    }

    // Token: 0x06001A69 RID: 6761 RVA: 0x005E1FC4 File Offset: 0x005E1FC4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkEnvironment other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<double>.Default.Equals(this.<ContentWidth>k__BackingField, other.<ContentWidth>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsWindowPreview>k__BackingField, other.<IsWindowPreview>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsGlancePreview>k__BackingField, other.<IsGlancePreview>k__BackingField));
    }

    // Token: 0x06001A6B RID: 6763 RVA: 0x005E2040 File Offset: 0x005E2040
    [CompilerGenerated]
    private ClickyInkEnvironment(ClickyInkEnvironment original)
    {
      this.ContentWidth = original.<ContentWidth>k__BackingField;
      this.IsWindowPreview = original.<IsWindowPreview>k__BackingField;
      this.IsGlancePreview = original.<IsGlancePreview>k__BackingField;
    }

    // Token: 0x06001A6C RID: 6764 RVA: 0x005E206C File Offset: 0x005E206C
    public ClickyInkEnvironment()
    {
      this.ContentWidth = 378.0;
      base..ctor();
    }

    // Token: 0x04000CB1 RID: 3249
    public static readonly ClickyInkEnvironment Default = new ClickyInkEnvironment();

    // Token: 0x04000CB2 RID: 3250
    [Nullable(2)]
    [ThreadStatic]
    private static ClickyInkEnvironment current;

    // Token: 0x02000823 RID: 2083
    [NullableContext(0)]
    private sealed class Restore : IDisposable
    {
      // Token: 0x06003830 RID: 14384 RVA: 0x00686960 File Offset: 0x00686960
      [NullableContext(2)]
      public Restore(ClickyInkEnvironment previous)
      {
      }

      // Token: 0x06003831 RID: 14385 RVA: 0x00686970 File Offset: 0x00686970
      public void Dispose()
      {
        ClickyInkEnvironment.current = this.<previous>P;
      }

      // Token: 0x040022B6 RID: 8886
      [Nullable(2)]
      [CompilerGenerated]
      private ClickyInkEnvironment <previous>P = previous;
    }
  }
}
