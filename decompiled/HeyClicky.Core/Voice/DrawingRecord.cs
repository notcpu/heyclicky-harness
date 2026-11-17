using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200006F RID: 111
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DrawingRecord : IEquatable<DrawingRecord>
  {
    // Token: 0x060006BD RID: 1725 RVA: 0x0035FA1C File Offset: 0x0035FA1C
    public DrawingRecord(string Question, string Title, string Program, IReadOnlyList<DrawingRecord.Line> Lines, double Duration, ScreenCaptureFrame Screen, string CursorColorHex, DateTimeOffset At, [Nullable(2)] byte[] Narration = null, int NarrationSampleRate = 0)
    {
      this.Question = Question;
      this.Title = Title;
      this.Program = Program;
      this.Lines = Lines;
      this.Duration = Duration;
      this.Screen = Screen;
      this.CursorColorHex = CursorColorHex;
      this.At = At;
      this.Narration = Narration;
      this.NarrationSampleRate = NarrationSampleRate;
      base..ctor();
    }

    // Token: 0x17000180 RID: 384
    // (get) Token: 0x060006BE RID: 1726 RVA: 0x0035FA7C File Offset: 0x0035FA7C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DrawingRecord);
      }
    }

    // Token: 0x17000181 RID: 385
    // (get) Token: 0x060006BF RID: 1727 RVA: 0x0035FA88 File Offset: 0x0035FA88
    // (set) Token: 0x060006C0 RID: 1728 RVA: 0x0035FA90 File Offset: 0x0035FA90
    public string Question { get; set; }

    // Token: 0x17000182 RID: 386
    // (get) Token: 0x060006C1 RID: 1729 RVA: 0x0035FA9C File Offset: 0x0035FA9C
    // (set) Token: 0x060006C2 RID: 1730 RVA: 0x0035FAA4 File Offset: 0x0035FAA4
    public string Title { get; set; }

    // Token: 0x17000183 RID: 387
    // (get) Token: 0x060006C3 RID: 1731 RVA: 0x0035FAB0 File Offset: 0x0035FAB0
    // (set) Token: 0x060006C4 RID: 1732 RVA: 0x0035FAB8 File Offset: 0x0035FAB8
    public string Program { get; set; }

    // Token: 0x17000184 RID: 388
    // (get) Token: 0x060006C5 RID: 1733 RVA: 0x0035FAC4 File Offset: 0x0035FAC4
    // (set) Token: 0x060006C6 RID: 1734 RVA: 0x0035FACC File Offset: 0x0035FACC
    public IReadOnlyList<DrawingRecord.Line> Lines { get; set; }

    // Token: 0x17000185 RID: 389
    // (get) Token: 0x060006C7 RID: 1735 RVA: 0x0035FAD8 File Offset: 0x0035FAD8
    // (set) Token: 0x060006C8 RID: 1736 RVA: 0x0035FAE0 File Offset: 0x0035FAE0
    public double Duration { get; set; }

    // Token: 0x17000186 RID: 390
    // (get) Token: 0x060006C9 RID: 1737 RVA: 0x0035FAEC File Offset: 0x0035FAEC
    // (set) Token: 0x060006CA RID: 1738 RVA: 0x0035FAF4 File Offset: 0x0035FAF4
    public ScreenCaptureFrame Screen { get; set; }

    // Token: 0x17000187 RID: 391
    // (get) Token: 0x060006CB RID: 1739 RVA: 0x0035FB00 File Offset: 0x0035FB00
    // (set) Token: 0x060006CC RID: 1740 RVA: 0x0035FB08 File Offset: 0x0035FB08
    public string CursorColorHex { get; set; }

    // Token: 0x17000188 RID: 392
    // (get) Token: 0x060006CD RID: 1741 RVA: 0x0035FB14 File Offset: 0x0035FB14
    // (set) Token: 0x060006CE RID: 1742 RVA: 0x0035FB1C File Offset: 0x0035FB1C
    public DateTimeOffset At { get; set; }

    // Token: 0x17000189 RID: 393
    // (get) Token: 0x060006CF RID: 1743 RVA: 0x0035FB28 File Offset: 0x0035FB28
    // (set) Token: 0x060006D0 RID: 1744 RVA: 0x0035FB30 File Offset: 0x0035FB30
    [Nullable(2)]
    public byte[] Narration
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700018A RID: 394
    // (get) Token: 0x060006D1 RID: 1745 RVA: 0x0035FB3C File Offset: 0x0035FB3C
    // (set) Token: 0x060006D2 RID: 1746 RVA: 0x0035FB44 File Offset: 0x0035FB44
    public int NarrationSampleRate { get; set; }

    // Token: 0x1700018B RID: 395
    // (get) Token: 0x060006D3 RID: 1747 RVA: 0x0035FB50 File Offset: 0x0035FB50
    // (set) Token: 0x060006D4 RID: 1748 RVA: 0x0035FB58 File Offset: 0x0035FB58
    public bool KeepsInChat { get; set; }

    // Token: 0x060006D5 RID: 1749 RVA: 0x0035FB64 File Offset: 0x0035FB64
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DrawingRecord");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060006D6 RID: 1750 RVA: 0x0035FBB0 File Offset: 0x0035FBB0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Question = ");
      builder.Append(this.Question);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Program = ");
      builder.Append(this.Program);
      builder.Append(", Lines = ");
      builder.Append(this.Lines);
      builder.Append(", Duration = ");
      builder.Append(this.Duration.ToString());
      builder.Append(", Screen = ");
      builder.Append(this.Screen);
      builder.Append(", CursorColorHex = ");
      builder.Append(this.CursorColorHex);
      builder.Append(", At = ");
      builder.Append(this.At.ToString());
      builder.Append(", Narration = ");
      builder.Append(this.Narration);
      builder.Append(", NarrationSampleRate = ");
      builder.Append(this.NarrationSampleRate.ToString());
      builder.Append(", KeepsInChat = ");
      builder.Append(this.KeepsInChat.ToString());
      return true;
    }

    // Token: 0x060006D7 RID: 1751 RVA: 0x0035FD10 File Offset: 0x0035FD10
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DrawingRecord left, DrawingRecord right)
    {
      return !(left == right);
    }

    // Token: 0x060006D8 RID: 1752 RVA: 0x0035FD1C File Offset: 0x0035FD1C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DrawingRecord left, DrawingRecord right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060006D9 RID: 1753 RVA: 0x0035FD30 File Offset: 0x0035FD30
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Question>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Program>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<DrawingRecord.Line>>.Default.GetHashCode(this.<Lines>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Duration>k__BackingField)) * -1521134295 + EqualityComparer<ScreenCaptureFrame>.Default.GetHashCode(this.<Screen>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CursorColorHex>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<At>k__BackingField)) * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(this.<Narration>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<NarrationSampleRate>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<KeepsInChat>k__BackingField);
    }

    // Token: 0x060006DA RID: 1754 RVA: 0x0035FE4C File Offset: 0x0035FE4C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DrawingRecord);
    }

    // Token: 0x060006DB RID: 1755 RVA: 0x0035FE5C File Offset: 0x0035FE5C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DrawingRecord other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Question>k__BackingField, other.<Question>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Program>k__BackingField, other.<Program>k__BackingField) && EqualityComparer<IReadOnlyList<DrawingRecord.Line>>.Default.Equals(this.<Lines>k__BackingField, other.<Lines>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Duration>k__BackingField, other.<Duration>k__BackingField) && EqualityComparer<ScreenCaptureFrame>.Default.Equals(this.<Screen>k__BackingField, other.<Screen>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CursorColorHex>k__BackingField, other.<CursorColorHex>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField) && EqualityComparer<byte[]>.Default.Equals(this.<Narration>k__BackingField, other.<Narration>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<NarrationSampleRate>k__BackingField, other.<NarrationSampleRate>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<KeepsInChat>k__BackingField, other.<KeepsInChat>k__BackingField));
    }

    // Token: 0x060006DD RID: 1757 RVA: 0x0035FFB0 File Offset: 0x0035FFB0
    [CompilerGenerated]
    private DrawingRecord(DrawingRecord original)
    {
      this.Question = original.<Question>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Program = original.<Program>k__BackingField;
      this.Lines = original.<Lines>k__BackingField;
      this.Duration = original.<Duration>k__BackingField;
      this.Screen = original.<Screen>k__BackingField;
      this.CursorColorHex = original.<CursorColorHex>k__BackingField;
      this.At = original.<At>k__BackingField;
      this.Narration = original.<Narration>k__BackingField;
      this.NarrationSampleRate = original.<NarrationSampleRate>k__BackingField;
      this.KeepsInChat = original.<KeepsInChat>k__BackingField;
    }

    // Token: 0x060006DE RID: 1758 RVA: 0x00360048 File Offset: 0x00360048
    [CompilerGenerated]
    public void Deconstruct(out string Question, out string Title, out string Program, out IReadOnlyList<DrawingRecord.Line> Lines, out double Duration, out ScreenCaptureFrame Screen, out string CursorColorHex, out DateTimeOffset At, [Nullable(2)] out byte[] Narration, out int NarrationSampleRate)
    {
      Question = this.Question;
      Title = this.Title;
      Program = this.Program;
      Lines = this.Lines;
      Duration = this.Duration;
      Screen = this.Screen;
      CursorColorHex = this.CursorColorHex;
      At = this.At;
      Narration = this.Narration;
      NarrationSampleRate = this.NarrationSampleRate;
    }

    // Token: 0x02000315 RID: 789
    [Nullable(0)]
    public sealed class Line : IEquatable<DrawingRecord.Line>
    {
      // Token: 0x06002799 RID: 10137 RVA: 0x004131B8 File Offset: 0x004131B8
      public Line(string Text, double Start, double? Length)
      {
        this.Text = Text;
        this.Start = Start;
        this.Length = Length;
        base..ctor();
      }

      // Token: 0x170007D4 RID: 2004
      // (get) Token: 0x0600279A RID: 10138 RVA: 0x004131D8 File Offset: 0x004131D8
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(DrawingRecord.Line);
        }
      }

      // Token: 0x170007D5 RID: 2005
      // (get) Token: 0x0600279B RID: 10139 RVA: 0x004131E4 File Offset: 0x004131E4
      // (set) Token: 0x0600279C RID: 10140 RVA: 0x004131EC File Offset: 0x004131EC
      public string Text { get; set; }

      // Token: 0x170007D6 RID: 2006
      // (get) Token: 0x0600279D RID: 10141 RVA: 0x004131F8 File Offset: 0x004131F8
      // (set) Token: 0x0600279E RID: 10142 RVA: 0x00413200 File Offset: 0x00413200
      public double Start { get; set; }

      // Token: 0x170007D7 RID: 2007
      // (get) Token: 0x0600279F RID: 10143 RVA: 0x0041320C File Offset: 0x0041320C
      // (set) Token: 0x060027A0 RID: 10144 RVA: 0x00413214 File Offset: 0x00413214
      public double? Length { get; set; }

      // Token: 0x060027A1 RID: 10145 RVA: 0x00413220 File Offset: 0x00413220
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Line");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060027A2 RID: 10146 RVA: 0x0041326C File Offset: 0x0041326C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Text = ");
        builder.Append(this.Text);
        builder.Append(", Start = ");
        builder.Append(this.Start.ToString());
        builder.Append(", Length = ");
        builder.Append(this.Length.ToString());
        return true;
      }

      // Token: 0x060027A3 RID: 10147 RVA: 0x004132E8 File Offset: 0x004132E8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(DrawingRecord.Line left, DrawingRecord.Line right)
      {
        return !(left == right);
      }

      // Token: 0x060027A4 RID: 10148 RVA: 0x004132F4 File Offset: 0x004132F4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(DrawingRecord.Line left, DrawingRecord.Line right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060027A5 RID: 10149 RVA: 0x00413308 File Offset: 0x00413308
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Start>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<Length>k__BackingField);
      }

      // Token: 0x060027A6 RID: 10150 RVA: 0x0041336C File Offset: 0x0041336C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as DrawingRecord.Line);
      }

      // Token: 0x060027A7 RID: 10151 RVA: 0x0041337C File Offset: 0x0041337C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(DrawingRecord.Line other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Start>k__BackingField, other.<Start>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<Length>k__BackingField, other.<Length>k__BackingField));
      }

      // Token: 0x060027A9 RID: 10153 RVA: 0x004133F8 File Offset: 0x004133F8
      [CompilerGenerated]
      private Line(DrawingRecord.Line original)
      {
        this.Text = original.<Text>k__BackingField;
        this.Start = original.<Start>k__BackingField;
        this.Length = original.<Length>k__BackingField;
      }

      // Token: 0x060027AA RID: 10154 RVA: 0x00413424 File Offset: 0x00413424
      [CompilerGenerated]
      public void Deconstruct(out string Text, out double Start, out double? Length)
      {
        Text = this.Text;
        Start = this.Start;
        Length = this.Length;
      }
    }
  }
}
