using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200019D RID: 413
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkEdgeCardEnvironment : IEquatable<ClickyInkEdgeCardEnvironment>
  {
    // Token: 0x170003F6 RID: 1014
    // (get) Token: 0x0600165E RID: 5726 RVA: 0x005BA728 File Offset: 0x005BA728
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkEdgeCardEnvironment);
      }
    }

    // Token: 0x170003F7 RID: 1015
    // (get) Token: 0x0600165F RID: 5727 RVA: 0x005BA734 File Offset: 0x005BA734
    // (set) Token: 0x06001660 RID: 5728 RVA: 0x005BA73C File Offset: 0x005BA73C
    public double? NoteWindowTitleBarHeight { get; set; }

    // Token: 0x170003F8 RID: 1016
    // (get) Token: 0x06001661 RID: 5729 RVA: 0x005BA748 File Offset: 0x005BA748
    // (set) Token: 0x06001662 RID: 5730 RVA: 0x005BA750 File Offset: 0x005BA750
    public double FooterHeight { get; set; }

    // Token: 0x170003F9 RID: 1017
    // (get) Token: 0x06001663 RID: 5731 RVA: 0x005BA75C File Offset: 0x005BA75C
    // (set) Token: 0x06001664 RID: 5732 RVA: 0x005BA764 File Offset: 0x005BA764
    public Size? SheetSize { get; set; }

    // Token: 0x170003FA RID: 1018
    // (get) Token: 0x06001665 RID: 5733 RVA: 0x005BA770 File Offset: 0x005BA770
    public static ClickyInkEdgeCardEnvironment Current
    {
      get
      {
        return ClickyInkEdgeCardEnvironment.current ?? ClickyInkEdgeCardEnvironment.Default;
      }
    }

    // Token: 0x06001666 RID: 5734 RVA: 0x005BA780 File Offset: 0x005BA780
    public static IDisposable Use(ClickyInkEdgeCardEnvironment environment)
    {
      ClickyInkEdgeCardEnvironment clickyInkEdgeCardEnvironment = ClickyInkEdgeCardEnvironment.current;
      ClickyInkEdgeCardEnvironment.current = environment;
      return new ClickyInkEdgeCardEnvironment.Restore(clickyInkEdgeCardEnvironment);
    }

    // Token: 0x06001667 RID: 5735 RVA: 0x005BA794 File Offset: 0x005BA794
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkEdgeCardEnvironment");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001668 RID: 5736 RVA: 0x005BA7E0 File Offset: 0x005BA7E0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("NoteWindowTitleBarHeight = ");
      builder.Append(this.NoteWindowTitleBarHeight.ToString());
      builder.Append(", FooterHeight = ");
      builder.Append(this.FooterHeight.ToString());
      builder.Append(", SheetSize = ");
      builder.Append(this.SheetSize.ToString());
      return true;
    }

    // Token: 0x06001669 RID: 5737 RVA: 0x005BA868 File Offset: 0x005BA868
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkEdgeCardEnvironment left, ClickyInkEdgeCardEnvironment right)
    {
      return !(left == right);
    }

    // Token: 0x0600166A RID: 5738 RVA: 0x005BA874 File Offset: 0x005BA874
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkEdgeCardEnvironment left, ClickyInkEdgeCardEnvironment right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600166B RID: 5739 RVA: 0x005BA888 File Offset: 0x005BA888
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<NoteWindowTitleBarHeight>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<FooterHeight>k__BackingField)) * -1521134295 + EqualityComparer<Size?>.Default.GetHashCode(this.<SheetSize>k__BackingField);
    }

    // Token: 0x0600166C RID: 5740 RVA: 0x005BA8EC File Offset: 0x005BA8EC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkEdgeCardEnvironment);
    }

    // Token: 0x0600166D RID: 5741 RVA: 0x005BA8FC File Offset: 0x005BA8FC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkEdgeCardEnvironment other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<double?>.Default.Equals(this.<NoteWindowTitleBarHeight>k__BackingField, other.<NoteWindowTitleBarHeight>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<FooterHeight>k__BackingField, other.<FooterHeight>k__BackingField) && EqualityComparer<Size?>.Default.Equals(this.<SheetSize>k__BackingField, other.<SheetSize>k__BackingField));
    }

    // Token: 0x0600166F RID: 5743 RVA: 0x005BA978 File Offset: 0x005BA978
    [CompilerGenerated]
    private ClickyInkEdgeCardEnvironment(ClickyInkEdgeCardEnvironment original)
    {
      this.NoteWindowTitleBarHeight = original.<NoteWindowTitleBarHeight>k__BackingField;
      this.FooterHeight = original.<FooterHeight>k__BackingField;
      this.SheetSize = original.<SheetSize>k__BackingField;
    }

    // Token: 0x06001670 RID: 5744 RVA: 0x005BA9A4 File Offset: 0x005BA9A4
    public ClickyInkEdgeCardEnvironment()
    {
      this.FooterHeight = 46.0;
      base..ctor();
    }

    // Token: 0x04000B01 RID: 2817
    public static readonly ClickyInkEdgeCardEnvironment Default = new ClickyInkEdgeCardEnvironment();

    // Token: 0x04000B02 RID: 2818
    [Nullable(2)]
    [ThreadStatic]
    private static ClickyInkEdgeCardEnvironment current;

    // Token: 0x02000751 RID: 1873
    [NullableContext(0)]
    private sealed class Restore : IDisposable
    {
      // Token: 0x060034E2 RID: 13538 RVA: 0x0067B0D8 File Offset: 0x0067B0D8
      [NullableContext(2)]
      public Restore(ClickyInkEdgeCardEnvironment previous)
      {
      }

      // Token: 0x060034E3 RID: 13539 RVA: 0x0067B0E8 File Offset: 0x0067B0E8
      public void Dispose()
      {
        ClickyInkEdgeCardEnvironment.current = this.<previous>P;
      }

      // Token: 0x04002047 RID: 8263
      [Nullable(2)]
      [CompilerGenerated]
      private ClickyInkEdgeCardEnvironment <previous>P = previous;
    }
  }
}
