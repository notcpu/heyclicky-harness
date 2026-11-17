using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000118 RID: 280
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class NotchTabs
  {
    // Token: 0x17000442 RID: 1090
    // (get) Token: 0x0600124E RID: 4686 RVA: 0x003A1090 File Offset: 0x003A1090
    // (set) Token: 0x0600124F RID: 4687 RVA: 0x003A1098 File Offset: 0x003A1098
    [Nullable(new byte[] { 2, 1, 2 })]
    public Func<string, ChatHandOff> EntryLookup
    {
      [return: Nullable(new byte[] { 2, 1, 2 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 2 })]
      set;
    }

    // Token: 0x17000443 RID: 1091
    // (get) Token: 0x06001250 RID: 4688 RVA: 0x003A10A4 File Offset: 0x003A10A4
    // (set) Token: 0x06001251 RID: 4689 RVA: 0x003A10AC File Offset: 0x003A10AC
    [Nullable(new byte[] { 2, 1 })]
    public Func<string, bool> OpensFullReplyInWindow
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x1400002D RID: 45
    // (add) Token: 0x06001252 RID: 4690 RVA: 0x003A10B8 File Offset: 0x003A10B8
    // (remove) Token: 0x06001253 RID: 4691 RVA: 0x003A10F0 File Offset: 0x003A10F0
    public event Action Changed;

    // Token: 0x1400002E RID: 46
    // (add) Token: 0x06001254 RID: 4692 RVA: 0x003A1128 File Offset: 0x003A1128
    // (remove) Token: 0x06001255 RID: 4693 RVA: 0x003A1160 File Offset: 0x003A1160
    public event Action<NotchTab> SelectionChanged;

    // Token: 0x1400002F RID: 47
    // (add) Token: 0x06001256 RID: 4694 RVA: 0x003A1198 File Offset: 0x003A1198
    // (remove) Token: 0x06001257 RID: 4695 RVA: 0x003A11D0 File Offset: 0x003A11D0
    public event Action ComposerFocusRequested;

    // Token: 0x06001258 RID: 4696 RVA: 0x003A1208 File Offset: 0x003A1208
    [NullableContext(1)]
    internal NotchTabs(NotchPresenter presenter, object gate)
    {
      this.presenter = presenter;
      this.gate = gate;
    }

    // Token: 0x17000444 RID: 1092
    // (get) Token: 0x06001259 RID: 4697 RVA: 0x003A1220 File Offset: 0x003A1220
    public NotchTab Selected
    {
      get
      {
        object obj = this.gate;
        NotchTab notchTab;
        lock (obj)
        {
          notchTab = this.selected;
        }
        return notchTab;
      }
    }

    // Token: 0x17000445 RID: 1093
    // (get) Token: 0x0600125A RID: 4698 RVA: 0x003A1264 File Offset: 0x003A1264
    // (set) Token: 0x0600125B RID: 4699 RVA: 0x003A12A8 File Offset: 0x003A12A8
    public string ChatFocusEntryId
    {
      get
      {
        object obj = this.gate;
        string text;
        lock (obj)
        {
          text = this.chatFocusEntryId;
        }
        return text;
      }
      set
      {
        this.Mutate(delegate
        {
          this.chatFocusEntryId = value;
        });
      }
    }

    // Token: 0x17000446 RID: 1094
    // (get) Token: 0x0600125C RID: 4700 RVA: 0x003A12DC File Offset: 0x003A12DC
    // (set) Token: 0x0600125D RID: 4701 RVA: 0x003A1320 File Offset: 0x003A1320
    public string ExpandedReplyEntryId
    {
      get
      {
        object obj = this.gate;
        string text;
        lock (obj)
        {
          text = this.expandedReplyEntryId;
        }
        return text;
      }
      set
      {
        this.Mutate(delegate
        {
          this.SetExpandedReply(value);
        });
      }
    }

    // Token: 0x17000447 RID: 1095
    // (get) Token: 0x0600125E RID: 4702 RVA: 0x003A1354 File Offset: 0x003A1354
    // (set) Token: 0x0600125F RID: 4703 RVA: 0x003A1398 File Offset: 0x003A1398
    public double? FullReplyContentHeight
    {
      get
      {
        object obj = this.gate;
        double? num;
        lock (obj)
        {
          num = this.fullReplyContentHeight;
        }
        return num;
      }
      set
      {
        this.Mutate(delegate
        {
          this.fullReplyContentHeight = value;
        });
      }
    }

    // Token: 0x17000448 RID: 1096
    // (get) Token: 0x06001260 RID: 4704 RVA: 0x003A13CC File Offset: 0x003A13CC
    // (set) Token: 0x06001261 RID: 4705 RVA: 0x003A1410 File Offset: 0x003A1410
    public NotchSettingsSection SettingsSection
    {
      get
      {
        object obj = this.gate;
        NotchSettingsSection notchSettingsSection;
        lock (obj)
        {
          notchSettingsSection = this.settingsSection;
        }
        return notchSettingsSection;
      }
      set
      {
        this.Mutate(delegate
        {
          this.settingsSection = value;
        });
      }
    }

    // Token: 0x17000449 RID: 1097
    // (get) Token: 0x06001262 RID: 4706 RVA: 0x003A1444 File Offset: 0x003A1444
    public string DocumentPath
    {
      get
      {
        object obj = this.gate;
        string text;
        lock (obj)
        {
          text = this.documentPath;
        }
        return text;
      }
    }

    // Token: 0x1700044A RID: 1098
    // (get) Token: 0x06001263 RID: 4707 RVA: 0x003A1488 File Offset: 0x003A1488
    public string DocumentThreadId
    {
      get
      {
        object obj = this.gate;
        string text;
        lock (obj)
        {
          text = this.documentThreadId;
        }
        return text;
      }
    }

    // Token: 0x1700044B RID: 1099
    // (get) Token: 0x06001264 RID: 4708 RVA: 0x003A14CC File Offset: 0x003A14CC
    public NotchTab TabBeforeDocument
    {
      get
      {
        object obj = this.gate;
        NotchTab notchTab;
        lock (obj)
        {
          notchTab = this.tabBeforeDocument;
        }
        return notchTab;
      }
    }

    // Token: 0x1700044C RID: 1100
    // (get) Token: 0x06001265 RID: 4709 RVA: 0x003A1510 File Offset: 0x003A1510
    public int ComposerFocusRequestId
    {
      get
      {
        object obj = this.gate;
        int num;
        lock (obj)
        {
          num = this.composerFocusRequestId;
        }
        return num;
      }
    }

    // Token: 0x1700044D RID: 1101
    // (get) Token: 0x06001266 RID: 4710 RVA: 0x003A1554 File Offset: 0x003A1554
    public bool HasPendingComposerFocusRequest
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.hasPendingComposerFocusRequest;
        }
        return flag2;
      }
    }

    // Token: 0x06001267 RID: 4711 RVA: 0x003A1598 File Offset: 0x003A1598
    private void SetExpandedReply(string value)
    {
      if (this.expandedReplyEntryId != value)
      {
        this.fullReplyContentHeight = null;
      }
      this.expandedReplyEntryId = value;
    }

    // Token: 0x06001268 RID: 4712 RVA: 0x003A15BC File Offset: 0x003A15BC
    [NullableContext(1)]
    private void Mutate(Action change)
    {
      object obj = this.gate;
      NotchTab notchTab;
      NotchTab notchTab2;
      lock (obj)
      {
        notchTab = this.selected;
        change();
        notchTab2 = this.selected;
      }
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      if (notchTab != notchTab2)
      {
        Action<NotchTab> selectionChanged = this.SelectionChanged;
        if (selectionChanged == null)
        {
          return;
        }
        selectionChanged(notchTab2);
      }
    }

    // Token: 0x06001269 RID: 4713 RVA: 0x003A1630 File Offset: 0x003A1630
    private void SelectCore(NotchTab tab)
    {
      if (this.selected == tab)
      {
        return;
      }
      this.selected = tab;
      if (tab != NotchTab.Chat)
      {
        this.SetExpandedReply(null);
        this.tabBeforeFullReply = null;
      }
      string text = "notch_peek_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "tab_" + tab.RawValue();
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
    }

    // Token: 0x0600126A RID: 4714 RVA: 0x003A16A0 File Offset: 0x003A16A0
    public void Select(NotchTab tab)
    {
      this.Mutate(delegate
      {
        this.SelectCore(tab);
      });
    }

    // Token: 0x0600126B RID: 4715 RVA: 0x003A16D4 File Offset: 0x003A16D4
    public void Show(NotchTab tab, string focusingEntryId = null)
    {
      this.Mutate(delegate
      {
        this.ShowCore(tab, focusingEntryId);
      });
    }

    // Token: 0x0600126C RID: 4716 RVA: 0x003A1710 File Offset: 0x003A1710
    private void ShowCore(NotchTab tab, string focusingEntryId)
    {
      this.pendingTab = new NotchTab?(tab);
      this.pendingExpandedReplyEntryId = null;
      this.tabBeforeFullReply = null;
      this.chatFocusEntryId = focusingEntryId;
      this.SetExpandedReply(null);
      this.SelectCore(tab);
    }

    // Token: 0x0600126D RID: 4717 RVA: 0x003A1748 File Offset: 0x003A1748
    public void RequestComposerFocus()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.hasPendingComposerFocusRequest = true;
        this.composerFocusRequestId++;
      }
      Action composerFocusRequested = this.ComposerFocusRequested;
      if (composerFocusRequested != null)
      {
        composerFocusRequested();
      }
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x0600126E RID: 4718 RVA: 0x003A17B8 File Offset: 0x003A17B8
    public void ConsumeComposerFocusRequest()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.hasPendingComposerFocusRequest = false;
      }
    }

    // Token: 0x0600126F RID: 4719 RVA: 0x003A17FC File Offset: 0x003A17FC
    public void ShowChatForTyping(bool isNotchOpen)
    {
      this.Mutate(delegate
      {
        if (isNotchOpen)
        {
          this.chatFocusEntryId = null;
          this.SetExpandedReply(null);
          this.tabBeforeFullReply = null;
          this.SelectCore(NotchTab.Chat);
          return;
        }
        this.ShowCore(NotchTab.Chat, null);
      });
    }

    // Token: 0x06001270 RID: 4720 RVA: 0x003A1830 File Offset: 0x003A1830
    public void ShowSettings(NotchSettingsSection section)
    {
      this.Mutate(delegate
      {
        this.settingsSection = section;
        this.ShowCore(NotchTab.Settings, null);
      });
      this.presenter.OpenNotchAgentPeekOnRequest();
    }

    // Token: 0x06001271 RID: 4721 RVA: 0x003A1870 File Offset: 0x003A1870
    [NullableContext(1)]
    public void ShowDocument(string path, [Nullable(2)] string threadId)
    {
      this.Mutate(delegate
      {
        if (this.selected != NotchTab.Document)
        {
          bool isShowingNotchAgentPeekUnlocked = this.presenter.IsShowingNotchAgentPeekUnlocked;
          this.tabBeforeDocument = (isShowingNotchAgentPeekUnlocked ? this.selected : NotchTab.Home);
          NotchTabs <>4__this = this;
          ValueTuple<string, NotchTab?>? valueTuple;
          if (isShowingNotchAgentPeekUnlocked && this.selected == NotchTab.Chat)
          {
            string text4 = this.expandedReplyEntryId;
            if (text4 != null)
            {
              valueTuple = new ValueTuple<string, NotchTab?>?(new ValueTuple<string, NotchTab?>(text4, this.tabBeforeFullReply));
              goto IL_0082;
            }
          }
          valueTuple = null;
          IL_0082:
          <>4__this.expandedReplyBeforeDocument = valueTuple;
        }
        this.documentPath = path;
        this.documentThreadId = threadId;
        this.ShowCore(NotchTab.Document, null);
      });
      this.presenter.OpenNotchDocument();
      string text = "notch_peek_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "document_opened";
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
    }

    // Token: 0x06001272 RID: 4722 RVA: 0x003A18E4 File Offset: 0x003A18E4
    public void CloseDocument()
    {
      this.Mutate(delegate
      {
        if (this.selected == NotchTab.Document)
        {
          this.SelectCore((this.tabBeforeDocument == NotchTab.Document) ? NotchTab.Home : this.tabBeforeDocument);
          if (this.selected == NotchTab.Chat)
          {
            ValueTuple<string, NotchTab?>? valueTuple = this.expandedReplyBeforeDocument;
            if (valueTuple != null)
            {
              ValueTuple<string, NotchTab?> valueOrDefault = valueTuple.GetValueOrDefault();
              this.SetExpandedReply(valueOrDefault.Item1);
              this.tabBeforeFullReply = valueOrDefault.Item2;
            }
          }
        }
        this.expandedReplyBeforeDocument = null;
      });
      this.presenter.CloseNotchDocument();
    }

    // Token: 0x1700044E RID: 1102
    // (get) Token: 0x06001273 RID: 4723 RVA: 0x003A1904 File Offset: 0x003A1904
    public bool IsShowingDocument
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.selected == NotchTab.Document && this.presenter.IsNotchDocumentOpen && this.presenter.IsShowingNotchAgentPeekUnlocked;
        }
        return flag2;
      }
    }

    // Token: 0x06001274 RID: 4724 RVA: 0x003A1964 File Offset: 0x003A1964
    public void DropDocumentIfShowing()
    {
      this.Mutate(new Action(this.DropDocumentIfShowingCore));
    }

    // Token: 0x06001275 RID: 4725 RVA: 0x003A1978 File Offset: 0x003A1978
    internal void DropDocumentIfShowingCore()
    {
      if (this.selected == NotchTab.Document)
      {
        string text = this.documentPath;
        if (text != null && !this.forgetsReplyAndDocumentOnClose)
        {
          this.documentLeftOnClose = new NotchTabs.RememberedDocument(text, this.documentThreadId, this.tabBeforeDocument, this.expandedReplyBeforeDocument);
        }
      }
      this.expandedReplyBeforeDocument = null;
      if (this.selected != NotchTab.Document)
      {
        return;
      }
      this.selected = ((this.tabBeforeDocument == NotchTab.Document) ? NotchTab.Home : this.tabBeforeDocument);
    }

    // Token: 0x06001276 RID: 4726 RVA: 0x003A19EC File Offset: 0x003A19EC
    public void ForgetReplyAndDocumentOnNextClose()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.forgetsReplyAndDocumentOnClose = true;
      }
    }

    // Token: 0x06001277 RID: 4727 RVA: 0x003A1A30 File Offset: 0x003A1A30
    internal void NoteNotchClosedCore()
    {
      if (this.forgetsReplyAndDocumentOnClose)
      {
        this.forgetsReplyAndDocumentOnClose = false;
        string text = this.expandedReplyEntryId;
        if (text != null)
        {
          this.TrackFullReplyClosed(text, NotchChatFullReplyCloseVia.ExpandedToWindow);
        }
        this.SetExpandedReply(null);
        this.tabBeforeFullReply = null;
        this.documentLeftOnClose = null;
        this.rememberedPlace = new NotchTabs.RememberedPlace(NotchTab.Home, null, null, null, this.presenter.Now);
        this.pendingTab = null;
        this.pendingExpandedReplyEntryId = null;
        return;
      }
      NotchTabs.RememberedDocument rememberedDocument;
      if ((rememberedDocument = this.documentLeftOnClose) == null)
      {
        if (this.selected == NotchTab.Document)
        {
          string text2 = this.documentPath;
          if (text2 != null)
          {
            rememberedDocument = new NotchTabs.RememberedDocument(text2, this.documentThreadId, this.tabBeforeDocument, this.expandedReplyBeforeDocument);
            goto IL_00A9;
          }
        }
        rememberedDocument = null;
      }
      IL_00A9:
      NotchTabs.RememberedDocument rememberedDocument2 = rememberedDocument;
      this.documentLeftOnClose = null;
      this.rememberedPlace = new NotchTabs.RememberedPlace((rememberedDocument2 != null) ? NotchTab.Document : this.selected, (this.selected == NotchTab.Chat) ? this.expandedReplyEntryId : null, (this.selected == NotchTab.Chat) ? this.tabBeforeFullReply : null, rememberedDocument2, this.presenter.Now);
      this.pendingTab = null;
      this.pendingExpandedReplyEntryId = null;
    }

    // Token: 0x06001278 RID: 4728 RVA: 0x003A1B50 File Offset: 0x003A1B50
    public void ResetToHome()
    {
      this.Mutate(delegate
      {
        this.rememberedPlace = null;
        this.documentLeftOnClose = null;
        this.forgetsReplyAndDocumentOnClose = false;
        this.pendingTab = null;
        this.pendingExpandedReplyEntryId = null;
        this.chatFocusEntryId = null;
        this.SetExpandedReply(null);
        this.tabBeforeFullReply = null;
        this.expandedReplyBeforeDocument = null;
        this.settingsSection = NotchSettingsSection.Account;
        this.selected = NotchTab.Home;
      });
    }

    // Token: 0x06001279 RID: 4729 RVA: 0x003A1B64 File Offset: 0x003A1B64
    [NullableContext(1)]
    public void ShowFullReply(string entryId, NotchChatFullReplyOpenSource? from = null)
    {
      Func<string, bool> opensFullReplyInWindow = this.OpensFullReplyInWindow;
      if (opensFullReplyInWindow != null && opensFullReplyInWindow(entryId))
      {
        return;
      }
      bool isShowingNotchAgentPeek = this.presenter.IsShowingNotchAgentPeek;
      this.TrackFullReplyOpened(entryId, from ?? (isShowingNotchAgentPeek ? NotchChatFullReplyOpenSource.ArcCard : NotchChatFullReplyOpenSource.NotchCard));
      this.Mutate(delegate
      {
        bool isShowingNotchAgentPeekUnlocked = this.presenter.IsShowingNotchAgentPeekUnlocked;
        NotchTab? notchTab = ((!isShowingNotchAgentPeekUnlocked) ? new NotchTab?(NotchTab.Home) : ((this.selected == NotchTab.Chat) ? null : new NotchTab?((this.selected == NotchTab.Document) ? NotchTab.Home : this.selected)));
        this.pendingTab = (isShowingNotchAgentPeekUnlocked ? null : new NotchTab?(NotchTab.Chat));
        this.pendingExpandedReplyEntryId = (isShowingNotchAgentPeekUnlocked ? null : entryId);
        this.chatFocusEntryId = null;
        this.SelectCore(NotchTab.Chat);
        this.tabBeforeFullReply = notchTab;
        this.SetExpandedReply(entryId);
      });
      this.presenter.OpenNotchAgentPeekOnRequest();
    }

    // Token: 0x0600127A RID: 4730 RVA: 0x003A1BF0 File Offset: 0x003A1BF0
    public void DropExpandedReply()
    {
      this.Mutate(delegate
      {
        this.SetExpandedReply(null);
        this.tabBeforeFullReply = null;
      });
    }

    // Token: 0x0600127B RID: 4731 RVA: 0x003A1C04 File Offset: 0x003A1C04
    public void CloseFullReply(NotchChatFullReplyCloseVia? via = 0)
    {
      if (via != null)
      {
        NotchChatFullReplyCloseVia valueOrDefault = via.GetValueOrDefault();
        string text = this.ExpandedReplyEntryId;
        if (text != null)
        {
          this.TrackFullReplyClosed(text, valueOrDefault);
        }
      }
      this.Mutate(delegate
      {
        NotchTab? notchTab = this.tabBeforeFullReply;
        this.tabBeforeFullReply = null;
        this.SetExpandedReply(null);
        if (notchTab != null)
        {
          NotchTab valueOrDefault2 = notchTab.GetValueOrDefault();
          this.SelectCore(valueOrDefault2);
        }
      });
    }

    // Token: 0x0600127C RID: 4732 RVA: 0x003A1C48 File Offset: 0x003A1C48
    [NullableContext(1)]
    public void TrackFullReplyOpened(string entryId, NotchChatFullReplyOpenSource from)
    {
      Func<string, ChatHandOff> entryLookup = this.EntryLookup;
      ChatHandOff chatHandOff = ((entryLookup != null) ? entryLookup(entryId) : null);
      object obj = this.gate;
      lock (obj)
      {
        this.fullReplyOpenedAt = new DateTimeOffset?(this.presenter.Now);
      }
      ClickyAnalytics.TrackProductEvent("notch_chat_action", (chatHandOff != null) ? chatHandOff.ClickySlug : null, (chatHandOff != null) ? chatHandOff.ThreadId : null, NotchChatAnalytics.FullReplyOpened(chatHandOff, from));
    }

    // Token: 0x0600127D RID: 4733 RVA: 0x003A1CD8 File Offset: 0x003A1CD8
    [NullableContext(1)]
    private void TrackFullReplyClosed(string entryId, NotchChatFullReplyCloseVia via)
    {
      Func<string, ChatHandOff> entryLookup = this.EntryLookup;
      ChatHandOff chatHandOff = ((entryLookup != null) ? entryLookup(entryId) : null);
      object obj = this.gate;
      TimeSpan? timeSpan2;
      lock (obj)
      {
        DateTimeOffset? dateTimeOffset = this.fullReplyOpenedAt;
        TimeSpan? timeSpan;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          timeSpan = new TimeSpan?(this.presenter.Now - valueOrDefault);
        }
        else
        {
          timeSpan = null;
        }
        timeSpan2 = timeSpan;
        this.fullReplyOpenedAt = null;
      }
      ClickyAnalytics.TrackProductEvent("notch_chat_action", (chatHandOff != null) ? chatHandOff.ClickySlug : null, (chatHandOff != null) ? chatHandOff.ThreadId : null, NotchChatAnalytics.FullReplyClosed(via, timeSpan2));
    }

    // Token: 0x0600127E RID: 4734 RVA: 0x003A1D9C File Offset: 0x003A1D9C
    internal void NoteFreshOpenCore(bool isHoverOpen = false)
    {
      this.forgetsReplyAndDocumentOnClose = false;
      if (!isHoverOpen)
      {
        NotchTab? notchTab = this.pendingTab;
        if (notchTab != null)
        {
          NotchTab valueOrDefault = notchTab.GetValueOrDefault();
          this.pendingTab = null;
          if (valueOrDefault != NotchTab.Chat)
          {
            this.chatFocusEntryId = null;
          }
          this.SetExpandedReply(this.pendingExpandedReplyEntryId);
          this.pendingExpandedReplyEntryId = null;
          if (this.expandedReplyEntryId == null)
          {
            this.tabBeforeFullReply = null;
          }
          if (this.selected != valueOrDefault)
          {
            this.selected = valueOrDefault;
          }
          return;
        }
      }
      this.pendingTab = null;
      this.pendingExpandedReplyEntryId = null;
      this.chatFocusEntryId = null;
      NotchTabs.RememberedPlace rememberedPlace = this.rememberedPlace;
      if (rememberedPlace == null || this.presenter.Now - rememberedPlace.ClosedAt >= NotchTabs.RememberedPlaceLifetime)
      {
        this.rememberedPlace = null;
        this.settingsSection = NotchSettingsSection.Account;
        this.SetExpandedReply(null);
        this.tabBeforeFullReply = null;
        if (this.selected != NotchTab.Home)
        {
          this.selected = NotchTab.Home;
        }
        return;
      }
      if (rememberedPlace.Tab == NotchTab.Document)
      {
        NotchTabs.RememberedDocument document = rememberedPlace.Document;
        if (document != null && File.Exists(document.Path))
        {
          this.documentPath = document.Path;
          this.documentThreadId = document.ThreadId;
          this.tabBeforeDocument = document.TabBefore;
          this.expandedReplyBeforeDocument = document.ExpandedReplyBefore;
          this.SetExpandedReply(null);
          this.tabBeforeFullReply = null;
          if (this.selected != NotchTab.Document)
          {
            this.selected = NotchTab.Document;
          }
          return;
        }
      }
      NotchTab notchTab2;
      if (rememberedPlace.Tab != NotchTab.Document)
      {
        notchTab2 = rememberedPlace.Tab;
      }
      else
      {
        NotchTabs.RememberedDocument document2 = rememberedPlace.Document;
        notchTab2 = ((document2 != null) ? document2.TabBefore : NotchTab.Home);
      }
      NotchTab notchTab3 = notchTab2;
      NotchTab notchTab4 = ((notchTab3 == NotchTab.Document) ? NotchTab.Home : notchTab3);
      if (notchTab4 == NotchTab.Chat)
      {
        string text = rememberedPlace.ExpandedReplyEntryId;
        if (text != null)
        {
          Func<string, ChatHandOff> entryLookup = this.EntryLookup;
          if (((entryLookup != null) ? entryLookup(text) : null) != null)
          {
            this.SetExpandedReply(text);
            this.tabBeforeFullReply = rememberedPlace.TabBeforeFullReply;
            goto IL_01DB;
          }
        }
      }
      this.SetExpandedReply(null);
      this.tabBeforeFullReply = null;
      IL_01DB:
      if (this.selected != notchTab4)
      {
        this.selected = notchTab4;
      }
    }

    // Token: 0x0600127F RID: 4735 RVA: 0x003A1F98 File Offset: 0x003A1F98
    internal void RaiseChangedAfterLadder(NotchTab before)
    {
      object obj = this.gate;
      NotchTab notchTab;
      lock (obj)
      {
        notchTab = this.selected;
      }
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      if (before != notchTab)
      {
        Action<NotchTab> selectionChanged = this.SelectionChanged;
        if (selectionChanged == null)
        {
          return;
        }
        selectionChanged(notchTab);
      }
    }

    // Token: 0x06001280 RID: 4736 RVA: 0x003A2000 File Offset: 0x003A2000
    internal void ConsumeComposerFocusRequestCore()
    {
      this.hasPendingComposerFocusRequest = false;
    }

    // Token: 0x1700044F RID: 1103
    // (get) Token: 0x06001281 RID: 4737 RVA: 0x003A200C File Offset: 0x003A200C
    internal NotchTab SelectedCore
    {
      get
      {
        return this.selected;
      }
    }

    // Token: 0x06001282 RID: 4738 RVA: 0x003A2014 File Offset: 0x003A2014
    internal void SelectCoreInternal(NotchTab tab)
    {
      this.SelectCore(tab);
    }

    // Token: 0x04000835 RID: 2101
    [Nullable(1)]
    private readonly NotchPresenter presenter;

    // Token: 0x04000836 RID: 2102
    [Nullable(1)]
    private readonly object gate;

    // Token: 0x04000837 RID: 2103
    private NotchTab selected;

    // Token: 0x04000838 RID: 2104
    private string chatFocusEntryId;

    // Token: 0x04000839 RID: 2105
    private string expandedReplyEntryId;

    // Token: 0x0400083A RID: 2106
    private double? fullReplyContentHeight;

    // Token: 0x0400083B RID: 2107
    private NotchSettingsSection settingsSection;

    // Token: 0x0400083C RID: 2108
    private string documentPath;

    // Token: 0x0400083D RID: 2109
    private string documentThreadId;

    // Token: 0x0400083E RID: 2110
    private NotchTab tabBeforeDocument;

    // Token: 0x0400083F RID: 2111
    [TupleElementNames(new string[] { "EntryId", "ReturnTab" })]
    [Nullable(new byte[] { 0, 1 })]
    private ValueTuple<string, NotchTab?>? expandedReplyBeforeDocument;

    // Token: 0x04000840 RID: 2112
    private int composerFocusRequestId;

    // Token: 0x04000841 RID: 2113
    private bool hasPendingComposerFocusRequest;

    // Token: 0x04000842 RID: 2114
    private NotchTab? pendingTab;

    // Token: 0x04000843 RID: 2115
    private string pendingExpandedReplyEntryId;

    // Token: 0x04000844 RID: 2116
    private NotchTab? tabBeforeFullReply;

    // Token: 0x04000845 RID: 2117
    private DateTimeOffset? fullReplyOpenedAt;

    // Token: 0x04000846 RID: 2118
    public static readonly TimeSpan RememberedPlaceLifetime = TimeSpan.FromMinutes(15L);

    // Token: 0x04000847 RID: 2119
    private NotchTabs.RememberedPlace rememberedPlace;

    // Token: 0x04000848 RID: 2120
    private NotchTabs.RememberedDocument documentLeftOnClose;

    // Token: 0x04000849 RID: 2121
    private bool forgetsReplyAndDocumentOnClose;

    // Token: 0x0200047A RID: 1146
    [NullableContext(1)]
    [Nullable(0)]
    private sealed class RememberedDocument : IEquatable<NotchTabs.RememberedDocument>
    {
      // Token: 0x06002E98 RID: 11928 RVA: 0x004300D8 File Offset: 0x004300D8
      public RememberedDocument(string Path, [Nullable(2)] string ThreadId, NotchTab TabBefore, [TupleElementNames(new string[] { "EntryId", "ReturnTab" })] [Nullable(new byte[] { 0, 1 })] ValueTuple<string, NotchTab?>? ExpandedReplyBefore)
      {
        this.Path = Path;
        this.ThreadId = ThreadId;
        this.TabBefore = TabBefore;
        this.ExpandedReplyBefore = ExpandedReplyBefore;
        base..ctor();
      }

      // Token: 0x1700086B RID: 2155
      // (get) Token: 0x06002E99 RID: 11929 RVA: 0x00430100 File Offset: 0x00430100
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchTabs.RememberedDocument);
        }
      }

      // Token: 0x1700086C RID: 2156
      // (get) Token: 0x06002E9A RID: 11930 RVA: 0x0043010C File Offset: 0x0043010C
      // (set) Token: 0x06002E9B RID: 11931 RVA: 0x00430114 File Offset: 0x00430114
      public string Path { get; set; }

      // Token: 0x1700086D RID: 2157
      // (get) Token: 0x06002E9C RID: 11932 RVA: 0x00430120 File Offset: 0x00430120
      // (set) Token: 0x06002E9D RID: 11933 RVA: 0x00430128 File Offset: 0x00430128
      [Nullable(2)]
      public string ThreadId
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x1700086E RID: 2158
      // (get) Token: 0x06002E9E RID: 11934 RVA: 0x00430134 File Offset: 0x00430134
      // (set) Token: 0x06002E9F RID: 11935 RVA: 0x0043013C File Offset: 0x0043013C
      public NotchTab TabBefore { get; set; }

      // Token: 0x1700086F RID: 2159
      // (get) Token: 0x06002EA0 RID: 11936 RVA: 0x00430148 File Offset: 0x00430148
      // (set) Token: 0x06002EA1 RID: 11937 RVA: 0x00430150 File Offset: 0x00430150
      [TupleElementNames(new string[] { "EntryId", "ReturnTab" })]
      [Nullable(new byte[] { 0, 1 })]
      public ValueTuple<string, NotchTab?>? ExpandedReplyBefore
      {
        [return: TupleElementNames(new string[] { "EntryId", "ReturnTab" })]
        [return: Nullable(new byte[] { 0, 1 })]
        get;
        [param: TupleElementNames(new string[] { "EntryId", "ReturnTab" })]
        [param: Nullable(new byte[] { 0, 1 })]
        set;
      }

      // Token: 0x06002EA2 RID: 11938 RVA: 0x0043015C File Offset: 0x0043015C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("RememberedDocument");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002EA3 RID: 11939 RVA: 0x004301A8 File Offset: 0x004301A8
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Path = ");
        builder.Append(this.Path);
        builder.Append(", ThreadId = ");
        builder.Append(this.ThreadId);
        builder.Append(", TabBefore = ");
        builder.Append(this.TabBefore.ToString());
        builder.Append(", ExpandedReplyBefore = ");
        builder.Append(this.ExpandedReplyBefore.ToString());
        return true;
      }

      // Token: 0x06002EA4 RID: 11940 RVA: 0x0043023C File Offset: 0x0043023C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchTabs.RememberedDocument left, NotchTabs.RememberedDocument right)
      {
        return !(left == right);
      }

      // Token: 0x06002EA5 RID: 11941 RVA: 0x00430248 File Offset: 0x00430248
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchTabs.RememberedDocument left, NotchTabs.RememberedDocument right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002EA6 RID: 11942 RVA: 0x0043025C File Offset: 0x0043025C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<NotchTab>.Default.GetHashCode(this.<TabBefore>k__BackingField)) * -1521134295 + EqualityComparer<ValueTuple<string, NotchTab?>?>.Default.GetHashCode(this.<ExpandedReplyBefore>k__BackingField);
      }

      // Token: 0x06002EA7 RID: 11943 RVA: 0x004302D8 File Offset: 0x004302D8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchTabs.RememberedDocument);
      }

      // Token: 0x06002EA8 RID: 11944 RVA: 0x004302E8 File Offset: 0x004302E8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchTabs.RememberedDocument other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<NotchTab>.Default.Equals(this.<TabBefore>k__BackingField, other.<TabBefore>k__BackingField) && EqualityComparer<ValueTuple<string, NotchTab?>?>.Default.Equals(this.<ExpandedReplyBefore>k__BackingField, other.<ExpandedReplyBefore>k__BackingField));
      }

      // Token: 0x06002EAA RID: 11946 RVA: 0x0043037C File Offset: 0x0043037C
      [CompilerGenerated]
      private RememberedDocument(NotchTabs.RememberedDocument original)
      {
        this.Path = original.<Path>k__BackingField;
        this.ThreadId = original.<ThreadId>k__BackingField;
        this.TabBefore = original.<TabBefore>k__BackingField;
        this.ExpandedReplyBefore = original.<ExpandedReplyBefore>k__BackingField;
      }

      // Token: 0x06002EAB RID: 11947 RVA: 0x004303B4 File Offset: 0x004303B4
      [CompilerGenerated]
      public void Deconstruct(out string Path, [Nullable(2)] out string ThreadId, out NotchTab TabBefore, [TupleElementNames(new string[] { "EntryId", "ReturnTab" })] [Nullable(new byte[] { 0, 1 })] out ValueTuple<string, NotchTab?>? ExpandedReplyBefore)
      {
        Path = this.Path;
        ThreadId = this.ThreadId;
        TabBefore = this.TabBefore;
        ExpandedReplyBefore = this.ExpandedReplyBefore;
      }
    }

    // Token: 0x0200047B RID: 1147
    [Nullable(0)]
    private sealed class RememberedPlace : IEquatable<NotchTabs.RememberedPlace>
    {
      // Token: 0x06002EAC RID: 11948 RVA: 0x004303DC File Offset: 0x004303DC
      public RememberedPlace(NotchTab Tab, string ExpandedReplyEntryId, NotchTab? TabBeforeFullReply, NotchTabs.RememberedDocument Document, DateTimeOffset ClosedAt)
      {
        this.Tab = Tab;
        this.ExpandedReplyEntryId = ExpandedReplyEntryId;
        this.TabBeforeFullReply = TabBeforeFullReply;
        this.Document = Document;
        this.ClosedAt = ClosedAt;
        base..ctor();
      }

      // Token: 0x17000870 RID: 2160
      // (get) Token: 0x06002EAD RID: 11949 RVA: 0x0043040C File Offset: 0x0043040C
      [Nullable(1)]
      [CompilerGenerated]
      private Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(NotchTabs.RememberedPlace);
        }
      }

      // Token: 0x17000871 RID: 2161
      // (get) Token: 0x06002EAE RID: 11950 RVA: 0x00430418 File Offset: 0x00430418
      // (set) Token: 0x06002EAF RID: 11951 RVA: 0x00430420 File Offset: 0x00430420
      public NotchTab Tab { get; set; }

      // Token: 0x17000872 RID: 2162
      // (get) Token: 0x06002EB0 RID: 11952 RVA: 0x0043042C File Offset: 0x0043042C
      // (set) Token: 0x06002EB1 RID: 11953 RVA: 0x00430434 File Offset: 0x00430434
      public string ExpandedReplyEntryId { get; set; }

      // Token: 0x17000873 RID: 2163
      // (get) Token: 0x06002EB2 RID: 11954 RVA: 0x00430440 File Offset: 0x00430440
      // (set) Token: 0x06002EB3 RID: 11955 RVA: 0x00430448 File Offset: 0x00430448
      public NotchTab? TabBeforeFullReply { get; set; }

      // Token: 0x17000874 RID: 2164
      // (get) Token: 0x06002EB4 RID: 11956 RVA: 0x00430454 File Offset: 0x00430454
      // (set) Token: 0x06002EB5 RID: 11957 RVA: 0x0043045C File Offset: 0x0043045C
      public NotchTabs.RememberedDocument Document { get; set; }

      // Token: 0x17000875 RID: 2165
      // (get) Token: 0x06002EB6 RID: 11958 RVA: 0x00430468 File Offset: 0x00430468
      // (set) Token: 0x06002EB7 RID: 11959 RVA: 0x00430470 File Offset: 0x00430470
      public DateTimeOffset ClosedAt { get; set; }

      // Token: 0x06002EB8 RID: 11960 RVA: 0x0043047C File Offset: 0x0043047C
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("RememberedPlace");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002EB9 RID: 11961 RVA: 0x004304C8 File Offset: 0x004304C8
      [NullableContext(1)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Tab = ");
        builder.Append(this.Tab.ToString());
        builder.Append(", ExpandedReplyEntryId = ");
        builder.Append(this.ExpandedReplyEntryId);
        builder.Append(", TabBeforeFullReply = ");
        builder.Append(this.TabBeforeFullReply.ToString());
        builder.Append(", Document = ");
        builder.Append(this.Document);
        builder.Append(", ClosedAt = ");
        builder.Append(this.ClosedAt.ToString());
        return true;
      }

      // Token: 0x06002EBA RID: 11962 RVA: 0x00430584 File Offset: 0x00430584
      [CompilerGenerated]
      public static bool operator !=(NotchTabs.RememberedPlace left, NotchTabs.RememberedPlace right)
      {
        return !(left == right);
      }

      // Token: 0x06002EBB RID: 11963 RVA: 0x00430590 File Offset: 0x00430590
      [CompilerGenerated]
      public static bool operator ==(NotchTabs.RememberedPlace left, NotchTabs.RememberedPlace right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002EBC RID: 11964 RVA: 0x004305A4 File Offset: 0x004305A4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<NotchTab>.Default.GetHashCode(this.<Tab>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ExpandedReplyEntryId>k__BackingField)) * -1521134295 + EqualityComparer<NotchTab?>.Default.GetHashCode(this.<TabBeforeFullReply>k__BackingField)) * -1521134295 + EqualityComparer<NotchTabs.RememberedDocument>.Default.GetHashCode(this.<Document>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<ClosedAt>k__BackingField);
      }

      // Token: 0x06002EBD RID: 11965 RVA: 0x00430634 File Offset: 0x00430634
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchTabs.RememberedPlace);
      }

      // Token: 0x06002EBE RID: 11966 RVA: 0x00430644 File Offset: 0x00430644
      [CompilerGenerated]
      public bool Equals(NotchTabs.RememberedPlace other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<NotchTab>.Default.Equals(this.<Tab>k__BackingField, other.<Tab>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ExpandedReplyEntryId>k__BackingField, other.<ExpandedReplyEntryId>k__BackingField) && EqualityComparer<NotchTab?>.Default.Equals(this.<TabBeforeFullReply>k__BackingField, other.<TabBeforeFullReply>k__BackingField) && EqualityComparer<NotchTabs.RememberedDocument>.Default.Equals(this.<Document>k__BackingField, other.<Document>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<ClosedAt>k__BackingField, other.<ClosedAt>k__BackingField));
      }

      // Token: 0x06002EC0 RID: 11968 RVA: 0x004306F4 File Offset: 0x004306F4
      [CompilerGenerated]
      private RememberedPlace([Nullable(1)] NotchTabs.RememberedPlace original)
      {
        this.Tab = original.<Tab>k__BackingField;
        this.ExpandedReplyEntryId = original.<ExpandedReplyEntryId>k__BackingField;
        this.TabBeforeFullReply = original.<TabBeforeFullReply>k__BackingField;
        this.Document = original.<Document>k__BackingField;
        this.ClosedAt = original.<ClosedAt>k__BackingField;
      }

      // Token: 0x06002EC1 RID: 11969 RVA: 0x00430744 File Offset: 0x00430744
      [CompilerGenerated]
      public void Deconstruct(out NotchTab Tab, out string ExpandedReplyEntryId, out NotchTab? TabBeforeFullReply, out NotchTabs.RememberedDocument Document, out DateTimeOffset ClosedAt)
      {
        Tab = this.Tab;
        ExpandedReplyEntryId = this.ExpandedReplyEntryId;
        TabBeforeFullReply = this.TabBeforeFullReply;
        Document = this.Document;
        ClosedAt = this.ClosedAt;
      }
    }
  }
}
