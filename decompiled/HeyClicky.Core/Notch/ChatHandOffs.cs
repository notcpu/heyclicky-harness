using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000100 RID: 256
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ChatHandOffs
  {
    // Token: 0x170003E7 RID: 999
    // (get) Token: 0x060010B0 RID: 4272 RVA: 0x00393078 File Offset: 0x00393078
    // (set) Token: 0x060010B1 RID: 4273 RVA: 0x00393080 File Offset: 0x00393080
    public IChatHandOffDirectory Directory { get; set; }

    // Token: 0x170003E8 RID: 1000
    // (get) Token: 0x060010B2 RID: 4274 RVA: 0x0039308C File Offset: 0x0039308C
    // (set) Token: 0x060010B3 RID: 4275 RVA: 0x00393094 File Offset: 0x00393094
    public Action RefreshHistorySoon { get; set; }

    // Token: 0x14000024 RID: 36
    // (add) Token: 0x060010B4 RID: 4276 RVA: 0x003930A0 File Offset: 0x003930A0
    // (remove) Token: 0x060010B5 RID: 4277 RVA: 0x003930D8 File Offset: 0x003930D8
    public event Action Changed;

    // Token: 0x060010B6 RID: 4278 RVA: 0x00393110 File Offset: 0x00393110
    public ChatHandOffs([Nullable(new byte[] { 1, 2 })] Func<string> accountId, string folder = null, Func<DateTimeOffset> clock = null)
    {
      this.accountId = accountId;
      Func<DateTimeOffset> func = clock;
      if (clock == null && (func = ChatHandOffs.<>c.<>9__22_0) == null)
      {
        func = (ChatHandOffs.<>c.<>9__22_0 = () => DateTimeOffset.UtcNow);
      }
      this.clock = func;
      this.folder = folder ?? AppPaths.Combine(new string[] { "HeyClickyHistory" });
    }

    // Token: 0x170003E9 RID: 1001
    // (get) Token: 0x060010B7 RID: 4279 RVA: 0x00393188 File Offset: 0x00393188
    [Nullable(1)]
    public IReadOnlyList<ChatHandOff> Entries
    {
      [NullableContext(1)]
      get
      {
        object obj = this.gate;
        IReadOnlyList<ChatHandOff> readOnlyList;
        lock (obj)
        {
          this.AdoptAccount(this.accountId());
          readOnlyList = this.entries.ToList<ChatHandOff>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x060010B8 RID: 4280 RVA: 0x003931E0 File Offset: 0x003931E0
    [NullableContext(1)]
    [return: Nullable(2)]
    public ChatHandOff Entry(string entryId)
    {
      object obj = this.gate;
      ChatHandOff chatHandOff;
      lock (obj)
      {
        chatHandOff = this.entries.FirstOrDefault<ChatHandOff>((ChatHandOff e) => e.Id == entryId);
      }
      return chatHandOff;
    }

    // Token: 0x060010B9 RID: 4281 RVA: 0x00393240 File Offset: 0x00393240
    [NullableContext(1)]
    public void NoteStartedJob(string jobId, string title)
    {
      this.Append(new ChatHandOff
      {
        Id = Guid.NewGuid().ToString(),
        Kind = ChatHandOffKind.Started,
        JobId = jobId,
        Title = title,
        CreatedAt = this.clock(),
        TaskTitle = title
      });
    }

    // Token: 0x060010BA RID: 4282 RVA: 0x003932A0 File Offset: 0x003932A0
    public void NoteStarted(string clickySlug, string threadId, bool isFollowUp, string taskTitle = null, bool isSentFromWindow = false, string sentText = null, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> sentFiles = null)
    {
      string text = ChatHandOffs.ShortTaskTitle(taskTitle);
      string text2 = (string.IsNullOrWhiteSpace(sentText) ? null : sentText.Trim());
      List<string> list = ((sentFiles != null && sentFiles.Count > 0) ? sentFiles.ToList<string>() : null);
      if (threadId != null && clickySlug == null)
      {
        IChatHandOffDirectory directory = this.Directory;
        ValueTuple<string, string>? valueTuple = ((directory != null) ? directory.JobForThread(threadId) : null);
        if (valueTuple != null)
        {
          ValueTuple<string, string> valueOrDefault = valueTuple.GetValueOrDefault();
          this.Append(new ChatHandOff
          {
            Id = Guid.NewGuid().ToString(),
            Kind = ChatHandOffKind.Started,
            JobId = valueOrDefault.Item1,
            Title = valueOrDefault.Item2,
            CreatedAt = this.clock(),
            IsFollowUp = isFollowUp,
            TaskTitle = (isFollowUp ? text : valueOrDefault.Item2),
            IsSentFromWindow = isSentFromWindow,
            SentText = text2,
            SentFiles = list
          });
          return;
        }
      }
      string text3 = clickySlug;
      if (clickySlug == null)
      {
        if (threadId != null)
        {
          IChatHandOffDirectory directory2 = this.Directory;
          text3 = ((directory2 != null) ? directory2.ClickySlugForThread(threadId) : null);
        }
        else
        {
          text3 = null;
        }
      }
      string text4 = text3;
      if (text4 != null)
      {
        IChatHandOffDirectory directory3 = this.Directory;
        string text5 = ((directory3 != null) ? directory3.ClickyName(text4) : null);
        if (text5 != null)
        {
          this.Append(new ChatHandOff
          {
            Id = Guid.NewGuid().ToString(),
            Kind = ChatHandOffKind.Started,
            ClickySlug = text4,
            Title = text5,
            CreatedAt = this.clock(),
            IsFollowUp = isFollowUp,
            TaskTitle = text,
            IsSentFromWindow = isSentFromWindow,
            SentText = text2,
            SentFiles = list
          });
          return;
        }
      }
    }

    // Token: 0x060010BB RID: 4283 RVA: 0x00393458 File Offset: 0x00393458
    public static string ShortTaskTitle(string text)
    {
      if (text == null)
      {
        return null;
      }
      string text2 = string.Join(" ", text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)).Trim();
      if (text2.Length == 0)
      {
        return null;
      }
      if (text2.Length <= 70)
      {
        return text2;
      }
      return text2.Substring(0, 69).Trim() + "…";
    }

    // Token: 0x060010BC RID: 4284 RVA: 0x003934C0 File Offset: 0x003934C0
    [NullableContext(1)]
    public void NoteTaskEnded(ChatTaskEnd task, ChatHandOffOutcome outcome)
    {
      if (task.IsDemo)
      {
        return;
      }
      string text;
      if ((text = task.JobId) == null)
      {
        if (task.ThreadId != null)
        {
          IChatHandOffDirectory directory = this.Directory;
          ValueTuple<string, string>? valueTuple;
          text = ((directory != null) ? ((directory.JobForThread(task.ThreadId) != null) ? valueTuple.GetValueOrDefault().Item1 : null) : null);
        }
        else
        {
          text = null;
        }
      }
      string text2 = text;
      object obj;
      if (text2 != null)
      {
        obj = null;
      }
      else if ((obj = task.ClickySlug) == null)
      {
        if (task.ThreadId != null)
        {
          IChatHandOffDirectory directory2 = this.Directory;
          obj = ((directory2 != null) ? directory2.ClickySlugForThread(task.ThreadId) : null);
        }
        else
        {
          obj = null;
        }
      }
      string text3 = obj;
      if (text2 == null && text3 == null)
      {
        return;
      }
      object obj2 = this.gate;
      bool flag = false;
      ChatHandOff chatHandOff4;
      try
      {
        Monitor.Enter(obj2, ref flag);
        this.AdoptAccount(this.accountId());
        string key = ChatHandOff.TargetKeyFor(text2, text3);
        ChatHandOff chatHandOff = this.entries.LastOrDefault<ChatHandOff>((ChatHandOff e) => e.TargetKey == key && e.Kind == ChatHandOffKind.Started);
        if (chatHandOff != null && this.clock() - chatHandOff.CreatedAt >= ChatHandOffs.ReturnWindow)
        {
          chatHandOff = null;
        }
        string text4;
        if ((text4 = ((chatHandOff != null) ? chatHandOff.Title : null)) == null)
        {
          string text5;
          if (text2 != null)
          {
            IChatHandOffDirectory directory3 = this.Directory;
            text5 = ((directory3 != null) ? directory3.JobTitle(text2) : null);
          }
          else
          {
            text5 = null;
          }
          if ((text4 = text5) == null)
          {
            string text6;
            if (text3 != null)
            {
              IChatHandOffDirectory directory4 = this.Directory;
              text6 = ((directory4 != null) ? directory4.ClickyName(text3) : null);
            }
            else
            {
              text6 = null;
            }
            text4 = text6 ?? "Your Clicky";
          }
        }
        string text7 = text4;
        bool flag2 = task.PendingComputerUseRequest != null || task.PendingUserStep != null || task.NeedsChromeConnection;
        ChatHandOffOutcome chatHandOffOutcome = ((outcome == ChatHandOffOutcome.Finished && flag2) ? ChatHandOffOutcome.NeedsYou : outcome);
        ChatHandOff chatHandOff2 = this.entries.LastOrDefault<ChatHandOff>((ChatHandOff e) => e.TargetKey == key);
        if (chatHandOff2 != null && chatHandOff2.Kind == ChatHandOffKind.CameBack && this.clock() - chatHandOff2.CreatedAt < TimeSpan.FromSeconds(3L))
        {
          return;
        }
        bool flag3 = chatHandOffOutcome == ChatHandOffOutcome.Finished;
        List<string> list = (flag3 ? ChatHandOffs.FollowUpsFrom(task.SuggestedNextActions) : new List<string>());
        ChatHandOff chatHandOff3 = new ChatHandOff();
        chatHandOff3.Id = Guid.NewGuid().ToString();
        chatHandOff3.Kind = ChatHandOffKind.CameBack;
        chatHandOff3.JobId = ((chatHandOff != null) ? chatHandOff.JobId : null) ?? text2;
        chatHandOff3.ClickySlug = ((chatHandOff != null) ? chatHandOff.ClickySlug : null) ?? text3;
        chatHandOff3.Title = text7;
        chatHandOff3.CreatedAt = this.clock();
        chatHandOff3.Outcome = new ChatHandOffOutcome?(chatHandOffOutcome);
        chatHandOff3.ReplyText = ChatHandOffs.ReplyText(task, chatHandOffOutcome);
        chatHandOff3.FullReply = (flag3 ? ChatHandOffs.FullReplyText(task) : null);
        chatHandOff3.ArtifactEntries = ((flag3 && task.UserFacingArtifactPaths.Count > 0) ? task.UserFacingArtifactPaths.ToList<string>() : null);
        chatHandOff3.ThreadId = task.ThreadId;
        chatHandOff3.IsRoutineRun = (task.IsScheduledRoutineRun ? new bool?(true) : null);
        chatHandOff3.Answer = (flag3 ? task.FinalAnswer : null);
        chatHandOff3.FollowUps = ((list.Count > 0) ? list : null);
        string text8;
        if ((text8 = ((chatHandOff != null) ? chatHandOff.TaskTitle : null)) == null)
        {
          string text9;
          if (text2 != null)
          {
            IChatHandOffDirectory directory5 = this.Directory;
            text9 = ((directory5 != null) ? directory5.JobTitle(text2) : null);
          }
          else
          {
            text9 = null;
          }
          text8 = text9 ?? ChatHandOffs.ShortTaskTitle(task.CompletedThreadName);
        }
        chatHandOff3.TaskTitle = text8;
        chatHandOff3.SitesRead = ((task.SitesRead.Count > 0) ? task.SitesRead.ToList<string>() : null);
        chatHandOff4 = chatHandOff3;
      }
      finally
      {
        if (flag)
        {
          Monitor.Exit(obj2);
        }
      }
      this.Append(chatHandOff4);
    }

    // Token: 0x060010BD RID: 4285 RVA: 0x00393874 File Offset: 0x00393874
    [NullableContext(1)]
    public IReadOnlyList<string> ShownFollowUps(string entryId)
    {
      object obj = this.gate;
      bool flag = false;
      IReadOnlyList<string> readOnlyList;
      try
      {
        Monitor.Enter(obj, ref flag);
        ChatHandOff entry = this.entries.FirstOrDefault<ChatHandOff>((ChatHandOff e) => e.Id == entryId);
        if (entry != null && entry.Kind == ChatHandOffKind.CameBack)
        {
          ChatHandOffOutcome? outcome = entry.Outcome;
          if (outcome != null && outcome.GetValueOrDefault() == ChatHandOffOutcome.Finished)
          {
            ChatHandOff chatHandOff = this.entries.LastOrDefault<ChatHandOff>((ChatHandOff e) => e.TargetKey == entry.TargetKey);
            if (((chatHandOff != null) ? chatHandOff.Id : null) != entry.Id)
            {
              return Array.Empty<string>();
            }
            return entry.FollowUps ?? Array.Empty<string>();
          }
        }
        readOnlyList = Array.Empty<string>();
      }
      finally
      {
        if (flag)
        {
          Monitor.Exit(obj);
        }
      }
      return readOnlyList;
    }

    // Token: 0x060010BE RID: 4286 RVA: 0x00393974 File Offset: 0x00393974
    [NullableContext(1)]
    public static List<string> FollowUpsFrom(IEnumerable<string> suggestedNextActions)
    {
      HashSet<string> hashSet = new HashSet<string>();
      List<string> list = new List<string>();
      char[] array = new char[] { ' ', '\t', '\n', '\r', '"', '\'', '“', '”', '.' };
      foreach (string text in suggestedNextActions)
      {
        string text2 = string.Join(" ", text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)).Trim(array);
        if (text2.Length != 0 && text2.Length <= 70 && hashSet.Add(text2.ToLowerInvariant()))
        {
          list.Add(text2);
          if (list.Count == 3)
          {
            break;
          }
        }
      }
      return list;
    }

    // Token: 0x060010BF RID: 4287 RVA: 0x00393A38 File Offset: 0x00393A38
    [NullableContext(1)]
    [return: Nullable(2)]
    public static string FullReplyText(ChatTaskEnd task)
    {
      List<string> list = task.CompletedAssistantMessages.ToList<string>();
      string finalResponseMarkdown = task.FinalResponseMarkdown;
      if (finalResponseMarkdown != null)
      {
        string text = finalResponseMarkdown.Trim();
        string text2 = list.LastOrDefault<string>();
        if (text != ((text2 != null) ? text2.Trim() : null))
        {
          list.Add(finalResponseMarkdown);
        }
      }
      return ChatHandOffs.FullReplyText(list);
    }

    // Token: 0x060010C0 RID: 4288 RVA: 0x00393A88 File Offset: 0x00393A88
    [NullableContext(1)]
    [return: Nullable(2)]
    public static string FullReplyText(IEnumerable<string> assistantMessages)
    {
      string text = string.Join("\n\n", from m in assistantMessages
        select m.Trim() into m
        where m.Length > 0
        select m);
      if (text.Length == 0)
      {
        return null;
      }
      if (text.Length <= 40000)
      {
        return text;
      }
      return text.Substring(0, 39999) + "…";
    }

    // Token: 0x060010C1 RID: 4289 RVA: 0x00393B18 File Offset: 0x00393B18
    [NullableContext(1)]
    [return: Nullable(2)]
    private static string ReplyText(ChatTaskEnd task, ChatHandOffOutcome outcome)
    {
      string[] array;
      if (outcome != ChatHandOffOutcome.Finished)
      {
        if (outcome == ChatHandOffOutcome.NeedsYou)
        {
          array = new string[]
          {
            task.PendingComputerUseRequest,
            task.PendingUserStep,
            task.NeedsChromeConnection ? "Needs your Chrome connected to carry on." : null,
            task.FinalSummary
          };
        }
        else
        {
          array = new string[] { task.FinalSummary };
        }
      }
      else
      {
        array = new string[] { task.FinalSummary, task.FinalResponseMarkdown };
      }
      string text = array.Select<string, string>(delegate(string c)
      {
        if (c == null)
        {
          return null;
        }
        return c.Trim();
      }).FirstOrDefault<string>((string c) => !string.IsNullOrEmpty(c));
      if (text == null)
      {
        return null;
      }
      if (text.Length <= 600)
      {
        return text;
      }
      return text.Substring(0, 599).Trim() + "…";
    }

    // Token: 0x060010C2 RID: 4290 RVA: 0x00393C08 File Offset: 0x00393C08
    public string LatestReturnEntryId(string jobId, string clickySlug)
    {
      string key = ChatHandOff.TargetKeyFor(jobId, clickySlug);
      object obj = this.gate;
      string text;
      lock (obj)
      {
        ChatHandOff chatHandOff = this.entries.LastOrDefault<ChatHandOff>((ChatHandOff e) => e.TargetKey == key && e.Kind == ChatHandOffKind.CameBack);
        text = ((chatHandOff != null) ? chatHandOff.Id : null);
      }
      return text;
    }

    // Token: 0x060010C3 RID: 4291 RVA: 0x00393C7C File Offset: 0x00393C7C
    public string LatestReplyEntryId(string jobId, string clickySlug)
    {
      string key = ChatHandOff.TargetKeyFor(jobId, clickySlug);
      object obj = this.gate;
      string text;
      lock (obj)
      {
        ChatHandOff chatHandOff = this.entries.LastOrDefault<ChatHandOff>(delegate(ChatHandOff e)
        {
          bool flag2 = e.TargetKey == key;
          if (flag2)
          {
            ChatHandOffKind kind = e.Kind;
            bool flag3 = kind <= ChatHandOffKind.CameBack;
            flag2 = flag3;
          }
          return flag2;
        });
        if (chatHandOff == null || chatHandOff.Kind != ChatHandOffKind.CameBack)
        {
          text = null;
        }
        else
        {
          string text2;
          if ((text2 = chatHandOff.FullReply) == null)
          {
            text2 = chatHandOff.ReplyText ?? "";
          }
          text = ((text2.Trim().Length == 0) ? null : chatHandOff.Id);
        }
      }
      return text;
    }

    // Token: 0x060010C4 RID: 4292 RVA: 0x00393D24 File Offset: 0x00393D24
    public string LatestEntryId(string jobId, string clickySlug)
    {
      string text = this.LatestReturnEntryId(jobId, clickySlug);
      if (text != null)
      {
        return text;
      }
      string key = ChatHandOff.TargetKeyFor(jobId, clickySlug);
      object obj = this.gate;
      string text2;
      lock (obj)
      {
        ChatHandOff chatHandOff = this.entries.LastOrDefault<ChatHandOff>((ChatHandOff e) => e.TargetKey == key);
        text2 = ((chatHandOff != null) ? chatHandOff.Id : null);
      }
      return text2;
    }

    // Token: 0x060010C5 RID: 4293 RVA: 0x00393DA8 File Offset: 0x00393DA8
    [NullableContext(1)]
    public void NoteRestartInterruption(string slug, [Nullable(2)] string threadId, DateTimeOffset? interruptedAt)
    {
      IChatHandOffDirectory directory = this.Directory;
      string text = ((directory != null) ? directory.ClickyName(slug) : null);
      if (text == null)
      {
        return;
      }
      object obj = this.gate;
      ChatHandOff chatHandOff;
      lock (obj)
      {
        chatHandOff = this.entries.LastOrDefault<ChatHandOff>((ChatHandOff e) => e.TargetKey == ChatHandOff.TargetKeyFor(null, slug) && e.Kind == ChatHandOffKind.Started);
      }
      this.Append(new ChatHandOff
      {
        Id = Guid.NewGuid().ToString(),
        Kind = ChatHandOffKind.CameBack,
        ClickySlug = slug,
        Title = (((chatHandOff != null) ? chatHandOff.Title : null) ?? text),
        CreatedAt = (interruptedAt ?? this.clock()),
        Outcome = new ChatHandOffOutcome?(ChatHandOffOutcome.Stopped),
        ReplyText = "HeyClicky restarted before this finished.",
        ThreadId = threadId,
        TaskTitle = ((chatHandOff != null) ? chatHandOff.TaskTitle : null)
      });
    }

    // Token: 0x060010C6 RID: 4294 RVA: 0x00393ECC File Offset: 0x00393ECC
    [NullableContext(1)]
    public bool NoteCard(JsonObject card, string threadId)
    {
      object obj = this.gate;
      lock (obj)
      {
        int num = this.entries.FindLastIndex((ChatHandOff e) => e.Kind == ChatHandOffKind.CameBack && e.ThreadId == threadId);
        if (num < 0 || this.clock() - this.entries[num].CreatedAt >= TimeSpan.FromDays(1))
        {
          return false;
        }
        List<ChatHandOff> list = this.entries;
        int num2 = num;
        ChatHandOff chatHandOff = this.entries[num].<Clone>$();
        chatHandOff.Card = card;
        list[num2] = chatHandOff;
        this.SaveToDisk();
      }
      this.RaiseChanged();
      return true;
    }

    // Token: 0x060010C7 RID: 4295 RVA: 0x00393F98 File Offset: 0x00393F98
    public JsonObject LatestCard(string jobId, string clickySlug)
    {
      string text = this.LatestReturnEntryId(jobId, clickySlug);
      if (text == null)
      {
        return null;
      }
      ChatHandOff chatHandOff = this.Entry(text);
      if (chatHandOff == null)
      {
        return null;
      }
      return chatHandOff.Card;
    }

    // Token: 0x060010C8 RID: 4296 RVA: 0x00393FC8 File Offset: 0x00393FC8
    [NullableContext(1)]
    public void NoteTextDelivered(string text, [Nullable(2)] string appName, bool wasCopied)
    {
      string text2 = text.Trim();
      if (text2.Length == 0)
      {
        return;
      }
      string text3 = ((text2.Length > 4000) ? (text2.Substring(0, 3999) + "…") : text2);
      this.Append(new ChatHandOff
      {
        Id = Guid.NewGuid().ToString(),
        Kind = ChatHandOffKind.Text,
        Title = (appName ?? ""),
        CreatedAt = this.clock(),
        DeliveredText = text3,
        DeliveredToApp = appName,
        WasCopied = (wasCopied ? new bool?(true) : null)
      });
    }

    // Token: 0x060010C9 RID: 4297 RVA: 0x00394080 File Offset: 0x00394080
    [NullableContext(1)]
    public void NoteDrawingKept(string lessonId, string title)
    {
      this.Append(new ChatHandOff
      {
        Id = Guid.NewGuid().ToString(),
        Kind = ChatHandOffKind.Drawing,
        Title = title,
        CreatedAt = this.clock(),
        LessonId = lessonId
      });
    }

    // Token: 0x060010CA RID: 4298 RVA: 0x003940D8 File Offset: 0x003940D8
    [NullableContext(1)]
    public void Remove(string entryId)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.entries.RemoveAll((ChatHandOff e) => e.Id == entryId) == 0)
        {
          return;
        }
        this.SaveToDisk();
      }
      this.RaiseChanged();
    }

    // Token: 0x060010CB RID: 4299 RVA: 0x00394148 File Offset: 0x00394148
    public void Reset()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.account = null;
        this.entries = new List<ChatHandOff>();
      }
      this.RaiseChanged();
    }

    // Token: 0x170003EA RID: 1002
    // (get) Token: 0x060010CC RID: 4300 RVA: 0x0039419C File Offset: 0x0039419C
    // (set) Token: 0x060010CD RID: 4301 RVA: 0x003941A4 File Offset: 0x003941A4
    [TupleElementNames(new string[] { "Slug", "ThreadId", "InterruptedAt" })]
    [Nullable(new byte[] { 2, 1, 0, 1, 2 })]
    public Func<IReadOnlyList<ValueTuple<string, string, DateTimeOffset?>>> TakeRestartInterruptions
    {
      [return: TupleElementNames(new string[] { "Slug", "ThreadId", "InterruptedAt" })]
      [return: Nullable(new byte[] { 2, 1, 0, 1, 2 })]
      get;
      [param: TupleElementNames(new string[] { "Slug", "ThreadId", "InterruptedAt" })]
      [param: Nullable(new byte[] { 2, 1, 0, 1, 2 })]
      set;
    }

    // Token: 0x060010CE RID: 4302 RVA: 0x003941B0 File Offset: 0x003941B0
    public void Load()
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        this.AdoptAccount(this.accountId());
        flag2 = this.account != null;
      }
      if (flag2)
      {
        Func<IReadOnlyList<ValueTuple<string, string, DateTimeOffset?>>> takeRestartInterruptions = this.TakeRestartInterruptions;
        IReadOnlyList<ValueTuple<string, string, DateTimeOffset?>> readOnlyList = ((takeRestartInterruptions != null) ? takeRestartInterruptions() : null);
        if (readOnlyList != null && readOnlyList.Count > 0)
        {
          foreach (ValueTuple<string, string, DateTimeOffset?> valueTuple in readOnlyList)
          {
            this.NoteRestartInterruption(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
          }
        }
      }
      this.RaiseChanged();
    }

    // Token: 0x060010CF RID: 4303 RVA: 0x00394284 File Offset: 0x00394284
    [NullableContext(1)]
    public void BackfillRepliesIfNeeded(IEnumerable<ChatHandOffs.BackfillTarget> targets, Func<string, IReadOnlyList<ChatHandOffs.FinishedTurn>> turns)
    {
      bool flag = false;
      object obj = this.gate;
      lock (obj)
      {
        this.AdoptAccount(this.accountId());
        if (this.account == null || this.backfilledAccount == this.account)
        {
          return;
        }
        this.backfilledAccount = this.account;
        foreach (ChatHandOffs.BackfillTarget backfillTarget in targets)
        {
          IReadOnlyList<ChatHandOffs.FinishedTurn> readOnlyList = turns(backfillTarget.ThreadId);
          if (readOnlyList.Count != 0)
          {
            IReadOnlyList<ChatHandOffs.FinishedTurn> readOnlyList2 = readOnlyList;
            ChatHandOffs.FinishedTurn finishedTurn = readOnlyList2[readOnlyList2.Count - 1];
            string key = ChatHandOff.TargetKeyFor(backfillTarget.JobId, backfillTarget.ClickySlug);
            List<int> list = (from p in this.entries.Select<ChatHandOff, ValueTuple<ChatHandOff, int>>((ChatHandOff e, int i) => new ValueTuple<ChatHandOff, int>(e, i))
              where p.Item1.TargetKey == key && p.Item1.Kind == ChatHandOffKind.CameBack
              select p.Item2).ToList<int>();
            foreach (int num in list)
            {
              ChatHandOff entry = this.entries[num];
              if (entry.FullReply == null && entry.Outcome.GetValueOrDefault() == ChatHandOffOutcome.Finished)
              {
                ChatHandOffs.FinishedTurn finishedTurn2 = (from t in readOnlyList
                  where t.FinishedAt != null
                  orderby Math.Abs((t.FinishedAt.Value - entry.CreatedAt).TotalSeconds)
                  select t).FirstOrDefault<ChatHandOffs.FinishedTurn>();
                ChatHandOffs.FinishedTurn finishedTurn3;
                if (finishedTurn2 == null || Math.Abs((finishedTurn2.FinishedAt.Value - entry.CreatedAt).TotalSeconds) > ChatHandOffs.TurnMatchTolerance.TotalSeconds)
                {
                  int num2 = num;
                  List<int> list2 = list;
                  finishedTurn3 = ((num2 == list2[list2.Count - 1]) ? finishedTurn : null);
                }
                else
                {
                  finishedTurn3 = finishedTurn2;
                }
                ChatHandOffs.FinishedTurn finishedTurn4 = finishedTurn3;
                if (finishedTurn4 != null)
                {
                  List<ChatHandOff> list3 = this.entries;
                  int num3 = num;
                  ChatHandOff chatHandOff = entry.<Clone>$();
                  chatHandOff.FullReply = ChatHandOffs.FullReplyText(finishedTurn4.AssistantMessages);
                  chatHandOff.ArtifactEntries = entry.ArtifactEntries ?? ((finishedTurn4.ArtifactEntries.Count > 0) ? finishedTurn4.ArtifactEntries : null);
                  chatHandOff.ThreadId = entry.ThreadId ?? backfillTarget.ThreadId;
                  chatHandOff.TaskTitle = entry.TaskTitle ?? backfillTarget.TaskTitle;
                  list3[num3] = chatHandOff;
                  flag = true;
                }
              }
            }
            if (list.Count == 0)
            {
              DateTimeOffset? finishedAt = finishedTurn.FinishedAt;
              if (finishedAt != null)
              {
                DateTimeOffset valueOrDefault = finishedAt.GetValueOrDefault();
                if (this.clock() - valueOrDefault < ChatHandOffs.BackfillWindow)
                {
                  this.entries.Add(new ChatHandOff
                  {
                    Id = Guid.NewGuid().ToString(),
                    Kind = ChatHandOffKind.CameBack,
                    JobId = backfillTarget.JobId,
                    ClickySlug = backfillTarget.ClickySlug,
                    Title = backfillTarget.Name,
                    CreatedAt = valueOrDefault,
                    Outcome = new ChatHandOffOutcome?(ChatHandOffOutcome.Finished),
                    FullReply = ChatHandOffs.FullReplyText(finishedTurn.AssistantMessages),
                    ArtifactEntries = ((finishedTurn.ArtifactEntries.Count > 0) ? finishedTurn.ArtifactEntries : null),
                    ThreadId = backfillTarget.ThreadId,
                    TaskTitle = backfillTarget.TaskTitle
                  });
                  flag = true;
                }
              }
            }
          }
        }
        if (!flag)
        {
          return;
        }
        this.entries = this.entries.OrderBy<ChatHandOff, DateTimeOffset>((ChatHandOff e) => e.CreatedAt).ToList<ChatHandOff>();
        if (this.entries.Count > 200)
        {
          this.entries.RemoveRange(0, this.entries.Count - 200);
        }
        this.SaveToDisk();
      }
      this.RaiseChanged();
    }

    // Token: 0x060010D0 RID: 4304 RVA: 0x00394744 File Offset: 0x00394744
    [NullableContext(1)]
    private void Append(ChatHandOff entry)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.AdoptAccount(this.accountId());
        if (this.account == null)
        {
          return;
        }
        this.entries.Add(entry);
        if (this.entries.Count > 200)
        {
          this.entries.RemoveRange(0, this.entries.Count - 200);
        }
        this.SaveToDisk();
      }
      this.RaiseChanged();
      Action refresh = this.RefreshHistorySoon;
      if (refresh != null)
      {
        Task.Delay(TimeSpan.FromSeconds(3L)).ContinueWith(delegate(Task _)
        {
          refresh();
        }, TaskScheduler.Default);
      }
    }

    // Token: 0x060010D1 RID: 4305 RVA: 0x00394820 File Offset: 0x00394820
    private void AdoptAccount(string next)
    {
      if (next == this.account)
      {
        return;
      }
      this.account = next;
      this.entries = new List<ChatHandOff>();
      string filePath = this.FilePath;
      if (filePath == null || !File.Exists(filePath))
      {
        return;
      }
      try
      {
        this.entries = JsonSerializer.Deserialize<List<ChatHandOff>>(File.ReadAllBytes(filePath), NotchChatJsonContext.Default.ListChatHandOff) ?? new List<ChatHandOff>();
      }
      catch (Exception ex)
      {
        Log.Warn("ChatHandOffs", "saved entries unreadable: " + ex.Message);
      }
    }

    // Token: 0x170003EB RID: 1003
    // (get) Token: 0x060010D2 RID: 4306 RVA: 0x003948BC File Offset: 0x003948BC
    private string FilePath
    {
      get
      {
        if (string.IsNullOrEmpty(this.account))
        {
          return null;
        }
        string text = new string(this.account.Where<char>((char c) => char.IsLetterOrDigit(c) || c == '-').ToArray<char>());
        if (text.Length != 0)
        {
          return Path.Combine(this.folder, text + "-handoffs.json");
        }
        return null;
      }
    }

    // Token: 0x060010D3 RID: 4307 RVA: 0x00394930 File Offset: 0x00394930
    private void SaveToDisk()
    {
      string path = this.FilePath;
      if (path == null)
      {
        return;
      }
      byte[] bytes = JsonSerializer.SerializeToUtf8Bytes<List<ChatHandOff>>(this.entries, NotchChatJsonContext.Default.ListChatHandOff);
      Task.Run(delegate
      {
        try
        {
          global::System.IO.Directory.CreateDirectory(Path.GetDirectoryName(path));
          string text = path + ".tmp";
          File.WriteAllBytes(text, bytes);
          File.Move(text, path, true);
        }
        catch (Exception ex)
        {
          Log.Warn("ChatHandOffs", "write failed: " + ex.Message);
        }
      });
    }

    // Token: 0x060010D4 RID: 4308 RVA: 0x00394988 File Offset: 0x00394988
    private void RaiseChanged()
    {
      try
      {
        Action changed = this.Changed;
        if (changed != null)
        {
          changed();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("ChatHandOffs", "Changed handler threw: " + ex.Message);
      }
    }

    // Token: 0x04000728 RID: 1832
    private const int KeptEntryLimit = 200;

    // Token: 0x04000729 RID: 1833
    private static readonly TimeSpan ReturnWindow = TimeSpan.FromDays(3);

    // Token: 0x0400072A RID: 1834
    private static readonly TimeSpan BackfillWindow = TimeSpan.FromDays(7);

    // Token: 0x0400072B RID: 1835
    private static readonly TimeSpan TurnMatchTolerance = TimeSpan.FromMinutes(20L);

    // Token: 0x0400072C RID: 1836
    [Nullable(1)]
    private readonly object gate = new object();

    // Token: 0x0400072D RID: 1837
    [Nullable(new byte[] { 1, 2 })]
    private readonly Func<string> accountId;

    // Token: 0x0400072E RID: 1838
    [Nullable(1)]
    private readonly Func<DateTimeOffset> clock;

    // Token: 0x0400072F RID: 1839
    [Nullable(1)]
    private readonly string folder;

    // Token: 0x04000730 RID: 1840
    [Nullable(1)]
    private List<ChatHandOff> entries = new List<ChatHandOff>();

    // Token: 0x04000731 RID: 1841
    private string account;

    // Token: 0x04000732 RID: 1842
    private string backfilledAccount;

    // Token: 0x0200041C RID: 1052
    [Nullable(0)]
    public sealed class BackfillTarget : IEquatable<ChatHandOffs.BackfillTarget>
    {
      // Token: 0x06002C53 RID: 11347 RVA: 0x0042BCA8 File Offset: 0x0042BCA8
      public BackfillTarget(string JobId, string ClickySlug, [Nullable(1)] string ThreadId, [Nullable(1)] string Name, string TaskTitle)
      {
        this.JobId = JobId;
        this.ClickySlug = ClickySlug;
        this.ThreadId = ThreadId;
        this.Name = Name;
        this.TaskTitle = TaskTitle;
        base..ctor();
      }

      // Token: 0x1700083B RID: 2107
      // (get) Token: 0x06002C54 RID: 11348 RVA: 0x0042BCD8 File Offset: 0x0042BCD8
      [Nullable(1)]
      [CompilerGenerated]
      private Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(ChatHandOffs.BackfillTarget);
        }
      }

      // Token: 0x1700083C RID: 2108
      // (get) Token: 0x06002C55 RID: 11349 RVA: 0x0042BCE4 File Offset: 0x0042BCE4
      // (set) Token: 0x06002C56 RID: 11350 RVA: 0x0042BCEC File Offset: 0x0042BCEC
      public string JobId { get; set; }

      // Token: 0x1700083D RID: 2109
      // (get) Token: 0x06002C57 RID: 11351 RVA: 0x0042BCF8 File Offset: 0x0042BCF8
      // (set) Token: 0x06002C58 RID: 11352 RVA: 0x0042BD00 File Offset: 0x0042BD00
      public string ClickySlug { get; set; }

      // Token: 0x1700083E RID: 2110
      // (get) Token: 0x06002C59 RID: 11353 RVA: 0x0042BD0C File Offset: 0x0042BD0C
      // (set) Token: 0x06002C5A RID: 11354 RVA: 0x0042BD14 File Offset: 0x0042BD14
      [Nullable(1)]
      public string ThreadId
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x1700083F RID: 2111
      // (get) Token: 0x06002C5B RID: 11355 RVA: 0x0042BD20 File Offset: 0x0042BD20
      // (set) Token: 0x06002C5C RID: 11356 RVA: 0x0042BD28 File Offset: 0x0042BD28
      [Nullable(1)]
      public string Name
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      }

      // Token: 0x17000840 RID: 2112
      // (get) Token: 0x06002C5D RID: 11357 RVA: 0x0042BD34 File Offset: 0x0042BD34
      // (set) Token: 0x06002C5E RID: 11358 RVA: 0x0042BD3C File Offset: 0x0042BD3C
      public string TaskTitle { get; set; }

      // Token: 0x06002C5F RID: 11359 RVA: 0x0042BD48 File Offset: 0x0042BD48
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("BackfillTarget");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002C60 RID: 11360 RVA: 0x0042BD94 File Offset: 0x0042BD94
      [NullableContext(1)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("JobId = ");
        builder.Append(this.JobId);
        builder.Append(", ClickySlug = ");
        builder.Append(this.ClickySlug);
        builder.Append(", ThreadId = ");
        builder.Append(this.ThreadId);
        builder.Append(", Name = ");
        builder.Append(this.Name);
        builder.Append(", TaskTitle = ");
        builder.Append(this.TaskTitle);
        return true;
      }

      // Token: 0x06002C61 RID: 11361 RVA: 0x0042BE24 File Offset: 0x0042BE24
      [CompilerGenerated]
      public static bool operator !=(ChatHandOffs.BackfillTarget left, ChatHandOffs.BackfillTarget right)
      {
        return !(left == right);
      }

      // Token: 0x06002C62 RID: 11362 RVA: 0x0042BE30 File Offset: 0x0042BE30
      [CompilerGenerated]
      public static bool operator ==(ChatHandOffs.BackfillTarget left, ChatHandOffs.BackfillTarget right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002C63 RID: 11363 RVA: 0x0042BE44 File Offset: 0x0042BE44
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TaskTitle>k__BackingField);
      }

      // Token: 0x06002C64 RID: 11364 RVA: 0x0042BED4 File Offset: 0x0042BED4
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChatHandOffs.BackfillTarget);
      }

      // Token: 0x06002C65 RID: 11365 RVA: 0x0042BEE4 File Offset: 0x0042BEE4
      [CompilerGenerated]
      public bool Equals(ChatHandOffs.BackfillTarget other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TaskTitle>k__BackingField, other.<TaskTitle>k__BackingField));
      }

      // Token: 0x06002C67 RID: 11367 RVA: 0x0042BF94 File Offset: 0x0042BF94
      [CompilerGenerated]
      private BackfillTarget([Nullable(1)] ChatHandOffs.BackfillTarget original)
      {
        this.JobId = original.<JobId>k__BackingField;
        this.ClickySlug = original.<ClickySlug>k__BackingField;
        this.ThreadId = original.<ThreadId>k__BackingField;
        this.Name = original.<Name>k__BackingField;
        this.TaskTitle = original.<TaskTitle>k__BackingField;
      }

      // Token: 0x06002C68 RID: 11368 RVA: 0x0042BFE4 File Offset: 0x0042BFE4
      [CompilerGenerated]
      public void Deconstruct(out string JobId, out string ClickySlug, [Nullable(1)] out string ThreadId, [Nullable(1)] out string Name, out string TaskTitle)
      {
        JobId = this.JobId;
        ClickySlug = this.ClickySlug;
        ThreadId = this.ThreadId;
        Name = this.Name;
        TaskTitle = this.TaskTitle;
      }
    }

    // Token: 0x0200041D RID: 1053
    [NullableContext(1)]
    [Nullable(0)]
    public sealed class FinishedTurn : IEquatable<ChatHandOffs.FinishedTurn>
    {
      // Token: 0x06002C69 RID: 11369 RVA: 0x0042C010 File Offset: 0x0042C010
      public FinishedTurn(DateTimeOffset? FinishedAt, IReadOnlyList<string> AssistantMessages, IReadOnlyList<string> ArtifactEntries)
      {
        this.FinishedAt = FinishedAt;
        this.AssistantMessages = AssistantMessages;
        this.ArtifactEntries = ArtifactEntries;
        base..ctor();
      }

      // Token: 0x17000841 RID: 2113
      // (get) Token: 0x06002C6A RID: 11370 RVA: 0x0042C030 File Offset: 0x0042C030
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ChatHandOffs.FinishedTurn);
        }
      }

      // Token: 0x17000842 RID: 2114
      // (get) Token: 0x06002C6B RID: 11371 RVA: 0x0042C03C File Offset: 0x0042C03C
      // (set) Token: 0x06002C6C RID: 11372 RVA: 0x0042C044 File Offset: 0x0042C044
      public DateTimeOffset? FinishedAt { get; set; }

      // Token: 0x17000843 RID: 2115
      // (get) Token: 0x06002C6D RID: 11373 RVA: 0x0042C050 File Offset: 0x0042C050
      // (set) Token: 0x06002C6E RID: 11374 RVA: 0x0042C058 File Offset: 0x0042C058
      public IReadOnlyList<string> AssistantMessages { get; set; }

      // Token: 0x17000844 RID: 2116
      // (get) Token: 0x06002C6F RID: 11375 RVA: 0x0042C064 File Offset: 0x0042C064
      // (set) Token: 0x06002C70 RID: 11376 RVA: 0x0042C06C File Offset: 0x0042C06C
      public IReadOnlyList<string> ArtifactEntries { get; set; }

      // Token: 0x06002C71 RID: 11377 RVA: 0x0042C078 File Offset: 0x0042C078
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("FinishedTurn");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002C72 RID: 11378 RVA: 0x0042C0C4 File Offset: 0x0042C0C4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("FinishedAt = ");
        builder.Append(this.FinishedAt.ToString());
        builder.Append(", AssistantMessages = ");
        builder.Append(this.AssistantMessages);
        builder.Append(", ArtifactEntries = ");
        builder.Append(this.ArtifactEntries);
        return true;
      }

      // Token: 0x06002C73 RID: 11379 RVA: 0x0042C130 File Offset: 0x0042C130
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ChatHandOffs.FinishedTurn left, ChatHandOffs.FinishedTurn right)
      {
        return !(left == right);
      }

      // Token: 0x06002C74 RID: 11380 RVA: 0x0042C13C File Offset: 0x0042C13C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ChatHandOffs.FinishedTurn left, ChatHandOffs.FinishedTurn right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002C75 RID: 11381 RVA: 0x0042C150 File Offset: 0x0042C150
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<FinishedAt>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<AssistantMessages>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ArtifactEntries>k__BackingField);
      }

      // Token: 0x06002C76 RID: 11382 RVA: 0x0042C1B4 File Offset: 0x0042C1B4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChatHandOffs.FinishedTurn);
      }

      // Token: 0x06002C77 RID: 11383 RVA: 0x0042C1C4 File Offset: 0x0042C1C4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ChatHandOffs.FinishedTurn other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<FinishedAt>k__BackingField, other.<FinishedAt>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<AssistantMessages>k__BackingField, other.<AssistantMessages>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ArtifactEntries>k__BackingField, other.<ArtifactEntries>k__BackingField));
      }

      // Token: 0x06002C79 RID: 11385 RVA: 0x0042C240 File Offset: 0x0042C240
      [CompilerGenerated]
      private FinishedTurn(ChatHandOffs.FinishedTurn original)
      {
        this.FinishedAt = original.<FinishedAt>k__BackingField;
        this.AssistantMessages = original.<AssistantMessages>k__BackingField;
        this.ArtifactEntries = original.<ArtifactEntries>k__BackingField;
      }

      // Token: 0x06002C7A RID: 11386 RVA: 0x0042C26C File Offset: 0x0042C26C
      [CompilerGenerated]
      public void Deconstruct(out DateTimeOffset? FinishedAt, out IReadOnlyList<string> AssistantMessages, out IReadOnlyList<string> ArtifactEntries)
      {
        FinishedAt = this.FinishedAt;
        AssistantMessages = this.AssistantMessages;
        ArtifactEntries = this.ArtifactEntries;
      }
    }
  }
}
