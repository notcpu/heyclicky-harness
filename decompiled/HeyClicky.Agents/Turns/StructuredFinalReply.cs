using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.Agents.Turns
{
  // Token: 0x0200001B RID: 27
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class StructuredFinalReply : IEquatable<StructuredFinalReply>
  {
    // Token: 0x0600035B RID: 859 RVA: 0x000E5274 File Offset: 0x000E5274
    public StructuredFinalReply([Nullable(1)] string Text, [Nullable(1)] IReadOnlyList<string> NextActions, string DoneTitle, [Nullable(1)] IReadOnlyList<string> Artifacts, string Summary, AgentRoutineRequest Routine, string ComputerUseRequest, ClickyJobUserStep UserStep = null, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> LearnedHowTos = null, ClickyAnswer Answer = null)
    {
      this.Text = Text;
      this.NextActions = NextActions;
      this.DoneTitle = DoneTitle;
      this.Artifacts = Artifacts;
      this.Summary = Summary;
      this.Routine = Routine;
      this.ComputerUseRequest = ComputerUseRequest;
      this.UserStep = UserStep;
      this.LearnedHowTos = LearnedHowTos;
      this.Answer = Answer;
      base..ctor();
    }

    // Token: 0x170000EC RID: 236
    // (get) Token: 0x0600035C RID: 860 RVA: 0x000E52D4 File Offset: 0x000E52D4
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(StructuredFinalReply);
      }
    }

    // Token: 0x170000ED RID: 237
    // (get) Token: 0x0600035D RID: 861 RVA: 0x000E52E0 File Offset: 0x000E52E0
    // (set) Token: 0x0600035E RID: 862 RVA: 0x000E52E8 File Offset: 0x000E52E8
    [Nullable(1)]
    public string Text
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170000EE RID: 238
    // (get) Token: 0x0600035F RID: 863 RVA: 0x000E52F4 File Offset: 0x000E52F4
    // (set) Token: 0x06000360 RID: 864 RVA: 0x000E52FC File Offset: 0x000E52FC
    [Nullable(1)]
    public IReadOnlyList<string> NextActions
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170000EF RID: 239
    // (get) Token: 0x06000361 RID: 865 RVA: 0x000E5308 File Offset: 0x000E5308
    // (set) Token: 0x06000362 RID: 866 RVA: 0x000E5310 File Offset: 0x000E5310
    public string DoneTitle { get; set; }

    // Token: 0x170000F0 RID: 240
    // (get) Token: 0x06000363 RID: 867 RVA: 0x000E531C File Offset: 0x000E531C
    // (set) Token: 0x06000364 RID: 868 RVA: 0x000E5324 File Offset: 0x000E5324
    [Nullable(1)]
    public IReadOnlyList<string> Artifacts
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170000F1 RID: 241
    // (get) Token: 0x06000365 RID: 869 RVA: 0x000E5330 File Offset: 0x000E5330
    // (set) Token: 0x06000366 RID: 870 RVA: 0x000E5338 File Offset: 0x000E5338
    public string Summary { get; set; }

    // Token: 0x170000F2 RID: 242
    // (get) Token: 0x06000367 RID: 871 RVA: 0x000E5344 File Offset: 0x000E5344
    // (set) Token: 0x06000368 RID: 872 RVA: 0x000E534C File Offset: 0x000E534C
    public AgentRoutineRequest Routine { get; set; }

    // Token: 0x170000F3 RID: 243
    // (get) Token: 0x06000369 RID: 873 RVA: 0x000E5358 File Offset: 0x000E5358
    // (set) Token: 0x0600036A RID: 874 RVA: 0x000E5360 File Offset: 0x000E5360
    public string ComputerUseRequest { get; set; }

    // Token: 0x170000F4 RID: 244
    // (get) Token: 0x0600036B RID: 875 RVA: 0x000E536C File Offset: 0x000E536C
    // (set) Token: 0x0600036C RID: 876 RVA: 0x000E5374 File Offset: 0x000E5374
    public ClickyJobUserStep UserStep { get; set; }

    // Token: 0x170000F5 RID: 245
    // (get) Token: 0x0600036D RID: 877 RVA: 0x000E5380 File Offset: 0x000E5380
    // (set) Token: 0x0600036E RID: 878 RVA: 0x000E5388 File Offset: 0x000E5388
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> LearnedHowTos
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x170000F6 RID: 246
    // (get) Token: 0x0600036F RID: 879 RVA: 0x000E5394 File Offset: 0x000E5394
    // (set) Token: 0x06000370 RID: 880 RVA: 0x000E539C File Offset: 0x000E539C
    public ClickyAnswer Answer { get; set; }

    // Token: 0x06000371 RID: 881 RVA: 0x000E53A8 File Offset: 0x000E53A8
    [NullableContext(1)]
    public static StructuredFinalReply Parse(string raw)
    {
      string text = StructuredFinalReply.NormalizingStructuredBlockTags(CitationMarkers.Strip(raw));
      List<string> list = StructuredFinalReply.ExtractList(ref text, "NEXT_ACTIONS", false, 4);
      string text2 = StructuredFinalReply.ExtractBlock(ref text, "DONE_TITLE");
      string text3 = ((text2 != null) ? StructuredFinalReply.Cap(text2.Trim().Trim(new char[] { '"', '\'', '`', '.' }).Trim(), 60) : null);
      List<string> list2 = StructuredFinalReply.ExtractList(ref text, "ARTIFACTS", true, 8);
      ClickyAnswer clickyAnswer = ClickyAnswer.Parse(StructuredFinalReply.ExtractTaggedBlock(ref text, "ANSWER"));
      string text4 = StructuredFinalReply.ExtractBlock(ref text, "SUMMARY");
      string text5 = ((text4 != null) ? text4.Trim() : null);
      string text6 = ((text5 != null && text5.Length > 0) ? StructuredFinalReply.Cap(text5, 320) : ((clickyAnswer != null) ? clickyAnswer.Line : null));
      string text7 = StructuredFinalReply.ExtractBlock(ref text, "ROUTINE");
      AgentRoutineRequest agentRoutineRequest = ((text7 != null) ? AgentRoutineRequest.Parse(text7) : null);
      string text8 = StructuredFinalReply.ExtractBlock(ref text, "COMPUTER_USE_REQUEST");
      string text9 = ((text8 != null) ? text8.Trim() : null);
      string text10 = ((text9 != null && text9.Length > 0) ? StructuredFinalReply.Cap(text9, 400) : null);
      string text11 = StructuredFinalReply.ExtractTaggedBlock(ref text, "WAIT_FOR");
      ClickyJobUserStep clickyJobUserStep = ((text11 != null) ? ClickyJobUserStep.Parse(text11, null) : null);
      IReadOnlyList<string> readOnlyList = ClickyLearnedHowTos.Lines(StructuredFinalReply.ExtractTaggedBlock(ref text, "LEARNED"));
      return new StructuredFinalReply(text.Trim(), list, string.IsNullOrEmpty(text3) ? null : text3, list2, text6, agentRoutineRequest, text10, clickyJobUserStep, readOnlyList, clickyAnswer);
    }

    // Token: 0x06000372 RID: 882 RVA: 0x000E5530 File Offset: 0x000E5530
    [NullableContext(1)]
    [return: Nullable(2)]
    private static string ExtractTaggedBlock(ref string text, string name)
    {
      string text2 = StructuredFinalReply.ExtractBlock(ref text, name);
      string text3 = ((text2 != null) ? text2.Trim() : null);
      if (text3 == null || text3.Length <= 0)
      {
        return null;
      }
      return text3;
    }

    // Token: 0x06000373 RID: 883 RVA: 0x000E5560 File Offset: 0x000E5560
    [NullableContext(1)]
    public static string NormalizingStructuredBlockTags(string text)
    {
      if (!text.Contains('<'))
      {
        return text;
      }
      string text2 = StructuredFinalReply.StructuredBlockTag.Replace(text, (Match m) => "<" + m.Groups[1].Value + m.Groups[2].Value + ">");
      int num = text2.LastIndexOf("<SUMMARY>", StringComparison.OrdinalIgnoreCase);
      if (num >= 0 && text2.IndexOf("</SUMMARY>", num + "<SUMMARY>".Length, StringComparison.OrdinalIgnoreCase) < 0)
      {
        text2 += "</SUMMARY>";
      }
      return text2;
    }

    // Token: 0x170000F7 RID: 247
    // (get) Token: 0x06000374 RID: 884 RVA: 0x000E55E0 File Offset: 0x000E55E0
    [Nullable(1)]
    public string SummaryOrFallback
    {
      [NullableContext(1)]
      get
      {
        string text;
        if ((text = this.Summary) == null)
        {
          if (this.Text.Length != 0)
          {
            return StructuredFinalReply.Cap(this.Text, 320);
          }
          text = "Codex child worker completed successfully.";
        }
        return text;
      }
    }

    // Token: 0x06000375 RID: 885 RVA: 0x000E5610 File Offset: 0x000E5610
    [NullableContext(1)]
    private static string Cap(string s, int n)
    {
      if (s.Length <= n)
      {
        return s;
      }
      return s.Substring(0, n);
    }

    // Token: 0x06000376 RID: 886 RVA: 0x000E5628 File Offset: 0x000E5628
    [NullableContext(1)]
    [return: Nullable(2)]
    private static string ExtractBlock(ref string text, string tag)
    {
      int num = text.IndexOf("<" + tag + ">", StringComparison.OrdinalIgnoreCase);
      if (num < 0)
      {
        return null;
      }
      int num2 = num + tag.Length + 2;
      int num3 = text.IndexOf("</" + tag + ">", num2, StringComparison.OrdinalIgnoreCase);
      if (num3 < 0)
      {
        return null;
      }
      string text2 = text;
      int num4 = num2;
      string text3 = text2.Substring(num4, num3 - num4);
      text = text.Remove(num, num3 + tag.Length + 3 - num);
      return text3;
    }

    // Token: 0x06000377 RID: 887 RVA: 0x000E56A4 File Offset: 0x000E56A4
    [NullableContext(1)]
    private static List<string> ExtractList(ref string text, string tag, bool keepPathCharacters, int max)
    {
      string text2 = StructuredFinalReply.ExtractBlock(ref text, tag);
      if (text2 == null)
      {
        return new List<string>();
      }
      List<string> list = new List<string>();
      string[] array = text2.Split('\n', StringSplitOptions.None);
      for (int i = 0; i < array.Length; i++)
      {
        string text3 = array[i].Trim();
        while (text3.StartsWith('-') || text3.StartsWith('*') || text3.StartsWith('•'))
        {
          string text4 = text3;
          text3 = text4.Substring(1, text4.Length - 1).Trim();
        }
        text3 = (keepPathCharacters ? text3.Trim(new char[] { '"', '\'', '`' }).Trim() : text3.Trim(new char[] { '"', '\'', '`', '.', ',', ';', ':' }).Trim());
        if (text3.Length > 0)
        {
          list.Add(text3);
        }
        if (list.Count == max)
        {
          break;
        }
      }
      return list;
    }

    // Token: 0x06000378 RID: 888 RVA: 0x000E5790 File Offset: 0x000E5790
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("StructuredFinalReply");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000379 RID: 889 RVA: 0x000E57DC File Offset: 0x000E57DC
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Text = ");
      builder.Append(this.Text);
      builder.Append(", NextActions = ");
      builder.Append(this.NextActions);
      builder.Append(", DoneTitle = ");
      builder.Append(this.DoneTitle);
      builder.Append(", Artifacts = ");
      builder.Append(this.Artifacts);
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      builder.Append(", Routine = ");
      builder.Append(this.Routine);
      builder.Append(", ComputerUseRequest = ");
      builder.Append(this.ComputerUseRequest);
      builder.Append(", UserStep = ");
      builder.Append(this.UserStep);
      builder.Append(", LearnedHowTos = ");
      builder.Append(this.LearnedHowTos);
      builder.Append(", Answer = ");
      builder.Append(this.Answer);
      builder.Append(", SummaryOrFallback = ");
      builder.Append(this.SummaryOrFallback);
      return true;
    }

    // Token: 0x0600037A RID: 890 RVA: 0x000E5904 File Offset: 0x000E5904
    [CompilerGenerated]
    public static bool operator !=(StructuredFinalReply left, StructuredFinalReply right)
    {
      return !(left == right);
    }

    // Token: 0x0600037B RID: 891 RVA: 0x000E5910 File Offset: 0x000E5910
    [CompilerGenerated]
    public static bool operator ==(StructuredFinalReply left, StructuredFinalReply right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600037C RID: 892 RVA: 0x000E5924 File Offset: 0x000E5924
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<NextActions>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DoneTitle>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Artifacts>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Summary>k__BackingField)) * -1521134295 + EqualityComparer<AgentRoutineRequest>.Default.GetHashCode(this.<Routine>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ComputerUseRequest>k__BackingField)) * -1521134295 + EqualityComparer<ClickyJobUserStep>.Default.GetHashCode(this.<UserStep>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<LearnedHowTos>k__BackingField)) * -1521134295 + EqualityComparer<ClickyAnswer>.Default.GetHashCode(this.<Answer>k__BackingField);
    }

    // Token: 0x0600037D RID: 893 RVA: 0x000E5A28 File Offset: 0x000E5A28
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as StructuredFinalReply);
    }

    // Token: 0x0600037E RID: 894 RVA: 0x000E5A38 File Offset: 0x000E5A38
    [CompilerGenerated]
    public bool Equals(StructuredFinalReply other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<NextActions>k__BackingField, other.<NextActions>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DoneTitle>k__BackingField, other.<DoneTitle>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Artifacts>k__BackingField, other.<Artifacts>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Summary>k__BackingField, other.<Summary>k__BackingField) && EqualityComparer<AgentRoutineRequest>.Default.Equals(this.<Routine>k__BackingField, other.<Routine>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ComputerUseRequest>k__BackingField, other.<ComputerUseRequest>k__BackingField) && EqualityComparer<ClickyJobUserStep>.Default.Equals(this.<UserStep>k__BackingField, other.<UserStep>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<LearnedHowTos>k__BackingField, other.<LearnedHowTos>k__BackingField) && EqualityComparer<ClickyAnswer>.Default.Equals(this.<Answer>k__BackingField, other.<Answer>k__BackingField));
    }

    // Token: 0x06000380 RID: 896 RVA: 0x000E5B70 File Offset: 0x000E5B70
    [CompilerGenerated]
    private StructuredFinalReply([Nullable(1)] StructuredFinalReply original)
    {
      this.Text = original.<Text>k__BackingField;
      this.NextActions = original.<NextActions>k__BackingField;
      this.DoneTitle = original.<DoneTitle>k__BackingField;
      this.Artifacts = original.<Artifacts>k__BackingField;
      this.Summary = original.<Summary>k__BackingField;
      this.Routine = original.<Routine>k__BackingField;
      this.ComputerUseRequest = original.<ComputerUseRequest>k__BackingField;
      this.UserStep = original.<UserStep>k__BackingField;
      this.LearnedHowTos = original.<LearnedHowTos>k__BackingField;
      this.Answer = original.<Answer>k__BackingField;
    }

    // Token: 0x06000381 RID: 897 RVA: 0x000E5BFC File Offset: 0x000E5BFC
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string Text, [Nullable(1)] out IReadOnlyList<string> NextActions, out string DoneTitle, [Nullable(1)] out IReadOnlyList<string> Artifacts, out string Summary, out AgentRoutineRequest Routine, out string ComputerUseRequest, out ClickyJobUserStep UserStep, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> LearnedHowTos, out ClickyAnswer Answer)
    {
      Text = this.Text;
      NextActions = this.NextActions;
      DoneTitle = this.DoneTitle;
      Artifacts = this.Artifacts;
      Summary = this.Summary;
      Routine = this.Routine;
      ComputerUseRequest = this.ComputerUseRequest;
      UserStep = this.UserStep;
      LearnedHowTos = this.LearnedHowTos;
      Answer = this.Answer;
    }

    // Token: 0x04000148 RID: 328
    [Nullable(1)]
    private static readonly Regex StructuredBlockTag = new Regex("<\\s*(/?)\\s*(SUMMARY|NEXT_ACTIONS|DONE_TITLE|ARTIFACTS|ROUTINE|COMPUTER_USE_REQUEST|WAIT_FOR|LEARNED|ANSWER)\\s*>", RegexOptions.IgnoreCase);
  }
}
