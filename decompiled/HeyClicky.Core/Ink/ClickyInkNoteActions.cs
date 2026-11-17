using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000123 RID: 291
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkNoteActions
  {
    // Token: 0x06001303 RID: 4867 RVA: 0x003A46E4 File Offset: 0x003A46E4
    public static ClickyInkNoteAction Ask(ClickyInkDocument.Block block)
    {
      string text = ClickyInkNoteActions.Plain(block.Text);
      string text2;
      if ((text2 = block["say"]) == null)
      {
        text2 = block["ask"] ?? block.Text;
      }
      string text3 = ClickyInkNoteActions.Plain(text2);
      return new ClickyInkNoteAction(ClickyInkNoteAction.ActionKind.Ask, ClickyInkObjectKey.Of(block), (text3.Length == 0) ? text : text3, "The user tapped \"" + ((text.Length == 0) ? text3 : text) + "\" on this job's note (note.md). Do what it asks. Then rewrite note.md in place for it: change only what changes and keep every other line exactly as it is. Put a ::note[…] beside the change saying what they asked, in a few words.", "you asked: " + ((text.Length == 0) ? text3 : text), "On it.", null).ForAnalytics("ask_tapped", null, null);
    }

    // Token: 0x06001304 RID: 4868 RVA: 0x003A4784 File Offset: 0x003A4784
    [return: Nullable(2)]
    public static ClickyInkNoteAction Answer(ClickyInkDocument.Block block, bool saidYes)
    {
      string text = (saidYes ? block["yes"] : block["no"]);
      if (text == null)
      {
        return null;
      }
      string text2 = ClickyInkNoteActions.Plain(text);
      if (text2.Length == 0)
      {
        return null;
      }
      string text3 = ClickyInkNoteActions.Plain(saidYes ? (block["yes_label"] ?? "Yes") : (block["no_label"] ?? "No"));
      string text4 = ClickyInkNoteActions.Plain(block.Text);
      string text5 = text3.Lowered();
      bool flag = text5 == "yes" || text5 == "no";
      bool flag2 = flag;
      ClickyInkNoteAction.ActionKind actionKind = ClickyInkNoteAction.ActionKind.Answer;
      string text6 = ClickyInkObjectKey.Of(block);
      string text7 = text2;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(151, 3);
      defaultInterpolatedStringHandler.AppendLiteral("The user answered the question on this job's note, \"");
      defaultInterpolatedStringHandler.AppendFormatted(text4);
      defaultInterpolatedStringHandler.AppendLiteral("\": they tapped \"");
      defaultInterpolatedStringHandler.AppendFormatted(text3);
      defaultInterpolatedStringHandler.AppendLiteral("\", ");
      defaultInterpolatedStringHandler.AppendLiteral("meaning: ");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral(" Act on that answer. Take that ::question off note.md (it's answered), ");
      return new ClickyInkNoteAction(actionKind, text6, text7, defaultInterpolatedStringHandler.ToStringAndClear() + "Then rewrite note.md in place for it: change only what changes and keep every other line exactly as it is. Put a ::note[…] beside the change saying what they chose.", "you said: " + (flag2 ? ClickyInkNoteActions.Short(text2) : text3), "Got it.", null).ForAnalytics("question_answered", saidYes ? "yes" : "no", null);
    }

    // Token: 0x06001305 RID: 4869 RVA: 0x003A48E8 File Offset: 0x003A48E8
    [return: Nullable(2)]
    public static ClickyInkNoteAction Send(ClickyInkDocument.Block block)
    {
      string text = block["send"] ?? block["ask"];
      if (text == null)
      {
        return null;
      }
      string text2 = ClickyInkNoteActions.Plain(text);
      if (text2.Length == 0)
      {
        return null;
      }
      string text3 = "";
      string text4 = block["to"];
      if (text4 != null && text4.Length > 0)
      {
        text3 = text3 + " to " + ClickyInkNoteActions.Plain(text4);
      }
      string text5 = block["subject"];
      if (text5 != null && text5.Length > 0)
      {
        text3 = text3 + " (subject \"" + ClickyInkNoteActions.Plain(text5) + "\")";
      }
      return new ClickyInkNoteAction(ClickyInkNoteAction.ActionKind.Send, ClickyInkObjectKey.Of(block), text2, "The user tapped Send on the draft" + text3 + " in this job's note. That tap is their go-ahead to send this one message: send it exactly as note.md has it now, without rewording it. Then rewrite note.md in place: the letter becomes one short line saying it was sent (to whom, when), or plainly why it couldn't be sent and what they can do. Keep every other line as it is.", "you said: send it", "Okay, I'll send it.", null).ForAnalytics("letter_send", null, null);
    }

    // Token: 0x06001306 RID: 4870 RVA: 0x003A49C0 File Offset: 0x003A49C0
    [return: Nullable(2)]
    public static ClickyInkNoteAction Review(ClickyInkDocument.Block block, IReadOnlyList<string> ticked, int total)
    {
      Func<string, string> func;
      if ((func = ClickyInkNoteActions.<>O.<0>__Plain) == null)
      {
        func = (ClickyInkNoteActions.<>O.<0>__Plain = new Func<string, string>(ClickyInkNoteActions.Plain));
      }
      List<string> list = (from i in ticked.Select<string, string>(func)
        where i.Length > 0
        select i).ToList<string>();
      if (list.Count == 0)
      {
        return null;
      }
      string text = ClickyInkNoteActions.Plain(ClickyInkReviewRules.Label(block));
      string text2 = ClickyInkReviewRules.GoInstruction(block);
      int num = Math.Max(0, total - list.Count);
      string text3 = text2 + ": " + string.Join("; ", list) + ".";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (num > 0)
      {
        string text4 = text3;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 2);
        defaultInterpolatedStringHandler.AppendLiteral(" Leave the other ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num);
        defaultInterpolatedStringHandler.AppendLiteral(" as ");
        defaultInterpolatedStringHandler.AppendFormatted((num == 1) ? "it is" : "they are");
        defaultInterpolatedStringHandler.AppendLiteral(".");
        text3 = text4 + defaultInterpolatedStringHandler.ToStringAndClear();
      }
      string text5 = ((text.Length == 0) ? "the list" : ("the list \"" + text + "\""));
      ClickyInkNoteAction.ActionKind actionKind = ClickyInkNoteAction.ActionKind.Review;
      string text6 = ClickyInkObjectKey.Of(block);
      string text7 = text3;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(171, 4);
      defaultInterpolatedStringHandler.AppendLiteral("The user reviewed ");
      defaultInterpolatedStringHandler.AppendFormatted(text5);
      defaultInterpolatedStringHandler.AppendLiteral(" on this job's note and ticked ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" of ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(total);
      defaultInterpolatedStringHandler.AppendLiteral(" to go ahead with. ");
      defaultInterpolatedStringHandler.AppendLiteral("Do it for exactly those ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
      defaultInterpolatedStringHandler.AppendLiteral(", nothing else; leave the unticked ones alone. Then rewrite that :::review ");
      string text8 = defaultInterpolatedStringHandler.ToStringAndClear() + "list in note.md in place: mark each one you did {done} (or {out} with the reason on the line under it when it couldn't be done), keep every unticked line exactly as it is (each still its own line, still theirs to decide), and put a ::note[…] beside it saying what they approved.";
      string text9 = "you asked: ";
      string text10 = ClickyInkReviewRules.GoLabel(block, list.Count).Lowered();
      string text11;
      if (num <= 0)
      {
        text11 = "";
      }
      else
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
        defaultInterpolatedStringHandler.AppendLiteral(" of ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(total);
        text11 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      string text12 = text9 + text10 + text11;
      string text13;
      if (list.Count != 1)
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 1);
        defaultInterpolatedStringHandler.AppendLiteral("Okay, those ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
        defaultInterpolatedStringHandler.AppendLiteral(".");
        text13 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text13 = "Okay, that one.";
      }
      ClickyInkNoteAction clickyInkNoteAction = new ClickyInkNoteAction(actionKind, text6, text7, text8, text12, text13, list);
      string text14 = "review_go_ahead";
      string text15 = null;
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      dictionary["ticked"] = list.Count;
      dictionary["total"] = total;
      return clickyInkNoteAction.ForAnalytics(text14, text15, dictionary);
    }

    // Token: 0x06001307 RID: 4871 RVA: 0x003A4C3C File Offset: 0x003A4C3C
    [return: Nullable(2)]
    public static ClickyInkNoteAction SkipReview(ClickyInkDocument.Block block)
    {
      string text = block["skip"];
      if (text == null)
      {
        return null;
      }
      string text2 = ClickyInkNoteActions.Plain(text);
      if (text2.Length == 0)
      {
        return null;
      }
      string text3 = ClickyInkNoteActions.Plain(ClickyInkReviewRules.Label(block));
      return new ClickyInkNoteAction(ClickyInkNoteAction.ActionKind.Review, ClickyInkObjectKey.Of(block), text2, "The user looked at " + ((text3.Length == 0) ? "the list" : ("the list \"" + text3 + "\"")) + " on this job's note and said not now to all of it: do none of them. Then rewrite note.md in place: that :::review list becomes one short line saying they passed for now. Keep every other line as it is.", "you said: " + ClickyInkNoteActions.Plain(block["skip_label"] ?? "not now").Lowered(), "Okay, none of them.", Array.Empty<string>()).ForAnalytics("review_skipped", null, null);
    }

    // Token: 0x06001308 RID: 4872 RVA: 0x003A4CF0 File Offset: 0x003A4CF0
    public static ClickyInkNoteAction ActionForSentWords(string words, ClickyInkLiveNote note)
    {
      string text = ClickyInkNoteActions.Plain(words);
      foreach (ClickyInkLiveNote.Page page in note.Pages)
      {
        foreach (ClickyInkLiveNote.Block block in page.Blocks)
        {
          if (block.GoneAt == null)
          {
            ClickyInkDocument.Block source = block.Source;
            ClickyInkDocument.Kind kind = source.Kind;
            if (kind <= ClickyInkDocument.Kind.Ask)
            {
              if (kind != ClickyInkDocument.Kind.Letter)
              {
                if (kind == ClickyInkDocument.Kind.Ask)
                {
                  string text2;
                  if ((text2 = source["say"]) == null)
                  {
                    text2 = source["ask"] ?? source.Text;
                  }
                  if (ClickyInkNoteActions.Plain(text2) == text)
                  {
                    return ClickyInkNoteActions.Ask(source);
                  }
                }
              }
              else
              {
                string text3 = source["send"] ?? source["ask"];
                if (text3 != null && ClickyInkNoteActions.Plain(text3) == text)
                {
                  ClickyInkNoteAction clickyInkNoteAction = ClickyInkNoteActions.Send(source);
                  if (clickyInkNoteAction != null)
                  {
                    return clickyInkNoteAction;
                  }
                }
              }
            }
            else if (kind != ClickyInkDocument.Kind.Question)
            {
              if (kind == ClickyInkDocument.Kind.Review)
              {
                string text4 = source["skip"];
                if (text4 != null && ClickyInkNoteActions.Plain(text4) == text)
                {
                  ClickyInkNoteAction clickyInkNoteAction2 = ClickyInkNoteActions.SkipReview(source);
                  if (clickyInkNoteAction2 != null)
                  {
                    return clickyInkNoteAction2;
                  }
                }
                else if (text.HasPrefix(ClickyInkReviewRules.GoInstruction(source) + ":"))
                {
                  List<string> list = ClickyInkReviewRules.ItemsToSend(source, Array.Empty<string>());
                  IEnumerable<ClickyInkDocument.Item> items = source.Items;
                  Func<ClickyInkDocument.Item, bool> func;
                  if ((func = ClickyInkNoteActions.<>O.<1>__IsOpen) == null)
                  {
                    func = (ClickyInkNoteActions.<>O.<1>__IsOpen = new Func<ClickyInkDocument.Item, bool>(ClickyInkReviewRules.IsOpen));
                  }
                  int num = items.Count<ClickyInkDocument.Item>(func);
                  ClickyInkNoteAction clickyInkNoteAction3 = ClickyInkNoteActions.Review(source, list, num);
                  if (clickyInkNoteAction3 != null)
                  {
                    return clickyInkNoteAction3;
                  }
                }
              }
            }
            else
            {
              string text5 = source["yes"];
              if (text5 != null && ClickyInkNoteActions.Plain(text5) == text)
              {
                ClickyInkNoteAction clickyInkNoteAction4 = ClickyInkNoteActions.Answer(source, true);
                if (clickyInkNoteAction4 != null)
                {
                  return clickyInkNoteAction4;
                }
              }
              else
              {
                string text6 = source["no"];
                if (text6 != null && ClickyInkNoteActions.Plain(text6) == text)
                {
                  ClickyInkNoteAction clickyInkNoteAction5 = ClickyInkNoteActions.Answer(source, false);
                  if (clickyInkNoteAction5 != null)
                  {
                    return clickyInkNoteAction5;
                  }
                }
              }
            }
          }
        }
      }
      ClickyInkDocument.Kind kind2 = ClickyInkDocument.Kind.Ask;
      string text7 = text;
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["say"] = text;
      return ClickyInkNoteActions.Ask(new ClickyInkDocument.Block(kind2, text7, dictionary, null, null)).ForAnalytics("ask_tapped", "unmatched_words", null);
    }

    // Token: 0x06001309 RID: 4873 RVA: 0x003A4FD8 File Offset: 0x003A4FD8
    public static string Plain(string text)
    {
      return ClickyInkParser.Emphasis.Strip(text).TrimWsNl();
    }

    // Token: 0x0600130A RID: 4874 RVA: 0x003A4FE8 File Offset: 0x003A4FE8
    private static string Short(string text)
    {
      if (text.CharacterCount() <= 64)
      {
        return text;
      }
      return text.PrefixCharacters(61).TrimWs() + "…";
    }

    // Token: 0x0600130B RID: 4875 RVA: 0x003A5010 File Offset: 0x003A5010
    [return: Nullable(2)]
    public static ClickyInkNoteAction Answer(ClickyInkDocument.Block block, string answer, bool wasTyped = false)
    {
      string text = ClickyInkNoteActions.Plain(answer);
      if (text.Length == 0)
      {
        return null;
      }
      string text2 = ClickyInkNoteActions.Plain(block.Text);
      return new ClickyInkNoteAction(ClickyInkNoteAction.ActionKind.Answer, ClickyInkObjectKey.Of(block), text, string.Concat(new string[]
      {
        "The user answered the question on this job's note, \"",
        text2,
        "\": ",
        wasTyped ? ("they typed \"" + text + "\".") : ("they chose \"" + text + "\"."),
        " Act on that answer. Take that ::question off note.md (it's answered), Then rewrite note.md in place for it: change only what changes and keep every other line exactly as it is. Put a ::note[…] beside the change saying what they chose."
      }), "you said: " + ClickyInkNoteActions.Shortened(text), "Got it.", null).ForAnalytics("question_answered", wasTyped ? "typed" : "option", null);
    }

    // Token: 0x0600130C RID: 4876 RVA: 0x003A50C8 File Offset: 0x003A50C8
    [return: Nullable(2)]
    public static ClickyInkNoteAction Form(ClickyInkDocument.Block block, [TupleElementNames(new string[] { "Label", "Value" })] [Nullable(new byte[] { 1, 0, 1, 1 })] IReadOnlyList<ValueTuple<string, string>> answers, bool usedDefaults)
    {
      List<ValueTuple<string, string>> list = (from a in answers
        select new ValueTuple<string, string>(ClickyInkNoteActions.Plain(a.Item1), ClickyInkNoteActions.Plain(a.Item2)) into a
        where a.Item2.Length > 0
        select a).ToList<ValueTuple<string, string>>();
      string text = ClickyInkNoteActions.Plain(block.Text);
      string text2 = ClickyInkNoteActions.Plain(block["submit"] ?? "Here are my answers");
      string text3;
      if (usedDefaults)
      {
        text3 = ClickyInkNoteActions.Plain(block["defaults"] ?? "Use your best guess for anything I haven't told you.");
        if (list.Count > 0)
        {
          string[] array = new string[5];
          array[0] = text2;
          array[1] = ":\n";
          array[2] = string.Join("\n", list.Select<ValueTuple<string, string>, string>(([TupleElementNames(new string[] { "Label", "Value" })] ValueTuple<string, string> f) => "- " + f.Item1 + ": " + f.Item2));
          array[3] = "\n";
          array[4] = text3;
          text3 = string.Concat(array);
        }
      }
      else
      {
        if (list.Count == 0)
        {
          return null;
        }
        text3 = text2 + ":\n" + string.Join("\n", list.Select<ValueTuple<string, string>, string>(([TupleElementNames(new string[] { "Label", "Value" })] ValueTuple<string, string> f) => "- " + f.Item1 + ": " + f.Item2));
      }
      ClickyInkNoteAction.ActionKind actionKind = ClickyInkNoteAction.ActionKind.Answer;
      string text4 = ClickyInkObjectKey.Of(block);
      string text5 = text3;
      string text6 = "The user filled in the form" + ((text.Length == 0) ? "" : (" \"" + text + "\"")) + " on this job's note. Use their answers and carry on with the work. Take the :::form off note.md (it's answered), Then rewrite note.md in place for it: change only what changes and keep every other line exactly as it is.";
      string text7;
      if (!usedDefaults || list.Count != 0)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 2);
        defaultInterpolatedStringHandler.AppendLiteral("you sent ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" answer");
        defaultInterpolatedStringHandler.AppendFormatted((list.Count == 1) ? "" : "s");
        text7 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text7 = "you said: use your best guess";
      }
      ClickyInkNoteAction clickyInkNoteAction = new ClickyInkNoteAction(actionKind, text4, text5, text6, text7, "Thanks, carrying on.", null);
      string text8 = "form_sent";
      string text9 = (usedDefaults ? "best_guess" : "filled");
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      dictionary["fields_filled"] = list.Count;
      dictionary["fields_total"] = answers.Count;
      return clickyInkNoteAction.ForAnalytics(text8, text9, dictionary);
    }

    // Token: 0x0600130D RID: 4877 RVA: 0x003A52F8 File Offset: 0x003A52F8
    public static ClickyInkNoteAction Connected(ClickyInkDocument.Block block)
    {
      string text = ClickyInkNoteActions.Plain((block.Text.Length == 0) ? (block["app"] ?? "the app") : block.Text);
      string text2 = ClickyInkNoteActions.Plain(block["retry"] ?? ("I connected " + text + ". Try again."));
      return new ClickyInkNoteAction(ClickyInkNoteAction.ActionKind.Ask, ClickyInkObjectKey.Of(block), text2, "The user connected " + text + " from the note, as it asked. Try again with it. If it works, take the ::connect off note.md; if it still isn't connected, say so plainly. Then rewrite note.md in place for it: change only what changes and keep every other line exactly as it is.", "you said: " + text + " is connected", "Trying again.", null).ForAnalytics("connect_confirmed", null, null);
    }

    // Token: 0x0600130E RID: 4878 RVA: 0x003A5398 File Offset: 0x003A5398
    [return: Nullable(2)]
    public static ClickyInkNoteAction ItemAction(ClickyInkDocument.Block block, string item, string action)
    {
      string text = ClickyInkReviewRules.Action(item);
      string text2 = ClickyInkNoteActions.Plain(action);
      if (text.Length == 0 || text2.Length == 0)
      {
        return null;
      }
      string text3 = ClickyInkNoteActions.Plain(block.Text);
      ClickyInkNoteAction.ActionKind actionKind = ClickyInkNoteAction.ActionKind.Answer;
      string text4 = ClickyInkNoteActions.ItemKey(block, item);
      string text5 = text2 + ": " + text;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(57, 3);
      defaultInterpolatedStringHandler.AppendLiteral("The user tapped \"");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral("\" on \"");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\" in the list");
      defaultInterpolatedStringHandler.AppendFormatted((text3.Length == 0) ? "" : (" \"" + text3 + "\""));
      defaultInterpolatedStringHandler.AppendLiteral(" on this job's note. ");
      return new ClickyInkNoteAction(actionKind, text4, text5, defaultInterpolatedStringHandler.ToStringAndClear() + "Do that for that one row only. Then rewrite that row in note.md in place to show what happened (mark it {done}, or {out} with why on the line under it, and put any draft it needs as a :::letter under the list); keep every other line exactly as it is.", "you said: " + text2.Lowered(), "On it.", null).ForAnalytics("item_action", null, null);
    }

    // Token: 0x0600130F RID: 4879 RVA: 0x003A5490 File Offset: 0x003A5490
    public static string ItemKey(ClickyInkDocument.Block block, string item)
    {
      return ClickyInkObjectKey.Of(block) + "|" + ClickyInkObjectKey.Normalized(ClickyInkReviewRules.Action(item));
    }

    // Token: 0x06001310 RID: 4880 RVA: 0x003A54B0 File Offset: 0x003A54B0
    public static ClickyInkNoteAction QuizResults(ClickyInkDocument.Block block, int right, int total, IReadOnlyList<string> missed)
    {
      string text = ClickyInkNoteActions.Plain(block.Text);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 2);
      defaultInterpolatedStringHandler.AppendLiteral("I got ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(right);
      defaultInterpolatedStringHandler.AppendLiteral(" of ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(total);
      defaultInterpolatedStringHandler.AppendLiteral(" right");
      string text2 = defaultInterpolatedStringHandler.ToStringAndClear();
      if (missed.Count > 0)
      {
        string text3 = text2;
        string text4 = ". I missed: ";
        string text5 = "; ";
        Func<string, string> func;
        if ((func = ClickyInkNoteActions.<>O.<0>__Plain) == null)
        {
          func = (ClickyInkNoteActions.<>O.<0>__Plain = new Func<string, string>(ClickyInkNoteActions.Plain));
        }
        text2 = text3 + text4 + string.Join(text5, missed.Select<string, string>(func));
      }
      text2 += ".";
      ClickyInkNoteAction.ActionKind actionKind = ClickyInkNoteAction.ActionKind.Ask;
      string text6 = ClickyInkObjectKey.Of(block);
      string text7 = text2;
      string text8 = "The user worked through the quiz" + ((text.Length == 0) ? "" : (" \"" + text + "\"")) + " on this job's note and sent their score. Explain what they missed in a few words each, then give them a new :::quiz that practises exactly those. Then rewrite note.md in place for it: change only what changes and keep every other line exactly as it is.";
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 2);
      defaultInterpolatedStringHandler.AppendLiteral("you sent your score: ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(right);
      defaultInterpolatedStringHandler.AppendLiteral("/");
      defaultInterpolatedStringHandler.AppendFormatted<int>(total);
      ClickyInkNoteAction clickyInkNoteAction = new ClickyInkNoteAction(actionKind, text6, text7, text8, defaultInterpolatedStringHandler.ToStringAndClear(), "Nice work.", null);
      string text9 = "quiz_score_sent";
      string text10 = null;
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      dictionary["right"] = right;
      dictionary["total"] = total;
      return clickyInkNoteAction.ForAnalytics(text9, text10, dictionary);
    }

    // Token: 0x06001311 RID: 4881 RVA: 0x003A5600 File Offset: 0x003A5600
    [return: Nullable(2)]
    public static ClickyInkNoteAction Pick(ClickyInkDocument.Block block, string option)
    {
      string text = ClickyInkNoteActions.Plain(option);
      if (text.Length == 0)
      {
        return null;
      }
      string text2 = ClickyInkNoteActions.Plain(block["pick"] ?? "Go with");
      ClickyInkNoteAction.ActionKind actionKind = ClickyInkNoteAction.ActionKind.Answer;
      string text3 = ClickyInkObjectKey.Of(block);
      string text4 = text2 + ": " + text;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(56, 2);
      defaultInterpolatedStringHandler.AppendLiteral("The user picked \"");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\" from the options");
      defaultInterpolatedStringHandler.AppendFormatted((block.Text.Length == 0) ? "" : (" \"" + ClickyInkNoteActions.Plain(block.Text) + "\""));
      defaultInterpolatedStringHandler.AppendLiteral(" on this job's note. ");
      return new ClickyInkNoteAction(actionKind, text3, text4, defaultInterpolatedStringHandler.ToStringAndClear() + "Carry on with that one. Mark it {pick} in the :::gallery and the rest {out}, Then rewrite note.md in place for it: change only what changes and keep every other line exactly as it is.", "you picked: " + ClickyInkNoteActions.Shortened(text), "Good pick.", null).ForAnalytics("gallery_pick", null, null);
    }

    // Token: 0x06001312 RID: 4882 RVA: 0x003A56F0 File Offset: 0x003A56F0
    [return: Nullable(2)]
    public static ClickyInkNoteAction Criteria(ClickyInkDocument.Block block, [TupleElementNames(new string[] { "Name", "From", "To" })] [Nullable(new byte[] { 1, 0, 1, 1, 1 })] IReadOnlyList<ValueTuple<string, string, string>> changes)
    {
      List<ValueTuple<string, string, string>> list = changes.Where<ValueTuple<string, string, string>>(([TupleElementNames(new string[] { "Name", "From", "To" })] ValueTuple<string, string, string> c) => ClickyInkNoteActions.Plain(c.Item2) != ClickyInkNoteActions.Plain(c.Item3)).ToList<ValueTuple<string, string, string>>();
      if (list.Count == 0)
      {
        return null;
      }
      IEnumerable<string> enumerable = list.Select<ValueTuple<string, string, string>, string>(delegate([TupleElementNames(new string[] { "Name", "From", "To" })] ValueTuple<string, string, string> c)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(11, 3);
        defaultInterpolatedStringHandler2.AppendLiteral("- ");
        defaultInterpolatedStringHandler2.AppendFormatted(ClickyInkNoteActions.Plain(c.Item1));
        defaultInterpolatedStringHandler2.AppendLiteral(": ");
        defaultInterpolatedStringHandler2.AppendFormatted((ClickyInkNoteActions.Plain(c.Item3).Length == 0) ? "(none)" : ClickyInkNoteActions.Plain(c.Item3));
        defaultInterpolatedStringHandler2.AppendLiteral(" (was ");
        defaultInterpolatedStringHandler2.AppendFormatted((ClickyInkNoteActions.Plain(c.Item2).Length == 0) ? "none" : ClickyInkNoteActions.Plain(c.Item2));
        defaultInterpolatedStringHandler2.AppendLiteral(")");
        return defaultInterpolatedStringHandler2.ToStringAndClear();
      });
      ClickyInkNoteAction.ActionKind actionKind = ClickyInkNoteAction.ActionKind.Ask;
      string text = ClickyInkObjectKey.Of(block);
      string text2 = "Change what you look for:\n" + string.Join("\n", enumerable);
      string text3 = "The user edited the criteria on this job's note. Use the new values from now on: if this is a routine, update the routine's task to match (a <ROUTINE> update block). Update the :::criteria in note.md to the new values, Then rewrite note.md in place for it: change only what changes and keep every other line exactly as it is.";
      string text4 = "you changed ";
      string text5;
      if (list.Count != 1)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" criteria");
        text5 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text5 = ClickyInkNoteActions.Plain(list[0].Item1).Lowered();
      }
      ClickyInkNoteAction clickyInkNoteAction = new ClickyInkNoteAction(actionKind, text, text2, text3, text4 + text5, "Updated.", null);
      string text6 = "criteria_saved";
      string text7 = null;
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      dictionary["changed"] = list.Count;
      return clickyInkNoteAction.ForAnalytics(text6, text7, dictionary);
    }

    // Token: 0x06001313 RID: 4883 RVA: 0x003A57FC File Offset: 0x003A57FC
    public static ClickyInkNoteAction Retone(ClickyInkDocument.Block block, string tone)
    {
      string text = ClickyInkNoteActions.Plain(block["to"] ?? "");
      ClickyInkNoteAction.ActionKind actionKind = ClickyInkNoteAction.ActionKind.Ask;
      string text2 = ClickyInkObjectKey.Of(block);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 2);
      defaultInterpolatedStringHandler.AppendLiteral("Make the draft");
      defaultInterpolatedStringHandler.AppendFormatted((text.Length == 0) ? "" : (" to " + text));
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(tone.Lowered());
      defaultInterpolatedStringHandler.AppendLiteral(".");
      string text3 = defaultInterpolatedStringHandler.ToStringAndClear();
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(54, 2);
      defaultInterpolatedStringHandler.AppendLiteral("The user asked to make the draft");
      defaultInterpolatedStringHandler.AppendFormatted((text.Length == 0) ? "" : (" to " + text));
      defaultInterpolatedStringHandler.AppendLiteral(" on this job's note ");
      defaultInterpolatedStringHandler.AppendFormatted(tone.Lowered());
      defaultInterpolatedStringHandler.AppendLiteral(". ");
      return new ClickyInkNoteAction(actionKind, text2, text3, defaultInterpolatedStringHandler.ToStringAndClear() + "Rewrite only that :::letter's text, keeping what it says; do not send it. Then rewrite note.md in place for it: change only what changes and keep every other line exactly as it is.", "you asked: " + tone.Lowered(), "Rewriting it.", null).ForAnalytics("letter_retone", null, null);
    }

    // Token: 0x06001314 RID: 4884 RVA: 0x003A5924 File Offset: 0x003A5924
    private static string Shortened(string text)
    {
      if (text.CharacterCount() <= 64)
      {
        return text;
      }
      return text.PrefixCharacters(61).TrimWs() + "…";
    }

    // Token: 0x0400088D RID: 2189
    private const string RewriteInPlace = "Then rewrite note.md in place for it: change only what changes and keep every other line exactly as it is.";

    // Token: 0x0400088E RID: 2190
    private const string EditInPlace = "Then rewrite note.md in place for it: change only what changes and keep every other line exactly as it is.";

    // Token: 0x02000495 RID: 1173
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001D5D RID: 7517
      [Nullable(0)]
      public static Func<string, string> <0>__Plain;

      // Token: 0x04001D5E RID: 7518
      [Nullable(0)]
      public static Func<ClickyInkDocument.Item, bool> <1>__IsOpen;
    }
  }
}
