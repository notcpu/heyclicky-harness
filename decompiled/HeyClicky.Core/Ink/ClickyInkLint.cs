using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200015B RID: 347
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkLint
  {
    // Token: 0x06001517 RID: 5399 RVA: 0x003B3380 File Offset: 0x003B3380
    public static List<string> Problems(string text)
    {
      ClickyInkDocument clickyInkDocument = ClickyInkParser.Parse(text, false);
      ClickyInkLint.<>c__DisplayClass2_0 CS$<>8__locals1;
      CS$<>8__locals1.problems = new List<string>();
      if (string.IsNullOrEmpty(clickyInkDocument.Title))
      {
        ClickyInkLint.<Problems>g__Add|2_0("The note has no # title: start it with a two-to-four word title.", ref CS$<>8__locals1);
      }
      if (clickyInkDocument.Pages.Count > 4)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(73, 1);
        defaultInterpolatedStringHandler.AppendLiteral("The note has ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(clickyInkDocument.Pages.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" pages; keep it to four at most (merge or cut the thinnest).");
        ClickyInkLint.<Problems>g__Add|2_0(defaultInterpolatedStringHandler.ToStringAndClear(), ref CS$<>8__locals1);
      }
      int num = text.Components("{now}").Length - 1;
      if (num > 1)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(91, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(num);
        defaultInterpolatedStringHandler.AppendLiteral(" lines are marked {now}: keep {now} on the one step you're on, and tick or drop the others.");
        ClickyInkLint.<Problems>g__Add|2_0(defaultInterpolatedStringHandler.ToStringAndClear(), ref CS$<>8__locals1);
      }
      for (int j = 0; j < clickyInkDocument.Pages.Count; j++)
      {
        ClickyInkDocument.Page page = clickyInkDocument.Pages[j];
        string text2;
        if (page.Name.Length != 0)
        {
          text2 = "“" + page.Name + "”";
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
          defaultInterpolatedStringHandler.AppendLiteral("page ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(j + 1);
          text2 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        string text3 = text2;
        foreach (ClickyInkDocument.Block block in page.Blocks)
        {
          List<string> list = new List<string> { block.Text };
          foreach (ClickyInkDocument.Item item in block.Items)
          {
            list.Add(item.Text);
            list.Add(item.Detail ?? "");
            list.AddRange(item.Cells);
          }
          list.AddRange(block.Columns);
          foreach (string text4 in list)
          {
            string text5 = ClickyInkLint.RawMarkup(text4);
            if (text5 != null)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(78, 3);
              defaultInterpolatedStringHandler.AppendLiteral("On ");
              defaultInterpolatedStringHandler.AppendFormatted(text3);
              defaultInterpolatedStringHandler.AppendLiteral(", “");
              defaultInterpolatedStringHandler.AppendFormatted(ClickyInkLint.Short(text4));
              defaultInterpolatedStringHandler.AppendLiteral("” shows ");
              defaultInterpolatedStringHandler.AppendFormatted(text5);
              defaultInterpolatedStringHandler.AppendLiteral(" as raw text: write it as an object on its own line, or drop it.");
              ClickyInkLint.<Problems>g__Add|2_0(defaultInterpolatedStringHandler.ToStringAndClear(), ref CS$<>8__locals1);
            }
          }
          ClickyInkDocument.Kind kind = block.Kind;
          switch (kind)
          {
          case ClickyInkDocument.Kind.Paragraph:
            if (block.Text.CharacterCount() > 260)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(63, 2);
              defaultInterpolatedStringHandler.AppendLiteral("On ");
              defaultInterpolatedStringHandler.AppendFormatted(text3);
              defaultInterpolatedStringHandler.AppendLiteral(", a paragraph runs ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(block.Text.CharacterCount());
              defaultInterpolatedStringHandler.AppendLiteral(" characters: two short sentences at most.");
              ClickyInkLint.<Problems>g__Add|2_0(defaultInterpolatedStringHandler.ToStringAndClear(), ref CS$<>8__locals1);
            }
            break;
          case ClickyInkDocument.Kind.Answer:
          {
            string text6 = block["big"];
            if (text6 != null && text6.CharacterCount() > 12)
            {
              ClickyInkLint.<Problems>g__Add|2_0("The answer's big=\"" + text6 + "\" is too long for the circle: use the number that matters (a price, a count) or the short name people already call it, under 12 characters. Never squash words together.", ref CS$<>8__locals1);
            }
            break;
          }
          case ClickyInkDocument.Kind.Quote:
          case ClickyInkDocument.Kind.Code:
          case ClickyInkDocument.Kind.Rule:
          case ClickyInkDocument.Kind.Figure:
          case ClickyInkDocument.Kind.Sticky:
          case ClickyInkDocument.Kind.Receipt:
            break;
          case ClickyInkDocument.Kind.List:
          case ClickyInkDocument.Kind.Checklist:
          case ClickyInkDocument.Kind.Numbered:
            if (block.Items.Count > 6)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(81, 2);
              defaultInterpolatedStringHandler.AppendLiteral("On ");
              defaultInterpolatedStringHandler.AppendFormatted(text3);
              defaultInterpolatedStringHandler.AppendLiteral(", a list has ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(block.Items.Count);
              defaultInterpolatedStringHandler.AppendLiteral(" items: keep the five that matter, or make it a table or a chart.");
              ClickyInkLint.<Problems>g__Add|2_0(defaultInterpolatedStringHandler.ToStringAndClear(), ref CS$<>8__locals1);
            }
            break;
          case ClickyInkDocument.Kind.Table:
          {
            if (block.Columns.Count > 4)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(77, 2);
              defaultInterpolatedStringHandler.AppendLiteral("On ");
              defaultInterpolatedStringHandler.AppendFormatted(text3);
              defaultInterpolatedStringHandler.AppendLiteral(", a table has ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(block.Columns.Count);
              defaultInterpolatedStringHandler.AppendLiteral(" columns: four at most fit the page (put the rest under it).");
              ClickyInkLint.<Problems>g__Add|2_0(defaultInterpolatedStringHandler.ToStringAndClear(), ref CS$<>8__locals1);
            }
            string text7 = block.Items.SelectMany<ClickyInkDocument.Item, string>((ClickyInkDocument.Item i) => i.Cells).FirstOrDefault<string>((string c) => c.CharacterCount() > 34);
            if (text7 != null)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(97, 2);
              defaultInterpolatedStringHandler.AppendLiteral("On ");
              defaultInterpolatedStringHandler.AppendFormatted(text3);
              defaultInterpolatedStringHandler.AppendLiteral(", the table cell “");
              defaultInterpolatedStringHandler.AppendFormatted(ClickyInkLint.Short(text7));
              defaultInterpolatedStringHandler.AppendLiteral("” is too long: a number or two or three words; the why goes under the table.");
              ClickyInkLint.<Problems>g__Add|2_0(defaultInterpolatedStringHandler.ToStringAndClear(), ref CS$<>8__locals1);
            }
            break;
          }
          case ClickyInkDocument.Kind.Ticket:
            if (block["from"] == null || block["to"] == null)
            {
              ClickyInkLint.<Problems>g__Add|2_0("The ticket “" + ClickyInkLint.Short(block.Text) + "” needs from= and to=, or it isn't a ticket.", ref CS$<>8__locals1);
            }
            break;
          case ClickyInkDocument.Kind.Photo:
            if (block["query"] == null && block["path"] == null)
            {
              ClickyInkLint.<Problems>g__Add|2_0("The photo “" + ClickyInkLint.Short(block.Text) + "” needs query= (what to search a photo for) or path=.", ref CS$<>8__locals1);
            }
            break;
          default:
            if (kind == ClickyInkDocument.Kind.Unknown)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(175, 2);
              defaultInterpolatedStringHandler.AppendLiteral("On ");
              defaultInterpolatedStringHandler.AppendFormatted(text3);
              defaultInterpolatedStringHandler.AppendLiteral(", “");
              defaultInterpolatedStringHandler.AppendFormatted(ClickyInkLint.Short(block.Text));
              defaultInterpolatedStringHandler.AppendLiteral("” isn't a paper object the page knows: use one from the brief (figure, sticky, ticket, receipt, photo, place, day, letter, clip, chart, file, link, ask, question, note).");
              ClickyInkLint.<Problems>g__Add|2_0(defaultInterpolatedStringHandler.ToStringAndClear(), ref CS$<>8__locals1);
            }
            break;
          }
        }
      }
      return CS$<>8__locals1.problems.Take<string>(8).ToList<string>();
    }

    // Token: 0x06001518 RID: 5400 RVA: 0x003B39EC File Offset: 0x003B39EC
    [return: Nullable(2)]
    public static string FileText(IReadOnlyList<string> problems)
    {
      if (problems.Count == 0)
      {
        return null;
      }
      return "What will look broken on the page (fix these lines in your next save of note.md):\n" + string.Join("\n", problems.Select<string, string>((string p) => "- " + p)) + "\n";
    }

    // Token: 0x06001519 RID: 5401 RVA: 0x003B3A44 File Offset: 0x003B3A44
    [return: Nullable(2)]
    private static string RawMarkup(string text)
    {
      foreach (ValueTuple<string, string> valueTuple in new ValueTuple<string, string>[]
      {
        new ValueTuple<string, string>("::", "“::”"),
        new ValueTuple<string, string>("](", "a markdown link"),
        new ValueTuple<string, string>("**", "“**”"),
        new ValueTuple<string, string>("`", "a backtick"),
        new ValueTuple<string, string>("{", "“{”"),
        new ValueTuple<string, string>("}", "“}”"),
        new ValueTuple<string, string>("<", "“<”")
      })
      {
        string item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        if (text.Contains(item, StringComparison.Ordinal))
        {
          return item2;
        }
      }
      return null;
    }

    // Token: 0x0600151A RID: 5402 RVA: 0x003B3B24 File Offset: 0x003B3B24
    private static string Short(string text)
    {
      string text2 = text.Replace("\n", " ");
      if (text2.CharacterCount() <= 48)
      {
        return text2;
      }
      return text2.PrefixCharacters(47) + "…";
    }

    // Token: 0x0600151B RID: 5403 RVA: 0x003B3B60 File Offset: 0x003B3B60
    [CompilerGenerated]
    internal static void <Problems>g__Add|2_0(string problem, ref ClickyInkLint.<>c__DisplayClass2_0 A_1)
    {
      if (!A_1.problems.Contains(problem))
      {
        A_1.problems.Add(problem);
      }
    }

    // Token: 0x04000927 RID: 2343
    public const string FileName = "note.problems";

    // Token: 0x04000928 RID: 2344
    public const int MaximumProblems = 8;
  }
}
