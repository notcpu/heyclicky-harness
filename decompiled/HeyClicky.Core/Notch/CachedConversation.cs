using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Notch
{
  // Token: 0x0200010A RID: 266
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CachedConversation : IEquatable<CachedConversation>
  {
    // Token: 0x06001194 RID: 4500 RVA: 0x0039D488 File Offset: 0x0039D488
    public CachedConversation(IReadOnlyList<ConversationMessage> Messages, bool HasEarlierMessages)
    {
      this.Messages = Messages;
      this.HasEarlierMessages = HasEarlierMessages;
      base..ctor();
    }

    // Token: 0x1700041D RID: 1053
    // (get) Token: 0x06001195 RID: 4501 RVA: 0x0039D4A0 File Offset: 0x0039D4A0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(CachedConversation);
      }
    }

    // Token: 0x1700041E RID: 1054
    // (get) Token: 0x06001196 RID: 4502 RVA: 0x0039D4AC File Offset: 0x0039D4AC
    // (set) Token: 0x06001197 RID: 4503 RVA: 0x0039D4B4 File Offset: 0x0039D4B4
    public IReadOnlyList<ConversationMessage> Messages { get; set; }

    // Token: 0x1700041F RID: 1055
    // (get) Token: 0x06001198 RID: 4504 RVA: 0x0039D4C0 File Offset: 0x0039D4C0
    // (set) Token: 0x06001199 RID: 4505 RVA: 0x0039D4C8 File Offset: 0x0039D4C8
    public bool HasEarlierMessages { get; set; }

    // Token: 0x0600119A RID: 4506 RVA: 0x0039D4D4 File Offset: 0x0039D4D4
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CachedConversation");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600119B RID: 4507 RVA: 0x0039D520 File Offset: 0x0039D520
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Messages = ");
      builder.Append(this.Messages);
      builder.Append(", HasEarlierMessages = ");
      builder.Append(this.HasEarlierMessages.ToString());
      return true;
    }

    // Token: 0x0600119C RID: 4508 RVA: 0x0039D574 File Offset: 0x0039D574
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(CachedConversation left, CachedConversation right)
    {
      return !(left == right);
    }

    // Token: 0x0600119D RID: 4509 RVA: 0x0039D580 File Offset: 0x0039D580
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(CachedConversation left, CachedConversation right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600119E RID: 4510 RVA: 0x0039D594 File Offset: 0x0039D594
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<ConversationMessage>>.Default.GetHashCode(this.<Messages>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<HasEarlierMessages>k__BackingField);
    }

    // Token: 0x0600119F RID: 4511 RVA: 0x0039D5D4 File Offset: 0x0039D5D4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CachedConversation);
    }

    // Token: 0x060011A0 RID: 4512 RVA: 0x0039D5E4 File Offset: 0x0039D5E4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(CachedConversation other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<ConversationMessage>>.Default.Equals(this.<Messages>k__BackingField, other.<Messages>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<HasEarlierMessages>k__BackingField, other.<HasEarlierMessages>k__BackingField));
    }

    // Token: 0x060011A2 RID: 4514 RVA: 0x0039D648 File Offset: 0x0039D648
    [CompilerGenerated]
    private CachedConversation(CachedConversation original)
    {
      this.Messages = original.<Messages>k__BackingField;
      this.HasEarlierMessages = original.<HasEarlierMessages>k__BackingField;
    }

    // Token: 0x060011A3 RID: 4515 RVA: 0x0039D668 File Offset: 0x0039D668
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<ConversationMessage> Messages, out bool HasEarlierMessages)
    {
      Messages = this.Messages;
      HasEarlierMessages = this.HasEarlierMessages;
    }
  }
}
