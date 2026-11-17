using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000188 RID: 392
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchUpdateRequest : IEquatable<NotchUpdateRequest>
  {
    // Token: 0x06001546 RID: 5446 RVA: 0x005B49F0 File Offset: 0x005B49F0
    public NotchUpdateRequest(string Title, string Detail, IReadOnlyList<NotchUpdateButton> Buttons, bool IsChromeAsk = false)
    {
      this.Title = Title;
      this.Detail = Detail;
      this.Buttons = Buttons;
      this.IsChromeAsk = IsChromeAsk;
      base..ctor();
    }

    // Token: 0x170003D2 RID: 978
    // (get) Token: 0x06001547 RID: 5447 RVA: 0x005B4A18 File Offset: 0x005B4A18
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(NotchUpdateRequest);
      }
    }

    // Token: 0x170003D3 RID: 979
    // (get) Token: 0x06001548 RID: 5448 RVA: 0x005B4A24 File Offset: 0x005B4A24
    // (set) Token: 0x06001549 RID: 5449 RVA: 0x005B4A2C File Offset: 0x005B4A2C
    public string Title { get; set; }

    // Token: 0x170003D4 RID: 980
    // (get) Token: 0x0600154A RID: 5450 RVA: 0x005B4A38 File Offset: 0x005B4A38
    // (set) Token: 0x0600154B RID: 5451 RVA: 0x005B4A40 File Offset: 0x005B4A40
    public string Detail { get; set; }

    // Token: 0x170003D5 RID: 981
    // (get) Token: 0x0600154C RID: 5452 RVA: 0x005B4A4C File Offset: 0x005B4A4C
    // (set) Token: 0x0600154D RID: 5453 RVA: 0x005B4A54 File Offset: 0x005B4A54
    public IReadOnlyList<NotchUpdateButton> Buttons { get; set; }

    // Token: 0x170003D6 RID: 982
    // (get) Token: 0x0600154E RID: 5454 RVA: 0x005B4A60 File Offset: 0x005B4A60
    // (set) Token: 0x0600154F RID: 5455 RVA: 0x005B4A68 File Offset: 0x005B4A68
    public bool IsChromeAsk { get; set; }

    // Token: 0x06001550 RID: 5456 RVA: 0x005B4A74 File Offset: 0x005B4A74
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("NotchUpdateRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001551 RID: 5457 RVA: 0x005B4AC0 File Offset: 0x005B4AC0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", Detail = ");
      builder.Append(this.Detail);
      builder.Append(", Buttons = ");
      builder.Append(this.Buttons);
      builder.Append(", IsChromeAsk = ");
      builder.Append(this.IsChromeAsk.ToString());
      return true;
    }

    // Token: 0x06001552 RID: 5458 RVA: 0x005B4B48 File Offset: 0x005B4B48
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(NotchUpdateRequest left, NotchUpdateRequest right)
    {
      return !(left == right);
    }

    // Token: 0x06001553 RID: 5459 RVA: 0x005B4B54 File Offset: 0x005B4B54
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(NotchUpdateRequest left, NotchUpdateRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001554 RID: 5460 RVA: 0x005B4B68 File Offset: 0x005B4B68
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Detail>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<NotchUpdateButton>>.Default.GetHashCode(this.<Buttons>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsChromeAsk>k__BackingField);
    }

    // Token: 0x06001555 RID: 5461 RVA: 0x005B4BE4 File Offset: 0x005B4BE4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as NotchUpdateRequest);
    }

    // Token: 0x06001556 RID: 5462 RVA: 0x005B4BF4 File Offset: 0x005B4BF4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(NotchUpdateRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Detail>k__BackingField, other.<Detail>k__BackingField) && EqualityComparer<IReadOnlyList<NotchUpdateButton>>.Default.Equals(this.<Buttons>k__BackingField, other.<Buttons>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsChromeAsk>k__BackingField, other.<IsChromeAsk>k__BackingField));
    }

    // Token: 0x06001558 RID: 5464 RVA: 0x005B4C88 File Offset: 0x005B4C88
    [CompilerGenerated]
    private NotchUpdateRequest(NotchUpdateRequest original)
    {
      this.Title = original.<Title>k__BackingField;
      this.Detail = original.<Detail>k__BackingField;
      this.Buttons = original.<Buttons>k__BackingField;
      this.IsChromeAsk = original.<IsChromeAsk>k__BackingField;
    }

    // Token: 0x06001559 RID: 5465 RVA: 0x005B4CC0 File Offset: 0x005B4CC0
    [CompilerGenerated]
    public void Deconstruct(out string Title, out string Detail, out IReadOnlyList<NotchUpdateButton> Buttons, out bool IsChromeAsk)
    {
      Title = this.Title;
      Detail = this.Detail;
      Buttons = this.Buttons;
      IsChromeAsk = this.IsChromeAsk;
    }
  }
}
