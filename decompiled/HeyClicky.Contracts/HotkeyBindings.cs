using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000010 RID: 16
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class HotkeyBindings : IEquatable<HotkeyBindings>
  {
    // Token: 0x060000BC RID: 188 RVA: 0x0001DFA0 File Offset: 0x0001DFA0
    public HotkeyBindings(HotkeyBinding PushToTalk, HotkeyBinding TextMode, HotkeyBinding Dictation, HotkeyBinding HandsFreeDictation)
    {
      this.PushToTalk = PushToTalk;
      this.TextMode = TextMode;
      this.Dictation = Dictation;
      this.HandsFreeDictation = HandsFreeDictation;
      base..ctor();
    }

    // Token: 0x17000036 RID: 54
    // (get) Token: 0x060000BD RID: 189 RVA: 0x0001DFC8 File Offset: 0x0001DFC8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(HotkeyBindings);
      }
    }

    // Token: 0x17000037 RID: 55
    // (get) Token: 0x060000BE RID: 190 RVA: 0x0001DFD4 File Offset: 0x0001DFD4
    // (set) Token: 0x060000BF RID: 191 RVA: 0x0001DFDC File Offset: 0x0001DFDC
    public HotkeyBinding PushToTalk { get; set; }

    // Token: 0x17000038 RID: 56
    // (get) Token: 0x060000C0 RID: 192 RVA: 0x0001DFE8 File Offset: 0x0001DFE8
    // (set) Token: 0x060000C1 RID: 193 RVA: 0x0001DFF0 File Offset: 0x0001DFF0
    public HotkeyBinding TextMode { get; set; }

    // Token: 0x17000039 RID: 57
    // (get) Token: 0x060000C2 RID: 194 RVA: 0x0001DFFC File Offset: 0x0001DFFC
    // (set) Token: 0x060000C3 RID: 195 RVA: 0x0001E004 File Offset: 0x0001E004
    public HotkeyBinding Dictation { get; set; }

    // Token: 0x1700003A RID: 58
    // (get) Token: 0x060000C4 RID: 196 RVA: 0x0001E010 File Offset: 0x0001E010
    // (set) Token: 0x060000C5 RID: 197 RVA: 0x0001E018 File Offset: 0x0001E018
    public HotkeyBinding HandsFreeDictation { get; set; }

    // Token: 0x060000C6 RID: 198 RVA: 0x0001E024 File Offset: 0x0001E024
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("HotkeyBindings");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060000C7 RID: 199 RVA: 0x0001E070 File Offset: 0x0001E070
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("PushToTalk = ");
      builder.Append(this.PushToTalk);
      builder.Append(", TextMode = ");
      builder.Append(this.TextMode);
      builder.Append(", Dictation = ");
      builder.Append(this.Dictation);
      builder.Append(", HandsFreeDictation = ");
      builder.Append(this.HandsFreeDictation);
      return true;
    }

    // Token: 0x060000C8 RID: 200 RVA: 0x0001E0E8 File Offset: 0x0001E0E8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(HotkeyBindings left, HotkeyBindings right)
    {
      return !(left == right);
    }

    // Token: 0x060000C9 RID: 201 RVA: 0x0001E0F4 File Offset: 0x0001E0F4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(HotkeyBindings left, HotkeyBindings right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060000CA RID: 202 RVA: 0x0001E108 File Offset: 0x0001E108
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<HotkeyBinding>.Default.GetHashCode(this.<PushToTalk>k__BackingField)) * -1521134295 + EqualityComparer<HotkeyBinding>.Default.GetHashCode(this.<TextMode>k__BackingField)) * -1521134295 + EqualityComparer<HotkeyBinding>.Default.GetHashCode(this.<Dictation>k__BackingField)) * -1521134295 + EqualityComparer<HotkeyBinding>.Default.GetHashCode(this.<HandsFreeDictation>k__BackingField);
    }

    // Token: 0x060000CB RID: 203 RVA: 0x0001E184 File Offset: 0x0001E184
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as HotkeyBindings);
    }

    // Token: 0x060000CC RID: 204 RVA: 0x0001E194 File Offset: 0x0001E194
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(HotkeyBindings other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<HotkeyBinding>.Default.Equals(this.<PushToTalk>k__BackingField, other.<PushToTalk>k__BackingField) && EqualityComparer<HotkeyBinding>.Default.Equals(this.<TextMode>k__BackingField, other.<TextMode>k__BackingField) && EqualityComparer<HotkeyBinding>.Default.Equals(this.<Dictation>k__BackingField, other.<Dictation>k__BackingField) && EqualityComparer<HotkeyBinding>.Default.Equals(this.<HandsFreeDictation>k__BackingField, other.<HandsFreeDictation>k__BackingField));
    }

    // Token: 0x060000CE RID: 206 RVA: 0x0001E228 File Offset: 0x0001E228
    [CompilerGenerated]
    private HotkeyBindings(HotkeyBindings original)
    {
      this.PushToTalk = original.<PushToTalk>k__BackingField;
      this.TextMode = original.<TextMode>k__BackingField;
      this.Dictation = original.<Dictation>k__BackingField;
      this.HandsFreeDictation = original.<HandsFreeDictation>k__BackingField;
    }

    // Token: 0x060000CF RID: 207 RVA: 0x0001E260 File Offset: 0x0001E260
    [CompilerGenerated]
    public void Deconstruct(out HotkeyBinding PushToTalk, out HotkeyBinding TextMode, out HotkeyBinding Dictation, out HotkeyBinding HandsFreeDictation)
    {
      PushToTalk = this.PushToTalk;
      TextMode = this.TextMode;
      Dictation = this.Dictation;
      HandsFreeDictation = this.HandsFreeDictation;
    }

    // Token: 0x0400004F RID: 79
    public static readonly HotkeyBindings Defaults = new HotkeyBindings(HotkeyBinding.PushToTalkDefault, HotkeyBinding.TextModeDefault, HotkeyBinding.DictationDefault, HotkeyBinding.HandsFreeDictationDefault);
  }
}
