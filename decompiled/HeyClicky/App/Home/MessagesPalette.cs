using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Design;
using Windows.UI;

namespace HeyClicky.App.Home
{
  // Token: 0x020002BA RID: 698
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MessagesPalette : IEquatable<MessagesPalette>
  {
    // Token: 0x06001CD3 RID: 7379 RVA: 0x005FC40C File Offset: 0x005FC40C
    public MessagesPalette(bool IsDark, Color PaneFill, Color SidebarFill, Color SearchFieldFill, Color RowDivider, Color SelectionBlue, Color OutgoingBubble, Color IncomingBubble, Color IncomingBubbleText, Color ComposerFieldFill, Color ComposerFieldStroke, Color ComposerButtonFill, Color ComposerButtonStroke, Color NamePillFill, Color AvatarPlate, Color PrimaryText, Color SecondaryText, Color TertiaryText, Color FieldGlyph, Color ToolbarGlyph, Color WindowControlGlyph, Color FailureRed, Color Ink, Color ThemeTextPrimary, Color ThemeTextSecondary, Color ThemeTextTertiary, Color ChatDots, Color WindowBackground, Color LinkBlue)
    {
      this.IsDark = IsDark;
      this.PaneFill = PaneFill;
      this.SidebarFill = SidebarFill;
      this.SearchFieldFill = SearchFieldFill;
      this.RowDivider = RowDivider;
      this.SelectionBlue = SelectionBlue;
      this.OutgoingBubble = OutgoingBubble;
      this.IncomingBubble = IncomingBubble;
      this.IncomingBubbleText = IncomingBubbleText;
      this.ComposerFieldFill = ComposerFieldFill;
      this.ComposerFieldStroke = ComposerFieldStroke;
      this.ComposerButtonFill = ComposerButtonFill;
      this.ComposerButtonStroke = ComposerButtonStroke;
      this.NamePillFill = NamePillFill;
      this.AvatarPlate = AvatarPlate;
      this.PrimaryText = PrimaryText;
      this.SecondaryText = SecondaryText;
      this.TertiaryText = TertiaryText;
      this.FieldGlyph = FieldGlyph;
      this.ToolbarGlyph = ToolbarGlyph;
      this.WindowControlGlyph = WindowControlGlyph;
      this.FailureRed = FailureRed;
      this.Ink = Ink;
      this.ThemeTextPrimary = ThemeTextPrimary;
      this.ThemeTextSecondary = ThemeTextSecondary;
      this.ThemeTextTertiary = ThemeTextTertiary;
      this.ChatDots = ChatDots;
      this.WindowBackground = WindowBackground;
      this.LinkBlue = LinkBlue;
      base..ctor();
    }

    // Token: 0x17000502 RID: 1282
    // (get) Token: 0x06001CD4 RID: 7380 RVA: 0x005FC504 File Offset: 0x005FC504
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(MessagesPalette);
      }
    }

    // Token: 0x17000503 RID: 1283
    // (get) Token: 0x06001CD5 RID: 7381 RVA: 0x005FC510 File Offset: 0x005FC510
    // (set) Token: 0x06001CD6 RID: 7382 RVA: 0x005FC518 File Offset: 0x005FC518
    public bool IsDark { get; set; }

    // Token: 0x17000504 RID: 1284
    // (get) Token: 0x06001CD7 RID: 7383 RVA: 0x005FC524 File Offset: 0x005FC524
    // (set) Token: 0x06001CD8 RID: 7384 RVA: 0x005FC52C File Offset: 0x005FC52C
    public Color PaneFill { get; set; }

    // Token: 0x17000505 RID: 1285
    // (get) Token: 0x06001CD9 RID: 7385 RVA: 0x005FC538 File Offset: 0x005FC538
    // (set) Token: 0x06001CDA RID: 7386 RVA: 0x005FC540 File Offset: 0x005FC540
    public Color SidebarFill { get; set; }

    // Token: 0x17000506 RID: 1286
    // (get) Token: 0x06001CDB RID: 7387 RVA: 0x005FC54C File Offset: 0x005FC54C
    // (set) Token: 0x06001CDC RID: 7388 RVA: 0x005FC554 File Offset: 0x005FC554
    public Color SearchFieldFill { get; set; }

    // Token: 0x17000507 RID: 1287
    // (get) Token: 0x06001CDD RID: 7389 RVA: 0x005FC560 File Offset: 0x005FC560
    // (set) Token: 0x06001CDE RID: 7390 RVA: 0x005FC568 File Offset: 0x005FC568
    public Color RowDivider { get; set; }

    // Token: 0x17000508 RID: 1288
    // (get) Token: 0x06001CDF RID: 7391 RVA: 0x005FC574 File Offset: 0x005FC574
    // (set) Token: 0x06001CE0 RID: 7392 RVA: 0x005FC57C File Offset: 0x005FC57C
    public Color SelectionBlue { get; set; }

    // Token: 0x17000509 RID: 1289
    // (get) Token: 0x06001CE1 RID: 7393 RVA: 0x005FC588 File Offset: 0x005FC588
    // (set) Token: 0x06001CE2 RID: 7394 RVA: 0x005FC590 File Offset: 0x005FC590
    public Color OutgoingBubble { get; set; }

    // Token: 0x1700050A RID: 1290
    // (get) Token: 0x06001CE3 RID: 7395 RVA: 0x005FC59C File Offset: 0x005FC59C
    // (set) Token: 0x06001CE4 RID: 7396 RVA: 0x005FC5A4 File Offset: 0x005FC5A4
    public Color IncomingBubble { get; set; }

    // Token: 0x1700050B RID: 1291
    // (get) Token: 0x06001CE5 RID: 7397 RVA: 0x005FC5B0 File Offset: 0x005FC5B0
    // (set) Token: 0x06001CE6 RID: 7398 RVA: 0x005FC5B8 File Offset: 0x005FC5B8
    public Color IncomingBubbleText { get; set; }

    // Token: 0x1700050C RID: 1292
    // (get) Token: 0x06001CE7 RID: 7399 RVA: 0x005FC5C4 File Offset: 0x005FC5C4
    // (set) Token: 0x06001CE8 RID: 7400 RVA: 0x005FC5CC File Offset: 0x005FC5CC
    public Color ComposerFieldFill { get; set; }

    // Token: 0x1700050D RID: 1293
    // (get) Token: 0x06001CE9 RID: 7401 RVA: 0x005FC5D8 File Offset: 0x005FC5D8
    // (set) Token: 0x06001CEA RID: 7402 RVA: 0x005FC5E0 File Offset: 0x005FC5E0
    public Color ComposerFieldStroke { get; set; }

    // Token: 0x1700050E RID: 1294
    // (get) Token: 0x06001CEB RID: 7403 RVA: 0x005FC5EC File Offset: 0x005FC5EC
    // (set) Token: 0x06001CEC RID: 7404 RVA: 0x005FC5F4 File Offset: 0x005FC5F4
    public Color ComposerButtonFill { get; set; }

    // Token: 0x1700050F RID: 1295
    // (get) Token: 0x06001CED RID: 7405 RVA: 0x005FC600 File Offset: 0x005FC600
    // (set) Token: 0x06001CEE RID: 7406 RVA: 0x005FC608 File Offset: 0x005FC608
    public Color ComposerButtonStroke { get; set; }

    // Token: 0x17000510 RID: 1296
    // (get) Token: 0x06001CEF RID: 7407 RVA: 0x005FC614 File Offset: 0x005FC614
    // (set) Token: 0x06001CF0 RID: 7408 RVA: 0x005FC61C File Offset: 0x005FC61C
    public Color NamePillFill { get; set; }

    // Token: 0x17000511 RID: 1297
    // (get) Token: 0x06001CF1 RID: 7409 RVA: 0x005FC628 File Offset: 0x005FC628
    // (set) Token: 0x06001CF2 RID: 7410 RVA: 0x005FC630 File Offset: 0x005FC630
    public Color AvatarPlate { get; set; }

    // Token: 0x17000512 RID: 1298
    // (get) Token: 0x06001CF3 RID: 7411 RVA: 0x005FC63C File Offset: 0x005FC63C
    // (set) Token: 0x06001CF4 RID: 7412 RVA: 0x005FC644 File Offset: 0x005FC644
    public Color PrimaryText { get; set; }

    // Token: 0x17000513 RID: 1299
    // (get) Token: 0x06001CF5 RID: 7413 RVA: 0x005FC650 File Offset: 0x005FC650
    // (set) Token: 0x06001CF6 RID: 7414 RVA: 0x005FC658 File Offset: 0x005FC658
    public Color SecondaryText { get; set; }

    // Token: 0x17000514 RID: 1300
    // (get) Token: 0x06001CF7 RID: 7415 RVA: 0x005FC664 File Offset: 0x005FC664
    // (set) Token: 0x06001CF8 RID: 7416 RVA: 0x005FC66C File Offset: 0x005FC66C
    public Color TertiaryText { get; set; }

    // Token: 0x17000515 RID: 1301
    // (get) Token: 0x06001CF9 RID: 7417 RVA: 0x005FC678 File Offset: 0x005FC678
    // (set) Token: 0x06001CFA RID: 7418 RVA: 0x005FC680 File Offset: 0x005FC680
    public Color FieldGlyph { get; set; }

    // Token: 0x17000516 RID: 1302
    // (get) Token: 0x06001CFB RID: 7419 RVA: 0x005FC68C File Offset: 0x005FC68C
    // (set) Token: 0x06001CFC RID: 7420 RVA: 0x005FC694 File Offset: 0x005FC694
    public Color ToolbarGlyph { get; set; }

    // Token: 0x17000517 RID: 1303
    // (get) Token: 0x06001CFD RID: 7421 RVA: 0x005FC6A0 File Offset: 0x005FC6A0
    // (set) Token: 0x06001CFE RID: 7422 RVA: 0x005FC6A8 File Offset: 0x005FC6A8
    public Color WindowControlGlyph { get; set; }

    // Token: 0x17000518 RID: 1304
    // (get) Token: 0x06001CFF RID: 7423 RVA: 0x005FC6B4 File Offset: 0x005FC6B4
    // (set) Token: 0x06001D00 RID: 7424 RVA: 0x005FC6BC File Offset: 0x005FC6BC
    public Color FailureRed { get; set; }

    // Token: 0x17000519 RID: 1305
    // (get) Token: 0x06001D01 RID: 7425 RVA: 0x005FC6C8 File Offset: 0x005FC6C8
    // (set) Token: 0x06001D02 RID: 7426 RVA: 0x005FC6D0 File Offset: 0x005FC6D0
    public Color Ink { get; set; }

    // Token: 0x1700051A RID: 1306
    // (get) Token: 0x06001D03 RID: 7427 RVA: 0x005FC6DC File Offset: 0x005FC6DC
    // (set) Token: 0x06001D04 RID: 7428 RVA: 0x005FC6E4 File Offset: 0x005FC6E4
    public Color ThemeTextPrimary { get; set; }

    // Token: 0x1700051B RID: 1307
    // (get) Token: 0x06001D05 RID: 7429 RVA: 0x005FC6F0 File Offset: 0x005FC6F0
    // (set) Token: 0x06001D06 RID: 7430 RVA: 0x005FC6F8 File Offset: 0x005FC6F8
    public Color ThemeTextSecondary { get; set; }

    // Token: 0x1700051C RID: 1308
    // (get) Token: 0x06001D07 RID: 7431 RVA: 0x005FC704 File Offset: 0x005FC704
    // (set) Token: 0x06001D08 RID: 7432 RVA: 0x005FC70C File Offset: 0x005FC70C
    public Color ThemeTextTertiary { get; set; }

    // Token: 0x1700051D RID: 1309
    // (get) Token: 0x06001D09 RID: 7433 RVA: 0x005FC718 File Offset: 0x005FC718
    // (set) Token: 0x06001D0A RID: 7434 RVA: 0x005FC720 File Offset: 0x005FC720
    public Color ChatDots { get; set; }

    // Token: 0x1700051E RID: 1310
    // (get) Token: 0x06001D0B RID: 7435 RVA: 0x005FC72C File Offset: 0x005FC72C
    // (set) Token: 0x06001D0C RID: 7436 RVA: 0x005FC734 File Offset: 0x005FC734
    public Color WindowBackground { get; set; }

    // Token: 0x1700051F RID: 1311
    // (get) Token: 0x06001D0D RID: 7437 RVA: 0x005FC740 File Offset: 0x005FC740
    // (set) Token: 0x06001D0E RID: 7438 RVA: 0x005FC748 File Offset: 0x005FC748
    public Color LinkBlue { get; set; }

    // Token: 0x06001D0F RID: 7439 RVA: 0x005FC754 File Offset: 0x005FC754
    public Color InkAt(double alpha)
    {
      return this.Ink.WithAlpha(alpha);
    }

    // Token: 0x17000520 RID: 1312
    // (get) Token: 0x06001D10 RID: 7440 RVA: 0x005FC764 File Offset: 0x005FC764
    public static MessagesPalette Current
    {
      get
      {
        return MessagesPalette.current ?? MessagesPalette.Dark;
      }
    }

    // Token: 0x06001D11 RID: 7441 RVA: 0x005FC774 File Offset: 0x005FC774
    public static MessagesPalette.Scope Use(MessagesPalette palette)
    {
      MessagesPalette messagesPalette = MessagesPalette.current;
      MessagesPalette.current = palette;
      return new MessagesPalette.Scope(messagesPalette);
    }

    // Token: 0x06001D12 RID: 7442 RVA: 0x005FC788 File Offset: 0x005FC788
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("MessagesPalette");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001D13 RID: 7443 RVA: 0x005FC7D4 File Offset: 0x005FC7D4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("IsDark = ");
      builder.Append(this.IsDark.ToString());
      builder.Append(", PaneFill = ");
      builder.Append(this.PaneFill.ToString());
      builder.Append(", SidebarFill = ");
      builder.Append(this.SidebarFill.ToString());
      builder.Append(", SearchFieldFill = ");
      builder.Append(this.SearchFieldFill.ToString());
      builder.Append(", RowDivider = ");
      builder.Append(this.RowDivider.ToString());
      builder.Append(", SelectionBlue = ");
      builder.Append(this.SelectionBlue.ToString());
      builder.Append(", OutgoingBubble = ");
      builder.Append(this.OutgoingBubble.ToString());
      builder.Append(", IncomingBubble = ");
      builder.Append(this.IncomingBubble.ToString());
      builder.Append(", IncomingBubbleText = ");
      builder.Append(this.IncomingBubbleText.ToString());
      builder.Append(", ComposerFieldFill = ");
      builder.Append(this.ComposerFieldFill.ToString());
      builder.Append(", ComposerFieldStroke = ");
      builder.Append(this.ComposerFieldStroke.ToString());
      builder.Append(", ComposerButtonFill = ");
      builder.Append(this.ComposerButtonFill.ToString());
      builder.Append(", ComposerButtonStroke = ");
      builder.Append(this.ComposerButtonStroke.ToString());
      builder.Append(", NamePillFill = ");
      builder.Append(this.NamePillFill.ToString());
      builder.Append(", AvatarPlate = ");
      builder.Append(this.AvatarPlate.ToString());
      builder.Append(", PrimaryText = ");
      builder.Append(this.PrimaryText.ToString());
      builder.Append(", SecondaryText = ");
      builder.Append(this.SecondaryText.ToString());
      builder.Append(", TertiaryText = ");
      builder.Append(this.TertiaryText.ToString());
      builder.Append(", FieldGlyph = ");
      builder.Append(this.FieldGlyph.ToString());
      builder.Append(", ToolbarGlyph = ");
      builder.Append(this.ToolbarGlyph.ToString());
      builder.Append(", WindowControlGlyph = ");
      builder.Append(this.WindowControlGlyph.ToString());
      builder.Append(", FailureRed = ");
      builder.Append(this.FailureRed.ToString());
      builder.Append(", Ink = ");
      builder.Append(this.Ink.ToString());
      builder.Append(", ThemeTextPrimary = ");
      builder.Append(this.ThemeTextPrimary.ToString());
      builder.Append(", ThemeTextSecondary = ");
      builder.Append(this.ThemeTextSecondary.ToString());
      builder.Append(", ThemeTextTertiary = ");
      builder.Append(this.ThemeTextTertiary.ToString());
      builder.Append(", ChatDots = ");
      builder.Append(this.ChatDots.ToString());
      builder.Append(", WindowBackground = ");
      builder.Append(this.WindowBackground.ToString());
      builder.Append(", LinkBlue = ");
      builder.Append(this.LinkBlue.ToString());
      return true;
    }

    // Token: 0x06001D14 RID: 7444 RVA: 0x005FCC54 File Offset: 0x005FCC54
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(MessagesPalette left, MessagesPalette right)
    {
      return !(left == right);
    }

    // Token: 0x06001D15 RID: 7445 RVA: 0x005FCC60 File Offset: 0x005FCC60
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(MessagesPalette left, MessagesPalette right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001D16 RID: 7446 RVA: 0x005FCC74 File Offset: 0x005FCC74
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((((((((((((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDark>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<PaneFill>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<SidebarFill>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<SearchFieldFill>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<RowDivider>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<SelectionBlue>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<OutgoingBubble>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<IncomingBubble>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<IncomingBubbleText>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ComposerFieldFill>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ComposerFieldStroke>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ComposerButtonFill>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ComposerButtonStroke>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<NamePillFill>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<AvatarPlate>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<PrimaryText>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<SecondaryText>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<TertiaryText>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<FieldGlyph>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ToolbarGlyph>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<WindowControlGlyph>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<FailureRed>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Ink>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ThemeTextPrimary>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ThemeTextSecondary>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ThemeTextTertiary>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ChatDots>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<WindowBackground>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<LinkBlue>k__BackingField);
    }

    // Token: 0x06001D17 RID: 7447 RVA: 0x005FCF2C File Offset: 0x005FCF2C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as MessagesPalette);
    }

    // Token: 0x06001D18 RID: 7448 RVA: 0x005FCF3C File Offset: 0x005FCF3C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(MessagesPalette other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<IsDark>k__BackingField, other.<IsDark>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<PaneFill>k__BackingField, other.<PaneFill>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<SidebarFill>k__BackingField, other.<SidebarFill>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<SearchFieldFill>k__BackingField, other.<SearchFieldFill>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<RowDivider>k__BackingField, other.<RowDivider>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<SelectionBlue>k__BackingField, other.<SelectionBlue>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<OutgoingBubble>k__BackingField, other.<OutgoingBubble>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<IncomingBubble>k__BackingField, other.<IncomingBubble>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<IncomingBubbleText>k__BackingField, other.<IncomingBubbleText>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ComposerFieldFill>k__BackingField, other.<ComposerFieldFill>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ComposerFieldStroke>k__BackingField, other.<ComposerFieldStroke>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ComposerButtonFill>k__BackingField, other.<ComposerButtonFill>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ComposerButtonStroke>k__BackingField, other.<ComposerButtonStroke>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<NamePillFill>k__BackingField, other.<NamePillFill>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<AvatarPlate>k__BackingField, other.<AvatarPlate>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<PrimaryText>k__BackingField, other.<PrimaryText>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<SecondaryText>k__BackingField, other.<SecondaryText>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<TertiaryText>k__BackingField, other.<TertiaryText>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<FieldGlyph>k__BackingField, other.<FieldGlyph>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ToolbarGlyph>k__BackingField, other.<ToolbarGlyph>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<WindowControlGlyph>k__BackingField, other.<WindowControlGlyph>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<FailureRed>k__BackingField, other.<FailureRed>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Ink>k__BackingField, other.<Ink>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ThemeTextPrimary>k__BackingField, other.<ThemeTextPrimary>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ThemeTextSecondary>k__BackingField, other.<ThemeTextSecondary>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ThemeTextTertiary>k__BackingField, other.<ThemeTextTertiary>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ChatDots>k__BackingField, other.<ChatDots>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<WindowBackground>k__BackingField, other.<WindowBackground>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<LinkBlue>k__BackingField, other.<LinkBlue>k__BackingField));
    }

    // Token: 0x06001D1A RID: 7450 RVA: 0x005FD274 File Offset: 0x005FD274
    [CompilerGenerated]
    private MessagesPalette(MessagesPalette original)
    {
      this.IsDark = original.<IsDark>k__BackingField;
      this.PaneFill = original.<PaneFill>k__BackingField;
      this.SidebarFill = original.<SidebarFill>k__BackingField;
      this.SearchFieldFill = original.<SearchFieldFill>k__BackingField;
      this.RowDivider = original.<RowDivider>k__BackingField;
      this.SelectionBlue = original.<SelectionBlue>k__BackingField;
      this.OutgoingBubble = original.<OutgoingBubble>k__BackingField;
      this.IncomingBubble = original.<IncomingBubble>k__BackingField;
      this.IncomingBubbleText = original.<IncomingBubbleText>k__BackingField;
      this.ComposerFieldFill = original.<ComposerFieldFill>k__BackingField;
      this.ComposerFieldStroke = original.<ComposerFieldStroke>k__BackingField;
      this.ComposerButtonFill = original.<ComposerButtonFill>k__BackingField;
      this.ComposerButtonStroke = original.<ComposerButtonStroke>k__BackingField;
      this.NamePillFill = original.<NamePillFill>k__BackingField;
      this.AvatarPlate = original.<AvatarPlate>k__BackingField;
      this.PrimaryText = original.<PrimaryText>k__BackingField;
      this.SecondaryText = original.<SecondaryText>k__BackingField;
      this.TertiaryText = original.<TertiaryText>k__BackingField;
      this.FieldGlyph = original.<FieldGlyph>k__BackingField;
      this.ToolbarGlyph = original.<ToolbarGlyph>k__BackingField;
      this.WindowControlGlyph = original.<WindowControlGlyph>k__BackingField;
      this.FailureRed = original.<FailureRed>k__BackingField;
      this.Ink = original.<Ink>k__BackingField;
      this.ThemeTextPrimary = original.<ThemeTextPrimary>k__BackingField;
      this.ThemeTextSecondary = original.<ThemeTextSecondary>k__BackingField;
      this.ThemeTextTertiary = original.<ThemeTextTertiary>k__BackingField;
      this.ChatDots = original.<ChatDots>k__BackingField;
      this.WindowBackground = original.<WindowBackground>k__BackingField;
      this.LinkBlue = original.<LinkBlue>k__BackingField;
    }

    // Token: 0x06001D1B RID: 7451 RVA: 0x005FD3E4 File Offset: 0x005FD3E4
    [CompilerGenerated]
    public void Deconstruct(out bool IsDark, out Color PaneFill, out Color SidebarFill, out Color SearchFieldFill, out Color RowDivider, out Color SelectionBlue, out Color OutgoingBubble, out Color IncomingBubble, out Color IncomingBubbleText, out Color ComposerFieldFill, out Color ComposerFieldStroke, out Color ComposerButtonFill, out Color ComposerButtonStroke, out Color NamePillFill, out Color AvatarPlate, out Color PrimaryText, out Color SecondaryText, out Color TertiaryText, out Color FieldGlyph, out Color ToolbarGlyph, out Color WindowControlGlyph, out Color FailureRed, out Color Ink, out Color ThemeTextPrimary, out Color ThemeTextSecondary, out Color ThemeTextTertiary, out Color ChatDots, out Color WindowBackground, out Color LinkBlue)
    {
      IsDark = this.IsDark;
      PaneFill = this.PaneFill;
      SidebarFill = this.SidebarFill;
      SearchFieldFill = this.SearchFieldFill;
      RowDivider = this.RowDivider;
      SelectionBlue = this.SelectionBlue;
      OutgoingBubble = this.OutgoingBubble;
      IncomingBubble = this.IncomingBubble;
      IncomingBubbleText = this.IncomingBubbleText;
      ComposerFieldFill = this.ComposerFieldFill;
      ComposerFieldStroke = this.ComposerFieldStroke;
      ComposerButtonFill = this.ComposerButtonFill;
      ComposerButtonStroke = this.ComposerButtonStroke;
      NamePillFill = this.NamePillFill;
      AvatarPlate = this.AvatarPlate;
      PrimaryText = this.PrimaryText;
      SecondaryText = this.SecondaryText;
      TertiaryText = this.TertiaryText;
      FieldGlyph = this.FieldGlyph;
      ToolbarGlyph = this.ToolbarGlyph;
      WindowControlGlyph = this.WindowControlGlyph;
      FailureRed = this.FailureRed;
      Ink = this.Ink;
      ThemeTextPrimary = this.ThemeTextPrimary;
      ThemeTextSecondary = this.ThemeTextSecondary;
      ThemeTextTertiary = this.ThemeTextTertiary;
      ChatDots = this.ChatDots;
      WindowBackground = this.WindowBackground;
      LinkBlue = this.LinkBlue;
    }

    // Token: 0x04000DED RID: 3565
    public static readonly MessagesPalette Dark = new MessagesPalette(true, HomeTheme.PaneFill, HomeTheme.SidebarFill, HomeTheme.SearchFieldFill, HomeTheme.RowDivider, HomeTheme.SelectionBlue, DS.Rgb(0.1607843137254902, 0.592156862745098, 1.0, 1.0), HomeTheme.IncomingBubble, DS.Hex(16777215U, 1.0), HomeTheme.ComposerFieldFill, HomeTheme.ComposerFieldStroke, DS.Hex(2236962U, 1.0), DS.Hex(4079166U, 1.0), HomeTheme.NamePillFill, HomeTheme.AvatarPlate, HomeTheme.PrimaryText, HomeTheme.SecondaryText, HomeTheme.TertiaryText, DS.Hex(8750469U, 1.0), HomeTheme.ToolbarGlyph, HomeTheme.WindowControlGlyph, HomeTheme.FailureRed, DS.Hex(16777215U, 1.0), HomeTheme.ThemeTextPrimary, HomeTheme.ThemeTextSecondary, HomeTheme.ThemeTextTertiary, DS.White(0.07), DS.Hex(0U, 1.0), DS.Colors.Blue300);

    // Token: 0x04000DEE RID: 3566
    public static readonly MessagesPalette Light = new MessagesPalette(false, DS.Hex(16777215U, 1.0), DS.Hex(15987698U, 1.0), DS.Hex(15132390U, 1.0), DS.Hex(14869218U, 1.0), DS.Hex(31487U, 1.0), DS.Rgb(0.1607843137254902, 0.592156862745098, 1.0, 1.0), DS.Hex(15329771U, 1.0), DS.Hex(1315862U, 1.0), DS.Hex(16777215U, 1.0), DS.Hex(14079702U, 1.0), DS.Hex(15921906U, 1.0), DS.Hex(14342874U, 1.0), DS.Hex(15527148U, 1.0), DS.Hex(13816534U, 1.0), DS.Hex(1579034U, 1.0), DS.Hex(7237235U, 1.0), DS.Hex(8421509U, 1.0), DS.Hex(9211025U, 1.0), DS.Hex(3947584U, 1.0), DS.Hex(9211025U, 1.0), DS.Hex(16726832U, 1.0), DS.Hex(0U, 1.0), DS.Rgb(0.106, 0.11, 0.122, 1.0), DS.Rgb(0.353, 0.365, 0.4, 1.0), DS.Rgb(0.557, 0.565, 0.6, 1.0), DS.Black(0.11), DS.Rgb(0.956, 0.956, 0.964, 1.0), DS.Rgb(0.0, 0.4, 0.86, 1.0));

    // Token: 0x04000DEF RID: 3567
    [Nullable(2)]
    [ThreadStatic]
    private static MessagesPalette current;

    // Token: 0x020008B0 RID: 2224
    [NullableContext(0)]
    public readonly struct Scope : IDisposable
    {
      // Token: 0x06003A59 RID: 14937 RVA: 0x0068D904 File Offset: 0x0068D904
      [NullableContext(2)]
      public Scope(MessagesPalette before)
      {
        this.<before>P = before;
      }

      // Token: 0x06003A5A RID: 14938 RVA: 0x0068D910 File Offset: 0x0068D910
      public void Dispose()
      {
        MessagesPalette.current = this.<before>P;
      }

      // Token: 0x040024B6 RID: 9398
      [Nullable(2)]
      [CompilerGenerated]
      private readonly MessagesPalette <before>P;
    }
  }
}
