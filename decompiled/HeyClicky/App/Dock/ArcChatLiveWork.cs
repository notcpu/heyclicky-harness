using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Ink;
using HeyClicky.App.Notch.Peek;
using HeyClicky.Core.Notch;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002F0 RID: 752
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class ArcChatLiveWork : IChatLiveWork
  {
    // Token: 0x06002047 RID: 8263 RVA: 0x00615FC4 File Offset: 0x00615FC4
    public ArcChatLiveWork(ArcDock dock, AgentService agents, [Nullable(2)] IChatLiveWork fallback)
    {
    }

    // Token: 0x06002048 RID: 8264 RVA: 0x00615FE4 File Offset: 0x00615FE4
    [return: Nullable(2)]
    private static string Key(ChatHandOff entry)
    {
      string jobId = entry.JobId;
      if (jobId == null)
      {
        return entry.ClickySlug;
      }
      return "job:" + jobId;
    }

    // Token: 0x06002049 RID: 8265 RVA: 0x00616010 File Offset: 0x00616010
    [return: Nullable(2)]
    public FrameworkElement WorkCard(ChatHandOff entry)
    {
      return this.WorkCard(entry, false);
    }

    // Token: 0x0600204A RID: 8266 RVA: 0x0061601C File Offset: 0x0061601C
    [return: Nullable(2)]
    public FrameworkElement WorkCard(ChatHandOff entry, bool hidesFinalizing)
    {
      string text = ArcChatLiveWork.Key(entry);
      if (text != null)
      {
        ArcCardContent arcCardContent = this.<dock>P.ContentForKey(text);
        if (arcCardContent != null)
        {
          return new ChatWorkCard(this.<dock>P, text, arcCardContent, hidesFinalizing);
        }
      }
      return null;
    }

    // Token: 0x0600204B RID: 8267 RVA: 0x00616054 File Offset: 0x00616054
    [return: Nullable(2)]
    public FrameworkElement NextStepButton(ChatHandOff entry)
    {
      ArcChatLiveWork.<>c__DisplayClass7_0 CS$<>8__locals1 = new ArcChatLiveWork.<>c__DisplayClass7_0();
      CS$<>8__locals1.<>4__this = this;
      string text = ArcChatLiveWork.Key(entry);
      if (text != null)
      {
        CS$<>8__locals1.content = this.<dock>P.ContentForKey(text);
        if (CS$<>8__locals1.content != null && CS$<>8__locals1.content.IsDone)
        {
          CS$<>8__locals1.holder = new Border();
          CS$<>8__locals1.<NextStepButton>g__Fill|0(false);
          if (CS$<>8__locals1.holder.Child != null)
          {
            return CS$<>8__locals1.holder;
          }
          return null;
        }
      }
      return null;
    }

    // Token: 0x0600204C RID: 8268 RVA: 0x006160C8 File Offset: 0x006160C8
    public bool IsWorking(ChatHandOff entry)
    {
      string text = ArcChatLiveWork.Key(entry);
      if (text != null)
      {
        ArcCardContent arcCardContent = this.<dock>P.ContentForKey(text);
        if (arcCardContent != null)
        {
          return arcCardContent.IsWorking;
        }
      }
      IChatLiveWork chatLiveWork = this.<fallback>P;
      return chatLiveWork != null && chatLiveWork.IsWorking(entry);
    }

    // Token: 0x14000079 RID: 121
    // (add) Token: 0x0600204D RID: 8269 RVA: 0x00616108 File Offset: 0x00616108
    // (remove) Token: 0x0600204E RID: 8270 RVA: 0x00616118 File Offset: 0x00616118
    [Nullable(2)]
    public event Action Changed
    {
      [NullableContext(2)]
      add
      {
        this.<dock>P.ContentChanged += value;
      }
      [NullableContext(2)]
      remove
      {
        this.<dock>P.ContentChanged -= value;
      }
    }

    // Token: 0x0600204F RID: 8271 RVA: 0x00616128 File Offset: 0x00616128
    [return: Nullable(2)]
    public string DisplayName(ChatHandOff entry)
    {
      IChatLiveWork chatLiveWork = this.<fallback>P;
      if (chatLiveWork == null)
      {
        return null;
      }
      return chatLiveWork.DisplayName(entry);
    }

    // Token: 0x06002050 RID: 8272 RVA: 0x0061613C File Offset: 0x0061613C
    [return: Nullable(2)]
    public FrameworkElement Chip(ChatHandOff entry, double size)
    {
      int num = NotePaperColors.Seed(entry.Id);
      string clickySlug = entry.ClickySlug;
      if (clickySlug != null)
      {
        ClickyInfo clickyInfo = this.<agents>P.FindClicky(clickySlug);
        if (clickyInfo != null)
        {
          return new NoteChipView("clicky:" + clickySlug, ClickyInkIcon.Inferred(clickyInfo.Name + " " + clickyInfo.RoleText) ?? "sparkles", NotePaperColors.Seed(clickySlug), num, size, true);
        }
      }
      string jobId = entry.JobId;
      if (jobId != null)
      {
        ClickyJob clickyJob = this.<agents>P.Jobs.Job(jobId);
        if (clickyJob != null)
        {
          return new NoteChipView("job:" + clickyJob.Id, ClickyInkIcon.Inferred(clickyJob.Title) ?? "checklist", NotePaperColors.Seed(clickyJob.RailKey), num, size, true);
        }
      }
      return null;
    }

    // Token: 0x06002051 RID: 8273 RVA: 0x0061620C File Offset: 0x0061620C
    [return: Nullable(2)]
    public FrameworkElement FileCards(IReadOnlyList<string> entries, Action<string> open)
    {
      return new FileCardRow(entries, open);
    }

    // Token: 0x06002052 RID: 8274 RVA: 0x00616218 File Offset: 0x00616218
    [return: Nullable(2)]
    public FrameworkElement PlanFold(ChatHandOff entry, Action onFinished)
    {
      string jobId = entry.JobId;
      string text;
      if (jobId == null)
      {
        string clickySlug = entry.ClickySlug;
        text = ((clickySlug != null) ? ArcLiveNotes.ClickyNotePath(this.<agents>P, clickySlug) : null);
      }
      else
      {
        text = "job:" + jobId;
      }
      string text2 = text;
      if (text2 != null)
      {
        ClickyArcPlan clickyArcPlan = this.<agents>P.LiveSteps.LastPlan(text2);
        if (clickyArcPlan != null && !clickyArcPlan.IsEmpty)
        {
          string text3 = ArcChatLiveWork.Key(entry);
          if (text3 != null)
          {
            ArcCardContent arcCardContent = this.<dock>P.ContentForKey(text3);
            if (arcCardContent != null)
            {
              JsonObject answer = entry.Answer;
              string text4;
              if ((text4 = (string)((answer != null) ? answer["line"] : null)) == null)
              {
                text4 = entry.ReplyText ?? "";
              }
              string text5 = text4;
              return new ChatPlanFold(clickyArcPlan, PlanCascade.Done(clickyArcPlan), text5, arcCardContent.Paper.Ink, arcCardContent.Paper.TitleWash, onFinished);
            }
          }
          return null;
        }
      }
      return null;
    }

    // Token: 0x04000FA2 RID: 4002
    [CompilerGenerated]
    private ArcDock <dock>P = dock;

    // Token: 0x04000FA3 RID: 4003
    [CompilerGenerated]
    private AgentService <agents>P = agents;

    // Token: 0x04000FA4 RID: 4004
    [Nullable(2)]
    [CompilerGenerated]
    private IChatLiveWork <fallback>P = fallback;
  }
}
