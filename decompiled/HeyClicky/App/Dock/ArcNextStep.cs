using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000315 RID: 789
  internal static class ArcNextStep
  {
    // Token: 0x06002250 RID: 8784 RVA: 0x006222E0 File Offset: 0x006222E0
    [NullableContext(1)]
    [return: Nullable(2)]
    public static FrameworkElement Button(ArcDock dock, ArcCardContent content, bool isDraftCopied, Action onCopied, Action<string, JsonObject> track, bool canShowFullResponse = true, double height = 32.0, double fontSize = 13.5)
    {
      ClickyAnswer clickyAnswer = (content.IsDone ? content.Answer : null);
      if (((clickyAnswer != null) ? clickyAnswer.Ask : null) != null)
      {
        return ArcButtons.Gel("Type an answer", "keyboard", GelAppearance.SecondaryWhite, new double?(height), new double?(fontSize), GelButton.Sizing.Normal, delegate
        {
          Action<string, JsonObject> track2 = track;
          string text2 = "next_step";
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["kind"] = "type_answer";
          track2(text2, jsonObject);
          dock.OpenInChat(content);
          dock.Host.RequestComposerFocus();
        });
      }
      ClickyAnswer.Draft draft = ((clickyAnswer != null) ? clickyAnswer.DraftText : null);
      if (draft != null)
      {
        return ArcButtons.Gel(isDraftCopied ? "Copy again" : "Copy", "doc.on.doc", GelAppearance.PrimaryBlue, new double?(height), new double?(fontSize), GelButton.Sizing.Normal, delegate
        {
          Action<string, JsonObject> track3 = track;
          string text3 = "next_step";
          JsonObject jsonObject2 = new JsonObject(null);
          jsonObject2["kind"] = "copy";
          jsonObject2["is_repeat"] = isDraftCopied;
          track3(text3, jsonObject2);
          dock.CopyDraft(content, draft.Text);
          onCopied();
        });
      }
      ClickyAnswer.NextStep next = ((clickyAnswer != null) ? clickyAnswer.Next : null);
      if (next != null)
      {
        bool isLink = next.Open.StartsWith("http", StringComparison.OrdinalIgnoreCase);
        return ArcButtons.Gel(next.Label, isLink ? "arrow.up.right" : null, GelAppearance.PrimaryBlue, new double?(height), new double?(fontSize), GelButton.Sizing.Normal, delegate
        {
          Action<string, JsonObject> track4 = track;
          string text4 = "next_step";
          JsonObject jsonObject3 = new JsonObject(null);
          jsonObject3["kind"] = (isLink ? "next_link" : "next_file");
          track4(text4, jsonObject3);
          dock.OpenResultCardTarget(next.Open, content);
        });
      }
      CardReadyOutput output = content.ReadyOutput;
      if (output != null && (!output.OpensFullReplyOnly || !canShowFullResponse))
      {
        string buttonTitle = output.ButtonTitle;
        string text = output.ButtonSymbol;
        if (text == null)
        {
          CardReadyOutput.Kinds kind = output.Kind;
          bool flag = kind == CardReadyOutput.Kinds.File || kind == CardReadyOutput.Kinds.Several;
          text = (flag ? output.Symbol : null);
        }
        return ArcButtons.Gel(buttonTitle, text, GelAppearance.PrimaryBlue, new double?(height), new double?(fontSize), GelButton.Sizing.Normal, delegate
        {
          Action<string, JsonObject> track5 = track;
          string text5 = "next_step";
          JsonObject jsonObject4 = new JsonObject(null);
          jsonObject4["kind"] = "open_output";
          jsonObject4["output_kind"] = ArcDockAnalytics.OutputKind(output.Kind);
          track5(text5, jsonObject4);
          dock.OpenReadyOutput(content.RailKey, output);
        });
      }
      if (clickyAnswer != null && clickyAnswer.IsQuiet)
      {
        string routineId = dock.ResultCardRoutineId(content);
        if (routineId != null)
        {
          return ArcButtons.Gel("Run it now", "arrow.clockwise", GelAppearance.SecondaryWhite, new double?(height), new double?(fontSize), GelButton.Sizing.Normal, delegate
          {
            Action<string, JsonObject> track6 = track;
            string text6 = "next_step";
            JsonObject jsonObject5 = new JsonObject(null);
            jsonObject5["kind"] = "run_now";
            track6(text6, jsonObject5);
            dock.RunRoutineNow(routineId);
          });
        }
      }
      return null;
    }
  }
}
