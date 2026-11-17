using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000153 RID: 339
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class TutorialInviteSurface : NotchSurface
  {
    // Token: 0x060012E3 RID: 4835 RVA: 0x0059AEA4 File Offset: 0x0059AEA4
    public TutorialInviteSurface(TutorialInviteSurface.Mode mode, bool showsContinue = true)
    {
      TutorialInviteSurface <>4__this = this;
      ValueTuple<string, string, string, string, bool> valueTuple;
      if (mode == TutorialInviteSurface.Mode.Invite)
      {
        valueTuple = new ValueTuple<string, string, string, string, bool>("Okay, let me show you around", "I'm sorry my CEO yaps so much.", "Show me", "\uf5b0", true);
      }
      else
      {
        valueTuple = new ValueTuple<string, string, string, string, bool>("Quick tour in progress", "Finish it and I'm all yours", "Continue", "\uf5b0", true);
      }
      ValueTuple<string, string, string, string, bool> valueTuple2 = valueTuple;
      string item = valueTuple2.Item1;
      string item2 = valueTuple2.Item2;
      string item3 = valueTuple2.Item3;
      string item4 = valueTuple2.Item4;
      bool item5 = valueTuple2.Item5;
      TextBlock textBlock = Ui.Text(item, 14.0, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Center, 0, true, false);
      TextBlock textBlock2 = Ui.Text(item2, 12.0, FontWeights.Normal, DS.Colors.TextSecondary, TextAlignment.Center, 0, true, false);
      this.view = new StackPanel
      {
        Spacing = 12.0,
        Margin = new Thickness(40.0, 34.0, 40.0, 0.0),
        HorizontalAlignment = HorizontalAlignment.Stretch
      };
      this.view.Children.Add(new StackPanel
      {
        Spacing = 5.0,
        Children = { textBlock, textBlock2 }
      });
      if (showsContinue)
      {
        GelButton gelButton = new GelButton(item3, GelAppearance.PrimaryBlue, item5 ? GelButton.Sizing.Large : GelButton.Sizing.Normal, 100.0, item4, null, null)
        {
          HorizontalAlignment = HorizontalAlignment.Center,
          Margin = new Thickness(0.0, 6.0, 0.0, 0.0)
        };
        gelButton.Click += delegate
        {
          INotchCardActions notchCardActions = <>4__this.Host.Actions as INotchCardActions;
          if (mode == TutorialInviteSurface.Mode.Invite)
          {
            <>4__this.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.OnboardingTutorialInvite));
            <>4__this.Host.Actions.StartTutorial();
            return;
          }
          if (notchCardActions != null)
          {
            notchCardActions.ResumeTutorial();
            return;
          }
          <>4__this.Host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.OnboardingTutorialResume));
          <>4__this.Host.Actions.StartTutorial();
        };
        this.view.Children.Add(gelButton);
      }
    }

    // Token: 0x1700036C RID: 876
    // (get) Token: 0x060012E4 RID: 4836 RVA: 0x0059B090 File Offset: 0x0059B090
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x040008B0 RID: 2224
    private readonly StackPanel view;

    // Token: 0x020006B4 RID: 1716
    [NullableContext(0)]
    public enum Mode
    {
      // Token: 0x04001E76 RID: 7798
      Invite,
      // Token: 0x04001E77 RID: 7799
      Resume
    }
  }
}
