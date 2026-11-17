using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000108 RID: 264
  [NullableContext(1)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class ConversationMessage : IEquatable<ConversationMessage>
  {
    // Token: 0x17000402 RID: 1026
    // (get) Token: 0x0600113C RID: 4412 RVA: 0x0039B134 File Offset: 0x0039B134
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ConversationMessage);
      }
    }

    // Token: 0x17000403 RID: 1027
    // (get) Token: 0x0600113D RID: 4413 RVA: 0x0039B140 File Offset: 0x0039B140
    // (set) Token: 0x0600113E RID: 4414 RVA: 0x0039B148 File Offset: 0x0039B148
    [RequiredMember]
    public string Id { get; set; }

    // Token: 0x17000404 RID: 1028
    // (get) Token: 0x0600113F RID: 4415 RVA: 0x0039B154 File Offset: 0x0039B154
    // (set) Token: 0x06001140 RID: 4416 RVA: 0x0039B15C File Offset: 0x0039B15C
    public long? ServerId { get; set; }

    // Token: 0x17000405 RID: 1029
    // (get) Token: 0x06001141 RID: 4417 RVA: 0x0039B168 File Offset: 0x0039B168
    // (set) Token: 0x06001142 RID: 4418 RVA: 0x0039B170 File Offset: 0x0039B170
    [RequiredMember]
    public string Role { get; set; }

    // Token: 0x17000406 RID: 1030
    // (get) Token: 0x06001143 RID: 4419 RVA: 0x0039B17C File Offset: 0x0039B17C
    // (set) Token: 0x06001144 RID: 4420 RVA: 0x0039B184 File Offset: 0x0039B184
    [RequiredMember]
    public string Content { get; set; }

    // Token: 0x17000407 RID: 1031
    // (get) Token: 0x06001145 RID: 4421 RVA: 0x0039B190 File Offset: 0x0039B190
    // (set) Token: 0x06001146 RID: 4422 RVA: 0x0039B198 File Offset: 0x0039B198
    public string Source { get; set; }

    // Token: 0x17000408 RID: 1032
    // (get) Token: 0x06001147 RID: 4423 RVA: 0x0039B1A4 File Offset: 0x0039B1A4
    // (set) Token: 0x06001148 RID: 4424 RVA: 0x0039B1AC File Offset: 0x0039B1AC
    [RequiredMember]
    public DateTimeOffset CreatedAt { get; set; }

    // Token: 0x17000409 RID: 1033
    // (get) Token: 0x06001149 RID: 4425 RVA: 0x0039B1B8 File Offset: 0x0039B1B8
    // (set) Token: 0x0600114A RID: 4426 RVA: 0x0039B1C0 File Offset: 0x0039B1C0
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> AttachmentPaths
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x1700040A RID: 1034
    // (get) Token: 0x0600114B RID: 4427 RVA: 0x0039B1CC File Offset: 0x0039B1CC
    // (set) Token: 0x0600114C RID: 4428 RVA: 0x0039B1D4 File Offset: 0x0039B1D4
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> BubbleSegments
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x1700040B RID: 1035
    // (get) Token: 0x0600114D RID: 4429 RVA: 0x0039B1E0 File Offset: 0x0039B1E0
    // (set) Token: 0x0600114E RID: 4430 RVA: 0x0039B1E8 File Offset: 0x0039B1E8
    [Nullable(2)]
    public string ReplacedLocalId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700040C RID: 1036
    // (get) Token: 0x0600114F RID: 4431 RVA: 0x0039B1F4 File Offset: 0x0039B1F4
    // (set) Token: 0x06001150 RID: 4432 RVA: 0x0039B1FC File Offset: 0x0039B1FC
    [Nullable(2)]
    public string SentToAgentName
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700040D RID: 1037
    // (get) Token: 0x06001151 RID: 4433 RVA: 0x0039B208 File Offset: 0x0039B208
    [JsonIgnore]
    public bool IsPending
    {
      get
      {
        return this.ServerId == null && this.SentToAgentName == null;
      }
    }

    // Token: 0x1700040E RID: 1038
    // (get) Token: 0x06001152 RID: 4434 RVA: 0x0039B230 File Offset: 0x0039B230
    [JsonIgnore]
    public bool IsUser
    {
      get
      {
        return this.Role == "user";
      }
    }

    // Token: 0x1700040F RID: 1039
    // (get) Token: 0x06001153 RID: 4435 RVA: 0x0039B244 File Offset: 0x0039B244
    [JsonIgnore]
    public string RowId
    {
      get
      {
        return this.ReplacedLocalId ?? this.Id;
      }
    }

    // Token: 0x17000410 RID: 1040
    // (get) Token: 0x06001154 RID: 4436 RVA: 0x0039B258 File Offset: 0x0039B258
    [JsonIgnore]
    public IReadOnlyList<string> BubbleTexts
    {
      get
      {
        IReadOnlyList<string> bubbleSegments = this.BubbleSegments;
        if (bubbleSegments != null && bubbleSegments.Count > 1)
        {
          return bubbleSegments;
        }
        string bubbleText = this.BubbleText;
        if (bubbleText.Length != 0)
        {
          return new <>z__ReadOnlySingleElementList<string>(bubbleText);
        }
        return Array.Empty<string>();
      }
    }

    // Token: 0x06001155 RID: 4437 RVA: 0x0039B29C File Offset: 0x0039B29C
    public static List<string> BubbleSegmentsFrom(string text)
    {
      return (from s in text.Split("\n\n", StringSplitOptions.None)
        select s.Trim() into s
        where s.Length > 0
        select s).ToList<string>();
    }

    // Token: 0x17000411 RID: 1041
    // (get) Token: 0x06001156 RID: 4438 RVA: 0x0039B304 File Offset: 0x0039B304
    [JsonIgnore]
    public bool BubbleIsOnlyAboutAttachments
    {
      get
      {
        IReadOnlyList<string> attachmentPaths = this.AttachmentPaths;
        if (attachmentPaths == null || attachmentPaths.Count <= 0)
        {
          return false;
        }
        string text = this.TypedWords.Trim();
        return text.Length == 0 || text.StartsWith("\ud83d\udcce ", StringComparison.Ordinal) || text == "Please review and act on the attached files.";
      }
    }

    // Token: 0x17000412 RID: 1042
    // (get) Token: 0x06001157 RID: 4439 RVA: 0x0039B358 File Offset: 0x0039B358
    [JsonIgnore]
    public string TypedWords
    {
      get
      {
        if (!this.IsUser)
        {
          return this.Content;
        }
        return ConversationMessage.WithoutAttachedFilesNote(this.Content);
      }
    }

    // Token: 0x06001158 RID: 4440 RVA: 0x0039B374 File Offset: 0x0039B374
    private static string WithoutAttachedFilesNote(string text)
    {
      foreach (string text2 in ConversationMessage.AttachedFilesNoteHeaders)
      {
        if (text.StartsWith(text2, StringComparison.Ordinal))
        {
          return "";
        }
        int num = text.LastIndexOf("\n\n" + text2, StringComparison.Ordinal);
        if (num >= 0)
        {
          return text.Substring(0, num);
        }
      }
      return text;
    }

    // Token: 0x17000413 RID: 1043
    // (get) Token: 0x06001159 RID: 4441 RVA: 0x0039B3CC File Offset: 0x0039B3CC
    [JsonIgnore]
    public string BubbleText
    {
      get
      {
        return ConversationMessage.Split(this.TypedWords).Item1;
      }
    }

    // Token: 0x17000414 RID: 1044
    // (get) Token: 0x0600115A RID: 4442 RVA: 0x0039B3E0 File Offset: 0x0039B3E0
    [Nullable(2)]
    [JsonIgnore]
    public string ActionCaption
    {
      [NullableContext(2)]
      get
      {
        return ConversationMessage.Split(this.TypedWords).Item2;
      }
    }

    // Token: 0x0600115B RID: 4443 RVA: 0x0039B3F4 File Offset: 0x0039B3F4
    [return: Nullable(2)]
    private static string WithoutHiddenActions(string caption)
    {
      List<string> list = new List<string>();
      string[] array = caption.Split("; ", StringSplitOptions.None);
      for (int i = 0; i < array.Length; i++)
      {
        string text = array[i].Trim();
        if (!text.StartsWith("called ", StringComparison.Ordinal))
        {
          if (text.Length > 0)
          {
            list.Add(text);
          }
        }
        else
        {
          string text2 = text;
          int length = "called ".Length;
          List<string> list2 = (from t in text2.Substring(length, text2.Length - length).Split(", ", StringSplitOptions.None)
            select t.Trim() into t
            where t.Length > 0 && !ConversationMessage.HiddenActionTools.Contains(t)
            select t).ToList<string>();
          if (list2.Count > 0)
          {
            list.Add("called " + string.Join(", ", list2));
          }
        }
      }
      if (list.Count != 0)
      {
        return string.Join("; ", list);
      }
      return null;
    }

    // Token: 0x0600115C RID: 4444 RVA: 0x0039B510 File Offset: 0x0039B510
    [return: TupleElementNames(new string[] { "Spoken", "Caption" })]
    [return: Nullable(new byte[] { 0, 1, 2 })]
    private static ValueTuple<string, string> Split(string content)
    {
      List<string> list = content.Split('\n', StringSplitOptions.None).ToList<string>();
      string text = null;
      List<string> list2 = list;
      string text2 = list2[list2.Count - 1].Trim();
      if (text2.StartsWith('(') && text2.EndsWith(')') && (text2.StartsWith("(Drew ", StringComparison.Ordinal) || text2.StartsWith("(called ", StringComparison.Ordinal)))
      {
        string text3 = text2;
        text = ConversationMessage.WithoutHiddenActions(text3.Substring(1, text3.Length - 1 - 1));
        list.RemoveAt(list.Count - 1);
      }
      return new ValueTuple<string, string>(ConversationMessage.TagPattern.Replace(string.Join("\n", list), "").Trim(), text);
    }

    // Token: 0x0600115D RID: 4445 RVA: 0x0039B5C0 File Offset: 0x0039B5C0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ConversationMessage");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600115E RID: 4446 RVA: 0x0039B60C File Offset: 0x0039B60C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", ServerId = ");
      builder.Append(this.ServerId.ToString());
      builder.Append(", Role = ");
      builder.Append(this.Role);
      builder.Append(", Content = ");
      builder.Append(this.Content);
      builder.Append(", Source = ");
      builder.Append(this.Source);
      builder.Append(", CreatedAt = ");
      builder.Append(this.CreatedAt.ToString());
      builder.Append(", AttachmentPaths = ");
      builder.Append(this.AttachmentPaths);
      builder.Append(", BubbleSegments = ");
      builder.Append(this.BubbleSegments);
      builder.Append(", ReplacedLocalId = ");
      builder.Append(this.ReplacedLocalId);
      builder.Append(", SentToAgentName = ");
      builder.Append(this.SentToAgentName);
      builder.Append(", IsPending = ");
      builder.Append(this.IsPending.ToString());
      builder.Append(", IsUser = ");
      builder.Append(this.IsUser.ToString());
      builder.Append(", RowId = ");
      builder.Append(this.RowId);
      builder.Append(", BubbleTexts = ");
      builder.Append(this.BubbleTexts);
      builder.Append(", BubbleIsOnlyAboutAttachments = ");
      builder.Append(this.BubbleIsOnlyAboutAttachments.ToString());
      builder.Append(", TypedWords = ");
      builder.Append(this.TypedWords);
      builder.Append(", BubbleText = ");
      builder.Append(this.BubbleText);
      builder.Append(", ActionCaption = ");
      builder.Append(this.ActionCaption);
      return true;
    }

    // Token: 0x0600115F RID: 4447 RVA: 0x0039B828 File Offset: 0x0039B828
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ConversationMessage left, ConversationMessage right)
    {
      return !(left == right);
    }

    // Token: 0x06001160 RID: 4448 RVA: 0x0039B834 File Offset: 0x0039B834
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ConversationMessage left, ConversationMessage right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001161 RID: 4449 RVA: 0x0039B848 File Offset: 0x0039B848
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<long?>.Default.GetHashCode(this.<ServerId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Role>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Content>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Source>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<CreatedAt>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<AttachmentPaths>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<BubbleSegments>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ReplacedLocalId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SentToAgentName>k__BackingField);
    }

    // Token: 0x06001162 RID: 4450 RVA: 0x0039B94C File Offset: 0x0039B94C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ConversationMessage);
    }

    // Token: 0x06001163 RID: 4451 RVA: 0x0039B95C File Offset: 0x0039B95C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ConversationMessage other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<long?>.Default.Equals(this.<ServerId>k__BackingField, other.<ServerId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Role>k__BackingField, other.<Role>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Content>k__BackingField, other.<Content>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Source>k__BackingField, other.<Source>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<CreatedAt>k__BackingField, other.<CreatedAt>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<AttachmentPaths>k__BackingField, other.<AttachmentPaths>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<BubbleSegments>k__BackingField, other.<BubbleSegments>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ReplacedLocalId>k__BackingField, other.<ReplacedLocalId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SentToAgentName>k__BackingField, other.<SentToAgentName>k__BackingField));
    }

    // Token: 0x06001165 RID: 4453 RVA: 0x0039BA94 File Offset: 0x0039BA94
    [CompilerGenerated]
    [SetsRequiredMembers]
    private ConversationMessage(ConversationMessage original)
    {
      this.Id = original.<Id>k__BackingField;
      this.ServerId = original.<ServerId>k__BackingField;
      this.Role = original.<Role>k__BackingField;
      this.Content = original.<Content>k__BackingField;
      this.Source = original.<Source>k__BackingField;
      this.CreatedAt = original.<CreatedAt>k__BackingField;
      this.AttachmentPaths = original.<AttachmentPaths>k__BackingField;
      this.BubbleSegments = original.<BubbleSegments>k__BackingField;
      this.ReplacedLocalId = original.<ReplacedLocalId>k__BackingField;
      this.SentToAgentName = original.<SentToAgentName>k__BackingField;
    }

    // Token: 0x06001166 RID: 4454 RVA: 0x0039BB20 File Offset: 0x0039BB20
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public ConversationMessage()
    {
      this.Source = "text";
      base..ctor();
    }

    // Token: 0x04000786 RID: 1926
    public const string AttachmentOnlyTurnText = "Please review and act on the attached files.";

    // Token: 0x04000787 RID: 1927
    private static readonly string[] AttachedFilesNoteHeaders = new string[] { "[They attached files.", "[They attached files with no message." };

    // Token: 0x04000788 RID: 1928
    private static readonly Regex TagPattern = new Regex("\\[(?:POINT|TYPE|TARGET|HOVER|CLICK)[^\\]]*\\]", RegexOptions.Compiled);

    // Token: 0x04000789 RID: 1929
    private static readonly HashSet<string> HiddenActionTools = new HashSet<string> { "show_result" };
  }
}
