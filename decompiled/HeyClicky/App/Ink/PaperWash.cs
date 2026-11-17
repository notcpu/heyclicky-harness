using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000207 RID: 519
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PaperWash : IEquatable<PaperWash>
  {
    // Token: 0x060018E0 RID: 6368 RVA: 0x005CDD64 File Offset: 0x005CDD64
    public PaperWash(PaperFill Fill, [Nullable(2)] Func<Size, Rect> Frame = null, bool Clipped = true, [Nullable(new byte[] { 2, 1 })] Func<Size, PaperPath> Path = null)
    {
      this.Fill = Fill;
      this.Frame = Frame;
      this.Clipped = Clipped;
      this.Path = Path;
      base..ctor();
    }

    // Token: 0x17000461 RID: 1121
    // (get) Token: 0x060018E1 RID: 6369 RVA: 0x005CDD8C File Offset: 0x005CDD8C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PaperWash);
      }
    }

    // Token: 0x17000462 RID: 1122
    // (get) Token: 0x060018E2 RID: 6370 RVA: 0x005CDD98 File Offset: 0x005CDD98
    // (set) Token: 0x060018E3 RID: 6371 RVA: 0x005CDDA0 File Offset: 0x005CDDA0
    public PaperFill Fill { get; set; }

    // Token: 0x17000463 RID: 1123
    // (get) Token: 0x060018E4 RID: 6372 RVA: 0x005CDDAC File Offset: 0x005CDDAC
    // (set) Token: 0x060018E5 RID: 6373 RVA: 0x005CDDB4 File Offset: 0x005CDDB4
    [Nullable(2)]
    public Func<Size, Rect> Frame
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000464 RID: 1124
    // (get) Token: 0x060018E6 RID: 6374 RVA: 0x005CDDC0 File Offset: 0x005CDDC0
    // (set) Token: 0x060018E7 RID: 6375 RVA: 0x005CDDC8 File Offset: 0x005CDDC8
    public bool Clipped { get; set; }

    // Token: 0x17000465 RID: 1125
    // (get) Token: 0x060018E8 RID: 6376 RVA: 0x005CDDD4 File Offset: 0x005CDDD4
    // (set) Token: 0x060018E9 RID: 6377 RVA: 0x005CDDDC File Offset: 0x005CDDDC
    [Nullable(new byte[] { 2, 1 })]
    public Func<Size, PaperPath> Path
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x060018EA RID: 6378 RVA: 0x005CDDE8 File Offset: 0x005CDDE8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PaperWash");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060018EB RID: 6379 RVA: 0x005CDE34 File Offset: 0x005CDE34
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Fill = ");
      builder.Append(this.Fill);
      builder.Append(", Frame = ");
      builder.Append(this.Frame);
      builder.Append(", Clipped = ");
      builder.Append(this.Clipped.ToString());
      builder.Append(", Path = ");
      builder.Append(this.Path);
      return true;
    }

    // Token: 0x060018EC RID: 6380 RVA: 0x005CDEBC File Offset: 0x005CDEBC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PaperWash left, PaperWash right)
    {
      return !(left == right);
    }

    // Token: 0x060018ED RID: 6381 RVA: 0x005CDEC8 File Offset: 0x005CDEC8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PaperWash left, PaperWash right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060018EE RID: 6382 RVA: 0x005CDEDC File Offset: 0x005CDEDC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<PaperFill>.Default.GetHashCode(this.<Fill>k__BackingField)) * -1521134295 + EqualityComparer<Func<Size, Rect>>.Default.GetHashCode(this.<Frame>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Clipped>k__BackingField)) * -1521134295 + EqualityComparer<Func<Size, PaperPath>>.Default.GetHashCode(this.<Path>k__BackingField);
    }

    // Token: 0x060018EF RID: 6383 RVA: 0x005CDF58 File Offset: 0x005CDF58
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PaperWash);
    }

    // Token: 0x060018F0 RID: 6384 RVA: 0x005CDF68 File Offset: 0x005CDF68
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PaperWash other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<PaperFill>.Default.Equals(this.<Fill>k__BackingField, other.<Fill>k__BackingField) && EqualityComparer<Func<Size, Rect>>.Default.Equals(this.<Frame>k__BackingField, other.<Frame>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Clipped>k__BackingField, other.<Clipped>k__BackingField) && EqualityComparer<Func<Size, PaperPath>>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField));
    }

    // Token: 0x060018F2 RID: 6386 RVA: 0x005CDFFC File Offset: 0x005CDFFC
    [CompilerGenerated]
    private PaperWash(PaperWash original)
    {
      this.Fill = original.<Fill>k__BackingField;
      this.Frame = original.<Frame>k__BackingField;
      this.Clipped = original.<Clipped>k__BackingField;
      this.Path = original.<Path>k__BackingField;
    }

    // Token: 0x060018F3 RID: 6387 RVA: 0x005CE034 File Offset: 0x005CE034
    [CompilerGenerated]
    public void Deconstruct(out PaperFill Fill, [Nullable(2)] out Func<Size, Rect> Frame, out bool Clipped, [Nullable(new byte[] { 2, 1 })] out Func<Size, PaperPath> Path)
    {
      Fill = this.Fill;
      Frame = this.Frame;
      Clipped = this.Clipped;
      Path = this.Path;
    }
  }
}
