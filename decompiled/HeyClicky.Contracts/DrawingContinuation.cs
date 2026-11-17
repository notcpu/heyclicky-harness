using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000022 RID: 34
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DrawingContinuation : IEquatable<DrawingContinuation>
  {
    // Token: 0x0600019A RID: 410 RVA: 0x0001F2E8 File Offset: 0x0001F2E8
    public DrawingContinuation(int FirstLine, int Marks, string Code)
    {
      this.FirstLine = FirstLine;
      this.Marks = Marks;
      this.Code = Code;
      base..ctor();
    }

    // Token: 0x17000070 RID: 112
    // (get) Token: 0x0600019B RID: 411 RVA: 0x0001F308 File Offset: 0x0001F308
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DrawingContinuation);
      }
    }

    // Token: 0x17000071 RID: 113
    // (get) Token: 0x0600019C RID: 412 RVA: 0x0001F314 File Offset: 0x0001F314
    // (set) Token: 0x0600019D RID: 413 RVA: 0x0001F31C File Offset: 0x0001F31C
    public int FirstLine { get; set; }

    // Token: 0x17000072 RID: 114
    // (get) Token: 0x0600019E RID: 414 RVA: 0x0001F328 File Offset: 0x0001F328
    // (set) Token: 0x0600019F RID: 415 RVA: 0x0001F330 File Offset: 0x0001F330
    public int Marks { get; set; }

    // Token: 0x17000073 RID: 115
    // (get) Token: 0x060001A0 RID: 416 RVA: 0x0001F33C File Offset: 0x0001F33C
    // (set) Token: 0x060001A1 RID: 417 RVA: 0x0001F344 File Offset: 0x0001F344
    public string Code { get; set; }

    // Token: 0x060001A2 RID: 418 RVA: 0x0001F350 File Offset: 0x0001F350
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DrawingContinuation");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060001A3 RID: 419 RVA: 0x0001F39C File Offset: 0x0001F39C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("FirstLine = ");
      builder.Append(this.FirstLine.ToString());
      builder.Append(", Marks = ");
      builder.Append(this.Marks.ToString());
      builder.Append(", Code = ");
      builder.Append(this.Code);
      return true;
    }

    // Token: 0x060001A4 RID: 420 RVA: 0x0001F418 File Offset: 0x0001F418
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DrawingContinuation left, DrawingContinuation right)
    {
      return !(left == right);
    }

    // Token: 0x060001A5 RID: 421 RVA: 0x0001F424 File Offset: 0x0001F424
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DrawingContinuation left, DrawingContinuation right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060001A6 RID: 422 RVA: 0x0001F438 File Offset: 0x0001F438
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<FirstLine>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Marks>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Code>k__BackingField);
    }

    // Token: 0x060001A7 RID: 423 RVA: 0x0001F49C File Offset: 0x0001F49C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DrawingContinuation);
    }

    // Token: 0x060001A8 RID: 424 RVA: 0x0001F4AC File Offset: 0x0001F4AC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DrawingContinuation other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<FirstLine>k__BackingField, other.<FirstLine>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Marks>k__BackingField, other.<Marks>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Code>k__BackingField, other.<Code>k__BackingField));
    }

    // Token: 0x060001AA RID: 426 RVA: 0x0001F528 File Offset: 0x0001F528
    [CompilerGenerated]
    private DrawingContinuation(DrawingContinuation original)
    {
      this.FirstLine = original.<FirstLine>k__BackingField;
      this.Marks = original.<Marks>k__BackingField;
      this.Code = original.<Code>k__BackingField;
    }

    // Token: 0x060001AB RID: 427 RVA: 0x0001F554 File Offset: 0x0001F554
    [CompilerGenerated]
    public void Deconstruct(out int FirstLine, out int Marks, out string Code)
    {
      FirstLine = this.FirstLine;
      Marks = this.Marks;
      Code = this.Code;
    }
  }
}
