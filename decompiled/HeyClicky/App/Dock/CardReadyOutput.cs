using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200030D RID: 781
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CardReadyOutput : IEquatable<CardReadyOutput>
  {
    // Token: 0x060021E3 RID: 8675 RVA: 0x0061EF84 File Offset: 0x0061EF84
    public CardReadyOutput(CardReadyOutput.Kinds Kind, [Nullable(2)] string Entry, int Count, string Title, string Detail, string ButtonTitle, [Nullable(2)] string ButtonSymbol, string Symbol, uint IconBackgroundRgb, [Nullable(2)] string ThreadId)
    {
      this.Kind = Kind;
      this.Entry = Entry;
      this.Count = Count;
      this.Title = Title;
      this.Detail = Detail;
      this.ButtonTitle = ButtonTitle;
      this.ButtonSymbol = ButtonSymbol;
      this.Symbol = Symbol;
      this.IconBackgroundRgb = IconBackgroundRgb;
      this.ThreadId = ThreadId;
      base..ctor();
    }

    // Token: 0x1700063E RID: 1598
    // (get) Token: 0x060021E4 RID: 8676 RVA: 0x0061EFE4 File Offset: 0x0061EFE4
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(CardReadyOutput);
      }
    }

    // Token: 0x1700063F RID: 1599
    // (get) Token: 0x060021E5 RID: 8677 RVA: 0x0061EFF0 File Offset: 0x0061EFF0
    // (set) Token: 0x060021E6 RID: 8678 RVA: 0x0061EFF8 File Offset: 0x0061EFF8
    public CardReadyOutput.Kinds Kind { get; set; }

    // Token: 0x17000640 RID: 1600
    // (get) Token: 0x060021E7 RID: 8679 RVA: 0x0061F004 File Offset: 0x0061F004
    // (set) Token: 0x060021E8 RID: 8680 RVA: 0x0061F00C File Offset: 0x0061F00C
    [Nullable(2)]
    public string Entry
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000641 RID: 1601
    // (get) Token: 0x060021E9 RID: 8681 RVA: 0x0061F018 File Offset: 0x0061F018
    // (set) Token: 0x060021EA RID: 8682 RVA: 0x0061F020 File Offset: 0x0061F020
    public int Count { get; set; }

    // Token: 0x17000642 RID: 1602
    // (get) Token: 0x060021EB RID: 8683 RVA: 0x0061F02C File Offset: 0x0061F02C
    // (set) Token: 0x060021EC RID: 8684 RVA: 0x0061F034 File Offset: 0x0061F034
    public string Title { get; set; }

    // Token: 0x17000643 RID: 1603
    // (get) Token: 0x060021ED RID: 8685 RVA: 0x0061F040 File Offset: 0x0061F040
    // (set) Token: 0x060021EE RID: 8686 RVA: 0x0061F048 File Offset: 0x0061F048
    public string Detail { get; set; }

    // Token: 0x17000644 RID: 1604
    // (get) Token: 0x060021EF RID: 8687 RVA: 0x0061F054 File Offset: 0x0061F054
    // (set) Token: 0x060021F0 RID: 8688 RVA: 0x0061F05C File Offset: 0x0061F05C
    public string ButtonTitle { get; set; }

    // Token: 0x17000645 RID: 1605
    // (get) Token: 0x060021F1 RID: 8689 RVA: 0x0061F068 File Offset: 0x0061F068
    // (set) Token: 0x060021F2 RID: 8690 RVA: 0x0061F070 File Offset: 0x0061F070
    [Nullable(2)]
    public string ButtonSymbol
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000646 RID: 1606
    // (get) Token: 0x060021F3 RID: 8691 RVA: 0x0061F07C File Offset: 0x0061F07C
    // (set) Token: 0x060021F4 RID: 8692 RVA: 0x0061F084 File Offset: 0x0061F084
    public string Symbol { get; set; }

    // Token: 0x17000647 RID: 1607
    // (get) Token: 0x060021F5 RID: 8693 RVA: 0x0061F090 File Offset: 0x0061F090
    // (set) Token: 0x060021F6 RID: 8694 RVA: 0x0061F098 File Offset: 0x0061F098
    public uint IconBackgroundRgb { get; set; }

    // Token: 0x17000648 RID: 1608
    // (get) Token: 0x060021F7 RID: 8695 RVA: 0x0061F0A4 File Offset: 0x0061F0A4
    // (set) Token: 0x060021F8 RID: 8696 RVA: 0x0061F0AC File Offset: 0x0061F0AC
    [Nullable(2)]
    public string ThreadId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000649 RID: 1609
    // (get) Token: 0x060021F9 RID: 8697 RVA: 0x0061F0B8 File Offset: 0x0061F0B8
    public bool OpensFullReplyOnly
    {
      get
      {
        return this.CardEntry == null;
      }
    }

    // Token: 0x1700064A RID: 1610
    // (get) Token: 0x060021FA RID: 8698 RVA: 0x0061F0C4 File Offset: 0x0061F0C4
    [Nullable(2)]
    public string CardEntry
    {
      [NullableContext(2)]
      get
      {
        CardReadyOutput.Kinds kind = this.Kind;
        if (kind > CardReadyOutput.Kinds.Link)
        {
          return null;
        }
        return this.Entry;
      }
    }

    // Token: 0x060021FB RID: 8699 RVA: 0x0061F0F0 File Offset: 0x0061F0F0
    public static CardReadyOutput Make(IReadOnlyList<string> entries, [Nullable(2)] string summary, [Nullable(2)] string threadId, Func<IReadOnlyList<string>, IReadOnlyList<string>> mainEntries, Func<string, string> resolve, [TupleElementNames(new string[] { "KindLabel", "Symbol", "Tint" })] [Nullable(new byte[] { 1, 1, 0, 1, 1 })] Func<string, ValueTuple<string, string, uint>> kind)
    {
      HashSet<string> seen = new HashSet<string>();
      IReadOnlyList<string> readOnlyList = mainEntries(entries.Where<string>((string e) => e.Length > 0 && seen.Add(e)).ToList<string>());
      string text = ((summary != null) ? summary.Trim() : null);
      string text2 = ((text != null && text.Length > 0) ? text : null);
      if (readOnlyList.Count == 1)
      {
        string text3 = readOnlyList[0];
        if (CardReadyOutput.IsWebLink(text3))
        {
          ClickyLinkName.Name name = ClickyLinkName.For(text3);
          string text4;
          if (name != null)
          {
            ClickyLinkName.Name name2 = name;
            string thing = name2.Thing;
            text4 = ((thing != null) ? ("Your " + thing + " is ready") : ("Your " + name2.Site + " link is ready"));
          }
          else
          {
            text4 = "Your link is ready";
          }
          string text5 = text4;
          string text6;
          if (name != null)
          {
            ClickyLinkName.Name name3 = name;
            text6 = "Open " + (name3.Thing ?? name3.Site);
          }
          else
          {
            text6 = "Open link";
          }
          string text7 = text6;
          return new CardReadyOutput(CardReadyOutput.Kinds.Link, text3, 1, text5, text2 ?? "Open it to see what it made.", text7, "arrow.up.right", "link", 3900150U, threadId);
        }
        string text8 = resolve(text3);
        ValueTuple<string, string, uint> valueTuple = kind(text8);
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text8.TrimEnd(new char[] { '\\', '/' }));
        return new CardReadyOutput(CardReadyOutput.Kinds.File, text3, 1, ((fileNameWithoutExtension.Length == 0) ? "Your file" : fileNameWithoutExtension) + " is ready", text2 ?? ("Your " + CardReadyOutput.KindNoun(valueTuple.Item1) + " is ready to open."), CardReadyOutput.OpenTitle(valueTuple.Item1), null, valueTuple.Item2, valueTuple.Item3, threadId);
      }
      else
      {
        if (readOnlyList.Count > 1)
        {
          CardReadyOutput.Kinds kinds = CardReadyOutput.Kinds.Several;
          string text9 = null;
          int count = readOnlyList.Count;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(readOnlyList.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" files are ready");
          return new CardReadyOutput(kinds, text9, count, defaultInterpolatedStringHandler.ToStringAndClear(), text2 ?? "Open it to see everything it made.", "Open", null, "doc.on.doc.fill", 2269291U, threadId);
        }
        return new CardReadyOutput(CardReadyOutput.Kinds.NothingToOpen, null, 0, "Done", text2 ?? "It finished. Open it to see what it did.", "Open", null, "checkmark", 2269291U, threadId);
      }
    }

    // Token: 0x060021FC RID: 8700 RVA: 0x0061F330 File Offset: 0x0061F330
    public static string CardButtonTitle(CardReadyOutput output)
    {
      if (output.Kind != CardReadyOutput.Kinds.Link || output.Entry == null)
      {
        return output.ButtonTitle;
      }
      ClickyLinkName.Name name = ClickyLinkName.For(output.Entry);
      string text = ((name != null) ? name.Thing : null);
      if (text == null)
      {
        return "Open link";
      }
      string text2 = text.ToLowerInvariant();
      if (text2 == "pull request")
      {
        return "Open PR";
      }
      string text3 = text2.Split(' ', StringSplitOptions.RemoveEmptyEntries).LastOrDefault<string>() ?? text2;
      return "Open " + text3;
    }

    // Token: 0x060021FD RID: 8701 RVA: 0x0061F3B0 File Offset: 0x0061F3B0
    public static bool IsWebLink(string entry)
    {
      return entry.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || entry.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
    }

    // Token: 0x060021FE RID: 8702 RVA: 0x0061F3D0 File Offset: 0x0061F3D0
    private static string OpenTitle(string kindLabel)
    {
      if (kindLabel != null)
      {
        switch (kindLabel.Length)
        {
        case 3:
          if (!(kindLabel == "PDF"))
          {
            goto IL_01AD;
          }
          return "Open PDF";
        case 4:
          if (!(kindLabel == "Text"))
          {
            goto IL_01AD;
          }
          break;
        case 5:
        {
          char c = kindLabel[0];
          if (c != 'A')
          {
            if (c != 'I')
            {
              if (c != 'V')
              {
                goto IL_01AD;
              }
              if (!(kindLabel == "Video"))
              {
                goto IL_01AD;
              }
              return "Play video";
            }
            else
            {
              if (!(kindLabel == "Image"))
              {
                goto IL_01AD;
              }
              return "Open image";
            }
          }
          else
          {
            if (!(kindLabel == "Audio"))
            {
              goto IL_01AD;
            }
            return "Play audio";
          }
          break;
        }
        case 6:
        {
          char c = kindLabel[0];
          if (c != 'F')
          {
            if (c != 'S')
            {
              goto IL_01AD;
            }
            if (!(kindLabel == "Slides"))
            {
              goto IL_01AD;
            }
            return "Open slides";
          }
          else
          {
            if (!(kindLabel == "Folder"))
            {
              goto IL_01AD;
            }
            return "Open folder";
          }
          break;
        }
        case 7:
        case 9:
        case 10:
          goto IL_01AD;
        case 8:
        {
          char c = kindLabel[0];
          if (c != 'D')
          {
            if (c != 'M')
            {
              if (c != 'W')
              {
                goto IL_01AD;
              }
              if (!(kindLabel == "Web page"))
              {
                goto IL_01AD;
              }
              return "Open page";
            }
            else if (!(kindLabel == "Markdown"))
            {
              goto IL_01AD;
            }
          }
          else if (!(kindLabel == "Document"))
          {
            goto IL_01AD;
          }
          break;
        }
        case 11:
          if (!(kindLabel == "Spreadsheet"))
          {
            goto IL_01AD;
          }
          return "Open sheet";
        default:
          goto IL_01AD;
        }
        return "Open doc";
      }
      IL_01AD:
      return "Open";
    }

    // Token: 0x060021FF RID: 8703 RVA: 0x0061F594 File Offset: 0x0061F594
    private static string KindNoun(string kindLabel)
    {
      if (kindLabel != null)
      {
        switch (kindLabel.Length)
        {
        case 3:
          if (!(kindLabel == "PDF"))
          {
            goto IL_00EE;
          }
          return "PDF";
        case 4:
          if (!(kindLabel == "Text"))
          {
            goto IL_00EE;
          }
          break;
        case 5:
        case 7:
        case 9:
        case 10:
          goto IL_00EE;
        case 6:
          if (!(kindLabel == "Folder"))
          {
            goto IL_00EE;
          }
          return "folder";
        case 8:
        {
          char c = kindLabel[0];
          if (c != 'D')
          {
            if (c != 'M')
            {
              if (c != 'W')
              {
                goto IL_00EE;
              }
              if (!(kindLabel == "Web page"))
              {
                goto IL_00EE;
              }
              return "page";
            }
            else if (!(kindLabel == "Markdown"))
            {
              goto IL_00EE;
            }
          }
          else if (!(kindLabel == "Document"))
          {
            goto IL_00EE;
          }
          break;
        }
        case 11:
          if (!(kindLabel == "Spreadsheet"))
          {
            goto IL_00EE;
          }
          return "sheet";
        default:
          goto IL_00EE;
        }
        return "doc";
      }
      IL_00EE:
      return (kindLabel.Length == 0) ? "file" : kindLabel.ToLowerInvariant();
    }

    // Token: 0x06002200 RID: 8704 RVA: 0x0061F6A8 File Offset: 0x0061F6A8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CardReadyOutput");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06002201 RID: 8705 RVA: 0x0061F6F4 File Offset: 0x0061F6F4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", Entry = ");
      builder.Append(this.Entry);
      builder.Append(", Count = ");
      builder.Append(this.Count.ToString());
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Detail = ");
      builder.Append(this.Detail);
      builder.Append(", ButtonTitle = ");
      builder.Append(this.ButtonTitle);
      builder.Append(", ButtonSymbol = ");
      builder.Append(this.ButtonSymbol);
      builder.Append(", Symbol = ");
      builder.Append(this.Symbol);
      builder.Append(", IconBackgroundRgb = ");
      builder.Append(this.IconBackgroundRgb.ToString());
      builder.Append(", ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", OpensFullReplyOnly = ");
      builder.Append(this.OpensFullReplyOnly.ToString());
      builder.Append(", CardEntry = ");
      builder.Append(this.CardEntry);
      return true;
    }

    // Token: 0x06002202 RID: 8706 RVA: 0x0061F86C File Offset: 0x0061F86C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(CardReadyOutput left, CardReadyOutput right)
    {
      return !(left == right);
    }

    // Token: 0x06002203 RID: 8707 RVA: 0x0061F878 File Offset: 0x0061F878
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(CardReadyOutput left, CardReadyOutput right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06002204 RID: 8708 RVA: 0x0061F88C File Offset: 0x0061F88C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<CardReadyOutput.Kinds>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Entry>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Count>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Detail>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ButtonTitle>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ButtonSymbol>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Symbol>k__BackingField)) * -1521134295 + EqualityComparer<uint>.Default.GetHashCode(this.<IconBackgroundRgb>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField);
    }

    // Token: 0x06002205 RID: 8709 RVA: 0x0061F990 File Offset: 0x0061F990
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CardReadyOutput);
    }

    // Token: 0x06002206 RID: 8710 RVA: 0x0061F9A0 File Offset: 0x0061F9A0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(CardReadyOutput other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<CardReadyOutput.Kinds>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Entry>k__BackingField, other.<Entry>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Count>k__BackingField, other.<Count>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Detail>k__BackingField, other.<Detail>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ButtonTitle>k__BackingField, other.<ButtonTitle>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ButtonSymbol>k__BackingField, other.<ButtonSymbol>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Symbol>k__BackingField, other.<Symbol>k__BackingField) && EqualityComparer<uint>.Default.Equals(this.<IconBackgroundRgb>k__BackingField, other.<IconBackgroundRgb>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField));
    }

    // Token: 0x06002208 RID: 8712 RVA: 0x0061FAD8 File Offset: 0x0061FAD8
    [CompilerGenerated]
    private CardReadyOutput(CardReadyOutput original)
    {
      this.Kind = original.<Kind>k__BackingField;
      this.Entry = original.<Entry>k__BackingField;
      this.Count = original.<Count>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Detail = original.<Detail>k__BackingField;
      this.ButtonTitle = original.<ButtonTitle>k__BackingField;
      this.ButtonSymbol = original.<ButtonSymbol>k__BackingField;
      this.Symbol = original.<Symbol>k__BackingField;
      this.IconBackgroundRgb = original.<IconBackgroundRgb>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
    }

    // Token: 0x06002209 RID: 8713 RVA: 0x0061FB64 File Offset: 0x0061FB64
    [CompilerGenerated]
    public void Deconstruct(out CardReadyOutput.Kinds Kind, [Nullable(2)] out string Entry, out int Count, out string Title, out string Detail, out string ButtonTitle, [Nullable(2)] out string ButtonSymbol, out string Symbol, out uint IconBackgroundRgb, [Nullable(2)] out string ThreadId)
    {
      Kind = this.Kind;
      Entry = this.Entry;
      Count = this.Count;
      Title = this.Title;
      Detail = this.Detail;
      ButtonTitle = this.ButtonTitle;
      ButtonSymbol = this.ButtonSymbol;
      Symbol = this.Symbol;
      IconBackgroundRgb = this.IconBackgroundRgb;
      ThreadId = this.ThreadId;
    }

    // Token: 0x02000956 RID: 2390
    [NullableContext(0)]
    public enum Kinds
    {
      // Token: 0x04002741 RID: 10049
      File,
      // Token: 0x04002742 RID: 10050
      Link,
      // Token: 0x04002743 RID: 10051
      Several,
      // Token: 0x04002744 RID: 10052
      NothingToOpen
    }
  }
}
