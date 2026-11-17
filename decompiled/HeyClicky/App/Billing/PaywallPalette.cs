using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Design;
using Microsoft.Win32;
using Windows.UI;

namespace HeyClicky.App.Billing
{
  // Token: 0x02000367 RID: 871
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PaywallPalette : IEquatable<PaywallPalette>
  {
    // Token: 0x0600261C RID: 9756 RVA: 0x0063F660 File Offset: 0x0063F660
    public PaywallPalette(bool IsDark, Color Accent, Color ActionInk, Color CardSurface, Color CardBorder, Color BenefitSurface, Color BenefitBorder, Color Primary, Color Secondary, Color ErrorText)
    {
      this.IsDark = IsDark;
      this.Accent = Accent;
      this.ActionInk = ActionInk;
      this.CardSurface = CardSurface;
      this.CardBorder = CardBorder;
      this.BenefitSurface = BenefitSurface;
      this.BenefitBorder = BenefitBorder;
      this.Primary = Primary;
      this.Secondary = Secondary;
      this.ErrorText = ErrorText;
      base..ctor();
    }

    // Token: 0x170006C4 RID: 1732
    // (get) Token: 0x0600261D RID: 9757 RVA: 0x0063F6C0 File Offset: 0x0063F6C0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PaywallPalette);
      }
    }

    // Token: 0x170006C5 RID: 1733
    // (get) Token: 0x0600261E RID: 9758 RVA: 0x0063F6CC File Offset: 0x0063F6CC
    // (set) Token: 0x0600261F RID: 9759 RVA: 0x0063F6D4 File Offset: 0x0063F6D4
    public bool IsDark { get; set; }

    // Token: 0x170006C6 RID: 1734
    // (get) Token: 0x06002620 RID: 9760 RVA: 0x0063F6E0 File Offset: 0x0063F6E0
    // (set) Token: 0x06002621 RID: 9761 RVA: 0x0063F6E8 File Offset: 0x0063F6E8
    public Color Accent { get; set; }

    // Token: 0x170006C7 RID: 1735
    // (get) Token: 0x06002622 RID: 9762 RVA: 0x0063F6F4 File Offset: 0x0063F6F4
    // (set) Token: 0x06002623 RID: 9763 RVA: 0x0063F6FC File Offset: 0x0063F6FC
    public Color ActionInk { get; set; }

    // Token: 0x170006C8 RID: 1736
    // (get) Token: 0x06002624 RID: 9764 RVA: 0x0063F708 File Offset: 0x0063F708
    // (set) Token: 0x06002625 RID: 9765 RVA: 0x0063F710 File Offset: 0x0063F710
    public Color CardSurface { get; set; }

    // Token: 0x170006C9 RID: 1737
    // (get) Token: 0x06002626 RID: 9766 RVA: 0x0063F71C File Offset: 0x0063F71C
    // (set) Token: 0x06002627 RID: 9767 RVA: 0x0063F724 File Offset: 0x0063F724
    public Color CardBorder { get; set; }

    // Token: 0x170006CA RID: 1738
    // (get) Token: 0x06002628 RID: 9768 RVA: 0x0063F730 File Offset: 0x0063F730
    // (set) Token: 0x06002629 RID: 9769 RVA: 0x0063F738 File Offset: 0x0063F738
    public Color BenefitSurface { get; set; }

    // Token: 0x170006CB RID: 1739
    // (get) Token: 0x0600262A RID: 9770 RVA: 0x0063F744 File Offset: 0x0063F744
    // (set) Token: 0x0600262B RID: 9771 RVA: 0x0063F74C File Offset: 0x0063F74C
    public Color BenefitBorder { get; set; }

    // Token: 0x170006CC RID: 1740
    // (get) Token: 0x0600262C RID: 9772 RVA: 0x0063F758 File Offset: 0x0063F758
    // (set) Token: 0x0600262D RID: 9773 RVA: 0x0063F760 File Offset: 0x0063F760
    public Color Primary { get; set; }

    // Token: 0x170006CD RID: 1741
    // (get) Token: 0x0600262E RID: 9774 RVA: 0x0063F76C File Offset: 0x0063F76C
    // (set) Token: 0x0600262F RID: 9775 RVA: 0x0063F774 File Offset: 0x0063F774
    public Color Secondary { get; set; }

    // Token: 0x170006CE RID: 1742
    // (get) Token: 0x06002630 RID: 9776 RVA: 0x0063F780 File Offset: 0x0063F780
    // (set) Token: 0x06002631 RID: 9777 RVA: 0x0063F788 File Offset: 0x0063F788
    public Color ErrorText { get; set; }

    // Token: 0x06002632 RID: 9778 RVA: 0x0063F794 File Offset: 0x0063F794
    public static PaywallPalette Current()
    {
      string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_PAYWALL_THEME");
      if (string.Equals(environmentVariable, "light", StringComparison.OrdinalIgnoreCase))
      {
        return PaywallPalette.Light;
      }
      if (string.Equals(environmentVariable, "dark", StringComparison.OrdinalIgnoreCase))
      {
        return PaywallPalette.Dark;
      }
      PaywallPalette paywallPalette;
      try
      {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize"))
        {
          object obj = ((registryKey != null) ? registryKey.GetValue("AppsUseLightTheme") : null);
          paywallPalette = ((obj is int && (int)obj != 0) ? PaywallPalette.Light : PaywallPalette.Dark);
        }
      }
      catch (Exception)
      {
        paywallPalette = PaywallPalette.Dark;
      }
      return paywallPalette;
    }

    // Token: 0x06002633 RID: 9779 RVA: 0x0063F84C File Offset: 0x0063F84C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PaywallPalette");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06002634 RID: 9780 RVA: 0x0063F898 File Offset: 0x0063F898
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("IsDark = ");
      builder.Append(this.IsDark.ToString());
      builder.Append(", Accent = ");
      builder.Append(this.Accent.ToString());
      builder.Append(", ActionInk = ");
      builder.Append(this.ActionInk.ToString());
      builder.Append(", CardSurface = ");
      builder.Append(this.CardSurface.ToString());
      builder.Append(", CardBorder = ");
      builder.Append(this.CardBorder.ToString());
      builder.Append(", BenefitSurface = ");
      builder.Append(this.BenefitSurface.ToString());
      builder.Append(", BenefitBorder = ");
      builder.Append(this.BenefitBorder.ToString());
      builder.Append(", Primary = ");
      builder.Append(this.Primary.ToString());
      builder.Append(", Secondary = ");
      builder.Append(this.Secondary.ToString());
      builder.Append(", ErrorText = ");
      builder.Append(this.ErrorText.ToString());
      return true;
    }

    // Token: 0x06002635 RID: 9781 RVA: 0x0063FA34 File Offset: 0x0063FA34
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PaywallPalette left, PaywallPalette right)
    {
      return !(left == right);
    }

    // Token: 0x06002636 RID: 9782 RVA: 0x0063FA40 File Offset: 0x0063FA40
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PaywallPalette left, PaywallPalette right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06002637 RID: 9783 RVA: 0x0063FA54 File Offset: 0x0063FA54
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDark>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Accent>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ActionInk>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<CardSurface>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<CardBorder>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<BenefitSurface>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<BenefitBorder>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Primary>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Secondary>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ErrorText>k__BackingField);
    }

    // Token: 0x06002638 RID: 9784 RVA: 0x0063FB58 File Offset: 0x0063FB58
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PaywallPalette);
    }

    // Token: 0x06002639 RID: 9785 RVA: 0x0063FB68 File Offset: 0x0063FB68
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PaywallPalette other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<IsDark>k__BackingField, other.<IsDark>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Accent>k__BackingField, other.<Accent>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ActionInk>k__BackingField, other.<ActionInk>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<CardSurface>k__BackingField, other.<CardSurface>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<CardBorder>k__BackingField, other.<CardBorder>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<BenefitSurface>k__BackingField, other.<BenefitSurface>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<BenefitBorder>k__BackingField, other.<BenefitBorder>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Primary>k__BackingField, other.<Primary>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Secondary>k__BackingField, other.<Secondary>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<ErrorText>k__BackingField, other.<ErrorText>k__BackingField));
    }

    // Token: 0x0600263B RID: 9787 RVA: 0x0063FCA0 File Offset: 0x0063FCA0
    [CompilerGenerated]
    private PaywallPalette(PaywallPalette original)
    {
      this.IsDark = original.<IsDark>k__BackingField;
      this.Accent = original.<Accent>k__BackingField;
      this.ActionInk = original.<ActionInk>k__BackingField;
      this.CardSurface = original.<CardSurface>k__BackingField;
      this.CardBorder = original.<CardBorder>k__BackingField;
      this.BenefitSurface = original.<BenefitSurface>k__BackingField;
      this.BenefitBorder = original.<BenefitBorder>k__BackingField;
      this.Primary = original.<Primary>k__BackingField;
      this.Secondary = original.<Secondary>k__BackingField;
      this.ErrorText = original.<ErrorText>k__BackingField;
    }

    // Token: 0x0600263C RID: 9788 RVA: 0x0063FD2C File Offset: 0x0063FD2C
    [CompilerGenerated]
    public void Deconstruct(out bool IsDark, out Color Accent, out Color ActionInk, out Color CardSurface, out Color CardBorder, out Color BenefitSurface, out Color BenefitBorder, out Color Primary, out Color Secondary, out Color ErrorText)
    {
      IsDark = this.IsDark;
      Accent = this.Accent;
      ActionInk = this.ActionInk;
      CardSurface = this.CardSurface;
      CardBorder = this.CardBorder;
      BenefitSurface = this.BenefitSurface;
      BenefitBorder = this.BenefitBorder;
      Primary = this.Primary;
      Secondary = this.Secondary;
      ErrorText = this.ErrorText;
    }

    // Token: 0x04001211 RID: 4625
    public static readonly PaywallPalette Dark = new PaywallPalette(true, DS.Hex(10668031U, 1.0), DS.Hex(1518925U, 1.0), DS.Hex(1645087U, 1.0), DS.White(0.1), DS.White(0.055), DS.White(0.09), DS.White(0.85), DS.White(0.55), DS.Rgb(0.95, 0.45, 0.45, 1.0));

    // Token: 0x04001212 RID: 4626
    public static readonly PaywallPalette Light = new PaywallPalette(false, DS.Hex(3168395U, 1.0), DS.Hex(1518925U, 1.0), DS.Hex(16448511U, 1.0), DS.Black(0.12), DS.Hex(16054271U, 1.0), DS.Hex(2846719U, 0.14), DS.Black(0.85), DS.Black(0.5), DS.Rgb(0.95, 0.45, 0.45, 1.0));
  }
}
