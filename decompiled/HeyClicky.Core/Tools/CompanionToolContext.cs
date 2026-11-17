using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Core.Voice;

namespace HeyClicky.Core.Tools
{
  // Token: 0x020000A3 RID: 163
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class CompanionToolContext : IEquatable<CompanionToolContext>
  {
    // Token: 0x06000A20 RID: 2592 RVA: 0x0037398C File Offset: 0x0037398C
    public CompanionToolContext(string UserWords, bool Typed, [Nullable(1)] IReadOnlyList<string> AttachedFilePaths, byte[] ScreenshotJpeg = null, string HighlightGesture = null, PageReference Page = null)
    {
      this.UserWords = UserWords;
      this.Typed = Typed;
      this.AttachedFilePaths = AttachedFilePaths;
      this.ScreenshotJpeg = ScreenshotJpeg;
      this.HighlightGesture = HighlightGesture;
      this.Page = Page;
      base..ctor();
    }

    // Token: 0x17000234 RID: 564
    // (get) Token: 0x06000A21 RID: 2593 RVA: 0x003739C4 File Offset: 0x003739C4
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(CompanionToolContext);
      }
    }

    // Token: 0x17000235 RID: 565
    // (get) Token: 0x06000A22 RID: 2594 RVA: 0x003739D0 File Offset: 0x003739D0
    // (set) Token: 0x06000A23 RID: 2595 RVA: 0x003739D8 File Offset: 0x003739D8
    public string UserWords { get; set; }

    // Token: 0x17000236 RID: 566
    // (get) Token: 0x06000A24 RID: 2596 RVA: 0x003739E4 File Offset: 0x003739E4
    // (set) Token: 0x06000A25 RID: 2597 RVA: 0x003739EC File Offset: 0x003739EC
    public bool Typed { get; set; }

    // Token: 0x17000237 RID: 567
    // (get) Token: 0x06000A26 RID: 2598 RVA: 0x003739F8 File Offset: 0x003739F8
    // (set) Token: 0x06000A27 RID: 2599 RVA: 0x00373A00 File Offset: 0x00373A00
    [Nullable(1)]
    public IReadOnlyList<string> AttachedFilePaths
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000238 RID: 568
    // (get) Token: 0x06000A28 RID: 2600 RVA: 0x00373A0C File Offset: 0x00373A0C
    // (set) Token: 0x06000A29 RID: 2601 RVA: 0x00373A14 File Offset: 0x00373A14
    public byte[] ScreenshotJpeg { get; set; }

    // Token: 0x17000239 RID: 569
    // (get) Token: 0x06000A2A RID: 2602 RVA: 0x00373A20 File Offset: 0x00373A20
    // (set) Token: 0x06000A2B RID: 2603 RVA: 0x00373A28 File Offset: 0x00373A28
    public string HighlightGesture { get; set; }

    // Token: 0x1700023A RID: 570
    // (get) Token: 0x06000A2C RID: 2604 RVA: 0x00373A34 File Offset: 0x00373A34
    // (set) Token: 0x06000A2D RID: 2605 RVA: 0x00373A3C File Offset: 0x00373A3C
    public PageReference Page { get; set; }

    // Token: 0x06000A2E RID: 2606 RVA: 0x00373A48 File Offset: 0x00373A48
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CompanionToolContext");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000A2F RID: 2607 RVA: 0x00373A94 File Offset: 0x00373A94
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("UserWords = ");
      builder.Append(this.UserWords);
      builder.Append(", Typed = ");
      builder.Append(this.Typed.ToString());
      builder.Append(", AttachedFilePaths = ");
      builder.Append(this.AttachedFilePaths);
      builder.Append(", ScreenshotJpeg = ");
      builder.Append(this.ScreenshotJpeg);
      builder.Append(", HighlightGesture = ");
      builder.Append(this.HighlightGesture);
      builder.Append(", Page = ");
      builder.Append(this.Page);
      return true;
    }

    // Token: 0x06000A30 RID: 2608 RVA: 0x00373B4C File Offset: 0x00373B4C
    [CompilerGenerated]
    public static bool operator !=(CompanionToolContext left, CompanionToolContext right)
    {
      return !(left == right);
    }

    // Token: 0x06000A31 RID: 2609 RVA: 0x00373B58 File Offset: 0x00373B58
    [CompilerGenerated]
    public static bool operator ==(CompanionToolContext left, CompanionToolContext right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000A32 RID: 2610 RVA: 0x00373B6C File Offset: 0x00373B6C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UserWords>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Typed>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<AttachedFilePaths>k__BackingField)) * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(this.<ScreenshotJpeg>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<HighlightGesture>k__BackingField)) * -1521134295 + EqualityComparer<PageReference>.Default.GetHashCode(this.<Page>k__BackingField);
    }

    // Token: 0x06000A33 RID: 2611 RVA: 0x00373C14 File Offset: 0x00373C14
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CompanionToolContext);
    }

    // Token: 0x06000A34 RID: 2612 RVA: 0x00373C24 File Offset: 0x00373C24
    [CompilerGenerated]
    public bool Equals(CompanionToolContext other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<UserWords>k__BackingField, other.<UserWords>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Typed>k__BackingField, other.<Typed>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<AttachedFilePaths>k__BackingField, other.<AttachedFilePaths>k__BackingField) && EqualityComparer<byte[]>.Default.Equals(this.<ScreenshotJpeg>k__BackingField, other.<ScreenshotJpeg>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<HighlightGesture>k__BackingField, other.<HighlightGesture>k__BackingField) && EqualityComparer<PageReference>.Default.Equals(this.<Page>k__BackingField, other.<Page>k__BackingField));
    }

    // Token: 0x06000A36 RID: 2614 RVA: 0x00373CF0 File Offset: 0x00373CF0
    [CompilerGenerated]
    private CompanionToolContext([Nullable(1)] CompanionToolContext original)
    {
      this.UserWords = original.<UserWords>k__BackingField;
      this.Typed = original.<Typed>k__BackingField;
      this.AttachedFilePaths = original.<AttachedFilePaths>k__BackingField;
      this.ScreenshotJpeg = original.<ScreenshotJpeg>k__BackingField;
      this.HighlightGesture = original.<HighlightGesture>k__BackingField;
      this.Page = original.<Page>k__BackingField;
    }

    // Token: 0x06000A37 RID: 2615 RVA: 0x00373D4C File Offset: 0x00373D4C
    [CompilerGenerated]
    public void Deconstruct(out string UserWords, out bool Typed, [Nullable(1)] out IReadOnlyList<string> AttachedFilePaths, out byte[] ScreenshotJpeg, out string HighlightGesture, out PageReference Page)
    {
      UserWords = this.UserWords;
      Typed = this.Typed;
      AttachedFilePaths = this.AttachedFilePaths;
      ScreenshotJpeg = this.ScreenshotJpeg;
      HighlightGesture = this.HighlightGesture;
      Page = this.Page;
    }

    // Token: 0x0400049F RID: 1183
    [Nullable(1)]
    public static readonly CompanionToolContext None = new CompanionToolContext(null, false, Array.Empty<string>(), null, null, null);
  }
}
