using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000148 RID: 328
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkParser
  {
    // Token: 0x0600149F RID: 5279 RVA: 0x003ADE58 File Offset: 0x003ADE58
    public static ClickyInkDocument Parse(string source, bool isPartial = false)
    {
      List<string> list = source.Replace("\r\n", "\n").Split('\n', StringSplitOptions.None).ToList<string>();
      list = list.SelectMany<string, string>(delegate(string line)
      {
        string text21 = line.TrimWs();
        if (!text21.HasPrefix("::") || text21.HasPrefix(":::") || !text21.Contains("} ::", StringComparison.Ordinal))
        {
          return new string[] { line };
        }
        string[] pieces = text21.Components("} ::");
        return pieces.Select<string, string>((string piece, int index) => ((index == 0) ? "" : "::") + piece + ((piece.HasSuffix("}") || index == pieces.Length - 1) ? "" : "}")).ToArray<string>();
      }).ToList<string>();
      if (isPartial && !source.HasSuffix("\n") && list.Count > 0)
      {
        List<string> list2 = list;
        string text = list2[list2.Count - 1];
        string text2 = text.TrimWs();
        bool flag = text2.HasPrefix("::") || text2.HasPrefix("|") || text2.HasPrefix("```") || text2.HasPrefix("#") || text2.Contains('{') || !text2.Contains(' ') || text2.HasPrefix("[");
        list.RemoveAt(list.Count - 1);
        int num = text.LastIndexOf(' ');
        if (!flag && num >= 0)
        {
          string text3 = text.Substring(0, num);
          ClickyInkParser.ListStart listStart = ClickyInkParser.ListItemStart(text3.TrimWs());
          if ((((listStart != null) ? listStart.Text : null) ?? text3).TrimWs().Length > 0)
          {
            list.Add(text3);
          }
        }
      }
      ClickyInkParser.Builder builder = new ClickyInkParser.Builder();
      int i = 0;
      while (i < list.Count)
      {
        string text4 = list[i].TrimWs();
        i++;
        if (text4.Length == 0)
        {
          builder.EndParagraph();
        }
        else if (text4.HasPrefix("```"))
        {
          builder.EndParagraph();
          string text5 = text4;
          string text6 = text5.Substring(3, text5.Length - 3).TrimWs().Lowered();
          List<string> list3 = new List<string>();
          bool flag2 = false;
          while (i < list.Count)
          {
            string text7 = list[i];
            i++;
            if (text7.TrimWs().HasPrefix("```"))
            {
              flag2 = true;
              break;
            }
            list3.Add(text7);
          }
          if (!flag2 && isPartial)
          {
            break;
          }
          bool flag3 = text6 == "draw" || text6 == "sketch";
          if (flag3)
          {
            ClickyInkParser.Builder builder2 = builder;
            ClickyInkDocument.Kind kind = ClickyInkDocument.Kind.Drawing;
            string text8 = "";
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["lang"] = text6;
            dictionary["sketch"] = string.Join("\n", list3);
            builder2.Add(new ClickyInkDocument.Block(kind, text8, dictionary, null, null));
          }
          else
          {
            ClickyInkParser.Builder builder3 = builder;
            ClickyInkDocument.Kind kind2 = ClickyInkDocument.Kind.Code;
            string text9 = string.Join("\n", list3);
            Dictionary<string, string> dictionary2;
            if (text6.Length != 0)
            {
              (dictionary2 = new Dictionary<string, string>())["lang"] = text6;
            }
            else
            {
              dictionary2 = new Dictionary<string, string>();
            }
            builder3.Add(new ClickyInkDocument.Block(kind2, text9, dictionary2, null, null));
          }
        }
        else
        {
          if (text4.HasPrefix(":::"))
          {
            builder.EndParagraph();
            string text5 = text4;
            string text10 = text5.Substring(3, text5.Length - 3);
            if (text10.TrimWs().Length == 0)
            {
              continue;
            }
            ClickyInkParser.DirectiveHeader directiveHeader = ClickyInkParser.ParseDirectiveHeader(text10);
            bool flag4 = ClickyInkParser.ObjectHoldingContainers.Contains(directiveHeader.Name);
            bool flag5 = isPartial;
            for (int j = i; j < list.Count; j++)
            {
              string text11 = list[j].TrimWs();
              if (text11 == ":::")
              {
                flag5 = true;
                break;
              }
              if (text11.HasPrefix("# ") || text11.HasPrefix("## ") || (text11.HasPrefix(":::") && text11.Length > 3))
              {
                flag5 = false;
                break;
              }
            }
            List<string> list4 = new List<string>();
            List<string> list5 = new List<string>();
            bool flag6 = false;
            while (i < list.Count)
            {
              string text12 = list[i];
              string text13 = text12.TrimWs();
              if (text13 == ":::")
              {
                i++;
                flag6 = true;
                break;
              }
              if (!flag4 && flag5 && text13.HasPrefix("::") && !text13.HasPrefix(":::"))
              {
                list5.Add(text13);
                i++;
              }
              else
              {
                if (text13.HasPrefix("# ") || text13.HasPrefix("## ") || (text13.HasPrefix(":::") && text13.Length > 3) || (!flag4 && text13.HasPrefix("::") && !text13.HasPrefix(":::")))
                {
                  flag6 = true;
                  break;
                }
                list4.Add(text12);
                i++;
              }
            }
            if (!flag6 && isPartial)
            {
              builder.AddContainer(directiveHeader, list4);
              using (List<string>.Enumerator enumerator = list5.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  string text14 = enumerator.Current;
                  ClickyInkParser.Builder builder4 = builder;
                  text5 = text14;
                  builder4.AddLeaf(ClickyInkParser.ParseDirectiveHeader(text5.Substring(2, text5.Length - 2)));
                }
                break;
              }
            }
            builder.AddContainer(directiveHeader, list4);
            using (List<string>.Enumerator enumerator = list5.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                string text15 = enumerator.Current;
                ClickyInkParser.Builder builder5 = builder;
                text5 = text15;
                builder5.AddLeaf(ClickyInkParser.ParseDirectiveHeader(text5.Substring(2, text5.Length - 2)));
              }
              continue;
            }
          }
          if ((text4.HasPrefix("- ::") || text4.HasPrefix("* ::")) && !text4.HasPrefix("- :::"))
          {
            builder.EndParagraph();
            ClickyInkParser.Builder builder6 = builder;
            string text5 = text4;
            builder6.AddLeaf(ClickyInkParser.ParseDirectiveHeader(text5.Substring(4, text5.Length - 4)));
          }
          else if (text4.HasPrefix("::"))
          {
            builder.EndParagraph();
            ClickyInkParser.Builder builder7 = builder;
            string text5 = text4;
            builder7.AddLeaf(ClickyInkParser.ParseDirectiveHeader(text5.Substring(2, text5.Length - 2)));
          }
          else if (text4.HasPrefix("# "))
          {
            builder.EndParagraph();
            string text5 = text4;
            string text16 = ClickyInkParser.Clean(text5.Substring(2, text5.Length - 2));
            if (builder.Document.Title == null)
            {
              builder.Document.Title = text16;
            }
            else
            {
              builder.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Heading, text16, null, null, null));
            }
          }
          else if (text4.HasPrefix("## "))
          {
            builder.EndParagraph();
            string text5 = text4;
            ValueTuple<string, Dictionary<string, string>> valueTuple = ClickyInkParser.TrailingAttributes(text5.Substring(3, text5.Length - 3));
            string item = valueTuple.Item1;
            Dictionary<string, string> item2 = valueTuple.Item2;
            string text17;
            builder.StartPage(ClickyInkParser.Clean(item), item2.ContainsKey("board") || (item2.TryGetValue("layout", out text17) && text17 == "board"));
          }
          else if (text4.HasPrefix("### ") || text4.HasPrefix("#### "))
          {
            builder.EndParagraph();
            builder.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Heading, ClickyInkParser.Clean(text4.TrimStart('#').TrimWs()), null, null, null));
          }
          else
          {
            bool flag3 = text4 == "---" || text4 == "***" || text4 == "___";
            if (flag3)
            {
              builder.EndParagraph();
              builder.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Rule, "", null, null, null));
            }
            else if (text4.HasPrefix(">"))
            {
              builder.EndParagraph();
              List<string> list6 = new List<string>();
              string text5 = text4;
              list6.Add(text5.Substring(1, text5.Length - 1).TrimWs());
              List<string> list7 = list6;
              while (i < list.Count && list[i].TrimWs().HasPrefix(">"))
              {
                List<string> list8 = list7;
                text5 = list[i].TrimWs();
                list8.Add(text5.Substring(1, text5.Length - 1).TrimWs());
                i++;
              }
              ValueTuple<string, Dictionary<string, string>> valueTuple2 = ClickyInkParser.TrailingAttributes(string.Join(" ", list7));
              string item3 = valueTuple2.Item1;
              Dictionary<string, string> item4 = valueTuple2.Item2;
              bool flag7;
              if (builder.Document.Pages.Count <= 1)
              {
                List<ClickyInkDocument.Block> currentBlocks = builder.CurrentBlocks;
                flag7 = ((currentBlocks != null) ? currentBlocks.Count : 0) == 0;
              }
              else
              {
                flag7 = false;
              }
              bool flag8 = flag7;
              builder.Add(new ClickyInkDocument.Block((flag8 || item4.ContainsKey("big")) ? ClickyInkDocument.Kind.Answer : ClickyInkDocument.Kind.Quote, ClickyInkParser.Clean(item3), item4, null, null));
            }
            else if (text4.HasPrefix("|"))
            {
              builder.EndParagraph();
              List<string> list9 = new List<string> { text4 };
              while (i < list.Count && list[i].TrimWs().HasPrefix("|"))
              {
                list9.Add(list[i].TrimWs());
                i++;
              }
              builder.Add(ClickyInkParser.ParseTable(list9));
            }
            else
            {
              ClickyInkParser.ListStart listStart2 = ClickyInkParser.ListItemStart(text4);
              if (listStart2 != null)
              {
                builder.EndParagraph();
                List<ValueTuple<ClickyInkParser.ListStart, List<string>>> list10 = new List<ValueTuple<ClickyInkParser.ListStart, List<string>>>
                {
                  new ValueTuple<ClickyInkParser.ListStart, List<string>>(listStart2, new List<string>())
                };
                while (i < list.Count)
                {
                  string text18 = list[i];
                  string text19 = text18.TrimWs();
                  if (text19.Length == 0)
                  {
                    if (i + 1 >= list.Count)
                    {
                      break;
                    }
                    ClickyInkParser.ListStart listStart3 = ClickyInkParser.ListItemStart(list[i + 1].TrimWs());
                    if (listStart3 == null || listStart3.Kind != listStart2.Kind)
                    {
                      break;
                    }
                    i++;
                  }
                  else if ((text18.HasPrefix("  ") || text18.HasPrefix("\t")) && text19.Length > 0)
                  {
                    ClickyInkParser.ListStart listStart4 = ClickyInkParser.ListItemStart(text19);
                    string text20 = ((listStart4 != null) ? listStart4.Text : null) ?? text19;
                    List<ValueTuple<ClickyInkParser.ListStart, List<string>>> list11 = list10;
                    list11[list11.Count - 1].Item2.Add(text20);
                    i++;
                  }
                  else
                  {
                    ClickyInkParser.ListStart listStart5 = ClickyInkParser.ListItemStart(text19);
                    if (listStart5 == null || listStart5.Kind != listStart2.Kind)
                    {
                      break;
                    }
                    list10.Add(new ValueTuple<ClickyInkParser.ListStart, List<string>>(listStart5, new List<string>()));
                    i++;
                  }
                }
                builder.Add(new ClickyInkDocument.Block(listStart2.Kind, "", null, list10.Take<ValueTuple<ClickyInkParser.ListStart, List<string>>>(40).Select<ValueTuple<ClickyInkParser.ListStart, List<string>>, ClickyInkDocument.Item>(delegate([TupleElementNames(new string[] { "Start", "Detail" })] ValueTuple<ClickyInkParser.ListStart, List<string>> entry)
                {
                  ClickyInkDocument.Item item5 = entry.Item1.Item;
                  if (entry.Item2.Count > 0)
                  {
                    ClickyInkDocument.Item item6 = item5;
                    string text22 = " ";
                    IEnumerable<string> item7 = entry.Item2;
                    Func<string, string> func;
                    if ((func = ClickyInkParser.<>O.<0>__Clean) == null)
                    {
                      func = (ClickyInkParser.<>O.<0>__Clean = new Func<string, string>(ClickyInkParser.Clean));
                    }
                    item6.Detail = string.Join(text22, item7.Select<string, string>(func));
                  }
                  return item5;
                }).ToList<ClickyInkDocument.Item>(), null));
              }
              else
              {
                builder.AppendParagraphLine(text4);
              }
            }
          }
        }
      }
      builder.EndParagraph();
      return builder.Finished();
    }

    // Token: 0x060014A0 RID: 5280 RVA: 0x003AE8CC File Offset: 0x003AE8CC
    public static ClickyInkParser.DirectiveHeader ParseDirectiveHeader(string text)
    {
      string text2 = text.TrimWs();
      int num = 0;
      for (;;)
      {
        bool flag = num < text2.Length;
        if (flag)
        {
          bool flag2 = char.IsLetter(text2, num);
          if (!flag2)
          {
            char c = text2[num];
            bool flag3 = c == '-' || c == '_';
            flag2 = flag3;
          }
          flag = flag2;
        }
        if (!flag)
        {
          break;
        }
        num += (char.IsSurrogatePair(text2, num) ? 2 : 1);
      }
      string text3 = text2.Substring(0, num).Lowered();
      string text4 = text2;
      int num2 = num;
      text2 = text4.Substring(num2, text4.Length - num2);
      string text5 = "";
      if (text2.Length > 0 && text2[0] == '[')
      {
        int num3 = 0;
        int num4 = 0;
        for (int i = 0; i < text2.Length; i++)
        {
          if (text2[i] == '[')
          {
            num3++;
          }
          if (text2[i] == ']')
          {
            num3--;
            if (num3 == 0)
            {
              num4 = i;
              break;
            }
          }
        }
        if (num4 > 0)
        {
          text5 = text2.Substring(1, num4 - 1);
          text4 = text2;
          num2 = num4 + 1;
          text2 = text4.Substring(num2, text4.Length - num2);
        }
        else
        {
          text4 = text2;
          text5 = text4.Substring(1, text4.Length - 1);
          text2 = "";
        }
      }
      ValueTuple<string, Dictionary<string, string>> valueTuple = ClickyInkParser.TrailingAttributes(text2);
      string item = valueTuple.Item1;
      Dictionary<string, string> item2 = valueTuple.Item2;
      if (text5.Length == 0)
      {
        text5 = item.TrimWs();
      }
      int num5 = text5.IndexOf('{');
      if (num5 >= 0)
      {
        text5 = text5.Substring(0, num5).TrimWs();
      }
      return new ClickyInkParser.DirectiveHeader(text3, ClickyInkParser.Clean(text5), item2);
    }

    // Token: 0x060014A1 RID: 5281 RVA: 0x003AEA68 File Offset: 0x003AEA68
    [return: TupleElementNames(new string[] { "Words", "Attributes" })]
    [return: Nullable(new byte[] { 0, 1, 1, 1, 1 })]
    public static ValueTuple<string, Dictionary<string, string>> TrailingAttributes(string text)
    {
      ValueTuple<string, Dictionary<string, string>> valueTuple = ClickyInkParser.TrailingAttributeGroup(text);
      string text2 = valueTuple.Item1;
      Dictionary<string, string> item = valueTuple.Item2;
      while (item.Count > 0 && text2.HasSuffix("}"))
      {
        ValueTuple<string, Dictionary<string, string>> valueTuple2 = ClickyInkParser.TrailingAttributeGroup(text2);
        string item2 = valueTuple2.Item1;
        Dictionary<string, string> item3 = valueTuple2.Item2;
        if (item3.Count == 0)
        {
          break;
        }
        text2 = item2;
        foreach (KeyValuePair<string, string> keyValuePair in item3)
        {
          string text3;
          string text4;
          keyValuePair.Deconstruct(out text3, out text4);
          string text5 = text3;
          string text6 = text4;
          item.TryAdd(text5, text6);
        }
      }
      return new ValueTuple<string, Dictionary<string, string>>(text2, item);
    }

    // Token: 0x060014A2 RID: 5282 RVA: 0x003AEB20 File Offset: 0x003AEB20
    [return: Nullable(new byte[] { 0, 1, 1, 1, 1 })]
    private static ValueTuple<string, Dictionary<string, string>> TrailingAttributeGroup(string text)
    {
      int num = text.LastIndexOf('{');
      if (!text.HasSuffix("}") || num < 0)
      {
        return new ValueTuple<string, Dictionary<string, string>>(text.TrimWs(), new Dictionary<string, string>());
      }
      int num2 = num + 1;
      ClickyInkParser.<>c__DisplayClass9_0 CS$<>8__locals1;
      CS$<>8__locals1.inside = text.Substring(num2, text.Length - 1 - num2);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      CS$<>8__locals1.position = 0;
      CS$<>8__locals1.end = CS$<>8__locals1.inside.Length;
      while (CS$<>8__locals1.position < CS$<>8__locals1.end)
      {
        ClickyInkParser.<TrailingAttributeGroup>g__SkipSeparators|9_0(ref CS$<>8__locals1);
        if (CS$<>8__locals1.position >= CS$<>8__locals1.end)
        {
          break;
        }
        int position = CS$<>8__locals1.position;
        while (CS$<>8__locals1.position < CS$<>8__locals1.end && "=: ,;".IndexOf(CS$<>8__locals1.inside[CS$<>8__locals1.position]) < 0)
        {
          CS$<>8__locals1.position++;
        }
        string inside = CS$<>8__locals1.inside;
        num2 = position;
        string text2 = inside.Substring(num2, CS$<>8__locals1.position - num2).Lowered().TrimSet("#.");
        if (text2.Length == 0)
        {
          if (CS$<>8__locals1.position < CS$<>8__locals1.end)
          {
            CS$<>8__locals1.position++;
          }
        }
        else
        {
          bool flag = CS$<>8__locals1.position < CS$<>8__locals1.end;
          if (flag)
          {
            char c = CS$<>8__locals1.inside[CS$<>8__locals1.position];
            bool flag2 = c == ':' || c == '=';
            flag = flag2;
          }
          if (flag)
          {
            CS$<>8__locals1.position++;
            flag = CS$<>8__locals1.position < CS$<>8__locals1.end;
            if (flag)
            {
              char c = CS$<>8__locals1.inside[CS$<>8__locals1.position];
              bool flag2 = c == '"' || c == '\'' || c == '“';
              flag = flag2;
            }
            string text3;
            if (flag)
            {
              char c2 = ((CS$<>8__locals1.inside[CS$<>8__locals1.position] == '“') ? '”' : CS$<>8__locals1.inside[CS$<>8__locals1.position]);
              CS$<>8__locals1.position++;
              int position2 = CS$<>8__locals1.position;
              while (CS$<>8__locals1.position < CS$<>8__locals1.end && CS$<>8__locals1.inside[CS$<>8__locals1.position] != c2)
              {
                CS$<>8__locals1.position++;
              }
              string inside2 = CS$<>8__locals1.inside;
              num2 = position2;
              text3 = inside2.Substring(num2, CS$<>8__locals1.position - num2);
              if (CS$<>8__locals1.position < CS$<>8__locals1.end)
              {
                CS$<>8__locals1.position++;
              }
            }
            else
            {
              int position3 = CS$<>8__locals1.position;
              while (CS$<>8__locals1.position < CS$<>8__locals1.end)
              {
                char c3 = CS$<>8__locals1.inside[CS$<>8__locals1.position];
                flag = c3 == ' ' || c3 == ',';
                if (flag)
                {
                  int num3 = CS$<>8__locals1.position + 1;
                  for (;;)
                  {
                    flag = num3 < CS$<>8__locals1.end;
                    if (flag)
                    {
                      char c = CS$<>8__locals1.inside[num3];
                      bool flag2 = c == ' ' || c == ',';
                      flag = flag2;
                    }
                    if (!flag)
                    {
                      break;
                    }
                    num3++;
                  }
                  bool flag3 = num3 >= CS$<>8__locals1.end;
                  int num4 = num3;
                  for (;;)
                  {
                    flag = num4 < CS$<>8__locals1.end;
                    if (flag)
                    {
                      bool flag2 = char.IsLetterOrDigit(CS$<>8__locals1.inside[num4]) || char.IsNumber(CS$<>8__locals1.inside[num4]);
                      if (!flag2)
                      {
                        char c = CS$<>8__locals1.inside[num4];
                        bool flag4 = c == '-' || c == '_';
                        flag2 = flag4;
                      }
                      flag = flag2;
                    }
                    if (!flag)
                    {
                      break;
                    }
                    num4++;
                  }
                  bool flag5 = num4 > num3 && char.IsLetter(CS$<>8__locals1.inside[num3]);
                  int num5 = num4;
                  while (num5 < CS$<>8__locals1.end && CS$<>8__locals1.inside[num5] == ' ')
                  {
                    num5++;
                  }
                  flag = num5 < CS$<>8__locals1.end;
                  if (flag)
                  {
                    char c = CS$<>8__locals1.inside[num5];
                    bool flag2 = c == ':' || c == '=';
                    flag = flag2;
                  }
                  bool flag6 = flag;
                  if (flag3 || (flag5 && flag6))
                  {
                    break;
                  }
                }
                CS$<>8__locals1.position++;
              }
              string inside3 = CS$<>8__locals1.inside;
              num2 = position3;
              text3 = inside3.Substring(num2, CS$<>8__locals1.position - num2).TrimWs();
            }
            dictionary[text2] = text3;
          }
          else
          {
            dictionary[text2] = "";
          }
        }
      }
      if (dictionary.Count == 0)
      {
        return new ValueTuple<string, Dictionary<string, string>>(text.TrimWs(), new Dictionary<string, string>());
      }
      return new ValueTuple<string, Dictionary<string, string>>(text.Substring(0, num).TrimWs(), dictionary);
    }

    // Token: 0x060014A3 RID: 5283 RVA: 0x003AEFE8 File Offset: 0x003AEFE8
    [return: Nullable(2)]
    public static ClickyInkParser.ListStart ListItemStart(string line)
    {
      ClickyInkDocument.Kind kind = ClickyInkDocument.Kind.List;
      int? num = null;
      string text;
      if (!line.HasPrefix("- ") && !line.HasPrefix("* ") && !line.HasPrefix("• ") && !line.HasPrefix("+ "))
      {
        int num2 = -1;
        for (int i = 0; i < line.Length; i++)
        {
          char c = line[i];
          bool flag = c == ')' || c == '.';
          if (flag)
          {
            num2 = i;
            break;
          }
        }
        if (num2 > 0)
        {
          IEnumerable<char> enumerable = line.Substring(0, num2);
          Func<char, bool> func;
          if ((func = ClickyInkParser.<>O.<1>__IsNumber) == null)
          {
            func = (ClickyInkParser.<>O.<1>__IsNumber = new Func<char, bool>(char.IsNumber));
          }
          if (enumerable.All<char>(func) && line.Substring(0, num2).CharacterCount() <= 3 && num2 + 1 < line.Length && line[num2 + 1] == ' ')
          {
            long? num3 = InkText.ParseInt(line.Substring(0, num2));
            int? num4;
            if (num3 != null)
            {
              long valueOrDefault = num3.GetValueOrDefault();
              num4 = new int?((int)valueOrDefault);
            }
            else
            {
              num4 = null;
            }
            num = num4;
            int num5 = num2 + 2;
            text = line.Substring(num5, line.Length - num5);
            kind = ClickyInkDocument.Kind.Numbered;
            goto IL_0155;
          }
        }
        return null;
      }
      text = line.Substring(2, line.Length - 2);
      IL_0155:
      bool? flag2 = null;
      if (text.HasPrefix("[ ] ") || text.HasPrefix("[ ]"))
      {
        flag2 = new bool?(false);
        string text2 = text;
        text = text2.Substring(3, text2.Length - 3);
      }
      else if (text.Lowered().HasPrefix("[x]"))
      {
        flag2 = new bool?(true);
        string text2 = text;
        text = text2.Substring(3, text2.Length - 3);
      }
      if (flag2 != null)
      {
        kind = ClickyInkDocument.Kind.Checklist;
      }
      ClickyInkDocument.Mark? mark = null;
      int num6 = text.IndexOf('}');
      if (text.HasPrefix("{") && num6 >= 0)
      {
        ClickyInkDocument.Mark? mark2 = ClickyInkDocumentKinds.MarkFromWord(text.Substring(1, num6 - 1).TrimWs());
        if (mark2 != null)
        {
          ClickyInkDocument.Mark valueOrDefault2 = mark2.GetValueOrDefault();
          mark = new ClickyInkDocument.Mark?(valueOrDefault2);
          string text2 = text;
          int num5 = num6 + 1;
          text = text2.Substring(num5, text2.Length - num5);
        }
      }
      ValueTuple<string, Dictionary<string, string>> valueTuple = ClickyInkParser.TrailingAttributes(text.TrimWs());
      string text3 = valueTuple.Item1;
      Dictionary<string, string> item = valueTuple.Item2;
      if (mark != null)
      {
        ClickyInkDocument.Mark valueOrDefault3 = mark.GetValueOrDefault();
        item[valueOrDefault3.RawValue()] = "";
      }
      ValueTuple<string, ClickyInkDocument.Mark?> valueTuple2 = ClickyInkParser.ExtractingMarks(text3);
      string item2 = valueTuple2.Item1;
      ClickyInkDocument.Mark? item3 = valueTuple2.Item2;
      text3 = item2;
      if (item3 != null)
      {
        ClickyInkDocument.Mark valueOrDefault4 = item3.GetValueOrDefault();
        string text4;
        item[valueOrDefault4.RawValue()] = (item.TryGetValue(valueOrDefault4.RawValue(), out text4) ? text4 : "");
      }
      ClickyInkDocument.Mark? mark3 = ClickyInkDocumentKinds.FirstMark(item.Keys);
      string text5 = null;
      int num7 = text3.IndexOf(']');
      if (text3.HasPrefix("[") && num7 >= 0 && num7 + 1 < text3.Length && text3[num7 + 1] == '(')
      {
        int num8 = text3.IndexOf(')', num7);
        if (num8 >= 0)
        {
          string text6 = text3;
          int num5 = num7 + 2;
          string text7 = text6.Substring(num5, num8 - num5);
          if (text7.HasPrefix("http"))
          {
            text5 = text7;
            string text8 = "\ue005";
            string text9 = text3.Substring(1, num7 - 1);
            string text10 = "\ue006";
            string text2 = text3;
            num5 = num8 + 1;
            text3 = text8 + text9 + text10 + text2.Substring(num5, text2.Length - num5);
          }
        }
      }
      string text11;
      if (text5 == null && item.TryGetValue("url", out text11) && text11.HasPrefix("http"))
      {
        text5 = text11;
      }
      if (text5 == null)
      {
        text5 = ClickyInkParser.InlineLinks(text3).Item2;
        if (text5 != null)
        {
          text3 = ClickyInkParser.MarkingFirstLink(text3);
        }
      }
      return new ClickyInkParser.ListStart(kind, new ClickyInkDocument.Item(ClickyInkParser.Clean(text3), null, null, mark3, flag2, text5, num));
    }

    // Token: 0x060014A4 RID: 5284 RVA: 0x003AF3F4 File Offset: 0x003AF3F4
    public static ClickyInkDocument.Block ParseTable(List<string> rows)
    {
      List<string> list = rows.Where<string>((string r) => !ClickyInkParser.<ParseTable>g__IsDivider|12_1(r)).ToList<string>();
      if (list.Count == 0)
      {
        return new ClickyInkDocument.Block(ClickyInkDocument.Kind.Table, "", null, null, null);
      }
      string text = list[0];
      List<ClickyInkDocument.Item> list2 = list.Skip<string>(1).Take<string>(40).Select<string, ClickyInkDocument.Item>(delegate(string row)
      {
        List<string> list4 = ClickyInkParser.<ParseTable>g__Cells|12_0(row);
        ClickyInkDocument.Mark? mark = null;
        for (int i = 0; i < list4.Count; i++)
        {
          ValueTuple<string, Dictionary<string, string>> valueTuple = ClickyInkParser.TrailingAttributes(list4[i]);
          string item = valueTuple.Item1;
          ClickyInkDocument.Mark? mark2 = ClickyInkDocumentKinds.FirstMark(valueTuple.Item2.Keys);
          if (mark2 != null)
          {
            ClickyInkDocument.Mark valueOrDefault = mark2.GetValueOrDefault();
            ClickyInkDocument.Mark mark3 = mark.GetValueOrDefault();
            if (mark == null)
            {
              mark3 = valueOrDefault;
              mark = new ClickyInkDocument.Mark?(mark3);
            }
            list4[i] = item;
          }
          ValueTuple<string, ClickyInkDocument.Mark?> valueTuple2 = ClickyInkParser.ExtractingMarks(list4[i]);
          string item2 = valueTuple2.Item1;
          ClickyInkDocument.Mark? item3 = valueTuple2.Item2;
          if (item3 != null)
          {
            ClickyInkDocument.Mark valueOrDefault2 = item3.GetValueOrDefault();
            ClickyInkDocument.Mark mark3 = mark.GetValueOrDefault();
            if (mark == null)
            {
              mark3 = valueOrDefault2;
              mark = new ClickyInkDocument.Mark?(mark3);
            }
            list4[i] = item2;
          }
        }
        if (mark == null && list4.Count > 0 && (list4[0].HasPrefix("**") || list4[0].HasPrefix("★")))
        {
          mark = new ClickyInkDocument.Mark?(ClickyInkDocument.Mark.Pick);
        }
        if (list4.Count > 0 && list4[0].HasPrefix("~~"))
        {
          mark = new ClickyInkDocument.Mark?(ClickyInkDocument.Mark.Gone);
        }
        IEnumerable<string> enumerable2 = list4;
        Func<string, string> func2;
        if ((func2 = ClickyInkParser.<>O.<0>__Clean) == null)
        {
          func2 = (ClickyInkParser.<>O.<0>__Clean = new Func<string, string>(ClickyInkParser.Clean));
        }
        List<string> list5 = enumerable2.Select<string, string>(func2).ToList<string>();
        return new ClickyInkDocument.Item(string.Join(" · ", list5), null, list5, mark, null, null, null);
      })
        .ToList<ClickyInkDocument.Item>();
      ClickyInkDocument.Kind kind = ClickyInkDocument.Kind.Table;
      string text2 = "";
      Dictionary<string, string> dictionary = null;
      List<ClickyInkDocument.Item> list3 = list2;
      IEnumerable<string> enumerable = ClickyInkParser.<ParseTable>g__Cells|12_0(text);
      Func<string, string> func;
      if ((func = ClickyInkParser.<>O.<0>__Clean) == null)
      {
        func = (ClickyInkParser.<>O.<0>__Clean = new Func<string, string>(ClickyInkParser.Clean));
      }
      return new ClickyInkDocument.Block(kind, text2, dictionary, list3, enumerable.Select<string, string>(func).ToList<string>());
    }

    // Token: 0x060014A5 RID: 5285 RVA: 0x003AF4BC File Offset: 0x003AF4BC
    [GeneratedRegex("\\s*\\{\\s*(new|needs|done|pick|now|gone|out|ruled-out|ruledout|no|skip)\\s*\\}", RegexOptions.IgnoreCase)]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex MarkRegex()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MarkRegex_38.Instance;
    }

    // Token: 0x060014A6 RID: 5286 RVA: 0x003AF4C4 File Offset: 0x003AF4C4
    [return: TupleElementNames(new string[] { "Text", "Mark" })]
    [return: Nullable(new byte[] { 0, 1 })]
    public static ValueTuple<string, ClickyInkDocument.Mark?> ExtractingMarks(string text)
    {
      if (!text.Contains('{'))
      {
        return new ValueTuple<string, ClickyInkDocument.Mark?>(text, null);
      }
      Match match = ClickyInkParser.MarkRegex().Match(text);
      if (!match.Success)
      {
        return new ValueTuple<string, ClickyInkDocument.Mark?>(text, null);
      }
      ClickyInkDocument.Mark? mark = ClickyInkDocumentKinds.MarkFromWord(match.Groups[1].Value);
      return new ValueTuple<string, ClickyInkDocument.Mark?>(ClickyInkParser.MarkRegex().Replace(text, "").Replace("  ", " ")
        .TrimWs(), mark);
    }

    // Token: 0x060014A7 RID: 5287 RVA: 0x003AF550 File Offset: 0x003AF550
    public static string Clean(string text)
    {
      string text2 = ClickyInkParser.InlineLinks(text).Item1.TrimWs();
      text2 = ClickyInkParser.EmphasisMarks(text2);
      foreach (string text3 in new string[] { "**", "__", "~~", "★ ", "==", "`" })
      {
        text2 = text2.Replace(text3, "");
      }
      return text2;
    }

    // Token: 0x060014A8 RID: 5288 RVA: 0x003AF5CC File Offset: 0x003AF5CC
    [GeneratedRegex("\\*\\*(.+?)\\*\\*")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex BoldRegex()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BoldRegex_39.Instance;
    }

    // Token: 0x060014A9 RID: 5289 RVA: 0x003AF5D4 File Offset: 0x003AF5D4
    [GeneratedRegex("==(.+?)==")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex HighlightRegex()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HighlightRegex_40.Instance;
    }

    // Token: 0x060014AA RID: 5290 RVA: 0x003AF5DC File Offset: 0x003AF5DC
    public static string EmphasisMarks(string text)
    {
      if (!text.Contains("**", StringComparison.Ordinal) && !text.Contains("==", StringComparison.Ordinal))
      {
        return text;
      }
      Regex regex = ClickyInkParser.BoldRegex();
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
      defaultInterpolatedStringHandler.AppendFormatted<char>('\ue001');
      defaultInterpolatedStringHandler.AppendLiteral("$1");
      defaultInterpolatedStringHandler.AppendFormatted<char>('\ue002');
      string text2 = regex.Replace(text, defaultInterpolatedStringHandler.ToStringAndClear());
      Regex regex2 = ClickyInkParser.HighlightRegex();
      string text3 = text2;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
      defaultInterpolatedStringHandler.AppendFormatted<char>('\ue003');
      defaultInterpolatedStringHandler.AppendLiteral("$1");
      defaultInterpolatedStringHandler.AppendFormatted<char>('\ue004');
      return regex2.Replace(text3, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x060014AB RID: 5291 RVA: 0x003AF688 File Offset: 0x003AF688
    [GeneratedRegex("\\[([^\\]]+)\\]\\((https?://[^)\\s]+)\\)")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex LinkRegex()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__LinkRegex_41.Instance;
    }

    // Token: 0x060014AC RID: 5292 RVA: 0x003AF690 File Offset: 0x003AF690
    [GeneratedRegex("(\\[[^\\]]+\\]\\(https?://[^)\\s]+\\))[ \\t]*(?=\\[[^\\]]+\\]\\(https?://)")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex AdjacentLinksRegex()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AdjacentLinksRegex_42.Instance;
    }

    // Token: 0x060014AD RID: 5293 RVA: 0x003AF698 File Offset: 0x003AF698
    public static string MarkingFirstLink(string text)
    {
      Match match = ClickyInkParser.LinkRegex().Match(text);
      if (!match.Success)
      {
        return text;
      }
      string text2 = text.Substring(0, match.Index);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 4);
      defaultInterpolatedStringHandler.AppendLiteral("[");
      defaultInterpolatedStringHandler.AppendFormatted<char>('\ue005');
      defaultInterpolatedStringHandler.AppendFormatted(match.Groups[1].Value);
      defaultInterpolatedStringHandler.AppendFormatted<char>('\ue006');
      defaultInterpolatedStringHandler.AppendLiteral("](");
      defaultInterpolatedStringHandler.AppendFormatted(match.Groups[2].Value);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      string text3 = defaultInterpolatedStringHandler.ToStringAndClear();
      int num = match.Index + match.Length;
      return text2 + text3 + text.Substring(num, text.Length - num);
    }

    // Token: 0x060014AE RID: 5294 RVA: 0x003AF768 File Offset: 0x003AF768
    [return: TupleElementNames(new string[] { "Text", "Url" })]
    [return: Nullable(new byte[] { 0, 1, 2 })]
    public static ValueTuple<string, string> InlineLinks(string text)
    {
      if (!text.Contains("](", StringComparison.Ordinal))
      {
        return new ValueTuple<string, string>(text, null);
      }
      text = ClickyInkParser.AdjacentLinksRegex().Replace(text, "$1 · ");
      Match match = ClickyInkParser.LinkRegex().Match(text);
      string text2 = (match.Success ? match.Groups[2].Value : null);
      return new ValueTuple<string, string>(ClickyInkParser.LinkRegex().Replace(text, "$1"), text2);
    }

    // Token: 0x060014AF RID: 5295 RVA: 0x003AF7DC File Offset: 0x003AF7DC
    [return: TupleElementNames(new string[] { "Label", "Url" })]
    [return: Nullable(new byte[] { 2, 0, 1, 1 })]
    public static List<ValueTuple<string, string>> LinksOnly(string line)
    {
      string text = line.TrimWs();
      if (!text.HasPrefix("[") || !text.Contains("](", StringComparison.Ordinal))
      {
        return null;
      }
      MatchCollection matchCollection = ClickyInkParser.LinkRegex().Matches(text);
      if (matchCollection.Count == 0)
      {
        return null;
      }
      if (!ClickyInkParser.LinkRegex().Replace(text, "").All<char>((char c) => " \t·•,;|-–—".Contains(c)))
      {
        return null;
      }
      return matchCollection.Select<Match, ValueTuple<string, string>>((Match m) => new ValueTuple<string, string>(m.Groups[1].Value, m.Groups[2].Value)).ToList<ValueTuple<string, string>>();
    }

    // Token: 0x060014B0 RID: 5296 RVA: 0x003AF888 File Offset: 0x003AF888
    public static ClickyInkDocument.Item AmountLine(string line)
    {
      return ClickyInkParser.Builder.AmountLine(line);
    }

    // Token: 0x060014B1 RID: 5297 RVA: 0x003AF890 File Offset: 0x003AF890
    public static ClickyInkDocument.Item TimedLine(string line)
    {
      return ClickyInkParser.Builder.TimedLine(line);
    }

    // Token: 0x060014B2 RID: 5298 RVA: 0x003AF898 File Offset: 0x003AF898
    [GeneratedRegex("^((?:\\d{1,2}(?::\\d{2})?\\s*(?:am|pm|AM|PM)?)(?:\\s*[–-]\\s*\\d{1,2}(?::\\d{2})?\\s*(?:am|pm|AM|PM)?)?|(?:Morning|Afternoon|Evening|Night|Lunch|Dinner|Breakfast)(?=\\s*[:·—-]))\\s*[:·—-]?\\s+")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex TimedLineRegex()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TimedLineRegex_43.Instance;
    }

    // Token: 0x060014B4 RID: 5300 RVA: 0x003AFB04 File Offset: 0x003AFB04
    [CompilerGenerated]
    internal static void <TrailingAttributeGroup>g__SkipSeparators|9_0(ref ClickyInkParser.<>c__DisplayClass9_0 A_0)
    {
      for (;;)
      {
        bool flag = A_0.position < A_0.end;
        if (flag)
        {
          char c = A_0.inside[A_0.position];
          bool flag2 = c == ' ' || c == ',' || c == ';';
          flag = flag2;
        }
        if (!flag)
        {
          break;
        }
        A_0.position++;
      }
    }

    // Token: 0x060014B5 RID: 5301 RVA: 0x003AFB60 File Offset: 0x003AFB60
    [CompilerGenerated]
    internal static List<string> <ParseTable>g__Cells|12_0(string row)
    {
      string text = row.TrimWs();
      if (text.HasPrefix("|"))
      {
        string text2 = text;
        text = text2.Substring(1, text2.Length - 1);
      }
      if (text.HasSuffix("|"))
      {
        string text2 = text;
        text = text2.Substring(0, text2.Length - 1);
      }
      return (from c in text.Split('|', StringSplitOptions.None)
        select c.TrimWs()).ToList<string>();
    }

    // Token: 0x060014B6 RID: 5302 RVA: 0x003AFBE4 File Offset: 0x003AFBE4
    [CompilerGenerated]
    internal static bool <ParseTable>g__IsDivider|12_1(string row)
    {
      string text = row.Replace("|", "").TrimWs();
      if (text.Length > 0)
      {
        return text.All<char>((char c) => "-: ".Contains(c));
      }
      return false;
    }

    // Token: 0x04000908 RID: 2312
    public const int MaximumPages = 10;

    // Token: 0x04000909 RID: 2313
    public const int MaximumBlocksPerPage = 30;

    // Token: 0x0400090A RID: 2314
    public const int MaximumItems = 40;

    // Token: 0x0400090B RID: 2315
    public static readonly IReadOnlySet<string> KnownObjects = new HashSet<string>
    {
      "answer", "figure", "sticky", "ticket", "receipt", "photo", "place", "day", "letter", "clip",
      "quote", "file", "link", "source", "ask", "question", "chart", "note", "board", "drawing",
      "sketch", "diagram", "spark", "status", "progress", "tally", "streak", "countdown", "pipeline", "run",
      "quiet", "tiles", "review", "form", "connect", "items", "quiz", "todo", "gallery", "event",
      "criteria"
    };

    // Token: 0x0400090C RID: 2316
    public static readonly IReadOnlySet<string> ObjectHoldingContainers = new HashSet<string> { "tiles", "stats", "strip", "grid", "kpis", "metrics", "dashboard" };

    // Token: 0x020004D3 RID: 1235
    [Nullable(0)]
    public sealed class DirectiveHeader : IEquatable<ClickyInkParser.DirectiveHeader>
    {
      // Token: 0x06003057 RID: 12375 RVA: 0x004340E0 File Offset: 0x004340E0
      public DirectiveHeader(string Name, string Label, Dictionary<string, string> Attributes)
      {
        this.Name = Name;
        this.Label = Label;
        this.Attributes = Attributes;
        base..ctor();
      }

      // Token: 0x170008A8 RID: 2216
      // (get) Token: 0x06003058 RID: 12376 RVA: 0x00434100 File Offset: 0x00434100
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkParser.DirectiveHeader);
        }
      }

      // Token: 0x170008A9 RID: 2217
      // (get) Token: 0x06003059 RID: 12377 RVA: 0x0043410C File Offset: 0x0043410C
      // (set) Token: 0x0600305A RID: 12378 RVA: 0x00434114 File Offset: 0x00434114
      public string Name { get; set; }

      // Token: 0x170008AA RID: 2218
      // (get) Token: 0x0600305B RID: 12379 RVA: 0x00434120 File Offset: 0x00434120
      // (set) Token: 0x0600305C RID: 12380 RVA: 0x00434128 File Offset: 0x00434128
      public string Label { get; set; }

      // Token: 0x170008AB RID: 2219
      // (get) Token: 0x0600305D RID: 12381 RVA: 0x00434134 File Offset: 0x00434134
      // (set) Token: 0x0600305E RID: 12382 RVA: 0x0043413C File Offset: 0x0043413C
      public Dictionary<string, string> Attributes { get; set; }

      // Token: 0x0600305F RID: 12383 RVA: 0x00434148 File Offset: 0x00434148
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("DirectiveHeader");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003060 RID: 12384 RVA: 0x00434194 File Offset: 0x00434194
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Name = ");
        builder.Append(this.Name);
        builder.Append(", Label = ");
        builder.Append(this.Label);
        builder.Append(", Attributes = ");
        builder.Append(this.Attributes);
        return true;
      }

      // Token: 0x06003061 RID: 12385 RVA: 0x004341F4 File Offset: 0x004341F4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkParser.DirectiveHeader left, ClickyInkParser.DirectiveHeader right)
      {
        return !(left == right);
      }

      // Token: 0x06003062 RID: 12386 RVA: 0x00434200 File Offset: 0x00434200
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkParser.DirectiveHeader left, ClickyInkParser.DirectiveHeader right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003063 RID: 12387 RVA: 0x00434214 File Offset: 0x00434214
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField)) * -1521134295 + EqualityComparer<Dictionary<string, string>>.Default.GetHashCode(this.<Attributes>k__BackingField);
      }

      // Token: 0x06003064 RID: 12388 RVA: 0x00434278 File Offset: 0x00434278
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkParser.DirectiveHeader);
      }

      // Token: 0x06003065 RID: 12389 RVA: 0x00434288 File Offset: 0x00434288
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkParser.DirectiveHeader other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField) && EqualityComparer<Dictionary<string, string>>.Default.Equals(this.<Attributes>k__BackingField, other.<Attributes>k__BackingField));
      }

      // Token: 0x06003067 RID: 12391 RVA: 0x00434304 File Offset: 0x00434304
      [CompilerGenerated]
      private DirectiveHeader(ClickyInkParser.DirectiveHeader original)
      {
        this.Name = original.<Name>k__BackingField;
        this.Label = original.<Label>k__BackingField;
        this.Attributes = original.<Attributes>k__BackingField;
      }

      // Token: 0x06003068 RID: 12392 RVA: 0x00434330 File Offset: 0x00434330
      [CompilerGenerated]
      public void Deconstruct(out string Name, out string Label, out Dictionary<string, string> Attributes)
      {
        Name = this.Name;
        Label = this.Label;
        Attributes = this.Attributes;
      }
    }

    // Token: 0x020004D4 RID: 1236
    [Nullable(0)]
    public sealed class ListStart : IEquatable<ClickyInkParser.ListStart>
    {
      // Token: 0x06003069 RID: 12393 RVA: 0x0043434C File Offset: 0x0043434C
      public ListStart(ClickyInkDocument.Kind Kind, ClickyInkDocument.Item Item)
      {
        this.Kind = Kind;
        this.Item = Item;
        base..ctor();
      }

      // Token: 0x170008AC RID: 2220
      // (get) Token: 0x0600306A RID: 12394 RVA: 0x00434364 File Offset: 0x00434364
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkParser.ListStart);
        }
      }

      // Token: 0x170008AD RID: 2221
      // (get) Token: 0x0600306B RID: 12395 RVA: 0x00434370 File Offset: 0x00434370
      // (set) Token: 0x0600306C RID: 12396 RVA: 0x00434378 File Offset: 0x00434378
      public ClickyInkDocument.Kind Kind { get; set; }

      // Token: 0x170008AE RID: 2222
      // (get) Token: 0x0600306D RID: 12397 RVA: 0x00434384 File Offset: 0x00434384
      // (set) Token: 0x0600306E RID: 12398 RVA: 0x0043438C File Offset: 0x0043438C
      public ClickyInkDocument.Item Item { get; set; }

      // Token: 0x170008AF RID: 2223
      // (get) Token: 0x0600306F RID: 12399 RVA: 0x00434398 File Offset: 0x00434398
      public string Text
      {
        get
        {
          return this.Item.Text;
        }
      }

      // Token: 0x06003070 RID: 12400 RVA: 0x004343A8 File Offset: 0x004343A8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ListStart");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003071 RID: 12401 RVA: 0x004343F4 File Offset: 0x004343F4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Kind = ");
        builder.Append(this.Kind.ToString());
        builder.Append(", Item = ");
        builder.Append(this.Item);
        builder.Append(", Text = ");
        builder.Append(this.Text);
        return true;
      }

      // Token: 0x06003072 RID: 12402 RVA: 0x00434460 File Offset: 0x00434460
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkParser.ListStart left, ClickyInkParser.ListStart right)
      {
        return !(left == right);
      }

      // Token: 0x06003073 RID: 12403 RVA: 0x0043446C File Offset: 0x0043446C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkParser.ListStart left, ClickyInkParser.ListStart right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003074 RID: 12404 RVA: 0x00434480 File Offset: 0x00434480
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ClickyInkDocument.Kind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkDocument.Item>.Default.GetHashCode(this.<Item>k__BackingField);
      }

      // Token: 0x06003075 RID: 12405 RVA: 0x004344C0 File Offset: 0x004344C0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkParser.ListStart);
      }

      // Token: 0x06003076 RID: 12406 RVA: 0x004344D0 File Offset: 0x004344D0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkParser.ListStart other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ClickyInkDocument.Kind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<ClickyInkDocument.Item>.Default.Equals(this.<Item>k__BackingField, other.<Item>k__BackingField));
      }

      // Token: 0x06003078 RID: 12408 RVA: 0x00434534 File Offset: 0x00434534
      [CompilerGenerated]
      private ListStart(ClickyInkParser.ListStart original)
      {
        this.Kind = original.<Kind>k__BackingField;
        this.Item = original.<Item>k__BackingField;
      }

      // Token: 0x06003079 RID: 12409 RVA: 0x00434554 File Offset: 0x00434554
      [CompilerGenerated]
      public void Deconstruct(out ClickyInkDocument.Kind Kind, out ClickyInkDocument.Item Item)
      {
        Kind = this.Kind;
        Item = this.Item;
      }
    }

    // Token: 0x020004D5 RID: 1237
    [Nullable(0)]
    public static class Emphasis
    {
      // Token: 0x0600307A RID: 12410 RVA: 0x00434568 File Offset: 0x00434568
      public static bool IsMarker(char c)
      {
        return c >= '\ue001' && c <= '\ue006';
      }

      // Token: 0x0600307B RID: 12411 RVA: 0x00434580 File Offset: 0x00434580
      public static bool IsMarker(string character)
      {
        return character.Length == 1 && ClickyInkParser.Emphasis.IsMarker(character[0]);
      }

      // Token: 0x0600307C RID: 12412 RVA: 0x0043459C File Offset: 0x0043459C
      public static string Strip(string text)
      {
        bool flag = false;
        for (int i = 0; i < text.Length; i++)
        {
          if (ClickyInkParser.Emphasis.IsMarker(text[i]))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          return text;
        }
        StringBuilder stringBuilder = new StringBuilder(text.Length);
        foreach (char c in text)
        {
          if (!ClickyInkParser.Emphasis.IsMarker(c))
          {
            stringBuilder.Append(c);
          }
        }
        return stringBuilder.ToString();
      }

      // Token: 0x04001E39 RID: 7737
      public const char BoldOn = '\ue001';

      // Token: 0x04001E3A RID: 7738
      public const char BoldOff = '\ue002';

      // Token: 0x04001E3B RID: 7739
      public const char HighlightOn = '\ue003';

      // Token: 0x04001E3C RID: 7740
      public const char HighlightOff = '\ue004';

      // Token: 0x04001E3D RID: 7741
      public const char LinkOn = '\ue005';

      // Token: 0x04001E3E RID: 7742
      public const char LinkOff = '\ue006';

      // Token: 0x04001E3F RID: 7743
      public static readonly IReadOnlySet<char> All = new HashSet<char> { '\ue001', '\ue002', '\ue003', '\ue004', '\ue005', '\ue006' };
    }

    // Token: 0x020004D6 RID: 1238
    [Nullable(0)]
    private sealed class Builder
    {
      // Token: 0x170008B0 RID: 2224
      // (get) Token: 0x0600307E RID: 12414 RVA: 0x00434678 File Offset: 0x00434678
      [Nullable(new byte[] { 2, 1 })]
      public List<ClickyInkDocument.Block> CurrentBlocks
      {
        [return: Nullable(new byte[] { 2, 1 })]
        get
        {
          if (this.Document.Pages.Count <= 0)
          {
            return null;
          }
          List<ClickyInkDocument.Page> pages = this.Document.Pages;
          return pages[pages.Count - 1].Blocks;
        }
      }

      // Token: 0x0600307F RID: 12415 RVA: 0x004346AC File Offset: 0x004346AC
      public void StartPage(string name, bool isBoard)
      {
        if (this.Document.Pages.Count == 1)
        {
          List<ClickyInkDocument.Page> pages = this.Document.Pages;
          if (pages[pages.Count - 1].Blocks.Count == 0)
          {
            this.Document.Pages[0] = new ClickyInkDocument.Page(name, isBoard, new List<ClickyInkDocument.Block>());
            return;
          }
        }
        if (this.Document.Pages.Count >= 10)
        {
          return;
        }
        List<ClickyInkDocument.Page> pages2 = this.Document.Pages;
        string text;
        if (name.Length != 0)
        {
          text = name;
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
          defaultInterpolatedStringHandler.AppendLiteral("Page ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(this.Document.Pages.Count + 1);
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        pages2.Add(new ClickyInkDocument.Page(text, isBoard, new List<ClickyInkDocument.Block>()));
      }

      // Token: 0x06003080 RID: 12416 RVA: 0x00434780 File Offset: 0x00434780
      public void Add(ClickyInkDocument.Block block)
      {
        if (this.Document.Pages.Count == 0)
        {
          this.Document.Pages.Add(new ClickyInkDocument.Page("In short", false, new List<ClickyInkDocument.Block>()));
        }
        List<ClickyInkDocument.Page> pages = this.Document.Pages;
        if (pages[pages.Count - 1].Blocks.Count >= 30)
        {
          return;
        }
        List<ClickyInkDocument.Page> pages2 = this.Document.Pages;
        pages2[pages2.Count - 1].Blocks.Add(block);
      }

      // Token: 0x06003081 RID: 12417 RVA: 0x0043480C File Offset: 0x0043480C
      public void AppendParagraphLine(string line)
      {
        this.paragraph.Add(line);
      }

      // Token: 0x06003082 RID: 12418 RVA: 0x0043481C File Offset: 0x0043481C
      public void EndParagraph()
      {
        if (this.paragraph.Count == 0)
        {
          return;
        }
        List<string> list = this.paragraph.ToList<string>();
        this.paragraph.Clear();
        List<string> list2 = new List<string>();
        foreach (string text in list)
        {
          List<ValueTuple<string, string>> list3 = ClickyInkParser.LinksOnly(text);
          if (list3 != null)
          {
            this.AddProse(list2);
            list2 = new List<string>();
            using (List<ValueTuple<string, string>>.Enumerator enumerator2 = list3.GetEnumerator())
            {
              while (enumerator2.MoveNext())
              {
                ValueTuple<string, string> valueTuple = enumerator2.Current;
                ClickyInkDocument.Kind kind = ClickyInkDocument.Kind.Link;
                string text2 = ClickyInkParser.Clean(valueTuple.Item1);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary["url"] = valueTuple.Item2;
                this.Add(new ClickyInkDocument.Block(kind, text2, dictionary, null, null));
              }
              continue;
            }
          }
          list2.Add(text);
        }
        this.AddProse(list2);
      }

      // Token: 0x06003083 RID: 12419 RVA: 0x00434924 File Offset: 0x00434924
      private void AddProse(List<string> lines)
      {
        if (lines.Count == 0)
        {
          return;
        }
        bool flag;
        if (lines.Count > 1)
        {
          flag = lines.Take<string>(lines.Count - 1).All<string>((string l) => l.CharacterCount() >= 64);
        }
        else
        {
          flag = false;
        }
        ValueTuple<string, string> valueTuple = ClickyInkParser.InlineLinks(string.Join(flag ? " " : "\n", lines));
        string item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        ValueTuple<string, Dictionary<string, string>> valueTuple2 = ClickyInkParser.TrailingAttributes(item);
        string item3 = valueTuple2.Item1;
        Dictionary<string, string> item4 = valueTuple2.Item2;
        if (item2 != null && !item4.ContainsKey("url"))
        {
          item4["url"] = item2;
        }
        this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Paragraph, ClickyInkParser.Clean(item3), item4, null, null));
      }

      // Token: 0x06003084 RID: 12420 RVA: 0x004349E4 File Offset: 0x004349E4
      public void AddLeaf(ClickyInkParser.DirectiveHeader directive)
      {
        string name = directive.Name;
        ClickyInkDocument.Kind kind;
        if (name != null)
        {
          switch (name.Length)
          {
          case 3:
          {
            char c = name[0];
            if (c <= 'k')
            {
              switch (c)
              {
              case 'a':
                if (!(name == "ask"))
                {
                  goto IL_08D6;
                }
                goto IL_088B;
              case 'b':
                if (!(name == "bar"))
                {
                  goto IL_08D6;
                }
                goto IL_08A9;
              case 'c':
                goto IL_08D6;
              case 'd':
                if (!(name == "due"))
                {
                  goto IL_08D6;
                }
                goto IL_08B8;
              default:
                if (c != 'k')
                {
                  goto IL_08D6;
                }
                if (!(name == "kpi"))
                {
                  goto IL_08D6;
                }
                goto IL_089A;
              }
            }
            else if (c != 'm')
            {
              if (c != 'p')
              {
                if (c != 'r')
                {
                  goto IL_08D6;
                }
                if (!(name == "run"))
                {
                  goto IL_08D6;
                }
                goto IL_08C2;
              }
              else
              {
                if (!(name == "pin"))
                {
                  goto IL_08D6;
                }
                goto IL_0877;
              }
            }
            else
            {
              if (!(name == "map"))
              {
                goto IL_08D6;
              }
              goto IL_0877;
            }
            break;
          }
          case 4:
          {
            char c = name[0];
            switch (c)
            {
            case 'c':
              if (!(name == "clip"))
              {
                goto IL_08D6;
              }
              goto IL_087C;
            case 'd':
            case 'e':
              goto IL_08D6;
            case 'f':
              if (!(name == "file"))
              {
                goto IL_08D6;
              }
              goto IL_0881;
            case 'g':
              if (!(name == "goal"))
              {
                goto IL_08D6;
              }
              goto IL_08A9;
            default:
              switch (c)
              {
              case 'l':
                if (!(name == "link"))
                {
                  goto IL_08D6;
                }
                goto IL_0886;
              case 'm':
              case 'o':
                goto IL_08D6;
              case 'n':
                if (!(name == "note"))
                {
                  goto IL_08D6;
                }
                goto IL_0895;
              case 'p':
                if (!(name == "pass"))
                {
                  goto IL_08D6;
                }
                goto IL_086D;
              default:
                if (c != 's')
                {
                  goto IL_08D6;
                }
                if (!(name == "stat"))
                {
                  goto IL_08D6;
                }
                break;
              }
              break;
            }
            break;
          }
          case 5:
            switch (name[0])
            {
            case 'a':
              if (!(name == "aside"))
              {
                goto IL_08D6;
              }
              goto IL_0895;
            case 'b':
            case 'f':
            case 'g':
            case 'j':
            case 'k':
            case 'n':
            case 'o':
            case 'r':
              goto IL_08D6;
            case 'c':
              if (!(name == "count"))
              {
                goto IL_08D6;
              }
              goto IL_08AE;
            case 'd':
              if (!(name == "delta"))
              {
                goto IL_08D6;
              }
              goto IL_089A;
            case 'e':
              if (!(name == "event"))
              {
                goto IL_08D6;
              }
              goto IL_08D1;
            case 'h':
              if (!(name == "habit"))
              {
                goto IL_08D6;
              }
              goto IL_08B3;
            case 'i':
              if (!(name == "image"))
              {
                goto IL_08D6;
              }
              goto IL_0872;
            case 'l':
              if (!(name == "light"))
              {
                goto IL_08D6;
              }
              goto IL_08A4;
            case 'm':
              if (!(name == "meter"))
              {
                goto IL_08D6;
              }
              goto IL_08A9;
            case 'p':
              if (name == "photo")
              {
                goto IL_0872;
              }
              if (!(name == "place"))
              {
                goto IL_08D6;
              }
              goto IL_0877;
            case 'q':
              if (name == "quote")
              {
                goto IL_087C;
              }
              if (!(name == "quiet"))
              {
                goto IL_08D6;
              }
              goto IL_08C7;
            case 's':
              if (!(name == "spark"))
              {
                goto IL_08D6;
              }
              goto IL_089F;
            case 't':
              if (name == "trend")
              {
                goto IL_089F;
              }
              if (!(name == "tally"))
              {
                goto IL_08D6;
              }
              goto IL_08AE;
            case 'u':
              if (!(name == "until"))
              {
                goto IL_08D6;
              }
              goto IL_08B8;
            default:
              goto IL_08D6;
            }
            break;
          case 6:
            switch (name[3])
            {
            case 'b':
              if (!(name == "number"))
              {
                goto IL_08D6;
              }
              goto IL_0860;
            case 'c':
              if (!(name == "sticky"))
              {
                goto IL_08D6;
              }
              break;
            case 'd':
            case 'f':
            case 'h':
            case 'j':
            case 'm':
            case 'o':
            case 'p':
            case 'q':
            case 's':
            case 'v':
              goto IL_08D6;
            case 'e':
              if (!(name == "streak"))
              {
                goto IL_08D6;
              }
              goto IL_08B3;
            case 'g':
              if (name == "flight")
              {
                goto IL_086D;
              }
              if (name == "margin")
              {
                goto IL_0895;
              }
              if (!(name == "stages"))
              {
                goto IL_08D6;
              }
              goto IL_08BD;
            case 'i':
              if (name == "action")
              {
                goto IL_088B;
              }
              if (!(name == "habits"))
              {
                goto IL_08D6;
              }
              goto IL_08B3;
            case 'k':
              if (!(name == "ticket"))
              {
                goto IL_08D6;
              }
              goto IL_086D;
            case 'l':
              if (!(name == "health"))
              {
                goto IL_08D6;
              }
              goto IL_08A4;
            case 'n':
              if (name == "change")
              {
                goto IL_089A;
              }
              if (name == "signal")
              {
                goto IL_08A4;
              }
              if (!(name == "funnel"))
              {
                goto IL_08D6;
              }
              goto IL_08BD;
            case 'r':
              if (name == "source")
              {
                goto IL_0886;
              }
              if (!(name == "metric"))
              {
                goto IL_08D6;
              }
              goto IL_089A;
            case 't':
              if (!(name == "postit"))
              {
                if (name == "button")
                {
                  goto IL_088B;
                }
                if (!(name == "status"))
                {
                  goto IL_08D6;
                }
                goto IL_08A4;
              }
              break;
            case 'u':
              if (!(name == "figure"))
              {
                goto IL_08D6;
              }
              goto IL_0860;
            case 'w':
              if (!(name == "answer"))
              {
                goto IL_08D6;
              }
              kind = ClickyInkDocument.Kind.Answer;
              goto IL_08E9;
            default:
              goto IL_08D6;
            }
            kind = ClickyInkDocument.Kind.Sticky;
            goto IL_08E9;
          case 7:
          {
            char c = name[0];
            if (c != 'c')
            {
              if (c != 'e')
              {
                switch (c)
                {
                case 'm':
                  if (!(name == "meeting"))
                  {
                    goto IL_08D6;
                  }
                  goto IL_08D1;
                case 'n':
                  if (!(name == "nothing"))
                  {
                    goto IL_08D6;
                  }
                  goto IL_08C7;
                case 'o':
                case 'q':
                  goto IL_08D6;
                case 'p':
                  if (!(name == "preview"))
                  {
                    goto IL_08D6;
                  }
                  goto IL_0881;
                case 'r':
                  if (!(name == "runline"))
                  {
                    goto IL_08D6;
                  }
                  goto IL_08C2;
                default:
                  goto IL_08D6;
                }
              }
              else
              {
                if (!(name == "edition"))
                {
                  goto IL_08D6;
                }
                goto IL_08C2;
              }
            }
            else
            {
              if (name == "callout")
              {
                goto IL_0890;
              }
              if (name == "counter")
              {
                goto IL_08AE;
              }
              if (!(name == "connect"))
              {
                goto IL_08D6;
              }
              goto IL_08CC;
            }
            break;
          }
          case 8:
          {
            char c = name[3];
            switch (c)
            {
            case 'a':
              if (!(name == "polaroid"))
              {
                goto IL_08D6;
              }
              goto IL_0872;
            case 'b':
            case 'c':
            case 'f':
              goto IL_08D6;
            case 'd':
              if (!(name == "deadline"))
              {
                goto IL_08D6;
              }
              goto IL_08B8;
            case 'e':
              if (name == "pipeline")
              {
                goto IL_08BD;
              }
              if (!(name == "calendar"))
              {
                goto IL_08D6;
              }
              goto IL_08D1;
            case 'g':
              if (!(name == "progress"))
              {
                goto IL_08D6;
              }
              goto IL_08A9;
            case 'h':
              if (!(name == "nochange"))
              {
                goto IL_08D6;
              }
              goto IL_08C7;
            default:
              switch (c)
              {
              case 'p':
                if (!(name == "clipping"))
                {
                  goto IL_08D6;
                }
                goto IL_087C;
              case 'q':
                if (!(name == "allquiet"))
                {
                  goto IL_08D6;
                }
                goto IL_08C7;
              case 'r':
                goto IL_08D6;
              case 's':
                if (!(name == "question"))
                {
                  goto IL_08D6;
                }
                goto IL_0890;
              default:
                goto IL_08D6;
              }
              break;
            }
            break;
          }
          case 9:
          {
            char c = name[0];
            if (c != 'c')
            {
              if (c != 's')
              {
                if (c != 'u')
                {
                  goto IL_08D6;
                }
                if (!(name == "unchanged"))
                {
                  goto IL_08D6;
                }
                goto IL_08C7;
              }
              else
              {
                if (!(name == "sparkline"))
                {
                  goto IL_08D6;
                }
                goto IL_089F;
              }
            }
            else
            {
              if (!(name == "countdown"))
              {
                goto IL_08D6;
              }
              goto IL_08B8;
            }
            break;
          }
          case 10:
            if (!(name == "connectapp"))
            {
              goto IL_08D6;
            }
            goto IL_08CC;
          case 11:
          {
            char c = name[0];
            if (c != 'a')
            {
              if (c != 'i')
              {
                goto IL_08D6;
              }
              if (!(name == "integration"))
              {
                goto IL_08D6;
              }
              goto IL_08CC;
            }
            else
            {
              if (!(name == "appointment"))
              {
                goto IL_08D6;
              }
              goto IL_08D1;
            }
            break;
          }
          default:
            goto IL_08D6;
          }
          IL_0860:
          kind = ClickyInkDocument.Kind.Figure;
          goto IL_08E9;
          IL_086D:
          kind = ClickyInkDocument.Kind.Ticket;
          goto IL_08E9;
          IL_0872:
          kind = ClickyInkDocument.Kind.Photo;
          goto IL_08E9;
          IL_0877:
          kind = ClickyInkDocument.Kind.Place;
          goto IL_08E9;
          IL_087C:
          kind = ClickyInkDocument.Kind.Clip;
          goto IL_08E9;
          IL_0881:
          kind = ClickyInkDocument.Kind.File;
          goto IL_08E9;
          IL_0886:
          kind = ClickyInkDocument.Kind.Link;
          goto IL_08E9;
          IL_088B:
          kind = ClickyInkDocument.Kind.Ask;
          goto IL_08E9;
          IL_0890:
          kind = ClickyInkDocument.Kind.Question;
          goto IL_08E9;
          IL_0895:
          kind = ClickyInkDocument.Kind.Note;
          goto IL_08E9;
          IL_089A:
          kind = ClickyInkDocument.Kind.Figure;
          goto IL_08E9;
          IL_089F:
          kind = ClickyInkDocument.Kind.Spark;
          goto IL_08E9;
          IL_08A4:
          kind = ClickyInkDocument.Kind.Status;
          goto IL_08E9;
          IL_08A9:
          kind = ClickyInkDocument.Kind.Progress;
          goto IL_08E9;
          IL_08AE:
          kind = ClickyInkDocument.Kind.Tally;
          goto IL_08E9;
          IL_08B3:
          kind = ClickyInkDocument.Kind.Streak;
          goto IL_08E9;
          IL_08B8:
          kind = ClickyInkDocument.Kind.Countdown;
          goto IL_08E9;
          IL_08BD:
          kind = ClickyInkDocument.Kind.Pipeline;
          goto IL_08E9;
          IL_08C2:
          kind = ClickyInkDocument.Kind.Run;
          goto IL_08E9;
          IL_08C7:
          kind = ClickyInkDocument.Kind.Quiet;
          goto IL_08E9;
          IL_08CC:
          kind = ClickyInkDocument.Kind.Connect;
          goto IL_08E9;
          IL_08D1:
          kind = ClickyInkDocument.Kind.Event;
          goto IL_08E9;
        }
        IL_08D6:
        kind = ((directive.Name.Length == 0) ? ClickyInkDocument.Kind.Paragraph : ClickyInkDocument.Kind.Unknown);
        IL_08E9:
        ClickyInkDocument.Kind kind2 = kind;
        if (kind2 == ClickyInkDocument.Kind.Unknown)
        {
          Dictionary<string, string> dictionary = new Dictionary<string, string>(directive.Attributes);
          dictionary.TryAdd("was", directive.Name);
          this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Paragraph, directive.Label, dictionary, null, null));
          return;
        }
        this.Add(new ClickyInkDocument.Block(kind2, directive.Label, new Dictionary<string, string>(directive.Attributes), null, null));
      }

      // Token: 0x06003085 RID: 12421 RVA: 0x00435338 File Offset: 0x00435338
      public void AddContainer(ClickyInkParser.DirectiveHeader directive, List<string> body)
      {
        List<string> list = (from l in body
          select l.TrimWs() into l
          where l.Length > 0
          select l).ToList<string>();
        Dictionary<string, string> dictionary = new Dictionary<string, string>(directive.Attributes);
        string name = directive.Name;
        if (name != null)
        {
          switch (name.Length)
          {
          case 3:
            if (!(name == "day"))
            {
              goto IL_08A4;
            }
            goto IL_072A;
          case 4:
          {
            char c = name[0];
            if (c != 'b')
            {
              if (c != 'd')
              {
                if (c != 'n')
                {
                  goto IL_08A4;
                }
                if (!(name == "note"))
                {
                  goto IL_08A4;
                }
                goto IL_081E;
              }
              else
              {
                if (!(name == "draw"))
                {
                  goto IL_08A4;
                }
                goto IL_07AE;
              }
            }
            else if (!(name == "bill"))
            {
              goto IL_08A4;
            }
            break;
          }
          case 5:
          {
            switch (name[0])
            {
            case 'c':
              if (!(name == "chart"))
              {
                goto IL_08A4;
              }
              break;
            case 'd':
              if (!(name == "draft"))
              {
                goto IL_08A4;
              }
              goto IL_076D;
            case 'e':
              if (!(name == "email"))
              {
                goto IL_08A4;
              }
              goto IL_076D;
            case 'f':
              goto IL_08A4;
            case 'g':
              if (!(name == "graph"))
              {
                goto IL_08A4;
              }
              break;
            default:
              goto IL_08A4;
            }
            ClickyInkDocument.Kind kind = ClickyInkDocument.Kind.Chart;
            string label = directive.Label;
            Dictionary<string, string> dictionary2 = dictionary;
            IEnumerable<string> enumerable = list.Take<string>(16);
            Func<string, ClickyInkDocument.Item> func;
            if ((func = ClickyInkParser.Builder.<>O.<0>__AmountLine) == null)
            {
              func = (ClickyInkParser.Builder.<>O.<0>__AmountLine = new Func<string, ClickyInkDocument.Item>(ClickyInkParser.Builder.AmountLine));
            }
            this.Add(new ClickyInkDocument.Block(kind, label, dictionary2, enumerable.Select<string, ClickyInkDocument.Item>(func).ToList<ClickyInkDocument.Item>(), null));
            return;
          }
          case 6:
          {
            char c = name[1];
            if (c <= 'g')
            {
              if (c != 'e')
              {
                if (c != 'g')
                {
                  goto IL_08A4;
                }
                if (!(name == "agenda"))
                {
                  goto IL_08A4;
                }
                goto IL_072A;
              }
              else
              {
                if (!(name == "letter"))
                {
                  goto IL_08A4;
                }
                goto IL_076D;
              }
            }
            else if (c != 'k')
            {
              if (c != 't')
              {
                if (c != 'u')
                {
                  goto IL_08A4;
                }
                if (!(name == "budget"))
                {
                  goto IL_08A4;
                }
              }
              else
              {
                if (!(name == "sticky"))
                {
                  goto IL_08A4;
                }
                goto IL_081E;
              }
            }
            else
            {
              if (!(name == "sketch"))
              {
                goto IL_08A4;
              }
              goto IL_07AE;
            }
            break;
          }
          case 7:
          {
            char c = name[3];
            if (c <= 'g')
            {
              if (c != 'e')
              {
                if (c != 'g')
                {
                  goto IL_08A4;
                }
                if (!(name == "diagram"))
                {
                  goto IL_08A4;
                }
                goto IL_07AE;
              }
              else if (!(name == "receipt"))
              {
                goto IL_08A4;
              }
            }
            else if (c != 'l')
            {
              if (c != 's')
              {
                if (c != 'w')
                {
                  goto IL_08A4;
                }
                if (!(name == "drawing"))
                {
                  goto IL_08A4;
                }
                goto IL_07AE;
              }
              else
              {
                if (!(name == "message"))
                {
                  goto IL_08A4;
                }
                goto IL_076D;
              }
            }
            else
            {
              if (!(name == "callout"))
              {
                goto IL_08A4;
              }
              goto IL_0850;
            }
            break;
          }
          case 8:
          {
            char c = name[0];
            if (c != 'q')
            {
              if (c != 's')
              {
                goto IL_08A4;
              }
              if (!(name == "schedule"))
              {
                goto IL_08A4;
              }
              goto IL_072A;
            }
            else
            {
              if (!(name == "question"))
              {
                goto IL_08A4;
              }
              goto IL_0850;
            }
            break;
          }
          default:
            goto IL_08A4;
          }
          ClickyInkDocument.Kind kind2 = ClickyInkDocument.Kind.Receipt;
          string label2 = directive.Label;
          Dictionary<string, string> dictionary3 = dictionary;
          IEnumerable<string> enumerable2 = list.Take<string>(40);
          Func<string, ClickyInkDocument.Item> func2;
          if ((func2 = ClickyInkParser.Builder.<>O.<0>__AmountLine) == null)
          {
            func2 = (ClickyInkParser.Builder.<>O.<0>__AmountLine = new Func<string, ClickyInkDocument.Item>(ClickyInkParser.Builder.AmountLine));
          }
          this.Add(new ClickyInkDocument.Block(kind2, label2, dictionary3, enumerable2.Select<string, ClickyInkDocument.Item>(func2).ToList<ClickyInkDocument.Item>(), null));
          return;
          IL_072A:
          ClickyInkDocument.Kind kind3 = ClickyInkDocument.Kind.Day;
          string label3 = directive.Label;
          Dictionary<string, string> dictionary4 = dictionary;
          IEnumerable<string> enumerable3 = list.Take<string>(40);
          Func<string, ClickyInkDocument.Item> func3;
          if ((func3 = ClickyInkParser.Builder.<>O.<1>__TimedLine) == null)
          {
            func3 = (ClickyInkParser.Builder.<>O.<1>__TimedLine = new Func<string, ClickyInkDocument.Item>(ClickyInkParser.Builder.TimedLine));
          }
          this.Add(new ClickyInkDocument.Block(kind3, label3, dictionary4, enumerable3.Select<string, ClickyInkDocument.Item>(func3).ToList<ClickyInkDocument.Item>(), null));
          return;
          IL_076D:
          if (directive.Label.Length > 0)
          {
            dictionary.TryAdd("subject", directive.Label);
          }
          this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Letter, string.Join("\n", body).TrimWsNl(), dictionary, null, null));
          return;
          IL_07AE:
          dictionary["sketch"] = string.Join("\n", list);
          this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Drawing, directive.Label, dictionary, null, null));
          return;
          IL_081E:
          this.Add(new ClickyInkDocument.Block((directive.Name == "sticky") ? ClickyInkDocument.Kind.Sticky : ClickyInkDocument.Kind.Note, string.Join("\n", list), dictionary, null, null));
          return;
          IL_0850:
          this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Question, string.Join(" ", from s in new string[] { directive.Label }.Concat<string>(list)
            where s.Length > 0
            select s), dictionary, null, null));
          return;
        }
        IL_08A4:
        if (!ClickyInkParser.ObjectHoldingContainers.Contains(name))
        {
          if (name != null)
          {
            switch (name.Length)
            {
            case 4:
            {
              char c = name[0];
              if (c != 'f')
              {
                switch (c)
                {
                case 'q':
                  if (!(name == "quiz"))
                  {
                    goto IL_0FC1;
                  }
                  goto IL_0B07;
                case 'r':
                  if (!(name == "rows"))
                  {
                    goto IL_0FC1;
                  }
                  goto IL_0ACD;
                case 's':
                  goto IL_0FC1;
                case 't':
                  if (!(name == "todo"))
                  {
                    goto IL_0FC1;
                  }
                  goto IL_0AEA;
                default:
                  goto IL_0FC1;
                }
              }
              else if (!(name == "form"))
              {
                goto IL_0FC1;
              }
              break;
            }
            case 5:
            {
              char c = name[0];
              if (c <= 'i')
              {
                if (c != 'c')
                {
                  if (c != 'd')
                  {
                    if (c != 'i')
                    {
                      goto IL_0FC1;
                    }
                    if (!(name == "items"))
                    {
                      goto IL_0FC1;
                    }
                    goto IL_0ACD;
                  }
                  else
                  {
                    if (!(name == "drill"))
                    {
                      goto IL_0FC1;
                    }
                    goto IL_0B07;
                  }
                }
                else
                {
                  if (!(name == "cards"))
                  {
                    goto IL_0FC1;
                  }
                  goto IL_0B07;
                }
              }
              else if (c != 'l')
              {
                if (c != 's')
                {
                  if (c != 't')
                  {
                    goto IL_0FC1;
                  }
                  if (!(name == "trend"))
                  {
                    goto IL_0FC1;
                  }
                  goto IL_0EA8;
                }
                else
                {
                  if (!(name == "spark"))
                  {
                    goto IL_0FC1;
                  }
                  goto IL_0EA8;
                }
              }
              else
              {
                if (!(name == "leads"))
                {
                  goto IL_0FC1;
                }
                goto IL_0ACD;
              }
              break;
            }
            case 6:
            {
              char c = name[1];
              if (c <= 'i')
              {
                if (c != 'e')
                {
                  if (c != 'i')
                  {
                    goto IL_0FC1;
                  }
                  if (!(name == "fields"))
                  {
                    goto IL_0FC1;
                  }
                }
                else
                {
                  if (!(name == "review"))
                  {
                    goto IL_0FC1;
                  }
                  goto IL_097D;
                }
              }
              else if (c != 'n')
              {
                if (c != 't')
                {
                  if (c != 'u')
                  {
                    goto IL_0FC1;
                  }
                  if (!(name == "funnel"))
                  {
                    goto IL_0FC1;
                  }
                  goto IL_0DFF;
                }
                else
                {
                  if (!(name == "stages"))
                  {
                    goto IL_0FC1;
                  }
                  goto IL_0DFF;
                }
              }
              else if (!(name == "inputs"))
              {
                goto IL_0FC1;
              }
              break;
            }
            case 7:
            {
              char c = name[0];
              switch (c)
              {
              case 'a':
                if (!(name == "approve"))
                {
                  goto IL_0FC1;
                }
                goto IL_097D;
              case 'b':
              case 'd':
              case 'e':
                goto IL_0FC1;
              case 'c':
                if (!(name == "confirm"))
                {
                  goto IL_0FC1;
                }
                goto IL_097D;
              case 'f':
                if (!(name == "filters"))
                {
                  goto IL_0FC1;
                }
                goto IL_0DB4;
              case 'g':
                if (!(name == "gallery"))
                {
                  goto IL_0FC1;
                }
                goto IL_0D69;
              default:
                if (c != 't')
                {
                  goto IL_0FC1;
                }
                if (!(name == "tracker"))
                {
                  goto IL_0FC1;
                }
                goto IL_0AEA;
              }
              break;
            }
            case 8:
            {
              char c = name[3];
              if (c != 'e')
              {
                if (c != 'i')
                {
                  switch (c)
                  {
                  case 'p':
                    if (!(name == "proposed"))
                    {
                      goto IL_0FC1;
                    }
                    goto IL_097D;
                  case 'q':
                  case 's':
                    goto IL_0FC1;
                  case 'r':
                    if (!(name == "approval"))
                    {
                      goto IL_0FC1;
                    }
                    goto IL_097D;
                  case 't':
                    if (!(name == "criteria"))
                    {
                      goto IL_0FC1;
                    }
                    goto IL_0DB4;
                  default:
                    goto IL_0FC1;
                  }
                }
                else
                {
                  if (!(name == "variants"))
                  {
                    goto IL_0FC1;
                  }
                  goto IL_0D69;
                }
              }
              else
              {
                if (!(name == "pipeline"))
                {
                  goto IL_0FC1;
                }
                goto IL_0DFF;
              }
              break;
            }
            case 9:
              if (!(name == "sparkline"))
              {
                goto IL_0FC1;
              }
              goto IL_0EA8;
            case 10:
              if (!(name == "flashcards"))
              {
                goto IL_0FC1;
              }
              goto IL_0B07;
            default:
              goto IL_0FC1;
            }
            List<ClickyInkDocument.Item> list2 = list.Take<string>(40).Select<string, ClickyInkDocument.Item>(delegate(string line)
            {
              ClickyInkParser.ListStart listStart2 = ClickyInkParser.ListItemStart(line);
              string text9 = ((listStart2 != null) ? listStart2.Text : null) ?? line;
              int num4 = text9.IndexOf(':');
              if (num4 >= 0)
              {
                string text10 = text9.Substring(0, num4).TrimWs();
                string text11 = text9;
                int num5 = num4 + 1;
                string text12 = text11.Substring(num5, text11.Length - num5).TrimWs();
                return new ClickyInkDocument.Item(ClickyInkParser.Clean(text10), (text12.Length == 0) ? null : text12, null, null, null, null, null);
              }
              return new ClickyInkDocument.Item(ClickyInkParser.Clean(text9), null, null, null, null, null, null);
            }).ToList<ClickyInkDocument.Item>();
            this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Form, directive.Label, dictionary, list2, null));
            return;
            IL_0ACD:
            this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Items, directive.Label, dictionary, ClickyInkParser.Builder.ListItems(body, false), null));
            return;
            IL_0AEA:
            this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Todo, directive.Label, dictionary, ClickyInkParser.Builder.ListItems(body, true), null));
            return;
            IL_0B07:
            List<ClickyInkDocument.Item> list3 = new List<ClickyInkDocument.Item>();
            foreach (string text in list)
            {
              ClickyInkParser.ListStart listStart = ClickyInkParser.ListItemStart(text);
              string text2 = ((listStart != null) ? listStart.Text : null) ?? text;
              string text3 = text2.Lowered();
              if (text3.HasPrefix("a:") || text3.HasPrefix("answer:"))
              {
                if (list3.Count != 0)
                {
                  string text4 = text2;
                  int num = text2.IndexOf(':') + 1;
                  string text5 = text4.Substring(num, text4.Length - num).TrimWs();
                  List<ClickyInkDocument.Item> list4 = list3;
                  ClickyInkDocument.Item item5 = list4[list4.Count - 1];
                  string text6 = " ";
                  string[] array = new string[2];
                  int num2 = 0;
                  List<ClickyInkDocument.Item> list5 = list3;
                  array[num2] = list5[list5.Count - 1].Detail;
                  array[1] = ClickyInkParser.Clean(text5);
                  item5.Detail = string.Join(text6, array.Where<string>((string s) => s != null));
                }
              }
              else if (text3.HasPrefix("q:") || text3.HasPrefix("question:"))
              {
                string text4 = text2;
                int num = text2.IndexOf(':') + 1;
                string text7 = text4.Substring(num, text4.Length - num).TrimWs();
                list3.Add(new ClickyInkDocument.Item(ClickyInkParser.Clean(text7), null, null, null, null, null, null));
              }
              else
              {
                int num3 = text2.Find(" | ");
                if (num3 >= 0)
                {
                  List<ClickyInkDocument.Item> list6 = list3;
                  string text8 = ClickyInkParser.Clean(text2.Substring(0, num3));
                  string text4 = text2;
                  int num = num3 + 3;
                  list6.Add(new ClickyInkDocument.Item(text8, ClickyInkParser.Clean(text4.Substring(num, text4.Length - num)), null, null, null, null, null));
                }
                else
                {
                  list3.Add(new ClickyInkDocument.Item(ClickyInkParser.Clean(text2), null, null, null, null, null, null));
                }
              }
            }
            this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Quiz, directive.Label, dictionary, list3.Take<ClickyInkDocument.Item>(40).ToList<ClickyInkDocument.Item>(), null));
            return;
            IL_0D69:
            List<ClickyInkDocument.Item> list7 = list.Take<string>(16).Select<string, ClickyInkDocument.Item>(delegate(string line)
            {
              ClickyInkParser.ListStart listStart3 = ClickyInkParser.ListItemStart(line);
              string text13 = ((listStart3 != null) ? listStart3.Text : null) ?? line;
              ValueTuple<string, Dictionary<string, string>> valueTuple = ClickyInkParser.TrailingAttributes(text13);
              string item2 = valueTuple.Item1;
              Dictionary<string, string> item3 = valueTuple.Item2;
              text13 = item2;
              string text15;
              string text16;
              string text14 = (item3.TryGetValue("path", out text15) ? text15 : (item3.TryGetValue("url", out text16) ? text16 : null));
              string text17 = text13;
              string text19;
              if (text14 == null)
              {
                int num6 = -1;
                int num7 = 0;
                foreach (string text18 in new string[] { " — ", " – ", " - " })
                {
                  int num8 = text13.Find(text18);
                  if (num8 >= 0)
                  {
                    num6 = num8;
                    num7 = text18.Length;
                    break;
                  }
                }
                if (num6 >= 0)
                {
                  text14 = text13.Substring(0, num6).TrimWs();
                  text19 = text13;
                  int j = num6 + num7;
                  text17 = text19.Substring(j, text19.Length - j).TrimWs();
                }
                else
                {
                  text14 = text13.TrimWs();
                  text17 = "";
                }
              }
              ClickyInkDocument.Mark? mark = ClickyInkDocumentKinds.FirstMark(item3.Keys);
              string text20 = ClickyInkParser.Clean(text17);
              string text21 = null;
              List<string> list12 = null;
              ClickyInkDocument.Mark? mark2 = mark;
              text19 = text14;
              return new ClickyInkDocument.Item(text20, text21, list12, mark2, null, text19, null);
            }).ToList<ClickyInkDocument.Item>();
            this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Gallery, directive.Label, dictionary, list7, null));
            return;
            IL_0DB4:
            List<ClickyInkDocument.Item> list8 = list.Take<string>(16).Select<string, ClickyInkDocument.Item>(delegate(string line)
            {
              ClickyInkParser.ListStart listStart4 = ClickyInkParser.ListItemStart(line);
              string text22 = ((listStart4 != null) ? listStart4.Text : null) ?? line;
              int num9 = text22.IndexOf(':');
              if (num9 >= 0)
              {
                string text23 = ClickyInkParser.Clean(text22.Substring(0, num9));
                string text24 = text22;
                int num10 = num9 + 1;
                return new ClickyInkDocument.Item(text23, text24.Substring(num10, text24.Length - num10).TrimWs(), null, null, null, null, null);
              }
              return new ClickyInkDocument.Item(ClickyInkParser.Clean(text22), null, null, null, null, null, null);
            }).ToList<ClickyInkDocument.Item>();
            this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Criteria, directive.Label, dictionary, list8, null));
            return;
            IL_0DFF:
            if (!dictionary.ContainsKey("steps"))
            {
              dictionary["steps"] = ClickyInkPipeline.Attribute((from s in list.Select<string, ClickyInkPipeline.Stage?>(delegate(string line)
                {
                  ClickyInkDocument.Item item4 = ClickyInkParser.Builder.AmountLine(line);
                  if (item4.Cells.Count <= 1)
                  {
                    return ClickyInkPipeline.StageOf(item4.Text);
                  }
                  return new ClickyInkPipeline.Stage?(new ClickyInkPipeline.Stage(item4.Cells[0], item4.Cells[1]));
                })
                where s != null
                select s.Value).ToList<ClickyInkPipeline.Stage>());
            }
            this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Pipeline, directive.Label, dictionary, null, null));
            return;
            IL_0EA8:
            IEnumerable<string> enumerable4 = list;
            Func<string, ClickyInkDocument.Item> func4;
            if ((func4 = ClickyInkParser.Builder.<>O.<0>__AmountLine) == null)
            {
              func4 = (ClickyInkParser.Builder.<>O.<0>__AmountLine = new Func<string, ClickyInkDocument.Item>(ClickyInkParser.Builder.AmountLine));
            }
            List<ClickyInkDocument.Item> list9 = enumerable4.Select<string, ClickyInkDocument.Item>(func4).ToList<ClickyInkDocument.Item>();
            if (!dictionary.ContainsKey("values"))
            {
              dictionary["values"] = string.Join(", ", list9.Select<ClickyInkDocument.Item, string>(delegate(ClickyInkDocument.Item i)
              {
                if (i.Cells.Count <= 1)
                {
                  return i.Text;
                }
                return i.Cells[1];
              }));
              List<string> list10 = (from i in list9
                where i.Cells.Count > 1
                select i.Cells[0]).ToList<string>();
              if (list10.Count == list9.Count && list10.Count > 0 && !dictionary.ContainsKey("labels"))
              {
                dictionary["labels"] = string.Join(", ", list10);
              }
            }
            this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Spark, directive.Label, dictionary, null, null));
            return;
          }
          IL_0FC1:
          foreach (ClickyInkDocument.Page page in ClickyInkParser.Parse(string.Join("\n", body), false).Pages)
          {
            foreach (ClickyInkDocument.Block block in page.Blocks)
            {
              this.Add(block);
            }
          }
          return;
        }
        this.tileStrips++;
        using (List<ClickyInkDocument.Page>.Enumerator enumerator2 = ClickyInkParser.Parse(string.Join("\n", body), false).Pages.GetEnumerator())
        {
          while (enumerator2.MoveNext())
          {
            ClickyInkDocument.Page page2 = enumerator2.Current;
            foreach (ClickyInkDocument.Block block2 in page2.Blocks)
            {
              if (ClickyInkDashboardKinds.TileKinds.Contains(block2.Kind) || block2.Kind == ClickyInkDocument.Kind.Paragraph)
              {
                block2.Attributes["_tiles"] = this.tileStrips.ToString(CultureInfo.InvariantCulture);
              }
              this.Add(block2);
            }
          }
          return;
        }
        IL_097D:
        IEnumerable<string> enumerable5 = body.Select<string, string>(delegate(string line)
        {
          string text25 = line.TrimWs();
          bool flag = line.HasPrefix("  ") || line.HasPrefix("\t");
          if (text25.Length == 0 || flag || ClickyInkParser.ListItemStart(text25) != null)
          {
            return line;
          }
          return "- [ ] " + text25;
        });
        List<ClickyInkDocument.Item> list11 = ClickyInkParser.Parse(string.Join("\n", enumerable5), false).AllBlocks.Select<ValueTuple<int, ClickyInkDocument.Block>, ClickyInkDocument.Block>(([TupleElementNames(new string[] { "Page", "Block" })] ValueTuple<int, ClickyInkDocument.Block> b) => b.Item2).Where<ClickyInkDocument.Block>(delegate(ClickyInkDocument.Block b)
        {
          ClickyInkDocument.Kind kind4 = b.Kind;
          return kind4 - ClickyInkDocument.Kind.List <= 2;
        }).SelectMany<ClickyInkDocument.Block, ClickyInkDocument.Item>((ClickyInkDocument.Block b) => b.Items)
          .Take<ClickyInkDocument.Item>(40)
          .Select<ClickyInkDocument.Item, ClickyInkDocument.Item>(delegate(ClickyInkDocument.Item item)
          {
            bool? isChecked = item.IsChecked;
            bool flag2 = isChecked.GetValueOrDefault();
            if (isChecked == null)
            {
              flag2 = false;
              bool? flag3 = new bool?(flag2);
              item.IsChecked = flag3;
            }
            return item;
          })
          .ToList<ClickyInkDocument.Item>();
        dictionary["review"] = directive.Label;
        this.Add(new ClickyInkDocument.Block(ClickyInkDocument.Kind.Review, directive.Label, dictionary, list11, null));
      }

      // Token: 0x06003086 RID: 12422 RVA: 0x004363FC File Offset: 0x004363FC
      private static List<ClickyInkDocument.Item> ListItems(List<string> body, bool boxed = false)
      {
        IEnumerable<string> enumerable = body.Select<string, string>(delegate(string line)
        {
          string text = line.TrimWs();
          bool flag = line.HasPrefix("  ") || line.HasPrefix("\t");
          if (text.Length == 0 || flag || ClickyInkParser.ListItemStart(text) != null)
          {
            return line;
          }
          return (boxed ? "- [ ] " : "- ") + text;
        });
        return ClickyInkParser.Parse(string.Join("\n", enumerable), false).AllBlocks.Select<ValueTuple<int, ClickyInkDocument.Block>, ClickyInkDocument.Block>(([TupleElementNames(new string[] { "Page", "Block" })] ValueTuple<int, ClickyInkDocument.Block> b) => b.Item2).Where<ClickyInkDocument.Block>(delegate(ClickyInkDocument.Block b)
        {
          ClickyInkDocument.Kind kind = b.Kind;
          return kind - ClickyInkDocument.Kind.List <= 2;
        }).SelectMany<ClickyInkDocument.Block, ClickyInkDocument.Item>((ClickyInkDocument.Block b) => b.Items)
          .Take<ClickyInkDocument.Item>(40)
          .Select<ClickyInkDocument.Item, ClickyInkDocument.Item>(delegate(ClickyInkDocument.Item item)
          {
            if (boxed && item.IsChecked == null)
            {
              item.IsChecked = new bool?(false);
            }
            return item;
          })
          .ToList<ClickyInkDocument.Item>();
      }

      // Token: 0x06003087 RID: 12423 RVA: 0x004364C8 File Offset: 0x004364C8
      public unsafe static ClickyInkDocument.Item AmountLine(string line)
      {
        string text = line;
        ClickyInkParser.ListStart listStart = ClickyInkParser.ListItemStart(line);
        if (listStart != null)
        {
          text = listStart.Text;
        }
        ValueTuple<string, Dictionary<string, string>> valueTuple = ClickyInkParser.TrailingAttributes(text);
        string item = valueTuple.Item1;
        ClickyInkDocument.Mark? mark = ClickyInkDocumentKinds.FirstMark(valueTuple.Item2.Keys);
        Span<string> span;
        int num3;
        foreach (string text2 in new string[] { " | ", ": ", " … ", "...", " — ", " - ", "\t" })
        {
          int num = item.FindLast(text2);
          if (num >= 0)
          {
            string text3 = item.Substring(0, num).TrimSet(" .…");
            string text4 = item;
            int num2 = num + text2.Length;
            string text5 = text4.Substring(num2, text4.Length - num2).TrimWs();
            if (text3.Length > 0 && text5.Length > 0)
            {
              string text6 = ClickyInkParser.Clean(text3);
              string text7 = null;
              num2 = 2;
              List<string> list = new List<string>(num2);
              CollectionsMarshal.SetCount<string>(list, num2);
              span = CollectionsMarshal.AsSpan<string>(list);
              num3 = 0;
              *span[num3] = ClickyInkParser.Clean(text3);
              num3++;
              *span[num3] = text5;
              return new ClickyInkDocument.Item(text6, text7, list, mark, null, null, null);
            }
          }
        }
        string text8 = ClickyInkParser.Clean(item);
        string text9 = null;
        int i = 1;
        List<string> list2 = new List<string>(i);
        CollectionsMarshal.SetCount<string>(list2, i);
        span = CollectionsMarshal.AsSpan<string>(list2);
        num3 = 0;
        *span[num3] = ClickyInkParser.Clean(item);
        return new ClickyInkDocument.Item(text8, text9, list2, mark, null, null, null);
      }

      // Token: 0x06003088 RID: 12424 RVA: 0x0043667C File Offset: 0x0043667C
      public unsafe static ClickyInkDocument.Item TimedLine(string line)
      {
        string text = line;
        ClickyInkParser.ListStart listStart = ClickyInkParser.ListItemStart(line);
        if (listStart != null)
        {
          text = listStart.Text;
        }
        ValueTuple<string, Dictionary<string, string>> valueTuple = ClickyInkParser.TrailingAttributes(text);
        string item = valueTuple.Item1;
        ClickyInkDocument.Mark? mark = ClickyInkDocumentKinds.FirstMark(valueTuple.Item2.Keys);
        Match match = ClickyInkParser.TimedLineRegex().Match(item);
        if (match.Success)
        {
          string text2 = item;
          int num = match.Index + match.Length;
          string text3 = text2.Substring(num, text2.Length - num);
          string text4 = null;
          string text5 = text3;
          int num2 = text3.Find(" — ");
          if (num2 < 0)
          {
            num2 = text3.Find(" – ");
          }
          if (num2 >= 0)
          {
            text5 = text3.Substring(0, num2);
            text2 = text3;
            num = num2 + 3;
            text4 = text2.Substring(num, text2.Length - num);
          }
          string text6 = ClickyInkParser.Clean(text5);
          string text7 = ((text4 == null) ? null : ClickyInkParser.Clean(text4));
          num = 1;
          List<string> list = new List<string>(num);
          CollectionsMarshal.SetCount<string>(list, num);
          Span<string> span = CollectionsMarshal.AsSpan<string>(list);
          int num3 = 0;
          *span[num3] = match.Groups[1].Value.TrimWs();
          return new ClickyInkDocument.Item(text6, text7, list, mark, null, null, null);
        }
        return new ClickyInkDocument.Item(ClickyInkParser.Clean(item), null, null, mark, null, null, null);
      }

      // Token: 0x06003089 RID: 12425 RVA: 0x004367E8 File Offset: 0x004367E8
      public ClickyInkDocument Finished()
      {
        return new ClickyInkDocument(this.Document.Title, this.Document.Pages.Where<ClickyInkDocument.Page>((ClickyInkDocument.Page p) => p.Blocks.Count > 0).ToList<ClickyInkDocument.Page>());
      }

      // Token: 0x04001E40 RID: 7744
      public ClickyInkDocument Document = new ClickyInkDocument(null, new List<ClickyInkDocument.Page>());

      // Token: 0x04001E41 RID: 7745
      private readonly List<string> paragraph = new List<string>();

      // Token: 0x04001E42 RID: 7746
      private int tileStrips;

      // Token: 0x020006F9 RID: 1785
      [CompilerGenerated]
      private static class <>O
      {
        // Token: 0x04002653 RID: 9811
        [Nullable(0)]
        public static Func<string, ClickyInkDocument.Item> <0>__AmountLine;

        // Token: 0x04002654 RID: 9812
        [Nullable(0)]
        public static Func<string, ClickyInkDocument.Item> <1>__TimedLine;
      }
    }

    // Token: 0x020004D7 RID: 1239
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001E43 RID: 7747
      [Nullable(0)]
      public static Func<string, string> <0>__Clean;

      // Token: 0x04001E44 RID: 7748
      [Nullable(0)]
      public static Func<char, bool> <1>__IsNumber;
    }
  }
}
