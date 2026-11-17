using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace HeyClicky
{
  // Token: 0x0200000F RID: 15
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class HotkeyBinding : IEquatable<HotkeyBinding>
  {
    // Token: 0x060000A9 RID: 169 RVA: 0x0001DC68 File Offset: 0x0001DC68
    public HotkeyBinding(HotkeyModifiers Modifiers, HotkeyTrigger Trigger = HotkeyTrigger.Hold)
    {
      this.Modifiers = Modifiers;
      this.Trigger = Trigger;
      base..ctor();
    }

    // Token: 0x17000031 RID: 49
    // (get) Token: 0x060000AA RID: 170 RVA: 0x0001DC80 File Offset: 0x0001DC80
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(HotkeyBinding);
      }
    }

    // Token: 0x17000032 RID: 50
    // (get) Token: 0x060000AB RID: 171 RVA: 0x0001DC8C File Offset: 0x0001DC8C
    // (set) Token: 0x060000AC RID: 172 RVA: 0x0001DC94 File Offset: 0x0001DC94
    public HotkeyModifiers Modifiers { get; set; }

    // Token: 0x17000033 RID: 51
    // (get) Token: 0x060000AD RID: 173 RVA: 0x0001DCA0 File Offset: 0x0001DCA0
    // (set) Token: 0x060000AE RID: 174 RVA: 0x0001DCA8 File Offset: 0x0001DCA8
    public HotkeyTrigger Trigger { get; set; }

    // Token: 0x17000034 RID: 52
    // (get) Token: 0x060000AF RID: 175 RVA: 0x0001DCB4 File Offset: 0x0001DCB4
    [JsonIgnore]
    public IReadOnlyList<string> KeyNames
    {
      get
      {
        List<string> list = new List<string>(4);
        if (this.Modifiers.HasFlag(HotkeyModifiers.Control))
        {
          list.Add("Ctrl");
        }
        if (this.Modifiers.HasFlag(HotkeyModifiers.Alt))
        {
          list.Add("Alt");
        }
        if (this.Modifiers.HasFlag(HotkeyModifiers.Shift))
        {
          list.Add("Shift");
        }
        if (this.Modifiers.HasFlag(HotkeyModifiers.Win))
        {
          list.Add("Win");
        }
        return list;
      }
    }

    // Token: 0x17000035 RID: 53
    // (get) Token: 0x060000B0 RID: 176 RVA: 0x0001DD58 File Offset: 0x0001DD58
    [JsonIgnore]
    public string DisplayText
    {
      get
      {
        return string.Join(" + ", this.KeyNames) + ((this.Trigger == HotkeyTrigger.DoubleTap) ? ", pressed twice" : "");
      }
    }

    // Token: 0x060000B1 RID: 177 RVA: 0x0001DD84 File Offset: 0x0001DD84
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("HotkeyBinding");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060000B2 RID: 178 RVA: 0x0001DDD0 File Offset: 0x0001DDD0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Modifiers = ");
      builder.Append(this.Modifiers.ToString());
      builder.Append(", Trigger = ");
      builder.Append(this.Trigger.ToString());
      builder.Append(", KeyNames = ");
      builder.Append(this.KeyNames);
      builder.Append(", DisplayText = ");
      builder.Append(this.DisplayText);
      return true;
    }

    // Token: 0x060000B3 RID: 179 RVA: 0x0001DE64 File Offset: 0x0001DE64
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(HotkeyBinding left, HotkeyBinding right)
    {
      return !(left == right);
    }

    // Token: 0x060000B4 RID: 180 RVA: 0x0001DE70 File Offset: 0x0001DE70
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(HotkeyBinding left, HotkeyBinding right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060000B5 RID: 181 RVA: 0x0001DE84 File Offset: 0x0001DE84
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<HotkeyModifiers>.Default.GetHashCode(this.<Modifiers>k__BackingField)) * -1521134295 + EqualityComparer<HotkeyTrigger>.Default.GetHashCode(this.<Trigger>k__BackingField);
    }

    // Token: 0x060000B6 RID: 182 RVA: 0x0001DEC4 File Offset: 0x0001DEC4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as HotkeyBinding);
    }

    // Token: 0x060000B7 RID: 183 RVA: 0x0001DED4 File Offset: 0x0001DED4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(HotkeyBinding other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<HotkeyModifiers>.Default.Equals(this.<Modifiers>k__BackingField, other.<Modifiers>k__BackingField) && EqualityComparer<HotkeyTrigger>.Default.Equals(this.<Trigger>k__BackingField, other.<Trigger>k__BackingField));
    }

    // Token: 0x060000B9 RID: 185 RVA: 0x0001DF38 File Offset: 0x0001DF38
    [CompilerGenerated]
    private HotkeyBinding(HotkeyBinding original)
    {
      this.Modifiers = original.<Modifiers>k__BackingField;
      this.Trigger = original.<Trigger>k__BackingField;
    }

    // Token: 0x060000BA RID: 186 RVA: 0x0001DF58 File Offset: 0x0001DF58
    [CompilerGenerated]
    public void Deconstruct(out HotkeyModifiers Modifiers, out HotkeyTrigger Trigger)
    {
      Modifiers = this.Modifiers;
      Trigger = this.Trigger;
    }

    // Token: 0x04000047 RID: 71
    public static readonly HotkeyBinding PushToTalkDefault = new HotkeyBinding(HotkeyModifiers.Control | HotkeyModifiers.Alt, HotkeyTrigger.Hold);

    // Token: 0x04000048 RID: 72
    public static readonly HotkeyBinding TextModeDefault = new HotkeyBinding(HotkeyModifiers.Control, HotkeyTrigger.DoubleTap);

    // Token: 0x04000049 RID: 73
    public static readonly HotkeyBinding DictationDefault = new HotkeyBinding(HotkeyModifiers.Control | HotkeyModifiers.Win, HotkeyTrigger.Hold);

    // Token: 0x0400004A RID: 74
    public static readonly HotkeyBinding HandsFreeDictationDefault = new HotkeyBinding(HotkeyModifiers.Control | HotkeyModifiers.Win, HotkeyTrigger.DoubleTap);
  }
}
