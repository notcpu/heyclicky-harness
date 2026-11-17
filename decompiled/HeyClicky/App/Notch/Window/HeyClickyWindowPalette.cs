using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using Microsoft.Win32;
using Windows.UI;

namespace HeyClicky.App.Notch.Window
{
  // Token: 0x02000146 RID: 326
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class HeyClickyWindowPalette : IEquatable<HeyClickyWindowPalette>
  {
    // Token: 0x0600126C RID: 4716 RVA: 0x0059709C File Offset: 0x0059709C
    public HeyClickyWindowPalette(bool IsDark, Color Ground, Color Ink, Color PrimaryText, Color SecondaryText, Color TertiaryText, Color AvatarPlate, Color CardPlate)
    {
      this.IsDark = IsDark;
      this.Ground = Ground;
      this.Ink = Ink;
      this.PrimaryText = PrimaryText;
      this.SecondaryText = SecondaryText;
      this.TertiaryText = TertiaryText;
      this.AvatarPlate = AvatarPlate;
      this.CardPlate = CardPlate;
      base..ctor();
    }

    // Token: 0x1700035A RID: 858
    // (get) Token: 0x0600126D RID: 4717 RVA: 0x005970EC File Offset: 0x005970EC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(HeyClickyWindowPalette);
      }
    }

    // Token: 0x1700035B RID: 859
    // (get) Token: 0x0600126E RID: 4718 RVA: 0x005970F8 File Offset: 0x005970F8
    // (set) Token: 0x0600126F RID: 4719 RVA: 0x00597100 File Offset: 0x00597100
    public bool IsDark { get; set; }

    // Token: 0x1700035C RID: 860
    // (get) Token: 0x06001270 RID: 4720 RVA: 0x0059710C File Offset: 0x0059710C
    // (set) Token: 0x06001271 RID: 4721 RVA: 0x00597114 File Offset: 0x00597114
    public Color Ground { get; set; }

    // Token: 0x1700035D RID: 861
    // (get) Token: 0x06001272 RID: 4722 RVA: 0x00597120 File Offset: 0x00597120
    // (set) Token: 0x06001273 RID: 4723 RVA: 0x00597128 File Offset: 0x00597128
    public Color Ink { get; set; }

    // Token: 0x1700035E RID: 862
    // (get) Token: 0x06001274 RID: 4724 RVA: 0x00597134 File Offset: 0x00597134
    // (set) Token: 0x06001275 RID: 4725 RVA: 0x0059713C File Offset: 0x0059713C
    public Color PrimaryText { get; set; }

    // Token: 0x1700035F RID: 863
    // (get) Token: 0x06001276 RID: 4726 RVA: 0x00597148 File Offset: 0x00597148
    // (set) Token: 0x06001277 RID: 4727 RVA: 0x00597150 File Offset: 0x00597150
    public Color SecondaryText { get; set; }

    // Token: 0x17000360 RID: 864
    // (get) Token: 0x06001278 RID: 4728 RVA: 0x0059715C File Offset: 0x0059715C
    // (set) Token: 0x06001279 RID: 4729 RVA: 0x00597164 File Offset: 0x00597164
    public Color TertiaryText { get; set; }

    // Token: 0x17000361 RID: 865
    // (get) Token: 0x0600127A RID: 4730 RVA: 0x00597170 File Offset: 0x00597170
    // (set) Token: 0x0600127B RID: 4731 RVA: 0x00597178 File Offset: 0x00597178
    public Color AvatarPlate { get; set; }

    // Token: 0x17000362 RID: 866
    // (get) Token: 0x0600127C RID: 4732 RVA: 0x00597184 File Offset: 0x00597184
    // (set) Token: 0x0600127D RID: 4733 RVA: 0x0059718C File Offset: 0x0059718C
    public Color CardPlate { get; set; }

    // Token: 0x0600127E RID: 4734 RVA: 0x00597198 File Offset: 0x00597198
    public Color InkAt(double alpha)
    {
      return this.Ink.WithAlpha(alpha);
    }

    // Token: 0x17000363 RID: 867
    // (get) Token: 0x0600127F RID: 4735 RVA: 0x005971A8 File Offset: 0x005971A8
    public MessagesPalette Messages
    {
      get
      {
        if (!this.IsDark)
        {
          return MessagesPalette.Light;
        }
        return MessagesPalette.Dark;
      }
    }

    // Token: 0x06001280 RID: 4736 RVA: 0x005971C0 File Offset: 0x005971C0
    public static HeyClickyWindowPalette Current()
    {
      string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_WINDOW_THEME");
      if (string.Equals(environmentVariable, "light", StringComparison.OrdinalIgnoreCase))
      {
        return HeyClickyWindowPalette.Light;
      }
      if (string.Equals(environmentVariable, "dark", StringComparison.OrdinalIgnoreCase))
      {
        return HeyClickyWindowPalette.Dark;
      }
      HeyClickyWindowPalette heyClickyWindowPalette;
      try
      {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize"))
        {
          object obj = ((registryKey != null) ? registryKey.GetValue("AppsUseLightTheme") : null);
          heyClickyWindowPalette = ((obj is int && (int)obj != 0) ? HeyClickyWindowPalette.Light : HeyClickyWindowPalette.Dark);
        }
      }
      catch (Exception)
      {
        heyClickyWindowPalette = HeyClickyWindowPalette.Dark;
      }
      return heyClickyWindowPalette;
    }

    // Token: 0x06001281 RID: 4737 RVA: 0x00597278 File Offset: 0x00597278
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("HeyClickyWindowPalette");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001282 RID: 4738 RVA: 0x005972C4 File Offset: 0x005972C4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("IsDark = ");
      builder.Append(this.IsDark.ToString());
      builder.Append(", Ground = ");
      builder.Append(this.Ground.ToString());
      builder.Append(", Ink = ");
      builder.Append(this.Ink.ToString());
      builder.Append(", PrimaryText = ");
      builder.Append(this.PrimaryText.ToString());
      builder.Append(", SecondaryText = ");
      builder.Append(this.SecondaryText.ToString());
      builder.Append(", TertiaryText = ");
      builder.Append(this.TertiaryText.ToString());
      builder.Append(", AvatarPlate = ");
      builder.Append(this.AvatarPlate.ToString());
      builder.Append(", CardPlate = ");
      builder.Append(this.CardPlate.ToString());
      builder.Append(", Messages = ");
      builder.Append(this.Messages);
      return true;
    }

    // Token: 0x06001283 RID: 4739 RVA: 0x00597428 File Offset: 0x00597428
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(HeyClickyWindowPalette left, HeyClickyWindowPalette right)
    {
      return !(left == right);
    }

    // Token: 0x06001284 RID: 4740 RVA: 0x00597434 File Offset: 0x00597434
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(HeyClickyWindowPalette left, HeyClickyWindowPalette right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001285 RID: 4741 RVA: 0x00597448 File Offset: 0x00597448
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDark>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Ground>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Ink>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<PrimaryText>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<SecondaryText>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<TertiaryText>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<AvatarPlate>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<CardPlate>k__BackingField);
    }

    // Token: 0x06001286 RID: 4742 RVA: 0x00597520 File Offset: 0x00597520
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as HeyClickyWindowPalette);
    }

    // Token: 0x06001287 RID: 4743 RVA: 0x00597530 File Offset: 0x00597530
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(HeyClickyWindowPalette other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<IsDark>k__BackingField, other.<IsDark>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Ground>k__BackingField, other.<Ground>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Ink>k__BackingField, other.<Ink>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<PrimaryText>k__BackingField, other.<PrimaryText>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<SecondaryText>k__BackingField, other.<SecondaryText>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<TertiaryText>k__BackingField, other.<TertiaryText>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<AvatarPlate>k__BackingField, other.<AvatarPlate>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<CardPlate>k__BackingField, other.<CardPlate>k__BackingField));
    }

    // Token: 0x06001289 RID: 4745 RVA: 0x00597630 File Offset: 0x00597630
    [CompilerGenerated]
    private HeyClickyWindowPalette(HeyClickyWindowPalette original)
    {
      this.IsDark = original.<IsDark>k__BackingField;
      this.Ground = original.<Ground>k__BackingField;
      this.Ink = original.<Ink>k__BackingField;
      this.PrimaryText = original.<PrimaryText>k__BackingField;
      this.SecondaryText = original.<SecondaryText>k__BackingField;
      this.TertiaryText = original.<TertiaryText>k__BackingField;
      this.AvatarPlate = original.<AvatarPlate>k__BackingField;
      this.CardPlate = original.<CardPlate>k__BackingField;
    }

    // Token: 0x0600128A RID: 4746 RVA: 0x005976A4 File Offset: 0x005976A4
    [CompilerGenerated]
    public void Deconstruct(out bool IsDark, out Color Ground, out Color Ink, out Color PrimaryText, out Color SecondaryText, out Color TertiaryText, out Color AvatarPlate, out Color CardPlate)
    {
      IsDark = this.IsDark;
      Ground = this.Ground;
      Ink = this.Ink;
      PrimaryText = this.PrimaryText;
      SecondaryText = this.SecondaryText;
      TertiaryText = this.TertiaryText;
      AvatarPlate = this.AvatarPlate;
      CardPlate = this.CardPlate;
    }

    // Token: 0x04000884 RID: 2180
    public static readonly HeyClickyWindowPalette Dark = new HeyClickyWindowPalette(true, DS.Hex(1315860U, 1.0), DS.White(1.0), HomeTheme.PrimaryText, HomeTheme.SecondaryText, HomeTheme.TertiaryText, HomeTheme.AvatarPlate, HomeTheme.IncomingBubble);

    // Token: 0x04000885 RID: 2181
    public static readonly HeyClickyWindowPalette Light = new HeyClickyWindowPalette(false, DS.Hex(15198185U, 1.0), DS.Black(1.0), DS.Hex(1579034U, 1.0), DS.Hex(7237235U, 1.0), DS.Hex(8421509U, 1.0), DS.Hex(13816534U, 1.0), DS.Hex(15329771U, 1.0));
  }
}
