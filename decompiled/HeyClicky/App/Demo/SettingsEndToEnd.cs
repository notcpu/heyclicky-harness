using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.Billing;
using HeyClicky.App.Interop;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Settings;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Settings;
using HeyClicky.Platform.Audio;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.Demo
{
  // Token: 0x02000359 RID: 857
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SettingsEndToEnd
  {
    // Token: 0x06002546 RID: 9542 RVA: 0x0063ABB0 File Offset: 0x0063ABB0
    public SettingsEndToEnd(AppHost host, string outDir, Action<string> say)
    {
    }

    // Token: 0x170006A1 RID: 1697
    // (get) Token: 0x06002547 RID: 9543 RVA: 0x0063ABD0 File Offset: 0x0063ABD0
    private SettingsContent View
    {
      get
      {
        return NotchSettingsTab.ShownContent;
      }
    }

    // Token: 0x06002548 RID: 9544 RVA: 0x0063ABD8 File Offset: 0x0063ABD8
    private void Check(string name, bool ok, string detail = "")
    {
      if (ok)
      {
        this.passed++;
      }
      else
      {
        this.failed++;
      }
      Action<string> action = this.<say>P;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 3);
      defaultInterpolatedStringHandler.AppendLiteral("settings: ");
      defaultInterpolatedStringHandler.AppendFormatted(ok ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(name);
      defaultInterpolatedStringHandler.AppendFormatted((detail.Length > 0) ? (" (" + detail + ")") : "");
      action(defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x06002549 RID: 9545 RVA: 0x0063AC80 File Offset: 0x0063AC80
    private static Task Wait(int ms)
    {
      return Task.Delay(ms);
    }

    // Token: 0x0600254A RID: 9546 RVA: 0x0063AC88 File Offset: 0x0063AC88
    private async Task Page(SettingsWindow.Page page, string shot)
    {
      this.View.Show(new SettingsWindow.Page?(page));
      await SettingsEndToEnd.Wait(900);
      await Snapshot.SaveAsync(this.View, "settings-" + shot);
      string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_E2E_SCREENSHOT");
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (environmentVariable != null && environmentVariable.Length > 0 && File.Exists(environmentVariable))
      {
        string text = Path.Combine(this.<outDir>P, "desktop-settings-" + shot + ".png");
        string text2 = "powershell.exe";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(52, 2);
        defaultInterpolatedStringHandler.AppendLiteral("-NoProfile -ExecutionPolicy Bypass -File \"");
        defaultInterpolatedStringHandler.AppendFormatted(environmentVariable);
        defaultInterpolatedStringHandler.AppendLiteral("\" -Path \"");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        defaultInterpolatedStringHandler.AppendLiteral("\"");
        using (Process process = Process.Start(new ProcessStartInfo(text2, defaultInterpolatedStringHandler.ToStringAndClear())
        {
          CreateNoWindow = true,
          UseShellExecute = false
        }))
        {
          if (process != null)
          {
            process.WaitForExit(15000);
          }
        }
      }
      Action<string> action = this.<say>P;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 1);
      defaultInterpolatedStringHandler.AppendLiteral("settings: page ");
      defaultInterpolatedStringHandler.AppendFormatted<SettingsWindow.Page>(page);
      defaultInterpolatedStringHandler.AppendLiteral(" shown");
      action(defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x0600254B RID: 9547 RVA: 0x0063ACDC File Offset: 0x0063ACDC
    private static IEnumerable<DependencyObject> Descendants(DependencyObject root)
    {
      SettingsEndToEnd.<Descendants>d__11 <Descendants>d__ = new SettingsEndToEnd.<Descendants>d__11(-2);
      <Descendants>d__.<>3__root = root;
      return <Descendants>d__;
    }

    // Token: 0x0600254C RID: 9548 RVA: 0x0063ACEC File Offset: 0x0063ACEC
    [NullableContext(2)]
    private TextBlock FindText([Nullable(1)] string text, DependencyObject within = null)
    {
      return SettingsEndToEnd.Descendants(within ?? this.View).OfType<TextBlock>().FirstOrDefault<TextBlock>((TextBlock t) => t.Text == text);
    }

    // Token: 0x0600254D RID: 9549 RVA: 0x0063AD2C File Offset: 0x0063AD2C
    private bool Click(string text, bool trailingControl = false, bool last = false)
    {
      TextBlock textBlock = (last ? SettingsEndToEnd.Descendants(this.View).OfType<TextBlock>().LastOrDefault<TextBlock>((TextBlock x) => x.Text == text) : this.FindText(text, null));
      if (textBlock == null)
      {
        this.<say>P("settings: no \"" + text + "\" on screen");
        return false;
      }
      DependencyObject dependencyObject = textBlock;
      if (!trailingControl)
      {
        while (dependencyObject != null)
        {
          Pressable pressable = dependencyObject as Pressable;
          if (pressable != null)
          {
            pressable.RaiseClick();
            return true;
          }
          dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
        }
      }
      for (dependencyObject = textBlock; dependencyObject != null; dependencyObject = VisualTreeHelper.GetParent(dependencyObject))
      {
        Grid grid = dependencyObject as Grid;
        if (grid != null && grid.ColumnDefinitions.Count >= 2)
        {
          Pressable pressable2 = SettingsEndToEnd.Descendants(grid).OfType<Pressable>().LastOrDefault<Pressable>();
          if (pressable2 != null)
          {
            pressable2.RaiseClick();
            return true;
          }
        }
      }
      this.<say>P("settings: no control beside \"" + text + "\"");
      return false;
    }

    // Token: 0x0600254E RID: 9550 RVA: 0x0063AE2C File Offset: 0x0063AE2C
    [return: Nullable(2)]
    private TextBox FindTextBox(string placeholder)
    {
      return SettingsEndToEnd.Descendants(this.View).OfType<TextBox>().FirstOrDefault<TextBox>((TextBox b) => b.PlaceholderText == placeholder);
    }

    // Token: 0x170006A2 RID: 1698
    // (get) Token: 0x0600254F RID: 9551 RVA: 0x0063AE68 File Offset: 0x0063AE68
    private AppSettings Saved
    {
      get
      {
        return this.<host>P.Real.Core.Settings.Current;
      }
    }

    // Token: 0x06002550 RID: 9552 RVA: 0x0063AE84 File Offset: 0x0063AE84
    private static void Key(byte vk, bool down)
    {
      Native.keybd_event(vk, 0, down ? 0U : 2U, (UIntPtr)((IntPtr)0));
    }

    // Token: 0x06002551 RID: 9553 RVA: 0x0063AE98 File Offset: 0x0063AE98
    public async Task<bool> RunAsync()
    {
      SettingsEndToEnd.<>c__DisplayClass18_0 CS$<>8__locals1 = new SettingsEndToEnd.<>c__DisplayClass18_0();
      CS$<>8__locals1.real = this.<host>P.Real;
      SettingsStore store = CS$<>8__locals1.real.Core.Settings;
      CS$<>8__locals1.original = store.Current;
      this.<host>P.OpenSettings(NotchSettingsSection.Account);
      await SettingsEndToEnd.Wait(1500);
      await this.Page(SettingsWindow.Page.Account, "account");
      await SettingsEndToEnd.Wait(1500);
      string text = "plan card loaded from /me/plan";
      BillingUi billing = this.<host>P.Billing;
      bool flag = ((billing != null) ? billing.State.Plan : null) != null && this.FindText(this.<host>P.Billing.PlanCard.Title, null) != null;
      BillingUi billing2 = this.<host>P.Billing;
      this.Check(text, flag, ((billing2 != null) ? billing2.PlanCard.Title : null) ?? "no billing");
      this.Check("usage meters", this.FindText("Talk to HeyClicky", null) != null && this.FindText("Agent messages", null) != null, "");
      this.Check("account rows", this.FindText("Log out", null) != null && this.FindText("Delete account", null) != null && this.FindText("Quit HeyClicky", null) != null, "");
      this.Click("Delete account", false, false);
      await SettingsEndToEnd.Wait(500);
      this.Check("delete account: step 1", this.FindText("Delete your account forever?", null) != null, "");
      this.Click("Too buggy", false, false);
      await SettingsEndToEnd.Wait(200);
      this.Click("Continue", false, false);
      await SettingsEndToEnd.Wait(500);
      this.Check("delete account: step 2 (type DELETE)", this.FindText("One last step.", null) != null, "");
      await Snapshot.SaveAsync(this.View, "settings-account-delete");
      this.Click("Go back", false, false);
      await SettingsEndToEnd.Wait(300);
      this.Click("Never mind", false, false);
      await SettingsEndToEnd.Wait(300);
      this.Check("delete account: cancelled, nothing deleted", this.FindText("Delete your account forever?", null) == null && CS$<>8__locals1.real.Core.Auth.IsSignedIn, "");
      this.Check("account: no Invite & Earn row", this.FindText("Get paid a share of every payment your friends make.", null) == null, "");
      await this.Page(SettingsWindow.Page.Referral, "referral");
      await this.Page(SettingsWindow.Page.General, "general");
      this.Click("Show in screen recordings", true, false);
      await SettingsEndToEnd.Wait(300);
      this.Check("show in screen recordings: off → saved + windows excluded", !this.Saved.ShowInScreenRecordings && !OverlayWindow.ShowInScreenRecordings, "");
      this.Click("Show in screen recordings", true, false);
      await SettingsEndToEnd.Wait(300);
      this.Check("show in screen recordings: on again", this.Saved.ShowInScreenRecordings && OverlayWindow.ShowInScreenRecordings, "");
      string text2 = "updates row (Velopack updater)";
      bool flag2;
      if (this.FindText("What's new", null) != null)
      {
        flag2 = SettingsEndToEnd.Descendants(this.View).OfType<TextBlock>().Any<TextBlock>(delegate(TextBlock t)
        {
          string text6 = t.Text;
          return text6 == "App updates" || text6 == "Update available" || text6 == "Update ready to install";
        });
      }
      else
      {
        flag2 = false;
      }
      this.Check(text2, flag2, "");
      this.Click("What's new", false, false);
      this.Click("Request a feature", false, false);
      this.Click("WhatsApp community", false, false);
      this.Click("Reddit", false, false);
      this.Click("Report a bug", false, false);
      await SettingsEndToEnd.Wait(1500);
      string[] files = Directory.GetFiles(AppPaths.Combine(new string[] { "bug-reports" }), "*.zip");
      string text3 = "report a bug wrote diagnostics";
      bool flag3 = files.Any<string>((string r) => DateTime.Now - File.GetLastWriteTime(r) < TimeSpan.FromMinutes(1L));
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
      defaultInterpolatedStringHandler.AppendFormatted<int>(files.Length);
      defaultInterpolatedStringHandler.AppendLiteral(" report(s)");
      this.Check(text3, flag3, defaultInterpolatedStringHandler.ToStringAndClear());
      await this.Page(SettingsWindow.Page.Voice, "voice");
      this.Click("1.25x", false, false);
      await SettingsEndToEnd.Wait(300);
      this.Check("speech speed 1.25x saved", Math.Abs(this.Saved.SpeechSpeed - 1.25) < 0.001, "");
      this.Click("Marin", false, false);
      await SettingsEndToEnd.Wait(800);
      this.Check("voice Marin saved", this.Saved.VoiceIdentifier == "marin", "");
      IReadOnlyList<AudioDeviceInfo> readOnlyList = AudioDevices.List(false);
      AudioDeviceInfo output = readOnlyList.FirstOrDefault<AudioDeviceInfo>((AudioDeviceInfo d) => !d.IsDefault) ?? readOnlyList.FirstOrDefault<AudioDeviceInfo>();
      if (output != null)
      {
        this.Click(output.Name, false, true);
        await SettingsEndToEnd.Wait(600);
        this.Check("output device picked (" + output.Name + ")", this.Saved.OutputDeviceId == output.Id && AudioDevices.PreferredRenderId == output.Id, "");
        this.Click("Play a test sound", false, false);
        await SettingsEndToEnd.Wait(800);
        this.Click("System Default", false, false);
        await SettingsEndToEnd.Wait(300);
        this.Check("output back to System Default", this.Saved.OutputDeviceId == null && AudioDevices.PreferredRenderId == null, "");
      }
      else
      {
        this.<say>P("settings: no output devices listed");
      }
      await this.Page(SettingsWindow.Page.Microphone, "microphone");
      IReadOnlyList<AudioDeviceInfo> readOnlyList2 = AudioDevices.List(true);
      this.<say>P("settings: microphones: " + string.Join(" | ", readOnlyList2.Select<AudioDeviceInfo, string>((AudioDeviceInfo m) => m.Name + (m.IsDefault ? " (default)" : ""))));
      AudioDeviceInfo mic = readOnlyList2.FirstOrDefault<AudioDeviceInfo>((AudioDeviceInfo d) => !d.IsDefault) ?? readOnlyList2.FirstOrDefault<AudioDeviceInfo>();
      if (mic != null)
      {
        this.Click(mic.Name, false, true);
        await SettingsEndToEnd.Wait(800);
        this.Check("microphone picked (" + mic.Name + ")", this.Saved.MicrophoneDeviceId == mic.Id && AudioDevices.PreferredCaptureId == mic.Id, "");
      }
      this.Click("Test microphone", false, false);
      await SettingsEndToEnd.Wait(2600);
      string text4 = (from t in SettingsEndToEnd.Descendants(this.View).OfType<TextBlock>()
        select t.Text).FirstOrDefault<string>((string t) => t == "Receiving audio" || t == "HeyClicky can't hear you. Try different microphone." || t == "Listening...");
      this.Check("microphone test runs", text4 != null, text4 ?? "no status");
      await Snapshot.SaveAsync(this.View, "settings-microphone-test");
      this.Click("Stop", false, false);
      await SettingsEndToEnd.Wait(300);
      this.Click("System Default", false, false);
      await SettingsEndToEnd.Wait(500);
      this.Check("microphone back to System Default", this.Saved.MicrophoneDeviceId == null, "");
      await this.Page(SettingsWindow.Page.Dictation, "dictation");
      this.Click("Auto-detect language", true, false);
      await SettingsEndToEnd.Wait(300);
      this.Check("auto-detect off → the pinned language", this.Saved.DictationLanguageCode == this.Saved.DictationSpecificLanguageCode && this.Saved.DictationLanguageCode != "multi", "");
      this.Click("Spanish", false, false);
      await SettingsEndToEnd.Wait(300);
      this.Check("pinned Spanish → language code es", this.Saved.DictationLanguageCode == "es" && this.Saved.DictationSpecificLanguageCode == "es", "");
      TextBox search = this.FindTextBox("Search languages");
      if (search != null)
      {
        search.Text = "port";
        await SettingsEndToEnd.Wait(300);
        this.Check("language search filters", this.FindText("Portuguese", null) != null && this.FindText("Spanish", null) == null, "");
        search.Text = "";
      }
      this.Click("Auto-detect language", true, false);
      await SettingsEndToEnd.Wait(300);
      this.Check("auto-detect on → multi, pin kept", this.Saved.DictationLanguageCode == "multi" && this.Saved.DictationSpecificLanguageCode == "es", "");
      TextBox textBox = this.FindTextBox("Add a word");
      if (textBox != null)
      {
        textBox.Text = "Farza";
        await SettingsEndToEnd.Wait(100);
        this.Click("Add", false, false);
        await SettingsEndToEnd.Wait(400);
      }
      this.Check("dictionary word added", this.Saved.DictationCustomWords.Contains("Farza"), "");
      TextBlock textBlock = this.FindText("Farza", null);
      StackPanel stackPanel = ((textBlock != null) ? textBlock.Parent : null) as StackPanel;
      if (stackPanel != null)
      {
        Pressable pressable = stackPanel.Children.OfType<Pressable>().FirstOrDefault<Pressable>();
        if (pressable != null)
        {
          pressable.RaiseClick();
          await SettingsEndToEnd.Wait(300);
        }
      }
      this.Check("dictionary word removed", !this.Saved.DictationCustomWords.Contains("Farza"), "");
      await this.Page(SettingsWindow.Page.Shortcuts, "shortcuts");
      if (OverlayWindow.Offscreen || Environment.GetEnvironmentVariable("HEYCLICKY_E2E_KEYS") != "1")
      {
        this.<say>P("settings: offscreen run: shortcut recording (real key presses) skipped");
      }
      else
      {
        this.Click("Change", false, false);
        await SettingsEndToEnd.Wait(400);
        this.Check("recorder armed (monitor recording)", CS$<>8__locals1.real.Shortcuts.IsRecording, "");
        SettingsEndToEnd.Key(162, true);
        await SettingsEndToEnd.Wait(80);
        SettingsEndToEnd.Key(160, true);
        await SettingsEndToEnd.Wait(600);
        SettingsEndToEnd.Key(160, false);
        await SettingsEndToEnd.Wait(60);
        SettingsEndToEnd.Key(162, false);
        await SettingsEndToEnd.Wait(500);
        await Snapshot.SaveAsync(this.View, "settings-shortcuts-recorded");
        this.Click("Save", false, false);
        await SettingsEndToEnd.Wait(500);
        this.Check("push to talk rebound to Ctrl+Shift (settings.json + monitor)", this.Saved.EffectiveHotkeys.PushToTalk.Modifiers == (HotkeyModifiers.Control | HotkeyModifiers.Shift) && CS$<>8__locals1.real.Shortcuts.Bindings.PushToTalk.Modifiers == (HotkeyModifiers.Control | HotkeyModifiers.Shift) && !CS$<>8__locals1.real.Shortcuts.IsRecording, CS$<>8__locals1.real.Shortcuts.Bindings.PushToTalk.DisplayText);
        CS$<>8__locals1.fired = new List<ShortcutAction>();
        CS$<>8__locals1.real.Shortcuts.ShortcutTriggered += CS$<>8__locals1.<RunAsync>g__OnShortcut|7;
        this.<host>P.Hotkeys.AllowsPushToTalk = () => false;
        SettingsEndToEnd.Key(162, true);
        await SettingsEndToEnd.Wait(60);
        SettingsEndToEnd.Key(164, true);
        await SettingsEndToEnd.Wait(300);
        SettingsEndToEnd.Key(164, false);
        await SettingsEndToEnd.Wait(60);
        SettingsEndToEnd.Key(162, false);
        await SettingsEndToEnd.Wait(300);
        List<ShortcutAction> list = CS$<>8__locals1.fired;
        bool oldComboSilent;
        lock (list)
        {
          oldComboSilent = !CS$<>8__locals1.fired.Contains(ShortcutAction.PushToTalkDown);
        }
        SettingsEndToEnd.Key(162, true);
        await SettingsEndToEnd.Wait(60);
        SettingsEndToEnd.Key(160, true);
        await SettingsEndToEnd.Wait(300);
        SettingsEndToEnd.Key(160, false);
        await SettingsEndToEnd.Wait(60);
        SettingsEndToEnd.Key(162, false);
        await SettingsEndToEnd.Wait(300);
        list = CS$<>8__locals1.fired;
        bool flag5;
        lock (list)
        {
          flag5 = CS$<>8__locals1.fired.Contains(ShortcutAction.PushToTalkDown) && CS$<>8__locals1.fired.Contains(ShortcutAction.PushToTalkUp);
        }
        CS$<>8__locals1.real.Shortcuts.ShortcutTriggered -= CS$<>8__locals1.<RunAsync>g__OnShortcut|7;
        this.<host>P.Hotkeys.AllowsPushToTalk = () => CS$<>8__locals1.real.Tutorial.AllowsPushToTalk;
        this.Check("old Ctrl+Alt no longer talks; new Ctrl+Shift does", oldComboSilent && flag5, string.Join<ShortcutAction>(",", CS$<>8__locals1.fired));
        IEnumerable<TextBlock> enumerable = from t in SettingsEndToEnd.Descendants(this.View).OfType<TextBlock>()
          where t.Text == "Change"
          select t;
        Func<TextBlock, Pressable> func;
        if ((func = SettingsEndToEnd.<>O.<0>__FindAncestorPressable) == null)
        {
          func = (SettingsEndToEnd.<>O.<0>__FindAncestorPressable = new Func<TextBlock, Pressable>(SettingsEndToEnd.FindAncestorPressable));
        }
        List<Pressable> list2 = enumerable.Select<TextBlock, Pressable>(func).OfType<Pressable>().Distinct<Pressable>()
          .ToList<Pressable>();
        if (list2.Count > 1)
        {
          list2[1].RaiseClick();
          await SettingsEndToEnd.Wait(300);
        }
        SettingsEndToEnd.Key(162, true);
        await SettingsEndToEnd.Wait(60);
        SettingsEndToEnd.Key(160, true);
        await SettingsEndToEnd.Wait(600);
        SettingsEndToEnd.Key(160, false);
        await SettingsEndToEnd.Wait(60);
        SettingsEndToEnd.Key(162, false);
        await SettingsEndToEnd.Wait(400);
        this.Click("Save", false, false);
        await SettingsEndToEnd.Wait(400);
        this.Check("conflicting combo refused", this.FindText("Already used by Push to talk. Pick a different combo.", null) != null && this.Saved.EffectiveHotkeys.TextMode == HotkeyBinding.TextModeDefault, "");
        await Snapshot.SaveAsync(this.View, "settings-shortcuts-conflict");
        this.Click("Cancel", false, false);
        await SettingsEndToEnd.Wait(300);
        this.Click("Reset to defaults", false, false);
        await SettingsEndToEnd.Wait(400);
        this.Check("reset to defaults", this.Saved.Hotkeys == null && CS$<>8__locals1.real.Shortcuts.Bindings == HotkeyBindings.Defaults, "");
      }
      await this.Page(SettingsWindow.Page.Cursor, "cursor");
      this.Click("Hide floating cursor", true, false);
      await SettingsEndToEnd.Wait(3200);
      this.Check("hide floating cursor toggles it", this.Saved.CursorDocked != CS$<>8__locals1.original.CursorDocked, "");
      this.Click("Hide floating cursor", true, false);
      await SettingsEndToEnd.Wait(3200);
      this.Check("hide floating cursor back as it was", this.Saved.CursorDocked == CS$<>8__locals1.original.CursorDocked, "");
      this.Click("Screen highlight", true, false);
      await SettingsEndToEnd.Wait(200);
      this.Check("screen highlight off → overlay", !this.Saved.ScreenHighlightEnabled && !CS$<>8__locals1.real.Highlight.IsEnabled, "");
      this.Click("Screen highlight", true, false);
      await SettingsEndToEnd.Wait(200);
      await this.Page(SettingsWindow.Page.Agents, "agents");
      this.Click("Auto-approve extra usage", true, false);
      await SettingsEndToEnd.Wait(200);
      this.Check("auto-approve extra usage → AgentService", this.Saved.AgentAutoApproveExtraUsage && CS$<>8__locals1.real.Agents.AutoApproveExtraUsage, "");
      this.Click("Auto-approve extra usage", true, false);
      await SettingsEndToEnd.Wait(200);
      this.Check("auto-approve off again", !this.Saved.AgentAutoApproveExtraUsage && !CS$<>8__locals1.real.Agents.AutoApproveExtraUsage, "");
      this.Click("Speak when a job starts or finishes", true, false);
      await SettingsEndToEnd.Wait(200);
      this.Check("voice announcements off saved", !this.Saved.AgentAnnouncementsVoiceEnabled, "");
      this.Click("Speak when a job starts or finishes", true, false);
      await SettingsEndToEnd.Wait(200);
      CS$<>8__locals1.folder = Path.Combine(Path.GetTempPath(), "heyclicky-e2e-agent-folder");
      store.Update(delegate(AppSettings s)
      {
        s.AgentFolderPath = CS$<>8__locals1.folder;
      });
      await SettingsEndToEnd.Wait(500);
      this.Check("agent folder → AgentService.ProjectsRoot", CS$<>8__locals1.real.Agents.ProjectsRoot == CS$<>8__locals1.folder && this.FindText("Reset to default folder", null) != null, "");
      this.Click("Reset to default folder", false, false);
      await SettingsEndToEnd.Wait(500);
      this.Check("agent folder reset to the default", this.Saved.AgentFolderPath == CS$<>8__locals1.original.AgentFolderPath || this.Saved.AgentFolderPath == null, "");
      if (CS$<>8__locals1.original.AgentFolderPath != null)
      {
        store.Update(delegate(AppSettings s)
        {
          s.AgentFolderPath = CS$<>8__locals1.original.AgentFolderPath;
        });
      }
      await this.Page(SettingsWindow.Page.Integrations, "integrations");
      await SettingsEndToEnd.Wait(4000);
      await Snapshot.SaveAsync(this.View, "settings-integrations");
      this.Check("integrations catalog listed", this.FindText("AVAILABLE INTEGRATIONS", null) != null || this.FindText("CONNECTED", null) != null, (this.FindText("Integrations aren't set up on this server.", null) != null) ? "Composio not configured on this Worker" : "");
      this.Click("Add custom connector", false, false);
      await SettingsEndToEnd.Wait(400);
      TextBox textBox2 = this.FindTextBox("Like Stripe or Meta Ads");
      if (textBox2 != null)
      {
        TextBox textBox3 = this.FindTextBox("https://mcp.example.com/mcp");
        if (textBox3 != null)
        {
          textBox2.Text = "E2E Test MCP";
          textBox3.Text = "https://mcp.example.com/mcp";
          await SettingsEndToEnd.Wait(100);
          this.Click("Save", false, false);
          await SettingsEndToEnd.Wait(800);
          this.Check("custom connector saved → AgentService", CustomConnectorStore.Load().Any<CustomConnector>((CustomConnector c) => c.Name == "E2E Test MCP"), "");
          await Snapshot.SaveAsync(this.View, "settings-integrations-custom");
          CustomConnectorStore.Remove(CustomConnectorStore.Load().First<CustomConnector>((CustomConnector c) => c.Name == "E2E Test MCP").Id);
          await SettingsEndToEnd.Wait(300);
          goto IL_30E7;
        }
      }
      this.Check("custom connector editor opens", false, "");
      IL_30E7:
      if (this.View.Host.ShowsDeveloperSection)
      {
        await this.Page(SettingsWindow.Page.Developer, "developer");
        this.Click("Drawing sounds", true, false);
        await SettingsEndToEnd.Wait(200);
        this.Check("drawing sounds off → canvas", !this.Saved.DrawingSoundsEnabled && !CS$<>8__locals1.real.Canvas.PlaysSounds, "");
        this.Click("Drawing sounds", true, false);
        await SettingsEndToEnd.Wait(200);
        this.Check("drawing sounds on → canvas", this.Saved.DrawingSoundsEnabled && CS$<>8__locals1.real.Canvas.PlaysSounds, "");
        this.Click("Pen sounds", true, false);
        await SettingsEndToEnd.Wait(200);
        this.Check("pen sounds → canvas", this.Saved.DrawingPenSoundsEnabled && CS$<>8__locals1.real.Canvas.PlaysPenSounds, "");
        this.Click("Pen sounds", true, false);
        await SettingsEndToEnd.Wait(200);
        PlanFixtures.Apply(this.<host>P.Billing, "Pro cancelling");
        await SettingsEndToEnd.Wait(300);
        await this.Page(SettingsWindow.Page.Account, "account-forced-pro-cancelling");
        string text5 = "forced plan state shows Pro + cancellation";
        bool flag6;
        if (this.FindText("You're on HeyClicky Pro.", null) != null)
        {
          flag6 = SettingsEndToEnd.Descendants(this.View).OfType<TextBlock>().Any<TextBlock>((TextBlock t) => t.Text.StartsWith("Cancels in"));
        }
        else
        {
          flag6 = false;
        }
        this.Check(text5, flag6, "");
        PlanFixtures.Apply(this.<host>P.Billing, null);
      }
      store.Update(delegate(AppSettings s)
      {
        s.SpeechSpeed = CS$<>8__locals1.original.SpeechSpeed;
        s.VoiceIdentifier = CS$<>8__locals1.original.VoiceIdentifier;
        s.AgentAnnouncementsVoiceEnabled = CS$<>8__locals1.original.AgentAnnouncementsVoiceEnabled;
      });
      this.View.Show(new SettingsWindow.Page?(SettingsWindow.Page.Account));
      NotchPresenter notchPresenter = this.<host>P.Services.Presenter as NotchPresenter;
      if (notchPresenter != null)
      {
        notchPresenter.DismissNotchAgentPeek();
      }
      Action<string> action = this.<say>P;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 2);
      defaultInterpolatedStringHandler.AppendLiteral("settings: ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(this.passed);
      defaultInterpolatedStringHandler.AppendLiteral(" passed, ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(this.failed);
      defaultInterpolatedStringHandler.AppendLiteral(" failed");
      action(defaultInterpolatedStringHandler.ToStringAndClear());
      return this.failed == 0;
    }

    // Token: 0x06002552 RID: 9554 RVA: 0x0063AEDC File Offset: 0x0063AEDC
    [return: Nullable(2)]
    private static Pressable FindAncestorPressable(DependencyObject node)
    {
      for (DependencyObject dependencyObject = node; dependencyObject != null; dependencyObject = VisualTreeHelper.GetParent(dependencyObject))
      {
        Pressable pressable = dependencyObject as Pressable;
        if (pressable != null)
        {
          return pressable;
        }
      }
      return null;
    }

    // Token: 0x040011BB RID: 4539
    [CompilerGenerated]
    private AppHost <host>P = host;

    // Token: 0x040011BC RID: 4540
    [CompilerGenerated]
    private string <outDir>P = outDir;

    // Token: 0x040011BD RID: 4541
    [CompilerGenerated]
    private Action<string> <say>P = say;

    // Token: 0x040011BE RID: 4542
    private int passed;

    // Token: 0x040011BF RID: 4543
    private int failed;

    // Token: 0x02000A5D RID: 2653
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002CCC RID: 11468
      [Nullable(0)]
      public static Func<TextBlock, Pressable> <0>__FindAncestorPressable;
    }
  }
}
