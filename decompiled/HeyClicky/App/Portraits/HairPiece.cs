using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Portraits
{
  // Token: 0x020000FC RID: 252
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class HairPiece : IEquatable<HairPiece>
  {
    // Token: 0x06000D3C RID: 3388 RVA: 0x00565AA0 File Offset: 0x00565AA0
    public HairPiece(PPath Path, PieceFill? Fill = 0, bool Stroked = true)
    {
      this.Path = Path;
      this.Fill = Fill;
      this.Stroked = Stroked;
      base..ctor();
    }

    // Token: 0x1700025F RID: 607
    // (get) Token: 0x06000D3D RID: 3389 RVA: 0x00565AC0 File Offset: 0x00565AC0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(HairPiece);
      }
    }

    // Token: 0x17000260 RID: 608
    // (get) Token: 0x06000D3E RID: 3390 RVA: 0x00565ACC File Offset: 0x00565ACC
    // (set) Token: 0x06000D3F RID: 3391 RVA: 0x00565AD4 File Offset: 0x00565AD4
    public PPath Path { get; set; }

    // Token: 0x17000261 RID: 609
    // (get) Token: 0x06000D40 RID: 3392 RVA: 0x00565AE0 File Offset: 0x00565AE0
    // (set) Token: 0x06000D41 RID: 3393 RVA: 0x00565AE8 File Offset: 0x00565AE8
    public PieceFill? Fill { get; set; }

    // Token: 0x17000262 RID: 610
    // (get) Token: 0x06000D42 RID: 3394 RVA: 0x00565AF4 File Offset: 0x00565AF4
    // (set) Token: 0x06000D43 RID: 3395 RVA: 0x00565AFC File Offset: 0x00565AFC
    public bool Stroked { get; set; }

    // Token: 0x06000D44 RID: 3396 RVA: 0x00565B08 File Offset: 0x00565B08
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("HairPiece");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000D45 RID: 3397 RVA: 0x00565B54 File Offset: 0x00565B54
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Path = ");
      builder.Append(this.Path);
      builder.Append(", Fill = ");
      builder.Append(this.Fill.ToString());
      builder.Append(", Stroked = ");
      builder.Append(this.Stroked.ToString());
      return true;
    }

    // Token: 0x06000D46 RID: 3398 RVA: 0x00565BD0 File Offset: 0x00565BD0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(HairPiece left, HairPiece right)
    {
      return !(left == right);
    }

    // Token: 0x06000D47 RID: 3399 RVA: 0x00565BDC File Offset: 0x00565BDC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(HairPiece left, HairPiece right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000D48 RID: 3400 RVA: 0x00565BF0 File Offset: 0x00565BF0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<PPath>.Default.GetHashCode(this.<Path>k__BackingField)) * -1521134295 + EqualityComparer<PieceFill?>.Default.GetHashCode(this.<Fill>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Stroked>k__BackingField);
    }

    // Token: 0x06000D49 RID: 3401 RVA: 0x00565C54 File Offset: 0x00565C54
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as HairPiece);
    }

    // Token: 0x06000D4A RID: 3402 RVA: 0x00565C64 File Offset: 0x00565C64
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(HairPiece other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<PPath>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField) && EqualityComparer<PieceFill?>.Default.Equals(this.<Fill>k__BackingField, other.<Fill>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Stroked>k__BackingField, other.<Stroked>k__BackingField));
    }

    // Token: 0x06000D4C RID: 3404 RVA: 0x00565CE0 File Offset: 0x00565CE0
    [CompilerGenerated]
    private HairPiece(HairPiece original)
    {
      this.Path = original.<Path>k__BackingField;
      this.Fill = original.<Fill>k__BackingField;
      this.Stroked = original.<Stroked>k__BackingField;
    }

    // Token: 0x06000D4D RID: 3405 RVA: 0x00565D0C File Offset: 0x00565D0C
    [CompilerGenerated]
    public void Deconstruct(out PPath Path, out PieceFill? Fill, out bool Stroked)
    {
      Path = this.Path;
      Fill = this.Fill;
      Stroked = this.Stroked;
    }
  }
}
