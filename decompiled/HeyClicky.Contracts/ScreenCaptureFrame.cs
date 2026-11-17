using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000012 RID: 18
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ScreenCaptureFrame : IEquatable<ScreenCaptureFrame>
  {
    // Token: 0x060000D6 RID: 214 RVA: 0x0001E2A4 File Offset: 0x0001E2A4
    public ScreenCaptureFrame(DisplayInfo Display, byte[] Jpeg, int PixelWidth, int PixelHeight, bool ContainsCursor, PointD CursorInImage)
    {
      this.Display = Display;
      this.Jpeg = Jpeg;
      this.PixelWidth = PixelWidth;
      this.PixelHeight = PixelHeight;
      this.ContainsCursor = ContainsCursor;
      this.CursorInImage = CursorInImage;
      base..ctor();
    }

    // Token: 0x1700003C RID: 60
    // (get) Token: 0x060000D7 RID: 215 RVA: 0x0001E2DC File Offset: 0x0001E2DC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ScreenCaptureFrame);
      }
    }

    // Token: 0x1700003D RID: 61
    // (get) Token: 0x060000D8 RID: 216 RVA: 0x0001E2E8 File Offset: 0x0001E2E8
    // (set) Token: 0x060000D9 RID: 217 RVA: 0x0001E2F0 File Offset: 0x0001E2F0
    public DisplayInfo Display { get; set; }

    // Token: 0x1700003E RID: 62
    // (get) Token: 0x060000DA RID: 218 RVA: 0x0001E2FC File Offset: 0x0001E2FC
    // (set) Token: 0x060000DB RID: 219 RVA: 0x0001E304 File Offset: 0x0001E304
    public byte[] Jpeg { get; set; }

    // Token: 0x1700003F RID: 63
    // (get) Token: 0x060000DC RID: 220 RVA: 0x0001E310 File Offset: 0x0001E310
    // (set) Token: 0x060000DD RID: 221 RVA: 0x0001E318 File Offset: 0x0001E318
    public int PixelWidth { get; set; }

    // Token: 0x17000040 RID: 64
    // (get) Token: 0x060000DE RID: 222 RVA: 0x0001E324 File Offset: 0x0001E324
    // (set) Token: 0x060000DF RID: 223 RVA: 0x0001E32C File Offset: 0x0001E32C
    public int PixelHeight { get; set; }

    // Token: 0x17000041 RID: 65
    // (get) Token: 0x060000E0 RID: 224 RVA: 0x0001E338 File Offset: 0x0001E338
    // (set) Token: 0x060000E1 RID: 225 RVA: 0x0001E340 File Offset: 0x0001E340
    public bool ContainsCursor { get; set; }

    // Token: 0x17000042 RID: 66
    // (get) Token: 0x060000E2 RID: 226 RVA: 0x0001E34C File Offset: 0x0001E34C
    // (set) Token: 0x060000E3 RID: 227 RVA: 0x0001E354 File Offset: 0x0001E354
    public PointD CursorInImage { get; set; }

    // Token: 0x060000E4 RID: 228 RVA: 0x0001E360 File Offset: 0x0001E360
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ScreenCaptureFrame");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060000E5 RID: 229 RVA: 0x0001E3AC File Offset: 0x0001E3AC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Display = ");
      builder.Append(this.Display);
      builder.Append(", Jpeg = ");
      builder.Append(this.Jpeg);
      builder.Append(", PixelWidth = ");
      builder.Append(this.PixelWidth.ToString());
      builder.Append(", PixelHeight = ");
      builder.Append(this.PixelHeight.ToString());
      builder.Append(", ContainsCursor = ");
      builder.Append(this.ContainsCursor.ToString());
      builder.Append(", CursorInImage = ");
      builder.Append(this.CursorInImage.ToString());
      return true;
    }

    // Token: 0x060000E6 RID: 230 RVA: 0x0001E490 File Offset: 0x0001E490
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ScreenCaptureFrame left, ScreenCaptureFrame right)
    {
      return !(left == right);
    }

    // Token: 0x060000E7 RID: 231 RVA: 0x0001E49C File Offset: 0x0001E49C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ScreenCaptureFrame left, ScreenCaptureFrame right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060000E8 RID: 232 RVA: 0x0001E4B0 File Offset: 0x0001E4B0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<DisplayInfo>.Default.GetHashCode(this.<Display>k__BackingField)) * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(this.<Jpeg>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PixelWidth>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PixelHeight>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ContainsCursor>k__BackingField)) * -1521134295 + EqualityComparer<PointD>.Default.GetHashCode(this.<CursorInImage>k__BackingField);
    }

    // Token: 0x060000E9 RID: 233 RVA: 0x0001E558 File Offset: 0x0001E558
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ScreenCaptureFrame);
    }

    // Token: 0x060000EA RID: 234 RVA: 0x0001E568 File Offset: 0x0001E568
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ScreenCaptureFrame other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<DisplayInfo>.Default.Equals(this.<Display>k__BackingField, other.<Display>k__BackingField) && EqualityComparer<byte[]>.Default.Equals(this.<Jpeg>k__BackingField, other.<Jpeg>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<PixelWidth>k__BackingField, other.<PixelWidth>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<PixelHeight>k__BackingField, other.<PixelHeight>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ContainsCursor>k__BackingField, other.<ContainsCursor>k__BackingField) && EqualityComparer<PointD>.Default.Equals(this.<CursorInImage>k__BackingField, other.<CursorInImage>k__BackingField));
    }

    // Token: 0x060000EC RID: 236 RVA: 0x0001E634 File Offset: 0x0001E634
    [CompilerGenerated]
    private ScreenCaptureFrame(ScreenCaptureFrame original)
    {
      this.Display = original.<Display>k__BackingField;
      this.Jpeg = original.<Jpeg>k__BackingField;
      this.PixelWidth = original.<PixelWidth>k__BackingField;
      this.PixelHeight = original.<PixelHeight>k__BackingField;
      this.ContainsCursor = original.<ContainsCursor>k__BackingField;
      this.CursorInImage = original.<CursorInImage>k__BackingField;
    }

    // Token: 0x060000ED RID: 237 RVA: 0x0001E690 File Offset: 0x0001E690
    [CompilerGenerated]
    public void Deconstruct(out DisplayInfo Display, out byte[] Jpeg, out int PixelWidth, out int PixelHeight, out bool ContainsCursor, out PointD CursorInImage)
    {
      Display = this.Display;
      Jpeg = this.Jpeg;
      PixelWidth = this.PixelWidth;
      PixelHeight = this.PixelHeight;
      ContainsCursor = this.ContainsCursor;
      CursorInImage = this.CursorInImage;
    }
  }
}
