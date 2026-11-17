using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Backend;
using HeyClicky.Agents.Harness;
using HeyClicky.Agents.Routines;
using HeyClicky.App.Billing;
using HeyClicky.App.Browser;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.Interop;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Onboarding;
using HeyClicky.App.OneBrain;
using HeyClicky.App.Portraits;
using HeyClicky.App.Proactive;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Browser;
using HeyClicky.Core.Connectors;
using HeyClicky.Core.Dictation;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Voice;
using HeyClicky.Core.Worker;
using HeyClicky.Platform.Audio;
using HeyClicky.Platform.Input;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI;
using WinRT;
using WinRT.HeyClickyVtableClasses;
using WinRT.Interop;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000D3 RID: 211
  [NullableContext(1)]
  [Nullable(0)]
  [WinRTRuntimeClassName("Microsoft.UI.Xaml.IUIElementOverrides")]
  [WinRTExposedType(typeof(HeyClicky_App_Settings_SettingsContentWinRTTypeDetails))]
  public sealed class SettingsContent : Grid
  {
    // Token: 0x06000911 RID: 2321 RVA: 0x00543D30 File Offset: 0x00543D30
    private FrameworkElement AccountPage()
    {
      if (!this.s.Auth.IsSignedIn)
      {
        GelButton gelButton = new GelButton("Sign in with Google", GelAppearance.PrimaryBlue, GelButton.Sizing.Compact, 100.0, null, null, null)
        {
          HorizontalAlignment = HorizontalAlignment.Left
        };
        gelButton.Click += delegate
        {
          this.actions.SignIn();
        };
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 12.0,
          Padding = new Thickness(16.0),
          Children = 
          {
            Ui.Text("You're not signed in", 14.0, FontWeights.SemiBold, SK.TextPrimary, TextAlignment.Left, 0, false, false),
            Ui.Text("Sign in to sync your plan, connect integrations, and let agents work for you.", 11.5, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 0, true, false),
            gelButton
          }
        };
        GelButton gelButton2 = DevSignIn.Button(this.actions, GelButton.Sizing.Compact);
        if (gelButton2 != null)
        {
          gelButton2.HorizontalAlignment = HorizontalAlignment.Left;
          stackPanel.Children.Add(gelButton2);
        }
        return SK.Scaffold("Account", "Your plan, usage, and account.", new <>z__ReadOnlySingleElementList<FrameworkElement>(SK.Group(null, new FrameworkElement[] { stackPanel })), 20.0);
      }
      return SK.Scaffold("Account", "Your plan, usage, and account.", new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
      {
        this.PlanGroup(),
        this.AccountActionsGroup()
      }), 20.0);
    }

    // Token: 0x06000912 RID: 2322 RVA: 0x00543EA8 File Offset: 0x00543EA8
    private FrameworkElement PlanGroup()
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 14.0,
        Padding = new Thickness(16.0)
      };
      BillingUi billing = this.host.Billing;
      if (billing == null)
      {
        stackPanel.Children.Add(Ui.Text("Your plan shows here when HeyClicky is connected to its servers.", 11.5, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 0, true, false));
        return SK.Group("Plan", new FrameworkElement[] { stackPanel });
      }
      BillingPlanCard planCard = billing.PlanCard;
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 5.0
      };
      stackPanel2.Children.Add(Ui.Text(planCard.Title, 14.0, FontWeights.SemiBold, SK.TextPrimary, TextAlignment.Left, 0, true, false));
      string description = planCard.Description;
      if (description != null)
      {
        stackPanel2.Children.Add(Ui.Text(description, 11.5, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 0, true, false));
      }
      string teamLabel = planCard.TeamLabel;
      if (teamLabel != null)
      {
        stackPanel2.Children.Add(Ui.H(5.0, new UIElement[]
        {
          Icons.Glyph("\ue716", 10.0, SK.TextSecondary),
          Ui.Text(teamLabel, 11.0, FontWeights.SemiBold, SK.TextSecondary, TextAlignment.Left, 0, false, false)
        }));
      }
      string errorText = planCard.ErrorText;
      if (errorText != null)
      {
        stackPanel2.Children.Add(Ui.Text(errorText, 11.0, FontWeights.SemiBold, SK.Destructive, TextAlignment.Left, 0, true, false));
      }
      string cancellationLabel = planCard.CancellationLabel;
      if (cancellationLabel != null)
      {
        stackPanel2.Children.Add(SK.Alert(cancellationLabel, SK.Warning));
      }
      stackPanel.Children.Add(stackPanel2);
      if (planCard.Meters.Count > 0)
      {
        Grid grid = new Grid
        {
          ColumnSpacing = 20.0
        };
        for (int i = 0; i < planCard.Meters.Count; i++)
        {
          grid.ColumnDefinitions.Add(new ColumnDefinition());
          FrameworkElement frameworkElement = SettingsContent.UsageMeter(planCard.Meters[i].Title, planCard.Meters[i].ValueText, planCard.Meters[i].Progress);
          Grid.SetColumn(frameworkElement, i);
          grid.Children.Add(frameworkElement);
        }
        stackPanel.Children.Add(grid);
      }
      WrapPanel wrapPanel = SK.Wrap(10.0);
      foreach (BillingPlanAction billingPlanAction in planCard.Actions)
      {
        BillingActionStyle style = billingPlanAction.Style;
        GelAppearance gelAppearance;
        if (style != BillingActionStyle.Primary)
        {
          if (style != BillingActionStyle.SecondaryWhite)
          {
            gelAppearance = GelAppearance.SecondaryOutline;
          }
          else
          {
            gelAppearance = GelAppearance.SecondaryWhite;
          }
        }
        else
        {
          gelAppearance = GelAppearance.PrimaryBlue;
        }
        GelAppearance gelAppearance2 = gelAppearance;
        GelButton gelButton = new GelButton(billingPlanAction.Title, gelAppearance2, GelButton.Sizing.Large, 100.0, null, null, null);
        BillingPlanActionKind kind = billingPlanAction.Kind;
        gelButton.Click += delegate
        {
          this.RunPlanActionAsync(billing, kind);
        };
        wrapPanel.Children.Add(gelButton);
      }
      if (wrapPanel.Children.Count > 0)
      {
        stackPanel.Children.Add(wrapPanel);
      }
      string text = this.billingActionError;
      if (text != null)
      {
        stackPanel.Children.Add(SK.Alert(text, SK.Destructive));
      }
      return SK.Group("Plan", new FrameworkElement[] { stackPanel });
    }

    // Token: 0x06000913 RID: 2323 RVA: 0x00544284 File Offset: 0x00544284
    private async Task RunPlanActionAsync(BillingUi billing, BillingPlanActionKind kind)
    {
      this.billingActionError = null;
      string text = "settings";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 1);
      defaultInterpolatedStringHandler.AppendLiteral("plan action: ");
      defaultInterpolatedStringHandler.AppendFormatted<BillingPlanActionKind>(kind);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      string text2 = await billing.RunPlanActionAsync(kind);
      string error = text2;
      this.dq.TryEnqueue(delegate
      {
        this.billingActionError = error;
        if (this.page == SettingsWindow.Page.Account)
        {
          this.Render();
        }
      });
    }

    // Token: 0x06000914 RID: 2324 RVA: 0x005442D8 File Offset: 0x005442D8
    private static FrameworkElement UsageMeter(string title, string value, double progress)
    {
      Grid grid = new Grid
      {
        Height = 5.0
      };
      grid.Children.Add(new Border
      {
        Background = Ui.Brush(SK.Ink.WithAlpha(0.1))
      }.Capsule());
      Border fill = new Border
      {
        Background = Ui.Brush(SK.Ink.WithAlpha(0.85)),
        HorizontalAlignment = HorizontalAlignment.Left,
        Width = 4.0
      }.Capsule();
      grid.Children.Add(fill);
      grid.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        fill.Width = Math.Max(4.0, e.NewSize.Width * Math.Clamp(progress, 0.0, 1.0));
      };
      return new StackPanel
      {
        Spacing = 4.0,
        Children = 
        {
          Ui.Text(title, 10.5, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, false, false),
          Ui.Text(value, 12.0, FontWeights.SemiBold, SK.TextPrimary, TextAlignment.Left, 0, false, false),
          grid
        }
      };
    }

    // Token: 0x06000915 RID: 2325 RVA: 0x00544414 File Offset: 0x00544414
    private FrameworkElement AccountActionsGroup()
    {
      string text = "Account";
      FrameworkElement[] array = new FrameworkElement[3];
      int num = 0;
      string text2 = "Log out";
      UserProfile user = this.s.Auth.User;
      array[num] = SK.ActionRow(text2, (user != null) ? user.Email : null, delegate
      {
        Log.Info("settings", "log out");
        this.s.Auth.SignOutAsync();
      }, null, null, false);
      array[1] = SK.ActionRow("Delete account", "Permanently erase your account and all its data.", new Action(this.ShowAccountDeletion), null, null, true);
      array[2] = SK.ActionRow("Quit HeyClicky", null, new Action(this.actions.Quit), null, null, true);
      return SK.Group(text, array);
    }

    // Token: 0x06000916 RID: 2326 RVA: 0x005444B0 File Offset: 0x005444B0
    private void ShowAccountDeletion()
    {
      SettingsContent.<>c__DisplayClass8_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass8_0();
      CS$<>8__locals1.<>4__this = this;
      Log.Info("settings", "account deletion: opened");
      CS$<>8__locals1.step = SettingsContent.DeletionStep.Reason;
      CS$<>8__locals1.picked = null;
      CS$<>8__locals1.detail = "";
      CS$<>8__locals1.confirmation = "";
      CS$<>8__locals1.error = null;
      CS$<>8__locals1.deleting = false;
      CS$<>8__locals1.deleteButton = null;
      CS$<>8__locals1.card = new Grid
      {
        Width = 400.0,
        Padding = new Thickness(24.0),
        CornerRadius = new CornerRadius(14.0),
        Background = Ui.Brush(DS.Hex(1710618U, 1.0)),
        BorderBrush = Ui.Brush(DS.White(0.1)),
        BorderThickness = new Thickness(0.5)
      };
      CS$<>8__locals1.<ShowAccountDeletion>g__Build|0();
      this.ShowOverlay(CS$<>8__locals1.card);
    }

    // Token: 0x06000917 RID: 2327 RVA: 0x005445B4 File Offset: 0x005445B4
    private async Task PurgeLocalAccountDataAsync()
    {
      SettingsContent.<>c__DisplayClass9_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass9_0();
      SettingsContent.<>c__DisplayClass9_0 CS$<>8__locals2 = CS$<>8__locals1;
      AgentService agents2 = this.host.Agents;
      CS$<>8__locals2.projectsRoot = ((agents2 != null) ? agents2.ProjectsRoot : null);
      SettingsContent.<>c__DisplayClass9_0 CS$<>8__locals3 = CS$<>8__locals1;
      AgentService agents3 = this.host.Agents;
      CS$<>8__locals3.codexHome = ((agents3 != null) ? agents3.CodexHome : null);
      try
      {
        AgentService agents = this.host.Agents;
        if (agents != null)
        {
          AgentService agentService = agents;
          Func<string> currentUserId = agents.CurrentUserId;
          agentService.ForgetWorkReportsForDeletedAccount((currentUserId != null) ? currentUserId() : null);
          await agents.SignOutAsync();
          agents.ClickyApps.ForgetAll();
          agents.DeleteAllLocalClickys();
        }
        agents = null;
      }
      catch (Exception ex)
      {
        Log.Warn("settings", "purge: agents sign-out: " + ex.Message);
      }
      await this.s.Auth.SignOutAsync();
      ConversationHistory history = NotchChat.History;
      if (history != null)
      {
        history.Reset();
      }
      ChatHandOffs handOffs = NotchChat.HandOffs;
      if (handOffs != null)
      {
        handOffs.Reset();
      }
      NotchPresenter notchPresenter = this.s.Presenter as NotchPresenter;
      if (notchPresenter != null)
      {
        notchPresenter.Updates.Reset();
      }
      ProactiveCoordinator proactiveCoordinator = ProactiveCoordinator.Current;
      if (proactiveCoordinator != null)
      {
        proactiveCoordinator.ForgetForAccountDeletion();
      }
      ClickyRoster.Shared.ForgetAll();
      await Task.Run(delegate
      {
        AgentHarnessRunnerProcess.Stop();
        DictationTranscriptArchive.DeleteAll();
        string root = AppPaths.Root;
        foreach (string text in new string[]
        {
          (CS$<>8__locals1.projectsRoot == null) ? null : Path.Combine(CS$<>8__locals1.projectsRoot, "agents"),
          (CS$<>8__locals1.projectsRoot == null) ? null : Path.Combine(CS$<>8__locals1.projectsRoot, "jobs"),
          LessonStore.Root
        })
        {
          if (text != null && Directory.Exists(text))
          {
            try
            {
              FileSystem.DeleteDirectory(text, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
              Log.Info("settings", "purge: " + text + " → Recycle Bin");
            }
            catch (Exception ex2)
            {
              Log.Warn("settings", "purge: " + text + ": " + ex2.Message);
            }
          }
        }
        foreach (string text2 in new string[]
        {
          Path.Combine(root, "agents"),
          CS$<>8__locals1.codexHome,
          Path.Combine(root, "OneBrain"),
          Path.Combine(root, "HeyClickyHistory"),
          Path.Combine(root, "Ink"),
          Path.Combine(root, "helper"),
          Path.Combine(root, "ClickyApps"),
          GoodMorningBrief.ResearchFilePath
        })
        {
          if (text2 != null)
          {
            try
            {
              if (Directory.Exists(text2))
              {
                Directory.Delete(text2, true);
              }
              else
              {
                if (!File.Exists(text2))
                {
                  goto IL_01A6;
                }
                File.Delete(text2);
              }
              Log.Info("settings", "purge: " + text2 + " erased");
            }
            catch (Exception ex3)
            {
              Log.Warn("settings", "purge: " + text2 + ": " + ex3.Message);
            }
          }
          IL_01A6:;
        }
      });
      this.dq.TryEnqueue(delegate
      {
        GoodMorningBrief shared = GoodMorningBrief.Shared;
        if (shared == null)
        {
          return;
        }
        shared.RefreshStatusFromDisk(null);
      });
    }

    // Token: 0x06000918 RID: 2328 RVA: 0x005445F8 File Offset: 0x005445F8
    private static FrameworkElement TwoButtons(string leftTitle, Action left, string rightTitle, GelAppearance rightAppearance, Action right, bool rightEnabled)
    {
      Grid grid = new Grid();
      grid.ColumnSpacing = 10.0;
      grid.MaxWidth = 320.0;
      grid.Margin = new Thickness(0.0, 18.0, 0.0, 0.0);
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      GelButton gelButton = new GelButton(leftTitle, GelAppearance.SecondaryOutline, GelButton.Sizing.Large, 100.0, null, null, null)
      {
        HorizontalAlignment = HorizontalAlignment.Stretch
      };
      gelButton.Click += left;
      GelButton r = new GelButton(rightTitle, rightAppearance, GelButton.Sizing.Large, 100.0, null, null, null)
      {
        HorizontalAlignment = HorizontalAlignment.Stretch
      };
      r.IsEnabledGel = rightEnabled;
      r.Click += delegate
      {
        if (r.IsEnabledGel)
        {
          right();
        }
      };
      Grid.SetColumn(r, 1);
      grid.Children.Add(gelButton);
      grid.Children.Add(r);
      return grid;
    }

    // Token: 0x06000919 RID: 2329 RVA: 0x00544738 File Offset: 0x00544738
    private FrameworkElement AgentsPage()
    {
      AppSettings appSettings = this.host.Current;
      string @default = AgentFolders.Default;
      string agentFolderPath = appSettings.AgentFolderPath;
      string text = ((agentFolderPath != null) ? AgentFolders.Abbreviate(agentFolderPath) : "HeyClicky default");
      List<FrameworkElement> list = new List<FrameworkElement> { SK.ActionRow("Agent folder", "Where agents spawn and put the things they make.", delegate
      {
        this.PickAgentFolderAsync();
      }, text, "chevron", false) };
      if (appSettings.AgentFolderPath != null)
      {
        list.Add(SK.ActionRow("Reset to default folder", AgentFolders.Abbreviate(@default), delegate
        {
          this.host.Set("agent_folder", "default", delegate(AppSettings x)
          {
            x.AgentFolderPath = null;
          });
        }, null, null, false));
      }
      string text2 = "Agents";
      string text3 = "Where agents work and how much rope they get.";
      FrameworkElement[] array = new FrameworkElement[4];
      array[0] = SK.Group("Workspace", "The default is in your local app data, so OneDrive doesn't sync agents' scratch files.", list, 14.0, null);
      int num = 1;
      string text4 = "Autonomy";
      string text5 = null;
      FrameworkElement[] array2 = new FrameworkElement[3];
      array2[0] = SK.ToggleRow("Auto-approve extra usage", "Let long tasks keep running without asking each time. Uses more of your plan's agent messages.", appSettings.AgentAutoApproveExtraUsage, delegate(bool v)
      {
        this.Quiet("auto_approve_extra_usage", v, delegate(AppSettings x)
        {
          x.AgentAutoApproveExtraUsage = v;
        });
      });
      array2[1] = SK.ToggleRow("Always approve file uploads", "Let jobs add files from your PC to websites without asking each turn. Keys, keychains and browser profiles stay off limits.", appSettings.AgentAlwaysApproveFileUploads, delegate(bool v)
      {
        this.Quiet("always_approve_file_uploads", v, delegate(AppSettings x)
        {
          x.AgentAlwaysApproveFileUploads = v;
        });
      });
      array[num] = SK.Group(text4, text5, new <>z__ReadOnlyArray<FrameworkElement>(array2), 14.0, null);
      array[2] = SK.Group("Announcements", "HeyClicky stays quiet while you're on a call or sharing your screen, and in Do Not Disturb your scheduled routines finish quietly. Finished work waits in the chat and the notch shows how many jobs have something new.", new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
      {
        SK.ToggleRow("Speak when a job starts or finishes", "HeyClicky's voice tells you what a job got done.", appSettings.AgentAnnouncementsVoiceEnabled, delegate(bool v)
        {
          this.Quiet("agent_voice_announcements", v, delegate(AppSettings x)
          {
            x.AgentAnnouncementsVoiceEnabled = v;
          });
        }),
        SK.ToggleRow("Show updates beside the cursor", "A job's reply streams in a bubble next to your cursor when it finishes.", appSettings.AgentCursorTextAnnouncementsEnabled, delegate(bool v)
        {
          this.Quiet("agent_cursor_text_announcements", v, delegate(AppSettings x)
          {
            x.AgentCursorTextAnnouncementsEnabled = v;
          });
        })
      }), 14.0, null);
      array[3] = SK.Group("What agents can reach", "Windows privacy settings HeyClicky's agents inherit. Tap one to review it in Windows Settings.", this.PermissionRows(), 14.0, null);
      return SK.Scaffold(text2, text3, new <>z__ReadOnlyArray<FrameworkElement>(array), 20.0);
    }

    // Token: 0x0600091A RID: 2330 RVA: 0x0054490C File Offset: 0x0054490C
    private async Task PickAgentFolderAsync()
    {
      try
      {
        SettingsContent.<>c__DisplayClass12_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass12_0();
        FolderPicker folderPicker = new FolderPicker();
        folderPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
        folderPicker.CommitButtonText = "Use Folder";
        folderPicker.FileTypeFilter.Add("*");
        InitializeWithWindow.Initialize(folderPicker, Native.GetForegroundWindow());
        StorageFolder storageFolder = await folderPicker.PickSingleFolderAsync();
        if (storageFolder != null)
        {
          CS$<>8__locals1.path = storageFolder.Path;
          this.host.Set("agent_folder", CS$<>8__locals1.path, delegate(AppSettings x)
          {
            x.AgentFolderPath = CS$<>8__locals1.path;
          });
          CS$<>8__locals1 = null;
        }
      }
      catch (Exception ex)
      {
        Log.Warn("settings", "agent folder picker: " + ex.Message);
      }
    }

    // Token: 0x0600091B RID: 2331 RVA: 0x00544950 File Offset: 0x00544950
    [return: Nullable(new byte[] { 1, 2 })]
    private List<FrameworkElement> PermissionRows()
    {
      List<FrameworkElement> list = new List<FrameworkElement>();
      ValueTuple<string, string, string, string, bool>[] privacyCapabilities = SettingsContent.PrivacyCapabilities;
      for (int i = 0; i < privacyCapabilities.Length; i++)
      {
        ValueTuple<string, string, string, string, bool> valueTuple = privacyCapabilities[i];
        string item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        string item3 = valueTuple.Item3;
        string uri = valueTuple.Item4;
        bool item4 = valueTuple.Item5;
        string text = SettingsContent.ConsentStatus(item);
        if (item4 || !(text != "GRANTED"))
        {
          list.Add(this.PermissionRow(item2, item3, item4 ? "Folders an agent opens or writes to." : null, item4 ? null : text, delegate
          {
            this.host.OpenUrl(uri);
          }));
        }
      }
      if (list.Count == 0)
      {
        list.Add(SK.Row("Nothing granted yet", "Agents will ask when a task needs access to your data.", null, null));
      }
      return list;
    }

    // Token: 0x0600091C RID: 2332 RVA: 0x00544A38 File Offset: 0x00544A38
    private static string ConsentStatus(string capability)
    {
      string text3;
      try
      {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\CapabilityAccessManager\\ConsentStore\\" + capability))
        {
          using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\CapabilityAccessManager\\ConsentStore\\" + capability + "\\NonPackaged"))
          {
            string text = ((registryKey != null) ? registryKey.GetValue("Value") : null) as string;
            string text2 = ((registryKey2 != null) ? registryKey2.GetValue("Value") : null) as string;
            if (text == "Deny" || text2 == "Deny")
            {
              text3 = "DENIED";
            }
            else if (text == "Allow" && text2 != "Deny")
            {
              text3 = "GRANTED";
            }
            else
            {
              text3 = "NOT GRANTED";
            }
          }
        }
      }
      catch
      {
        text3 = "UNKNOWN";
      }
      return text3;
    }

    // Token: 0x0600091D RID: 2333 RVA: 0x00544B3C File Offset: 0x00544B3C
    private FrameworkElement PermissionRow(string name, string glyph, [Nullable(2)] string subtitle, [Nullable(2)] string status, Action onClick)
    {
      Pressable p = new Pressable(true);
      Grid grid = new Grid
      {
        Padding = new Thickness(14.0, 10.0, 14.0, 10.0),
        ColumnSpacing = 12.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(18.0)
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.Children.Add(Icons.Glyph(glyph, 12.0, SK.TextSecondary));
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 1.5,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { Ui.Text(name, 13.0, FontWeights.Medium, SK.TextPrimary, TextAlignment.Left, 0, false, false) }
      };
      if (subtitle != null)
      {
        stackPanel.Children.Add(Ui.Text(subtitle, 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 0, false, false));
      }
      Grid.SetColumn(stackPanel, 1);
      grid.Children.Add(stackPanel);
      if (status != null && status != "UNKNOWN")
      {
        Color color = ((status == "GRANTED") ? SK.Positive : ((status == "DENIED") ? SK.Destructive : SK.TextTertiary));
        TextBlock textBlock = Ui.Text(status, 10.0, FontWeights.Bold, color, TextAlignment.Left, 0, false, false);
        textBlock.CharacterSpacing = 40;
        Border border = new Border
        {
          CornerRadius = new CornerRadius(100.0),
          Background = Ui.Brush(color.WithAlpha(0.14)),
          Padding = new Thickness(8.0, 3.5, 8.0, 3.5),
          Child = textBlock,
          VerticalAlignment = VerticalAlignment.Center
        }.Capsule();
        Grid.SetColumn(border, 2);
        grid.Children.Add(border);
      }
      p.Children.Add(grid);
      p.HoverChanged += delegate(bool h)
      {
        p.Background = Ui.Brush(this.look.Ink.WithAlpha(h ? 0.05 : 0.0));
      };
      p.Click += onClick;
      return p;
    }

    // Token: 0x0600091E RID: 2334 RVA: 0x00544DCC File Offset: 0x00544DCC
    public void OnBrowserConnectionChanged()
    {
      if (this.page != SettingsWindow.Page.Integrations || this.browserCardHost == null)
      {
        return;
      }
      this.FillBrowserCard();
    }

    // Token: 0x0600091F RID: 2335 RVA: 0x00544DE8 File Offset: 0x00544DE8
    private FrameworkElement BrowserConnectionSection()
    {
      this.browserCardHost = new StackPanel();
      this.FillBrowserCard();
      Task.Run(async delegate
      {
        ClickyBrowserBridge.Shared.RefreshSetupBrowserName();
        await ClickyBrowserBridge.Shared.StartIfEnabledAsync();
      });
      return this.browserCardHost;
    }

    // Token: 0x06000920 RID: 2336 RVA: 0x00544E28 File Offset: 0x00544E28
    private void FillBrowserCard()
    {
      using (MessagesPalette.Use(this.look))
      {
        if (this.browserCardHost != null)
        {
          this.browserCardHost.Children.Clear();
          bool flag = this.integrationSearch.Length == 0 || "chrome browser connection heyclicky".Contains(this.integrationSearch.Trim(), StringComparison.OrdinalIgnoreCase);
          this.browserCardHost.Visibility = (flag ? Visibility.Visible : Visibility.Collapsed);
          if (flag)
          {
            this.browserCardHost.Children.Add(SK.Group("Browser", "Works with Chrome, Edge, Brave, Arc and other Chrome-based browsers. HeyClicky's extension is in the Chrome Web Store.", this.BrowserConnectionRows(false), 14.0, null));
          }
        }
      }
    }

    // Token: 0x06000921 RID: 2337 RVA: 0x00544EEC File Offset: 0x00544EEC
    [return: Nullable(new byte[] { 1, 2 })]
    public IEnumerable<FrameworkElement> BrowserConnectionRows(bool isEmbedded = true)
    {
      SettingsContent.<BrowserConnectionRows>d__24 <BrowserConnectionRows>d__ = new SettingsContent.<BrowserConnectionRows>d__24(-2);
      <BrowserConnectionRows>d__.<>4__this = this;
      <BrowserConnectionRows>d__.<>3__isEmbedded = isEmbedded;
      return <BrowserConnectionRows>d__;
    }

    // Token: 0x06000922 RID: 2338 RVA: 0x00544F04 File Offset: 0x00544F04
    private Grid BrowserHeader(bool isEmbedded)
    {
      ClickyBrowserBridge shared = ClickyBrowserBridge.Shared;
      ChromeSetupFlow shared2 = ChromeSetupFlow.Shared;
      string browserName = shared2.BrowserName;
      Image image = new Image
      {
        Width = 32.0,
        Height = 32.0,
        Stretch = Stretch.Uniform,
        VerticalAlignment = VerticalAlignment.Center
      };
      SettingsContent.LoadBrowserIcon(image);
      StackPanel stackPanel = Ui.H(7.0, new UIElement[] { Ui.Text(browserName, 13.0, FontWeights.SemiBold, SK.TextPrimary, TextAlignment.Left, 0, false, false) });
      FrameworkElement frameworkElement = SettingsContent.BrowserStatusBadge(shared, shared2);
      if (frameworkElement != null)
      {
        frameworkElement.VerticalAlignment = VerticalAlignment.Center;
        stackPanel.Children.Add(frameworkElement);
      }
      string text;
      if (shared.ActiveCount <= 0)
      {
        if (!shared.IsConnected)
        {
          string setupFailureMessage = shared.SetupFailureMessage;
          text = ((setupFailureMessage != null && !shared2.IsBusy) ? setupFailureMessage : (shared2.IsBusy ? ("Setting it up. It connects by itself once it's added to " + browserName + ".") : (shared.IsAwaitingExtension ? ("Finish in " + browserName + ": add HeyClicky from the Chrome Web Store. It connects by itself.") : (shared2.AddsAgain ? "There's a new version of the extension that updates itself. Swap it in once and you're set." : "Let HeyClicky work in the sites you're already signed into. You can stop it anytime."))));
        }
        else
        {
          text = "HeyClicky can work in the sites you're signed into. Stop it anytime from the toolbar.";
        }
      }
      else
      {
        text = "A job is using " + browserName + " right now, in a tab group next to yours.";
      }
      string text2 = text;
      StackPanel stackPanel2 = Ui.V(3.0, new UIElement[]
      {
        stackPanel,
        Ui.Text(text2, 11.5, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 0, true, false)
      });
      stackPanel2.VerticalAlignment = VerticalAlignment.Center;
      Grid grid = new Grid
      {
        Padding = new Thickness(14.0, 12.0, 14.0, 12.0),
        ColumnSpacing = 12.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetColumn(stackPanel2, 1);
      grid.Children.Add(image);
      grid.Children.Add(stackPanel2);
      FrameworkElement frameworkElement2 = SettingsContent.BrowserHeaderAction(shared, shared2, isEmbedded);
      if (frameworkElement2 != null)
      {
        frameworkElement2.VerticalAlignment = VerticalAlignment.Center;
        frameworkElement2.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
        Grid.SetColumn(frameworkElement2, 2);
        grid.Children.Add(frameworkElement2);
      }
      return grid;
    }

    // Token: 0x06000923 RID: 2339 RVA: 0x00545194 File Offset: 0x00545194
    [return: Nullable(2)]
    private static FrameworkElement BrowserHeaderAction(ClickyBrowserBridge bridge, ChromeSetupFlow flow, bool isEmbedded)
    {
      if (bridge.ActiveCount > 0)
      {
        GelButton gelButton = new GelButton("Stop", GelAppearance.SecondaryWhite, GelButton.Sizing.Normal, 15.0, "\ue71a", null, null);
        gelButton.Click += delegate
        {
          bridge.StopAccessForUser("settings");
        };
        return gelButton;
      }
      if (!bridge.IsConnected && flow.IsBusy)
      {
        GelButton gelButton2 = new GelButton("Stop", GelAppearance.SecondaryWhite, GelButton.Sizing.Normal, 15.0, null, null, null);
        gelButton2.Click += delegate
        {
          SettingsContent.TrackBrowserSettingsAction("settings_setup_stopped", isEmbedded, null);
          flow.Stop();
        };
        return gelButton2;
      }
      if (bridge.IsConnected)
      {
        return null;
      }
      StackPanel stackPanel = Ui.H(8.0, Array.Empty<UIElement>());
      if (flow.CanDoItForThem)
      {
        GelButton gelButton3 = new GelButton("Get new extension", GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 15.0, null, null, null);
        gelButton3.Click += delegate
        {
          string text = "settings_setup_pressed";
          bool isEmbedded2 = isEmbedded;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["choice"] = "do_it_for_me";
          SettingsContent.TrackBrowserSettingsAction(text, isEmbedded2, jsonObject);
          flow.Choose(ChromeSetupFlow.Choice.DoItForMe, ChromeSetupFlow.Target.None);
        };
        stackPanel.Children.Add(gelButton3);
      }
      GelButton gelButton4 = new GelButton(flow.CanDoItForThem ? "I'll do it" : (flow.AddsAgain ? "Show me how" : ("Add to " + flow.BrowserName)), flow.CanDoItForThem ? GelAppearance.SecondaryWhite : GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 15.0, null, null, null);
      gelButton4.Click += delegate
      {
        string text2 = "settings_setup_pressed";
        bool isEmbedded3 = isEmbedded;
        JsonObject jsonObject2 = new JsonObject(null);
        jsonObject2["choice"] = "show_me_how";
        jsonObject2["do_it_for_me_offered"] = flow.CanDoItForThem;
        jsonObject2["adds_again"] = flow.AddsAgain;
        SettingsContent.TrackBrowserSettingsAction(text2, isEmbedded3, jsonObject2);
        flow.Choose(ChromeSetupFlow.Choice.ShowMeHow, ChromeSetupFlow.Target.None);
      };
      stackPanel.Children.Add(gelButton4);
      return stackPanel;
    }

    // Token: 0x06000924 RID: 2340 RVA: 0x00545368 File Offset: 0x00545368
    private static void TrackBrowserSettingsAction(string action, bool isEmbedded, [Nullable(2)] JsonObject properties = null)
    {
      ClickyBrowserBridge shared = ClickyBrowserBridge.Shared;
      if (properties == null)
      {
        properties = new JsonObject(null);
      }
      properties["action"] = action;
      properties["is_connected"] = shared.IsConnected;
      properties["helper_registered"] = shared.IsEnabled;
      properties["is_embedded"] = isEmbedded;
      ClickyAnalytics.TrackProductEvent("browser_connection_action", null, null, properties);
    }

    // Token: 0x06000925 RID: 2341 RVA: 0x005453EC File Offset: 0x005453EC
    [return: Nullable(2)]
    private static FrameworkElement BrowserStatusBadge(ClickyBrowserBridge bridge, ChromeSetupFlow flow)
    {
      ValueTuple<string, Color, bool>? valueTuple = ((bridge.ActiveCount > 0) ? new ValueTuple<string, Color, bool>?(new ValueTuple<string, Color, bool>("Working", DS.Hex(3111935U, 1.0), true)) : (bridge.IsConnected ? new ValueTuple<string, Color, bool>?(new ValueTuple<string, Color, bool>("Connected", SK.Positive, false)) : ((bridge.SetupFailureMessage != null && !flow.IsBusy) ? new ValueTuple<string, Color, bool>?(new ValueTuple<string, Color, bool>("Setup failed", SK.Destructive, false)) : ((bridge.IsAwaitingExtension || flow.IsBusy) ? new ValueTuple<string, Color, bool>?(new ValueTuple<string, Color, bool>("Waiting", SK.Warning, false)) : null))));
      if (valueTuple != null)
      {
        ValueTuple<string, Color, bool> valueOrDefault = valueTuple.GetValueOrDefault();
        Ellipse ellipse = new Ellipse
        {
          Width = 6.0,
          Height = 6.0,
          Fill = Ui.Brush(valueOrDefault.Item2),
          VerticalAlignment = VerticalAlignment.Center
        };
        if (valueOrDefault.Item3 && !MotionPreference.Reduced)
        {
          Anim.LoopScalar(ellipse, "Opacity", 1.4, delegate(double t)
          {
            double num = ((t < 0.7) ? (t / 0.7) : (2.0 - t / 0.7));
            double num2 = (1.0 - Math.Cos(3.141592653589793 * num)) / 2.0;
            return (float)(1.0 - 0.7 * num2);
          }, 48, 0.0);
        }
        TextBlock textBlock = Ui.Text(valueOrDefault.Item1, 10.5, FontWeights.SemiBold, valueOrDefault.Item2, TextAlignment.Left, 0, false, false);
        return new Border
        {
          Padding = new Thickness(7.0, 2.5, 7.0, 2.5),
          CornerRadius = new CornerRadius(100.0),
          Background = Ui.Brush(valueOrDefault.Item2.WithAlpha(0.14)),
          Child = Ui.H(5.0, new UIElement[] { ellipse, textBlock })
        }.Capsule();
      }
      return null;
    }

    // Token: 0x06000926 RID: 2342 RVA: 0x005455F8 File Offset: 0x005455F8
    private FrameworkElement BrowserDisconnectRow(string browserName, bool isEmbedded)
    {
      if (this.isConfirmingBrowserDisconnect)
      {
        GelButton gelButton = new GelButton("Cancel", GelAppearance.SecondaryWhite, GelButton.Sizing.Compact, 15.0, null, null, null);
        gelButton.Click += delegate
        {
          this.isConfirmingBrowserDisconnect = false;
          SettingsContent.TrackBrowserSettingsAction("settings_disconnect_cancelled", isEmbedded, null);
          this.FillBrowserCard();
        };
        GelButton gelButton2 = new GelButton("Disconnect", GelAppearance.SecondaryOutline, GelButton.Sizing.Compact, 15.0, null, null, null);
        gelButton2.Click += delegate
        {
          this.isConfirmingBrowserDisconnect = false;
          ClickyBrowserBridge.Shared.Disconnect();
          this.FillBrowserCard();
        };
        return SK.Row("Disconnect " + browserName + "?", "HeyClicky stops using it. You can connect again anytime.", Ui.H(8.0, new UIElement[] { gelButton, gelButton2 }), new Color?(SK.Destructive));
      }
      return SK.ActionRow("Disconnect " + browserName, null, delegate
      {
        this.isConfirmingBrowserDisconnect = true;
        SettingsContent.TrackBrowserSettingsAction("settings_disconnect_asked", isEmbedded, null);
        this.FillBrowserCard();
      }, null, null, true);
    }

    // Token: 0x06000927 RID: 2343 RVA: 0x005456FC File Offset: 0x005456FC
    private static void LoadBrowserIcon(Image image)
    {
      SettingsContent.<>c__DisplayClass30_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass30_0();
      CS$<>8__locals1.image = image;
      CS$<>8__locals1.name = ClickyBrowserBridge.Shared.SetupBrowserName;
      ImageSource imageSource;
      if (SettingsContent.browserIcons.TryGetValue(CS$<>8__locals1.name, out imageSource))
      {
        CS$<>8__locals1.image.Source = imageSource;
        return;
      }
      string text = Path.Combine(AppContext.BaseDirectory, "Assets", "AppIcon.ico");
      if (File.Exists(text))
      {
        CS$<>8__locals1.image.Source = new BitmapImage(new Uri(text));
      }
      if (SettingsContent.browserIconsLookedUp.Add(CS$<>8__locals1.name))
      {
        CS$<>8__locals1.executable = ChromiumBrowsers.ExecutablePathFor(CS$<>8__locals1.name);
        if (CS$<>8__locals1.executable != null && CS$<>8__locals1.executable.Length > 0)
        {
          CS$<>8__locals1.<LoadBrowserIcon>g__LoadAsync|0();
          return;
        }
      }
    }

    // Token: 0x06000928 RID: 2344 RVA: 0x005457BC File Offset: 0x005457BC
    [return: TupleElementNames(new string[] { "Key", "Value" })]
    [return: Nullable(new byte[] { 1, 0, 1, 1 })]
    private static List<ValueTuple<string, string>> KeyValueLines(string text, char[] separators)
    {
      List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>();
      string[] array = text.Split('\n', StringSplitOptions.None);
      for (int i = 0; i < array.Length; i++)
      {
        string text2 = array[i].Trim();
        if (text2.Length != 0 && !text2.StartsWith('#'))
        {
          int num = text2.IndexOfAny(separators);
          if (num > 0)
          {
            List<ValueTuple<string, string>> list2 = list;
            string text3 = text2.Substring(0, num).Trim();
            string text4 = text2;
            int num2 = num + 1;
            list2.Add(new ValueTuple<string, string>(text3, text4.Substring(num2, text4.Length - num2).Trim()));
          }
        }
      }
      return list;
    }

    // Token: 0x06000929 RID: 2345 RVA: 0x0054584C File Offset: 0x0054584C
    internal static List<string> ParseArguments(string line)
    {
      List<string> list = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = false;
      bool flag2 = false;
      char? c = null;
      foreach (char c2 in line)
      {
        if (flag2)
        {
          stringBuilder.Append(c2);
          flag = true;
          flag2 = false;
        }
        else
        {
          if (c2 == '\\')
          {
            char? c3 = c;
            int? num = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
            int num2 = 39;
            if (!((num.GetValueOrDefault() == num2) & (num != null)))
            {
              flag2 = true;
              goto IL_0110;
            }
          }
          if (c != null)
          {
            char valueOrDefault = c.GetValueOrDefault();
            if (c2 == valueOrDefault)
            {
              c = null;
            }
            else
            {
              stringBuilder.Append(c2);
            }
          }
          else
          {
            bool flag3 = c2 == '"' || c2 == '\'';
            if (flag3)
            {
              c = new char?(c2);
              flag = true;
            }
            else if (char.IsWhiteSpace(c2))
            {
              if (flag)
              {
                list.Add(stringBuilder.ToString());
                stringBuilder.Clear();
                flag = false;
              }
            }
            else
            {
              stringBuilder.Append(c2);
              flag = true;
            }
          }
        }
        IL_0110:;
      }
      if (flag)
      {
        list.Add(stringBuilder.ToString());
      }
      return list;
    }

    // Token: 0x0600092A RID: 2346 RVA: 0x00545990 File Offset: 0x00545990
    internal static string JoinArguments(IEnumerable<string> args)
    {
      return string.Join<string>(' ', args.Select<string, string>(delegate(string a)
      {
        if (a.Length != 0)
        {
          if (!a.Any<char>(delegate(char c)
          {
            bool flag = char.IsWhiteSpace(c);
            if (!flag)
            {
              bool flag2 = c == '"' || c == '\'' || c == '\\';
              flag = flag2;
            }
            return flag;
          }))
          {
            return a;
          }
        }
        return "\"" + a.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
      }));
    }

    // Token: 0x0600092B RID: 2347 RVA: 0x005459C0 File Offset: 0x005459C0
    private void ParkConnectorDraft()
    {
      if (this.connectorDraft == null)
      {
        return;
      }
      SettingsContent.parkedConnectorDraft = this.connectorDraft;
      SettingsContent.parkedConnectorDraftAt = DateTime.UtcNow;
      this.connectorDraft = null;
      Log.Info("settings", "custom connector draft parked");
    }

    // Token: 0x0600092C RID: 2348 RVA: 0x005459F8 File Offset: 0x005459F8
    private void RestoreParkedConnectorDraftIfFresh()
    {
      if (this.connectorDraft == null)
      {
        SettingsContent.ConnectorDraft connectorDraft = SettingsContent.parkedConnectorDraft;
        if (connectorDraft != null)
        {
          if (!(DateTime.UtcNow - SettingsContent.parkedConnectorDraftAt > SettingsContent.ParkedConnectorDraftRetention) && this.host.IsSignedIn)
          {
            string id = connectorDraft.EditingId;
            if (id == null || !CustomConnectorStore.Load().All<CustomConnector>((CustomConnector c) => c.Id != id))
            {
              this.connectorDraft = connectorDraft;
              SettingsContent.parkedConnectorDraft = null;
              Log.Info("settings", "custom connector draft restored");
              return;
            }
          }
          SettingsContent.ClearParkedConnectorDraft();
          return;
        }
      }
    }

    // Token: 0x0600092D RID: 2349 RVA: 0x00545A94 File Offset: 0x00545A94
    public static void ClearParkedConnectorDraft()
    {
      SettingsContent.parkedConnectorDraft = null;
      SettingsContent.parkedConnectorDraftAt = default(DateTime);
    }

    // Token: 0x0600092E RID: 2350 RVA: 0x00545AA8 File Offset: 0x00545AA8
    private void HookConnectorEvents()
    {
      if (this.connectorEventsHooked)
      {
        return;
      }
      this.connectorEventsHooked = true;
      ConnectorAuth.Changed += delegate
      {
        this.dq.TryEnqueue(delegate
        {
          if (this.page == SettingsWindow.Page.Integrations && this.isShown)
          {
            this.FillIntegrationList();
          }
        });
      };
      ObsidianVault.Changed += delegate
      {
        this.dq.TryEnqueue(delegate
        {
          if (this.page == SettingsWindow.Page.Integrations && this.isShown)
          {
            this.FillIntegrationList();
          }
        });
      };
    }

    // Token: 0x0600092F RID: 2351 RVA: 0x00545ADC File Offset: 0x00545ADC
    public void DemoOpenConnectorEditor()
    {
      if (this.connectorDraft == null)
      {
        this.OpenConnectorEditor(null);
      }
    }

    // Token: 0x06000930 RID: 2352 RVA: 0x00545AF0 File Offset: 0x00545AF0
    [NullableContext(2)]
    private void OpenConnectorEditor(CustomConnector existing)
    {
      if (existing == null)
      {
        SettingsContent.ConnectorDraft connectorDraft = this.connectorDraft;
        if (connectorDraft != null && connectorDraft.EditingId == null)
        {
          this.connectorDraft = null;
          this.Render();
          return;
        }
      }
      this.connectorDraft = new SettingsContent.ConnectorDraft();
      if (existing != null)
      {
        CustomConnectorSecrets secrets = CustomConnectorStore.ReadSecrets(existing.Id);
        this.connectorDraft = new SettingsContent.ConnectorDraft
        {
          EditingId = existing.Id,
          Existing = existing,
          Name = existing.Name,
          Remote = existing.IsRemote,
          Url = (existing.Url ?? ""),
          Command = (existing.Command ?? ""),
          Arguments = SettingsContent.JoinArguments(existing.Arguments),
          Auth = (existing.UsesPlainBearerToken ? "apiKey" : "browser"),
          ApiKey = (existing.UsesPlainBearerToken ? (secrets.BearerToken ?? "") : ""),
          OAuthClientId = existing.TrimmedOAuthClientId,
          Headers = string.Join("\n", existing.HeaderNames.Select<string, string>((string n) => n + ": " + secrets.Headers.GetValueOrDefault(n, ""))),
          Environment = string.Join("\n", existing.EnvironmentNames.Select<string, string>((string n) => n + "=" + secrets.Environment.GetValueOrDefault(n, "")))
        };
        this.connectorDraft.ShowAdvanced = this.connectorDraft.UsesAdvanced;
      }
      Log.Info("settings", (existing == null) ? "custom connector: add" : ("custom connector: edit " + existing.Name));
      this.Render();
    }

    // Token: 0x06000931 RID: 2353 RVA: 0x00545C98 File Offset: 0x00545C98
    [NullableContext(2)]
    private FrameworkElement ConnectorEditor()
    {
      SettingsContent.ConnectorDraft d = this.connectorDraft;
      if (d == null)
      {
        return null;
      }
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 14.0,
        Padding = new Thickness(14.0)
      };
      string text = ((d.EditingId == null) ? "New connector" : ("Edit " + ((d.Name.Trim().Length == 0) ? "connector" : d.Name.Trim())));
      stackPanel.Children.Add(SettingsContent.Field("Name", "e.g. Meta Ads", d.Name, delegate(string t)
      {
        d.Name = t;
      }, false, false, null));
      if (d.Remote)
      {
        stackPanel.Children.Add(SettingsContent.Field("Server URL", "e.g. https://mcp.example.com/mcp", d.Url, delegate(string t)
        {
          d.Url = t;
        }, false, false, null));
        StackPanel stackPanel2 = Ui.H(6.0, new UIElement[]
        {
          SK.Chip("Sign in with browser", d.Auth == "browser", false, delegate
          {
            d.Auth = "browser";
            this.Render();
          }),
          SK.Chip("API key", d.Auth == "apiKey", false, delegate
          {
            d.Auth = "apiKey";
            this.Render();
          }),
          SK.Chip("None", d.Auth == "none", false, delegate
          {
            d.Auth = "none";
            this.Render();
          })
        });
        stackPanel.Children.Add(SettingsContent.Labeled("Authentication", stackPanel2, (d.Auth == "browser") ? "You'll sign in through your browser after adding it." : null));
        if (d.Auth == "apiKey")
        {
          stackPanel.Children.Add(SettingsContent.Field("API key", "Paste your key", d.ApiKey, delegate(string t)
          {
            d.ApiKey = t;
          }, true, false, null));
        }
      }
      else
      {
        stackPanel.Children.Add(SettingsContent.Field("Command", "e.g. npx", d.Command, delegate(string t)
        {
          d.Command = t;
        }, false, false, null));
        stackPanel.Children.Add(SettingsContent.Field("Arguments", "e.g. -y some-mcp-server --flag value", d.Arguments, delegate(string t)
        {
          d.Arguments = t;
        }, false, false, "Everything after the command, quoted like a shell."));
      }
      Pressable pressable = new Pressable(true)
      {
        HorizontalAlignment = HorizontalAlignment.Left
      };
      TextBlock textBlock = Icons.Glyph("\ue76c", 9.0, SK.TextSecondary);
      textBlock.FontWeight = FontWeights.Bold;
      textBlock.RenderTransformOrigin = new Point(0.5, 0.5);
      textBlock.RenderTransform = new RotateTransform
      {
        Angle = (double)(d.ShowAdvanced ? 90 : 0)
      };
      pressable.Children.Add(Ui.H(6.0, new UIElement[]
      {
        textBlock,
        Ui.Text("Advanced", 12.0, FontWeights.SemiBold, SK.TextSecondary, TextAlignment.Left, 0, false, false)
      }));
      pressable.Click += delegate
      {
        d.ShowAdvanced = !d.ShowAdvanced;
        this.Render();
      };
      stackPanel.Children.Add(pressable);
      if (d.ShowAdvanced)
      {
        stackPanel.Children.Add(SettingsContent.Labeled("Type", Ui.H(6.0, new UIElement[]
        {
          SK.Chip("Remote URL", d.Remote, false, delegate
          {
            d.Remote = true;
            this.Render();
          }),
          SK.Chip("Command on this PC", !d.Remote, false, delegate
          {
            d.Remote = false;
            this.Render();
          })
        }), null));
        if (d.Remote)
        {
          stackPanel.Children.Add(SettingsContent.Field("OAuth client ID", "e.g. 123456789012345", d.OAuthClientId, delegate(string t)
          {
            d.OAuthClientId = t;
          }, false, false, "Only for servers that make you register an app with them. For Meta Ads, paste your Meta App ID and add this redirect URL to the app's Facebook Login settings: " + McpOAuth.RedirectUri(48611)));
          stackPanel.Children.Add(SettingsContent.Field("Extra headers", "e.g. X-Api-Key: …", d.Headers, delegate(string t)
          {
            d.Headers = t;
          }, false, true, "One per line, like X-Api-Key: value."));
        }
        else
        {
          stackPanel.Children.Add(SettingsContent.Field("Environment variables", "e.g. API_TOKEN=…", d.Environment, delegate(string t)
          {
            d.Environment = t;
          }, false, true, "One per line, like API_TOKEN=value. Kept encrypted for your Windows account."));
        }
      }
      if (d.AttemptedSave)
      {
        string text2 = d.ValidationProblem ?? d.Problem;
        if (text2 != null)
        {
          stackPanel.Children.Add(SK.Alert(text2, SK.Destructive));
          goto IL_0541;
        }
      }
      string advisoryNote = d.AdvisoryNote;
      if (advisoryNote != null)
      {
        stackPanel.Children.Add(SK.Alert(advisoryNote, SK.Warning));
      }
      IL_0541:
      GelButton gelButton = new GelButton("Cancel", GelAppearance.SecondaryWhite, GelButton.Sizing.Normal, 15.0, null, null, null);
      gelButton.Click += delegate
      {
        this.connectorDraft = null;
        SettingsContent.ClearParkedConnectorDraft();
        this.Render();
      };
      GelButton gelButton2 = new GelButton((d.EditingId == null) ? "Add connector" : "Save changes", GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 15.0, null, null, null);
      gelButton2.Click += this.SaveConnectorDraft;
      StackPanel stackPanel3 = Ui.H(8.0, new UIElement[] { gelButton, gelButton2 });
      stackPanel3.HorizontalAlignment = HorizontalAlignment.Right;
      stackPanel.Children.Add(stackPanel3);
      return SK.Group(text, new FrameworkElement[] { stackPanel });
    }

    // Token: 0x06000932 RID: 2354 RVA: 0x005462C0 File Offset: 0x005462C0
    private static FrameworkElement Labeled(string label, FrameworkElement control, [Nullable(2)] string detail)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 5.0
      };
      stackPanel.Children.Add(Ui.Text(label, 12.0, FontWeights.SemiBold, SK.TextPrimary, TextAlignment.Left, 0, false, false));
      stackPanel.Children.Add(control);
      if (detail != null)
      {
        stackPanel.Children.Add(Ui.Text(detail, 10.5, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, true, false));
      }
      return stackPanel;
    }

    // Token: 0x06000933 RID: 2355 RVA: 0x00546344 File Offset: 0x00546344
    private static FrameworkElement Field(string label, string placeholder, string value, Action<string> changed, bool secret = false, bool multiline = false, [Nullable(2)] string detail = null)
    {
      FrameworkElement frameworkElement;
      if (secret)
      {
        PasswordBox box = new PasswordBox
        {
          Password = value,
          PlaceholderText = placeholder,
          FontSize = 12.0,
          CornerRadius = new CornerRadius(9.0),
          BorderThickness = new Thickness(0.0),
          MinHeight = 0.0,
          Padding = new Thickness(11.0, 6.0, 9.0, 6.0)
        };
        box.Resources["TextControlBackground"] = (box.Resources["TextControlBackgroundPointerOver"] = (box.Resources["TextControlBackgroundFocused"] = Ui.Brush(SK.Ink.WithAlpha(0.07))));
        box.PasswordChanged += delegate(object _, RoutedEventArgs _)
        {
          changed(box.Password);
        };
        frameworkElement = box;
      }
      else
      {
        ValueTuple<Grid, TextBox> valueTuple = SK.SearchField(placeholder, value, changed, false, 12.5, new Color?(SK.TextTertiary.WithAlpha(0.7)));
        Grid item = valueTuple.Item1;
        TextBox item2 = valueTuple.Item2;
        item2.FontFamily = DS.Fonts.Mono;
        item2.Padding = new Thickness(11.0, 7.0, 11.0, 7.0);
        if (multiline)
        {
          item2.AcceptsReturn = true;
          item2.TextWrapping = TextWrapping.Wrap;
          item2.MinHeight = 48.0;
        }
        frameworkElement = item;
      }
      return SettingsContent.Labeled(label, frameworkElement, detail);
    }

    // Token: 0x06000934 RID: 2356 RVA: 0x00546520 File Offset: 0x00546520
    private void SaveConnectorDraft()
    {
      SettingsContent.ConnectorDraft connectorDraft = this.connectorDraft;
      if (connectorDraft == null)
      {
        return;
      }
      connectorDraft.AttemptedSave = true;
      connectorDraft.Problem = null;
      string validationProblem = connectorDraft.ValidationProblem;
      if (validationProblem != null)
      {
        Log.Info("settings", "custom connector not saved: " + validationProblem);
        this.Render();
        return;
      }
      CustomConnectorSecrets customConnectorSecrets = new CustomConnectorSecrets();
      if (connectorDraft.Remote)
      {
        if (connectorDraft.Auth == "apiKey")
        {
          customConnectorSecrets.BearerToken = connectorDraft.ApiKey.Trim();
        }
        else
        {
          CustomConnector existing = connectorDraft.Existing;
          if (existing != null && existing.UsesBrowserSignIn && existing.HasApiKey && connectorDraft.Auth == "browser")
          {
            customConnectorSecrets.BearerToken = CustomConnectorStore.ReadSecrets(existing.Id).BearerToken;
          }
        }
        using (List<ValueTuple<string, string>>.Enumerator enumerator = connectorDraft.ParsedHeaders.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            ValueTuple<string, string> valueTuple = enumerator.Current;
            string item = valueTuple.Item1;
            string item2 = valueTuple.Item2;
            if (item2.Length > 0)
            {
              customConnectorSecrets.Headers[item] = item2;
            }
          }
          goto IL_016A;
        }
      }
      foreach (ValueTuple<string, string> valueTuple2 in connectorDraft.ParsedEnvironment)
      {
        string item3 = valueTuple2.Item1;
        string item4 = valueTuple2.Item2;
        if (item4.Length > 0)
        {
          customConnectorSecrets.Environment[item3] = item4;
        }
      }
      IL_016A:
      string text = connectorDraft.OAuthClientId.Trim();
      CustomConnector customConnector = new CustomConnector(connectorDraft.EditingId ?? Guid.NewGuid().ToString("N").Substring(0, 12), connectorDraft.Name.Trim(), connectorDraft.Remote ? "remote" : "stdio", connectorDraft.Remote ? connectorDraft.Url.Trim() : null, connectorDraft.Remote ? null : connectorDraft.Command.Trim(), connectorDraft.Remote ? new List<string>() : SettingsContent.ParseArguments(connectorDraft.Arguments), Array.Empty<string>(), false);
      customConnector.OAuthClientId = ((connectorDraft.Remote && text.Length > 0) ? text : null);
      CustomConnector existing2 = connectorDraft.Existing;
      customConnector.DerivedCapabilityDescription = ((existing2 != null) ? existing2.DerivedCapabilityDescription : null);
      CustomConnector existing3 = connectorDraft.Existing;
      customConnector.TokenFromSignIn = existing3 != null && existing3.TokenFromSignIn && connectorDraft.Auth == "browser";
      CustomConnector customConnector2 = customConnector;
      bool flag;
      string text2 = CustomConnectorStore.Save(customConnector2, customConnectorSecrets, out flag);
      if (text2 != null)
      {
        Log.Info("settings", "custom connector not saved: " + text2);
        connectorDraft.Problem = text2;
        this.Render();
        return;
      }
      string text3 = "settings";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 4);
      defaultInterpolatedStringHandler.AppendLiteral("custom connector ");
      defaultInterpolatedStringHandler.AppendFormatted(flag ? "added" : "edited");
      defaultInterpolatedStringHandler.AppendLiteral(": ");
      defaultInterpolatedStringHandler.AppendFormatted(customConnector2.Name);
      defaultInterpolatedStringHandler.AppendLiteral(" (auth ");
      defaultInterpolatedStringHandler.AppendFormatted(connectorDraft.Auth);
      string text4;
      if (!(connectorDraft.Auth == "apiKey"))
      {
        text4 = "";
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(11, 1);
        defaultInterpolatedStringHandler2.AppendLiteral(", ");
        defaultInterpolatedStringHandler2.AppendFormatted<int>(connectorDraft.ApiKey.Trim().Length);
        defaultInterpolatedStringHandler2.AppendLiteral("-char key");
        text4 = defaultInterpolatedStringHandler2.ToStringAndClear();
      }
      defaultInterpolatedStringHandler.AppendFormatted(text4);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
      string text5 = "custom_connector_action";
      string text6 = null;
      string text7 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = (flag ? "add" : "edit");
      jsonObject["connector_id"] = customConnector2.Id;
      jsonObject["transport"] = (connectorDraft.Remote ? "remote" : "local");
      ClickyAnalytics.TrackProductEvent(text5, text6, text7, jsonObject);
      if (flag)
      {
        ClickyAnalytics.TrackIntegrationConnected(customConnector2.IntegrationId, customConnector2.Name, new <>z__ReadOnlySingleElementList<string>(customConnector2.ServerKey), "agent_space_settings", "connected", new int?(1), new int?(1));
      }
      this.connectorDraft = null;
      SettingsContent.ClearParkedConnectorDraft();
      ConnectorAuth.AfterSave(customConnector2.Id);
      this.Render(true);
    }

    // Token: 0x06000935 RID: 2357 RVA: 0x00546998 File Offset: 0x00546998
    private List<FrameworkElement> CustomConnectorRows()
    {
      List<FrameworkElement> list = new List<FrameworkElement>();
      foreach (CustomConnector customConnector in from c in CustomConnectorStore.Load()
        orderby c.CreatedAt
        select c)
      {
        list.Add(this.CustomConnectorRow(customConnector));
      }
      return list;
    }

    // Token: 0x06000936 RID: 2358 RVA: 0x00546A18 File Offset: 0x00546A18
    private static ConnectorSignInState EffectiveState(CustomConnector c)
    {
      ConnectorSignInState connectorSignInState = ConnectorAuth.StateFor(c.Id);
      if (!c.UsesPlainBearerToken)
      {
        return connectorSignInState;
      }
      ConnectorSignInKind kind = connectorSignInState.Kind;
      if (kind != ConnectorSignInKind.Checking && kind != ConnectorSignInKind.NotNeeded && kind - ConnectorSignInKind.Failed > 1)
      {
        return new ConnectorSignInState(ConnectorSignInKind.NotNeeded, null);
      }
      return connectorSignInState;
    }

    // Token: 0x06000937 RID: 2359 RVA: 0x00546A64 File Offset: 0x00546A64
    [return: TupleElementNames(new string[] { "Text", "Color" })]
    [return: Nullable(new byte[] { 0, 1 })]
    private static ValueTuple<string, Color> ConnectorStatus(CustomConnector c, ConnectorSignInState s)
    {
      ValueTuple<string, Color> valueTuple;
      switch (s.Kind)
      {
      case ConnectorSignInKind.Checking:
        valueTuple = new ValueTuple<string, Color>("Checking", SK.Warning);
        break;
      case ConnectorSignInKind.SignInUnavailable:
        valueTuple = new ValueTuple<string, Color>("Needs a token", SK.Warning);
        break;
      case ConnectorSignInKind.NotNeeded:
      case ConnectorSignInKind.SignedIn:
        valueTuple = new ValueTuple<string, Color>("Connected", SK.Positive);
        break;
      case ConnectorSignInKind.NeedsSignIn:
        valueTuple = new ValueTuple<string, Color>("Needs sign-in", SK.Warning);
        break;
      case ConnectorSignInKind.AwaitingBrowser:
        valueTuple = new ValueTuple<string, Color>("Signing in", SK.Warning);
        break;
      case ConnectorSignInKind.Failed:
        valueTuple = new ValueTuple<string, Color>(c.UsesPlainBearerToken ? "Check failed" : "Sign-in failed", SK.Destructive);
        break;
      case ConnectorSignInKind.TokenRejected:
        valueTuple = new ValueTuple<string, Color>("Token rejected", SK.Destructive);
        break;
      default:
        if (c.UsesBrowserSignIn && c.HasApiKey)
        {
          valueTuple = new ValueTuple<string, Color>("Connected", SK.Positive);
        }
        else if (c.TrimmedOAuthClientId.Length > 0 || c.BrowserSignInSupport == "supported")
        {
          valueTuple = new ValueTuple<string, Color>("Needs sign-in", SK.Warning);
        }
        else if (c.BrowserSignInSupport == "unsupported")
        {
          valueTuple = new ValueTuple<string, Color>("Needs a token", SK.Warning);
        }
        else
        {
          valueTuple = new ValueTuple<string, Color>("Connected", SK.Positive);
        }
        break;
      }
      return valueTuple;
    }

    // Token: 0x06000938 RID: 2360 RVA: 0x00546BD0 File Offset: 0x00546BD0
    private FrameworkElement CustomConnectorRow(CustomConnector c)
    {
      ConnectorSignInState connectorSignInState = SettingsContent.EffectiveState(c);
      ValueTuple<string, Color> valueTuple = SettingsContent.ConnectorStatus(c, connectorSignInState);
      string item = valueTuple.Item1;
      Color item2 = valueTuple.Item2;
      FrameworkElement frameworkElement = this.ConnectorGlyph(c, 26.0);
      StackPanel stackPanel = Ui.H(7.0, new UIElement[]
      {
        Ui.Text(c.Name, 13.0, FontWeights.Medium, SK.TextPrimary, TextAlignment.Left, 1, false, false),
        SettingsContent.StatusChip(item, item2)
      });
      ConnectorSignInKind kind = connectorSignInState.Kind;
      bool flag = kind == ConnectorSignInKind.SignInUnavailable || kind - ConnectorSignInKind.Failed <= 1;
      string text = (flag ? connectorSignInState.Text : null);
      string effectiveCapabilityDescription = c.EffectiveCapabilityDescription;
      string text2 = ((connectorSignInState.Kind == ConnectorSignInKind.AwaitingBrowser) ? "Finish connecting in your browser." : ((effectiveCapabilityDescription.Length == 0) ? c.LocationSummary : (effectiveCapabilityDescription + " · " + c.LocationSummary)));
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 1.5,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          stackPanel,
          Ui.Text(text2, 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 2, false, false)
        }
      };
      if (text != null)
      {
        stackPanel2.Children.Add(Ui.Text(text, 10.5, FontWeights.Medium, SK.Destructive, TextAlignment.Left, 0, true, false));
      }
      FrameworkElement frameworkElement2;
      if (this.confirmRemoveConnector == c.Id)
      {
        GelButton gelButton = new GelButton("Remove", GelAppearance.SecondaryWhite, GelButton.Sizing.Normal, 15.0, null, null, null);
        gelButton.Click += delegate
        {
          this.confirmRemoveConnector = null;
          SettingsContent.ConnectorDraft connectorDraft = this.connectorDraft;
          if (((connectorDraft != null) ? connectorDraft.EditingId : null) == c.Id)
          {
            this.connectorDraft = null;
          }
          string text3 = "custom_connector_action";
          string text4 = null;
          string text5 = null;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["action"] = "remove";
          jsonObject["connector_id"] = c.Id;
          ClickyAnalytics.TrackProductEvent(text3, text4, text5, jsonObject);
          ClickyAnalytics.TrackIntegrationDisconnected(c.IntegrationId, c.Name, new <>z__ReadOnlySingleElementList<string>(c.ServerKey), "agent_space_settings", ConnectorAuth.NeedsReconnect(c.Id) ? "needs_reconnect" : "connected", new int?(1), new int?(1), new int?(1));
          CustomConnectorStore.Remove(c.Id);
          ConnectorAuth.Removed(c.Id);
          this.Render();
        };
        GelButton gelButton2 = new GelButton("Keep", GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 15.0, null, null, null);
        gelButton2.Click += delegate
        {
          this.confirmRemoveConnector = null;
          this.Render();
        };
        frameworkElement2 = Ui.H(6.0, new UIElement[] { gelButton, gelButton2 });
      }
      else
      {
        StackPanel stackPanel3 = Ui.H(8.0, Array.Empty<UIElement>());
        FrameworkElement frameworkElement3 = this.SignInControl(c, connectorSignInState);
        if (frameworkElement3 != null)
        {
          stackPanel3.Children.Add(frameworkElement3);
        }
        GelButton gelButton3 = new GelButton("Edit", GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 15.0, null, null, null);
        gelButton3.Click += delegate
        {
          this.OpenConnectorEditor(c);
        };
        GelButton gelButton4 = new GelButton("Remove", GelAppearance.SecondaryWhite, GelButton.Sizing.Normal, 15.0, null, null, null);
        gelButton4.Click += delegate
        {
          this.confirmRemoveConnector = c.Id;
          this.Render();
        };
        stackPanel3.Children.Add(gelButton3);
        stackPanel3.Children.Add(gelButton4);
        frameworkElement2 = stackPanel3;
      }
      Grid grid = new Grid
      {
        Padding = new Thickness(14.0, 10.0, 14.0, 10.0),
        ColumnSpacing = 12.0,
        Tag = "connector:" + c.Id
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(26.0)
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      frameworkElement.VerticalAlignment = VerticalAlignment.Center;
      grid.Children.Add(frameworkElement);
      Grid.SetColumn(stackPanel2, 1);
      grid.Children.Add(stackPanel2);
      frameworkElement2.VerticalAlignment = VerticalAlignment.Center;
      Grid.SetColumn(frameworkElement2, 2);
      grid.Children.Add(frameworkElement2);
      Grid hover = new Grid
      {
        Background = Ui.Transparent,
        Children = { grid }
      };
      hover.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        hover.Background = Ui.Brush(this.look.Ink.WithAlpha(0.04));
      };
      hover.PointerExited += delegate(object _, PointerRoutedEventArgs e)
      {
        if (PointerFrames.IsStale(e))
        {
          return;
        }
        hover.Background = Ui.Transparent;
      };
      return hover;
    }

    // Token: 0x06000939 RID: 2361 RVA: 0x00547054 File Offset: 0x00547054
    [return: Nullable(2)]
    private FrameworkElement SignInControl(CustomConnector c, ConnectorSignInState s)
    {
      SettingsContent.<>c__DisplayClass55_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass55_0();
      CS$<>8__locals1.c = c;
      CS$<>8__locals1.<>4__this = this;
      bool usesBrowserSignIn = CS$<>8__locals1.c.UsesBrowserSignIn;
      bool flag = CS$<>8__locals1.c.UsesPlainBearerToken;
      if (flag)
      {
        ConnectorSignInKind kind = s.Kind;
        bool flag2 = kind - ConnectorSignInKind.Failed <= 1;
        flag = flag2;
      }
      bool flag3 = flag;
      if (!usesBrowserSignIn && !flag3)
      {
        return null;
      }
      FrameworkElement frameworkElement;
      switch (s.Kind)
      {
      case ConnectorSignInKind.Checking:
        frameworkElement = Ui.Text(CS$<>8__locals1.c.UsesPlainBearerToken ? "Checking…" : "Checking sign-in…", 11.0, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, false, false);
        break;
      case ConnectorSignInKind.SignInUnavailable:
        frameworkElement = SettingsContent.<SignInControl>g__Button|55_0("Add token", new Action(CS$<>8__locals1.<SignInControl>g__Edit|2));
        break;
      case ConnectorSignInKind.NotNeeded:
        frameworkElement = null;
        break;
      case ConnectorSignInKind.NeedsSignIn:
        frameworkElement = SettingsContent.<SignInControl>g__Button|55_0("Sign in", new Action(CS$<>8__locals1.<SignInControl>g__SignIn|1));
        break;
      case ConnectorSignInKind.AwaitingBrowser:
        frameworkElement = Ui.Text("Finish in your browser…", 11.0, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, false, false);
        break;
      case ConnectorSignInKind.SignedIn:
        frameworkElement = SettingsContent.<SignInControl>g__Button|55_0("Sign in again", new Action(CS$<>8__locals1.<SignInControl>g__SignIn|1));
        break;
      case ConnectorSignInKind.Failed:
        frameworkElement = SettingsContent.<SignInControl>g__Button|55_0("Try again", CS$<>8__locals1.c.UsesPlainBearerToken ? delegate
        {
          ConnectorAuth.VerifyToken(CS$<>8__locals1.c.Id, true);
        } : new Action(CS$<>8__locals1.<SignInControl>g__SignIn|1));
        break;
      case ConnectorSignInKind.TokenRejected:
        frameworkElement = SettingsContent.<SignInControl>g__Button|55_0("Edit token", new Action(CS$<>8__locals1.<SignInControl>g__Edit|2));
        break;
      default:
        if (CS$<>8__locals1.c.HasApiKey)
        {
          frameworkElement = SettingsContent.<SignInControl>g__Button|55_0("Sign in again", new Action(CS$<>8__locals1.<SignInControl>g__SignIn|1));
        }
        else if (CS$<>8__locals1.c.TrimmedOAuthClientId.Length > 0 || CS$<>8__locals1.c.BrowserSignInSupport == "supported")
        {
          frameworkElement = SettingsContent.<SignInControl>g__Button|55_0("Sign in", new Action(CS$<>8__locals1.<SignInControl>g__SignIn|1));
        }
        else if (CS$<>8__locals1.c.BrowserSignInSupport == "unsupported")
        {
          frameworkElement = SettingsContent.<SignInControl>g__Button|55_0("Add token", new Action(CS$<>8__locals1.<SignInControl>g__Edit|2));
        }
        else
        {
          frameworkElement = null;
        }
        break;
      }
      return frameworkElement;
    }

    // Token: 0x0600093A RID: 2362 RVA: 0x005472A4 File Offset: 0x005472A4
    private FrameworkElement ConnectorGlyph(CustomConnector c, double size)
    {
      Border border = new Border
      {
        Width = size,
        Height = size,
        CornerRadius = new CornerRadius(size * 0.27),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.1)),
        Child = Icons.Glyph(c.IsRemote ? "\ue774" : "\ue756", size * 0.46, SK.TextSecondary)
      };
      string text = c.LogoUrl(this.host.WorkerBaseUrl);
      if (text == null)
      {
        return border;
      }
      return IntegrationLogos.Remote(text, size, border);
    }

    // Token: 0x0600093B RID: 2363 RVA: 0x00547348 File Offset: 0x00547348
    private FrameworkElement CursorPage()
    {
      AppSettings appSettings = this.host.Current;
      Grid grid = new Grid
      {
        ColumnSpacing = 12.0
      };
      ValueTuple<string, Color>[] array = new ValueTuple<string, Color>[]
      {
        new ValueTuple<string, Color>("Red", DS.Colors.CursorRed),
        new ValueTuple<string, Color>("Blue", DS.Colors.CursorBlue),
        new ValueTuple<string, Color>("Yellow", DS.Colors.CursorYellow),
        new ValueTuple<string, Color>("Green", DS.Colors.CursorGreen)
      };
      for (int i = 0; i < array.Length; i++)
      {
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        ValueTuple<string, Color> valueTuple = array[i];
        string name = valueTuple.Item1;
        Color item = valueTuple.Item2;
        bool flag = string.Equals(DS.ToHex(item), DS.ToHex(DS.CursorColor(appSettings.CursorColorHex)), StringComparison.OrdinalIgnoreCase);
        Grid grid2 = new Grid
        {
          Width = 30.0,
          Height = 30.0,
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center
        };
        grid2.Children.Add(Ui.GlowSpot(item, 0.45, 66.0, 0.3).Pad(-18.0));
        grid2.Children.Add(IntroMark.Cursor(30.0, item, -20.0));
        string hex = DS.ToHex(item);
        Action<AppSettings> <>9__5;
        Pressable tile = SK.Tile(grid2, flag, 20.0, delegate
        {
          SettingsHost settingsHost = this.host;
          string text = "cursor_color";
          object obj = name.ToLowerInvariant();
          Action<AppSettings> action;
          if ((action = <>9__5) == null)
          {
            action = (<>9__5 = delegate(AppSettings x)
            {
              x.CursorColorHex = hex;
            });
          }
          settingsHost.Set(text, obj, action);
          ICursorOverlay cursor = this.s.Cursor;
          if (cursor == null)
          {
            return;
          }
          cursor.SetColor(hex);
        }, 20.0, true);
        tile.SizeChanged += delegate(object _, SizeChangedEventArgs e)
        {
          if (double.IsNaN(tile.Height) || Math.Abs(tile.Height - e.NewSize.Width) > 0.5)
          {
            tile.Height = e.NewSize.Width;
          }
        };
        ToolTipService.SetToolTip(tile, name + " cursor. Sets the cursor and app accent color");
        Grid.SetColumn(tile, i);
        grid.Children.Add(tile);
      }
      Grid grid3 = new Grid();
      grid3.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star),
        MaxWidth = 520.0
      });
      grid3.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(0.0)
      });
      grid3.Children.Add(grid);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 10.0,
        Children = 
        {
          SK.MicroLabel("Color", 10.0, FontWeights.SemiBold, 110),
          grid3
        }
      };
      return SK.Scaffold("Cursor", "The HeyClicky cursor that lives on your screen.", new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
      {
        stackPanel,
        SK.Group("Visibility", null, new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
        {
          SK.ToggleRow("Hide floating cursor", "Hides the cursor and stops it from following you.", appSettings.CursorDocked, delegate(bool v)
          {
            this.quietWrites++;
            try
            {
              string text2 = "settings";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(14, 1);
              defaultInterpolatedStringHandler.AppendLiteral("dock_cursor = ");
              defaultInterpolatedStringHandler.AppendFormatted<bool>(v);
              Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
              AppHost app = this.host.App;
              if (app != null)
              {
                app.SetCursorDocked(v);
              }
              SettingsHost.TrackSettingChanged("dock_cursor", v);
            }
            finally
            {
              this.quietWrites--;
            }
            Microsoft.UI.Dispatching.DispatcherQueueTimer dispatcherQueueTimer = this.dq.CreateTimer().Rooted();
            dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(1.2);
            dispatcherQueueTimer.IsRepeating = false;
            dispatcherQueueTimer.Tick += delegate(Microsoft.UI.Dispatching.DispatcherQueueTimer _, object _)
            {
              if (this.page == SettingsWindow.Page.Cursor)
              {
                this.RenderSoon();
              }
            };
            dispatcherQueueTimer.Start();
          }),
          SK.ToggleRow("Screen highlight", "Draw on screen while holding push-to-talk.", appSettings.ScreenHighlightEnabled, delegate(bool v)
          {
            this.Quiet("screen_highlight", v, delegate(AppSettings x)
            {
              x.ScreenHighlightEnabled = v;
            });
          }),
          SK.ToggleRow("Shake to detach", "Shake the mouse to leave HeyClicky where it is; click it to talk.", appSettings.ShakeToDetachEnabled, delegate(bool v)
          {
            this.Quiet("shake_to_detach", v, delegate(AppSettings x)
            {
              x.ShakeToDetachEnabled = v;
            });
          })
        }), 14.0, null)
      }), 20.0);
    }

    // Token: 0x0600093C RID: 2364 RVA: 0x005476DC File Offset: 0x005476DC
    private FrameworkElement FreshSessionRow()
    {
      Pressable row = null;
      row = SK.ActionRow("Start a fresh session", "Drops this session's earlier turns, so your next turn starts from turn 1 with an empty conversation. The session that ends still gets its note and memory written.", delegate
      {
        AppHost app = this.host.App;
        if (app != null)
        {
          RealStack real = app.Real;
          if (real != null)
          {
            real.Companion.StartFreshSession();
          }
        }
        SettingsContent.oneBrainFreshSessionStartedAt = new DateTime?(DateTime.Now);
        Log.Info("settings", "one brain: fresh session");
        if (row != null)
        {
          TextBlock textBlock = SettingsContent.TrailingTextOf(row);
          if (textBlock != null)
          {
            textBlock.Text = SettingsContent.<FreshSessionRow>g__Trailing|60_0();
          }
        }
      }, SettingsContent.<FreshSessionRow>g__Trailing|60_0(), null, false);
      return row;
    }

    // Token: 0x0600093D RID: 2365 RVA: 0x0054772C File Offset: 0x0054772C
    [return: Nullable(2)]
    private static TextBlock TrailingTextOf(DependencyObject element)
    {
      TextBlock textBlock = element as TextBlock;
      if (textBlock != null && textBlock.MaxWidth == 220.0)
      {
        return textBlock;
      }
      Panel panel = element as Panel;
      IEnumerable<UIElement> enumerable;
      if (panel == null)
      {
        Border border = element as Border;
        if (border != null)
        {
          UIElement child = border.Child;
          if (child != null)
          {
            enumerable = new <>z__ReadOnlySingleElementList<UIElement>(child);
            goto IL_0058;
          }
        }
        enumerable = Array.Empty<UIElement>();
      }
      else
      {
        enumerable = panel.Children;
      }
      IL_0058:
      foreach (UIElement uielement in enumerable)
      {
        TextBlock textBlock2 = SettingsContent.TrailingTextOf(uielement);
        if (textBlock2 != null)
        {
          return textBlock2;
        }
      }
      return null;
    }

    // Token: 0x0600093E RID: 2366 RVA: 0x005477E0 File Offset: 0x005477E0
    private FrameworkElement NewAgentFeaturesGroup()
    {
      List<FrameworkElement> list = this.BrowserConnectionRows(true).ToList<FrameworkElement>();
      if (this.host.ShowsDeveloperSection)
      {
        list.Add(SK.ActionRow("Preview HeyClicky drive", "The card, the sounds, the pill and the glow, with nothing clicked.", delegate
        {
          AppHost app = this.host.App;
          if (app == null)
          {
            return;
          }
          app.PreviewClickyDrive();
        }, null, "\ue768", false));
      }
      return SK.Group("Chrome and HeyClicky drive", null, list, 14.0, null);
    }

    // Token: 0x0600093F RID: 2367 RVA: 0x00547848 File Offset: 0x00547848
    private FrameworkElement DeveloperPage()
    {
      AppSettings c = this.host.Current;
      Pressable pressable = SK.MenuPicker(SettingsContent.BrainModels.Select<ValueTuple<string, string>, string>(([TupleElementNames(new string[] { "Id", "Name" })] ValueTuple<string, string> m) => m.Item2).ToList<string>(), Math.Max(0, Array.FindIndex<ValueTuple<string, string>>(SettingsContent.BrainModels, ([TupleElementNames(new string[] { "Id", "Name" })] ValueTuple<string, string> m) => m.Item1 == (c.OneBrainModel ?? "gpt-6.1-sol-fast-medium"))), delegate(int index)
      {
        string id = SettingsContent.BrainModels[index].Item1;
        this.Quiet("one_brain_model", id, delegate(AppSettings x)
        {
          x.OneBrainModel = ((id == "gpt-6.1-sol-fast-medium") ? null : id);
        });
      }, null);
      IReadOnlyList<AgentHarnessModel> harnessModels = AgentHarnessNames.AllModels;
      Pressable pressable2 = SK.MenuPicker(harnessModels.Select<AgentHarnessModel, string>((AgentHarnessModel model) => model.DisplayName()).ToList<string>(), harnessModels.ToList<AgentHarnessModel>().IndexOf(AgentHarnessPreferences.Model), delegate(int index)
      {
        AgentDefaults.Set("ClickyAgentHarnessModel", harnessModels[index].RawValue());
      }, null);
      string text = "Live (use Worker)";
      string[] array = PlanFixtures.Names;
      int num = 0;
      string[] array2 = new string[1 + array.Length];
      array2[num] = text;
      num++;
      ReadOnlySpan<string> readOnlySpan = new ReadOnlySpan<string>(array);
      readOnlySpan.CopyTo(new Span<string>(array2).Slice(num, readOnlySpan.Length));
      num += readOnlySpan.Length;
      IReadOnlyList<string> readOnlyList = new <>z__ReadOnlyArray<string>(array2);
      string text2 = PlanFixtures.Current;
      int num2 = ((text2 != null) ? (Array.IndexOf<string>(PlanFixtures.Names, text2) + 1) : 0);
      Action<int> action = delegate(int index)
      {
        PlanFixtures.Apply(this.host.Billing, (index <= 0) ? null : PlanFixtures.Names[index - 1]);
      };
      text = "Live";
      array2 = PlanFixtures.Names;
      num = 0;
      array = new string[1 + array2.Length];
      array[num] = text;
      num++;
      readOnlySpan = new ReadOnlySpan<string>(array2);
      readOnlySpan.CopyTo(new Span<string>(array).Slice(num, readOnlySpan.Length));
      num += readOnlySpan.Length;
      Pressable pressable3 = SK.MenuPicker(readOnlyList, num2, action, new <>z__ReadOnlyArray<string>(array));
      string text3 = "Developer";
      string text4 = "Previews and debug tools.";
      List<FrameworkElement> list = new List<FrameworkElement>();
      list.Add(this.NewAgentFeaturesGroup());
      list.Add(SK.Group("Drawing on screen", null, new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
      {
        SK.ToggleRow("Drawing sounds", "Every sound a drawing makes: a photo or card landing, a button appearing, the click target, typing, the model's sfx() accents, and the sweep when it's cleared. Off silences them all, saved drawings too. The same setting as Voice → Drawing sounds.", c.DrawingSoundsEnabled, delegate(bool v)
        {
          this.Quiet("screen_drawing_sounds_developer", v, delegate(AppSettings x)
          {
            x.DrawingSoundsEnabled = v;
          });
        }),
        SK.ToggleRow("Pen sounds", "Also a soft felt-tip, pencil or handwriting sound under every stroke and word, following the pen's speed. Off by default.", c.DrawingPenSoundsEnabled, delegate(bool v)
        {
          this.Quiet("screen_drawing_pen_sounds", v, delegate(AppSettings x)
          {
            x.DrawingPenSoundsEnabled = v;
          });
        }),
        SK.ToggleRow("JavaScript JIT", "V8 compiles the drawing engine's JavaScript and the 3D kernel's three.js to machine code. Off runs them interpreted, to compare. Takes effect after you quit and reopen HeyClicky.", c.DrawingJavaScriptJit, delegate(bool v)
        {
          this.Quiet("screen_drawing_javascript_jit", v, delegate(AppSettings x)
          {
            x.DrawingJavaScriptJit = v;
          });
        })
      }), 14.0, null));
      list.Add(SK.Group("One brain", "The Worker honors these for internal testers only.", new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
      {
        SK.Row("One brain model", "The model one brain answers every turn with (sent as brain_model).", pressable, null),
        SK.ToggleRow("One brain reads apps", "One brain reads your connected apps itself for a quick look (an email, an event, a doc) and hands bigger digs to a job. Read only: after a read the rest of that turn only talks. GPT-6.1 Sol models only. The local Worker needs its HTTPS tunnel (npm run dev:mcp).", c.OneBrainReadsApps, delegate(bool v)
        {
          this.Quiet("one_brain_reads_apps", v, delegate(AppSettings x)
          {
            x.OneBrainReadsApps = v;
          });
        }),
        SK.ToggleRow("Keep screens in history", "Each turn's screenshot of the display with your cursor stays in the conversation, so HeyClicky can look back at what was on screen a few turns ago. It's read back from the cache at about a tenth of the price; compaction folds old ones away, and they're removed when the session ends.", c.OneBrainKeepsScreensInHistory, delegate(bool v)
        {
          this.Quiet("one_brain_keeps_screens", v, delegate(AppSettings x)
          {
            x.OneBrainKeepsScreensInHistory = v;
          });
        }),
        SK.ToggleRow("Show one brain stats", "A small panel in the top-left corner: tokens in the conversation and how many until compaction, kept screenshots, the prompt cache on the last turn (read, written, fresh), each step's timing from key release to first voice, and session events. Hidden from screenshots, so Clicky never sees it.", c.ShowOneBrainStats, delegate(bool v)
        {
          this.Quiet("one_brain_stats_hud", v, delegate(AppSettings x)
          {
            x.ShowOneBrainStats = v;
          });
        }),
        this.FreshSessionRow()
      }), 14.0, null));
      list.Add(SK.Group("Agent harness", "The Worker honors this for internal testers only.", new <>z__ReadOnlySingleElementList<FrameworkElement>(SK.Row("H1 model", "New jobs start on it at the server's effort; a job keeps the model it started on. Default is the server's model, what everyone else runs on.", pressable2, null)), 14.0, null));
      list.AddRange(this.ProactiveDeveloperGroups());
      List<FrameworkElement> list2 = list;
      string text5 = "Debug toggles";
      string text6 = null;
      FrameworkElement[] array3 = new FrameworkElement[3];
      array3[0] = SK.ToggleRow("Show agent names", "The notch's activity strip shows what each agent is working on. Off shows only the working Clicky's face.", c.ShowAgentLabelsInActivityStrip, delegate(bool v)
      {
        this.Quiet("show_agent_names", v, delegate(AppSettings x)
        {
          x.ShowAgentLabelsInActivityStrip = v;
        });
      });
      array3[1] = SK.ToggleRow("Preview app update", "Home's header and the notch peek show the orange Update app pill as if an update were downloaded. Resets on relaunch.", HeaderPills.PreviewUpdateReady, delegate(bool v)
      {
        HeaderPills.PreviewUpdateReady = v;
      });
      array3[2] = SK.Row("Force plan state", "Shows Settings → Account in each payment state without a Stripe trip. Resets on relaunch.", pressable3, null);
      list2.Add(SK.Group(text5, text6, new <>z__ReadOnlyArray<FrameworkElement>(array3), 14.0, null));
      list.Add(SK.Group("Debug actions", null, new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
      {
        SK.ActionRow("Preview onboarding tutorial", null, new Action(this.actions.StartTutorial), null, null, false),
        SK.ActionRow("Preview paywall", "The plan picker the tour ends with, Free option included. Your plan and offer are untouched.", delegate
        {
          AppHost app = this.host.App;
          if (app == null)
          {
            return;
          }
          app.PreviewOnboardingPaywall();
        }, null, null, false),
        SK.ActionRow("Reset paywall video", "The limit-hit paywall plays its intro video again.", delegate
        {
          AppHost app2 = this.host.App;
          if (app2 == null)
          {
            return;
          }
          app2.ResetPaywallIntroVideo();
        }, null, null, false),
        SK.ActionRow("Show the intro", "The pre-sign-in welcome, squad and color picker.", new Action(this.actions.ShowIntro), null, null, false),
        SK.ActionRow("Dry-run the intro reel's cues", "Fires the reel's demo cues at their timestamps without the video (they open pages and launch the demo agents).", delegate
        {
          AppHost app3 = this.host.App;
          if (app3 == null)
          {
            return;
          }
          app3.StartOnboardingVideoDemoDryRun();
        }, null, null, false),
        SK.ActionRow("Reset onboarding", "The notch's welcome and permission cards show again.", delegate
        {
          this.host.Set("reset_onboarding", true, delegate(AppSettings x)
          {
            x.Onboarding.HasCompletedOnboarding = false;
            x.Onboarding.HasAcknowledgedPermissionIntro = false;
            x.Onboarding.TutorialCompletedAtEpochSeconds = null;
          });
        }, null, null, false),
        SK.ActionRow("Reset intro", "The pre-sign-in intro shows again on the next launch.", delegate
        {
          this.host.Set("reset_intro", true, delegate(AppSettings x)
          {
            x.Onboarding.PreSignInIntroDismissed = false;
          });
        }, null, null, false),
        SK.ActionRow("Open logs folder", Log.Directory, delegate
        {
          this.host.OpenUrl(Log.Directory);
        }, null, null, false),
        SK.ActionRow("Relaunch HeyClicky", "Quits and opens HeyClicky again, e.g. after switching the JIT.", new Action(this.actions.Relaunch), null, null, false)
      }), 14.0, null));
      return SK.Scaffold(text3, text4, new <>z__ReadOnlyList<FrameworkElement>(list), 20.0);
    }

    // Token: 0x06000940 RID: 2368 RVA: 0x00547E20 File Offset: 0x00547E20
    private IEnumerable<FrameworkElement> DemoAgentMomentRows()
    {
      SettingsContent.<DemoAgentMomentRows>d__65 <DemoAgentMomentRows>d__ = new SettingsContent.<DemoAgentMomentRows>d__65(-2);
      <DemoAgentMomentRows>d__.<>4__this = this;
      return <DemoAgentMomentRows>d__;
    }

    // Token: 0x06000941 RID: 2369 RVA: 0x00547E30 File Offset: 0x00547E30
    private FrameworkElement DictationPage()
    {
      AppSettings c = this.host.Current;
      bool flag = c.DictationLanguageCode == "multi";
      string pinned = (SettingsContent.Languages.Any<ValueTuple<string, string, string>>(([TupleElementNames(new string[] { "Code", "Name", "Native" })] ValueTuple<string, string, string> l) => l.Item1 == c.DictationSpecificLanguageCode) ? c.DictationSpecificLanguageCode : "en");
      Grid item = SK.SearchField("Search languages", this.languageSearch, delegate(string t)
      {
        this.languageSearch = t;
        this.FillLanguageGrid(pinned);
      }, true, 12.0, null).Item1;
      this.languageGridHost = new Grid();
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 10.0,
        Padding = new Thickness(14.0),
        Opacity = (flag ? 0.35 : 1.0),
        IsHitTestVisible = !flag,
        Children = { item, this.languageGridHost }
      };
      this.FillLanguageGrid(pinned);
      StackPanel stackPanel2 = SK.Group("Language", "Auto-detect follows you across languages mid-sentence. Pin one language for the most accurate results.", new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
      {
        SK.ToggleRow("Auto-detect language", "HeyClicky figures out the language as you speak.", flag, delegate(bool v)
        {
          this.host.Set("auto_detect_language", v, delegate(AppSettings x)
          {
            x.DictationLanguageCode = (v ? "multi" : x.DictationSpecificLanguageCode);
          });
        }),
        stackPanel
      }), 14.0, null);
      List<string> dictationCustomWords = c.DictationCustomWords;
      ValueTuple<Grid, TextBox> valueTuple = SK.SearchField("Add a word", this.draftWord, delegate(string t)
      {
        this.draftWord = t;
      }, false, 12.5, null);
      Grid item2 = valueTuple.Item1;
      TextBox wordInput = valueTuple.Item2;
      wordInput.KeyDown += delegate(object _, KeyRoutedEventArgs e)
      {
        if (e.Key == VirtualKey.Enter)
        {
          e.Handled = true;
          this.CommitDraftWord();
        }
      };
      GelButton add = new GelButton("Add", GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 100.0, null, null, null)
      {
        Opacity = (string.IsNullOrWhiteSpace(this.draftWord) ? 0.5 : 1.0)
      };
      wordInput.TextChanged += delegate(object _, TextChangedEventArgs _)
      {
        add.Opacity = (string.IsNullOrWhiteSpace(wordInput.Text) ? 0.5 : 1.0);
      };
      add.Click += this.CommitDraftWord;
      Grid grid = new Grid
      {
        ColumnSpacing = 8.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetColumn(add, 1);
      grid.Children.Add(item2);
      grid.Children.Add(add);
      StackPanel stackPanel3 = new StackPanel
      {
        Spacing = 12.0,
        Padding = new Thickness(14.0),
        Children = { grid }
      };
      if (dictationCustomWords.Count > 0)
      {
        WrapPanel wrapPanel = SK.Wrap(6.0);
        foreach (string text in dictationCustomWords)
        {
          wrapPanel.Children.Add(this.WordChip(text));
        }
        stackPanel3.Children.Add(wrapPanel);
      }
      if (this.focusWordInputAfterRender)
      {
        this.focusWordInputAfterRender = false;
        wordInput.Loaded += delegate(object _, RoutedEventArgs _)
        {
          wordInput.Focus(FocusState.Programmatic);
        };
      }
      return SK.Scaffold("Dictation", "Languages and the words HeyClicky should spell right.", new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
      {
        SK.Group("Dictionary", "Up to 50 words. HeyClicky leans toward these spellings — names, jargon, product words.", new <>z__ReadOnlySingleElementList<FrameworkElement>(stackPanel3), 14.0, null),
        stackPanel2
      }), 32.0);
    }

    // Token: 0x06000942 RID: 2370 RVA: 0x00548228 File Offset: 0x00548228
    private void FillLanguageGrid(string pinned)
    {
      using (MessagesPalette.Use(this.look))
      {
        if (this.languageGridHost != null)
        {
          this.languageGridHost.Children.Clear();
          string q = this.languageSearch.Trim().ToLowerInvariant();
          ValueTuple<string, string, string>[] array = ((q.Length == 0) ? SettingsContent.LanguagesForGrid : SettingsContent.LanguagesForGrid.Where<ValueTuple<string, string, string>>(delegate([TupleElementNames(new string[] { "Code", "Name", "Native" })] ValueTuple<string, string, string> l)
          {
            if (!l.Item2.ToLowerInvariant().Contains(q))
            {
              string item3 = l.Item3;
              if (item3 == null || !item3.ToLowerInvariant().Contains(q))
              {
                return l.Item1 == q;
              }
            }
            return true;
          }).ToArray<ValueTuple<string, string, string>>());
          if (array.Length == 0)
          {
            this.languageGridHost.Children.Add(Ui.Text("No languages match \"" + this.languageSearch + "\".", 11.5, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, false, false));
          }
          else
          {
            Grid grid = new Grid
            {
              ColumnSpacing = 8.0,
              RowSpacing = 8.0
            };
            for (int i = 0; i < 4; i++)
            {
              grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int j = 0; j < (array.Length + 3) / 4; j++)
            {
              grid.RowDefinitions.Add(new RowDefinition
              {
                Height = GridLength.Auto
              });
            }
            for (int k = 0; k < array.Length; k++)
            {
              ValueTuple<string, string, string> valueTuple = array[k];
              string code = valueTuple.Item1;
              string item = valueTuple.Item2;
              string item2 = valueTuple.Item3;
              bool flag = code == pinned;
              StackPanel stackPanel = new StackPanel
              {
                Spacing = 3.0,
                VerticalAlignment = VerticalAlignment.Center,
                Children = { Ui.Text(item, 11.5, flag ? FontWeights.SemiBold : FontWeights.Medium, flag ? SK.TextPrimary : SK.TextSecondary, TextAlignment.Left, 1, false, false) }
              };
              if (item2 != null)
              {
                stackPanel.Children.Add(Ui.Text(item2, 10.5, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 1, false, false));
              }
              Grid grid2 = new Grid
              {
                Padding = new Thickness(13.0, 12.0, 13.0, 12.0),
                MinHeight = 54.0,
                ColumnSpacing = 6.0
              };
              grid2.ColumnDefinitions.Add(new ColumnDefinition());
              grid2.ColumnDefinitions.Add(new ColumnDefinition
              {
                Width = new GridLength(18.0)
              });
              grid2.Children.Add(stackPanel);
              if (flag)
              {
                FrameworkElement frameworkElement = IntroWindow.GelCheckmark(18.0);
                frameworkElement.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(frameworkElement, 1);
                grid2.Children.Add(frameworkElement);
              }
              Action<AppSettings> <>9__2;
              Pressable pressable = SK.Tile(grid2, flag, 9.0, delegate
              {
                SettingsHost settingsHost = this.host;
                string text = "dictation_language";
                object code2 = code;
                Action<AppSettings> action;
                if ((action = <>9__2) == null)
                {
                  action = (<>9__2 = delegate(AppSettings x)
                  {
                    x.DictationSpecificLanguageCode = code;
                    if (x.DictationLanguageCode != "multi")
                    {
                      x.DictationLanguageCode = code;
                    }
                  });
                }
                settingsHost.Set(text, code2, action);
              }, 20.0, false);
              ToolTipService.SetToolTip(pressable, (item2 == null) ? item : (item + ", " + item2));
              Grid.SetColumn(pressable, k % 4);
              Grid.SetRow(pressable, k / 4);
              grid.Children.Add(pressable);
            }
            this.languageGridHost.Children.Add(grid);
          }
        }
      }
    }

    // Token: 0x06000943 RID: 2371 RVA: 0x005485CC File Offset: 0x005485CC
    private void CommitDraftWord()
    {
      string word = this.draftWord.Trim();
      if (word.Length == 0)
      {
        return;
      }
      List<string> words = this.host.Read<List<string>>(([Nullable(1)] AppSettings x) => x.DictationCustomWords.ToList<string>());
      if (word.Length <= 60 && words.Count < 50 && !words.Any<string>((string w) => string.Equals(w, word, StringComparison.OrdinalIgnoreCase)))
      {
        words.Add(word);
        SettingsHost settingsHost = this.host;
        string text = "dictation_dictionary";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
        defaultInterpolatedStringHandler.AppendLiteral("add (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(words.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" words)");
        settingsHost.Set(text, defaultInterpolatedStringHandler.ToStringAndClear(), delegate(AppSettings x)
        {
          x.DictationCustomWords = words;
        });
      }
      this.draftWord = "";
      this.focusWordInputAfterRender = true;
      this.Render();
    }

    // Token: 0x06000944 RID: 2372 RVA: 0x005486EC File Offset: 0x005486EC
    private FrameworkElement WordChip(string word)
    {
      Border chip = new Border
      {
        CornerRadius = new CornerRadius(100.0),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.08)),
        Padding = new Thickness(10.0, 5.5, 10.0, 5.5)
      }.Capsule();
      Pressable pressable = new Pressable(true)
      {
        Width = 12.0,
        Height = 12.0
      };
      TextBlock textBlock = Icons.Glyph("\ue711", 7.5, SK.TextTertiary);
      textBlock.FontWeight = FontWeights.Bold;
      pressable.Children.Add(textBlock);
      Func<string, bool> <>9__5;
      Func<AppSettings, List<string>> <>9__3;
      pressable.Click += delegate
      {
        SettingsContent.<>c__DisplayClass78_1 CS$<>8__locals2 = new SettingsContent.<>c__DisplayClass78_1();
        SettingsContent.<>c__DisplayClass78_1 CS$<>8__locals3 = CS$<>8__locals2;
        SettingsHost settingsHost = this.host;
        Func<AppSettings, List<string>> func;
        if ((func = <>9__3) == null)
        {
          func = (<>9__3 = delegate([Nullable(1)] AppSettings s)
          {
            IEnumerable<string> dictationCustomWords = s.DictationCustomWords;
            Func<string, bool> func2;
            if ((func2 = <>9__5) == null)
            {
              func2 = (<>9__5 = (string w) => !string.Equals(w, word, StringComparison.OrdinalIgnoreCase));
            }
            return dictationCustomWords.Where<string>(func2).ToList<string>();
          });
        }
        CS$<>8__locals3.words = settingsHost.Read<List<string>>(func);
        this.host.Set("dictation_dictionary", "remove", delegate(AppSettings s)
        {
          s.DictationCustomWords = CS$<>8__locals2.words;
        });
      };
      chip.Child = Ui.H(5.0, new UIElement[]
      {
        Ui.Text(word, 11.5, FontWeights.Medium, SK.TextPrimary, TextAlignment.Left, 0, false, false),
        pressable
      });
      chip.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        chip.Background = Ui.Brush(this.look.Ink.WithAlpha(0.12));
      };
      chip.PointerExited += delegate(object _, PointerRoutedEventArgs e)
      {
        if (PointerFrames.IsStale(e))
        {
          return;
        }
        chip.Background = Ui.Brush(this.look.Ink.WithAlpha(0.08));
      };
      return chip;
    }

    // Token: 0x06000945 RID: 2373 RVA: 0x00548864 File Offset: 0x00548864
    private FrameworkElement GeneralPage()
    {
      AppSettings appSettings = this.host.Current;
      AppConfigLinks links = SettingsHost.Links;
      return SK.Scaffold("General", "How HeyClicky behaves on your PC.", new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
      {
        SK.Group("Behavior", null, new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
        {
          SK.ToggleRow("Show in taskbar", "Turn off to keep HeyClicky notch only.", appSettings.ShowInTaskbar, delegate(bool v)
          {
            this.Quiet("show_in_taskbar", v, delegate(AppSettings x)
            {
              x.ShowInTaskbar = v;
            });
          }),
          SK.ToggleRow("Show in screen recordings", "Let screen sharing and recording tools capture HeyClicky.", appSettings.ShowInScreenRecordings, delegate(bool v)
          {
            this.Quiet("show_in_screen_recordings", v, delegate(AppSettings x)
            {
              x.ShowInScreenRecordings = v;
            });
          }),
          SK.ToggleRow("Launch at login", "Open HeyClicky when you sign in to Windows.", this.s.Settings.LaunchAtLogin, delegate(bool v)
          {
            this.quietWrites++;
            try
            {
              string text = "settings";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
              defaultInterpolatedStringHandler.AppendLiteral("launch_at_login = ");
              defaultInterpolatedStringHandler.AppendFormatted<bool>(v);
              Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
              this.s.Settings.LaunchAtLogin = v;
              SettingsHost.TrackSettingChanged("launch_at_login", v);
            }
            finally
            {
              this.quietWrites--;
            }
          }),
          SK.ToggleRow("Pause HeyClicky", "Ignore the shortcuts until you resume.", this.s.Settings.IsPaused, delegate(bool v)
          {
            this.quietWrites++;
            try
            {
              string text2 = "settings";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(9, 1);
              defaultInterpolatedStringHandler2.AppendLiteral("paused = ");
              defaultInterpolatedStringHandler2.AppendFormatted<bool>(v);
              Log.Info(text2, defaultInterpolatedStringHandler2.ToStringAndClear());
              this.actions.SetPaused(v);
              SettingsHost.TrackSettingChanged("paused", v);
            }
            finally
            {
              this.quietWrites--;
            }
          })
        }), 14.0, null),
        this.UpdatesGroup(),
        SK.Group("Community", null, new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
        {
          this.BrandLinkRow("WhatsApp community", "whatsapp-logo.png", delegate
          {
            if (!string.IsNullOrEmpty(links.CommunityWhatsApp))
            {
              ClickyAnalytics.TrackCommunityLinkOpened("whatsapp");
            }
            this.host.OpenUrl(links.CommunityWhatsApp);
          }),
          this.BrandLinkRow("Reddit", "reddit-logo.svg", delegate
          {
            if (!string.IsNullOrEmpty(links.CommunityReddit))
            {
              ClickyAnalytics.TrackCommunityLinkOpened("reddit");
            }
            this.host.OpenUrl(links.CommunityReddit);
          })
        }), 14.0, null),
        SK.Group("Support", null, new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
        {
          SK.ActionRow("Request a feature", null, delegate
          {
            ClickyAnalytics.TrackFeatureRequestOpened();
            this.host.OpenUrl(links.FeatureRequest);
          }, null, "arrow.up.right", false),
          SK.ActionRow("Report a bug", "Sends diagnostics along so we can actually fix it.", delegate
          {
            BugReport.Run(this.host);
          }, null, null, false)
        }), 14.0, null),
        SK.Group("Welcome tour", null, new <>z__ReadOnlySingleElementList<FrameworkElement>(SK.ActionRow("Replay the welcome tour", "Watch the intro video again, then redo the hands-on tour and the interview. HeyClicky suggests three new jobs from your answers; the ones you have stay.", delegate
        {
          Log.Info("settings", "replay_welcome_tour");
          SettingsHost.TrackSettingChanged("replay_welcome_tour", null);
          AppHost appHost = this.actions as AppHost;
          if (appHost != null)
          {
            appHost.ReplayOnboardingExperienceFromSettings();
            return;
          }
          this.actions.StartTutorial();
        }, null, "play.fill", false)), 14.0, null)
      }), 20.0);
    }

    // Token: 0x06000946 RID: 2374 RVA: 0x00548A90 File Offset: 0x00548A90
    private FrameworkElement BrandLinkRow(string title, string asset, Action onClick)
    {
      Pressable p = new Pressable(true);
      string text = Path.Combine(AppContext.BaseDirectory, "Assets", "Logos", asset);
      FrameworkElement frameworkElement;
      if (asset.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
      {
        frameworkElement = new Image
        {
          Width = 16.0,
          Height = 16.0,
          Source = new SvgImageSource(new Uri(text))
          {
            RasterizePixelWidth = 32.0,
            RasterizePixelHeight = 32.0
          }
        };
      }
      else
      {
        frameworkElement = new Image
        {
          Width = 16.0,
          Height = 16.0,
          Source = new BitmapImage(new Uri(text))
        };
      }
      Grid grid = new Grid
      {
        Padding = new Thickness(14.0, 11.0, 14.0, 11.0),
        ColumnSpacing = 10.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      TextBlock textBlock = Ui.Text(title, 13.0, FontWeights.Medium, SK.TextPrimary, TextAlignment.Left, 0, false, false);
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      FrameworkElement frameworkElement2 = SK.TrailingSymbol("arrow.up.right");
      Grid.SetColumn(textBlock, 1);
      Grid.SetColumn(frameworkElement2, 2);
      grid.Children.Add(frameworkElement);
      grid.Children.Add(textBlock);
      grid.Children.Add(frameworkElement2);
      p.Children.Add(grid);
      p.HoverChanged += delegate(bool h)
      {
        p.Background = Ui.Brush(this.look.Ink.WithAlpha(h ? 0.05 : 0.0));
      };
      p.Click += onClick;
      return p;
    }

    // Token: 0x17000178 RID: 376
    // (get) Token: 0x06000947 RID: 2375 RVA: 0x00548C94 File Offset: 0x00548C94
    // (set) Token: 0x06000948 RID: 2376 RVA: 0x00548C9C File Offset: 0x00548C9C
    [Nullable(2)]
    public static AgentIntegrationCatalog DemoCatalog
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000949 RID: 2377 RVA: 0x00548CA4 File Offset: 0x00548CA4
    private async Task RefreshIntegrationsAsync()
    {
      SettingsContent.<>c__DisplayClass97_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass97_0();
      CS$<>8__locals1.<>4__this = this;
      if (this.host.Agents == null)
      {
        CS$<>8__locals1.demo = SettingsContent.DemoCatalog;
        if (CS$<>8__locals1.demo != null && this.host.IsSignedIn)
        {
          this.dq.TryEnqueue(delegate
          {
            CS$<>8__locals1.<>4__this.catalog = CS$<>8__locals1.demo;
            CS$<>8__locals1.<>4__this.integrationError = null;
            CS$<>8__locals1.<>4__this.loadingIntegrations = false;
            if (CS$<>8__locals1.<>4__this.page == SettingsWindow.Page.Integrations)
            {
              CS$<>8__locals1.<>4__this.Render();
            }
          });
          return;
        }
      }
      AgentService agents = this.host.Agents;
      if (agents == null || !this.host.IsSignedIn)
      {
        this.dq.TryEnqueue(delegate
        {
          if (CS$<>8__locals1.<>4__this.page == SettingsWindow.Page.Integrations)
          {
            CS$<>8__locals1.<>4__this.Render();
          }
        });
      }
      else
      {
        this.loadingIntegrations = true;
        try
        {
          SettingsContent.<>c__DisplayClass97_1 CS$<>8__locals2 = new SettingsContent.<>c__DisplayClass97_1();
          CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
          AgentIntegrationCatalog agentIntegrationCatalog = await agents.ListIntegrationsAsync(default(CancellationToken));
          CS$<>8__locals2.result = agentIntegrationCatalog;
          ConnectedApps.SetCatalog(CS$<>8__locals2.result);
          this.dq.TryEnqueue(delegate
          {
            CS$<>8__locals2.CS$<>8__locals1.<>4__this.catalog = CS$<>8__locals2.result;
            string text = CS$<>8__locals2.CS$<>8__locals1.<>4__this.integrationError;
            CS$<>8__locals2.CS$<>8__locals1.<>4__this.integrationError = (CS$<>8__locals2.result.ComposioConfigured ? null : "Integrations aren't set up on this server.");
            CS$<>8__locals2.CS$<>8__locals1.<>4__this.loadingIntegrations = false;
            string text2 = "settings";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 2);
            defaultInterpolatedStringHandler.AppendLiteral("integrations: ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals2.result.Integrations.Count);
            defaultInterpolatedStringHandler.AppendLiteral(" in the catalog, ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals2.result.Integrations.Count<AgentIntegration>((AgentIntegration i) => i.IsActiveConnection));
            defaultInterpolatedStringHandler.AppendLiteral(" connected");
            Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
            CS$<>8__locals2.CS$<>8__locals1.<>4__this.CheckAwaitedConnection();
            if (CS$<>8__locals2.CS$<>8__locals1.<>4__this.page == SettingsWindow.Page.Integrations)
            {
              if (text == CS$<>8__locals2.CS$<>8__locals1.<>4__this.integrationError && CS$<>8__locals2.CS$<>8__locals1.<>4__this.integrationListHost != null)
              {
                CS$<>8__locals2.CS$<>8__locals1.<>4__this.FillIntegrationList();
                return;
              }
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.Render();
            }
          });
          CS$<>8__locals2 = null;
        }
        catch (Exception ex)
        {
          Log.Warn("settings", "integrations fetch failed: " + ex.Message);
          this.dq.TryEnqueue(delegate
          {
            CS$<>8__locals1.<>4__this.loadingIntegrations = false;
            CS$<>8__locals1.<>4__this.integrationError = "HeyClicky couldn't load your integrations. Check your connection and try again.";
            if (CS$<>8__locals1.<>4__this.page == SettingsWindow.Page.Integrations)
            {
              CS$<>8__locals1.<>4__this.Render();
            }
          });
        }
      }
    }

    // Token: 0x0600094A RID: 2378 RVA: 0x00548CE8 File Offset: 0x00548CE8
    [NullableContext(2)]
    public void OnIntegrationCallback(string toolkit)
    {
      Log.Info("settings", "integration callback (" + (toolkit ?? "?") + ")");
      this.RefreshIntegrationsAsync();
    }

    // Token: 0x0600094B RID: 2379 RVA: 0x00548D14 File Offset: 0x00548D14
    private void CheckAwaitedConnection()
    {
      ValueTuple<string, int, DateTime>? valueTuple = this.awaitingConnection;
      if (valueTuple != null)
      {
        ValueTuple<string, int, DateTime> waiting = valueTuple.GetValueOrDefault();
        if (this.catalog != null)
        {
          AgentIntegration agentIntegration = this.catalog.Integrations.FirstOrDefault<AgentIntegration>((AgentIntegration i) => i.Id == waiting.Item1);
          if (agentIntegration == null || agentIntegration.ActiveAccounts.Count <= waiting.Item2)
          {
            if (DateTime.UtcNow - waiting.Item3 > TimeSpan.FromMinutes(5L))
            {
              this.awaitingConnection = null;
            }
            return;
          }
          string text = "settings";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 2);
          defaultInterpolatedStringHandler.AppendLiteral("integration connected: ");
          defaultInterpolatedStringHandler.AppendFormatted(waiting.Item1);
          defaultInterpolatedStringHandler.AppendLiteral(" (");
          defaultInterpolatedStringHandler.AppendFormatted<int>(agentIntegration.ActiveAccounts.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" account(s))");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
          SettingsContent.TrackIntegration("connected", agentIntegration, "agent_space_settings", null);
          this.awaitingConnection = null;
          this.justConnected = waiting.Item1;
          AgentService agents = this.host.Agents;
          if (agents == null)
          {
            return;
          }
          agents.InvalidateConnectedApps();
          return;
        }
      }
    }

    // Token: 0x0600094C RID: 2380 RVA: 0x00548E6C File Offset: 0x00548E6C
    private async Task PollAwaitedConnectionAsync()
    {
      for (;;)
      {
        ValueTuple<string, int, DateTime>? valueTuple = this.awaitingConnection;
        if (valueTuple == null)
        {
          break;
        }
        await Task.Delay(3000);
        valueTuple = this.awaitingConnection;
        if (valueTuple == null)
        {
          break;
        }
        await this.RefreshIntegrationsAsync();
      }
    }

    // Token: 0x0600094D RID: 2381 RVA: 0x00548EB0 File Offset: 0x00548EB0
    private void ScheduleConnectorStateRefresh()
    {
      if (CustomConnectorStore.Load().Count == 0)
      {
        return;
      }
      Task.Run(async delegate
      {
        await Task.Delay(4000);
        if (this.page == SettingsWindow.Page.Integrations && this.isShown)
        {
          await ConnectorAuth.RefreshStatesAsync();
        }
      });
    }

    // Token: 0x0600094E RID: 2382 RVA: 0x00548ED4 File Offset: 0x00548ED4
    private FrameworkElement IntegrationsPage()
    {
      this.HookConnectorEvents();
      this.RestoreParkedConnectorDraftIfFresh();
      List<FrameworkElement> list = new List<FrameworkElement>();
      list.Add(this.BrowserConnectionSection());
      Grid item = SK.SearchField("Search integrations", this.integrationSearch, delegate(string t)
      {
        this.integrationSearch = t;
        this.FillIntegrationList();
        this.FillBrowserCard();
      }, true, 12.0, null).Item1;
      GelButton gelButton = new GelButton("Add custom connector", GelAppearance.SecondaryWhite, GelButton.Sizing.Normal, 15.0, null, null, null);
      gelButton.Click += delegate
      {
        this.OpenConnectorEditor(null);
      };
      Grid grid = new Grid
      {
        ColumnSpacing = 8.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetColumn(gelButton, 1);
      grid.Children.Add(item);
      grid.Children.Add(gelButton);
      StackPanel stackPanel = new StackPanel
      {
        Children = { grid }
      };
      FrameworkElement frameworkElement = this.ConnectorEditor();
      if (frameworkElement != null)
      {
        frameworkElement.Margin = new Thickness(0.0, 20.0, 0.0, 0.0);
        stackPanel.Children.Add(frameworkElement);
      }
      list.Add(stackPanel);
      string text = this.integrationError;
      if (text != null)
      {
        list.Add(SK.Alert(text, SK.Destructive));
      }
      this.integrationListHost = new StackPanel
      {
        Spacing = 20.0
      };
      this.FillIntegrationList();
      list.Add(this.integrationListHost);
      return SK.Scaffold("Integrations", "Apps your agents can reach into.", list, 20.0);
    }

    // Token: 0x0600094F RID: 2383 RVA: 0x0054909C File Offset: 0x0054909C
    private void ResetDepartures()
    {
      this.departures.Clear();
      this.departureRows.Clear();
      this.lastConnected = new List<ValueTuple<string, string, Func<FrameworkElement>>>();
    }

    // Token: 0x06000950 RID: 2384 RVA: 0x005490C0 File Offset: 0x005490C0
    private void TrackDepartures([TupleElementNames(new string[] { "Id", "Name", "Logo" })] [Nullable(new byte[] { 1, 0, 1, 1, 1, 1 })] List<ValueTuple<string, string, Func<FrameworkElement>>> connected, bool retain)
    {
      if (!retain)
      {
        this.departures.Clear();
        this.lastConnected = connected;
        return;
      }
      HashSet<string> hashSet = connected.Select<ValueTuple<string, string, Func<FrameworkElement>>, string>(([TupleElementNames(new string[] { "Id", "Name", "Logo" })] ValueTuple<string, string, Func<FrameworkElement>> c) => c.Item1).ToHashSet<string>();
      foreach (string text in this.departures.Keys.Where<string>(new Func<string, bool>(hashSet.Contains)).ToList<string>())
      {
        this.departures.Remove(text);
      }
      for (int i = 0; i < this.lastConnected.Count; i++)
      {
        ValueTuple<string, string, Func<FrameworkElement>> valueTuple = this.lastConnected[i];
        if (!hashSet.Contains(valueTuple.Item1) && !this.departures.ContainsKey(valueTuple.Item1))
        {
          Guid guid = Guid.NewGuid();
          this.departures[valueTuple.Item1] = new SettingsContent.Departure(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, i, guid);
          Log.Info("settings", "integration departing: " + valueTuple.Item1);
          this.FinishDepartureAsync(valueTuple.Item1, guid);
        }
      }
      this.lastConnected = connected;
    }

    // Token: 0x06000951 RID: 2385 RVA: 0x0054922C File Offset: 0x0054922C
    private async Task FinishDepartureAsync(string id, Guid token)
    {
      SettingsContent.<>c__DisplayClass109_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass109_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.id = id;
      CS$<>8__locals1.token = token;
      await Task.Delay(1200);
      this.dq.TryEnqueue(delegate
      {
        SettingsContent.<>c__DisplayClass109_0.<<FinishDepartureAsync>b__0>d <<FinishDepartureAsync>b__0>d;
        <<FinishDepartureAsync>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
        <<FinishDepartureAsync>b__0>d.<>4__this = CS$<>8__locals1;
        <<FinishDepartureAsync>b__0>d.<>1__state = -1;
        <<FinishDepartureAsync>b__0>d.<>t__builder.Start<SettingsContent.<>c__DisplayClass109_0.<<FinishDepartureAsync>b__0>d>(ref <<FinishDepartureAsync>b__0>d);
      });
    }

    // Token: 0x06000952 RID: 2386 RVA: 0x00549280 File Offset: 0x00549280
    private FrameworkElement DepartedRow(SettingsContent.Departure d)
    {
      StackPanel stackPanel = Ui.H(7.0, new UIElement[]
      {
        Ui.Text(d.Name, 13.0, FontWeights.Medium, SK.TextPrimary, TextAlignment.Left, 0, false, false),
        Ui.H(3.0, new UIElement[]
        {
          Icons.Glyph("\ue73e", 9.0, SK.TextSecondary),
          Ui.Text("Disconnected", 10.0, FontWeights.SemiBold, SK.TextSecondary, TextAlignment.Left, 0, false, false)
        }).Align(HorizontalAlignment.Left, VerticalAlignment.Center)
      });
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 1.5,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          stackPanel,
          Ui.Text("HeyClicky no longer has access.", 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 2, false, false)
        }
      };
      Grid grid = new Grid
      {
        ColumnSpacing = 12.0,
        Padding = new Thickness(14.0, 10.0, 14.0, 10.0),
        Tag = "departed:" + d.Id
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(26.0)
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      FrameworkElement frameworkElement = d.Logo();
      frameworkElement.VerticalAlignment = VerticalAlignment.Center;
      grid.Children.Add(frameworkElement);
      Grid.SetColumn(stackPanel2, 1);
      grid.Children.Add(stackPanel2);
      this.departureRows[d.Id] = grid;
      return grid;
    }

    // Token: 0x06000953 RID: 2387 RVA: 0x0054944C File Offset: 0x0054944C
    private void FillIntegrationList()
    {
      using (MessagesPalette.Use(this.look))
      {
        if (this.integrationListHost != null)
        {
          this.integrationListHost.Children.Clear();
          bool flag = this.integrationSearch.Trim().Length > 0;
          if (!this.host.IsSignedIn)
          {
            this.integrationListHost.Children.Add(SettingsContent.EmptyText("Sign in to connect integrations."));
          }
          else
          {
            AgentIntegrationCatalog agentIntegrationCatalog = this.catalog;
            List<AgentIntegration> list = IntegrationSearch.Sorted((((agentIntegrationCatalog != null) ? agentIntegrationCatalog.Integrations : null) ?? Array.Empty<AgentIntegration>()).Where<AgentIntegration>((AgentIntegration i) => !i.IsLocalIntegration).Append(ObsidianVault.Row()));
            List<AgentIntegration> connected = list.Where<AgentIntegration>(delegate(AgentIntegration i)
            {
              if (!i.IsActiveConnection && !(i.Status == "needs_reconnect"))
              {
                string id = i.Id;
                SettingsContent <>4__this = this;
                return id == ((<>4__this.awaitingConnection != null) ? <>4__this.awaitingConnection.GetValueOrDefault().Item1 : null) && i.Accounts.Count > 0;
              }
              return true;
            }).ToList<AgentIntegration>();
            List<CustomConnector> list2 = (from c in CustomConnectorStore.Load()
              orderby c.CreatedAt
              select c).ToList<CustomConnector>();
            List<ValueTuple<string, string, Func<FrameworkElement>, Func<FrameworkElement>>> list3 = connected.Select<AgentIntegration, ValueTuple<string, string, Func<FrameworkElement>, Func<FrameworkElement>>>((AgentIntegration i) => new ValueTuple<string, string, Func<FrameworkElement>, Func<FrameworkElement>>(i.Id, i.DisplayName, () => SettingsContent.IntegrationLogo(i, 26.0), () => this.IntegrationRow(i))).Concat<ValueTuple<string, string, Func<FrameworkElement>, Func<FrameworkElement>>>(list2.Select<CustomConnector, ValueTuple<string, string, Func<FrameworkElement>, Func<FrameworkElement>>>((CustomConnector c) => new ValueTuple<string, string, Func<FrameworkElement>, Func<FrameworkElement>>(c.IntegrationId, c.Name, () => this.ConnectorGlyph(c, 26.0), () => this.CustomConnectorRow(c)))).ToList<ValueTuple<string, string, Func<FrameworkElement>, Func<FrameworkElement>>>();
            this.TrackDepartures(list3.Select<ValueTuple<string, string, Func<FrameworkElement>, Func<FrameworkElement>>, ValueTuple<string, string, Func<FrameworkElement>>>((ValueTuple<string, string, Func<FrameworkElement>, Func<FrameworkElement>> e) => new ValueTuple<string, string, Func<FrameworkElement>>(e.Item1, e.Item2, e.Item3)).ToList<ValueTuple<string, string, Func<FrameworkElement>>>(), !flag && this.catalog != null);
            List<FrameworkElement> list4 = list3.Select<ValueTuple<string, string, Func<FrameworkElement>, Func<FrameworkElement>>, FrameworkElement>((ValueTuple<string, string, Func<FrameworkElement>, Func<FrameworkElement>> e) => e.Item4()).ToList<FrameworkElement>();
            foreach (SettingsContent.Departure departure in this.departures.Values.OrderBy<SettingsContent.Departure, int>((SettingsContent.Departure d) => d.Index))
            {
              list4.Insert(Math.Min(departure.Index, list4.Count), this.DepartedRow(departure));
            }
            if (!flag && list4.Count > 0)
            {
              this.integrationListHost.Children.Add(SK.Group("Connected", null, list4, 52.0, null));
            }
            if (this.catalog == null && (this.loadingIntegrations || this.integrationError == null))
            {
              this.integrationListHost.Children.Add(SettingsContent.IntegrationsLoadingShimmer());
            }
            else
            {
              List<AgentIntegration> list5 = (flag ? list.Where<AgentIntegration>((AgentIntegration i) => IntegrationSearch.Matches(i, this.integrationSearch)) : list.Where<AgentIntegration>((AgentIntegration i) => !connected.Contains(i) && !this.departures.ContainsKey(i.Id))).ToList<AgentIntegration>();
              if (list5.Count == 0)
              {
                this.integrationListHost.Children.Add(SettingsContent.EmptyText(flag ? ("Nothing matches \"" + this.integrationSearch + "\".") : ((connected.Count > 0) ? "No more integrations to connect." : "No integrations available yet.")));
              }
              else
              {
                this.integrationListHost.Children.Add(SK.Group(flag ? "Search results" : "Available integrations", "Custom connectors are any MCP server: a hosted URL or a command on this PC. Tokens stay encrypted for your Windows account and are handed only to HeyClicky's agents.", list5.Select<AgentIntegration, FrameworkElement>((AgentIntegration i) => this.IntegrationRow(i)).ToList<FrameworkElement>(), 52.0, null));
              }
            }
          }
        }
      }
    }

    // Token: 0x06000954 RID: 2388 RVA: 0x00549804 File Offset: 0x00549804
    private static FrameworkElement EmptyText(string text)
    {
      TextBlock textBlock = Ui.Text(text, 12.0, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, true, false);
      textBlock.Margin = new Thickness(0.0, 6.0, 0.0, 0.0);
      return textBlock;
    }

    // Token: 0x06000955 RID: 2389 RVA: 0x00549860 File Offset: 0x00549860
    private static FrameworkElement IntegrationLogo(AgentIntegration i, double size)
    {
      return IntegrationLogos.Artwork(i.PrimaryToolkitSlug, i.DisplayName, size, 12.0, i.LogoUrl);
    }

    // Token: 0x06000956 RID: 2390 RVA: 0x00549884 File Offset: 0x00549884
    private FrameworkElement IntegrationRow(AgentIntegration i)
    {
      bool flag = this.integrationBusy.Contains(i.Id);
      bool flag2 = ((this.awaitingConnection != null) ? this.awaitingConnection.GetValueOrDefault().Item1 : null) == i.Id;
      bool flag3 = !i.IsLocalIntegration && i.Accounts.Count > 0 && (i.IsActiveConnection || i.Status == "needs_reconnect");
      bool shouldDisconnect = !i.IsLocalIntegration && i.ConnectedCount > 0 && i.Status != "needs_reconnect";
      bool flag4 = this.justConnected == i.Id;
      StackPanel stackPanel = Ui.H(7.0, new UIElement[] { Ui.Text(i.DisplayName, 13.0, FontWeights.Medium, SK.TextPrimary, TextAlignment.Left, 0, false, false) });
      if (flag2)
      {
        stackPanel.Children.Add(SettingsContent.ConnectionBadge("Checking…", SK.Warning, false));
      }
      else if (flag4)
      {
        stackPanel.Children.Add(SettingsContent.ConnectionBadge("Connected", SK.Positive, true));
      }
      else if ((i.IsActiveConnection || i.Status == "needs_reconnect") && !flag3)
      {
        string text = i.Status;
        ValueTuple<string, Color> valueTuple;
        if (!(text == "connected"))
        {
          if (!(text == "partial"))
          {
            if (!(text == "needs_reconnect"))
            {
              if (!(text == "unavailable") && !(text == "error"))
              {
                valueTuple = new ValueTuple<string, Color>("Not connected", SK.TextTertiary);
              }
              else
              {
                valueTuple = new ValueTuple<string, Color>((i.Status == "error") ? "Error" : "Unavailable", SK.Destructive);
              }
            }
            else
            {
              valueTuple = new ValueTuple<string, Color>("Reconnect required", SK.Warning);
            }
          }
          else
          {
            valueTuple = new ValueTuple<string, Color>("Partial", SK.Warning);
          }
        }
        else if (i.IsLocalIntegration)
        {
          valueTuple = new ValueTuple<string, Color>("Configured", SK.Positive);
        }
        else
        {
          valueTuple = new ValueTuple<string, Color>("Connected", SK.Positive);
        }
        ValueTuple<string, Color> valueTuple2 = valueTuple;
        string item = valueTuple2.Item1;
        Color item2 = valueTuple2.Item2;
        stackPanel.Children.Add(SettingsContent.StatusChip(item, item2));
      }
      string text2 = (flag2 ? ("Finish signing in to " + i.DisplayName + " in your browser, then come back.") : ((flag4 && !i.IsLocalIntegration) ? "You're all set. HeyClicky can use it now." : i.Description));
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 1.5,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          stackPanel,
          Ui.Text(text2, 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 2, false, false)
        }
      };
      FrameworkElement frameworkElement;
      if (flag2)
      {
        GelButton gelButton = new GelButton("Refresh", GelAppearance.SecondaryWhite, GelButton.Sizing.Normal, 15.0, null, null, null);
        gelButton.Click += delegate
        {
          this.RefreshIntegrationsAsync();
        };
        frameworkElement = gelButton;
      }
      else if (flag3)
      {
        frameworkElement = null;
      }
      else if (flag)
      {
        frameworkElement = Ui.Text(shouldDisconnect ? "Disconnecting…" : ((i.Status == "needs_reconnect") ? "Reconnecting…" : "Connecting…"), 11.0, FontWeights.Medium, SK.Warning, TextAlignment.Left, 0, false, false);
      }
      else if (this.confirmDisconnectIntegration == i.Id)
      {
        GelButton gelButton2 = new GelButton("Disconnect", GelAppearance.SecondaryOutline, GelButton.Sizing.Normal, 15.0, null, null, null);
        gelButton2.Click += delegate
        {
          this.DisconnectAsync(i, null);
        };
        GelButton gelButton3 = new GelButton("Keep", GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 15.0, null, null, null);
        gelButton3.Click += delegate
        {
          this.confirmDisconnectIntegration = null;
          this.Render();
        };
        frameworkElement = Ui.H(6.0, new UIElement[] { gelButton2, gelButton3 });
      }
      else if (i.IsLocalIntegration || i.Status != "unavailable")
      {
        string text;
        if (i.IsLocalIntegration)
        {
          string status = i.Status;
          string text3;
          if (!(status == "connected"))
          {
            if (!(status == "needs_reconnect"))
            {
              text3 = "Configure";
            }
            else
            {
              text3 = "Reconnect";
            }
          }
          else
          {
            text3 = "Change";
          }
          text = text3;
        }
        else
        {
          text = ((i.Status == "needs_reconnect") ? "Reconnect" : (shouldDisconnect ? "Disconnect" : "Connect"));
        }
        GelButton gelButton4 = new GelButton(text, shouldDisconnect ? GelAppearance.SecondaryOutline : GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 15.0, null, null, null);
        gelButton4.Click += delegate
        {
          if (shouldDisconnect)
          {
            this.confirmDisconnectIntegration = i.Id;
            this.Render();
            return;
          }
          this.ConnectAsync(i, false);
        };
        frameworkElement = gelButton4;
        if (i.Status == "needs_reconnect" && i.Accounts.Count == 0)
        {
          GelButton gelButton5 = new GelButton("", GelAppearance.SecondaryWhite, GelButton.Sizing.Normal, 15.0, "\ue711", null, null);
          gelButton5.Click += delegate
          {
            this.DisconnectAsync(i, null);
          };
          gelButton5.Tip("Remove this expired connection");
          frameworkElement = Ui.H(6.0, new UIElement[] { gelButton4, gelButton5 });
        }
      }
      else
      {
        frameworkElement = Ui.Text("Unavailable", 11.0, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, false, false);
      }
      Grid grid = new Grid
      {
        ColumnSpacing = 12.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(26.0)
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      FrameworkElement frameworkElement2 = SettingsContent.IntegrationLogo(i, 26.0);
      frameworkElement2.VerticalAlignment = VerticalAlignment.Center;
      grid.Children.Add(frameworkElement2);
      Grid.SetColumn(stackPanel2, 1);
      grid.Children.Add(stackPanel2);
      if (frameworkElement != null)
      {
        frameworkElement.VerticalAlignment = VerticalAlignment.Center;
        Grid.SetColumn(frameworkElement, 2);
        grid.Children.Add(frameworkElement);
      }
      StackPanel stackPanel3 = new StackPanel
      {
        Spacing = 8.0,
        Padding = new Thickness(14.0, 10.0, 14.0, 10.0),
        Children = { grid }
      };
      if (flag3)
      {
        stackPanel3.Children.Add(this.AccountsCard(i));
      }
      Grid hover = new Grid
      {
        Background = Ui.Transparent,
        Children = { stackPanel3 }
      };
      hover.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        hover.Background = Ui.Brush(this.look.Ink.WithAlpha(0.04));
      };
      hover.PointerExited += delegate(object _, PointerRoutedEventArgs e)
      {
        if (PointerFrames.IsStale(e))
        {
          return;
        }
        hover.Background = Ui.Transparent;
      };
      return hover;
    }

    // Token: 0x06000957 RID: 2391 RVA: 0x0054A0A4 File Offset: 0x0054A0A4
    private static FrameworkElement ConnectionBadge(string title, Color tint, bool connected)
    {
      FrameworkElement frameworkElement;
      if (!connected)
      {
        if (!Anim.ReduceMotion)
        {
          ProgressRing progressRing = new ProgressRing();
          progressRing.Width = 9.0;
          progressRing.Height = 9.0;
          progressRing.MinWidth = 0.0;
          progressRing.MinHeight = 0.0;
          progressRing.IsActive = true;
          frameworkElement = progressRing;
          progressRing.Foreground = Ui.Brush(tint);
        }
        else
        {
          frameworkElement = Icons.Glyph("\ue823", 10.0, tint);
        }
      }
      else
      {
        Path path = new Path();
        path.Data = (Geometry)XamlBindingHelper.ConvertValue(typeof(Geometry), "M1,6 L4.5,9 L11,2");
        path.Stroke = Ui.Brush(tint);
        path.StrokeThickness = 1.8;
        path.StrokeStartLineCap = PenLineCap.Round;
        path.StrokeEndLineCap = PenLineCap.Round;
        path.StrokeLineJoin = PenLineJoin.Round;
        path.Width = 12.0;
        frameworkElement = path;
        path.Height = 12.0;
      }
      FrameworkElement frameworkElement2 = frameworkElement;
      Grid grid = new Grid
      {
        Width = 12.0,
        Height = 12.0,
        Children = { frameworkElement2 }
      };
      frameworkElement2.HorizontalAlignment = HorizontalAlignment.Center;
      frameworkElement2.VerticalAlignment = VerticalAlignment.Center;
      TextBlock textBlock = Ui.Text(title, 10.0, FontWeights.SemiBold, tint, TextAlignment.Left, 0, false, false);
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      return new Border
      {
        Background = Ui.Brush(tint.WithAlpha(0.1)),
        Padding = new Thickness(7.0, 4.0, 7.0, 4.0),
        VerticalAlignment = VerticalAlignment.Center,
        Child = Ui.H(5.0, new UIElement[] { grid, textBlock })
      }.Capsule();
    }

    // Token: 0x06000958 RID: 2392 RVA: 0x0054A27C File Offset: 0x0054A27C
    private static FrameworkElement StatusChip(string text, Color color)
    {
      return Ui.H(4.0, new UIElement[]
      {
        new Ellipse
        {
          Width = 5.0,
          Height = 5.0,
          Fill = Ui.Brush(color),
          VerticalAlignment = VerticalAlignment.Center
        },
        Ui.Text(text, 10.0, FontWeights.SemiBold, color, TextAlignment.Left, 0, false, false).Align(HorizontalAlignment.Left, VerticalAlignment.Center)
      });
    }

    // Token: 0x06000959 RID: 2393 RVA: 0x0054A2FC File Offset: 0x0054A2FC
    private FrameworkElement AccountsCard(AgentIntegration i)
    {
      StackPanel stackPanel = new StackPanel();
      bool flag = i.Accounts.Count > 1;
      List<string> list = new string[] { "Work", "Personal" }.Where<string>((string l) => !i.Accounts.Any<AgentIntegrationAccount>((AgentIntegrationAccount a) => string.Equals(a.TrimmedLabel, l, StringComparison.OrdinalIgnoreCase))).ToList<string>();
      for (int j = 0; j < i.Accounts.Count; j++)
      {
        if (j > 0)
        {
          stackPanel.Children.Add(SK.Divider(52.0));
        }
        stackPanel.Children.Add(this.AccountRow(i, i.Accounts[j], j, !flag, list));
      }
      if (i.Status != "needs_reconnect")
      {
        stackPanel.Children.Add(SK.Divider(0.0));
        if (i.HasReachedActiveAccountLimit)
        {
          UIElementCollection children = stackPanel.Children;
          double num = 10.0;
          UIElement[] array = new UIElement[2];
          array[0] = Icons.Glyph("\ue946", 11.0, SK.TextTertiary).Size(30.0, 30.0);
          int num2 = 1;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(70, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(5);
          defaultInterpolatedStringHandler.AppendLiteral(" accounts is the most one app can have. Disconnect one to add another.");
          array[num2] = Ui.Text(defaultInterpolatedStringHandler.ToStringAndClear(), 11.0, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, true, false);
          children.Add(Ui.H(num, array).Pad(10.0, 7.0, 10.0, 7.0));
        }
        else
        {
          Pressable pressable = new Pressable(true);
          bool flag2 = this.integrationBusy.Contains(i.Id) || ((this.awaitingConnection != null) ? this.awaitingConnection.GetValueOrDefault().Item1 : null) == i.Id;
          Grid grid = new Grid
          {
            Width = 30.0,
            Height = 30.0
          };
          grid.Children.Add(new Ellipse
          {
            Stroke = Ui.Brush(SK.Ink.WithAlpha(0.22)),
            StrokeThickness = 1.0,
            StrokeDashArray = new DoubleCollection { 3.0, 2.5 }
          });
          UIElementCollection children2 = grid.Children;
          UIElement uielement;
          if (!flag2)
          {
            uielement = Icons.Glyph("\ue710", 10.5, SK.TextSecondary);
          }
          else
          {
            ProgressRing progressRing = new ProgressRing();
            progressRing.Width = 12.0;
            progressRing.Height = 12.0;
            uielement = progressRing;
            progressRing.IsActive = true;
          }
          children2.Add(uielement);
          TextBlock label = Ui.Text(flag2 ? ("Checking for your new " + i.DisplayName + " account…") : ("Add another " + i.DisplayName + " account"), 12.0, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 1, false, false);
          label.VerticalAlignment = VerticalAlignment.Center;
          pressable.Children.Add(Ui.H(10.0, new UIElement[] { grid, label }).Pad(10.0, 7.0, 10.0, 7.0));
          pressable.HoverChanged += delegate(bool h)
          {
            label.Foreground = Ui.Brush(h ? this.look.ThemeTextPrimary : this.look.ThemeTextSecondary);
          };
          pressable.Tip("Connect another " + i.DisplayName + " account, like a work and a personal one");
          if (!flag2)
          {
            pressable.Click += delegate
            {
              this.ConnectAsync(i, true);
            };
          }
          stackPanel.Children.Add(pressable);
        }
      }
      return new Border
      {
        Margin = new Thickness(38.0, 0.0, 0.0, 0.0),
        CornerRadius = new CornerRadius(11.0),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.04)),
        BorderBrush = Ui.Brush(SK.Ink.WithAlpha(0.07)),
        BorderThickness = new Thickness(1.0),
        Child = stackPanel
      };
    }

    // Token: 0x0600095A RID: 2394 RVA: 0x0054A7F8 File Offset: 0x0054A7F8
    private FrameworkElement AccountRow(AgentIntegration i, AgentIntegrationAccount a, int position, bool isOnly, List<string> freeQuickLabels)
    {
      string text = a.TrimmedAccountName ?? a.TrimmedLabel;
      string text2;
      if ((text2 = text) == null)
      {
        if (!isOnly)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 2);
          defaultInterpolatedStringHandler.AppendFormatted(i.DisplayName);
          defaultInterpolatedStringHandler.AppendLiteral(" account ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(position + 1);
          text2 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          text2 = i.DisplayName + " account";
        }
      }
      string text3 = text2;
      Grid grid = new Grid
      {
        Width = 30.0,
        Height = 30.0,
        Opacity = (a.IsActive ? 1.0 : 0.75)
      };
      if (text != null)
      {
        grid.Children.Add(new Border
        {
          CornerRadius = new CornerRadius(a.IsOrganization ? 7.800000000000001 : 15.0),
          Background = Ui.Brush(SettingsContent.InitialColor(text))
        });
        grid.Children.Add(Ui.Text(text.Substring(0, 1).ToUpperInvariant(), 13.5, FontWeights.SemiBold, Colors.White, TextAlignment.Left, 0, false, false).Center<TextBlock>());
      }
      else
      {
        grid.Children.Add(new Ellipse
        {
          Fill = Ui.Brush(SK.Ink.WithAlpha(0.1))
        });
        grid.Children.Add(SettingsContent.IntegrationLogo(i, 17.0).Center<FrameworkElement>());
      }
      string avatarUrl = a.AvatarUrl;
      Uri uri;
      if (avatarUrl != null && Uri.TryCreate(avatarUrl, UriKind.Absolute, out uri) && uri.Scheme == "https")
      {
        grid.Children.Add(new Ellipse
        {
          Fill = new ImageBrush
          {
            ImageSource = new BitmapImage(uri),
            Stretch = Stretch.UniformToFill
          }
        });
      }
      if (!a.IsActive)
      {
        grid.Children.Add(new Ellipse
        {
          Width = 9.0,
          Height = 9.0,
          Fill = Ui.Brush(SK.Warning),
          Stroke = Ui.Brush(DS.Black(0.6)),
          StrokeThickness = 1.5,
          HorizontalAlignment = HorizontalAlignment.Right,
          VerticalAlignment = VerticalAlignment.Bottom,
          Margin = new Thickness(0.0, 0.0, -1.0, -1.0)
        });
      }
      StackPanel stackPanel = Ui.H(6.0, new UIElement[] { Ui.Text(text3, 13.0, FontWeights.SemiBold, SK.TextPrimary, TextAlignment.Left, 1, false, false) });
      bool flag = this.editingLabelAccount == a.ConnectedAccountId;
      if (flag)
      {
        ValueTuple<Grid, TextBox> valueTuple = SK.SearchField("Label, like Work", a.TrimmedLabel ?? "", delegate(string _)
        {
        }, false, 11.5, null);
        Grid item = valueTuple.Item1;
        TextBox input = valueTuple.Item2;
        item.Width = 120.0;
        input.Padding = new Thickness(7.0, 1.5, 7.0, 1.5);
        input.CornerRadius = new CornerRadius(6.0);
        input.Loaded += delegate(object _, RoutedEventArgs _)
        {
          input.Focus(FocusState.Programmatic);
        };
        input.KeyDown += delegate(object _, KeyRoutedEventArgs e)
        {
          if (e.Key == VirtualKey.Enter)
          {
            e.Handled = true;
            _ = this.SaveLabelAsync(i, a, input.Text);
            return;
          }
          if (e.Key == VirtualKey.Escape)
          {
            this.editingLabelAccount = null;
            this.Render();
          }
        };
        input.LostFocus += delegate(object _, RoutedEventArgs _)
        {
          if (this.editingLabelAccount == a.ConnectedAccountId)
          {
            this.SaveLabelAsync(i, a, input.Text);
          }
        };
        stackPanel.Children.Add(item);
      }
      else if (a.TrimmedAccountName != null && !isOnly)
      {
        string trimmedLabel = a.TrimmedLabel;
        if (trimmedLabel != null)
        {
          Pressable badge = new Pressable(true)
          {
            CornerRadius = new CornerRadius(100.0),
            Background = Ui.Brush(DS.Rgb(0.36, 0.58, 1.0, 0.18))
          };
          badge.Children.Add(Ui.Text(trimmedLabel, 10.5, FontWeights.SemiBold, DS.Rgb(0.62, 0.78, 1.0, 1.0), TextAlignment.Left, 0, false, false).Pad(7.0, 2.0, 7.0, 2.0));
          badge.HoverChanged += delegate(bool h)
          {
            badge.Background = Ui.Brush(DS.Rgb(0.36, 0.58, 1.0, h ? 0.26 : 0.18));
          };
          badge.Tip("Rename this label");
          badge.Click += delegate
          {
            this.editingLabelAccount = a.ConnectedAccountId;
            this.Render();
          };
          stackPanel.Children.Add(badge);
        }
      }
      FrameworkElement frameworkElement;
      if (!a.IsActive)
      {
        frameworkElement = Ui.Text("Sign-in expired", 11.0, FontWeights.SemiBold, SK.Warning, TextAlignment.Left, 0, false, false);
      }
      else if (flag)
      {
        frameworkElement = Ui.Text("Press Enter to save", 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 0, false, false);
      }
      else
      {
        StackPanel stackPanel2 = Ui.H(6.0, new UIElement[] { SettingsContent.StatusChip("Connected", SK.Positive) });
        if (!isOnly && a.TrimmedLabel == null)
        {
          if (freeQuickLabels.Count == 0)
          {
            stackPanel2.Children.Add(SettingsContent.LabelChip("Add label", delegate
            {
              this.editingLabelAccount = a.ConnectedAccountId;
              this.Render();
            }));
          }
          else
          {
            stackPanel2.Children.Add(Ui.Text("Label as", 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 0, false, false).Tip("Label your accounts so HeyClicky knows which one to use."));
            using (List<string>.Enumerator enumerator = freeQuickLabels.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                string quick = enumerator.Current;
                stackPanel2.Children.Add(SettingsContent.LabelChip(quick, delegate
                {
                  this.SaveLabelAsync(i, a, quick);
                }));
              }
            }
            stackPanel2.Children.Add(SettingsContent.LabelChip("Other…", delegate
            {
              this.editingLabelAccount = a.ConnectedAccountId;
              this.Render();
            }));
          }
        }
        else
        {
          List<string> list = new List<string>();
          if (a.IsOrganization)
          {
            list.Add("Organization");
          }
          DateTimeOffset? connectedDate = a.ConnectedDate;
          if (connectedDate != null)
          {
            DateTimeOffset valueOrDefault = connectedDate.GetValueOrDefault();
            list.Add("since " + valueOrDefault.ToLocalTime().ToString((valueOrDefault.Year == DateTime.Now.Year) ? "MMM d" : "MMM d, yyyy", CultureInfo.InvariantCulture));
          }
          if (list.Count > 0)
          {
            stackPanel2.Children.Add(Ui.Text("· " + string.Join(" · ", list), 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 1, false, false));
          }
        }
        frameworkElement = stackPanel2;
      }
      StackPanel stackPanel3 = new StackPanel
      {
        Spacing = 2.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          stackPanel.Size(0.0, 20.0),
          frameworkElement
        }
      };
      bool flag2 = this.accountBusy.Contains(a.ConnectedAccountId) || (isOnly && this.integrationBusy.Contains(i.Id));
      FrameworkElement frameworkElement2;
      if (a.IsActive)
      {
        if (flag2)
        {
          frameworkElement2 = new GelButton("Disconnect", GelAppearance.SecondaryOutline, GelButton.Sizing.Compact, 15.0, null, null, null)
          {
            IsEnabledGel = false
          };
        }
        else if (this.confirmDisconnectAccount == a.ConnectedAccountId)
        {
          GelButton gelButton = new GelButton("Disconnect", GelAppearance.SecondaryOutline, GelButton.Sizing.Compact, 15.0, null, null, null);
          gelButton.Click += delegate
          {
            this.DisconnectAsync(i, isOnly ? null : a);
          };
          GelButton gelButton2 = new GelButton("Keep", GelAppearance.PrimaryBlue, GelButton.Sizing.Compact, 15.0, null, null, null);
          gelButton2.Click += delegate
          {
            this.confirmDisconnectAccount = null;
            this.Render();
          };
          frameworkElement2 = Ui.H(6.0, new UIElement[] { gelButton, gelButton2 });
        }
        else
        {
          GelButton gelButton3 = new GelButton("Disconnect", GelAppearance.SecondaryOutline, GelButton.Sizing.Compact, 15.0, null, null, null);
          gelButton3.Click += delegate
          {
            this.confirmDisconnectAccount = a.ConnectedAccountId;
            this.Render();
          };
          frameworkElement2 = gelButton3;
        }
      }
      else
      {
        GelButton gelButton4 = new GelButton("", GelAppearance.SecondaryOutline, GelButton.Sizing.Compact, 15.0, "\ue711", null, null);
        gelButton4.Tip("Remove this expired account");
        gelButton4.Click += delegate
        {
          this.DisconnectAsync(i, (i.Accounts.Count <= 1) ? null : a);
        };
        GelButton gelButton5 = new GelButton("Reconnect", GelAppearance.PrimaryBlue, GelButton.Sizing.Compact, 15.0, null, null, null);
        gelButton5.Tip("Sign in to this " + i.DisplayName + " account again");
        gelButton5.Click += delegate
        {
          this.ConnectAsync(i, i.IsActiveConnection);
        };
        frameworkElement2 = Ui.H(6.0, new UIElement[] { gelButton4, gelButton5 });
      }
      Grid grid2 = new Grid();
      grid2.Padding = new Thickness(10.0, 8.0, 10.0, 8.0);
      grid2.ColumnSpacing = 10.0;
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition());
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.VerticalAlignment = VerticalAlignment.Center;
      grid2.Children.Add(grid);
      Grid.SetColumn(stackPanel3, 1);
      grid2.Children.Add(stackPanel3);
      frameworkElement2.VerticalAlignment = VerticalAlignment.Center;
      Grid.SetColumn(frameworkElement2, 2);
      grid2.Children.Add(frameworkElement2);
      return grid2;
    }

    // Token: 0x0600095B RID: 2395 RVA: 0x0054B3C0 File Offset: 0x0054B3C0
    private static Pressable LabelChip(string title, Action onClick)
    {
      MessagesPalette look = MessagesPalette.Current;
      Pressable p = new Pressable(true)
      {
        CornerRadius = new CornerRadius(100.0),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.07)),
        BorderBrush = Ui.Brush(SK.Ink.WithAlpha(0.08)),
        BorderThickness = new Thickness(1.0)
      };
      TextBlock t = Ui.Text(title, 10.5, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 0, false, false);
      p.Children.Add(t.Pad(7.0, 2.0, 7.0, 2.0));
      p.HoverChanged += delegate(bool h)
      {
        p.Background = Ui.Brush(look.Ink.WithAlpha(h ? 0.12 : 0.07));
        t.Foreground = Ui.Brush(h ? look.ThemeTextPrimary : look.ThemeTextSecondary);
      };
      p.Click += onClick;
      return p;
    }

    // Token: 0x0600095C RID: 2396 RVA: 0x0054B4DC File Offset: 0x0054B4DC
    private static Color InitialColor(string identity)
    {
      uint num = 5381U;
      foreach (char c in identity.ToLowerInvariant())
      {
        num = num * 33U + (uint)c;
      }
      double num2 = num % 360U / 360.0;
      double num3 = 0.42;
      double num4 = 0.58;
      int num5 = (int)(num2 * 6.0);
      double num6 = num2 * 6.0 - (double)num5;
      double num7 = num4 * (1.0 - num3);
      double num8 = num4 * (1.0 - num6 * num3);
      double num9 = num4 * (1.0 - (1.0 - num6) * num3);
      ValueTuple<double, double, double> valueTuple;
      switch (num5 % 6)
      {
      case 0:
        valueTuple = new ValueTuple<double, double, double>(num4, num9, num7);
        break;
      case 1:
        valueTuple = new ValueTuple<double, double, double>(num8, num4, num7);
        break;
      case 2:
        valueTuple = new ValueTuple<double, double, double>(num7, num4, num9);
        break;
      case 3:
        valueTuple = new ValueTuple<double, double, double>(num7, num8, num4);
        break;
      case 4:
        valueTuple = new ValueTuple<double, double, double>(num9, num7, num4);
        break;
      default:
        valueTuple = new ValueTuple<double, double, double>(num4, num7, num8);
        break;
      }
      ValueTuple<double, double, double> valueTuple2 = valueTuple;
      double item = valueTuple2.Item1;
      double item2 = valueTuple2.Item2;
      double item3 = valueTuple2.Item3;
      return DS.Rgb(item, item2, item3, 1.0);
    }

    // Token: 0x0600095D RID: 2397 RVA: 0x0054B644 File Offset: 0x0054B644
    private async Task ConnectAsync(AgentIntegration i, bool addingAnother = false)
    {
      SettingsContent.<>c__DisplayClass121_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass121_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.i = i;
      CS$<>8__locals1.addingAnother = addingAnother;
      if (CS$<>8__locals1.i.IsLocalIntegration)
      {
        SettingsContent.TrackIntegration("initiated", CS$<>8__locals1.i, "agent_space_settings", null);
        if (ObsidianVault.Configure(Native.GetForegroundWindow()))
        {
          this.justConnected = CS$<>8__locals1.i.Id;
          SettingsContent.TrackIntegration("connected", ObsidianVault.Row(), "agent_space_settings", null);
        }
        this.FillIntegrationList();
      }
      else
      {
        AgentService agents = this.host.Agents;
        if (agents != null && !this.integrationBusy.Contains(CS$<>8__locals1.i.Id))
        {
          this.integrationBusy.Add(CS$<>8__locals1.i.Id);
          this.integrationError = null;
          this.Render();
          SettingsContent.TrackIntegration("initiated", CS$<>8__locals1.i, "agent_space_settings", null);
          try
          {
            SettingsContent.<>c__DisplayClass121_1 CS$<>8__locals2 = new SettingsContent.<>c__DisplayClass121_1();
            CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
            string text = await agents.ConnectIntegrationAsync(CS$<>8__locals2.CS$<>8__locals1.i.PrimaryToolkitSlug, default(CancellationToken));
            CS$<>8__locals2.url = text;
            this.dq.TryEnqueue(delegate
            {
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.integrationBusy.Remove(CS$<>8__locals2.CS$<>8__locals1.i.Id);
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.awaitingConnection = new ValueTuple<string, int, DateTime>?(new ValueTuple<string, int, DateTime>(CS$<>8__locals2.CS$<>8__locals1.i.Id, CS$<>8__locals2.CS$<>8__locals1.i.ActiveAccounts.Count, DateTime.UtcNow));
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.justConnected = null;
              Log.Info("settings", "integration connect: " + CS$<>8__locals2.CS$<>8__locals1.i.Id + (CS$<>8__locals2.CS$<>8__locals1.addingAnother ? " (another account)" : "") + " → browser");
              AppHost app = CS$<>8__locals2.CS$<>8__locals1.<>4__this.host.App;
              if (app != null)
              {
                app.FoldNotchForOpenedLink();
              }
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.host.OpenUrl(CS$<>8__locals2.url);
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.Render();
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.PollAwaitedConnectionAsync();
            });
            CS$<>8__locals2 = null;
          }
          catch (Exception ex)
          {
            Exception ex3;
            Exception ex = ex3;
            Log.Warn("settings", "integration connect " + CS$<>8__locals1.i.Id + " failed: " + ex.Message);
            this.dq.TryEnqueue(delegate
            {
              CS$<>8__locals1.<>4__this.integrationBusy.Remove(CS$<>8__locals1.i.Id);
              SettingsContent <>4__this = CS$<>8__locals1.<>4__this;
              AgentBackendException ex2 = ex as AgentBackendException;
              <>4__this.integrationError = ((ex2 != null) ? SettingsContent.AgentErrorText(ex2) : ex.Message);
              CS$<>8__locals1.<>4__this.Render();
            });
          }
        }
      }
    }

    // Token: 0x0600095E RID: 2398 RVA: 0x0054B698 File Offset: 0x0054B698
    private static void TrackIntegration(string kind, AgentIntegration i, string source = "agent_space_settings", int? deletedAccountCount = null)
    {
      List<string> list = i.Toolkits.Select<AgentIntegrationToolkit, string>((AgentIntegrationToolkit t) => t.Slug).ToList<string>();
      if (kind == "initiated")
      {
        ClickyAnalytics.TrackIntegrationInitiated(i.Id, i.DisplayName, list, source, i.Status, new int?(i.ConnectedCount), new int?(i.TotalCount));
        return;
      }
      if (!(kind == "connected"))
      {
        ClickyAnalytics.TrackIntegrationDisconnected(i.Id, i.DisplayName, list, source, i.Status, new int?(i.ConnectedCount), new int?(i.TotalCount), deletedAccountCount);
        return;
      }
      ClickyAnalytics.TrackIntegrationConnected(i.Id, i.DisplayName, list, source, i.Status, new int?(i.ConnectedCount), new int?(i.TotalCount));
    }

    // Token: 0x0600095F RID: 2399 RVA: 0x0054B784 File Offset: 0x0054B784
    private static string AgentErrorText(AgentBackendException ex)
    {
      try
      {
        JsonObject jsonObject = JsonNode.Parse(ex.Body, null, default(JsonDocumentOptions)) as JsonObject;
        if (jsonObject != null)
        {
          JsonNode jsonNode = jsonObject["error"];
          string text = ((jsonNode != null) ? jsonNode.ToString() : null);
          if (text != null && text.Length > 0)
          {
            return text;
          }
        }
      }
      catch
      {
      }
      int length = ex.Body.Length;
      if (length <= 0 || length >= 300 || ex.Body.TrimStart().StartsWith('{'))
      {
        return "Something went wrong. Try again in a moment.";
      }
      return ex.Body;
    }

    // Token: 0x06000960 RID: 2400 RVA: 0x0054B834 File Offset: 0x0054B834
    private async Task DisconnectAsync(AgentIntegration i, [Nullable(2)] AgentIntegrationAccount account)
    {
      if (i.IsLocalIntegration)
      {
        this.confirmDisconnectIntegration = null;
        SettingsContent.TrackIntegration("disconnected", i, "agent_space_settings", new int?(1));
        ObsidianVault.Unset();
        if (this.justConnected == i.Id)
        {
          this.justConnected = null;
        }
        this.FillIntegrationList();
      }
      else
      {
        AgentService agents = this.host.Agents;
        if (agents != null)
        {
          this.confirmDisconnectIntegration = null;
          this.confirmDisconnectAccount = null;
          if (account == null)
          {
            this.integrationBusy.Add(i.Id);
          }
          else
          {
            this.accountBusy.Add(account.ConnectedAccountId);
          }
          this.Render();
          try
          {
            AgentService agentService = agents;
            string primaryToolkitSlug = i.PrimaryToolkitSlug;
            AgentIntegrationAccount account2 = account;
            await agentService.DisconnectIntegrationAsync(primaryToolkitSlug, (account2 != null) ? account2.ConnectedAccountId : null, default(CancellationToken));
            Log.Info("settings", "integration disconnected: " + i.Id + ((account == null) ? "" : (" account " + account.ConnectedAccountId)));
            SettingsContent.TrackIntegration("disconnected", i, "settings", new int?((account == null) ? Math.Max(1, i.ActiveAccounts.Count) : 1));
          }
          catch (Exception ex)
          {
            Exception ex3;
            Exception ex = ex3;
            Log.Warn("settings", "integration disconnect " + i.Id + " failed: " + ex.Message);
            this.dq.TryEnqueue(delegate
            {
              SettingsContent <>4__this = this;
              AgentBackendException ex2 = ex as AgentBackendException;
              <>4__this.integrationError = ((ex2 != null) ? SettingsContent.AgentErrorText(ex2) : ex.Message);
            });
          }
          this.dq.TryEnqueue(delegate
          {
            this.integrationBusy.Remove(i.Id);
            if (account != null)
            {
              this.accountBusy.Remove(account.ConnectedAccountId);
            }
          });
          await this.RefreshIntegrationsAsync();
        }
      }
    }

    // Token: 0x06000961 RID: 2401 RVA: 0x0054B888 File Offset: 0x0054B888
    private async Task SaveLabelAsync(AgentIntegration i, AgentIntegrationAccount a, string label)
    {
      this.editingLabelAccount = null;
      string trimmed = label.Trim();
      if (!(trimmed == (a.TrimmedLabel ?? "")))
      {
        AgentService agents = this.host.Agents;
        if (agents != null)
        {
          this.accountBusy.Add(a.ConnectedAccountId);
          this.Render();
          try
          {
            await agents.LabelIntegrationAccountAsync(i.PrimaryToolkitSlug, a.ConnectedAccountId, trimmed, default(CancellationToken));
            string text = "settings";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 3);
            defaultInterpolatedStringHandler.AppendLiteral("integration label: ");
            defaultInterpolatedStringHandler.AppendFormatted(i.Id);
            defaultInterpolatedStringHandler.AppendLiteral(" ");
            defaultInterpolatedStringHandler.AppendFormatted(a.ConnectedAccountId);
            defaultInterpolatedStringHandler.AppendLiteral(" = \"");
            defaultInterpolatedStringHandler.AppendFormatted(trimmed);
            defaultInterpolatedStringHandler.AppendLiteral("\"");
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
          }
          catch (Exception ex)
          {
            Log.Warn("settings", "label save failed: " + ex.Message);
            this.dq.TryEnqueue(delegate
            {
              this.integrationError = "Couldn't save that label. Try again.";
            });
          }
          this.dq.TryEnqueue(delegate
          {
            this.accountBusy.Remove(a.ConnectedAccountId);
          });
          await this.RefreshIntegrationsAsync();
          return;
        }
      }
      this.Render();
    }

    // Token: 0x06000962 RID: 2402 RVA: 0x0054B8E4 File Offset: 0x0054B8E4
    private static FrameworkElement IntegrationsLoadingShimmer()
    {
      Border border = new Border
      {
        Width = 128.0,
        Height = 8.0,
        CornerRadius = new CornerRadius(4.0),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.08)),
        HorizontalAlignment = HorizontalAlignment.Left,
        Margin = new Thickness(4.0, 2.0, 4.0, 2.0)
      };
      StackPanel stackPanel = new StackPanel();
      for (int i = 0; i < SettingsContent.ShimmerNameWidths.Length; i++)
      {
        if (i > 0)
        {
          stackPanel.Children.Add(SK.Divider(52.0));
        }
        stackPanel.Children.Add(SettingsContent.ShimmerRow(SettingsContent.ShimmerNameWidths[i], SettingsContent.ShimmerDescriptionFractions[i]));
      }
      Grid card = new Grid
      {
        CornerRadius = new CornerRadius(14.0),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.05)),
        BorderBrush = Ui.Brush(SK.Ink.WithAlpha(0.06)),
        BorderThickness = new Thickness(1.0)
      };
      card.Children.Add(stackPanel);
      Border band = new Border
      {
        Width = 160.0,
        HorizontalAlignment = HorizontalAlignment.Left,
        IsHitTestVisible = false,
        Background = Ui.Horizontal(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, SK.Ink.WithAlpha(0.0)),
          new ValueTuple<double, Color>(0.5, SK.Ink.WithAlpha(0.06)),
          new ValueTuple<double, Color>(1.0, SK.Ink.WithAlpha(0.0))
        })
      };
      card.Children.Add(band);
      Ui.ClipRounded(card, 14.0);
      if (!Anim.ReduceMotion && !Anim.Instant)
      {
        band.Loaded += delegate(object _, RoutedEventArgs _)
        {
          double span = Math.Max(card.ActualWidth, 400.0) + 160.0;
          Anim.LoopVector3(band, "Translation", 1.6, (double t) => new Vector3((float)(-160.0 + span * (t / 1.6)), 0f, 0f), 48, 0.0);
        };
      }
      StackPanel stackPanel2 = new StackPanel();
      stackPanel2.Spacing = 7.0;
      stackPanel2.IsHitTestVisible = false;
      stackPanel2.Children.Add(border);
      stackPanel2.Children.Add(card);
      AutomationProperties.SetName(stackPanel2, "Loading integrations");
      return stackPanel2;
    }

    // Token: 0x06000963 RID: 2403 RVA: 0x0054BBA4 File Offset: 0x0054BBA4
    private static FrameworkElement ShimmerRow(double nameWidth, double descriptionFraction)
    {
      Grid grid = new Grid();
      grid.ColumnSpacing = 12.0;
      grid.Padding = new Thickness(14.0, 12.0, 14.0, 12.0);
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.Children.Add(new Border
      {
        Width = 26.0,
        Height = 26.0,
        CornerRadius = new CornerRadius(7.0),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.09))
      });
      Border description = new Border
      {
        Height = 7.0,
        CornerRadius = new CornerRadius(3.5),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.07)),
        HorizontalAlignment = HorizontalAlignment.Left
      };
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 6.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          new Border
          {
            Width = nameWidth,
            Height = 9.0,
            CornerRadius = new CornerRadius(4.5),
            Background = Ui.Brush(SK.Ink.WithAlpha(0.1)),
            HorizontalAlignment = HorizontalAlignment.Left
          },
          description
        }
      };
      stackPanel.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        description.Width = Math.Max(0.0, e.NewSize.Width * descriptionFraction);
      };
      Grid.SetColumn(stackPanel, 1);
      grid.Children.Add(stackPanel);
      Border border = new Border
      {
        Width = 64.0,
        Height = 24.0,
        CornerRadius = new CornerRadius(12.0),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.08)),
        VerticalAlignment = VerticalAlignment.Center
      };
      Grid.SetColumn(border, 2);
      grid.Children.Add(border);
      return grid;
    }

    // Token: 0x06000964 RID: 2404 RVA: 0x0054BE1C File Offset: 0x0054BE1C
    private IEnumerable<FrameworkElement> ProactiveDeveloperGroups()
    {
      SettingsContent.<ProactiveDeveloperGroups>d__130 <ProactiveDeveloperGroups>d__ = new SettingsContent.<ProactiveDeveloperGroups>d__130(-2);
      <ProactiveDeveloperGroups>d__.<>4__this = this;
      return <ProactiveDeveloperGroups>d__;
    }

    // Token: 0x06000965 RID: 2405 RVA: 0x0054BE2C File Offset: 0x0054BE2C
    public void RefreshReferralIfStale(double maximumAgeSeconds = 30.0)
    {
      if (this.referral != null)
      {
        DateTimeOffset? dateTimeOffset = this.referralLoadedAt;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          string text = this.referralLoadedFor;
          UserProfile user = this.s.Auth.User;
          if (text == ((user != null) ? user.UserId : null) && DateTimeOffset.UtcNow - valueOrDefault < TimeSpan.FromSeconds(maximumAgeSeconds))
          {
            return;
          }
        }
      }
      this.RefreshReferralAsync();
    }

    // Token: 0x06000966 RID: 2406 RVA: 0x0054BEA4 File Offset: 0x0054BEA4
    private async Task RefreshReferralAsync()
    {
      SettingsContent.<>c__DisplayClass143_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass143_0();
      CS$<>8__locals1.<>4__this = this;
      WorkerClient worker = this.host.Worker;
      if (worker != null && this.host.IsSignedIn && !this.referralLoading)
      {
        this.referralLoading = true;
        int referralRequestsForTests = this.ReferralRequestsForTests;
        this.ReferralRequestsForTests = referralRequestsForTests + 1;
        SettingsContent.<>c__DisplayClass143_0 CS$<>8__locals2 = CS$<>8__locals1;
        UserProfile user = this.s.Auth.User;
        CS$<>8__locals2.account = ((user != null) ? user.UserId : null);
        try
        {
          SettingsContent.<>c__DisplayClass143_1 CS$<>8__locals3 = new SettingsContent.<>c__DisplayClass143_1();
          CS$<>8__locals3.CS$<>8__locals1 = CS$<>8__locals1;
          JsonObject jsonObject = await worker.GetReferralAsync(default(CancellationToken));
          CS$<>8__locals3.r = jsonObject;
          this.dq.TryEnqueue(delegate
          {
            CS$<>8__locals3.CS$<>8__locals1.<>4__this.referralLoading = false;
            UserProfile user2 = CS$<>8__locals3.CS$<>8__locals1.<>4__this.s.Auth.User;
            if (((user2 != null) ? user2.UserId : null) != CS$<>8__locals3.CS$<>8__locals1.account)
            {
              return;
            }
            bool flag = !JsonNode.DeepEquals(CS$<>8__locals3.CS$<>8__locals1.<>4__this.referral, CS$<>8__locals3.r) || CS$<>8__locals3.CS$<>8__locals1.<>4__this.referralFailed != (CS$<>8__locals3.r == null);
            if (CS$<>8__locals3.r != null)
            {
              CS$<>8__locals3.CS$<>8__locals1.<>4__this.referralLoadedAt = new DateTimeOffset?(DateTimeOffset.UtcNow);
              CS$<>8__locals3.CS$<>8__locals1.<>4__this.referralLoadedFor = CS$<>8__locals3.CS$<>8__locals1.account;
            }
            CS$<>8__locals3.CS$<>8__locals1.<>4__this.referral = CS$<>8__locals3.r;
            CS$<>8__locals3.CS$<>8__locals1.<>4__this.referralFailed = CS$<>8__locals3.r == null;
            string text = "settings";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 3);
            defaultInterpolatedStringHandler.AppendLiteral("referral: enabled=");
            JsonObject r = CS$<>8__locals3.r;
            defaultInterpolatedStringHandler.AppendFormatted<JsonNode>((r != null) ? r["enabled"] : null);
            defaultInterpolatedStringHandler.AppendLiteral(", code=");
            JsonObject r2 = CS$<>8__locals3.r;
            defaultInterpolatedStringHandler.AppendFormatted<JsonNode>((r2 != null) ? r2["code"] : null);
            defaultInterpolatedStringHandler.AppendFormatted(flag ? "" : " (unchanged)");
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
            if (flag && CS$<>8__locals3.CS$<>8__locals1.<>4__this.page == SettingsWindow.Page.Referral)
            {
              CS$<>8__locals3.CS$<>8__locals1.<>4__this.Render();
            }
          });
          CS$<>8__locals3 = null;
        }
        catch (Exception ex)
        {
          Log.Warn("settings", "/me/referral failed: " + ex.Message);
          this.dq.TryEnqueue(delegate
          {
            CS$<>8__locals1.<>4__this.referralLoading = false;
            UserProfile user3 = CS$<>8__locals1.<>4__this.s.Auth.User;
            if (((user3 != null) ? user3.UserId : null) != CS$<>8__locals1.account)
            {
              return;
            }
            bool flag2 = !CS$<>8__locals1.<>4__this.referralFailed;
            CS$<>8__locals1.<>4__this.referralFailed = true;
            if (flag2 && CS$<>8__locals1.<>4__this.page == SettingsWindow.Page.Referral)
            {
              CS$<>8__locals1.<>4__this.Render();
            }
          });
        }
      }
    }

    // Token: 0x06000967 RID: 2407 RVA: 0x0054BEE8 File Offset: 0x0054BEE8
    private void ResetReferralIfAccountChanged()
    {
      if (this.referralLoadedFor != null)
      {
        string text = this.referralLoadedFor;
        UserProfile user = this.s.Auth.User;
        if (!(text == ((user != null) ? user.UserId : null)))
        {
          this.referral = null;
          this.referralFailed = false;
          this.referralLoadedAt = null;
          this.referralLoadedFor = null;
          return;
        }
      }
    }

    // Token: 0x17000179 RID: 377
    // (get) Token: 0x06000968 RID: 2408 RVA: 0x0054BF48 File Offset: 0x0054BF48
    // (set) Token: 0x06000969 RID: 2409 RVA: 0x0054BF50 File Offset: 0x0054BF50
    public bool IsSurfaceSettled
    {
      get
      {
        return this.isSurfaceSettled;
      }
      set
      {
        if (this.isSurfaceSettled == value)
        {
          return;
        }
        this.isSurfaceSettled = value;
        if (this.page == SettingsWindow.Page.Referral)
        {
          this.Render();
        }
      }
    }

    // Token: 0x0600096A RID: 2410 RVA: 0x0054BF74 File Offset: 0x0054BF74
    private void NoteReferralPageAppeared()
    {
      SettingsContent.<>c__DisplayClass152_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass152_0();
      CS$<>8__locals1.<>4__this = this;
      this.referralPageSettled = false;
      SettingsContent.<>c__DisplayClass152_0 CS$<>8__locals2 = CS$<>8__locals1;
      int num = this.referralSettleGeneration + 1;
      this.referralSettleGeneration = num;
      CS$<>8__locals2.generation = num;
      Microsoft.UI.Dispatching.DispatcherQueueTimer dispatcherQueueTimer = this.dq.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = TimeSpan.FromMilliseconds(150L, 0L);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(Microsoft.UI.Dispatching.DispatcherQueueTimer _, object _)
      {
        if (CS$<>8__locals1.generation != CS$<>8__locals1.<>4__this.referralSettleGeneration)
        {
          return;
        }
        CS$<>8__locals1.<>4__this.referralPageSettled = true;
        if (CS$<>8__locals1.<>4__this.page == SettingsWindow.Page.Referral)
        {
          CS$<>8__locals1.<>4__this.Render();
        }
      };
      dispatcherQueueTimer.Start();
    }

    // Token: 0x1700017A RID: 378
    // (get) Token: 0x0600096B RID: 2411 RVA: 0x0054BFF0 File Offset: 0x0054BFF0
    public bool ReferralShowsSkeleton
    {
      get
      {
        return this.page == SettingsWindow.Page.Referral && this.host.IsSignedIn && !this.referralFailed && (!this.isSurfaceSettled || !this.referralPageSettled || this.referral == null);
      }
    }

    // Token: 0x0600096C RID: 2412 RVA: 0x0054C030 File Offset: 0x0054C030
    public void SetReferralForTests(JsonObject body)
    {
      this.referral = body;
      this.referralFailed = false;
      if (this.page == SettingsWindow.Page.Referral)
      {
        this.Render();
      }
    }

    // Token: 0x1700017B RID: 379
    // (get) Token: 0x0600096D RID: 2413 RVA: 0x0054C050 File Offset: 0x0054C050
    [Nullable(2)]
    public JsonObject ReferralForTests
    {
      [NullableContext(2)]
      get
      {
        return this.referral;
      }
    }

    // Token: 0x1700017C RID: 380
    // (get) Token: 0x0600096E RID: 2414 RVA: 0x0054C058 File Offset: 0x0054C058
    // (set) Token: 0x0600096F RID: 2415 RVA: 0x0054C060 File Offset: 0x0054C060
    public int ReferralRequestsForTests { get; private set; }

    // Token: 0x06000970 RID: 2416 RVA: 0x0054C06C File Offset: 0x0054C06C
    public void ScrollReferralForTests(double offset)
    {
      this.scroller.ChangeView(null, new double?(offset), null, true);
    }

    // Token: 0x06000971 RID: 2417 RVA: 0x0054C0A0 File Offset: 0x0054C0A0
    [NullableContext(2)]
    private static string Str(JsonNode n)
    {
      JsonValue jsonValue = n as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text))
      {
        return null;
      }
      return text;
    }

    // Token: 0x06000972 RID: 2418 RVA: 0x0054C0C4 File Offset: 0x0054C0C4
    [NullableContext(2)]
    private static int Int(JsonNode n)
    {
      JsonValue jsonValue = n as JsonValue;
      int num;
      if (jsonValue != null && jsonValue.TryGetValue<int>(out num))
      {
        return num;
      }
      JsonValue jsonValue2 = n as JsonValue;
      double num2;
      if (jsonValue2 == null || !jsonValue2.TryGetValue<double>(out num2))
      {
        return 0;
      }
      return (int)num2;
    }

    // Token: 0x06000973 RID: 2419 RVA: 0x0054C100 File Offset: 0x0054C100
    [NullableContext(2)]
    private static double Dbl(JsonNode n)
    {
      JsonValue jsonValue = n as JsonValue;
      if (jsonValue == null)
      {
        return 0.0;
      }
      double num;
      if (!jsonValue.TryGetValue<double>(out num))
      {
        long num2;
        int num3;
        return (double)(jsonValue.TryGetValue<long>(out num2) ? num2 : ((long)(jsonValue.TryGetValue<int>(out num3) ? num3 : 0)));
      }
      return num;
    }

    // Token: 0x06000974 RID: 2420 RVA: 0x0054C14C File Offset: 0x0054C14C
    [NullableContext(2)]
    private static bool Bool(JsonNode n)
    {
      JsonValue jsonValue = n as JsonValue;
      bool flag;
      return jsonValue != null && jsonValue.TryGetValue<bool>(out flag) && flag;
    }

    // Token: 0x06000975 RID: 2421 RVA: 0x0054C170 File Offset: 0x0054C170
    private static string Dollars(int cents)
    {
      if (cents % 100 != 0)
      {
        return ((double)cents / 100.0).ToString("$#,0.00", CultureInfo.InvariantCulture);
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
      defaultInterpolatedStringHandler.AppendLiteral("$");
      defaultInterpolatedStringHandler.AppendFormatted<int>(cents / 100, "N0");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000976 RID: 2422 RVA: 0x0054C1D0 File Offset: 0x0054C1D0
    private FrameworkElement ReferralPage()
    {
      if (!this.host.IsSignedIn)
      {
        return SettingsContent.ReferralFrame(new <>z__ReadOnlySingleElementList<FrameworkElement>(SettingsContent.ReferralMessage("Refer a friend", "Sign in to get your referral link.")));
      }
      if (this.ReferralShowsSkeleton)
      {
        this.referralSkeletonShown = true;
        return SettingsContent.ReferralFrame(new <>z__ReadOnlySingleElementList<FrameworkElement>(SettingsContent.ReferralLoadingSkeleton()));
      }
      bool flag = this.referralSkeletonShown && !Anim.ReduceMotion;
      this.referralSkeletonShown = false;
      FrameworkElement built = this.BuildReferralPage();
      if (flag)
      {
        built.Opacity = 0.0;
        built.Loaded += delegate(object _, RoutedEventArgs _)
        {
          Anim.Opacity(built, 1f, 0.2, null, 0.0);
        };
      }
      return built;
    }

    // Token: 0x06000977 RID: 2423 RVA: 0x0054C284 File Offset: 0x0054C284
    private FrameworkElement BuildReferralPage()
    {
      if (this.referral == null)
      {
        return SettingsContent.ReferralFrame(new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
        {
          SettingsContent.ReferralMessage("Refer a friend", "HeyClicky couldn't load your referral page."),
          SK.PillButton("Try again", delegate
          {
            this.RefreshReferralAsync();
          }, false, false, false)
        }));
      }
      if (!SettingsContent.Bool(this.referral["enabled"]))
      {
        return SettingsContent.ReferralFrame(new <>z__ReadOnlySingleElementList<FrameworkElement>(SettingsContent.ReferralMessage("Referrals are paused", "We're not taking new referrals right now. Check back soon.")));
      }
      JsonObject jsonObject;
      if ((jsonObject = this.referral["terms"] as JsonObject) == null)
      {
        jsonObject = new JsonObject(null);
      }
      JsonObject jsonObject2 = jsonObject;
      JsonObject jsonObject3;
      if ((jsonObject3 = this.referral["stats"] as JsonObject) == null)
      {
        jsonObject3 = new JsonObject(null);
      }
      JsonObject jsonObject4 = jsonObject3;
      bool flag = SettingsContent.Int(jsonObject4["referred_count"]) > 0;
      return SettingsContent.ReferralFrame(new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
      {
        this.HeldCodeCard(jsonObject2, jsonObject4),
        this.NegativeBalanceNote(jsonObject4),
        this.TermsCard(jsonObject2),
        flag ? this.EarningsGroup(jsonObject4, jsonObject2) : null,
        this.FriendsGroup(jsonObject4),
        this.EstimatorGroup(jsonObject2),
        this.PayoutGroup(jsonObject4, jsonObject2)
      }));
    }

    // Token: 0x06000978 RID: 2424 RVA: 0x0054C3CC File Offset: 0x0054C3CC
    private static FrameworkElement ReferralFrame([Nullable(new byte[] { 1, 2 })] IEnumerable<FrameworkElement> sections)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 26.0,
        MaxWidth = 640.0,
        HorizontalAlignment = HorizontalAlignment.Stretch
      };
      foreach (FrameworkElement frameworkElement in sections)
      {
        if (frameworkElement != null)
        {
          stackPanel.Children.Add(frameworkElement);
        }
      }
      return new Grid
      {
        Padding = new Thickness(32.0, 0.0, 32.0, 36.0),
        Children = { stackPanel }
      };
    }

    // Token: 0x06000979 RID: 2425 RVA: 0x0054C488 File Offset: 0x0054C488
    private static FrameworkElement ReferralMessage(string title, string detail)
    {
      return new StackPanel
      {
        Spacing = 5.0,
        Margin = new Thickness(0.0, 10.0, 0.0, 0.0),
        Children = 
        {
          Ui.Text(title, 22.0, FontWeights.SemiBold, SK.TextPrimary, TextAlignment.Left, 0, false, false),
          Ui.Text(detail, 12.0, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, true, false)
        }
      };
    }

    // Token: 0x0600097A RID: 2426 RVA: 0x0054C528 File Offset: 0x0054C528
    private static TextBlock CardLabel(string text)
    {
      TextBlock textBlock = Ui.Text(text.ToUpperInvariant(), 9.5, FontWeights.Bold, MessagesPalette.Current.TertiaryText, TextAlignment.Left, 0, false, false);
      textBlock.CharacterSpacing = 84;
      return textBlock;
    }

    // Token: 0x0600097B RID: 2427 RVA: 0x0054C55C File Offset: 0x0054C55C
    private FrameworkElement TermsCard(JsonObject terms)
    {
      Grid grid = new Grid
      {
        ColumnSpacing = 10.0
      };
      for (int i = 0; i < 3; i++)
      {
        grid.ColumnDefinitions.Add(new ColumnDefinition());
      }
      int num = SettingsContent.Int(terms["commission_months"]);
      int num2 = SettingsContent.Int(terms["customer_discount_months"]);
      double num3 = SettingsContent.Dbl(terms["commission_percent"]);
      string text = "Of their monthly subscription.";
      JsonObject jsonObject = this.referral;
      object obj;
      if (jsonObject == null)
      {
        obj = null;
      }
      else
      {
        JsonNode jsonNode = jsonObject["estimator"];
        obj = ((jsonNode != null) ? jsonNode["plans"] : null);
      }
      JsonArray jsonArray = obj as JsonArray;
      JsonObject jsonObject2 = ((jsonArray != null) ? jsonArray.OfType<JsonObject>().FirstOrDefault<JsonObject>() : null);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (jsonObject2 != null)
      {
        int num4 = SettingsContent.Int(jsonObject2["monthly_price_usd_cents"]);
        if (num4 > 0)
        {
          int perReferralUsdCents = ReferralEarningsEstimator.Estimate(1, 1, num4, num3, num).PerReferralUsdCents;
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 2);
          defaultInterpolatedStringHandler.AppendLiteral("For example, a ");
          defaultInterpolatedStringHandler.AppendFormatted(ReferralFormatting.CompactDollars(num4));
          defaultInterpolatedStringHandler.AppendLiteral("/month plan earns you ");
          defaultInterpolatedStringHandler.AppendFormatted(ReferralFormatting.CompactDollars(perReferralUsdCents));
          defaultInterpolatedStringHandler.AppendLiteral(" each month.");
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
      }
      string text2 = (SettingsContent.Bool(terms["customer_discount_applies_to_yearly_plans"]) ? "Pro or Max" : "Pro or Max on a monthly plan");
      ValueTuple<string, string, string>[] array = new ValueTuple<string, string, string>[3];
      array[0] = new ValueTuple<string, string, string>("For every friend", "Earn " + ReferralFormatting.Percent(num3), text);
      int num5 = 1;
      string text3 = "Your friend gets";
      string text4 = ReferralFormatting.Percent(SettingsContent.Dbl(terms["customer_discount_percent"])) + " off";
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 2);
      defaultInterpolatedStringHandler.AppendLiteral("Their ");
      string text5;
      if (num2 != 1)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(13, 1);
        defaultInterpolatedStringHandler2.AppendLiteral("first ");
        defaultInterpolatedStringHandler2.AppendFormatted<int>(num2);
        defaultInterpolatedStringHandler2.AppendLiteral(" months");
        text5 = defaultInterpolatedStringHandler2.ToStringAndClear();
      }
      else
      {
        text5 = "first month";
      }
      defaultInterpolatedStringHandler.AppendFormatted(text5);
      defaultInterpolatedStringHandler.AppendLiteral(" of ");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral(".");
      array[num5] = new ValueTuple<string, string, string>(text3, text4, defaultInterpolatedStringHandler.ToStringAndClear());
      int num6 = 2;
      string text6 = "Earn for up to";
      string text7;
      if (num != 1)
      {
        if (num <= 0)
        {
          text7 = "Forever";
        }
        else
        {
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(num);
          defaultInterpolatedStringHandler.AppendLiteral(" months");
          text7 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
      }
      else
      {
        text7 = "1 month";
      }
      array[num6] = new ValueTuple<string, string, string>(text6, text7, "You earn from their first paid month, for as long as they stay subscribed.");
      ValueTuple<string, string, string>[] array2 = array;
      for (int j = 0; j < 3; j++)
      {
        ValueTuple<string, string, string> valueTuple = array2[j];
        string item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        string item3 = valueTuple.Item3;
        FrameworkElement frameworkElement = Ui.Shrink(Ui.Text(item2, 24.0, FontWeights.Black, MessagesPalette.Current.PrimaryText, TextAlignment.Left, 0, false, false), HorizontalAlignment.Left, 0.85);
        TextBlock textBlock = Ui.Text(item3, 11.0, FontWeights.Medium, MessagesPalette.Current.TertiaryText, TextAlignment.Left, 0, true, false);
        textBlock.LineHeight = 16.130000000000003;
        textBlock.Margin = new Thickness(0.0, 3.0, 0.0, 0.0);
        Border border = new Border
        {
          CornerRadius = new CornerRadius(14.0),
          Background = Ui.Brush(SK.Ink.WithAlpha(0.05)),
          BorderBrush = Ui.Brush(SK.Ink.WithAlpha(0.06)),
          BorderThickness = new Thickness(1.0),
          Padding = new Thickness(13.0),
          Child = new StackPanel
          {
            Spacing = 3.0,
            Children = 
            {
              Ui.Text(item, 12.0, FontWeights.Bold, MessagesPalette.Current.SecondaryText, TextAlignment.Left, 0, true, false),
              frameworkElement,
              textBlock
            }
          }
        };
        Grid.SetColumn(border, j);
        grid.Children.Add(border);
      }
      return grid;
    }

    // Token: 0x0600097C RID: 2428 RVA: 0x0054C9A8 File Offset: 0x0054C9A8
    private FrameworkElement HeldCodeCard(JsonObject terms, JsonObject stats)
    {
      XamlRoot xamlRoot = base.XamlRoot;
      double? num = ((xamlRoot != null) ? new double?(xamlRoot.Size.Width) : null);
      double num2;
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        if (valueOrDefault > 0.0)
        {
          num2 = valueOrDefault;
          goto IL_005A;
        }
      }
      num2 = 940.0;
      IL_005A:
      XamlRoot xamlRoot2 = base.XamlRoot;
      num = ((xamlRoot2 != null) ? new double?(xamlRoot2.Size.Height) : null);
      double num3;
      if (num != null)
      {
        double valueOrDefault2 = num.GetValueOrDefault();
        if (valueOrDefault2 > 0.0)
        {
          num3 = valueOrDefault2;
          goto IL_00AE;
        }
      }
      num3 = 595.0;
      IL_00AE:
      HomeMetrics homeMetrics = HomeMetrics.For(num2, num3);
      double num4 = Math.Round(homeMetrics.SuggestionCharacterSize * 1.35);
      double num5 = Math.Round(homeMetrics.SuggestionHandWidth * 1.2);
      double num6 = num5 * 20.0 / 28.0;
      double num7 = Math.Round(num4 * 152.0 / 195.0);
      PortraitAppearance portraitAppearance = PortraitAppearance.HomeMascot(this.s.Settings.CursorColorHex);
      PortraitExpression portraitExpression = ((this.referralCopied == null) ? PortraitExpression.Happy : PortraitExpression.Excited);
      PortraitView face = new PortraitView(portraitAppearance, num4, num4, portraitExpression, false, (Environment.GetEnvironmentVariable("HEYCLICKY_E2E_NOGLOW") == "1") ? null : new Color?(portraitAppearance.Hair), num4 * 0.08)
      {
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top,
        Tag = "referral-hero-clicky"
      };
      if (this.referralCelebrate && !Anim.Instant && !Anim.ReduceMotion)
      {
        this.referralCelebrate = false;
        face.Loaded += delegate(object _, RoutedEventArgs _)
        {
          face.Perform(PortraitReaction.Celebrate);
        };
      }
      this.referralCelebrate = false;
      Grid grid = new Grid
      {
        Height = num7,
        Width = num4,
        HorizontalAlignment = HorizontalAlignment.Left,
        Margin = new Thickness(0.0, -Math.Round(num4 * 0.11), 0.0, 0.0)
      };
      grid.Children.Add(face);
      grid.Clip = new RectangleGeometry
      {
        Rect = new Rect(-40.0, -60.0, num4 + 80.0, num7 + 60.0)
      };
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 14.0
      };
      int num8 = SettingsContent.Int(stats["earned_usd_cents"]);
      int num9 = SettingsContent.Int(stats["paid_out_usd_cents"]);
      int num10 = SettingsContent.Int(stats["available_usd_cents"]);
      bool flag = num8 > 0 || num9 > 0 || num10 < 0;
      stackPanel.Children.Add(flag ? this.EarnedMoneyBlock(stats, terms) : this.EarnPitchBlock(terms, homeMetrics));
      stackPanel.Children.Add(new Border
      {
        Height = 1.0,
        Background = Ui.Brush(SK.Ink.WithAlpha(0.07))
      });
      foreach (FrameworkElement frameworkElement in this.LinkParts(terms))
      {
        stackPanel.Children.Add(frameworkElement);
      }
      Border border = new Border
      {
        CornerRadius = new CornerRadius(20.0),
        Background = Ui.Brush(MessagesPalette.Current.IncomingBubble),
        BorderBrush = Ui.Brush(SK.Ink.WithAlpha(0.06)),
        BorderThickness = new Thickness(1.0),
        Padding = new Thickness(homeMetrics.ContentPadding + 2.0),
        Child = stackPanel,
        Tag = "referral-held-card"
      };
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = Math.Max(0.0, num4 / 2.0 - num5),
        IsHitTestVisible = false,
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top,
        Margin = new Thickness(num4 / 4.0 - num5 / 2.0, -num6 / 2.0, 0.0, 0.0)
      };
      for (int i = 0; i < 2; i++)
      {
        FrameworkElement frameworkElement2 = ClickyLedgeHand.Create(portraitAppearance, num5, num6);
        frameworkElement2.CenterPoint = new Vector3((float)num5 / 2f, (float)num6 / 2f, 0f);
        frameworkElement2.Rotation = (float)((i == 0) ? (-8) : 8);
        stackPanel2.Children.Add(frameworkElement2);
      }
      Grid grid2 = new Grid
      {
        Children = { border, stackPanel2 }
      };
      return new StackPanel
      {
        Children = { grid, grid2 },
        Tag = "referral-hero"
      };
    }

    // Token: 0x0600097D RID: 2429 RVA: 0x0054CF0C File Offset: 0x0054CF0C
    private FrameworkElement EarnPitchBlock(JsonObject terms, HomeMetrics metrics)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 7.0
      };
      stackPanel.Children.Add(Ui.Text("Invite your friends and earn.", metrics.SuggestionTitleFontSize + 7.0, FontWeights.Medium, MessagesPalette.Current.PrimaryText, TextAlignment.Left, 0, true, false));
      string text = this.PitchSubtitle(terms);
      if (text != null)
      {
        stackPanel.Children.Add(Ui.Text(text, 12.5, FontWeights.Medium, MessagesPalette.Current.SecondaryText, TextAlignment.Left, 0, true, false));
      }
      return stackPanel;
    }

    // Token: 0x0600097E RID: 2430 RVA: 0x0054CFA0 File Offset: 0x0054CFA0
    [return: Nullable(2)]
    private string PitchSubtitle(JsonObject terms)
    {
      JsonObject jsonObject = this.referral;
      object obj;
      if (jsonObject == null)
      {
        obj = null;
      }
      else
      {
        JsonNode jsonNode = jsonObject["estimator"];
        obj = ((jsonNode != null) ? jsonNode["plans"] : null);
      }
      JsonArray jsonArray;
      if ((jsonArray = obj as JsonArray) == null)
      {
        jsonArray = new JsonArray(null);
      }
      List<int> list = (from p in jsonArray.OfType<JsonObject>()
        select SettingsContent.Int(p["monthly_price_usd_cents"]) into p
        where p > 0
        select p).ToList<int>();
      if (list.Count == 0)
      {
        return null;
      }
      double commission = SettingsContent.Dbl(terms["commission_percent"]);
      int commissionMonths = SettingsContent.Int(terms["commission_months"]);
      int yearMonths = ((commissionMonths > 0) ? Math.Min(12, commissionMonths) : 12);
      int num = list.Min<int>((int price) => ReferralEarningsEstimator.Estimate(1, 1, price, commission, commissionMonths).PerReferralUsdCents);
      int num2 = list.Max<int>((int price) => ReferralEarningsEstimator.Estimate(1, yearMonths, price, commission, commissionMonths).PerReferralUsdCents);
      int num3 = num - num % 100;
      int num4 = num2 - num2 % 100;
      string text = ReferralFormatting.WholeDollars(num3);
      string text2 = ((num4 > num3) ? (text + " to " + ReferralFormatting.WholeDollars(num4)) : ("up to " + text));
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 2);
      defaultInterpolatedStringHandler.AppendLiteral("Earn ");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral(" per friend. They get ");
      defaultInterpolatedStringHandler.AppendFormatted(ReferralFormatting.Percent(SettingsContent.Dbl(terms["customer_discount_percent"])));
      defaultInterpolatedStringHandler.AppendLiteral(" off.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x0600097F RID: 2431 RVA: 0x0054D15C File Offset: 0x0054D15C
    private FrameworkElement EarnedMoneyBlock(JsonObject stats, JsonObject terms)
    {
      int num = SettingsContent.Int(stats["earned_usd_cents"]);
      int num2 = SettingsContent.Int(stats["available_usd_cents"]);
      int num3 = SettingsContent.Int(stats["on_hold_usd_cents"]);
      int num4 = SettingsContent.Int(terms["minimum_payout_usd_cents"]);
      int num5 = SettingsContent.Int(terms["commission_hold_days"]);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 2.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      stackPanel.Children.Add(SettingsContent.CardLabel("Total earned"));
      stackPanel.Children.Add(Ui.Text(ReferralFormatting.CompactDollars(num), 40.0, FontWeights.Black, MessagesPalette.Current.PrimaryText, TextAlignment.Left, 1, false, false));
      StackPanel stackPanel2 = Ui.H(12.0, Array.Empty<UIElement>());
      stackPanel2.VerticalAlignment = VerticalAlignment.Center;
      string text = ReferralFormatting.NegativeBalanceExplanation(num2, SettingsContent.Int(stats["reversed_payment_count"]), SettingsContent.Int(stats["reversed_usd_cents"]));
      stackPanel2.Children.Add(SettingsContent.BalancePill(ReferralFormatting.CompactDollars(num2), (num2 < 0) ? "to cover first" : "ready to pay out", (num2 < 0) ? GelAppearance.Peach : GelAppearance.Mint, text ?? ("Done waiting and all yours. Once it hits " + ReferralFormatting.CompactDollars(num4) + ", you can cash it out (\uff3e▽\uff3e)")));
      if (num3 > 0)
      {
        UIElementCollection children = stackPanel2.Children;
        string text2 = ReferralFormatting.CompactDollars(num3);
        string text3 = "on its way";
        GelAppearance gelAppearance = GelAppearance.SecondaryWhite;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(141, 1);
        defaultInterpolatedStringHandler.AppendLiteral("Your friends paid, so this is yours. Each payment just sits for ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num5);
        defaultInterpolatedStringHandler.AppendLiteral(" days in case it gets refunded, then it moves over to ready to pay out ( \u02d8▽\u02d8)");
        children.Add(SettingsContent.BalancePill(text2, text3, gelAppearance, defaultInterpolatedStringHandler.ToStringAndClear()));
      }
      Grid grid = new Grid
      {
        ColumnSpacing = 16.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetColumn(stackPanel2, 1);
      grid.Children.Add(stackPanel);
      grid.Children.Add(stackPanel2);
      StackPanel stackPanel3 = new StackPanel
      {
        Spacing = 12.0,
        Children = { grid }
      };
      if (num2 > 0 && num2 >= num4)
      {
        GelButton gelButton = new GelButton("Get paid", GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 100.0, null, null, null)
        {
          HorizontalAlignment = HorizontalAlignment.Right
        };
        string email = SettingsContent.Str(terms["payout_contact_email"]) ?? "hi@heyclicky.com";
        string code = SettingsContent.Str(this.referral["code"]) ?? "";
        gelButton.Click += delegate
        {
          ClickyAnalytics.TrackReferralPayoutRequested("earnings_card", true);
          SettingsHost settingsHost = this.host;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(22, 3);
          defaultInterpolatedStringHandler2.AppendLiteral("mailto:");
          defaultInterpolatedStringHandler2.AppendFormatted(email);
          defaultInterpolatedStringHandler2.AppendLiteral("?subject=");
          defaultInterpolatedStringHandler2.AppendFormatted(Uri.EscapeDataString("Referral payout (" + code + ")"));
          defaultInterpolatedStringHandler2.AppendLiteral("&body=");
          defaultInterpolatedStringHandler2.AppendFormatted(Uri.EscapeDataString("Hi! I'd like a payout for my referral code " + code + ". The best way to pay me is: "));
          settingsHost.OpenUrl(defaultInterpolatedStringHandler2.ToStringAndClear());
        };
        stackPanel3.Children.Add(gelButton);
      }
      return stackPanel3;
    }

    // Token: 0x06000980 RID: 2432 RVA: 0x0054D460 File Offset: 0x0054D460
    private static FrameworkElement BalancePill(string amount, string label, GelAppearance gel, string help)
    {
      Grid grid = new Grid
      {
        Tag = "referral-balance"
      };
      grid.Children.Add(new GelSurface(gel, 13.0));
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 1.0,
        Margin = new Thickness(13.0, 8.0, 13.0, 8.0)
      };
      stackPanel.Children.Add(Ui.Text(amount, 17.0, FontWeights.Bold, Colors.Black, TextAlignment.Left, 0, false, false));
      stackPanel.Children.Add(Ui.H(6.0, new UIElement[]
      {
        Ui.Text(label, 10.5, FontWeights.SemiBold, Colors.Black.WithAlpha(0.6), TextAlignment.Left, 0, false, false),
        Icons.Glyph("\ue946", 10.0, Colors.Black.WithAlpha(0.45))
      }));
      grid.Children.Add(stackPanel);
      ToolTipService.SetToolTip(grid, help);
      return grid;
    }

    // Token: 0x06000981 RID: 2433 RVA: 0x0054D590 File Offset: 0x0054D590
    private IEnumerable<FrameworkElement> LinkParts(JsonObject terms)
    {
      SettingsContent.<LinkParts>d__180 <LinkParts>d__ = new SettingsContent.<LinkParts>d__180(-2);
      <LinkParts>d__.<>4__this = this;
      <LinkParts>d__.<>3__terms = terms;
      return <LinkParts>d__;
    }

    // Token: 0x06000982 RID: 2434 RVA: 0x0054D5A8 File Offset: 0x0054D5A8
    private void CopyReferral(string what, string text)
    {
      DataPackage dataPackage = new DataPackage();
      dataPackage.SetText(text);
      Clipboard.SetContent(dataPackage);
      this.referralCopied = what;
      this.referralCelebrate = true;
      Log.Info("settings", "referral " + what + " copied");
      ClickyAnalytics.TrackReferralShareCopied(what);
      this.Render();
      Microsoft.UI.Dispatching.DispatcherQueueTimer dispatcherQueueTimer = this.dq.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(1.6);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(Microsoft.UI.Dispatching.DispatcherQueueTimer _, object _)
      {
        if (this.referralCopied == what)
        {
          this.referralCopied = null;
          if (this.page == SettingsWindow.Page.Referral)
          {
            this.Render();
          }
        }
      };
      dispatcherQueueTimer.Start();
    }

    // Token: 0x06000983 RID: 2435 RVA: 0x0054D660 File Offset: 0x0054D660
    private async Task SaveReferralCodeAsync()
    {
      SettingsContent.<>c__DisplayClass182_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass182_0();
      CS$<>8__locals1.<>4__this = this;
      WorkerClient worker = this.host.Worker;
      if (worker != null)
      {
        CS$<>8__locals1.draft = this.referralCodeDraft.Trim();
        try
        {
          SettingsContent.<>c__DisplayClass182_1 CS$<>8__locals2 = new SettingsContent.<>c__DisplayClass182_1();
          CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
          ReferralOutcomeResponse referralOutcomeResponse = await worker.ChangeReferralCodeAsync(CS$<>8__locals2.CS$<>8__locals1.draft, default(CancellationToken));
          CS$<>8__locals2.result = referralOutcomeResponse;
          ReferralOutcomeResponse result = CS$<>8__locals2.result;
          ClickyAnalytics.TrackReferralUsernameChanged(((result != null) ? result.Outcome : null) ?? "request_failed");
          this.dq.TryEnqueue(delegate
          {
            bool flag;
            if (CS$<>8__locals2.result != null)
            {
              string outcome = CS$<>8__locals2.result.Outcome;
              if (outcome == "changed" || outcome == "unchanged")
              {
                flag = true;
                goto IL_0034;
              }
            }
            flag = false;
            IL_0034:
            if (flag)
            {
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.editingReferralCode = false;
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.referralCodeProblem = "";
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.referralCelebrate = true;
              Log.Info("settings", "referral code = " + CS$<>8__locals2.CS$<>8__locals1.draft);
              CS$<>8__locals2.CS$<>8__locals1.<>4__this.RefreshReferralAsync();
            }
            else
            {
              SettingsContent <>4__this = CS$<>8__locals2.CS$<>8__locals1.<>4__this;
              ReferralOutcomeResponse result2 = CS$<>8__locals2.result;
              <>4__this.referralCodeProblem = ((result2 != null) ? result2.Message : null) ?? "That name isn't available. Try another.";
            }
            CS$<>8__locals2.CS$<>8__locals1.<>4__this.Render();
          });
          CS$<>8__locals2 = null;
        }
        catch (Exception ex)
        {
          Log.Warn("settings", "referral code change failed: " + ex.Message);
          ClickyAnalytics.TrackReferralUsernameChanged("request_failed");
          this.dq.TryEnqueue(delegate
          {
            CS$<>8__locals1.<>4__this.referralCodeProblem = "Couldn't save that. Try again in a moment.";
            CS$<>8__locals1.<>4__this.Render();
          });
        }
      }
    }

    // Token: 0x06000984 RID: 2436 RVA: 0x0054D6A4 File Offset: 0x0054D6A4
    private FrameworkElement EarningsGroup(JsonObject stats, JsonObject terms)
    {
      int num = SettingsContent.Int(terms["commission_hold_days"]);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 14.0,
        Padding = new Thickness(14.0)
      };
      JsonArray jsonArray;
      if ((jsonArray = this.referral["monthly_earnings"] as JsonArray) == null)
      {
        jsonArray = new JsonArray(null);
      }
      List<JsonObject> list = jsonArray.OfType<JsonObject>().ToList<JsonObject>();
      if (list.Any<JsonObject>((JsonObject m) => SettingsContent.Int(m["earned_usd_cents"]) > 0))
      {
        stackPanel.Children.Add(new MoneyBarChart(list.Select<JsonObject, int>((JsonObject m) => SettingsContent.Int(m["earned_usd_cents"])).ToList<int>(), list.Select<JsonObject, string>((JsonObject m) => ReferralFormatting.ShortMonthLabel(SettingsContent.Str(m["month"]) ?? "")).ToList<string>(), Enumerable.Range(0, list.Count), false, 96.0));
      }
      Grid grid = new Grid();
      for (int i = 0; i < 4; i++)
      {
        grid.ColumnDefinitions.Add(new ColumnDefinition());
      }
      FrameworkElement frameworkElement = SettingsContent.StatCell("Friends joined", SettingsContent.Int(stats["referred_count"]).ToString(CultureInfo.InvariantCulture), null);
      FrameworkElement frameworkElement2 = SettingsContent.StatCell("Paying you", SettingsContent.Int(stats["paying_count"]).ToString(CultureInfo.InvariantCulture), null);
      FrameworkElement frameworkElement3 = SettingsContent.StatCell("Last 30 days", ReferralFormatting.CompactDollars(SettingsContent.Int(stats["earned_last_30_days_usd_cents"])), null);
      FrameworkElement frameworkElement4 = SettingsContent.StatCell("Total paid out", ReferralFormatting.CompactDollars(SettingsContent.Int(stats["paid_out_usd_cents"])), null);
      Grid.SetColumn(frameworkElement2, 1);
      Grid.SetColumn(frameworkElement3, 2);
      Grid.SetColumn(frameworkElement4, 3);
      grid.Children.Add(frameworkElement);
      grid.Children.Add(frameworkElement2);
      grid.Children.Add(frameworkElement3);
      grid.Children.Add(frameworkElement4);
      stackPanel.Children.Add(grid);
      string text = "Your earnings";
      string text2;
      if (num <= 0)
      {
        text2 = null;
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(179, 1);
        defaultInterpolatedStringHandler.AppendLiteral("Your share of each payment is ready to cash out ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num);
        defaultInterpolatedStringHandler.AppendLiteral(" days after your friend pays. We wait because a payment can still be refunded during that time. Until then, it shows as on its way.");
        text2 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      return SK.Group(text, text2, new <>z__ReadOnlySingleElementList<FrameworkElement>(stackPanel), 14.0, null);
    }

    // Token: 0x06000985 RID: 2437 RVA: 0x0054D92C File Offset: 0x0054D92C
    [return: Nullable(2)]
    private FrameworkElement NegativeBalanceNote(JsonObject stats)
    {
      string text = ReferralFormatting.NegativeBalanceExplanation(SettingsContent.Int(stats["available_usd_cents"]), SettingsContent.Int(stats["reversed_payment_count"]), SettingsContent.Int(stats["reversed_usd_cents"]));
      if (text == null)
      {
        return null;
      }
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 3.0,
        Children = 
        {
          Ui.Text("Why your balance is below zero", 12.5, FontWeights.Bold, SK.TextPrimary, TextAlignment.Left, 0, false, false),
          Ui.Text(text, 12.0, FontWeights.Normal, SK.TextSecondary, TextAlignment.Left, 0, true, false)
        }
      };
      Grid grid = new Grid
      {
        ColumnSpacing = 10.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      TextBlock textBlock = Icons.Glyph("\ue946", 14.0, SK.Warning);
      textBlock.VerticalAlignment = VerticalAlignment.Top;
      Grid.SetColumn(stackPanel, 1);
      grid.Children.Add(textBlock);
      grid.Children.Add(stackPanel);
      return new Border
      {
        CornerRadius = new CornerRadius(16.0),
        Padding = new Thickness(14.0),
        Background = Ui.Brush(MessagesPalette.Current.IncomingBubble),
        BorderBrush = Ui.Brush(SK.Warning.WithAlpha(0.25)),
        BorderThickness = new Thickness(1.0),
        Child = grid
      };
    }

    // Token: 0x06000986 RID: 2438 RVA: 0x0054DAD8 File Offset: 0x0054DAD8
    [return: Nullable(2)]
    private FrameworkElement EstimatorGroup(JsonObject terms)
    {
      JsonObject jsonObject = this.referral["estimator"] as JsonObject;
      if (jsonObject == null)
      {
        return null;
      }
      JsonArray jsonArray;
      if ((jsonArray = jsonObject["plans"] as JsonArray) == null)
      {
        jsonArray = new JsonArray(null);
      }
      List<ReferralEstimatorCard.Plan> list = (from p in jsonArray.OfType<JsonObject>()
        select new ReferralEstimatorCard.Plan(SettingsContent.Str(p["key"]) ?? "", SettingsContent.Str(p["name"]) ?? "", SettingsContent.Int(p["monthly_price_usd_cents"]), SettingsContent.Int(p["default_referrals_per_month"])) into p
        where p.Key.Length > 0 && p.MonthlyPriceUsdCents > 0
        select p).ToList<ReferralEstimatorCard.Plan>();
      if (list.Count == 0)
      {
        return null;
      }
      ReferralEstimatorCard referralEstimatorCard = new ReferralEstimatorCard(list, SettingsContent.Int(jsonObject["max_referrals_per_month"]), SettingsContent.Int(jsonObject["default_months_retained"]), SettingsContent.Dbl(terms["commission_percent"]), SettingsContent.Int(terms["commission_months"]), this.estimatorState)
      {
        Tag = "referral-estimator"
      };
      return SK.Group("What you could make", new FrameworkElement[] { referralEstimatorCard });
    }

    // Token: 0x06000987 RID: 2439 RVA: 0x0054DBEC File Offset: 0x0054DBEC
    private static FrameworkElement StatCell(string title, string value, [Nullable(2)] string detail)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 4.0,
        Children = 
        {
          Ui.Shrink(Ui.Text(value, 17.0, FontWeights.Bold, SK.TextPrimary, TextAlignment.Left, 0, false, false), HorizontalAlignment.Left, 0.85),
          Ui.Text(title, 10.5, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 2, false, false)
        }
      };
      if (detail != null)
      {
        stackPanel.Children.Add(Ui.Text(detail, 10.5, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 0, true, false));
      }
      return stackPanel;
    }

    // Token: 0x06000988 RID: 2440 RVA: 0x0054DC9C File Offset: 0x0054DC9C
    private FrameworkElement FriendsGroup(JsonObject stats)
    {
      SettingsContent.<>c__DisplayClass188_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass188_0();
      CS$<>8__locals1.<>4__this = this;
      SettingsContent.<>c__DisplayClass188_0 CS$<>8__locals2 = CS$<>8__locals1;
      JsonArray jsonArray;
      if ((jsonArray = this.referral["invited_friends"] as JsonArray) == null)
      {
        jsonArray = new JsonArray(null);
      }
      CS$<>8__locals2.friends = jsonArray.OfType<JsonObject>().ToList<JsonObject>();
      int num = SettingsContent.Int(stats["referred_count"]);
      List<FrameworkElement> list = new List<FrameworkElement>();
      if (CS$<>8__locals1.friends.Count == 0)
      {
        Grid grid = new Grid
        {
          VerticalAlignment = VerticalAlignment.Center
        };
        grid.Children.Add(new GelSurface(GelAppearance.Peach, 100.0));
        TextBlock textBlock = Ui.Text("EXAMPLE", 9.0, FontWeights.Black, Colors.Black.WithAlpha(0.75), TextAlignment.Left, 0, false, false);
        textBlock.CharacterSpacing = 80;
        textBlock.Margin = new Thickness(7.0, 3.0, 7.0, 3.0);
        grid.Children.Add(textBlock);
        TextBlock textBlock2 = Ui.Text("Nobody has joined yet. Here's how your list will look once friends do.", 12.0, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 0, true, false);
        textBlock2.VerticalAlignment = VerticalAlignment.Center;
        Grid grid2 = new Grid
        {
          ColumnSpacing = 6.0,
          Padding = new Thickness(14.0, 11.0, 14.0, 11.0),
          Tag = "referral-example"
        };
        grid2.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        grid2.ColumnDefinitions.Add(new ColumnDefinition());
        Grid.SetColumn(textBlock2, 1);
        grid2.Children.Add(grid);
        grid2.Children.Add(textBlock2);
        list.Add(grid2);
        foreach (ValueTuple<string, string, string, string, string> valueTuple in SettingsContent.SampleFriends)
        {
          list.Add(SettingsContent.FriendRow(valueTuple.Item1, SettingsContent.SampleFriendPhoto(valueTuple.Item2), valueTuple.Item3, valueTuple.Item4, valueTuple.Item5, "Example: " + valueTuple.Item1 + ", " + SettingsContent.PlanLabel(valueTuple.Item3)));
        }
      }
      foreach (JsonObject jsonObject in CS$<>8__locals1.friends.Take<JsonObject>(this.referralVisibleFriends))
      {
        string text = SettingsContent.Str(jsonObject["display_name"]) ?? "A friend";
        string text2 = SettingsContent.Str(jsonObject["plan"]) ?? "free";
        int? num2 = ((jsonObject["subscribed_months"] == null) ? null : new int?(SettingsContent.Int(jsonObject["subscribed_months"])));
        DateTimeOffset dateTimeOffset2;
        DateTimeOffset? dateTimeOffset = (DateTimeOffset.TryParse(SettingsContent.Str(jsonObject["joined_at"]), CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTimeOffset2) ? new DateTimeOffset?(dateTimeOffset2) : null);
        string text3 = " · ";
        string[] array = new string[2];
        array[0] = ((dateTimeOffset == null) ? null : ("Joined " + dateTimeOffset.GetValueOrDefault().ToLocalTime().ToString("MMM d, yyyy", CultureInfo.InvariantCulture)));
        int num3 = 1;
        string text4;
        if (!(num2 > 0))
        {
          text4 = "Joined through your link";
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 2);
          defaultInterpolatedStringHandler.AppendLiteral("Subscribed for ");
          defaultInterpolatedStringHandler.AppendFormatted<int?>(num2);
          defaultInterpolatedStringHandler.AppendLiteral(" month");
          defaultInterpolatedStringHandler.AppendFormatted((num2.GetValueOrDefault() == 1) ? "" : "s");
          text4 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        array[num3] = text4;
        string text5 = string.Join(text3, array.Where<string>((string x) => x != null));
        int num4 = SettingsContent.Int(jsonObject["earned_usd_cents"]);
        list.Add(SettingsContent.FriendRow(text, SettingsContent.Str(jsonObject["avatar_url"]), text2, text5, (num4 > 0) ? ("+" + SettingsContent.Dollars(num4)) : null, null));
      }
      string text6;
      if (CS$<>8__locals1.friends.Count <= 0)
      {
        text6 = "People you've invited";
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 1);
        defaultInterpolatedStringHandler.AppendLiteral("People you've invited · ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num);
        text6 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      int num5 = Math.Max(0, CS$<>8__locals1.friends.Count - this.referralVisibleFriends);
      string text7;
      if (num5 != 0 || CS$<>8__locals1.friends.Count <= 0 || CS$<>8__locals1.friends.Count >= num)
      {
        text7 = null;
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 2);
        defaultInterpolatedStringHandler.AppendLiteral("Showing your latest ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.friends.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" of ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num);
        defaultInterpolatedStringHandler.AppendLiteral(".");
        text7 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      StackPanel stackPanel = SK.Group(text6, text7, list, 56.0, null);
      FrameworkElement frameworkElement = null;
      if (num5 > 0)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
        defaultInterpolatedStringHandler.AppendLiteral("Show ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(Math.Min(10, num5));
        defaultInterpolatedStringHandler.AppendLiteral(" more");
        Pressable pressable = SK.PillButton(defaultInterpolatedStringHandler.ToStringAndClear(), delegate
        {
          CS$<>8__locals1.<>4__this.referralVisibleFriends = CS$<>8__locals1.<>4__this.referralVisibleFriends + 10;
          CS$<>8__locals1.<>4__this.Render();
        }, false, false, false);
        frameworkElement = Ui.H(8.0, new UIElement[] { pressable });
        if (num5 > 10)
        {
          UIElementCollection children = ((StackPanel)frameworkElement).Children;
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
          defaultInterpolatedStringHandler.AppendLiteral("Show all ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.friends.Count);
          children.Add(SK.PillButton(defaultInterpolatedStringHandler.ToStringAndClear(), delegate
          {
            CS$<>8__locals1.<>4__this.referralVisibleFriends = CS$<>8__locals1.friends.Count;
            CS$<>8__locals1.<>4__this.Render();
          }, false, false, false));
        }
      }
      else if (this.referralVisibleFriends > 10 && CS$<>8__locals1.friends.Count > 10)
      {
        frameworkElement = Ui.H(8.0, new UIElement[] { SK.PillButton("Show less", delegate
        {
          CS$<>8__locals1.<>4__this.referralVisibleFriends = 10;
          CS$<>8__locals1.<>4__this.Render();
        }, false, false, false) });
      }
      if (frameworkElement != null)
      {
        Border border = stackPanel.Children.OfType<Border>().FirstOrDefault<Border>();
        StackPanel stackPanel2 = ((border != null) ? border.Child : null) as StackPanel;
        if (stackPanel2 != null)
        {
          frameworkElement.Margin = new Thickness(14.0, 10.0, 14.0, 10.0);
          stackPanel2.Children.Add(SK.Divider(14.0));
          stackPanel2.Children.Add(frameworkElement);
        }
      }
      return stackPanel;
    }

    // Token: 0x06000989 RID: 2441 RVA: 0x0054E3AC File Offset: 0x0054E3AC
    [return: Nullable(2)]
    private static string SampleFriendPhoto(string asset)
    {
      string text = Path.Combine(AppContext.BaseDirectory, "Assets", "Referral", asset + ".png");
      if (!File.Exists(text))
      {
        return null;
      }
      return new Uri(text).AbsoluteUri;
    }

    // Token: 0x0600098A RID: 2442 RVA: 0x0054E3F0 File Offset: 0x0054E3F0
    private static string PlanLabel(string plan)
    {
      string text;
      if (!(plan == "max"))
      {
        if (!(plan == "pro"))
        {
          if (!(plan == "canceled"))
          {
            text = "Free";
          }
          else
          {
            text = "Canceled";
          }
        }
        else
        {
          text = "Pro";
        }
      }
      else
      {
        text = "Max";
      }
      return text;
    }

    // Token: 0x0600098B RID: 2443 RVA: 0x0054E448 File Offset: 0x0054E448
    private static FrameworkElement FriendRow(string name, [Nullable(2)] string avatarUrl, string plan, string detail, [Nullable(2)] string earnedText, [Nullable(2)] string accessibleName)
    {
      Grid grid = HomeParts.AccountAvatar(30.0, name.Substring(0, 1).ToUpperInvariant(), avatarUrl);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 2.5,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          Ui.Text(name, 13.0, FontWeights.Medium, SK.TextPrimary, TextAlignment.Left, 1, false, false),
          Ui.Text(detail, 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 1, false, false)
        }
      };
      StackPanel stackPanel2 = Ui.H(12.0, Array.Empty<UIElement>());
      stackPanel2.VerticalAlignment = VerticalAlignment.Center;
      if (earnedText != null)
      {
        StackPanel stackPanel3 = Ui.H(4.0, new UIElement[]
        {
          Ui.Text("You earned", 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 0, false, false),
          Ui.Text(earnedText, 13.0, FontWeights.Medium, SK.TextPrimary, TextAlignment.Left, 0, false, false)
        });
        stackPanel3.VerticalAlignment = VerticalAlignment.Center;
        stackPanel2.Children.Add(stackPanel3);
      }
      stackPanel2.Children.Add(SettingsContent.PlanChip(plan));
      Grid grid2 = new Grid
      {
        Padding = new Thickness(14.0, 9.0, 14.0, 9.0),
        ColumnSpacing = 12.0
      };
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition());
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetColumn(stackPanel, 1);
      Grid.SetColumn(stackPanel2, 2);
      grid2.Children.Add(grid);
      grid2.Children.Add(stackPanel);
      grid2.Children.Add(stackPanel2);
      if (accessibleName != null)
      {
        AutomationProperties.SetName(grid2, accessibleName);
      }
      return grid2;
    }

    // Token: 0x0600098C RID: 2444 RVA: 0x0054E648 File Offset: 0x0054E648
    private static FrameworkElement PlanChip(string plan)
    {
      GelAppearance gelAppearance;
      if (!(plan == "pro"))
      {
        if (!(plan == "max"))
        {
          gelAppearance = GelAppearance.SecondaryOutline;
        }
        else
        {
          gelAppearance = GelAppearance.Peach;
        }
      }
      else
      {
        gelAppearance = GelAppearance.PrimaryBlue;
      }
      GelAppearance gelAppearance2 = gelAppearance;
      bool flag = gelAppearance2 == GelAppearance.SecondaryOutline;
      TextBlock textBlock = Ui.Text(SettingsContent.PlanLabel(plan), 10.5, FontWeights.Bold, flag ? Colors.White.WithAlpha((plan == "canceled") ? 0.55 : 0.82) : Colors.Black.WithAlpha(0.85), TextAlignment.Center, 0, false, false);
      textBlock.MinWidth = 34.0;
      Grid grid = new Grid();
      grid.VerticalAlignment = VerticalAlignment.Center;
      grid.Children.Add(new GelSurface(gelAppearance2, 100.0));
      FrameworkElement frameworkElement = (flag ? textBlock : Ui.Embossed(textBlock, 0.7, 1.0, 1.0));
      frameworkElement.Margin = new Thickness(9.0, 4.0, 9.0, 4.0);
      grid.Children.Add(frameworkElement);
      return grid;
    }

    // Token: 0x0600098D RID: 2445 RVA: 0x0054E780 File Offset: 0x0054E780
    private FrameworkElement PayoutGroup(JsonObject stats, JsonObject terms)
    {
      int num = SettingsContent.Int(stats["available_usd_cents"]);
      int num2 = SettingsContent.Int(terms["minimum_payout_usd_cents"]);
      bool canRequest = num >= num2 && num2 > 0;
      string email = SettingsContent.Str(terms["payout_contact_email"]) ?? "hi@heyclicky.com";
      string code = SettingsContent.Str(this.referral["code"]) ?? "";
      string text;
      if (num >= 0)
      {
        text = "Real money, not credits. We pay out monthly once you've earned " + SettingsContent.Dollars(num2) + "; smaller balances roll over.";
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(81, 2);
        defaultInterpolatedStringHandler.AppendLiteral("Your next earnings cover the ");
        defaultInterpolatedStringHandler.AppendFormatted(SettingsContent.Dollars(-num));
        defaultInterpolatedStringHandler.AppendLiteral(" first, then we pay out monthly once you've earned ");
        defaultInterpolatedStringHandler.AppendFormatted(SettingsContent.Dollars(num2));
        defaultInterpolatedStringHandler.AppendLiteral(".");
        text = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      string text2 = text;
      Pressable pressable = SK.PillButton(canRequest ? "Request payout" : "Email us", delegate
      {
        SettingsContent <>4__this = this;
        bool canRequest2 = canRequest;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(22, 3);
        defaultInterpolatedStringHandler2.AppendLiteral("mailto:");
        defaultInterpolatedStringHandler2.AppendFormatted(email);
        defaultInterpolatedStringHandler2.AppendLiteral("?subject=");
        defaultInterpolatedStringHandler2.AppendFormatted(Uri.EscapeDataString("Referral payout (" + code + ")"));
        defaultInterpolatedStringHandler2.AppendLiteral("&body=");
        defaultInterpolatedStringHandler2.AppendFormatted(Uri.EscapeDataString("Hi! I'd like a payout for my referral code " + code + ". The best way to pay me is: "));
        <>4__this.OpenPayoutMail(canRequest2, defaultInterpolatedStringHandler2.ToStringAndClear());
      }, canRequest, false, false);
      List<FrameworkElement> list = new List<FrameworkElement> { SK.Row("Get paid", text2, pressable, null) };
      string termsUrl = SettingsContent.Str(terms["terms_url"]);
      if (termsUrl != null)
      {
        list.Add(SK.ActionRow("Referral terms", "What counts, what doesn't, and how payouts work.", delegate
        {
          this.host.OpenUrl(termsUrl);
        }, null, "arrow.up.right", false));
      }
      return SK.Group("Getting paid", null, list, 14.0, null);
    }

    // Token: 0x0600098E RID: 2446 RVA: 0x0054E930 File Offset: 0x0054E930
    private void OpenPayoutMail(bool isAboveMinimum, string mailto)
    {
      ClickyAnalytics.TrackReferralPayoutRequested("getting_paid_row", isAboveMinimum);
      this.host.OpenUrl(mailto);
    }

    // Token: 0x0600098F RID: 2447 RVA: 0x0054E94C File Offset: 0x0054E94C
    private FrameworkElement FriendCodeGroup()
    {
      ValueTuple<Grid, TextBox> valueTuple = SK.SearchField("Friend's code", this.friendCodeDraft, delegate(string t)
      {
        this.friendCodeDraft = t;
      }, false, 12.5, null);
      Grid item = valueTuple.Item1;
      UIElement item2 = valueTuple.Item2;
      GelButton gelButton = new GelButton("Apply", GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 100.0, null, null, null);
      gelButton.Click += delegate
      {
        this.ClaimFriendCodeAsync();
      };
      item2.KeyDown += delegate(object _, KeyRoutedEventArgs e)
      {
        if (e.Key == VirtualKey.Enter)
        {
          e.Handled = true;
          _ = this.ClaimFriendCodeAsync();
        }
      };
      Grid grid = new Grid
      {
        ColumnSpacing = 8.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetColumn(gelButton, 1);
      grid.Children.Add(item);
      grid.Children.Add(gelButton);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 10.0,
        Padding = new Thickness(14.0),
        Children = 
        {
          Ui.Text("Enter their code to get your discount on Pro or Max.", 11.5, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 0, true, false),
          grid
        }
      };
      if (this.friendCodeMessage.Length > 0)
      {
        stackPanel.Children.Add(Ui.Text(this.friendCodeMessage, 11.0, FontWeights.SemiBold, SK.TextSecondary, TextAlignment.Left, 0, true, false));
      }
      return SK.Group("Were you invited by a friend?", new FrameworkElement[] { stackPanel });
    }

    // Token: 0x06000990 RID: 2448 RVA: 0x0054EAF4 File Offset: 0x0054EAF4
    private async Task ClaimFriendCodeAsync()
    {
      WorkerClient worker = this.host.Worker;
      if (worker != null && this.friendCodeDraft.Trim().Length != 0)
      {
        try
        {
          SettingsContent.<>c__DisplayClass199_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass199_0();
          CS$<>8__locals1.<>4__this = this;
          ReferralOutcomeResponse referralOutcomeResponse = await worker.ClaimReferralAsync(this.friendCodeDraft.Trim(), "code", default(CancellationToken));
          CS$<>8__locals1.result = referralOutcomeResponse;
          string text = "code";
          ReferralOutcomeResponse result = CS$<>8__locals1.result;
          ClickyAnalytics.TrackReferralFriendCodeClaimResult(text, ((result != null) ? result.Outcome : null) ?? "request_failed");
          this.dq.TryEnqueue(delegate
          {
            SettingsContent <>4__this = CS$<>8__locals1.<>4__this;
            ReferralOutcomeResponse result2 = CS$<>8__locals1.result;
            string text2;
            if (!(((result2 != null) ? result2.Outcome : null) == "claimed"))
            {
              ReferralOutcomeResponse result3 = CS$<>8__locals1.result;
              text2 = ((result3 != null) ? result3.Message : null) ?? "That code didn't work.";
            }
            else
            {
              text2 = "Done! Your discount is waiting at checkout.";
            }
            <>4__this.friendCodeMessage = text2;
            string text3 = "settings";
            string text4 = "referral claim: ";
            ReferralOutcomeResponse result4 = CS$<>8__locals1.result;
            Log.Info(text3, text4 + ((result4 != null) ? result4.Outcome : null));
            CS$<>8__locals1.<>4__this.RefreshReferralAsync();
            CS$<>8__locals1.<>4__this.Render();
          });
          CS$<>8__locals1 = null;
        }
        catch (Exception ex)
        {
          Log.Warn("settings", "referral claim failed: " + ex.Message);
          ClickyAnalytics.TrackReferralFriendCodeClaimResult("code", "request_failed");
          this.dq.TryEnqueue(delegate
          {
            this.friendCodeMessage = "Couldn't check that code. Try again in a moment.";
            this.Render();
          });
        }
      }
    }

    // Token: 0x06000991 RID: 2449 RVA: 0x0054EB38 File Offset: 0x0054EB38
    private static FrameworkElement ReferralLoadingSkeleton()
    {
      Grid grid = new Grid
      {
        ColumnSpacing = 10.0,
        Margin = new Thickness(0.0, 12.0, 0.0, 0.0)
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.Children.Add(new Border
      {
        Height = 40.0,
        CornerRadius = new CornerRadius(20.0),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.08))
      });
      Border border = new Border
      {
        Width = 130.0,
        Height = 40.0,
        CornerRadius = new CornerRadius(20.0),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.08))
      };
      Grid.SetColumn(border, 1);
      grid.Children.Add(border);
      Grid grid2 = new Grid
      {
        Padding = new Thickness(22.0)
      };
      grid2.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid2.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      grid2.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid2.Children.Add(new StackPanel
      {
        Spacing = 12.0,
        Children = 
        {
          SettingsContent.SkeletonBar(150.0, 10.0),
          SettingsContent.SkeletonBar(260.0, 22.0),
          SettingsContent.SkeletonBar(220.0, 9.0)
        }
      });
      Grid.SetRow(grid, 2);
      grid2.Children.Add(grid);
      Grid grid3 = new Grid
      {
        ColumnSpacing = 10.0
      };
      for (int i = 0; i < 3; i++)
      {
        grid3.ColumnDefinitions.Add(new ColumnDefinition());
        FrameworkElement frameworkElement = SettingsContent.SkeletonBlock(104.0, 14.0, new StackPanel
        {
          Spacing = 8.0,
          Padding = new Thickness(14.0),
          Children = 
          {
            SettingsContent.SkeletonBar(84.0, 8.0),
            SettingsContent.SkeletonBar(70.0, 18.0),
            SettingsContent.SkeletonBar(110.0, 7.0)
          }
        });
        Grid.SetColumn(frameworkElement, i);
        grid3.Children.Add(frameworkElement);
      }
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 16.0,
        Padding = new Thickness(16.0)
      };
      foreach (double num in SettingsContent.ReferralSkeletonNameWidths)
      {
        Grid grid4 = new Grid
        {
          ColumnSpacing = 12.0
        };
        grid4.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        grid4.ColumnDefinitions.Add(new ColumnDefinition());
        grid4.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        grid4.Children.Add(new Ellipse
        {
          Width = 30.0,
          Height = 30.0,
          Fill = Ui.Brush(SK.Ink.WithAlpha(0.09))
        });
        StackPanel stackPanel2 = new StackPanel
        {
          Spacing = 6.0,
          VerticalAlignment = VerticalAlignment.Center,
          Children = 
          {
            SettingsContent.SkeletonBar(num, 9.0),
            SettingsContent.SkeletonBar(70.0, 7.0)
          }
        };
        Grid.SetColumn(stackPanel2, 1);
        grid4.Children.Add(stackPanel2);
        Border border2 = new Border
        {
          Width = 48.0,
          Height = 20.0,
          CornerRadius = new CornerRadius(10.0),
          Background = Ui.Brush(SK.Ink.WithAlpha(0.08)),
          VerticalAlignment = VerticalAlignment.Center
        };
        Grid.SetColumn(border2, 2);
        grid4.Children.Add(border2);
        stackPanel.Children.Add(grid4);
      }
      StackPanel stackPanel3 = new StackPanel();
      stackPanel3.Spacing = 18.0;
      stackPanel3.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
      stackPanel3.IsHitTestVisible = false;
      stackPanel3.Tag = "referral-skeleton";
      stackPanel3.Children.Add(SettingsContent.SkeletonBlock(250.0, 22.0, grid2));
      stackPanel3.Children.Add(grid3);
      stackPanel3.Children.Add(SettingsContent.SkeletonBlock(170.0, 14.0, stackPanel));
      AutomationProperties.SetName(stackPanel3, "Loading Invite & Earn");
      return stackPanel3;
    }

    // Token: 0x06000992 RID: 2450 RVA: 0x0054F118 File Offset: 0x0054F118
    private static FrameworkElement SkeletonBar(double width, double height)
    {
      return new Border
      {
        Width = width,
        Height = height,
        CornerRadius = new CornerRadius(height / 2.0),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.09)),
        HorizontalAlignment = HorizontalAlignment.Left
      };
    }

    // Token: 0x06000993 RID: 2451 RVA: 0x0054F174 File Offset: 0x0054F174
    private static FrameworkElement SkeletonBlock(double height, double cornerRadius, FrameworkElement content)
    {
      Grid grid = new Grid
      {
        Height = height,
        CornerRadius = new CornerRadius(cornerRadius),
        Background = Ui.Brush(SK.Ink.WithAlpha(0.05)),
        BorderBrush = Ui.Brush(SK.Ink.WithAlpha(0.06)),
        BorderThickness = new Thickness(1.0)
      };
      grid.Children.Add(content);
      Border band = new Border
      {
        HorizontalAlignment = HorizontalAlignment.Left,
        IsHitTestVisible = false,
        Background = Ui.Horizontal(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, SK.Ink.WithAlpha(0.0)),
          new ValueTuple<double, Color>(0.5, SK.Ink.WithAlpha(0.06)),
          new ValueTuple<double, Color>(1.0, SK.Ink.WithAlpha(0.0))
        })
      };
      grid.Children.Add(band);
      grid.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        double width = e.NewSize.Width;
        if (width <= 0.0)
        {
          return;
        }
        band.Width = width * 0.7;
        band.Translation = new Vector3((float)(-(float)width), 0f, 0f);
        if (Anim.Instant)
        {
          return;
        }
        Anim.LoopVector3(band, "Translation", 1.4, (double t) => new Vector3((float)(width * (-1.0 + 2.3 * (t / 1.4))), 0f, 0f), 48, 0.0);
      };
      Ui.ClipRounded(grid, cornerRadius);
      return grid;
    }

    // Token: 0x06000994 RID: 2452 RVA: 0x0054F2CC File Offset: 0x0054F2CC
    private FrameworkElement RoutinesPage()
    {
      AgentService agents = this.host.Agents;
      if (agents != null && !this.routinesHooked)
      {
        this.routinesHooked = true;
        Microsoft.UI.Dispatching.DispatcherQueueHandler <>9__3;
        agents.RoutinesChanged += delegate([Nullable(2)] object _, EventArgs _)
        {
          Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue = this.dq;
          Microsoft.UI.Dispatching.DispatcherQueueHandler dispatcherQueueHandler;
          if ((dispatcherQueueHandler = <>9__3) == null)
          {
            dispatcherQueueHandler = (<>9__3 = delegate
            {
              if (this.page == SettingsWindow.Page.Routines && this.isShown)
              {
                this.RenderSoon();
              }
            });
          }
          dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
        };
      }
      List<ClickyRoutine> list = (((agents != null) ? agents.Routines.Routines : null) ?? Array.Empty<ClickyRoutine>()).OrderBy<ClickyRoutine, DateTimeOffset>((ClickyRoutine r) => r.CreatedAt).ToList<ClickyRoutine>();
      List<FrameworkElement> list2 = new List<FrameworkElement> { this.RoutineVoiceHint(list.Count > 0) };
      List<FrameworkElement> list3 = new List<FrameworkElement>();
      foreach (ClickyRoutine clickyRoutine in list)
      {
        SettingsContent.RoutineSettingsRow routineSettingsRow;
        if (!this.routineRows.TryGetValue(clickyRoutine.Id, out routineSettingsRow))
        {
          routineSettingsRow = (this.routineRows[clickyRoutine.Id] = new SettingsContent.RoutineSettingsRow(this, clickyRoutine.Id));
        }
        routineSettingsRow.Update(clickyRoutine);
        Panel panel = routineSettingsRow.Parent as Panel;
        if (panel != null)
        {
          panel.Children.Remove(routineSettingsRow);
        }
        list3.Add(routineSettingsRow);
      }
      HashSet<string> live = list.Select<ClickyRoutine, string>((ClickyRoutine r) => r.Id).ToHashSet<string>();
      IEnumerable<string> keys = this.routineRows.Keys;
      Func<string, bool> <>9__4;
      Func<string, bool> func;
      if ((func = <>9__4) == null)
      {
        func = (<>9__4 = (string k) => !live.Contains(k));
      }
      foreach (string text in keys.Where<string>(func).ToList<string>())
      {
        this.routineRows.Remove(text);
      }
      if (list3.Count > 0)
      {
        list2.Add(SK.Group(null, "Routines run on this PC while it's awake and HeyClicky is open.", list3, 14.0, null));
      }
      return SK.Scaffold("Routines", "Things HeyClicky does on repeat, on this PC.", list2, 20.0);
    }

    // Token: 0x06000995 RID: 2453 RVA: 0x0054F510 File Offset: 0x0054F510
    private FrameworkElement RoutineVoiceHint(bool hasRoutines)
    {
      TextBlock textBlock = Ui.Text("Hold", 12.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 0, false, false);
      StackPanel stackPanel = SK.KeyCaps(this.host.Current.EffectiveHotkeys.PushToTalk, true);
      TextBlock textBlock2 = Ui.Text(hasRoutines ? "and tell HeyClicky to add, change, pause or delete a routine." : "and tell HeyClicky what to do, and when.", 12.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 0, true, false);
      Grid grid = new Grid
      {
        ColumnSpacing = 6.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      Grid.SetColumn(stackPanel, 1);
      Grid.SetColumn(textBlock2, 2);
      foreach (FrameworkElement frameworkElement in new FrameworkElement[] { textBlock, stackPanel, textBlock2 })
      {
        frameworkElement.VerticalAlignment = VerticalAlignment.Center;
        grid.Children.Add(frameworkElement);
      }
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 6.0,
        Margin = new Thickness(4.0, 0.0, 4.0, 0.0),
        Children = { grid }
      };
      if (!hasRoutines)
      {
        stackPanel2.Children.Add(Ui.Text("“Every morning at 9, check my inbox for anything from investors and sum it up.”", 12.0, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 0, true, false));
      }
      return stackPanel2;
    }

    // Token: 0x1700017D RID: 381
    // (get) Token: 0x06000996 RID: 2454 RVA: 0x0054F6B8 File Offset: 0x0054F6B8
    private ShortcutRecorder Recorder
    {
      get
      {
        if (this.recorder != null)
        {
          return this.recorder;
        }
        this.recorder = new ShortcutRecorder(this.dq);
        this.recorder.Validate = delegate(ShortcutRecorder.Slot slot, HotkeyBinding binding)
        {
          HotkeyBindings hotkeyBindings = this.host.Read<HotkeyBindings>(([Nullable(1)] AppSettings x) => x.EffectiveHotkeys);
          ShortcutRecorder.Slot? slot2 = HotkeyRules.Conflict(hotkeyBindings, slot, binding);
          if (slot2 != null)
          {
            ShortcutRecorder.Slot valueOrDefault = slot2.GetValueOrDefault();
            return ShortcutCaptureRules.ConflictLine(binding, HotkeyRules.Title(valueOrDefault), HotkeyRules.Get(hotkeyBindings, valueOrDefault));
          }
          return null;
        };
        this.recorder.Changed += delegate
        {
          this.SetMonitorRecording(this.recorder.Mode != null);
          if (this.page == SettingsWindow.Page.Shortcuts)
          {
            this.Render();
          }
        };
        return this.recorder;
      }
    }

    // Token: 0x06000997 RID: 2455 RVA: 0x0054F71C File Offset: 0x0054F71C
    private void SetMonitorRecording(bool on)
    {
      ShortcutMonitor monitor = this.host.Monitor;
      if (monitor == null)
      {
        return;
      }
      if (on && !this.recordingHooked)
      {
        monitor.RecordingModifiersChanged += this.OnRecordingModifiers;
        monitor.RecordingKeyPressed += this.OnRecordingKey;
        this.recordingHooked = true;
      }
      else if (!on && this.recordingHooked)
      {
        monitor.RecordingModifiersChanged -= this.OnRecordingModifiers;
        monitor.RecordingKeyPressed -= this.OnRecordingKey;
        this.recordingHooked = false;
      }
      monitor.IsRecording = on;
    }

    // Token: 0x06000998 RID: 2456 RVA: 0x0054F7B0 File Offset: 0x0054F7B0
    private void OnRecordingModifiers(HotkeyModifiers held, uint _)
    {
      this.dq.TryEnqueue(delegate
      {
        ShortcutRecorder shortcutRecorder = this.recorder;
        if (shortcutRecorder == null)
        {
          return;
        }
        shortcutRecorder.IngestModifiers(held);
      });
    }

    // Token: 0x06000999 RID: 2457 RVA: 0x0054F7EC File Offset: 0x0054F7EC
    private void OnRecordingKey()
    {
      this.dq.TryEnqueue(delegate
      {
        ShortcutRecorder shortcutRecorder = this.recorder;
        if (shortcutRecorder == null)
        {
          return;
        }
        shortcutRecorder.IngestKey();
      });
    }

    // Token: 0x0600099A RID: 2458 RVA: 0x0054F808 File Offset: 0x0054F808
    private void CancelShortcutRecording()
    {
      ShortcutRecorder shortcutRecorder = this.recorder;
      if (shortcutRecorder != null)
      {
        shortcutRecorder.Cancel();
      }
      this.conflictRow = null;
      this.SetMonitorRecording(false);
    }

    // Token: 0x0600099B RID: 2459 RVA: 0x0054F830 File Offset: 0x0054F830
    private FrameworkElement ShortcutsPage()
    {
      HotkeyBindings hotkeyBindings = this.host.Read<HotkeyBindings>(([Nullable(1)] AppSettings x) => x.EffectiveHotkeys);
      return SK.Scaffold("Shortcuts", "The keys that summon HeyClicky.", new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
      {
        SK.Group("Talk to HeyClicky", null, new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
        {
          this.RecorderRow(hotkeyBindings, ShortcutRecorder.Slot.PushToTalk, "Push to talk", "Hold to talk to HeyClicky. Release to send."),
          this.RecorderRow(hotkeyBindings, ShortcutRecorder.Slot.TextMode, "Text mode", "Press to type to HeyClicky instead of talking.")
        }), 14.0, null),
        SK.Group("Dictation", null, new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
        {
          this.RecorderRow(hotkeyBindings, ShortcutRecorder.Slot.Dictation, "Dictate", "Hold and speak. Release to insert what you said at your cursor."),
          this.RecorderRow(hotkeyBindings, ShortcutRecorder.Slot.HandsFreeDictation, "Hands-free dictation", "Dictate without holding a key down. Press to start, press again to stop.")
        }), 14.0, null),
        SK.Group(null, null, new <>z__ReadOnlySingleElementList<FrameworkElement>(SK.ActionRow("Reset to defaults", null, delegate
        {
          this.CancelShortcutRecording();
          this.host.Set("shortcuts", "reset_defaults", delegate(AppSettings x)
          {
            x.Hotkeys = null;
          });
        }, null, null, false)), 14.0, null)
      }), 20.0);
    }

    // Token: 0x0600099C RID: 2460 RVA: 0x0054F958 File Offset: 0x0054F958
    private FrameworkElement RecorderRow(HotkeyBindings bindings, ShortcutRecorder.Slot slot, string title, string subtitle)
    {
      HotkeyBinding hotkeyBinding = HotkeyRules.Get(bindings, slot);
      ShortcutRecorder shortcutRecorder = this.recorder;
      bool flag;
      ShortcutRecorder.Slot? mode;
      ShortcutRecorder.Slot slot2;
      if (shortcutRecorder == null)
      {
        flag = false;
      }
      else
      {
        mode = shortcutRecorder.Mode;
        slot2 = slot;
        flag = (mode.GetValueOrDefault() == slot2) & (mode != null);
      }
      bool flag2 = flag;
      StackPanel stackPanel = Ui.H((double)(flag2 ? 8 : 10), Array.Empty<UIElement>());
      if (flag2)
      {
        HotkeyBinding captured = shortcutRecorder.Captured;
        if (captured != null)
        {
          stackPanel.Children.Add(SK.KeyCaps(captured, shortcutRecorder.IsLocked));
        }
        else
        {
          stackPanel.Children.Add(Ui.Text("Press your keys…", 11.5, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, false, false).Align(HorizontalAlignment.Left, VerticalAlignment.Center));
        }
        if (shortcutRecorder.Captured != null && shortcutRecorder.IsLocked)
        {
          GelButton gelButton = new GelButton("Save", GelAppearance.PrimaryBlue, GelButton.Sizing.Compact, 15.0, null, null, null);
          gelButton.Click += delegate
          {
            this.SaveRecorded(slot);
          };
          stackPanel.Children.Add(gelButton);
        }
        GelButton gelButton2 = new GelButton("Cancel", GelAppearance.SecondaryWhite, GelButton.Sizing.Compact, 15.0, null, null, null);
        gelButton2.Click += delegate
        {
          this.conflictRow = null;
          this.CancelShortcutRecording();
          this.Render();
        };
        stackPanel.Children.Add(gelButton2);
      }
      else
      {
        stackPanel.Children.Add(SK.KeyCaps(hotkeyBinding, true));
        GelButton gelButton3 = new GelButton("Change", GelAppearance.SecondaryWhite, GelButton.Sizing.Compact, 15.0, null, null, null);
        gelButton3.Click += delegate
        {
          this.conflictRow = null;
          this.Recorder.Begin(slot);
        };
        stackPanel.Children.Add(gelButton3);
      }
      foreach (FrameworkElement frameworkElement in stackPanel.Children.OfType<FrameworkElement>())
      {
        frameworkElement.VerticalAlignment = VerticalAlignment.Center;
      }
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 2.5,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          Ui.Text(title, 13.0, FontWeights.Medium, SK.TextPrimary, TextAlignment.Left, 0, true, false),
          Ui.Text(subtitle, 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 0, true, false)
        }
      };
      stackPanel.VerticalAlignment = VerticalAlignment.Center;
      Grid.SetColumn(stackPanel, 1);
      Grid grid = new Grid
      {
        ColumnSpacing = 24.0,
        Tag = "shortcut-row",
        Children = { stackPanel2, stackPanel }
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid grid2 = new Grid
      {
        Padding = new Thickness(14.0, 11.0, 14.0, 11.0),
        Children = { grid }
      };
      string text;
      if (flag2)
      {
        string hint = shortcutRecorder.Hint;
        if (hint != null)
        {
          text = hint;
          goto IL_03C8;
        }
      }
      mode = this.conflictRow;
      slot2 = slot;
      if ((mode.GetValueOrDefault() == slot2) & (mode != null))
      {
        mode = this.conflictWith;
        if (mode != null)
        {
          ShortcutRecorder.Slot valueOrDefault = mode.GetValueOrDefault();
          HotkeyBinding hotkeyBinding2 = ((shortcutRecorder != null) ? shortcutRecorder.Captured : null);
          if (hotkeyBinding2 != null)
          {
            text = ShortcutCaptureRules.ConflictLine(hotkeyBinding2, HotkeyRules.Title(valueOrDefault), HotkeyRules.Get(bindings, valueOrDefault));
            goto IL_03C8;
          }
        }
      }
      text = null;
      IL_03C8:
      string text2 = text;
      if (text2 == null)
      {
        return grid2;
      }
      TextBlock textBlock = Ui.Text(text2, 10.5, FontWeights.SemiBold, SK.Destructive, TextAlignment.Left, 0, true, false);
      textBlock.Margin = new Thickness(14.0, -5.0, 14.0, 11.0);
      return new StackPanel
      {
        Children = { grid2, textBlock }
      };
    }

    // Token: 0x0600099D RID: 2461 RVA: 0x0054FDB8 File Offset: 0x0054FDB8
    private void SaveRecorded(ShortcutRecorder.Slot slot)
    {
      ShortcutRecorder shortcutRecorder = this.recorder;
      HotkeyBinding hotkeyBinding = ((shortcutRecorder != null) ? shortcutRecorder.Captured : null);
      if (hotkeyBinding == null)
      {
        return;
      }
      HotkeyBindings hotkeyBindings = this.host.Read<HotkeyBindings>(([Nullable(1)] AppSettings x) => x.EffectiveHotkeys);
      ShortcutRecorder.Slot? slot2 = HotkeyRules.Conflict(hotkeyBindings, slot, hotkeyBinding);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (slot2 != null)
      {
        ShortcutRecorder.Slot valueOrDefault = slot2.GetValueOrDefault();
        this.conflictRow = new ShortcutRecorder.Slot?(slot);
        this.conflictWith = new ShortcutRecorder.Slot?(valueOrDefault);
        string text = "settings";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 3);
        defaultInterpolatedStringHandler.AppendLiteral("shortcut ");
        defaultInterpolatedStringHandler.AppendFormatted<ShortcutRecorder.Slot>(slot);
        defaultInterpolatedStringHandler.AppendLiteral(" = ");
        defaultInterpolatedStringHandler.AppendFormatted(hotkeyBinding.DisplayText);
        defaultInterpolatedStringHandler.AppendLiteral(" rejected: used by ");
        defaultInterpolatedStringHandler.AppendFormatted<ShortcutRecorder.Slot>(valueOrDefault);
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        this.Render();
        return;
      }
      this.conflictRow = null;
      this.CancelShortcutRecording();
      HotkeyBindings updated = HotkeyRules.With(hotkeyBindings, slot, hotkeyBinding);
      SettingsHost settingsHost = this.host;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
      defaultInterpolatedStringHandler.AppendLiteral("shortcut_");
      defaultInterpolatedStringHandler.AppendFormatted<ShortcutRecorder.Slot>(slot);
      settingsHost.Set(defaultInterpolatedStringHandler.ToStringAndClear(), hotkeyBinding.DisplayText, delegate(AppSettings x)
      {
        x.Hotkeys = updated;
      });
    }

    // Token: 0x0600099E RID: 2462 RVA: 0x0054FF08 File Offset: 0x0054FF08
    private FrameworkElement UpdatesGroup()
    {
      SettingsContent.<>c__DisplayClass222_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass222_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.updates = this.s.Updates;
      CS$<>8__locals1.rowHost = new ContentControl
      {
        HorizontalContentAlignment = HorizontalAlignment.Stretch,
        IsTabStop = false
      };
      CS$<>8__locals1.<UpdatesGroup>g__Fill|0();
      CS$<>8__locals1.dq = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
      CS$<>8__locals1.rowHost.Loaded += delegate(object _, RoutedEventArgs _)
      {
        CS$<>8__locals1.updates.Changed -= base.<UpdatesGroup>g__OnChanged|1;
        CS$<>8__locals1.updates.Changed += base.<UpdatesGroup>g__OnChanged|1;
        base.<UpdatesGroup>g__Fill|0();
      };
      CS$<>8__locals1.rowHost.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        CS$<>8__locals1.updates.Changed -= base.<UpdatesGroup>g__OnChanged|1;
      };
      Pressable pressable = SK.ActionRow("What's new", "The changelog of everything shipping in HeyClicky.", delegate
      {
        ClickyAnalytics.TrackChangelogOpened("agent_space_settings");
        CS$<>8__locals1.<>4__this.host.OpenUrl(SettingsHost.Links.Changelog ?? AppHost.ChangelogUrl);
      }, null, "arrow.up.right", false);
      return SK.Group("Updates", new FrameworkElement[] { CS$<>8__locals1.rowHost, pressable });
    }

    // Token: 0x0600099F RID: 2463 RVA: 0x0054FFCC File Offset: 0x0054FFCC
    [NullableContext(2)]
    private static VoiceLanguage LanguageForPreviews(string stored)
    {
      return VoiceCatalog.LanguageForPreviews(stored);
    }

    // Token: 0x060009A0 RID: 2464 RVA: 0x0054FFD4 File Offset: 0x0054FFD4
    private FrameworkElement VoicePage()
    {
      this.HookAudioDeviceChanges();
      AppSettings appSettings = this.host.Current;
      StackPanel stackPanel = Ui.H(6.0, Array.Empty<UIElement>());
      ValueTuple<double, string>[] speeds = SettingsContent.Speeds;
      for (int j = 0; j < speeds.Length; j++)
      {
        ValueTuple<double, string> valueTuple = speeds[j];
        double multiplier = valueTuple.Item1;
        string item = valueTuple.Item2;
        Action<AppSettings> <>9__4;
        stackPanel.Children.Add(SK.Chip(item, Math.Abs(appSettings.SpeechSpeed - multiplier) < 0.001, true, delegate
        {
          try
          {
            if (this.voicePreview != null)
            {
              this.voicePreview.PlaybackSession.PlaybackRate = multiplier;
            }
          }
          catch
          {
          }
          SettingsHost settingsHost = this.host;
          string text5 = "voice_speed";
          object obj = multiplier;
          Action<AppSettings> action;
          if ((action = <>9__4) == null)
          {
            action = (<>9__4 = delegate(AppSettings x)
            {
              x.SpeechSpeed = multiplier;
            });
          }
          settingsHost.Set(text5, obj, action);
        }));
      }
      stackPanel.Margin = new Thickness(14.0);
      this.previewSpinners.Clear();
      IEnumerable<ValueTuple<string, string, string, string, string, Color>> enumerable = SettingsContent.Voices.Select<ValueTuple<string, string, string, string, string, Color>, ValueTuple<string, string, string, string, string, Color>>(([TupleElementNames(new string[] { "Id", "Name", "Subtitle", "Fill", "Stroke", "Color" })] ValueTuple<string, string, string, string, string, Color> v) => new ValueTuple<string, string, string, string, string, Color>(v.Item1, v.Item2, v.Item3, v.Item4, v.Item5, v.Item6));
      IEnumerable<ValueTuple<string, string, string, string, string, Color>> enumerable2 = VoiceCatalog.ExpressiveVoices.Select<ExpressiveVoice, ValueTuple<string, string, string, string, string, Color>>((ExpressiveVoice v, int i) => new ValueTuple<string, string, string, string, string, Color>(v.Id, v.Name, v.Subtitle, SettingsContent.ExpressiveGlyphs[i].Item1, SettingsContent.ExpressiveGlyphs[i].Item2, SettingsContent.ExpressiveGlyphs[i].Item3));
      FrameworkElement frameworkElement = this.VoiceSection("More voices", "Good at English", enumerable, appSettings.VoiceIdentifier);
      FrameworkElement frameworkElement2 = this.VoiceSection("Voices", "Expressive, with a native accent in most languages", enumerable2, appSettings.VoiceIdentifier);
      this.StartPreviewSpinnerTimer();
      this.PrefetchExpressivePreviews();
      string text = "Voice";
      string text2 = "How HeyClicky speaks. Changes apply from the next spoken reply.";
      FrameworkElement[] array = new FrameworkElement[6];
      array[0] = SK.Group("Captions", null, new <>z__ReadOnlySingleElementList<FrameworkElement>(SK.ToggleRow("Captions while HeyClicky talks", "Show what HeyClicky says at the bottom of your screen, word by word.", appSettings.DrawingVoiceCaptions, delegate(bool v)
      {
        this.Quiet("screen_drawing_voice_captions", v, delegate(AppSettings x)
        {
          x.DrawingVoiceCaptions = v;
        });
      })), 14.0, null);
      int num = 1;
      string text3 = "Language";
      string text4 = null;
      IEnumerable<FrameworkElement> enumerable3 = new <>z__ReadOnlySingleElementList<FrameworkElement>(this.LanguagePicker(appSettings.VoiceLanguageCode));
      double num2 = 14.0;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
      defaultInterpolatedStringHandler.AppendFormatted<int>(VoiceCatalog.Languages.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" supported");
      array[num] = SK.Group(text3, text4, enumerable3, num2, defaultInterpolatedStringHandler.ToStringAndClear());
      array[2] = SK.Group("Speech speed", null, new <>z__ReadOnlySingleElementList<FrameworkElement>(stackPanel), 14.0, null);
      array[3] = frameworkElement2;
      array[4] = frameworkElement;
      array[5] = this.OutputDeviceGroup(appSettings.OutputDeviceId);
      return SK.Scaffold(text, text2, new <>z__ReadOnlyArray<FrameworkElement>(array), 32.0);
    }

    // Token: 0x060009A1 RID: 2465 RVA: 0x00550230 File Offset: 0x00550230
    private FrameworkElement VoiceSection(string label, string caption, [TupleElementNames(new string[] { "Id", "Name", "Subtitle", "Fill", "Stroke", "Color" })] [Nullable(new byte[] { 1, 0, 1, 1, 1, 2, 2 })] IEnumerable<ValueTuple<string, string, string, string, string, Color>> voices, string selectedId)
    {
      List<ValueTuple<string, string, string, string, string, Color>> list = voices.ToList<ValueTuple<string, string, string, string, string, Color>>();
      Grid grid = new Grid
      {
        ColumnSpacing = 12.0,
        RowSpacing = 12.0,
        MaxWidth = 650.0,
        HorizontalAlignment = HorizontalAlignment.Stretch
      };
      for (int i = 0; i < 5; i++)
      {
        grid.ColumnDefinitions.Add(new ColumnDefinition());
      }
      for (int j = 0; j < (list.Count + 4) / 5; j++)
      {
        grid.RowDefinitions.Add(new RowDefinition());
      }
      for (int k = 0; k < list.Count; k++)
      {
        ValueTuple<string, string, string, string, string, Color> valueTuple = list[k];
        bool flag = string.Equals(selectedId, valueTuple.Item1, StringComparison.OrdinalIgnoreCase);
        Grid grid2 = new Grid
        {
          Height = 28.0,
          Width = 28.0,
          HorizontalAlignment = HorizontalAlignment.Center
        };
        grid2.Children.Add(Ui.GlowSpot(valueTuple.Item6, 0.22, 46.0, 0.25));
        grid2.Children.Add(Icons.Vector(26.0, valueTuple.Item6, valueTuple.Item4, valueTuple.Item5, 1.9));
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 8.0,
          VerticalAlignment = VerticalAlignment.Center,
          HorizontalAlignment = HorizontalAlignment.Center,
          Padding = new Thickness(10.0),
          Children = 
          {
            grid2,
            new StackPanel
            {
              Spacing = 3.0,
              Children = 
              {
                Ui.Text(valueTuple.Item2, 12.5, FontWeights.SemiBold, SK.TextPrimary, TextAlignment.Center, 1, false, false),
                Ui.Text(valueTuple.Item3, 10.5, FontWeights.Medium, SK.TextTertiary, TextAlignment.Center, 2, false, false)
              }
            }
          }
        };
        string id = valueTuple.Item1;
        Grid grid3 = new Grid
        {
          Children = { stackPanel }
        };
        if (VoiceCatalog.IsGemini(id))
        {
          ProgressRing progressRing = new ProgressRing
          {
            Width = 12.0,
            Height = 12.0,
            MinWidth = 0.0,
            MinHeight = 0.0,
            IsActive = true,
            Foreground = Ui.Brush(SK.TextTertiary),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(9.0, 9.0, 0.0, 0.0),
            IsHitTestVisible = false,
            Visibility = Visibility.Collapsed
          };
          grid3.Children.Add(progressRing);
          this.previewSpinners[id] = progressRing;
        }
        Action<AppSettings> <>9__2;
        Pressable tile = SK.Tile(grid3, flag, 20.0, delegate
        {
          SettingsHost settingsHost = this.host;
          string text = "voice";
          object id2 = id;
          Action<AppSettings> action;
          if ((action = <>9__2) == null)
          {
            action = (<>9__2 = delegate(AppSettings x)
            {
              x.VoiceIdentifier = id;
            });
          }
          settingsHost.Set(text, id2, action);
          this.PlayVoicePreview(id);
        }, 20.0, true);
        tile.SizeChanged += delegate(object _, SizeChangedEventArgs e)
        {
          if (double.IsNaN(tile.Height) || Math.Abs(tile.Height - e.NewSize.Width) > 0.5)
          {
            tile.Height = e.NewSize.Width;
          }
        };
        ToolTipService.SetToolTip(tile, valueTuple.Item2 + ", " + valueTuple.Item3);
        Grid.SetColumn(tile, k % 5);
        Grid.SetRow(tile, k / 5);
        grid.Children.Add(tile);
      }
      StackPanel stackPanel2 = Ui.H(8.0, new UIElement[]
      {
        SK.MicroLabel(label.ToUpperInvariant(), 10.0, FontWeights.SemiBold, 110),
        Ui.Text(caption, 11.0, FontWeights.Normal, SK.TextSecondary, TextAlignment.Left, 0, false, false)
      });
      stackPanel2.Margin = new Thickness(4.0, 0.0, 4.0, 0.0);
      return new StackPanel
      {
        Spacing = 10.0,
        Children = { stackPanel2, grid }
      };
    }

    // Token: 0x060009A2 RID: 2466 RVA: 0x005506A8 File Offset: 0x005506A8
    private FrameworkElement LanguagePicker([Nullable(2)] string stored)
    {
      IReadOnlyList<string> readOnlyList = VoiceCatalog.PickedLanguages(stored);
      string text = ((readOnlyList.Count == 0) ? "Automatic" : VoiceCatalog.SpokenList(readOnlyList));
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 2.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          Ui.Text("Speak to me in", 13.0, FontWeights.Medium, SK.TextPrimary, TextAlignment.Left, 1, false, false),
          Ui.Text((readOnlyList.Count == 0) ? "Automatic: the language you talk to it in" : ((readOnlyList.Count == 1) ? text : "Whichever of these you use"), 11.5, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 1, false, false)
        }
      };
      TextBlock textBlock = Icons.Glyph(this.languageListOpen ? "\ue70e" : "\ue70d", 10.0, SK.TextTertiary);
      StackPanel stackPanel2 = Ui.H(6.0, new UIElement[]
      {
        Ui.Text(text, 12.5, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 1, false, false),
        textBlock
      });
      stackPanel2.VerticalAlignment = VerticalAlignment.Center;
      Grid grid = new Grid
      {
        Padding = new Thickness(14.0),
        ColumnSpacing = 12.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetColumn(stackPanel2, 1);
      grid.Children.Add(stackPanel);
      grid.Children.Add(stackPanel2);
      Pressable toggle = new Pressable(true)
      {
        Children = { grid }
      };
      toggle.HoverChanged += delegate(bool h)
      {
        toggle.Background = Ui.Brush(this.look.Ink.WithAlpha(h ? 0.04 : 0.0));
      };
      toggle.Click += delegate
      {
        this.languageListOpen = !this.languageListOpen;
        this.voiceLanguageSearch = "";
        this.voiceLanguageLimitReached = false;
        if (!this.languageListOpen)
        {
          this.choosingVoiceLanguages = false;
        }
        this.Render();
      };
      StackPanel stackPanel3 = new StackPanel
      {
        Children = { toggle }
      };
      if (!this.languageListOpen)
      {
        return stackPanel3;
      }
      bool flag = readOnlyList.Count == 0 && !this.choosingVoiceLanguages;
      Grid grid2 = SK.ToggleRow("Automatic", "Answer in the language you talk to it in. Turn off to pick one or more.", flag, delegate(bool on)
      {
        this.voiceLanguageLimitReached = false;
        if (on)
        {
          this.choosingVoiceLanguages = false;
          this.SetVoiceLanguages(Array.Empty<string>());
        }
        else
        {
          this.choosingVoiceLanguages = true;
        }
        this.Render();
      });
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 1);
      defaultInterpolatedStringHandler.AppendLiteral("Search ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(VoiceCatalog.Languages.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" languages");
      Grid item = SK.SearchField(defaultInterpolatedStringHandler.ToStringAndClear(), this.voiceLanguageSearch, delegate(string t)
      {
        this.voiceLanguageSearch = t;
        this.FillLanguageList(stored);
      }, true, 12.0, null).Item1;
      this.languageListHost = new Grid();
      this.FillLanguageList(stored);
      ScrollViewer scrollViewer = new ScrollViewer
      {
        Height = 260.0,
        Content = this.languageListHost,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto
      };
      StackPanel stackPanel4 = new StackPanel
      {
        Spacing = 10.0,
        Padding = new Thickness(14.0),
        Opacity = (flag ? 0.35 : 1.0),
        IsHitTestVisible = !flag,
        Children = { item }
      };
      if (this.voiceLanguageLimitReached)
      {
        UIElementCollection children = stackPanel4.Children;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 1);
        defaultInterpolatedStringHandler.AppendLiteral("Up to ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(5);
        defaultInterpolatedStringHandler.AppendLiteral(" languages. Take one off to add another.");
        children.Add(Ui.Text(defaultInterpolatedStringHandler.ToStringAndClear(), 11.5, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, false, false));
      }
      stackPanel4.Children.Add(scrollViewer);
      stackPanel3.Children.Add(SK.Divider(0.0));
      stackPanel3.Children.Add(grid2);
      stackPanel3.Children.Add(SK.Divider(0.0));
      stackPanel3.Children.Add(stackPanel4);
      return stackPanel3;
    }

    // Token: 0x060009A3 RID: 2467 RVA: 0x00550AD4 File Offset: 0x00550AD4
    [NullableContext(2)]
    private void FillLanguageList(string stored)
    {
      using (MessagesPalette.Use(this.look))
      {
        if (this.languageListHost != null)
        {
          this.languageListHost.Children.Clear();
          this.languageListHost.RowDefinitions.Clear();
          this.languageListHost.ColumnDefinitions.Clear();
          IReadOnlyList<string> readOnlyList = VoiceCatalog.PickedLanguages(stored);
          IReadOnlyList<VoiceLanguage> readOnlyList2 = VoiceCatalog.PickerList(this.voiceLanguageSearch);
          if (readOnlyList2.Count == 0)
          {
            this.languageListHost.Children.Add(Ui.Text("No languages match \"" + this.voiceLanguageSearch + "\".", 11.5, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 0, false, false));
          }
          else
          {
            this.languageListHost.ColumnSpacing = 8.0;
            this.languageListHost.RowSpacing = 8.0;
            for (int i = 0; i < 4; i++)
            {
              this.languageListHost.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int j = 0; j < (readOnlyList2.Count + 3) / 4; j++)
            {
              this.languageListHost.RowDefinitions.Add(new RowDefinition
              {
                Height = GridLength.Auto
              });
            }
            for (int k = 0; k < readOnlyList2.Count; k++)
            {
              VoiceLanguage voiceLanguage = readOnlyList2[k];
              bool flag = readOnlyList.Contains(voiceLanguage.Code);
              StackPanel stackPanel = new StackPanel
              {
                Spacing = 3.0,
                VerticalAlignment = VerticalAlignment.Center,
                Children = { Ui.Text(voiceLanguage.Name, 11.5, flag ? FontWeights.SemiBold : FontWeights.Medium, flag ? SK.TextPrimary : SK.TextSecondary, TextAlignment.Left, 1, false, false) }
              };
              string nativeName = voiceLanguage.NativeName;
              if (nativeName != null)
              {
                stackPanel.Children.Add(Ui.Text(nativeName, 10.5, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 1, false, false));
              }
              Grid grid = new Grid
              {
                Padding = new Thickness(13.0, 12.0, 13.0, 12.0),
                MinHeight = 46.0,
                ColumnSpacing = 6.0
              };
              grid.ColumnDefinitions.Add(new ColumnDefinition());
              grid.ColumnDefinitions.Add(new ColumnDefinition
              {
                Width = new GridLength(18.0)
              });
              grid.Children.Add(stackPanel);
              if (flag)
              {
                FrameworkElement frameworkElement = IntroWindow.GelCheckmark(18.0);
                frameworkElement.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(frameworkElement, 1);
                grid.Children.Add(frameworkElement);
              }
              string code = voiceLanguage.Code;
              Pressable pressable = SK.Tile(grid, flag, 9.0, delegate
              {
                this.ToggleVoiceLanguage(code);
              }, 20.0, false);
              ToolTipService.SetToolTip(pressable, voiceLanguage.Name);
              Grid.SetColumn(pressable, k % 4);
              Grid.SetRow(pressable, k / 4);
              this.languageListHost.Children.Add(pressable);
            }
          }
        }
      }
    }

    // Token: 0x060009A4 RID: 2468 RVA: 0x00550E50 File Offset: 0x00550E50
    private void ToggleVoiceLanguage(string code)
    {
      List<string> list = VoiceCatalog.PickedLanguages(this.host.Read<string>(([Nullable(1)] AppSettings x) => x.VoiceLanguageCode)).ToList<string>();
      string text;
      if (list.Remove(code))
      {
        this.voiceLanguageLimitReached = false;
        text = "remove";
      }
      else
      {
        if (list.Count >= 5)
        {
          this.voiceLanguageLimitReached = true;
          this.Render();
          return;
        }
        list.Add(code);
        text = "add";
      }
      string text2 = "setting_changed";
      string text3 = null;
      string text4 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["setting"] = "voice_language";
      jsonObject["action"] = text;
      jsonObject["value"] = code;
      jsonObject["surface"] = "home_space_settings";
      ClickyAnalytics.TrackProductEvent(text2, text3, text4, jsonObject);
      this.choosingVoiceLanguages = list.Count == 0;
      this.SetVoiceLanguages(list);
    }

    // Token: 0x060009A5 RID: 2469 RVA: 0x00550F4C File Offset: 0x00550F4C
    private void SetVoiceLanguages(IReadOnlyList<string> picks)
    {
      string stored = VoiceCatalog.StoredLanguages(picks);
      string text = this.host.Read<string>(([Nullable(1)] AppSettings x) => x.VoiceLanguageCode);
      if (stored == text)
      {
        this.Render();
        return;
      }
      VoiceLanguage voiceLanguage = SettingsContent.LanguageForPreviews(stored);
      string text2 = ((voiceLanguage != null) ? voiceLanguage.Code : null);
      VoiceLanguage voiceLanguage2 = SettingsContent.LanguageForPreviews(text);
      bool flag = text2 != ((voiceLanguage2 != null) ? voiceLanguage2.Code : null);
      if (flag)
      {
        this.StopVoicePreview();
        this.voicePreviewRequest++;
      }
      Log.Info("settings", "voice_language = " + (stored ?? "automatic"));
      this.host.Store.Update(delegate(AppSettings x)
      {
        x.VoiceLanguageCode = stored;
      });
      if (!flag)
      {
        return;
      }
      string text3 = this.host.Read<string>(([Nullable(1)] AppSettings x) => x.VoiceIdentifier);
      if (SettingsContent.LanguageForPreviews(stored) != null && VoiceCatalog.IsGemini(text3))
      {
        this.PlayVoicePreview(text3);
      }
      this.PrefetchExpressivePreviews();
    }

    // Token: 0x060009A6 RID: 2470 RVA: 0x00551080 File Offset: 0x00551080
    private void PrefetchExpressivePreviews()
    {
      VoicePreviewStore shared = VoicePreviewStore.Shared;
      if (shared == null)
      {
        return;
      }
      AppSettings c = this.host.Current;
      VoiceLanguage voiceLanguage = SettingsContent.LanguageForPreviews(c.VoiceLanguageCode);
      if (voiceLanguage == null)
      {
        return;
      }
      List<string> list = (from v in VoiceCatalog.ExpressiveVoices
        select v.Id into id
        orderby (!(id == c.VoiceIdentifier)) ? 1 : 0
        select id).ToList<string>();
      shared.PrefetchAsync(list, voiceLanguage.Code);
    }

    // Token: 0x060009A7 RID: 2471 RVA: 0x00551114 File Offset: 0x00551114
    private void StartPreviewSpinnerTimer()
    {
      if (this.previewSpinnerTimer == null)
      {
        this.previewSpinnerTimer = new DispatcherTimer
        {
          Interval = TimeSpan.FromMilliseconds(400L, 0L)
        };
        this.previewSpinnerTimer.Tick += delegate([Nullable(2)] object _, object _)
        {
          this.RefreshPreviewSpinners();
        };
        base.Unloaded += delegate(object _, RoutedEventArgs _)
        {
          DispatcherTimer dispatcherTimer = this.previewSpinnerTimer;
          if (dispatcherTimer == null)
          {
            return;
          }
          dispatcherTimer.Stop();
        };
      }
      this.RefreshPreviewSpinners();
      this.previewSpinnerTimer.Start();
    }

    // Token: 0x060009A8 RID: 2472 RVA: 0x00551184 File Offset: 0x00551184
    private void RefreshPreviewSpinners()
    {
      using (MessagesPalette.Use(this.look))
      {
        if (!this.isShown || this.page != SettingsWindow.Page.Voice)
        {
          DispatcherTimer dispatcherTimer = this.previewSpinnerTimer;
          if (dispatcherTimer != null)
          {
            dispatcherTimer.Stop();
          }
        }
        else
        {
          VoiceLanguage voiceLanguage = SettingsContent.LanguageForPreviews(this.host.Read<string>(([Nullable(1)] AppSettings x) => x.VoiceLanguageCode));
          VoicePreviewStore shared = VoicePreviewStore.Shared;
          foreach (KeyValuePair<string, FrameworkElement> keyValuePair in this.previewSpinners)
          {
            string text;
            FrameworkElement frameworkElement;
            keyValuePair.Deconstruct(out text, out frameworkElement);
            string text2 = text;
            FrameworkElement frameworkElement2 = frameworkElement;
            Visibility visibility = ((voiceLanguage != null && shared != null && shared.IsMaking(text2, voiceLanguage.Code)) ? Visibility.Visible : Visibility.Collapsed);
            if (frameworkElement2.Visibility != visibility)
            {
              frameworkElement2.Visibility = visibility;
            }
          }
        }
      }
    }

    // Token: 0x060009A9 RID: 2473 RVA: 0x0055129C File Offset: 0x0055129C
    private FrameworkElement OutputDeviceGroup([Nullable(2)] string picked)
    {
      List<FrameworkElement> list = new List<FrameworkElement> { this.DeviceRow("System Default", SettingsContent.DefaultName(this.renderDevices), picked == null, true, delegate
      {
        this.PickOutput(null);
      }) };
      using (IEnumerator<AudioDeviceInfo> enumerator = this.renderDevices.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          AudioDeviceInfo d2 = enumerator.Current;
          list.Add(this.DeviceRow(d2.Name, null, string.Equals(d2.Id, picked, StringComparison.OrdinalIgnoreCase), true, delegate
          {
            this.PickOutput(d2.Id);
          }));
        }
      }
      if (picked != null && !this.renderDevices.Any<AudioDeviceInfo>((AudioDeviceInfo d) => string.Equals(d.Id, picked, StringComparison.OrdinalIgnoreCase)) && this.renderDevices.Count > 0)
      {
        list.Add(this.DeviceRow("Unavailable output", "Disconnected", true, false, delegate
        {
        }));
      }
      Pressable pressable = SK.PillButton("Play a test sound", delegate
      {
        Log.Info("settings", "output test sound");
        AppHost app = this.host.App;
        WasapiAudioOutput wasapiAudioOutput;
        if (app == null)
        {
          wasapiAudioOutput = null;
        }
        else
        {
          RealStack real = app.Real;
          wasapiAudioOutput = ((real != null) ? real.Audio : null);
        }
        WasapiAudioOutput wasapiAudioOutput2 = wasapiAudioOutput;
        if (wasapiAudioOutput2 != null)
        {
          wasapiAudioOutput2.PlayEarcon("textOpen", 0.8f);
          return;
        }
        IAudioOutput audio = this.s.Audio;
        if (audio == null)
        {
          return;
        }
        audio.PlayEarcon("textOpen");
      }, false, false, false);
      list.Add(SK.Row("Test", "Plays a chime through the picked output.", pressable, null));
      return SK.Group("Output device", "HeyClicky's voice and sounds play here. System Default follows Windows.", list, 14.0, null);
    }

    // Token: 0x060009AA RID: 2474 RVA: 0x00551444 File Offset: 0x00551444
    [return: Nullable(2)]
    private static string DefaultName(IReadOnlyList<AudioDeviceInfo> devices)
    {
      AudioDeviceInfo audioDeviceInfo = devices.FirstOrDefault<AudioDeviceInfo>((AudioDeviceInfo d) => d.IsDefault);
      if (audioDeviceInfo == null)
      {
        return null;
      }
      return audioDeviceInfo.Name;
    }

    // Token: 0x060009AB RID: 2475 RVA: 0x00551478 File Offset: 0x00551478
    [NullableContext(2)]
    private void PickOutput(string id)
    {
      this.host.Set("output_device", id ?? "system_default", delegate(AppSettings x)
      {
        x.OutputDeviceId = id;
      });
    }

    // Token: 0x060009AC RID: 2476 RVA: 0x005514C0 File Offset: 0x005514C0
    private void PlayVoicePreview(string id)
    {
      SettingsContent.<>c__DisplayClass250_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass250_0();
      CS$<>8__locals1.id = id;
      CS$<>8__locals1.<>4__this = this;
      this.StopVoicePreview();
      SettingsContent.<>c__DisplayClass250_0 CS$<>8__locals2 = CS$<>8__locals1;
      int num = this.voicePreviewRequest + 1;
      this.voicePreviewRequest = num;
      CS$<>8__locals2.request = num;
      if (Environment.GetEnvironmentVariable("HEYCLICKY_MUTE_AUDIO") == "1")
      {
        return;
      }
      CS$<>8__locals1.bundled = Path.Combine(AppContext.BaseDirectory, "Assets", "VoicePreviews", VoiceCatalog.IsGemini(CS$<>8__locals1.id) ? ("gemini-voice-preview-" + VoiceCatalog.GeminiName(CS$<>8__locals1.id).ToLowerInvariant() + ".mp3") : ("realtime-voice-preview-" + CS$<>8__locals1.id + ".mp3"));
      if (VoiceCatalog.IsGemini(CS$<>8__locals1.id))
      {
        CS$<>8__locals1.language = SettingsContent.LanguageForPreviews(this.host.Read<string>(([Nullable(1)] AppSettings x) => x.VoiceLanguageCode));
        if (CS$<>8__locals1.language != null)
        {
          CS$<>8__locals1.store = VoicePreviewStore.Shared;
          if (CS$<>8__locals1.store != null)
          {
            string text = CS$<>8__locals1.store.Cached(CS$<>8__locals1.id, CS$<>8__locals1.language.Code);
            if (text != null)
            {
              this.PlayPreviewClip(text, CS$<>8__locals1.id);
              return;
            }
            this.RefreshPreviewSpinners();
            CS$<>8__locals1.<PlayVoicePreview>g__MakeAndPlayAsync|1();
            return;
          }
        }
      }
      this.PlayPreviewClip(CS$<>8__locals1.bundled, CS$<>8__locals1.id);
    }

    // Token: 0x060009AD RID: 2477 RVA: 0x00551620 File Offset: 0x00551620
    private void PlayPreviewClip(string path, string id)
    {
      if (!File.Exists(path))
      {
        Log.Warn("settings", "voice preview missing: " + path);
        return;
      }
      this.voicePreview = new MediaPlayer
      {
        Source = MediaSource.CreateFromUri(new Uri(path)),
        Volume = 0.95,
        AudioCategory = MediaPlayerAudioCategory.Speech
      };
      double rate = this.host.Read<double>((AppSettings x) => x.SpeechSpeed);
      try
      {
        this.voicePreview.PlaybackSession.PlaybackRate = rate;
      }
      catch
      {
      }
      this.voicePreview.MediaOpened += delegate(MediaPlayer player, object _)
      {
        try
        {
          player.PlaybackSession.PlaybackRate = rate;
        }
        catch
        {
        }
      };
      this.voicePreview.Play();
      Log.Info("settings", "voice preview " + id);
    }

    // Token: 0x060009AE RID: 2478 RVA: 0x00551718 File Offset: 0x00551718
    private void StopVoicePreview()
    {
      if (this.voicePreview == null)
      {
        return;
      }
      try
      {
        this.voicePreview.Pause();
        this.voicePreview.Dispose();
      }
      catch
      {
      }
      this.voicePreview = null;
    }

    // Token: 0x060009AF RID: 2479 RVA: 0x00551764 File Offset: 0x00551764
    private FrameworkElement MicrophonePage()
    {
      this.HookAudioDeviceChanges();
      string picked = this.host.Read<string>(([Nullable(1)] AppSettings x) => x.MicrophoneDeviceId);
      List<FrameworkElement> list = new List<FrameworkElement> { this.DeviceRow("System Default", SettingsContent.DefaultName(this.captureDevices), picked == null, true, delegate
      {
        this.PickMicrophone(null);
      }) };
      using (IEnumerator<AudioDeviceInfo> enumerator = this.captureDevices.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          AudioDeviceInfo d2 = enumerator.Current;
          list.Add(this.DeviceRow(d2.Name, null, string.Equals(d2.Id, picked, StringComparison.OrdinalIgnoreCase), true, delegate
          {
            this.PickMicrophone(d2.Id);
          }));
        }
      }
      if (picked != null && this.captureDevices.Count > 0 && !this.captureDevices.Any<AudioDeviceInfo>((AudioDeviceInfo d) => string.Equals(d.Id, picked, StringComparison.OrdinalIgnoreCase)))
      {
        list.Add(this.DeviceRow("Unavailable Microphone", "Disconnected", true, false, delegate
        {
        }));
      }
      this.micTestButton = new GelButton((this.micTest == null) ? "Test microphone" : "Stop", GelAppearance.SecondaryWhite, GelButton.Sizing.Normal, 100.0, null, null, null);
      this.micTestButton.Click += delegate
      {
        if (this.micTest == null)
        {
          this.StartMicrophoneTest();
          return;
        }
        this.StopMicrophoneTest();
      };
      this.micBars = Ui.H(3.0, Array.Empty<UIElement>());
      for (int i = 0; i < 16; i++)
      {
        this.micBars.Children.Add(new Rectangle
        {
          Width = 6.0,
          Height = 16.0,
          RadiusX = 2.0,
          RadiusY = 2.0,
          Fill = Ui.Brush(SK.Ink.WithAlpha(0.12))
        });
      }
      this.micStatusText = Ui.Text(this.micTestStatus, 11.0, FontWeights.Medium, (this.micTestStatusColor == default(Color)) ? SK.TextTertiary : this.micTestStatusColor, TextAlignment.Left, 0, false, false);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 10.0,
        Padding = new Thickness(14.0),
        Children = 
        {
          Ui.H(14.0, new UIElement[] { this.micTestButton, this.micBars }),
          this.micStatusText
        }
      };
      this.UpdateMicBars();
      return SK.Scaffold("Microphone", "Which microphone HeyClicky listens through.", new <>z__ReadOnlyArray<FrameworkElement>(new FrameworkElement[]
      {
        SK.Group("Input device", null, list, 14.0, null),
        SK.Group("Test", "Speak normally — the bars should move as HeyClicky hears you.", new <>z__ReadOnlySingleElementList<FrameworkElement>(stackPanel), 14.0, null)
      }), 20.0);
    }

    // Token: 0x060009B0 RID: 2480 RVA: 0x00551AF4 File Offset: 0x00551AF4
    [NullableContext(2)]
    private void PickMicrophone(string id)
    {
      this.StopMicrophoneTest();
      this.host.Set("microphone", id ?? "system_default", delegate(AppSettings x)
      {
        x.MicrophoneDeviceId = id;
      });
    }

    // Token: 0x060009B1 RID: 2481 RVA: 0x00551B40 File Offset: 0x00551B40
    private FrameworkElement DeviceRow(string name, [Nullable(2)] string subtitle, bool selected, bool available, Action onPick)
    {
      Pressable p = new Pressable(true)
      {
        Opacity = (available ? 1.0 : 0.45)
      };
      FrameworkElement frameworkElement;
      if (!selected)
      {
        Ellipse ellipse = new Ellipse();
        ellipse.Width = 18.0;
        ellipse.Height = 18.0;
        ellipse.Stroke = Ui.Brush(SK.TextTertiary);
        frameworkElement = ellipse;
        ellipse.StrokeThickness = 1.0;
      }
      else
      {
        frameworkElement = IntroWindow.GelCheckmark(18.0);
      }
      FrameworkElement frameworkElement2 = frameworkElement;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 1.5,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { Ui.Text(name, 13.0, FontWeights.Medium, SK.TextPrimary, TextAlignment.Left, 1, false, false) }
      };
      if (subtitle != null)
      {
        stackPanel.Children.Add(Ui.Text(subtitle, 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 1, false, false));
      }
      Grid grid = new Grid
      {
        Padding = new Thickness(14.0, 10.0, 14.0, 10.0),
        ColumnSpacing = 12.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      Grid.SetColumn(stackPanel, 1);
      grid.Children.Add(frameworkElement2);
      grid.Children.Add(stackPanel);
      p.Children.Add(grid);
      p.HoverChanged += delegate(bool h)
      {
        if (available)
        {
          p.Background = Ui.Brush(this.look.Ink.WithAlpha(h ? 0.05 : 0.0));
        }
      };
      if (available)
      {
        p.Click += onPick;
      }
      return p;
    }

    // Token: 0x060009B2 RID: 2482 RVA: 0x00551D28 File Offset: 0x00551D28
    private void HookAudioDeviceChanges()
    {
      if (this.audioDeviceChangesHooked)
      {
        return;
      }
      this.audioDeviceChangesHooked = true;
      AudioTopologyMonitor.Shared.Changed += this.OnAudioTopologyChanged;
      if (this.audioDeviceUnloadHooked)
      {
        return;
      }
      this.audioDeviceUnloadHooked = true;
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        AudioTopologyMonitor.Shared.Changed -= this.OnAudioTopologyChanged;
        this.audioDeviceChangesHooked = false;
      };
    }

    // Token: 0x060009B3 RID: 2483 RVA: 0x00551D80 File Offset: 0x00551D80
    private void OnAudioTopologyChanged(AudioTopologySnapshot snapshot)
    {
      this.dq.TryEnqueue(delegate
      {
        bool flag = !this.isShown;
        if (!flag)
        {
          SettingsWindow.Page page = this.page;
          bool flag2 = page - SettingsWindow.Page.Voice <= 1;
          flag = !flag2;
        }
        if (flag)
        {
          return;
        }
        Log.Info("settings", "audio devices changed: re-listing");
        this.RefreshAudioDevicesAsync();
      });
    }

    // Token: 0x060009B4 RID: 2484 RVA: 0x00551D9C File Offset: 0x00551D9C
    private async Task RefreshAudioDevicesAsync()
    {
      ValueTuple<IReadOnlyList<AudioDeviceInfo>, IReadOnlyList<AudioDeviceInfo>> valueTuple = await Task.Run<ValueTuple<IReadOnlyList<AudioDeviceInfo>, IReadOnlyList<AudioDeviceInfo>>>(() => new ValueTuple<IReadOnlyList<AudioDeviceInfo>, IReadOnlyList<AudioDeviceInfo>>(AudioDevices.List(true), AudioDevices.List(false)));
      IReadOnlyList<AudioDeviceInfo> capture = valueTuple.Item1;
      IReadOnlyList<AudioDeviceInfo> render = valueTuple.Item2;
      this.dq.TryEnqueue(delegate
      {
        this.captureDevices = capture;
        this.renderDevices = render;
        string text = "settings";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 2);
        defaultInterpolatedStringHandler.AppendLiteral("audio devices: ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(capture.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" microphones, ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(render.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" outputs");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        SettingsWindow.Page page = this.page;
        bool flag = page - SettingsWindow.Page.Voice <= 1;
        if (flag)
        {
          this.Render();
        }
      });
    }

    // Token: 0x060009B5 RID: 2485 RVA: 0x00551DE0 File Offset: 0x00551DE0
    private void StartMicrophoneTest()
    {
      using (MessagesPalette.Use(this.look))
      {
        this.StopMicrophoneTest();
        try
        {
          this.micTest = new WasapiMicrophoneCapture();
          this.micTest.Start();
        }
        catch (Exception ex)
        {
          Log.Warn("settings", "mic test failed: " + ex.Message);
          this.micTest = null;
          this.SetMicStatus("Could not start microphone test", SK.Warning);
          return;
        }
        this.micTestHeard = false;
        this.micTestLevel = 0.0;
        this.micTestStarted = DateTime.UtcNow;
        this.SetMicStatus("Listening...", SK.TextSecondary);
        if (this.micTestButton != null)
        {
          this.micTestButton.Title = "Stop";
        }
        this.micTestTimer = new DispatcherTimer
        {
          Interval = TimeSpan.FromMilliseconds(60L, 0L)
        };
        this.micTestTimer.Tick += delegate([Nullable(2)] object _, object _)
        {
          using (MessagesPalette.Use(this.look))
          {
            if (this.micTest != null)
            {
              this.micTestLevel = Math.Clamp(Math.Max((double)this.micTest.Level, this.micTestLevel * 0.68), 0.0, 1.0);
              if (this.micTestLevel >= 0.045 && !this.micTestHeard)
              {
                this.micTestHeard = true;
                this.SetMicStatus("Receiving audio", MessagesPalette.Current.SelectionBlue);
                Log.Info("settings", "mic test: receiving audio (" + this.micTest.CurrentDeviceId + ")");
              }
              else if (!this.micTestHeard && DateTime.UtcNow - this.micTestStarted > TimeSpan.FromSeconds(2.2) && this.micTestStatus != "HeyClicky can't hear you. Try different microphone.")
              {
                this.SetMicStatus("HeyClicky can't hear you. Try different microphone.", SK.Warning);
              }
              this.UpdateMicBars();
            }
          }
        };
        this.micTestTimer.Start();
        Log.Info("settings", "mic test: started");
      }
    }

    // Token: 0x060009B6 RID: 2486 RVA: 0x00551F10 File Offset: 0x00551F10
    private void StopMicrophoneTest()
    {
      using (MessagesPalette.Use(this.look))
      {
        DispatcherTimer dispatcherTimer = this.micTestTimer;
        if (dispatcherTimer != null)
        {
          dispatcherTimer.Stop();
        }
        this.micTestTimer = null;
        WasapiMicrophoneCapture wasapiMicrophoneCapture = this.micTest;
        if (wasapiMicrophoneCapture != null)
        {
          this.micTest = null;
          Task.Run(new Action(wasapiMicrophoneCapture.Dispose));
          Log.Info("settings", "mic test: stopped");
        }
        this.micTestLevel = 0.0;
        this.SetMicStatus("Tap to test selected input", default(Color));
        if (this.micTestButton != null)
        {
          this.micTestButton.Title = "Test microphone";
        }
        this.UpdateMicBars();
      }
    }

    // Token: 0x060009B7 RID: 2487 RVA: 0x00551FD8 File Offset: 0x00551FD8
    private void SetMicStatus(string text, Color color)
    {
      this.micTestStatus = text;
      this.micTestStatusColor = color;
      if (this.micStatusText != null)
      {
        this.micStatusText.Text = text;
        this.micStatusText.Foreground = Ui.Brush((color == default(Color)) ? SK.TextTertiary : color);
      }
    }

    // Token: 0x060009B8 RID: 2488 RVA: 0x00552030 File Offset: 0x00552030
    private void UpdateMicBars()
    {
      using (MessagesPalette.Use(this.look))
      {
        if (this.micBars != null)
        {
          for (int i = 0; i < this.micBars.Children.Count; i++)
          {
            bool flag = this.micTestLevel * 16.0 > (double)i;
            ((Rectangle)this.micBars.Children[i]).Fill = Ui.Brush((!flag) ? SK.Ink.WithAlpha(0.12) : ((i >= 13) ? SK.Destructive : ((i >= 10) ? SK.Warning : SK.Positive)));
          }
        }
      }
    }

    // Token: 0x1700017E RID: 382
    // (get) Token: 0x060009B9 RID: 2489 RVA: 0x00552100 File Offset: 0x00552100
    public SettingsWindow.Page CurrentPage
    {
      get
      {
        return this.page;
      }
    }

    // Token: 0x14000024 RID: 36
    // (add) Token: 0x060009BA RID: 2490 RVA: 0x00552108 File Offset: 0x00552108
    // (remove) Token: 0x060009BB RID: 2491 RVA: 0x00552140 File Offset: 0x00552140
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<SettingsWindow.Page> PageChanged;

    // Token: 0x1700017F RID: 383
    // (get) Token: 0x060009BC RID: 2492 RVA: 0x00552178 File Offset: 0x00552178
    public SettingsHost Host
    {
      get
      {
        return this.host;
      }
    }

    // Token: 0x060009BD RID: 2493 RVA: 0x00552180 File Offset: 0x00552180
    public SettingsContent(AppServices services, IAppActions actions, [Nullable(2)] Action close = null)
    {
      this.s = services;
      this.actions = actions;
      this.host = new SettingsHost(services, actions);
      this.dq = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
      base.ColumnDefinitions.Add(this.sidebarColumn);
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0)
      });
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      this.sidebar = new Grid
      {
        Padding = new Thickness(14.0, 6.0, 16.0, 14.0)
      };
      this.sidebar.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength((double)((close == null) ? 0 : 40))
      });
      this.sidebar.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      this.sidebar.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      if (close != null)
      {
        Pressable pressable = new Pressable(true)
        {
          Width = 24.0,
          Height = 24.0,
          HorizontalAlignment = HorizontalAlignment.Left,
          VerticalAlignment = VerticalAlignment.Center,
          Margin = new Thickness(2.0, 0.0, 0.0, 0.0)
        };
        pressable.Children.Add(new Ellipse
        {
          Fill = Ui.Brush(DS.White(0.1))
        });
        TextBlock textBlock = Icons.Glyph("\ue711", 9.0, DS.White(0.75));
        textBlock.FontWeight = FontWeights.Bold;
        pressable.Children.Add(textBlock);
        pressable.Click += close;
        pressable.Tip("Close");
        this.sidebar.Children.Add(pressable);
      }
      ScrollViewer scrollViewer = new ScrollViewer
      {
        Content = this.sidebarItems,
        VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
        Padding = new Thickness(0.0, 0.0, 0.0, 16.0)
      };
      Grid.SetRow(scrollViewer, 1);
      this.sidebar.Children.Add(scrollViewer);
      TextBlock textBlock2 = Ui.Text(this.host.BuildInfoLine, 9.5, FontWeights.Medium, SK.TextTertiary.WithAlpha(0.8), TextAlignment.Left, 0, false, false);
      textBlock2.Margin = new Thickness(10.0, 12.0, 10.0, 0.0);
      Grid.SetRow(textBlock2, 2);
      this.sidebar.Children.Add(textBlock2);
      Border border = new Border
      {
        Background = Ui.Brush(SK.Ink.WithAlpha(0.07))
      };
      Grid.SetColumn(border, 1);
      this.scroller = new ScrollViewer
      {
        Content = this.content,
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
        HorizontalScrollMode = ScrollMode.Disabled
      };
      Grid.SetColumn(this.scroller, 2);
      Grid.SetColumn(this.overlayLayer, 0);
      Grid.SetColumnSpan(this.overlayLayer, 3);
      base.Children.Add(this.sidebar);
      base.Children.Add(border);
      base.Children.Add(this.scroller);
      base.Children.Add(this.overlayLayer);
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        this.Subscribe();
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        this.Unsubscribe();
        this.OnPageLeft();
      };
    }

    // Token: 0x060009BE RID: 2494 RVA: 0x005526D8 File Offset: 0x005526D8
    private void Subscribe()
    {
      if (this.subscribed)
      {
        return;
      }
      this.subscribed = true;
      if (!(this.s.Settings is CoreAppSettings))
      {
        this.s.Settings.Changed += this.OnSettingsChanged;
      }
      this.host.Store.Changed += this.OnSettingsChanged;
      this.s.Auth.Changed += this.OnAuthChanged;
      this.HookBilling();
      if (this.loadedBefore)
      {
        this.RenderWhenShown();
      }
      this.loadedBefore = true;
    }

    // Token: 0x060009BF RID: 2495 RVA: 0x00552778 File Offset: 0x00552778
    private void Unsubscribe()
    {
      if (!this.subscribed)
      {
        return;
      }
      this.subscribed = false;
      if (!(this.s.Settings is CoreAppSettings))
      {
        this.s.Settings.Changed -= this.OnSettingsChanged;
      }
      this.host.Store.Changed -= this.OnSettingsChanged;
      this.s.Auth.Changed -= this.OnAuthChanged;
      if (this.billingHooked)
      {
        BillingUi billing = this.host.Billing;
        if (billing != null)
        {
          billing.Changed -= this.OnBillingChanged;
        }
      }
      this.billingHooked = false;
    }

    // Token: 0x060009C0 RID: 2496 RVA: 0x0055282C File Offset: 0x0055282C
    private void OnSettingsChanged([Nullable(2)] object sender, EventArgs e)
    {
      if (this.quietWrites == 0)
      {
        this.dq.TryEnqueue(new Microsoft.UI.Dispatching.DispatcherQueueHandler(this.RenderWhenShown));
      }
    }

    // Token: 0x060009C1 RID: 2497 RVA: 0x00552850 File Offset: 0x00552850
    private void OnAuthChanged([Nullable(2)] object sender, EventArgs e)
    {
      this.dq.TryEnqueue(delegate
      {
        this.ResetReferralIfAccountChanged();
        this.RenderWhenShown();
      });
    }

    // Token: 0x060009C2 RID: 2498 RVA: 0x0055286C File Offset: 0x0055286C
    private void RenderWhenShown()
    {
      if (this.isShown)
      {
        this.RenderSoon();
      }
    }

    // Token: 0x17000180 RID: 384
    // (set) Token: 0x060009C3 RID: 2499 RVA: 0x0055287C File Offset: 0x0055287C
    public double SidebarWidth
    {
      set
      {
        this.sidebarColumn.Width = new GridLength(value);
      }
    }

    // Token: 0x060009C4 RID: 2500 RVA: 0x00552890 File Offset: 0x00552890
    public void UseNotchGrid(double sidebarLeadingInset, double footerBottomInset)
    {
      this.sidebar.Padding = new Thickness(sidebarLeadingInset, 6.0, 16.0, footerBottomInset);
    }

    // Token: 0x060009C5 RID: 2501 RVA: 0x005528B8 File Offset: 0x005528B8
    public void Show(SettingsWindow.Page? at = null)
    {
      if (at != null)
      {
        SettingsWindow.Page valueOrDefault = at.GetValueOrDefault();
        if (valueOrDefault != this.page)
        {
          this.OnPageLeft();
          this.page = valueOrDefault;
        }
      }
      this.isShown = true;
      this.Render(true);
      this.OnPageShown();
    }

    // Token: 0x060009C6 RID: 2502 RVA: 0x00552900 File Offset: 0x00552900
    public void Hidden()
    {
      this.isShown = false;
      this.lastBuiltPage = null;
      this.OnPageLeft();
    }

    // Token: 0x060009C7 RID: 2503 RVA: 0x0055291C File Offset: 0x0055291C
    private void Quiet(string key, [Nullable(2)] object value, Action<AppSettings> change)
    {
      this.quietWrites++;
      try
      {
        this.host.Set(key, value, change);
      }
      finally
      {
        this.quietWrites--;
      }
    }

    // Token: 0x060009C8 RID: 2504 RVA: 0x00552968 File Offset: 0x00552968
    private void Render()
    {
      this.Render(false);
    }

    // Token: 0x060009C9 RID: 2505 RVA: 0x00552974 File Offset: 0x00552974
    private void RenderSoon()
    {
      if (this.renderQueued)
      {
        return;
      }
      this.renderQueued = true;
      this.dq.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Low, delegate
      {
        this.renderQueued = false;
        this.Render();
      });
    }

    // Token: 0x060009CA RID: 2506 RVA: 0x005529A0 File Offset: 0x005529A0
    private void Render(bool resetScroll)
    {
      using (MessagesPalette.Use(this.look))
      {
        this.SyncSidebar();
        double num = (resetScroll ? 0.0 : this.scroller.VerticalOffset);
        this.content.Content = this.BuildPage(this.page);
        if (this.page == SettingsWindow.Page.Account)
        {
          this.shownPlanSignature = this.PlanSignature();
        }
        if (num > 0.0)
        {
          this.scroller.UpdateLayout();
          this.scroller.ChangeView(null, new double?(num), null, true);
        }
        else if (resetScroll)
        {
          this.scroller.ChangeView(null, new double?(0.0), null, true);
        }
      }
    }

    // Token: 0x060009CB RID: 2507 RVA: 0x00552A94 File Offset: 0x00552A94
    private FrameworkElement BuildPage(SettingsWindow.Page p)
    {
      if (p == SettingsWindow.Page.Referral && this.lastBuiltPage.GetValueOrDefault() != SettingsWindow.Page.Referral)
      {
        this.NoteReferralPageAppeared();
      }
      this.lastBuiltPage = new SettingsWindow.Page?(p);
      FrameworkElement frameworkElement2;
      try
      {
        FrameworkElement frameworkElement;
        switch (p)
        {
        case SettingsWindow.Page.Account:
          frameworkElement = this.AccountPage();
          break;
        case SettingsWindow.Page.General:
          frameworkElement = this.GeneralPage();
          break;
        case SettingsWindow.Page.Referral:
          frameworkElement = this.ReferralPage();
          break;
        case SettingsWindow.Page.Voice:
          frameworkElement = this.VoicePage();
          break;
        case SettingsWindow.Page.Microphone:
          frameworkElement = this.MicrophonePage();
          break;
        case SettingsWindow.Page.Dictation:
          frameworkElement = this.DictationPage();
          break;
        case SettingsWindow.Page.Shortcuts:
          frameworkElement = this.ShortcutsPage();
          break;
        case SettingsWindow.Page.Cursor:
          frameworkElement = this.CursorPage();
          break;
        case SettingsWindow.Page.Agents:
          frameworkElement = this.AgentsPage();
          break;
        case SettingsWindow.Page.Routines:
          frameworkElement = this.RoutinesPage();
          break;
        case SettingsWindow.Page.Integrations:
          frameworkElement = this.IntegrationsPage();
          break;
        case SettingsWindow.Page.Developer:
          frameworkElement = this.DeveloperPage();
          break;
        default:
          frameworkElement = this.AccountPage();
          break;
        }
        frameworkElement2 = frameworkElement;
      }
      catch (Exception ex)
      {
        string text = "settings";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 1);
        defaultInterpolatedStringHandler.AppendFormatted<SettingsWindow.Page>(p);
        defaultInterpolatedStringHandler.AppendLiteral(" page failed to build");
        Log.Error(text, defaultInterpolatedStringHandler.ToStringAndClear(), ex);
        frameworkElement2 = SK.Scaffold(p.ToString(), "This page couldn't load.", new <>z__ReadOnlySingleElementList<FrameworkElement>(Ui.Text(ex.Message, 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 0, true, false)), 20.0);
      }
      return frameworkElement2;
    }

    // Token: 0x060009CC RID: 2508 RVA: 0x00552C00 File Offset: 0x00552C00
    private void SyncSidebar()
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
      defaultInterpolatedStringHandler.AppendFormatted<bool>(this.host.ShowsDeveloperSection);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      UserProfile user = this.s.Auth.User;
      defaultInterpolatedStringHandler.AppendFormatted((user != null) ? user.AvatarUrl : null);
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      if (text != this.sidebarSignature)
      {
        this.sidebarSignature = text;
        this.sidebarRows.Clear();
        this.sidebarRowSelected.Clear();
        this.sidebarItems.Children.Clear();
        this.AddSidebarGroup(null, new ValueTuple<SettingsWindow.Page, string, string>[]
        {
          new ValueTuple<SettingsWindow.Page, string, string>(SettingsWindow.Page.Account, "Account", "M12,2.5 A4.75,4.75 0 1 1 12,12 A4.75,4.75 0 1 1 12,2.5 Z M4,20.2 C4,16.4 7.6,13.8 12,13.8 C16.4,13.8 20,16.4 20,20.2 C20,21 19.5,21.5 18.7,21.5 L5.3,21.5 C4.5,21.5 4,21 4,20.2 Z"),
          new ValueTuple<SettingsWindow.Page, string, string>(SettingsWindow.Page.General, "General", "\ue713"),
          new ValueTuple<SettingsWindow.Page, string, string>(SettingsWindow.Page.Referral, "Invite & Earn", "\uf133")
        });
        this.AddSidebarGroup("HEYCLICKY", new ValueTuple<SettingsWindow.Page, string, string>[]
        {
          new ValueTuple<SettingsWindow.Page, string, string>(SettingsWindow.Page.Voice, "Voice", "M3,10 h2 v4 h-2 z M7,7 h2 v10 h-2 z M11,3 h2 v18 h-2 z M15,6 h2 v12 h-2 z M19,9 h2 v6 h-2 z"),
          new ValueTuple<SettingsWindow.Page, string, string>(SettingsWindow.Page.Microphone, "Microphone", "\ue720"),
          new ValueTuple<SettingsWindow.Page, string, string>(SettingsWindow.Page.Dictation, "Dictation", "M7.5,3 L16.5,3 L16.5,5 L13,5 L13,19 L16.5,19 L16.5,21 L7.5,21 L7.5,19 L11,19 L11,5 L7.5,5 Z"),
          new ValueTuple<SettingsWindow.Page, string, string>(SettingsWindow.Page.Shortcuts, "Shortcuts", "⌘"),
          new ValueTuple<SettingsWindow.Page, string, string>(SettingsWindow.Page.Cursor, "Cursor", "M6,2.5 L6,19.5 L10.2,15.6 L13,21.8 L15.6,20.6 L12.9,14.6 L18.6,14.6 Z")
        });
        this.AddSidebarGroup("WORK", new ValueTuple<SettingsWindow.Page, string, string>[]
        {
          new ValueTuple<SettingsWindow.Page, string, string>(SettingsWindow.Page.Integrations, "Integrations", "\uea86"),
          new ValueTuple<SettingsWindow.Page, string, string>(SettingsWindow.Page.Agents, "Agents", SettingsContent.HexGridPath),
          new ValueTuple<SettingsWindow.Page, string, string>(SettingsWindow.Page.Routines, "Routines", "\ue72c")
        });
        if (this.host.ShowsDeveloperSection)
        {
          this.AddSidebarGroup("INTERNAL", new ValueTuple<SettingsWindow.Page, string, string>[]
          {
            new ValueTuple<SettingsWindow.Page, string, string>(SettingsWindow.Page.Developer, "Developer", "\uec7a")
          });
        }
      }
      foreach (KeyValuePair<SettingsWindow.Page, ValueTuple<Pressable, Grid, TextBlock, string>> keyValuePair in this.sidebarRows)
      {
        SettingsWindow.Page page;
        ValueTuple<Pressable, Grid, TextBlock, string> valueTuple;
        keyValuePair.Deconstruct(out page, out valueTuple);
        SettingsWindow.Page page2 = page;
        ValueTuple<Pressable, Grid, TextBlock, string> valueTuple2 = valueTuple;
        this.StyleSidebarRow(page2, valueTuple2);
      }
    }

    // Token: 0x060009CD RID: 2509 RVA: 0x00552E5C File Offset: 0x00552E5C
    private void StyleSidebarRow(SettingsWindow.Page p, [TupleElementNames(new string[] { "Row", "IconBox", "Text", "Glyph" })] [Nullable(new byte[] { 0, 1, 1, 1, 1 })] ValueTuple<Pressable, Grid, TextBlock, string> parts)
    {
      SettingsContent.<>c__DisplayClass320_0 CS$<>8__locals1 = new SettingsContent.<>c__DisplayClass320_0();
      CS$<>8__locals1.parts = parts;
      CS$<>8__locals1.p = p;
      CS$<>8__locals1.<>4__this = this;
      bool flag = CS$<>8__locals1.p == this.page;
      bool flag2;
      if (this.sidebarRowSelected.TryGetValue(CS$<>8__locals1.p, out flag2) && flag2 == flag)
      {
        return;
      }
      this.sidebarRowSelected[CS$<>8__locals1.p] = flag;
      Color color = (flag ? SK.TextPrimary : SK.TextSecondary);
      if (CS$<>8__locals1.parts.Item2.Children.Count > 0)
      {
        CS$<>8__locals1.parts.Item2.Children.RemoveAt(0);
      }
      CS$<>8__locals1.parts.Item2.Children.Insert(0, SettingsContent.SidebarIcon(CS$<>8__locals1.parts.Item4, color));
      CS$<>8__locals1.parts.Item3.FontWeight = (flag ? FontWeights.SemiBold : FontWeights.Medium);
      CS$<>8__locals1.parts.Item3.Foreground = Ui.Brush(color);
      CS$<>8__locals1.parts.Item1.Background = Ui.Brush(SK.Ink.WithAlpha(flag ? 0.12 : (CS$<>8__locals1.parts.Item1.IsHovered ? 0.07 : 0.0)));
      if (flag)
      {
        if (CS$<>8__locals1.parts.Item1.IsLoaded)
        {
          CS$<>8__locals1.parts.Item1.StartBringIntoView(new BringIntoViewOptions
          {
            AnimationDesired = false
          });
          return;
        }
        CS$<>8__locals1.parts.Item1.Loaded += CS$<>8__locals1.<StyleSidebarRow>g__BringIntoViewOnce|0;
      }
    }

    // Token: 0x060009CE RID: 2510 RVA: 0x00552FFC File Offset: 0x00552FFC
    private static BitmapImage AvatarImage(Uri uri)
    {
      BitmapImage bitmapImage;
      if (!SettingsContent.AvatarImages.TryGetValue(uri.AbsoluteUri, out bitmapImage))
      {
        Dictionary<string, BitmapImage> avatarImages = SettingsContent.AvatarImages;
        string absoluteUri = uri.AbsoluteUri;
        BitmapImage bitmapImage2 = new BitmapImage();
        bitmapImage2.DecodePixelType = DecodePixelType.Logical;
        bitmapImage2.DecodePixelWidth = 18;
        bitmapImage2.UriSource = uri;
        bitmapImage = bitmapImage2;
        avatarImages[absoluteUri] = bitmapImage2;
      }
      return bitmapImage;
    }

    // Token: 0x060009CF RID: 2511 RVA: 0x0055304C File Offset: 0x0055304C
    [NullableContext(2)]
    private void AddSidebarGroup(string header, [TupleElementNames(new string[] { "Page", "Title", "Glyph" })] [Nullable(new byte[] { 1, 0, 1, 1 })] params ValueTuple<SettingsWindow.Page, string, string>[] rows)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 2.0
      };
      if (header != null)
      {
        TextBlock textBlock = Ui.Text(header, 9.5, FontWeights.Bold, SK.TextTertiary, TextAlignment.Left, 0, false, false);
        textBlock.CharacterSpacing = 100;
        textBlock.Margin = new Thickness(10.0, 0.0, 0.0, 4.0);
        stackPanel.Children.Add(textBlock);
      }
      foreach (ValueTuple<SettingsWindow.Page, string, string> valueTuple in rows)
      {
        SettingsWindow.Page item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        string item3 = valueTuple.Item3;
        stackPanel.Children.Add(this.SidebarRow(item, item2, item3));
      }
      this.sidebarItems.Children.Add(stackPanel);
    }

    // Token: 0x060009D0 RID: 2512 RVA: 0x00553128 File Offset: 0x00553128
    private FrameworkElement SidebarRow(SettingsWindow.Page p, string title, string glyph)
    {
      Pressable row = new Pressable(true)
      {
        CornerRadius = new CornerRadius(8.0),
        Padding = new Thickness(10.0, 12.0, 10.0, 12.0)
      };
      Grid grid = new Grid
      {
        Width = 18.0,
        Height = 18.0,
        Children = { SettingsContent.SidebarIcon(glyph, SK.TextSecondary) }
      };
      if (p == SettingsWindow.Page.Account)
      {
        UserProfile user = this.s.Auth.User;
        string text = ((user != null) ? user.AvatarUrl : null);
        Uri uri;
        if (text != null && text.Length > 0 && Uri.TryCreate(text, UriKind.Absolute, out uri))
        {
          Ellipse ellipse = new Ellipse
          {
            Width = 18.0,
            Height = 18.0,
            Fill = new ImageBrush
            {
              ImageSource = SettingsContent.AvatarImage(uri),
              Stretch = Stretch.UniformToFill
            }
          };
          grid.Children.Add(ellipse);
        }
      }
      TextBlock textBlock = Ui.Text(title, 12.5, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 0, false, false);
      row.Children.Add(Ui.H(9.0, new UIElement[] { grid, textBlock }));
      this.sidebarRows[p] = new ValueTuple<Pressable, Grid, TextBlock, string>(row, grid, textBlock, glyph);
      row.HoverChanged += delegate(bool h)
      {
        if (p != this.page)
        {
          row.Background = Ui.Brush(this.look.Ink.WithAlpha(h ? 0.07 : 0.0));
        }
      };
      row.Click += delegate
      {
        if (this.page == p)
        {
          return;
        }
        string text2 = "notch_peek_action";
        string text3 = null;
        string text4 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["action"] = "settings_" + SettingsContent.SidebarSectionRawValue(p);
        jsonObject["via"] = "sidebar";
        ClickyAnalytics.TrackProductEvent(text2, text3, text4, jsonObject);
        if (p == SettingsWindow.Page.Referral)
        {
          ClickyAnalytics.NoteReferralPageEntryPoint("settings_sidebar");
        }
        this.OnPageLeft();
        this.page = p;
        string text5 = "settings";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
        defaultInterpolatedStringHandler.AppendLiteral("page ");
        defaultInterpolatedStringHandler.AppendFormatted<SettingsWindow.Page>(p);
        Log.Info(text5, defaultInterpolatedStringHandler.ToStringAndClear());
        this.Render(true);
        this.OnPageShown();
        Action<SettingsWindow.Page> pageChanged = this.PageChanged;
        if (pageChanged == null)
        {
          return;
        }
        pageChanged(p);
      };
      return row;
    }

    // Token: 0x060009D1 RID: 2513 RVA: 0x00553304 File Offset: 0x00553304
    private static string SidebarSectionRawValue(SettingsWindow.Page p)
    {
      return Enum.Parse<NotchSettingsSection>(p.ToString()).RawValue();
    }

    // Token: 0x060009D2 RID: 2514 RVA: 0x00553320 File Offset: 0x00553320
    private static FrameworkElement SidebarIcon(string glyph, Color color)
    {
      if (glyph == "⌘")
      {
        return new TextBlock
        {
          Text = glyph,
          FontFamily = new FontFamily("Segoe UI Symbol"),
          FontSize = 14.0,
          FontWeight = FontWeights.SemiBold,
          Foreground = Ui.Brush(color),
          IsTextScaleFactorEnabled = false,
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center
        };
      }
      if (glyph.StartsWith('M'))
      {
        double num = ((glyph == "M3,10 h2 v4 h-2 z M7,7 h2 v10 h-2 z M11,3 h2 v18 h-2 z M15,6 h2 v12 h-2 z M19,9 h2 v6 h-2 z" || glyph == SettingsContent.HexGridPath) ? 13.0 : 12.5);
        return Icons.PathIcon(glyph, num, color);
      }
      TextBlock textBlock = Icons.Glyph(glyph, 11.5, color);
      textBlock.FontWeight = FontWeights.SemiBold;
      return textBlock;
    }

    // Token: 0x060009D3 RID: 2515 RVA: 0x005533F0 File Offset: 0x005533F0
    public static FrameworkElement Switch(bool on, Action<bool> changed)
    {
      return SK.Switch(on, changed);
    }

    // Token: 0x060009D4 RID: 2516 RVA: 0x005533FC File Offset: 0x005533FC
    private void ShowOverlay(FrameworkElement card)
    {
      this.overlayLayer.Children.Clear();
      Border border = new Border
      {
        Background = Ui.Brush(DS.Black(0.55))
      };
      this.overlayLayer.Children.Add(border);
      card.HorizontalAlignment = HorizontalAlignment.Center;
      card.VerticalAlignment = VerticalAlignment.Center;
      this.overlayLayer.Children.Add(card);
    }

    // Token: 0x060009D5 RID: 2517 RVA: 0x00553468 File Offset: 0x00553468
    private void HideOverlay()
    {
      this.overlayLayer.Children.Clear();
    }

    // Token: 0x060009D6 RID: 2518 RVA: 0x0055347C File Offset: 0x0055347C
    private void HookBilling()
    {
      if (!this.billingHooked && this.subscribed)
      {
        BillingUi billing = this.host.Billing;
        if (billing != null)
        {
          this.billingHooked = true;
          billing.Changed += this.OnBillingChanged;
          return;
        }
      }
    }

    // Token: 0x060009D7 RID: 2519 RVA: 0x005534C4 File Offset: 0x005534C4
    private void OnBillingChanged()
    {
      this.dq.TryEnqueue(delegate
      {
        if (this.page != SettingsWindow.Page.Account || this.PlanSignature() == this.shownPlanSignature)
        {
          return;
        }
        this.RenderWhenShown();
      });
    }

    // Token: 0x060009D8 RID: 2520 RVA: 0x005534E0 File Offset: 0x005534E0
    [NullableContext(2)]
    private unsafe string PlanSignature()
    {
      BillingUi billing = this.host.Billing;
      BillingPlanCard billingPlanCard = ((billing != null) ? billing.PlanCard : null);
      if (billingPlanCard == null)
      {
        return null;
      }
      string text = "|";
      <>y__InlineArray7<string> <>y__InlineArray = default(<>y__InlineArray7<string>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 0) = billingPlanCard.Title;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 1) = billingPlanCard.Description;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 2) = billingPlanCard.TeamLabel;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 3) = billingPlanCard.ErrorText;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 4) = billingPlanCard.CancellationLabel;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 5) = string.Join<BillingUsageMeter>(",", billingPlanCard.Meters);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 6) = string.Join<BillingPlanAction>(",", billingPlanCard.Actions);
      return string.Join(text, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray7<string>, string>(in <>y__InlineArray, 7));
    }

    // Token: 0x060009D9 RID: 2521 RVA: 0x005535A4 File Offset: 0x005535A4
    private void OnPageShown()
    {
      switch (this.page)
      {
      case SettingsWindow.Page.Account:
        this.HookBilling();
        if (this.host.IsSignedIn && PlanFixtures.Current == null)
        {
          BillingUi billing = this.host.Billing;
          if (billing == null)
          {
            return;
          }
          billing.RefreshAsync();
          return;
        }
        break;
      case SettingsWindow.Page.General:
        this.RefreshLinksThenRender();
        return;
      case SettingsWindow.Page.Referral:
        this.RefreshReferralIfStale(30.0);
        ClickyAnalytics.TrackReferralPageOpened(null);
        return;
      case SettingsWindow.Page.Voice:
        this.RefreshAudioDevicesAsync();
        break;
      case SettingsWindow.Page.Microphone:
        this.RefreshAudioDevicesAsync();
        return;
      case SettingsWindow.Page.Dictation:
      case SettingsWindow.Page.Shortcuts:
      case SettingsWindow.Page.Cursor:
      case SettingsWindow.Page.Agents:
      case SettingsWindow.Page.Routines:
        break;
      case SettingsWindow.Page.Integrations:
        this.ResetDepartures();
        this.RefreshIntegrationsAsync();
        this.ScheduleConnectorStateRefresh();
        return;
      default:
        return;
      }
    }

    // Token: 0x060009DA RID: 2522 RVA: 0x00553660 File Offset: 0x00553660
    private void OnPageLeft()
    {
      this.StopMicrophoneTest();
      this.StopVoicePreview();
      this.CancelShortcutRecording();
      this.ParkConnectorDraft();
    }

    // Token: 0x060009DB RID: 2523 RVA: 0x0055367C File Offset: 0x0055367C
    private async Task RefreshLinksThenRender()
    {
      await this.host.RefreshLinksAsync();
      this.dq.TryEnqueue(delegate
      {
        if (this.page == SettingsWindow.Page.General)
        {
          this.Render();
        }
      });
    }

    // Token: 0x060009E9 RID: 2537 RVA: 0x005551E8 File Offset: 0x005551E8
    [CompilerGenerated]
    internal static GelButton <SignInControl>g__Button|55_0(string title, Action click)
    {
      GelButton gelButton = new GelButton(title, GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 15.0, null, null, null);
      gelButton.Click += click;
      return gelButton;
    }

    // Token: 0x060009EE RID: 2542 RVA: 0x00555380 File Offset: 0x00555380
    [CompilerGenerated]
    internal static string <FreshSessionRow>g__Trailing|60_0()
    {
      DateTime? dateTime = SettingsContent.oneBrainFreshSessionStartedAt;
      if (dateTime != null)
      {
        return "Started " + dateTime.GetValueOrDefault().ToString("t");
      }
      return "";
    }

    // Token: 0x04000331 RID: 817
    [Nullable(2)]
    private string billingActionError;

    // Token: 0x04000332 RID: 818
    [TupleElementNames(new string[] { "Name", "Glyph" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private static readonly ValueTuple<string, string>[] DeletionReasons = new ValueTuple<string, string>[]
    {
      new ValueTuple<string, string>("Not useful for me", "\ue9ce"),
      new ValueTuple<string, string>("Too buggy", "\uebe8"),
      new ValueTuple<string, string>("Too expensive", "\ue8c7"),
      new ValueTuple<string, string>("Privacy concerns", "\ue72e"),
      new ValueTuple<string, string>("Found something better", "\ue734"),
      new ValueTuple<string, string>("Other", "\ue712")
    };

    // Token: 0x04000333 RID: 819
    [TupleElementNames(new string[] { "Capability", "Name", "Glyph", "SettingsUri", "Pinned" })]
    [Nullable(new byte[] { 1, 0, 1, 1, 1, 1 })]
    private static readonly ValueTuple<string, string, string, string, bool>[] PrivacyCapabilities = new ValueTuple<string, string, string, string, bool>[]
    {
      new ValueTuple<string, string, string, string, bool>("broadFileSystemAccess", "Files & Folders", "\ue8b7", "ms-settings:privacy-broadfilesystemaccess", true),
      new ValueTuple<string, string, string, string, bool>("documentsLibrary", "Documents", "\ue8a5", "ms-settings:privacy-documents", false),
      new ValueTuple<string, string, string, string, bool>("picturesLibrary", "Pictures", "\ue91b", "ms-settings:privacy-pictures", false),
      new ValueTuple<string, string, string, string, bool>("contacts", "Contacts", "\ue77b", "ms-settings:privacy-contacts", false),
      new ValueTuple<string, string, string, string, bool>("appointments", "Calendar", "\ue787", "ms-settings:privacy-calendar", false),
      new ValueTuple<string, string, string, string, bool>("microphone", "Microphone", "\ue720", "ms-settings:privacy-microphone", false),
      new ValueTuple<string, string, string, string, bool>("webcam", "Camera", "\ue722", "ms-settings:privacy-webcam", false),
      new ValueTuple<string, string, string, string, bool>("location", "Location", "\ue81d", "ms-settings:privacy-location", false)
    };

    // Token: 0x04000334 RID: 820
    private bool isConfirmingBrowserDisconnect;

    // Token: 0x04000335 RID: 821
    [Nullable(2)]
    private StackPanel browserCardHost;

    // Token: 0x04000336 RID: 822
    private static readonly Dictionary<string, ImageSource> browserIcons = new Dictionary<string, ImageSource>();

    // Token: 0x04000337 RID: 823
    private static readonly HashSet<string> browserIconsLookedUp = new HashSet<string>();

    // Token: 0x04000338 RID: 824
    [Nullable(2)]
    private SettingsContent.ConnectorDraft connectorDraft;

    // Token: 0x04000339 RID: 825
    [Nullable(2)]
    private string confirmRemoveConnector;

    // Token: 0x0400033A RID: 826
    private static readonly TimeSpan ParkedConnectorDraftRetention = TimeSpan.FromSeconds(120L);

    // Token: 0x0400033B RID: 827
    [Nullable(2)]
    private static SettingsContent.ConnectorDraft parkedConnectorDraft;

    // Token: 0x0400033C RID: 828
    private static DateTime parkedConnectorDraftAt;

    // Token: 0x0400033D RID: 829
    private bool connectorEventsHooked;

    // Token: 0x0400033E RID: 830
    [TupleElementNames(new string[] { "Id", "Name" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private static readonly ValueTuple<string, string>[] BrainModels = new ValueTuple<string, string>[]
    {
      new ValueTuple<string, string>("gpt-6.1-sol", "GPT-6.1 Sol"),
      new ValueTuple<string, string>("gpt-6.1-sol-medium", "GPT-6.1 Sol (medium)"),
      new ValueTuple<string, string>("gpt-6.1-sol-fast", "GPT-6.1 Sol fast"),
      new ValueTuple<string, string>("gpt-6.1-sol-fast-medium", "GPT-6.1 Sol fast (medium)"),
      new ValueTuple<string, string>("gpt-6.1-sol-ultrafast", "GPT-6.1 Sol Ultrafast (low)"),
      new ValueTuple<string, string>("gpt-6.1-sol-ultrafast-medium", "GPT-6.1 Sol Ultrafast (medium)"),
      new ValueTuple<string, string>("gpt-6.1-sol-ultrafast-high", "GPT-6.1 Sol Ultrafast (high)"),
      new ValueTuple<string, string>("gpt-6.1-terra-fast-medium", "GPT-6.1 Terra fast (medium)"),
      new ValueTuple<string, string>("claude-haiku-5-5", "Claude Haiku 5.5 (experimental)"),
      new ValueTuple<string, string>("claude-sonnet-5-5", "Claude Sonnet 5.5 (experimental)"),
      new ValueTuple<string, string>("claude-opus-5-5", "Claude Opus 5.5 (experimental)"),
      new ValueTuple<string, string>("deepseek-v4.1-flash-medium", "DeepSeek V4.1 Flash 1017 (medium, experimental)"),
      new ValueTuple<string, string>("deepseek-v4.1-flash-high", "DeepSeek V4.1 Flash 1017 (high, experimental)")
    };

    // Token: 0x0400033F RID: 831
    private static DateTime? oneBrainFreshSessionStartedAt;

    // Token: 0x04000340 RID: 832
    private const string DefaultBrainModel = "gpt-6.1-sol-fast-medium";

    // Token: 0x04000341 RID: 833
    [TupleElementNames(new string[] { "Code", "Name", "Native" })]
    [Nullable(new byte[] { 1, 0, 1, 1, 2 })]
    private static readonly ValueTuple<string, string, string>[] Languages = new ValueTuple<string, string, string>[]
    {
      new ValueTuple<string, string, string>("af", "Afrikaans", null),
      new ValueTuple<string, string, string>("am", "Amharic", "አማርኛ"),
      new ValueTuple<string, string, string>("ar", "Arabic", "العربية"),
      new ValueTuple<string, string, string>("hy", "Armenian", "Հայերեն"),
      new ValueTuple<string, string, string>("rup", "Aromanian", "Armãneashti"),
      new ValueTuple<string, string, string>("as", "Assamese", "অসম\u09c0য\u09bc\u09be"),
      new ValueTuple<string, string, string>("az", "Azerbaijani", "Azərbaycanca"),
      new ValueTuple<string, string, string>("be", "Belarusian", "Беларуская"),
      new ValueTuple<string, string, string>("bn", "Bengali", "ব\u09be\u0982ল\u09be"),
      new ValueTuple<string, string, string>("bs", "Bosnian", "Bosanski"),
      new ValueTuple<string, string, string>("bg", "Bulgarian", "Български"),
      new ValueTuple<string, string, string>("my", "Burmese", "မ\u103cန\u103aမ\u102c"),
      new ValueTuple<string, string, string>("yue", "Cantonese", "粵語"),
      new ValueTuple<string, string, string>("kea", "Cape Verdean Creole", "Kabuverdianu"),
      new ValueTuple<string, string, string>("ca", "Catalan", "Català"),
      new ValueTuple<string, string, string>("ceb", "Cebuano", "Sinugboanon"),
      new ValueTuple<string, string, string>("zh", "Chinese", "中文"),
      new ValueTuple<string, string, string>("hr", "Croatian", "Hrvatski"),
      new ValueTuple<string, string, string>("cs", "Czech", "Čeština"),
      new ValueTuple<string, string, string>("da", "Danish", "Dansk"),
      new ValueTuple<string, string, string>("nl", "Dutch", "Nederlands"),
      new ValueTuple<string, string, string>("en", "English", null),
      new ValueTuple<string, string, string>("et", "Estonian", "Eesti"),
      new ValueTuple<string, string, string>("fi", "Finnish", "Suomi"),
      new ValueTuple<string, string, string>("fr", "French", "Français"),
      new ValueTuple<string, string, string>("gl", "Galician", "Galego"),
      new ValueTuple<string, string, string>("ka", "Georgian", "ქართული"),
      new ValueTuple<string, string, string>("de", "German", "Deutsch"),
      new ValueTuple<string, string, string>("el", "Greek", "Ελληνικά"),
      new ValueTuple<string, string, string>("gu", "Gujarati", "ગ\u0ac1જર\u0abeત\u0ac0"),
      new ValueTuple<string, string, string>("ha", "Hausa", null),
      new ValueTuple<string, string, string>("he", "Hebrew", "עברית"),
      new ValueTuple<string, string, string>("hi", "Hindi", "ह\u093fन\u094dद\u0940"),
      new ValueTuple<string, string, string>("hu", "Hungarian", "Magyar"),
      new ValueTuple<string, string, string>("is", "Icelandic", "Íslenska"),
      new ValueTuple<string, string, string>("id", "Indonesian", "Bahasa Indonesia"),
      new ValueTuple<string, string, string>("it", "Italian", "Italiano"),
      new ValueTuple<string, string, string>("ja", "Japanese", "日本語"),
      new ValueTuple<string, string, string>("jv", "Javanese", "Basa Jawa"),
      new ValueTuple<string, string, string>("kn", "Kannada", "ಕನ\u0ccdನಡ"),
      new ValueTuple<string, string, string>("kk", "Kazakh", "Қазақ тілі"),
      new ValueTuple<string, string, string>("km", "Khmer", "ខ\u17d2ម\u17c2រ"),
      new ValueTuple<string, string, string>("ko", "Korean", "한국어"),
      new ValueTuple<string, string, string>("ky", "Kyrgyz", "Кыргызча"),
      new ValueTuple<string, string, string>("lv", "Latvian", "Latviešu"),
      new ValueTuple<string, string, string>("ln", "Lingala", "Lingála"),
      new ValueTuple<string, string, string>("lt", "Lithuanian", "Lietuvių"),
      new ValueTuple<string, string, string>("mk", "Macedonian", "Македонски"),
      new ValueTuple<string, string, string>("ms", "Malay", "Bahasa Melayu"),
      new ValueTuple<string, string, string>("ml", "Malayalam", "മലയ\u0d3eള\u0d02"),
      new ValueTuple<string, string, string>("mt", "Maltese", "Malti"),
      new ValueTuple<string, string, string>("mr", "Marathi", "मर\u093eठ\u0940"),
      new ValueTuple<string, string, string>("mn", "Mongolian", "Монгол"),
      new ValueTuple<string, string, string>("ne", "Nepali", "न\u0947प\u093eल\u0940"),
      new ValueTuple<string, string, string>("no", "Norwegian", "Norsk"),
      new ValueTuple<string, string, string>("or", "Odia", "ଓଡ\u0b3c\u0b3fଆ"),
      new ValueTuple<string, string, string>("ps", "Pashto", "پښتو"),
      new ValueTuple<string, string, string>("fa", "Persian", "فارسی"),
      new ValueTuple<string, string, string>("pl", "Polish", "Polski"),
      new ValueTuple<string, string, string>("pt", "Portuguese", "Português"),
      new ValueTuple<string, string, string>("pa", "Punjabi", "ਪ\u0a70ਜ\u0a3eਬ\u0a40"),
      new ValueTuple<string, string, string>("ro", "Romanian", "Română"),
      new ValueTuple<string, string, string>("ru", "Russian", "Русский"),
      new ValueTuple<string, string, string>("sr", "Serbian", "Српски"),
      new ValueTuple<string, string, string>("sd", "Sindhi", "سنڌي"),
      new ValueTuple<string, string, string>("si", "Sinhala", "ස\u0dd2\u0d82හල"),
      new ValueTuple<string, string, string>("sk", "Slovak", "Slovenčina"),
      new ValueTuple<string, string, string>("sl", "Slovenian", "Slovenščina"),
      new ValueTuple<string, string, string>("so", "Somali", "Soomaali"),
      new ValueTuple<string, string, string>("es", "Spanish", "Español"),
      new ValueTuple<string, string, string>("sw", "Swahili", "Kiswahili"),
      new ValueTuple<string, string, string>("sv", "Swedish", "Svenska"),
      new ValueTuple<string, string, string>("tl", "Tagalog", "Tagalog"),
      new ValueTuple<string, string, string>("tg", "Tajik", "Тоҷикӣ"),
      new ValueTuple<string, string, string>("ta", "Tamil", "தம\u0bbfழ\u0bcd"),
      new ValueTuple<string, string, string>("te", "Telugu", "త\u0c46ల\u0c41గ\u0c41"),
      new ValueTuple<string, string, string>("th", "Thai", "ไทย"),
      new ValueTuple<string, string, string>("tr", "Turkish", "Türkçe"),
      new ValueTuple<string, string, string>("uk", "Ukrainian", "Українська"),
      new ValueTuple<string, string, string>("ur", "Urdu", "اردو"),
      new ValueTuple<string, string, string>("uz", "Uzbek", "Oʻzbekcha"),
      new ValueTuple<string, string, string>("vi", "Vietnamese", "Tiếng Việt"),
      new ValueTuple<string, string, string>("yo", "Yoruba", "Yorùbá")
    };

    // Token: 0x04000342 RID: 834
    private static readonly string[] PopularLanguageCodes = new string[]
    {
      "en", "es", "fr", "de", "hi", "zh", "ja", "ko", "pt", "ru",
      "ar", "it"
    };

    // Token: 0x04000343 RID: 835
    [TupleElementNames(new string[] { "Code", "Name", "Native" })]
    [Nullable(new byte[] { 1, 0, 1, 1, 2 })]
    private static readonly ValueTuple<string, string, string>[] LanguagesForGrid = SettingsContent.PopularLanguageCodes.Select<string, ValueTuple<string, string, string>>((string c) => SettingsContent.Languages.First<ValueTuple<string, string, string>>(([TupleElementNames(new string[] { "Code", "Name", "Native" })] ValueTuple<string, string, string> l) => l.Item1 == c)).Concat<ValueTuple<string, string, string>>(SettingsContent.Languages.Where<ValueTuple<string, string, string>>(([TupleElementNames(new string[] { "Code", "Name", "Native" })] ValueTuple<string, string, string> l) => !SettingsContent.PopularLanguageCodes.Contains(l.Item1)).OrderBy<ValueTuple<string, string, string>, string>(([TupleElementNames(new string[] { "Code", "Name", "Native" })] ValueTuple<string, string, string> l) => l.Item2, StringComparer.Ordinal)).ToArray<ValueTuple<string, string, string>>();

    // Token: 0x04000344 RID: 836
    private const int MaxDictionaryWords = 50;

    // Token: 0x04000345 RID: 837
    private const int MaxDictionaryWordLength = 60;

    // Token: 0x04000346 RID: 838
    private string languageSearch = "";

    // Token: 0x04000347 RID: 839
    private string draftWord = "";

    // Token: 0x04000348 RID: 840
    [Nullable(2)]
    private Grid languageGridHost;

    // Token: 0x04000349 RID: 841
    private bool focusWordInputAfterRender;

    // Token: 0x0400034A RID: 842
    [Nullable(2)]
    private AgentIntegrationCatalog catalog;

    // Token: 0x0400034B RID: 843
    private bool loadingIntegrations;

    // Token: 0x0400034C RID: 844
    [Nullable(2)]
    private string integrationError;

    // Token: 0x0400034D RID: 845
    private string integrationSearch = "";

    // Token: 0x0400034E RID: 846
    private readonly HashSet<string> integrationBusy = new HashSet<string>();

    // Token: 0x0400034F RID: 847
    private readonly HashSet<string> accountBusy = new HashSet<string>();

    // Token: 0x04000350 RID: 848
    [Nullable(2)]
    private string confirmDisconnectIntegration;

    // Token: 0x04000351 RID: 849
    [Nullable(2)]
    private string confirmDisconnectAccount;

    // Token: 0x04000352 RID: 850
    [TupleElementNames(new string[] { "Id", "Baseline", "Started" })]
    [Nullable(new byte[] { 0, 1 })]
    private ValueTuple<string, int, DateTime>? awaitingConnection;

    // Token: 0x04000353 RID: 851
    [Nullable(2)]
    private string justConnected;

    // Token: 0x04000354 RID: 852
    [Nullable(2)]
    private StackPanel integrationListHost;

    // Token: 0x04000355 RID: 853
    [Nullable(2)]
    private string editingLabelAccount;

    // Token: 0x04000357 RID: 855
    private readonly Dictionary<string, SettingsContent.Departure> departures = new Dictionary<string, SettingsContent.Departure>();

    // Token: 0x04000358 RID: 856
    [TupleElementNames(new string[] { "Id", "Name", "Logo" })]
    [Nullable(new byte[] { 1, 0, 1, 1, 1, 1 })]
    private List<ValueTuple<string, string, Func<FrameworkElement>>> lastConnected = new List<ValueTuple<string, string, Func<FrameworkElement>>>();

    // Token: 0x04000359 RID: 857
    private readonly Dictionary<string, FrameworkElement> departureRows = new Dictionary<string, FrameworkElement>();

    // Token: 0x0400035A RID: 858
    private static readonly double[] ShimmerNameWidths = new double[] { 92.0, 120.0, 74.0, 108.0, 86.0, 130.0 };

    // Token: 0x0400035B RID: 859
    private static readonly double[] ShimmerDescriptionFractions = new double[] { 0.62, 0.48, 0.7, 0.55, 0.66, 0.44 };

    // Token: 0x0400035C RID: 860
    [Nullable(2)]
    private JsonObject referral;

    // Token: 0x0400035D RID: 861
    private bool referralLoading;

    // Token: 0x0400035E RID: 862
    private bool referralFailed;

    // Token: 0x0400035F RID: 863
    [Nullable(2)]
    private string referralCopied;

    // Token: 0x04000360 RID: 864
    private bool editingReferralCode;

    // Token: 0x04000361 RID: 865
    private string referralCodeDraft = "";

    // Token: 0x04000362 RID: 866
    private string referralCodeProblem = "";

    // Token: 0x04000363 RID: 867
    private string friendCodeDraft = "";

    // Token: 0x04000364 RID: 868
    private string friendCodeMessage = "";

    // Token: 0x04000365 RID: 869
    private DateTimeOffset? referralLoadedAt;

    // Token: 0x04000366 RID: 870
    [Nullable(2)]
    private string referralLoadedFor;

    // Token: 0x04000367 RID: 871
    private bool isSurfaceSettled = true;

    // Token: 0x04000368 RID: 872
    private bool referralPageSettled = true;

    // Token: 0x04000369 RID: 873
    private bool referralSkeletonShown;

    // Token: 0x0400036A RID: 874
    private int referralSettleGeneration;

    // Token: 0x0400036C RID: 876
    private bool referralCelebrate;

    // Token: 0x0400036D RID: 877
    private readonly ReferralEstimatorCard.State estimatorState = new ReferralEstimatorCard.State();

    // Token: 0x0400036E RID: 878
    private const int ReferralFriendsPage = 10;

    // Token: 0x0400036F RID: 879
    private int referralVisibleFriends = 10;

    // Token: 0x04000370 RID: 880
    [TupleElementNames(new string[] { "Name", "Asset", "Plan", "Detail", "Earned" })]
    [Nullable(new byte[] { 1, 0, 1, 1, 1, 1, 2 })]
    private static readonly ValueTuple<string, string, string, string, string>[] SampleFriends = new ValueTuple<string, string, string, string, string>[]
    {
      new ValueTuple<string, string, string, string, string>("Farza", "referral-sample-farza", "max", "Subscribed for 2 months", "+$50"),
      new ValueTuple<string, string, string, string, string>("Nilesh", "referral-sample-nilesh", "pro", "Subscribed for 3 months", "+$15"),
      new ValueTuple<string, string, string, string, string>("Kelli", "referral-sample-kelli", "free", "Joined through your link", null)
    };

    // Token: 0x04000371 RID: 881
    private static readonly double[] ReferralSkeletonNameWidths = new double[] { 110.0, 90.0, 124.0 };

    // Token: 0x04000372 RID: 882
    private readonly Dictionary<string, SettingsContent.RoutineSettingsRow> routineRows = new Dictionary<string, SettingsContent.RoutineSettingsRow>();

    // Token: 0x04000373 RID: 883
    private bool routinesHooked;

    // Token: 0x04000374 RID: 884
    [Nullable(2)]
    private ShortcutRecorder recorder;

    // Token: 0x04000375 RID: 885
    private ShortcutRecorder.Slot? conflictRow;

    // Token: 0x04000376 RID: 886
    private ShortcutRecorder.Slot? conflictWith;

    // Token: 0x04000377 RID: 887
    private bool recordingHooked;

    // Token: 0x04000378 RID: 888
    [TupleElementNames(new string[] { "Id", "Name", "Subtitle", "Fill", "Stroke", "Color" })]
    [Nullable(new byte[] { 1, 0, 1, 1, 1, 2, 2 })]
    private static readonly ValueTuple<string, string, string, string, string, Color>[] Voices = new ValueTuple<string, string, string, string, string, Color>[]
    {
      new ValueTuple<string, string, string, string, string, Color>("cedar", "Cedar", "Warm default", "M12,2 C15.3,2 17.8,4.4 18,7.4 C20.3,8.2 21.8,10.3 21.8,12.7 C21.8,15.8 19.3,18.2 16.2,18.2 L13.2,18.2 L13.2,21 C13.2,21.6 12.7,22 12,22 C11.3,22 10.8,21.6 10.8,21 L10.8,18.2 L7.8,18.2 C4.7,18.2 2.2,15.8 2.2,12.7 C2.2,10.3 3.7,8.2 6,7.4 C6.2,4.4 8.7,2 12,2 Z", null, DS.Rgb(0.42, 0.72, 0.52, 1.0)),
      new ValueTuple<string, string, string, string, string, Color>("marin", "Marin", "Bright & natural", null, "M3,7 C5,4.8 7,4.8 9,7 C11,9.2 13,9.2 15,7 C17,4.8 19,4.8 21,7 M3,12 C5,9.8 7,9.8 9,12 C11,14.2 13,14.2 15,12 C17,9.8 19,9.8 21,12 M3,17 C5,14.8 7,14.8 9,17 C11,19.2 13,19.2 15,17 C17,14.8 19,14.8 21,17", DS.Rgb(0.36, 0.66, 1.0, 1.0)),
      new ValueTuple<string, string, string, string, string, Color>("alloy", "Alloy", "Balanced", "M9,5 a3,3 0 1 0 6,0 a3,3 0 1 0 -6,0 Z M2,12 a3,3 0 1 0 6,0 a3,3 0 1 0 -6,0 Z M9,12 a3,3 0 1 0 6,0 a3,3 0 1 0 -6,0 Z M16,12 a3,3 0 1 0 6,0 a3,3 0 1 0 -6,0 Z M9,19 a3,3 0 1 0 6,0 a3,3 0 1 0 -6,0 Z", null, DS.Rgb(0.6, 0.62, 0.7, 1.0)),
      new ValueTuple<string, string, string, string, string, Color>("ash", "Ash", "Calm & even", "M9.5,4 C11.7,4 13.6,5.4 14.2,7.4 C14.8,7.2 15.4,7.1 16,7.1 C18.6,7.1 20.6,9 20.8,11.5 C22.1,12.2 23,13.6 23,15.2 C23,17.6 21.1,19.5 18.7,19.5 L5.6,19.5 C3,19.5 1,17.5 1,15 C1,12.9 2.4,11.1 4.4,10.6 C4.4,10.4 4.4,10.2 4.4,10 C4.4,6.7 6.6,4 9.5,4 Z", null, DS.Rgb(0.55, 0.58, 0.64, 1.0)),
      new ValueTuple<string, string, string, string, string, Color>("ballad", "Ballad", "Expressive", "M10.2,4.2 L10.2,15.3 C9.5,14.9 8.7,14.7 7.8,14.7 C5.6,14.7 3.8,16.1 3.8,17.9 C3.8,19.6 5.6,21 7.8,21 C10,21 11.9,19.6 11.9,17.9 L11.9,8.3 C14.4,8.9 16.6,10.1 17.5,12.6 C17.9,11.8 18.1,10.9 18.1,10 C18.1,6.6 14.7,5.4 12.2,3.6 C11.4,3 10.2,3.2 10.2,4.2 Z", null, DS.Rgb(0.74, 0.46, 0.92, 1.0)),
      new ValueTuple<string, string, string, string, string, Color>("coral", "Coral", "Friendly", "M12,21 C12,21 2.5,15.3 2.5,8.8 C2.5,5.9 4.8,3.6 7.6,3.6 C9.4,3.6 11,4.6 12,6.1 C13,4.6 14.6,3.6 16.4,3.6 C19.2,3.6 21.5,5.9 21.5,8.8 C21.5,15.3 12,21 12,21 Z", null, DS.Rgb(1.0, 0.5, 0.55, 1.0)),
      new ValueTuple<string, string, string, string, string, Color>("echo", "Echo", "Crisp", "M10,12 a2,2 0 1 0 4,0 a2,2 0 1 0 -4,0 Z", "M8.3,8.3 A5.2,5.2 0 0 0 8.3,15.7 M15.7,8.3 A5.2,5.2 0 0 1 15.7,15.7 M5.2,5.2 A9.6,9.6 0 0 0 5.2,18.8 M18.8,5.2 A9.6,9.6 0 0 1 18.8,18.8", DS.Rgb(0.4, 0.7, 0.86, 1.0)),
      new ValueTuple<string, string, string, string, string, Color>("sage", "Sage", "Measured", "M20.6,3.2 C12.2,3.4 4.2,6.4 4.2,13.9 C4.2,15.8 4.8,17.4 5.8,18.7 L3.6,20.9 L4.9,22.2 L7.1,20 C8.3,20.9 9.8,21.4 11.5,21.4 C18.6,21.4 20.7,13.2 20.6,3.2 Z", null, DS.Rgb(0.46, 0.74, 0.46, 1.0)),
      new ValueTuple<string, string, string, string, string, Color>("shimmer", "Shimmer", "Light & airy", "M10,3 C10.6,7.6 12.4,9.4 17,10 C12.4,10.6 10.6,12.4 10,17 C9.4,12.4 7.6,10.6 3,10 C7.6,9.4 9.4,7.6 10,3 Z M18,13 C18.3,15.2 18.8,15.7 21,16 C18.8,16.3 18.3,16.8 18,19 C17.7,16.8 17.2,16.3 15,16 C17.2,15.7 17.7,15.2 18,13 Z M17,2 C17.2,3.4 17.6,3.8 19,4 C17.6,4.2 17.2,4.6 17,6 C16.8,4.6 16.4,4.2 15,4 C16.4,3.8 16.8,3.4 17,2 Z", null, DS.Rgb(0.96, 0.78, 0.36, 1.0)),
      new ValueTuple<string, string, string, string, string, Color>("verse", "Verse", "Lively", "M5,3.5 L19,3.5 A3,3 0 0 1 22,6.5 L22,15 A3,3 0 0 1 19,18 L11.5,18 L6.5,21.8 L6.5,18 L5,18 A3,3 0 0 1 2,15 L2,6.5 A3,3 0 0 1 5,3.5 Z M6.5,8 L6.5,9.8 L17.5,9.8 L17.5,8 Z M6.5,11.7 L6.5,13.5 L14,13.5 L14,11.7 Z", null, DS.Rgb(1.0, 0.62, 0.34, 1.0))
    };

    // Token: 0x04000379 RID: 889
    [TupleElementNames(new string[] { "Fill", "Stroke", "Color" })]
    [Nullable(new byte[] { 1, 0, 2, 2 })]
    private static readonly ValueTuple<string, string, Color>[] ExpressiveGlyphs = new ValueTuple<string, string, Color>[]
    {
      new ValueTuple<string, string, Color>("M2,12 a10,10 0 1 0 20,0 a10,10 0 1 0 -20,0 Z M7.8,9.5 a1.5,1.7 0 1 0 3,0 a1.5,1.7 0 1 0 -3,0 Z M13.2,9.5 a1.5,1.7 0 1 0 3,0 a1.5,1.7 0 1 0 -3,0 Z M7.2,13.4 C8.4,17 15.6,17 16.8,13.4 L15.1,13.4 C14,15.3 10,15.3 8.9,13.4 Z", null, DS.Rgb(1.0, 0.7, 0.3, 1.0)),
      new ValueTuple<string, string, Color>("M7,12 a5,5 0 1 0 10,0 a5,5 0 1 0 -10,0 Z", "M12,1.8 L12,3.6 M12,20.4 L12,22.2 M1.8,12 L3.6,12 M20.4,12 L22.2,12 M4.8,4.8 L6.1,6.1 M17.9,17.9 L19.2,19.2 M4.8,19.2 L6.1,17.9 M17.9,6.1 L19.2,4.8", DS.Rgb(1.0, 0.84, 0.36, 1.0)),
      new ValueTuple<string, string, Color>(null, "M3,8 L14,8 C16,8 17,6.8 17,5.5 C17,4.2 16,3.2 14.7,3.2 C13.6,3.2 12.8,3.9 12.5,4.8 M3,12 L19,12 C20.8,12 22,13.2 22,14.8 C22,16.4 20.8,17.5 19.3,17.5 C18,17.5 17.1,16.7 16.8,15.6 M3,16 L11,16 C12.6,16 13.6,17.1 13.6,18.5 C13.6,19.9 12.6,20.8 11.3,20.8", DS.Rgb(0.52, 0.8, 0.92, 1.0)),
      new ValueTuple<string, string, Color>("M13.5,2 L4.5,13.5 L11,13.5 L10,22 L19.5,10 L13,10 Z", null, DS.Rgb(1.0, 0.48, 0.36, 1.0)),
      new ValueTuple<string, string, Color>("M12,2 C12.9,8.6 15.4,11.1 22,12 C15.4,12.9 12.9,15.4 12,22 C11.1,15.4 8.6,12.9 2,12 C8.6,11.1 11.1,8.6 12,2 Z", null, DS.Rgb(0.98, 0.56, 0.8, 1.0)),
      new ValueTuple<string, string, Color>("M12,2 C12.6,5.6 17.8,8.4 17.8,14.2 C17.8,18.6 15.2,22 12,22 C8.8,22 6.2,18.8 6.2,15.2 C6.2,12.2 7.8,10.3 9.2,9 C9.2,11 10,12.4 11.2,13 C10.8,9.4 11,5.4 12,2 Z", null, DS.Rgb(0.96, 0.58, 0.4, 1.0)),
      new ValueTuple<string, string, Color>("M7,21 C4.8,19.2 4,16.8 4,14 L4,9.5 C4,8.7 4.7,8 5.5,8 C6.3,8 7,8.7 7,9.5 L7,12 L8,12 L8,4.5 C8,3.7 8.7,3 9.5,3 C10.3,3 11,3.7 11,4.5 L11,11 L12,11 L12,3.5 C12,2.7 12.7,2 13.5,2 C14.3,2 15,2.7 15,3.5 L15,11 L16,11 L16,5 C16,4.2 16.7,3.5 17.5,3.5 C18.3,3.5 19,4.2 19,5 L19,15 C19,18.9 16.4,22 12.6,22 L10.5,22 C9.2,22 8,21.7 7,21 Z", "M20.8,2.6 C21.8,3.4 22.3,4.4 22.4,5.6", DS.Rgb(0.5, 0.78, 0.54, 1.0)),
      new ValueTuple<string, string, Color>("M3,21 L8.2,7.6 L16.4,15.8 Z", "M13,3 L13.6,5.6 M17,7.2 C18.6,6.2 20,6.6 21,7.8 M18.4,11.2 L21,11.8 M10.4,5.4 C11.1,4.5 11.1,3.5 10.5,2.7", DS.Rgb(0.7, 0.52, 0.96, 1.0)),
      new ValueTuple<string, string, Color>("M12,2 L21,12 L12,22 L3,12 Z", null, DS.Rgb(0.4, 0.62, 0.96, 1.0)),
      new ValueTuple<string, string, Color>("M3,4.5 C5.8,3.6 8.9,3.8 11.2,5.4 L11.2,20.4 C8.9,18.9 5.8,18.7 3,19.6 Z M12.8,5.4 C15.1,3.8 18.2,3.6 21,4.5 L21,19.6 C18.2,18.7 15.1,18.9 12.8,20.4 Z", null, DS.Rgb(0.58, 0.64, 0.74, 1.0)),
      new ValueTuple<string, string, Color>("M12,2 L20,5 L20,11 C20,16.2 16.6,20.4 12,22 C7.4,20.4 4,16.2 4,11 L4,5 Z", null, DS.Rgb(0.46, 0.56, 0.7, 1.0)),
      new ValueTuple<string, string, Color>("M7,13.5 a5,5 0 0 1 10,0 Z", "M12,3.5 L12,5.5 M5.4,6.6 L6.8,8 M18.6,6.6 L17.2,8 M2.5,13.5 L4.5,13.5 M19.5,13.5 L21.5,13.5 M4,17 L20,17 M7,20.5 L17,20.5", DS.Rgb(0.98, 0.74, 0.5, 1.0)),
      new ValueTuple<string, string, Color>("M12,2.5 L14.9,8.6 L21.5,9.4 L16.6,13.9 L17.9,20.5 L12,17.2 L6.1,20.5 L7.4,13.9 L2.5,9.4 L9.1,8.6 Z", null, DS.Rgb(1.0, 0.8, 0.3, 1.0)),
      new ValueTuple<string, string, Color>("M7,19.5 C4.2,19.5 2,17.4 2,14.7 C2,12.3 3.8,10.3 6.1,10 C6.8,6.9 9.4,4.5 12.6,4.5 C16.1,4.5 18.9,7.2 19.1,10.6 C21,11.2 22.3,12.9 22.3,15 C22.3,17.5 20.3,19.5 17.8,19.5 Z", null, DS.Rgb(0.66, 0.78, 0.92, 1.0)),
      new ValueTuple<string, string, Color>("M12,2.5 C12,2.5 5,10.4 5,14.8 C5,18.7 8.1,21.8 12,21.8 C15.9,21.8 19,18.7 19,14.8 C19,10.4 12,2.5 12,2.5 Z", null, DS.Rgb(0.38, 0.72, 0.96, 1.0)),
      new ValueTuple<string, string, Color>("M15,3 A9.3,9.3 0 1 0 21,15.6 A7.4,7.4 0 0 1 15,3 Z", null, DS.Rgb(0.62, 0.58, 0.86, 1.0)),
      new ValueTuple<string, string, Color>("M3,10 h2 v4 h-2 z M7,7 h2 v10 h-2 z M11,3 h2 v18 h-2 z M15,6 h2 v12 h-2 z M19,9 h2 v6 h-2 z", null, DS.Rgb(0.52, 0.66, 0.92, 1.0)),
      new ValueTuple<string, string, Color>("M12,2 L14.2,4.2 L17.3,3.6 L18.1,6.6 L21,7.8 L20.1,10.8 L21.9,13.4 L19.4,15.3 L19.4,18.4 L16.3,18.8 L14.6,21.5 L12,20 L9.4,21.5 L7.7,18.8 L4.6,18.4 L4.6,15.3 L2.1,13.4 L3.9,10.8 L3,7.8 L5.9,6.6 L6.7,3.6 L9.8,4.2 Z", null, DS.Rgb(0.9, 0.56, 0.74, 1.0)),
      new ValueTuple<string, string, Color>("M8,4.6 L11,6.3 L11,9.8 L8,11.5 L5,9.8 L5,6.3 Z M16,4.6 L19,6.3 L19,9.8 L16,11.5 L13,9.8 L13,6.3 Z M12,12.3 L15,14 L15,17.5 L12,19.2 L9,17.5 L9,14 Z", null, DS.Rgb(0.44, 0.8, 0.76, 1.0)),
      new ValueTuple<string, string, Color>("M1.5,20 L8.5,8 L12.4,14.6 L15.5,9.5 L22.5,20 Z", null, DS.Rgb(0.64, 0.56, 0.48, 1.0)),
      new ValueTuple<string, string, Color>("M12,2 C8.1,2 5,5 5,8.8 C5,11.3 6.3,13.1 7.8,14.5 C8.6,15.2 9,16 9,17 L15,17 C15,16 15.4,15.2 16.2,14.5 C17.7,13.1 19,11.3 19,8.8 C19,5 15.9,2 12,2 Z M9,18.5 L15,18.5 L15,19.8 L9,19.8 Z M10,21 L14,21 C14,21.8 13.1,22.4 12,22.4 C10.9,22.4 10,21.8 10,21 Z", null, DS.Rgb(0.98, 0.84, 0.42, 1.0)),
      new ValueTuple<string, string, Color>(SettingsContent.Voices[4].Item4, null, DS.Rgb(0.96, 0.5, 0.62, 1.0)),
      new ValueTuple<string, string, Color>("M12,5 A8,8 0 1 0 19.5,16.5 A6.4,6.4 0 0 1 12,5 Z M18,2 C18.3,3.7 18.8,4.2 20.5,4.5 C18.8,4.8 18.3,5.3 18,7 C17.7,5.3 17.2,4.8 15.5,4.5 C17.2,4.2 17.7,3.7 18,2 Z", null, DS.Rgb(0.8, 0.74, 0.92, 1.0)),
      new ValueTuple<string, string, Color>("M2,13.5 L13,4.5 L11.5,10.5 L22,10.5 L11,19.5 L12.5,13.5 Z", null, DS.Rgb(0.4, 0.58, 0.8, 1.0)),
      new ValueTuple<string, string, Color>("M2,12 a10,10 0 1 0 20,0 a10,10 0 1 0 -20,0 Z M7,8.6 L17,8.6 L17,10.6 L7,10.6 Z M7,13.4 L17,13.4 L17,15.4 L7,15.4 Z", null, DS.Rgb(0.56, 0.7, 0.66, 1.0)),
      new ValueTuple<string, string, Color>(SettingsContent.Voices[7].Item4, null, DS.Rgb(0.54, 0.72, 0.44, 1.0)),
      new ValueTuple<string, string, Color>("M2,12 a10,10 0 1 0 20,0 a10,10 0 1 0 -20,0 Z M6.5,11 L13.8,11 L11,8.2 L12.4,6.8 L17.6,12 L12.4,17.2 L11,15.8 L13.8,13 L6.5,13 Z", null, DS.Rgb(0.96, 0.56, 0.4, 1.0)),
      new ValueTuple<string, string, Color>("M4,6 L17,6 L17,12.5 C17,15.5 14.5,18 11.5,18 L9.5,18 C6.5,18 4,15.5 4,12.5 Z M17,7.5 L18.5,7.5 C20.4,7.5 21.8,8.9 21.8,10.6 C21.8,12.4 20.4,13.8 18.5,13.8 L16.8,13.8 L17,12 L18.5,12 C19.3,12 19.9,11.4 19.9,10.6 C19.9,9.9 19.3,9.3 18.5,9.3 L17,9.3 Z M2.5,19.5 L18.5,19.5 C18.5,20.6 17.6,21.5 16.5,21.5 L4.5,21.5 C3.4,21.5 2.5,20.6 2.5,19.5 Z", null, DS.Rgb(0.74, 0.58, 0.44, 1.0)),
      new ValueTuple<string, string, Color>(SettingsContent.Voices[5].Item4, null, DS.Rgb(0.96, 0.6, 0.7, 1.0)),
      new ValueTuple<string, string, Color>("M12,3 L23,8.5 L12,14 L1,8.5 Z M5.5,11.5 L5.5,16 C7,18 9.4,19 12,19 C14.6,19 17,18 18.5,16 L18.5,11.5 L12,14.8 Z", "M21,9.5 L21,15", DS.Rgb(0.5, 0.62, 0.9, 1.0))
    };

    // Token: 0x0400037A RID: 890
    [TupleElementNames(new string[] { "Multiplier", "Label" })]
    [Nullable(new byte[] { 1, 0, 1 })]
    private static readonly ValueTuple<double, string>[] Speeds = new ValueTuple<double, string>[]
    {
      new ValueTuple<double, string>(0.5, "0.5x"),
      new ValueTuple<double, string>(0.75, "0.75x"),
      new ValueTuple<double, string>(1.0, "1x"),
      new ValueTuple<double, string>(1.25, "1.25x"),
      new ValueTuple<double, string>(1.5, "1.5x")
    };

    // Token: 0x0400037B RID: 891
    private IReadOnlyList<AudioDeviceInfo> captureDevices = Array.Empty<AudioDeviceInfo>();

    // Token: 0x0400037C RID: 892
    private IReadOnlyList<AudioDeviceInfo> renderDevices = Array.Empty<AudioDeviceInfo>();

    // Token: 0x0400037D RID: 893
    [Nullable(2)]
    private MediaPlayer voicePreview;

    // Token: 0x0400037E RID: 894
    private int voicePreviewRequest;

    // Token: 0x0400037F RID: 895
    private bool languageListOpen;

    // Token: 0x04000380 RID: 896
    private string voiceLanguageSearch = "";

    // Token: 0x04000381 RID: 897
    [Nullable(2)]
    private Grid languageListHost;

    // Token: 0x04000382 RID: 898
    private bool choosingVoiceLanguages;

    // Token: 0x04000383 RID: 899
    private bool voiceLanguageLimitReached;

    // Token: 0x04000384 RID: 900
    private readonly Dictionary<string, FrameworkElement> previewSpinners = new Dictionary<string, FrameworkElement>();

    // Token: 0x04000385 RID: 901
    [Nullable(2)]
    private DispatcherTimer previewSpinnerTimer;

    // Token: 0x04000386 RID: 902
    [Nullable(2)]
    private WasapiMicrophoneCapture micTest;

    // Token: 0x04000387 RID: 903
    [Nullable(2)]
    private DispatcherTimer micTestTimer;

    // Token: 0x04000388 RID: 904
    private double micTestLevel;

    // Token: 0x04000389 RID: 905
    private bool micTestHeard;

    // Token: 0x0400038A RID: 906
    private DateTime micTestStarted;

    // Token: 0x0400038B RID: 907
    private string micTestStatus = "Tap to test selected input";

    // Token: 0x0400038C RID: 908
    private Color micTestStatusColor;

    // Token: 0x0400038D RID: 909
    [Nullable(2)]
    private StackPanel micBars;

    // Token: 0x0400038E RID: 910
    [Nullable(2)]
    private TextBlock micStatusText;

    // Token: 0x0400038F RID: 911
    [Nullable(2)]
    private GelButton micTestButton;

    // Token: 0x04000390 RID: 912
    private bool audioDeviceChangesHooked;

    // Token: 0x04000391 RID: 913
    private bool audioDeviceUnloadHooked;

    // Token: 0x04000392 RID: 914
    private readonly SettingsHost host;

    // Token: 0x04000393 RID: 915
    private readonly AppServices s;

    // Token: 0x04000394 RID: 916
    private readonly IAppActions actions;

    // Token: 0x04000395 RID: 917
    private readonly StackPanel sidebarItems = new StackPanel
    {
      Spacing = 16.0
    };

    // Token: 0x04000396 RID: 918
    private readonly ColumnDefinition sidebarColumn = new ColumnDefinition
    {
      Width = new GridLength(228.0)
    };

    // Token: 0x04000397 RID: 919
    private readonly ContentControl content = new ContentControl
    {
      HorizontalContentAlignment = HorizontalAlignment.Stretch,
      VerticalContentAlignment = VerticalAlignment.Stretch,
      IsTabStop = false
    };

    // Token: 0x04000398 RID: 920
    private readonly ScrollViewer scroller;

    // Token: 0x04000399 RID: 921
    private readonly Grid overlayLayer = new Grid();

    // Token: 0x0400039A RID: 922
    private readonly Microsoft.UI.Dispatching.DispatcherQueue dq;

    // Token: 0x0400039B RID: 923
    private readonly MessagesPalette look = MessagesPalette.Current;

    // Token: 0x0400039C RID: 924
    private SettingsWindow.Page page;

    // Token: 0x0400039D RID: 925
    private int quietWrites;

    // Token: 0x0400039E RID: 926
    private bool isShown = true;

    // Token: 0x040003A0 RID: 928
    private bool subscribed;

    // Token: 0x040003A1 RID: 929
    private bool loadedBefore;

    // Token: 0x040003A2 RID: 930
    private readonly Grid sidebar;

    // Token: 0x040003A3 RID: 931
    private SettingsWindow.Page? lastBuiltPage;

    // Token: 0x040003A4 RID: 932
    private bool renderQueued;

    // Token: 0x040003A5 RID: 933
    [TupleElementNames(new string[] { "Row", "IconBox", "Text", "Glyph" })]
    [Nullable(new byte[] { 1, 0, 1, 1, 1, 1 })]
    private readonly Dictionary<SettingsWindow.Page, ValueTuple<Pressable, Grid, TextBlock, string>> sidebarRows = new Dictionary<SettingsWindow.Page, ValueTuple<Pressable, Grid, TextBlock, string>>();

    // Token: 0x040003A6 RID: 934
    private readonly Dictionary<SettingsWindow.Page, bool> sidebarRowSelected = new Dictionary<SettingsWindow.Page, bool>();

    // Token: 0x040003A7 RID: 935
    [Nullable(2)]
    private string sidebarSignature;

    // Token: 0x040003A8 RID: 936
    private static readonly Dictionary<string, BitmapImage> AvatarImages = new Dictionary<string, BitmapImage>();

    // Token: 0x040003A9 RID: 937
    private const string PersonFillPath = "M12,2.5 A4.75,4.75 0 1 1 12,12 A4.75,4.75 0 1 1 12,2.5 Z M4,20.2 C4,16.4 7.6,13.8 12,13.8 C16.4,13.8 20,16.4 20,20.2 C20,21 19.5,21.5 18.7,21.5 L5.3,21.5 C4.5,21.5 4,21 4,20.2 Z";

    // Token: 0x040003AA RID: 938
    private const string TextCursorPath = "M7.5,3 L16.5,3 L16.5,5 L13,5 L13,19 L16.5,19 L16.5,21 L7.5,21 L7.5,19 L11,19 L11,5 L7.5,5 Z";

    // Token: 0x040003AB RID: 939
    private const string CursorArrowPath = "M6,2.5 L6,19.5 L10.2,15.6 L13,21.8 L15.6,20.6 L12.9,14.6 L18.6,14.6 Z";

    // Token: 0x040003AC RID: 940
    private static readonly string HexGridPath = string.Concat(new ValueTuple<double, double>[]
    {
      new ValueTuple<double, double>(12.0, 12.0),
      new ValueTuple<double, double>(18.5, 12.0),
      new ValueTuple<double, double>(15.25, 17.63),
      new ValueTuple<double, double>(8.75, 17.63),
      new ValueTuple<double, double>(5.5, 12.0),
      new ValueTuple<double, double>(8.75, 6.37),
      new ValueTuple<double, double>(15.25, 6.37)
    }.Select<ValueTuple<double, double>, string>((ValueTuple<double, double> c) => FormattableString.Invariant(FormattableStringFactory.Create("M{0},{1} a2.5,2.5 0 1 0 5,0 a2.5,2.5 0 1 0 -5,0 Z ", new object[]
    {
      c.Item1 - 2.5,
      c.Item2
    }))));

    // Token: 0x040003AD RID: 941
    private const string CommandGlyph = "⌘";

    // Token: 0x040003AE RID: 942
    private bool billingHooked;

    // Token: 0x040003AF RID: 943
    [Nullable(2)]
    private string shownPlanSignature;

    // Token: 0x020004A8 RID: 1192
    [NullableContext(0)]
    private enum DeletionStep
    {
      // Token: 0x0400170E RID: 5902
      Reason,
      // Token: 0x0400170F RID: 5903
      TypeToConfirm,
      // Token: 0x04001710 RID: 5904
      Farewell
    }

    // Token: 0x020004A9 RID: 1193
    [Nullable(0)]
    private sealed class ConnectorDraft
    {
      // Token: 0x1700071A RID: 1818
      // (get) Token: 0x06002B4E RID: 11086 RVA: 0x00654134 File Offset: 0x00654134
      public bool UsesAdvanced
      {
        get
        {
          return !this.Remote || this.OAuthClientId.Trim().Length > 0 || this.Headers.Trim().Length > 0;
        }
      }

      // Token: 0x1700071B RID: 1819
      // (get) Token: 0x06002B4F RID: 11087 RVA: 0x00654168 File Offset: 0x00654168
      [TupleElementNames(new string[] { "Key", "Value" })]
      [Nullable(new byte[] { 1, 0, 1, 1 })]
      public List<ValueTuple<string, string>> ParsedHeaders
      {
        [return: TupleElementNames(new string[] { "Key", "Value" })]
        [return: Nullable(new byte[] { 1, 0, 1, 1 })]
        get
        {
          return SettingsContent.KeyValueLines(this.Headers, new char[] { ':', '=' });
        }
      }

      // Token: 0x1700071C RID: 1820
      // (get) Token: 0x06002B50 RID: 11088 RVA: 0x00654188 File Offset: 0x00654188
      [TupleElementNames(new string[] { "Key", "Value" })]
      [Nullable(new byte[] { 1, 0, 1, 1 })]
      public List<ValueTuple<string, string>> ParsedEnvironment
      {
        [return: TupleElementNames(new string[] { "Key", "Value" })]
        [return: Nullable(new byte[] { 1, 0, 1, 1 })]
        get
        {
          return SettingsContent.KeyValueLines(this.Environment, new char[] { '=' });
        }
      }

      // Token: 0x1700071D RID: 1821
      // (get) Token: 0x06002B51 RID: 11089 RVA: 0x006541A0 File Offset: 0x006541A0
      [Nullable(2)]
      public string ValidationProblem
      {
        [NullableContext(2)]
        get
        {
          if (this.Name.Trim().Length == 0)
          {
            return "Give the connector a name.";
          }
          if (this.Remote)
          {
            Uri uri;
            bool flag = !Uri.TryCreate(this.Url.Trim(), UriKind.Absolute, out uri);
            if (!flag)
            {
              string scheme = uri.Scheme;
              bool flag2 = scheme == "https" || scheme == "http";
              flag = !flag2;
            }
            if (flag || uri.Host.Length == 0)
            {
              return "Enter the server's full URL, starting with https://.";
            }
            if (this.Auth == "apiKey" && this.ApiKey.Trim().Length == 0)
            {
              return "Paste the API key, or pick a different authentication.";
            }
            if (uri.UserInfo.Length > 0)
            {
              return "Remove credentials from the URL and use the authentication fields.";
            }
            using (List<ValueTuple<string, string>>.Enumerator enumerator = this.ParsedHeaders.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                ValueTuple<string, string> valueTuple = enumerator.Current;
                string item = valueTuple.Item1;
                if (!CustomConnector.IsValidHeaderName(item))
                {
                  return "\"" + item + "\" is not a valid header name.";
                }
              }
              goto IL_017E;
            }
          }
          if (this.Command.Trim().Length == 0)
          {
            return "Enter the command that starts the server.";
          }
          foreach (ValueTuple<string, string> valueTuple2 in this.ParsedEnvironment)
          {
            string item2 = valueTuple2.Item1;
            if (!CustomConnector.IsValidEnvironmentVariableName(item2))
            {
              return "\"" + item2 + "\" is not a valid environment variable name.";
            }
          }
          IL_017E:
          return null;
        }
      }

      // Token: 0x1700071E RID: 1822
      // (get) Token: 0x06002B52 RID: 11090 RVA: 0x0065434C File Offset: 0x0065434C
      [Nullable(2)]
      public string AdvisoryNote
      {
        [NullableContext(2)]
        get
        {
          if (this.Remote)
          {
            if (this.Existing != null && !new CustomConnector("", this.Name, "remote", this.Url, null, Array.Empty<string>(), Array.Empty<string>(), false)
            {
              OAuthClientId = this.OAuthClientId
            }.HasSameAuthenticationTarget(this.Existing))
            {
              return "Changing the server or client ID clears its saved credentials. Save, then sign in again or add the new key.";
            }
            if (!this.Url.Trim().StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
              return null;
            }
            return "This server uses plain http, so its traffic is not encrypted.";
          }
          else
          {
            string text = this.Command.Trim();
            if (text.Length <= 0 || CommandResolver.IsResolvable(text))
            {
              return null;
            }
            return "\"" + text + "\" wasn't found on this PC. Use the full path, or install it first.";
          }
        }
      }

      // Token: 0x04001711 RID: 5905
      [Nullable(2)]
      public string EditingId;

      // Token: 0x04001712 RID: 5906
      [Nullable(2)]
      public CustomConnector Existing;

      // Token: 0x04001713 RID: 5907
      public string Name = "";

      // Token: 0x04001714 RID: 5908
      public bool Remote = true;

      // Token: 0x04001715 RID: 5909
      public string Url = "";

      // Token: 0x04001716 RID: 5910
      public string Command = "";

      // Token: 0x04001717 RID: 5911
      public string Arguments = "";

      // Token: 0x04001718 RID: 5912
      public string Auth = "browser";

      // Token: 0x04001719 RID: 5913
      public string ApiKey = "";

      // Token: 0x0400171A RID: 5914
      public string OAuthClientId = "";

      // Token: 0x0400171B RID: 5915
      public string Headers = "";

      // Token: 0x0400171C RID: 5916
      public string Environment = "";

      // Token: 0x0400171D RID: 5917
      public bool ShowAdvanced;

      // Token: 0x0400171E RID: 5918
      public bool AttemptedSave;

      // Token: 0x0400171F RID: 5919
      [Nullable(2)]
      public string Problem;
    }

    // Token: 0x020004AA RID: 1194
    [Nullable(0)]
    private sealed class Departure : IEquatable<SettingsContent.Departure>
    {
      // Token: 0x06002B54 RID: 11092 RVA: 0x00654480 File Offset: 0x00654480
      public Departure(string Id, string Name, Func<FrameworkElement> Logo, int Index, Guid Token)
      {
        this.Id = Id;
        this.Name = Name;
        this.Logo = Logo;
        this.Index = Index;
        this.Token = Token;
        base..ctor();
      }

      // Token: 0x1700071F RID: 1823
      // (get) Token: 0x06002B55 RID: 11093 RVA: 0x006544B0 File Offset: 0x006544B0
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(SettingsContent.Departure);
        }
      }

      // Token: 0x17000720 RID: 1824
      // (get) Token: 0x06002B56 RID: 11094 RVA: 0x006544BC File Offset: 0x006544BC
      // (set) Token: 0x06002B57 RID: 11095 RVA: 0x006544C4 File Offset: 0x006544C4
      public string Id { get; set; }

      // Token: 0x17000721 RID: 1825
      // (get) Token: 0x06002B58 RID: 11096 RVA: 0x006544D0 File Offset: 0x006544D0
      // (set) Token: 0x06002B59 RID: 11097 RVA: 0x006544D8 File Offset: 0x006544D8
      public string Name { get; set; }

      // Token: 0x17000722 RID: 1826
      // (get) Token: 0x06002B5A RID: 11098 RVA: 0x006544E4 File Offset: 0x006544E4
      // (set) Token: 0x06002B5B RID: 11099 RVA: 0x006544EC File Offset: 0x006544EC
      public Func<FrameworkElement> Logo { get; set; }

      // Token: 0x17000723 RID: 1827
      // (get) Token: 0x06002B5C RID: 11100 RVA: 0x006544F8 File Offset: 0x006544F8
      // (set) Token: 0x06002B5D RID: 11101 RVA: 0x00654500 File Offset: 0x00654500
      public int Index { get; set; }

      // Token: 0x17000724 RID: 1828
      // (get) Token: 0x06002B5E RID: 11102 RVA: 0x0065450C File Offset: 0x0065450C
      // (set) Token: 0x06002B5F RID: 11103 RVA: 0x00654514 File Offset: 0x00654514
      public Guid Token { get; set; }

      // Token: 0x06002B60 RID: 11104 RVA: 0x00654520 File Offset: 0x00654520
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Departure");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002B61 RID: 11105 RVA: 0x0065456C File Offset: 0x0065456C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Id = ");
        builder.Append(this.Id);
        builder.Append(", Name = ");
        builder.Append(this.Name);
        builder.Append(", Logo = ");
        builder.Append(this.Logo);
        builder.Append(", Index = ");
        builder.Append(this.Index.ToString());
        builder.Append(", Token = ");
        builder.Append(this.Token.ToString());
        return true;
      }

      // Token: 0x06002B62 RID: 11106 RVA: 0x00654618 File Offset: 0x00654618
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(SettingsContent.Departure left, SettingsContent.Departure right)
      {
        return !(left == right);
      }

      // Token: 0x06002B63 RID: 11107 RVA: 0x00654624 File Offset: 0x00654624
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(SettingsContent.Departure left, SettingsContent.Departure right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002B64 RID: 11108 RVA: 0x00654638 File Offset: 0x00654638
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<Func<FrameworkElement>>.Default.GetHashCode(this.<Logo>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Index>k__BackingField)) * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(this.<Token>k__BackingField);
      }

      // Token: 0x06002B65 RID: 11109 RVA: 0x006546C8 File Offset: 0x006546C8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as SettingsContent.Departure);
      }

      // Token: 0x06002B66 RID: 11110 RVA: 0x006546D8 File Offset: 0x006546D8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(SettingsContent.Departure other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<Func<FrameworkElement>>.Default.Equals(this.<Logo>k__BackingField, other.<Logo>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Index>k__BackingField, other.<Index>k__BackingField) && EqualityComparer<Guid>.Default.Equals(this.<Token>k__BackingField, other.<Token>k__BackingField));
      }

      // Token: 0x06002B68 RID: 11112 RVA: 0x00654788 File Offset: 0x00654788
      [CompilerGenerated]
      private Departure(SettingsContent.Departure original)
      {
        this.Id = original.<Id>k__BackingField;
        this.Name = original.<Name>k__BackingField;
        this.Logo = original.<Logo>k__BackingField;
        this.Index = original.<Index>k__BackingField;
        this.Token = original.<Token>k__BackingField;
      }

      // Token: 0x06002B69 RID: 11113 RVA: 0x006547D8 File Offset: 0x006547D8
      [CompilerGenerated]
      public void Deconstruct(out string Id, out string Name, out Func<FrameworkElement> Logo, out int Index, out Guid Token)
      {
        Id = this.Id;
        Name = this.Name;
        Logo = this.Logo;
        Index = this.Index;
        Token = this.Token;
      }
    }

    // Token: 0x020004AB RID: 1195
    [Nullable(0)]
    private sealed class RoutineSettingsRow : StackPanel
    {
      // Token: 0x06002B6A RID: 11114 RVA: 0x00654808 File Offset: 0x00654808
      public RoutineSettingsRow(SettingsContent owner, string id)
      {
        this.owner = owner;
        this.id = id;
        base.Spacing = 8.0;
        base.Padding = new Thickness(14.0, 11.0, 14.0, 11.0);
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 2.5,
          VerticalAlignment = VerticalAlignment.Center,
          Margin = new Thickness(0.0, 0.0, 24.0, 0.0),
          Children = { this.title, this.status }
        };
        Grid grid = new Grid
        {
          ColumnSpacing = 12.0
        };
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        Grid.SetColumn(this.trailing, 1);
        grid.Children.Add(stackPanel);
        grid.Children.Add(this.trailing);
        base.Children.Add(grid);
        this.descriptionText.LineHeight = 17.96;
        this.descriptionText.MaxLines = 2;
        this.descriptionHolder.Children.Add(this.descriptionText);
      }

      // Token: 0x17000725 RID: 1829
      // (get) Token: 0x06002B6B RID: 11115 RVA: 0x00654A30 File Offset: 0x00654A30
      [Nullable(2)]
      private RoutineScheduler Scheduler
      {
        [NullableContext(2)]
        get
        {
          AgentService agents = this.owner.host.Agents;
          if (agents == null)
          {
            return null;
          }
          return agents.Routines;
        }
      }

      // Token: 0x17000726 RID: 1830
      // (get) Token: 0x06002B6C RID: 11116 RVA: 0x00654A50 File Offset: 0x00654A50
      private bool IsRunning
      {
        get
        {
          if (!this.justStartedRun)
          {
            ClickyRoutine clickyRoutine = this.routine;
            return clickyRoutine != null && clickyRoutine.IsRunInFlight(DateTimeOffset.Now, RoutineScheduler.RunStaleAfter);
          }
          return true;
        }
      }

      // Token: 0x17000727 RID: 1831
      // (get) Token: 0x06002B6D RID: 11117 RVA: 0x00654A78 File Offset: 0x00654A78
      private string Description
      {
        get
        {
          ClickyRoutine clickyRoutine = this.routine;
          return ((clickyRoutine != null) ? clickyRoutine.TaskPrompt.Trim() : null) ?? "";
        }
      }

      // Token: 0x17000728 RID: 1832
      // (get) Token: 0x06002B6E RID: 11118 RVA: 0x00654A9C File Offset: 0x00654A9C
      private bool DescriptionOpens
      {
        get
        {
          return this.Description.Length > 140 || this.Description.Contains('\n');
        }
      }

      // Token: 0x06002B6F RID: 11119 RVA: 0x00654AC0 File Offset: 0x00654AC0
      public void Update(ClickyRoutine r)
      {
        if (r.LastRunStartedAt != this.runStartedSeen)
        {
          this.justStartedRun = false;
          this.runStartedSeen = r.LastRunStartedAt;
        }
        this.routine = r;
        this.Refresh(false);
      }

      // Token: 0x06002B70 RID: 11120 RVA: 0x00654B30 File Offset: 0x00654B30
      private void Refresh(bool animated)
      {
        using (MessagesPalette.Use(this.owner.look))
        {
          ClickyRoutine clickyRoutine = this.routine;
          if (clickyRoutine != null)
          {
            this.title.Text = ((clickyRoutine.Title.Length == 0) ? "Routine" : clickyRoutine.Title);
            this.title.Foreground = Ui.Brush(clickyRoutine.IsPaused ? SK.TextSecondary : SK.TextPrimary);
            this.status.Text = (this.confirmingDelete ? "Delete it? Its past runs stay in the chat." : this.StatusLine(clickyRoutine));
            bool flag = clickyRoutine.IsPausedAfterRepeatedFailures || (clickyRoutine.LastRunFailed && !clickyRoutine.IsPaused);
            this.status.Foreground = Ui.Brush(this.confirmingDelete ? SK.TextSecondary : (flag ? SK.Warning : SK.TextTertiary));
            ValueTuple<bool, bool, bool, string> valueTuple = new ValueTuple<bool, bool, bool, string>(this.confirmingDelete, this.IsRunning, clickyRoutine.IsPaused, clickyRoutine.Title);
            ValueTuple<bool, bool, bool, string>? valueTuple2 = this.trailingBuiltFor;
            ValueTuple<bool, bool, bool, string> valueTuple3 = valueTuple;
            bool flag2 = valueTuple2 != null;
            bool flag3;
            if (!flag2)
            {
              flag3 = true;
            }
            else if (!flag2)
            {
              flag3 = false;
            }
            else
            {
              ValueTuple<bool, bool, bool, string> valueOrDefault = valueTuple2.GetValueOrDefault();
              flag3 = valueOrDefault.Item1 != valueTuple3.Item1 || valueOrDefault.Item2 != valueTuple3.Item2 || valueOrDefault.Item3 != valueTuple3.Item3 || valueOrDefault.Item4 != valueTuple3.Item4;
            }
            if (flag3)
            {
              valueTuple2 = this.trailingBuiltFor;
              bool flag4;
              if (valueTuple2 != null)
              {
                ValueTuple<bool, bool, bool, string> valueOrDefault2 = valueTuple2.GetValueOrDefault();
                flag4 = valueOrDefault2.Item1 != this.confirmingDelete;
              }
              else
              {
                flag4 = false;
              }
              bool flag5 = flag4;
              this.trailingBuiltFor = new ValueTuple<bool, bool, bool, string>?(valueTuple);
              this.BuildTrailing(clickyRoutine);
              if (animated && flag5)
              {
                this.trailing.Opacity = 0.0;
                this.status.Opacity = 0.0;
                Anim.OpacitySpring(this.trailing, 1f, SettingsContent.RoutineSettingsRow.RowSpring);
                Anim.OpacitySpring(this.status, 1f, SettingsContent.RoutineSettingsRow.RowSpring);
              }
            }
            this.RefreshDescription();
          }
        }
      }

      // Token: 0x06002B71 RID: 11121 RVA: 0x00654D78 File Offset: 0x00654D78
      private void BuildTrailing(ClickyRoutine r)
      {
        this.trailing.Children.Clear();
        if (this.confirmingDelete)
        {
          this.trailing.Children.Add(SK.PillButton("Cancel", delegate
          {
            this.confirmingDelete = false;
            this.Refresh(true);
          }, false, false, false));
          this.trailing.Children.Add(SK.PillButton("Delete", delegate
          {
            this.confirmingDelete = false;
            RoutineScheduler scheduler = this.Scheduler;
            if (scheduler == null)
            {
              return;
            }
            scheduler.Delete(this.id, "settings");
          }, false, true, false));
          return;
        }
        bool isRunning = this.IsRunning;
        Pressable pressable = SK.PillButton(isRunning ? "Running…" : "Run now", delegate
        {
          if (!this.IsRunning)
          {
            RoutineScheduler scheduler2 = this.Scheduler;
            if (scheduler2 != null)
            {
              this.justStartedRun = true;
              this.Refresh(false);
              scheduler2.RunNowAsync(this.id).ContinueWith(delegate(Task<bool> t)
              {
                if (!t.IsCompletedSuccessfully || !t.Result)
                {
                  this.owner.dq.TryEnqueue(delegate
                  {
                    this.justStartedRun = false;
                    this.Refresh(false);
                  });
                }
              }, TaskScheduler.Default);
              return;
            }
          }
        }, false, false, false);
        pressable.Opacity = (isRunning ? 0.55 : 1.0);
        pressable.VerticalAlignment = VerticalAlignment.Center;
        this.trailing.Children.Add(pressable);
        this.trailing.Children.Add(this.DeleteButton(r));
        Pressable pressable2 = SK.Switch(!r.IsPaused, delegate(bool on)
        {
          RoutineScheduler scheduler3 = this.Scheduler;
          if (scheduler3 == null)
          {
            return;
          }
          scheduler3.SetPaused(this.id, !on, "settings");
        });
        pressable2.VerticalAlignment = VerticalAlignment.Center;
        pressable2.Tip(r.IsPaused ? "Turn this routine back on" : "Pause this routine");
        AutomationProperties.SetName(pressable2, r.IsPaused ? ("Turn " + r.Title + " back on") : ("Pause " + r.Title));
        this.trailing.Children.Add(pressable2);
      }

      // Token: 0x06002B72 RID: 11122 RVA: 0x00654EEC File Offset: 0x00654EEC
      private Pressable DeleteButton(ClickyRoutine r)
      {
        Pressable button = new Pressable(true)
        {
          Width = 26.0,
          Height = 26.0,
          CornerRadius = new CornerRadius(13.0),
          VerticalAlignment = VerticalAlignment.Center
        };
        TextBlock glyph = Icons.Glyph("\ue74d", 11.5, SK.TextTertiary);
        glyph.FontWeight = FontWeights.Medium;
        glyph.HorizontalAlignment = HorizontalAlignment.Center;
        glyph.VerticalAlignment = VerticalAlignment.Center;
        button.Children.Add(glyph);
        button.HoverChanged += delegate(bool hovered)
        {
          button.Background = Ui.Brush(this.owner.look.Ink.WithAlpha(hovered ? 0.1 : 0.0));
          glyph.Foreground = Ui.Brush(hovered ? SK.Destructive : this.owner.look.ThemeTextTertiary);
        };
        button.Click += delegate
        {
          this.confirmingDelete = true;
          this.Refresh(true);
        };
        button.Tip("Delete this routine");
        AutomationProperties.SetName(button, "Delete " + r.Title);
        return button;
      }

      // Token: 0x06002B73 RID: 11123 RVA: 0x00655008 File Offset: 0x00655008
      private void RefreshDescription()
      {
        string description = this.Description;
        if (description == this.descriptionBuiltFor)
        {
          return;
        }
        this.descriptionBuiltFor = description;
        if (this.descriptionButton != null)
        {
          base.Children.Remove(this.descriptionButton);
        }
        this.descriptionButton = null;
        this.moreLabel = (this.moreChevron = null);
        Panel panel = this.descriptionHolder.Parent as Panel;
        if (panel != null)
        {
          panel.Children.Remove(this.descriptionHolder);
        }
        this.StopDescriptionAnimation();
        if (description.Length == 0)
        {
          return;
        }
        bool descriptionOpens = this.DescriptionOpens;
        if (!descriptionOpens)
        {
          this.descriptionExpanded = false;
        }
        this.descriptionText.Text = description;
        this.descriptionText.MaxLines = (this.descriptionExpanded ? 0 : 2);
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 4.0,
          Children = { this.descriptionHolder }
        };
        if (descriptionOpens)
        {
          this.moreLabel = Ui.Text(this.descriptionExpanded ? "Show less" : "Show more", 10.5, FontWeights.SemiBold, SK.TextTertiary, TextAlignment.Left, 0, false, false);
          this.moreChevron = Icons.Glyph("\ue70d", 10.5, SK.TextTertiary);
          this.moreChevron.FontWeight = FontWeights.SemiBold;
          this.moreChevron.Rotation = (float)(this.descriptionExpanded ? 180 : 0);
          TextBlock chevron = this.moreChevron;
          chevron.SizeChanged += delegate(object _, SizeChangedEventArgs _)
          {
            chevron.CenterPoint = new Vector3((float)chevron.ActualWidth / 2f, (float)chevron.ActualHeight / 2f, 0f);
          };
          TextBlock[] array = new TextBlock[] { this.moreLabel, this.moreChevron };
          for (int i = 0; i < array.Length; i++)
          {
            array[i].VerticalAlignment = VerticalAlignment.Center;
          }
          stackPanel.Children.Add(new StackPanel
          {
            Orientation = Orientation.Horizontal,
            Spacing = 3.0,
            Children = { this.moreLabel, this.moreChevron }
          });
          this.ColorShowMore();
        }
        Pressable pressable = new Pressable(descriptionOpens)
        {
          HorizontalAlignment = HorizontalAlignment.Stretch
        };
        pressable.Children.Add(stackPanel);
        if (descriptionOpens)
        {
          pressable.HoverChanged += delegate(bool hovered)
          {
            this.descriptionHovered = hovered;
            this.ColorShowMore();
          };
          pressable.Click += this.ToggleDescription;
        }
        this.descriptionButton = pressable;
        base.Children.Add(pressable);
      }

      // Token: 0x06002B74 RID: 11124 RVA: 0x00655288 File Offset: 0x00655288
      private void ColorShowMore()
      {
        SolidColorBrush solidColorBrush = Ui.Brush(this.descriptionHovered ? this.owner.look.ThemeTextSecondary : this.owner.look.ThemeTextTertiary);
        if (this.moreLabel != null)
        {
          this.moreLabel.Foreground = solidColorBrush;
        }
        if (this.moreChevron != null)
        {
          this.moreChevron.Foreground = solidColorBrush;
        }
      }

      // Token: 0x06002B75 RID: 11125 RVA: 0x006552F0 File Offset: 0x006552F0
      private void ToggleDescription()
      {
        if (!this.DescriptionOpens || this.moreLabel == null || this.moreChevron == null)
        {
          return;
        }
        double actualWidth = this.descriptionText.ActualWidth;
        double actualHeight = this.descriptionHolder.ActualHeight;
        this.descriptionExpanded = !this.descriptionExpanded;
        this.moreLabel.Text = (this.descriptionExpanded ? "Show less" : "Show more");
        float num = (float)(this.descriptionExpanded ? 0 : 180);
        float num2 = (float)(this.descriptionExpanded ? 180 : 0);
        this.moreChevron.Rotation = num2;
        if (!Anim.Instant && !Anim.ReduceMotion)
        {
          ScalarKeyFrameAnimation scalarKeyFrameAnimation = SpringCurve.Scalar(Ui.Compositor, SettingsContent.RoutineSettingsRow.RowSpring, num, num2);
          scalarKeyFrameAnimation.Target = "Rotation";
          this.moreChevron.StartAnimation(scalarKeyFrameAnimation);
        }
        double num3 = this.TextHeight(0, actualWidth);
        double num4 = (this.descriptionExpanded ? num3 : this.TextHeight(2, actualWidth));
        if (Anim.Instant || Anim.ReduceMotion || actualWidth <= 0.0 || actualHeight <= 0.0)
        {
          this.StopDescriptionAnimation();
          this.descriptionText.MaxLines = (this.descriptionExpanded ? 0 : 2);
          return;
        }
        this.descriptionText.MaxLines = 0;
        this.descriptionText.Height = num3;
        if (this.descriptionHeight == null)
        {
          this.descriptionHeight = new SpringValue(actualHeight, 0.25);
        }
        if (!this.descriptionTicking)
        {
          this.descriptionHeight.To(Ui.Compositor, actualHeight, null);
        }
        this.descriptionHeight.To(Ui.Compositor, num4, new SwiftSpring?(SettingsContent.RoutineSettingsRow.RowSpring));
        bool expanded = this.descriptionExpanded;
        this.descriptionSettled = delegate
        {
          this.descriptionText.MaxLines = (expanded ? 0 : 2);
          this.descriptionText.Height = double.NaN;
        };
        if (this.descriptionTicking)
        {
          return;
        }
        this.descriptionTicking = true;
        CompositionTarget.Rendering += this.OnDescriptionFrame;
      }

      // Token: 0x06002B76 RID: 11126 RVA: 0x006554F0 File Offset: 0x006554F0
      private void OnDescriptionFrame([Nullable(2)] object sender, object e)
      {
        SpringValue springValue = this.descriptionHeight;
        if (springValue == null)
        {
          this.StopDescriptionAnimation();
          return;
        }
        double num = Math.Max(0.0, springValue.ValueAt(SpringCurve.Now));
        this.descriptionHolder.Height = num;
        this.descriptionHolder.Clip = new RectangleGeometry
        {
          Rect = new Rect(0.0, 0.0, Math.Max(0.0, this.descriptionHolder.ActualWidth), num)
        };
        if (num == springValue.Target)
        {
          this.StopDescriptionAnimation();
        }
      }

      // Token: 0x06002B77 RID: 11127 RVA: 0x0065558C File Offset: 0x0065558C
      private void StopDescriptionAnimation()
      {
        if (this.descriptionTicking)
        {
          CompositionTarget.Rendering -= this.OnDescriptionFrame;
        }
        this.descriptionTicking = false;
        this.descriptionHeight = null;
        this.descriptionHolder.Height = double.NaN;
        this.descriptionHolder.Clip = null;
        Action action = this.descriptionSettled;
        this.descriptionSettled = null;
        if (action == null)
        {
          return;
        }
        action();
      }

      // Token: 0x06002B78 RID: 11128 RVA: 0x006555F8 File Offset: 0x006555F8
      private double TextHeight(int maxLines, double width)
      {
        TextBlock textBlock = Ui.Text(this.descriptionText.Text, 12.0, FontWeights.Normal, SK.TextSecondary, TextAlignment.Left, 0, true, false);
        textBlock.LineHeight = 17.96;
        textBlock.MaxLines = maxLines;
        textBlock.Measure(new Size(Math.Max(1.0, width), double.PositiveInfinity));
        return textBlock.DesiredSize.Height;
      }

      // Token: 0x06002B79 RID: 11129 RVA: 0x00655674 File Offset: 0x00655674
      private static string CadenceWords(int minutes)
      {
        if (minutes != 10080)
        {
          return ClickyRoutine.CadenceLabelFor(minutes);
        }
        return "Every week";
      }

      // Token: 0x06002B7A RID: 11130 RVA: 0x0065568C File Offset: 0x0065568C
      private static string NextRunWords(DateTimeOffset at)
      {
        DateTimeOffset dateTimeOffset = at.ToLocalTime();
        CultureInfo currentCulture = CultureInfo.CurrentCulture;
        string text = dateTimeOffset.ToString(currentCulture.DateTimeFormat.ShortTimePattern, currentCulture);
        if (dateTimeOffset.Date == DateTime.Today)
        {
          return "today " + text;
        }
        if (dateTimeOffset.Date == DateTime.Today.AddDays(1.0))
        {
          return "tomorrow " + text;
        }
        return currentCulture.DateTimeFormat.GetAbbreviatedDayName(dateTimeOffset.DayOfWeek) + " " + text;
      }

      // Token: 0x06002B7B RID: 11131 RVA: 0x00655728 File Offset: 0x00655728
      private string StatusLine(ClickyRoutine r)
      {
        List<string> list = new List<string> { r.HasSetTime ? r.ScheduleLabel : SettingsContent.RoutineSettingsRow.CadenceWords(r.IntervalMinutes) };
        if (r.IsPausedAfterRepeatedFailures)
        {
          List<string> list2 = list;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 1);
          defaultInterpolatedStringHandler.AppendLiteral("Paused after ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(r.ConsecutiveFailedRunCount);
          defaultInterpolatedStringHandler.AppendLiteral(" failed runs");
          list2.Add(defaultInterpolatedStringHandler.ToStringAndClear());
        }
        else if (r.IsPaused)
        {
          list.Add("Paused");
        }
        else if (this.IsRunning)
        {
          list.Add("Running now");
        }
        else if (r.PendingRetryAt != null)
        {
          list.Add("Retrying " + SettingsContent.RoutineSettingsRow.NextRunWords(r.NextRunAt));
        }
        else if (r.LastRunFailed)
        {
          list.Add("Last run failed");
        }
        else
        {
          list.Add("Next " + SettingsContent.RoutineSettingsRow.NextRunWords(r.NextRunAt));
        }
        if (r.RunCount > 0)
        {
          List<string> list3 = list;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
          defaultInterpolatedStringHandler.AppendLiteral("Ran ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(r.RunCount);
          defaultInterpolatedStringHandler.AppendLiteral("×");
          list3.Add(defaultInterpolatedStringHandler.ToStringAndClear());
        }
        return string.Join(" · ", list);
      }

      // Token: 0x04001725 RID: 5925
      private static readonly SwiftSpring RowSpring = new SwiftSpring(0.3, 0.86);

      // Token: 0x04001726 RID: 5926
      private const double DescriptionLineHeight = 17.96;

      // Token: 0x04001727 RID: 5927
      private readonly SettingsContent owner;

      // Token: 0x04001728 RID: 5928
      private readonly string id;

      // Token: 0x04001729 RID: 5929
      private readonly TextBlock title = Ui.Text("", 13.0, FontWeights.Medium, SK.TextPrimary, TextAlignment.Left, 1, false, false);

      // Token: 0x0400172A RID: 5930
      private readonly TextBlock status = Ui.Text("", 11.0, FontWeights.Normal, SK.TextTertiary, TextAlignment.Left, 1, false, false);

      // Token: 0x0400172B RID: 5931
      private readonly StackPanel trailing = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 12.0,
        VerticalAlignment = VerticalAlignment.Center
      };

      // Token: 0x0400172C RID: 5932
      [Nullable(2)]
      private ClickyRoutine routine;

      // Token: 0x0400172D RID: 5933
      private bool confirmingDelete;

      // Token: 0x0400172E RID: 5934
      private bool justStartedRun;

      // Token: 0x0400172F RID: 5935
      private bool descriptionExpanded;

      // Token: 0x04001730 RID: 5936
      private bool descriptionHovered;

      // Token: 0x04001731 RID: 5937
      private DateTimeOffset? runStartedSeen;

      // Token: 0x04001732 RID: 5938
      [TupleElementNames(new string[] { "Confirming", "Running", "Paused", "Title" })]
      [Nullable(new byte[] { 0, 1 })]
      private ValueTuple<bool, bool, bool, string>? trailingBuiltFor;

      // Token: 0x04001733 RID: 5939
      [Nullable(2)]
      private string descriptionBuiltFor;

      // Token: 0x04001734 RID: 5940
      [Nullable(2)]
      private Pressable descriptionButton;

      // Token: 0x04001735 RID: 5941
      private readonly Grid descriptionHolder = new Grid
      {
        VerticalAlignment = VerticalAlignment.Top
      };

      // Token: 0x04001736 RID: 5942
      private readonly TextBlock descriptionText = Ui.Text("", 12.0, FontWeights.Normal, SK.TextSecondary, TextAlignment.Left, 0, true, false);

      // Token: 0x04001737 RID: 5943
      [Nullable(2)]
      private TextBlock moreLabel;

      // Token: 0x04001738 RID: 5944
      [Nullable(2)]
      private TextBlock moreChevron;

      // Token: 0x04001739 RID: 5945
      [Nullable(2)]
      private SpringValue descriptionHeight;

      // Token: 0x0400173A RID: 5946
      [Nullable(2)]
      private Action descriptionSettled;

      // Token: 0x0400173B RID: 5947
      private bool descriptionTicking;
    }
  }
}
