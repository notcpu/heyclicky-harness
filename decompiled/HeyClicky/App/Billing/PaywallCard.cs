using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Telemetry;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Billing
{
  // Token: 0x02000365 RID: 869
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PaywallCard : StackPanel
  {
    // Token: 0x060025F5 RID: 9717 RVA: 0x0063D6B8 File Offset: 0x0063D6B8
    public PaywallCard(PaywallPayload payload, PaywallSource source, PaywallPalette palette, string currentTier, [Nullable(2)] ReferralPaywallDiscount referral, IPaywallCardHost host)
    {
      this.payload = payload;
      this.source = source;
      this.pal = palette;
      this.currentTier = currentTier;
      this.host = host;
      this.referral = referral;
      this.selectedInterval = payload.DefaultInterval ?? "month";
      base.Orientation = Orientation.Vertical;
      base.Spacing = 22.0;
      this.Build();
    }

    // Token: 0x170006BF RID: 1727
    // (get) Token: 0x060025F6 RID: 9718 RVA: 0x0063D734 File Offset: 0x0063D734
    private bool OffersFreeOption
    {
      get
      {
        return this.source.OffersFreeOption();
      }
    }

    // Token: 0x170006C0 RID: 1728
    // (get) Token: 0x060025F7 RID: 9719 RVA: 0x0063D744 File Offset: 0x0063D744
    [Nullable(2)]
    private ReferralPaywallDiscount ReferralDiscount
    {
      [NullableContext(2)]
      get
      {
        if (!this.payload.IsPaymentProblem)
        {
          IReadOnlyList<PaywallPlanOption> plans = this.payload.Plans;
          if (plans != null && plans.Count > 0 && this.currentTier == "free")
          {
            return this.referral;
          }
        }
        return null;
      }
    }

    // Token: 0x060025F8 RID: 9720 RVA: 0x0063D790 File Offset: 0x0063D790
    [NullableContext(2)]
    public void SetReferral(ReferralPaywallDiscount discount)
    {
      if (discount == this.referral)
      {
        return;
      }
      this.referral = discount;
      this.Build();
      this.host.ContentResized();
    }

    // Token: 0x170006C1 RID: 1729
    // (get) Token: 0x060025F9 RID: 9721 RVA: 0x0063D7BC File Offset: 0x0063D7BC
    [Nullable(2)]
    public string LaunchErrorText
    {
      [NullableContext(2)]
      get
      {
        return this.launchErrorText;
      }
    }

    // Token: 0x060025FA RID: 9722 RVA: 0x0063D7C4 File Offset: 0x0063D7C4
    private void Build()
    {
      base.Children.Clear();
      this.planButtons.Clear();
      base.Children.Add(this.Header());
      ReferralPaywallDiscount referralDiscount = this.ReferralDiscount;
      if (referralDiscount != null)
      {
        base.Children.Add(this.ReferralBanner(referralDiscount));
      }
      if (this.ShouldShowEyebrow)
      {
        TextBlock textBlock = Ui.Text(this.EyebrowText, 14.0, FontWeights.SemiBold, this.pal.Secondary, TextAlignment.Left, 0, true, false);
        base.Children.Add(textBlock);
      }
      foreach (FrameworkElement frameworkElement in this.ContentSection())
      {
        base.Children.Add(frameworkElement);
      }
      string text = this.launchErrorText;
      if (text != null)
      {
        base.Children.Add(this.ErrorRow(text));
      }
    }

    // Token: 0x060025FB RID: 9723 RVA: 0x0063D8B4 File Offset: 0x0063D8B4
    private FrameworkElement Header()
    {
      Grid grid = new Grid
      {
        Width = 44.0,
        Height = 34.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      grid.Children.Add(new BrandMark(44.0, this.pal.Primary, 1.0)
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      });
      TextBlock textBlock = Ui.Text(this.OffersFreeOption ? "Choose your HeyClicky plan" : this.payload.Title, 19.0, FontWeights.Bold, this.pal.Primary, TextAlignment.Left, 2, true, false);
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      Grid grid2 = new Grid();
      grid2.ColumnSpacing = 12.0;
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid2.Children.Add(grid);
      Grid.SetColumn(textBlock, 1);
      grid2.Children.Add(textBlock);
      GelIconButton gelIconButton = new GelIconButton("\ue711", GelAppearance.SecondaryOutline, 9.0, 24.0, "Close")
      {
        VerticalAlignment = VerticalAlignment.Center,
        Margin = new Thickness(8.0, 0.0, 0.0, 0.0)
      };
      if (!this.pal.IsDark)
      {
        TextBlock textBlock2 = gelIconButton.Children.OfType<TextBlock>().FirstOrDefault<TextBlock>();
        if (textBlock2 != null)
        {
          textBlock2.Foreground = Ui.Brush(DS.Black(0.55));
        }
      }
      gelIconButton.Click += delegate
      {
        ClickyAnalytics.TrackPaywallDismissed(this.source.WireName());
        this.host.Close(PaywallCloseKind.Dismissed);
      };
      Grid.SetColumn(gelIconButton, 2);
      grid2.Children.Add(gelIconButton);
      return grid2;
    }

    // Token: 0x060025FC RID: 9724 RVA: 0x0063DAB4 File Offset: 0x0063DAB4
    private FrameworkElement ReferralBanner(ReferralPaywallDiscount d)
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0
      };
      stackPanel.Children.Add(Icons.PathIcon("M3,8 h18 v4 h-18 z M4,13 h7 v8 h-7 z M13,13 h7 v8 h-7 z M11,8 h2 v13 h-2 z M12,7.5 C10.5,4 7.5,3 6.6,4.6 C5.7,6.2 8,7.6 12,7.5 Z M12,7.5 C13.5,4 16.5,3 17.4,4.6 C18.3,6.2 16,7.6 12,7.5 Z", 12.0, this.pal.Accent));
      TextBlock textBlock = Ui.Text("", 12.5, FontWeights.Medium, this.pal.Primary, TextAlignment.Left, 0, false, false);
      textBlock.Inlines.Add(new Run
      {
        Text = "You're getting "
      });
      textBlock.Inlines.Add(new Run
      {
        Text = d.PercentText + " off",
        FontWeight = FontWeights.Bold
      });
      textBlock.Inlines.Add(new Run
      {
        Text = " " + d.PeriodText + " from"
      });
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      stackPanel.Children.Add(textBlock);
      string referrerFirstName = d.ReferrerFirstName;
      if (referrerFirstName != null)
      {
        Grid grid = new Grid
        {
          Width = 20.0,
          Height = 20.0,
          VerticalAlignment = VerticalAlignment.Center
        };
        grid.Children.Add(new Ellipse
        {
          Fill = Ui.Brush(this.pal.Accent)
        });
        grid.Children.Add(Ui.Text(referrerFirstName.Substring(0, 1).ToUpperInvariant(), 10.0, FontWeights.Bold, Colors.White, TextAlignment.Left, 0, false, false).Center<TextBlock>());
        string referrerAvatarUrl = d.ReferrerAvatarUrl;
        if (referrerAvatarUrl != null)
        {
          Ellipse ellipse = new Ellipse
          {
            Fill = new ImageBrush
            {
              ImageSource = new BitmapImage(new Uri(referrerAvatarUrl)),
              Stretch = Stretch.UniformToFill
            }
          };
          grid.Children.Add(ellipse);
        }
        grid.Children.Add(new Ellipse
        {
          Stroke = Ui.Brush(DS.White(0.8)),
          StrokeThickness = 1.0
        });
        TextBlock textBlock2 = Ui.Text("", 12.5, FontWeights.Medium, this.pal.Primary, TextAlignment.Left, 0, false, false);
        textBlock2.Inlines.Add(new Run
        {
          Text = referrerFirstName,
          FontWeight = FontWeights.Bold
        });
        textBlock2.Inlines.Add(new Run
        {
          Text = "'s invite"
        });
        textBlock2.VerticalAlignment = VerticalAlignment.Center;
        stackPanel.Children.Add(new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 5.0,
          Children = { grid, textBlock2 }
        });
      }
      else
      {
        TextBlock textBlock3 = Ui.Text("a friend's invite", 12.5, FontWeights.Medium, this.pal.Primary, TextAlignment.Left, 0, false, false);
        textBlock3.VerticalAlignment = VerticalAlignment.Center;
        stackPanel.Children.Add(textBlock3);
      }
      return new Border
      {
        Child = stackPanel,
        Padding = new Thickness(12.0, 9.0, 12.0, 9.0),
        CornerRadius = new CornerRadius(12.0),
        Background = Ui.Brush(this.pal.Accent.WithAlpha(0.1)),
        BorderBrush = Ui.Brush(this.pal.Accent.WithAlpha(0.25)),
        BorderThickness = new Thickness(0.5)
      };
    }

    // Token: 0x170006C2 RID: 1730
    // (get) Token: 0x060025FD RID: 9725 RVA: 0x0063DE68 File Offset: 0x0063DE68
    private bool ShouldShowEyebrow
    {
      get
      {
        if (!this.payload.IsPaymentProblem)
        {
          IReadOnlyList<PaywallPlanOption> plans = this.payload.Plans;
          return plans != null && plans.Count == 0;
        }
        return true;
      }
    }

    // Token: 0x170006C3 RID: 1731
    // (get) Token: 0x060025FE RID: 9726 RVA: 0x0063DEA0 File Offset: 0x0063DEA0
    private string EyebrowText
    {
      get
      {
        if (this.payload.IsPaymentProblem)
        {
          return "Update your payment method to keep HeyClicky running.";
        }
        IReadOnlyList<PaywallPlanOption> plans = this.payload.Plans;
        if (plans == null)
        {
          return "Upgrade to HeyClicky Pro to keep it going!";
        }
        if (plans.Count == 0)
        {
          return "You're on our biggest plan, HeyClicky Max.";
        }
        if (plans.Count == 1 && plans[0].Key == "max")
        {
          return "Upgrade to HeyClicky Max to keep it going!";
        }
        return "Upgrade HeyClicky to keep it going!";
      }
    }

    // Token: 0x060025FF RID: 9727 RVA: 0x0063DF10 File Offset: 0x0063DF10
    private IEnumerable<FrameworkElement> ContentSection()
    {
      PaywallCard.<ContentSection>d__27 <ContentSection>d__ = new PaywallCard.<ContentSection>d__27(-2);
      <ContentSection>d__.<>4__this = this;
      return <ContentSection>d__;
    }

    // Token: 0x06002600 RID: 9728 RVA: 0x0063DF20 File Offset: 0x0063DF20
    private FrameworkElement PlansRow(IReadOnlyList<PaywallPlanOption> plans)
    {
      Grid grid = new Grid
      {
        ColumnSpacing = 12.0
      };
      List<FrameworkElement> list = new List<FrameworkElement>();
      if (this.OffersFreeOption)
      {
        list.Add(this.FreeOptionCard());
      }
      foreach (PaywallPlanOption paywallPlanOption in plans)
      {
        list.Add(this.PlanOptionCard(paywallPlanOption));
      }
      for (int i = 0; i < list.Count; i++)
      {
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = new GridLength(1.0, GridUnitType.Star)
        });
        Grid.SetColumn(list[i], i);
        grid.Children.Add(list[i]);
      }
      return grid;
    }

    // Token: 0x06002601 RID: 9729 RVA: 0x0063DFFC File Offset: 0x0063DFFC
    private Border CardFrame(FrameworkElement top, FrameworkElement cta)
    {
      Grid grid = new Grid();
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star),
        MinHeight = 24.0
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.Children.Add(top);
      Grid.SetRow(cta, 2);
      grid.Children.Add(cta);
      return new Border
      {
        Child = grid,
        Padding = new Thickness(18.0),
        CornerRadius = new CornerRadius(16.0),
        Background = Ui.Brush(this.pal.BenefitSurface),
        BorderBrush = Ui.Brush(this.pal.BenefitBorder),
        BorderThickness = new Thickness(0.5),
        VerticalAlignment = VerticalAlignment.Stretch
      };
    }

    // Token: 0x06002602 RID: 9730 RVA: 0x0063E114 File Offset: 0x0063E114
    private FrameworkElement Rule()
    {
      return new Border
      {
        Height = 1.0,
        Background = Ui.Brush(this.pal.BenefitBorder.WithAlpha(0.4)),
        HorizontalAlignment = HorizontalAlignment.Stretch
      };
    }

    // Token: 0x06002603 RID: 9731 RVA: 0x0063E160 File Offset: 0x0063E160
    private FrameworkElement Description(string text)
    {
      TextBlock textBlock = Ui.Text(text, 11.5, FontWeights.Normal, this.pal.Secondary, TextAlignment.Left, 0, true, false);
      textBlock.MinHeight = 30.590000000000003;
      return textBlock;
    }

    // Token: 0x06002604 RID: 9732 RVA: 0x0063E194 File Offset: 0x0063E194
    private FrameworkElement FreeOptionCard()
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 16.0
      };
      stackPanel.Children.Add(Ui.Text("Free", 16.0, FontWeights.SemiBold, this.pal.Primary, TextAlignment.Left, 0, false, false));
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 2.0
      };
      stackPanel2.Children.Add(Ui.Text("$0", 30.0, FontWeights.Bold, this.pal.Primary, TextAlignment.Left, 0, false, false));
      if (this.payload.SupportsYearly || this.ReferralDiscount != null)
      {
        stackPanel2.Children.Add(this.Caption("No credit card needed", this.pal.Secondary));
      }
      stackPanel.Children.Add(stackPanel2);
      stackPanel.Children.Add(this.Description("Get to know HeyClicky. Upgrade whenever you are ready."));
      stackPanel.Children.Add(this.Rule());
      stackPanel.Children.Add(this.Features(new ValueTuple<string, string>[]
      {
        new ValueTuple<string, string>("25 talk messages a month", "Includes your free monthly message allowance."),
        new ValueTuple<string, string>("plenty of dictation", "Includes your free monthly dictation allowance."),
        new ValueTuple<string, string>("25 agent messages a month", "Includes your free agent message allowance. Upgrade for more.")
      }));
      GelButton gelButton = new GelButton("Use HeyClicky for free", GelAppearance.PrimaryBlue, GelButton.Sizing.Large, 100.0, null, null, null)
      {
        HorizontalAlignment = HorizontalAlignment.Stretch
      };
      gelButton.Click += delegate
      {
        if (this.launchingPlanKey != null)
        {
          return;
        }
        ClickyAnalytics.TrackPaywallFreeSelected(this.source.WireName());
        this.host.Close(PaywallCloseKind.FreeChosen);
      };
      this.planButtons.Add(gelButton);
      return this.CardFrame(stackPanel, gelButton);
    }

    // Token: 0x06002605 RID: 9733 RVA: 0x0063E348 File Offset: 0x0063E348
    private FrameworkElement PlanOptionCard(PaywallPlanOption plan)
    {
      bool flag = this.selectedInterval == "year" && plan.SupportsYearly;
      ValueTuple<string, string> valueTuple = PaywallCard.SplitPriceDisplay(flag ? (plan.YearlyPriceDisplay ?? plan.PriceDisplay) : plan.PriceDisplay);
      string item = valueTuple.Item1;
      string item2 = valueTuple.Item2;
      ReferralPaywallDiscount referralDiscount = this.ReferralDiscount;
      string text = ((referralDiscount != null && (!flag || referralDiscount.AppliesToYearlyPlans)) ? referralDiscount.DiscountedAmountText(item) : null);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      string text3;
      if (text != null && referralDiscount != null)
      {
        string text2;
        if (!flag)
        {
          if (referralDiscount.Months != 1)
          {
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 1);
            defaultInterpolatedStringHandler.AppendLiteral("First ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(referralDiscount.Months);
            defaultInterpolatedStringHandler.AppendLiteral(" months");
            text2 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            text2 = "First month";
          }
        }
        else
        {
          text2 = "First year";
        }
        text3 = text2 + ", then " + item + item2;
      }
      else
      {
        text3 = null;
      }
      string text4 = text3;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 16.0
      };
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 6.0
      };
      stackPanel2.Children.Add(Ui.Text((plan.Key == "pro") ? "Pro" : "Max", 16.0, FontWeights.SemiBold, this.pal.Primary, TextAlignment.Left, 0, false, false));
      if (plan.Recommended)
      {
        stackPanel2.Children.Add(this.PopularBadge());
      }
      stackPanel.Children.Add(stackPanel2);
      StackPanel stackPanel3 = new StackPanel
      {
        Spacing = 2.0
      };
      StackPanel stackPanel4 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 3.0
      };
      if (text != null)
      {
        TextBlock textBlock = Ui.Text(item, 17.0, FontWeights.SemiBold, this.pal.Secondary, TextAlignment.Left, 0, false, false);
        textBlock.TextDecorations = TextDecorations.Strikethrough;
        textBlock.Margin = new Thickness(0.0, 0.0, 3.0, 0.0);
        textBlock.VerticalAlignment = VerticalAlignment.Bottom;
        textBlock.Padding = new Thickness(0.0, 0.0, 0.0, 5.0);
        stackPanel4.Children.Add(textBlock);
        stackPanel4.Children.Add(this.BigPrice(text));
      }
      else
      {
        stackPanel4.Children.Add(this.BigPrice(item));
      }
      TextBlock textBlock2 = Ui.Text(item2, 12.0, FontWeights.Medium, this.pal.Secondary, TextAlignment.Left, 0, false, false);
      textBlock2.VerticalAlignment = VerticalAlignment.Bottom;
      textBlock2.Padding = new Thickness(0.0, 0.0, 0.0, 7.0);
      stackPanel4.Children.Add(textBlock2);
      stackPanel3.Children.Add(stackPanel4);
      if (this.payload.SupportsYearly || referralDiscount != null)
      {
        UIElementCollection children = stackPanel3.Children;
        string text5;
        if ((text5 = text4) == null)
        {
          text5 = (flag ? (plan.YearlyBilledSummary ?? "") : "");
        }
        children.Add(this.Caption(text5, (text4 != null) ? this.pal.Accent : this.pal.Secondary));
      }
      stackPanel.Children.Add(stackPanel3);
      stackPanel.Children.Add(this.Description((plan.Key == "pro") ? "Great for everyday HeyClicky use." : "For power users who lean on agents."));
      stackPanel.Children.Add(this.Rule());
      UIElementCollection children2 = stackPanel.Children;
      ValueTuple<string, string>[] array = new ValueTuple<string, string>[3];
      array[0] = new ValueTuple<string, string>("Unlimited talk", "Ask, point and talk without watching the meter.");
      array[1] = new ValueTuple<string, string>("Unlimited dictation", "Hold to dictate as much as you want, everywhere.");
      int num = 2;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 1);
      defaultInterpolatedStringHandler.AppendFormatted<int>(plan.AgentsCap);
      defaultInterpolatedStringHandler.AppendLiteral(" agent messages a month");
      array[num] = new ValueTuple<string, string>(defaultInterpolatedStringHandler.ToStringAndClear(), "Handoff your work, build and automate work.");
      children2.Add(this.Features(array));
      GelButton cta = new GelButton(plan.CtaLabel, (!this.OffersFreeOption && plan.Recommended) ? GelAppearance.PrimaryBlue : GelAppearance.SecondaryWhite, GelButton.Sizing.Large, 100.0, null, null, null)
      {
        HorizontalAlignment = HorizontalAlignment.Stretch
      };
      cta.IsLoading = this.launchingPlanKey == plan.Key;
      cta.Click += delegate
      {
        this.OnTapPlanAsync(plan, cta);
      };
      this.planButtons.Add(cta);
      return this.CardFrame(stackPanel, cta);
    }

    // Token: 0x06002606 RID: 9734 RVA: 0x0063E870 File Offset: 0x0063E870
    private TextBlock BigPrice(string amount)
    {
      return Ui.Text(amount, 30.0, FontWeights.Bold, this.pal.Primary, TextAlignment.Left, 0, false, false);
    }

    // Token: 0x06002607 RID: 9735 RVA: 0x0063E898 File Offset: 0x0063E898
    private FrameworkElement Caption(string text, Color color)
    {
      TextBlock textBlock = Ui.Text(text, 10.5, FontWeights.Medium, color, TextAlignment.Left, 0, false, false);
      textBlock.Height = 13.0;
      textBlock.MaxLines = 1;
      return textBlock;
    }

    // Token: 0x06002608 RID: 9736 RVA: 0x0063E8CC File Offset: 0x0063E8CC
    private FrameworkElement PopularBadge()
    {
      return new Grid
      {
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          new GelSurface(GelAppearance.PrimaryBlue, 100.0),
          Ui.Text("Popular", 8.5, FontWeights.Bold, this.pal.ActionInk, TextAlignment.Left, 0, false, false).Pad(6.0, 2.0, 6.0, 2.0)
        }
      };
    }

    // Token: 0x06002609 RID: 9737 RVA: 0x0063E95C File Offset: 0x0063E95C
    private FrameworkElement Features([TupleElementNames(new string[] { "text", "tip" })] [Nullable(new byte[] { 1, 0, 1, 1 })] params ValueTuple<string, string>[] rows)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 9.0
      };
      foreach (ValueTuple<string, string> valueTuple in rows)
      {
        string item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        stackPanel.Children.Add(this.FeatureRow(item, item2));
      }
      return stackPanel;
    }

    // Token: 0x0600260A RID: 9738 RVA: 0x0063E9B8 File Offset: 0x0063E9B8
    private FrameworkElement FeatureRow(string text, string tip)
    {
      Grid grid = new Grid
      {
        Width = 12.0,
        Height = 12.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      grid.Children.Add(new Ellipse
      {
        Fill = Ui.Brush(this.pal.Accent)
      });
      TextBlock textBlock = Icons.Glyph("\ue73e", 7.0, this.pal.CardSurface);
      textBlock.FontWeight = FontWeights.Bold;
      grid.Children.Add(textBlock);
      TextBlock textBlock2 = Ui.Text(text, 11.5, FontWeights.Medium, this.pal.Primary, TextAlignment.Left, 0, true, false);
      textBlock2.VerticalAlignment = VerticalAlignment.Center;
      return new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 7.0,
        Children = 
        {
          grid,
          textBlock2,
          this.InfoTip(tip)
        }
      };
    }

    // Token: 0x0600260B RID: 9739 RVA: 0x0063EABC File Offset: 0x0063EABC
    private FrameworkElement InfoTip(string tip)
    {
      Pressable button = new Pressable(true)
      {
        Width = 14.0,
        Height = 14.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      TextBlock glyph = Icons.Glyph("\ue946", 11.0, this.pal.Secondary);
      glyph.Opacity = 0.6;
      button.Children.Add(glyph);
      button.HoverChanged += delegate(bool h)
      {
        glyph.Opacity = (h ? 1.0 : 0.6);
      };
      button.Tip(tip);
      Flyout flyout = new Flyout
      {
        Placement = FlyoutPlacementMode.Bottom,
        Content = new TextBlock
        {
          Text = tip,
          FontSize = 12.0,
          TextWrapping = TextWrapping.WrapWholeWords,
          Width = 200.0,
          IsTextScaleFactorEnabled = false
        }
      };
      button.Click += delegate
      {
        flyout.ShowAt(button);
      };
      return button;
    }

    // Token: 0x0600260C RID: 9740 RVA: 0x0063EBE0 File Offset: 0x0063EBE0
    [return: TupleElementNames(new string[] { "amount", "period" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    private static ValueTuple<string, string> SplitPriceDisplay(string price)
    {
      int num = price.IndexOf('/');
      if (num >= 0)
      {
        string text = price.Substring(0, num).Trim();
        int num2 = num;
        return new ValueTuple<string, string>(text, price.Substring(num2, price.Length - num2));
      }
      return new ValueTuple<string, string>(price, "");
    }

    // Token: 0x0600260D RID: 9741 RVA: 0x0063EC2C File Offset: 0x0063EC2C
    private FrameworkElement IntervalToggle()
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 4.0
      };
      stackPanel.Children.Add(this.IntervalSegment("month", "Monthly", null));
      stackPanel.Children.Add(this.IntervalSegment("year", "Yearly", this.payload.YearlySavingsLabel));
      return new Border
      {
        Child = stackPanel,
        Padding = new Thickness(3.0),
        CornerRadius = new CornerRadius(100.0),
        Background = Ui.Brush(this.pal.BenefitSurface),
        BorderBrush = Ui.Brush(this.pal.BenefitBorder),
        BorderThickness = new Thickness(0.5),
        HorizontalAlignment = HorizontalAlignment.Left
      }.Capsule();
    }

    // Token: 0x0600260E RID: 9742 RVA: 0x0063ED18 File Offset: 0x0063ED18
    private FrameworkElement IntervalSegment(string interval, string label, [Nullable(2)] string badge)
    {
      bool flag = this.selectedInterval == interval;
      Color color = (flag ? this.pal.ActionInk : this.pal.Primary.WithAlpha(0.6));
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 5.0,
        Padding = new Thickness(12.0, 6.0, 12.0, 6.0),
        VerticalAlignment = VerticalAlignment.Center
      };
      stackPanel.Children.Add(Ui.Text(label, 11.5, FontWeights.SemiBold, color, TextAlignment.Left, 0, false, false).Center<TextBlock>());
      if (badge != null)
      {
        Color color2 = (flag ? this.pal.ActionInk.WithAlpha(0.12) : this.pal.Accent.WithAlpha(0.12));
        Grid grid = new Grid
        {
          VerticalAlignment = VerticalAlignment.Center
        };
        grid.Children.Add(new ShadowLayer(100.0, DS.Black(0.18), 1.25, 1.0, 0.0));
        grid.Children.Add(new Border
        {
          CornerRadius = new CornerRadius(100.0),
          Background = Ui.Brush(color2)
        }.Capsule());
        grid.Children.Add(Ui.Text(badge, 9.5, FontWeights.ExtraBold, flag ? this.pal.ActionInk : this.pal.Accent, TextAlignment.Left, 0, false, false).Pad(6.0, 2.5, 6.0, 2.5));
        stackPanel.Children.Add(grid);
      }
      Pressable pressable = new Pressable(true);
      if (flag)
      {
        pressable.Children.Add(new GelSurface(GelAppearance.PrimaryBlue, 100.0));
      }
      pressable.Children.Add(stackPanel);
      pressable.Click += delegate
      {
        if (this.selectedInterval == interval)
        {
          return;
        }
        this.selectedInterval = interval;
        ClickyAnalytics.TrackPaywallIntervalToggled(this.payload.Surface, interval, this.payload.DefaultInterval ?? "month", this.source.WireName());
        Sounds.Play((interval == "year") ? "skill-up" : "skill-down", 0.18);
        this.Build();
        this.host.ContentResized();
      };
      return pressable;
    }

    // Token: 0x0600260F RID: 9743 RVA: 0x0063EF74 File Offset: 0x0063EF74
    private async Task OnTapPlanAsync(PaywallPlanOption plan, GelButton button)
    {
      if (this.launchingPlanKey == null)
      {
        this.launchingPlanKey = plan.Key;
        button.IsLoading = true;
        this.SetPlanButtonsEnabled(false);
        this.SetError(null);
        string text = ((this.selectedInterval == "year" && plan.SupportsYearly) ? "year" : "month");
        ClickyAnalytics.TrackPaywallCTAClicked(this.payload.Surface, plan.Key, text, this.source.WireName());
        string text2;
        try
        {
          text2 = await this.host.StartCheckoutAsync(plan.Key, text, this.payload.Surface, this.source);
        }
        catch (Exception ex)
        {
          text2 = ex.Message;
        }
        this.launchingPlanKey = null;
        button.IsLoading = false;
        this.SetPlanButtonsEnabled(true);
        if (text2 == null)
        {
          this.host.Close(PaywallCloseKind.HandedOff);
        }
        else
        {
          this.SetError(text2);
        }
      }
    }

    // Token: 0x06002610 RID: 9744 RVA: 0x0063EFC8 File Offset: 0x0063EFC8
    private void SetPlanButtonsEnabled(bool on)
    {
      foreach (GelButton gelButton in this.planButtons)
      {
        gelButton.IsEnabledGel = on;
      }
    }

    // Token: 0x06002611 RID: 9745 RVA: 0x0063F01C File Offset: 0x0063F01C
    private FrameworkElement CtaButton()
    {
      PaywallCard.<>c__DisplayClass45_0 CS$<>8__locals1 = new PaywallCard.<>c__DisplayClass45_0();
      CS$<>8__locals1.<>4__this = this;
      string text = (this.payload.IsPaymentProblem ? "\ue8c7" : "\uf4a5");
      CS$<>8__locals1.cta = new GelButton(this.payload.CtaLabel, GelAppearance.PrimaryBlue, GelButton.Sizing.Large, 100.0, text, null, null)
      {
        HorizontalAlignment = HorizontalAlignment.Stretch
      };
      CS$<>8__locals1.cta.IsLoading = this.isLaunchingCheckout;
      CS$<>8__locals1.cta.Click += delegate
      {
        PaywallCard.<>c__DisplayClass45_0.<<CtaButton>b__0>d <<CtaButton>b__0>d;
        <<CtaButton>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
        <<CtaButton>b__0>d.<>4__this = CS$<>8__locals1;
        <<CtaButton>b__0>d.<>1__state = -1;
        <<CtaButton>b__0>d.<>t__builder.Start<PaywallCard.<>c__DisplayClass45_0.<<CtaButton>b__0>d>(ref <<CtaButton>b__0>d);
      };
      return CS$<>8__locals1.cta;
    }

    // Token: 0x06002612 RID: 9746 RVA: 0x0063F0BC File Offset: 0x0063F0BC
    [NullableContext(2)]
    private void SetError(string error)
    {
      if (error == this.launchErrorText)
      {
        return;
      }
      this.launchErrorText = error;
      if (base.Children.Count > 0)
      {
        UIElementCollection children = base.Children;
        FrameworkElement frameworkElement = children[children.Count - 1] as FrameworkElement;
        if (frameworkElement != null)
        {
          string text = frameworkElement.Tag as string;
          if (text != null && text == "error")
          {
            base.Children.Remove(frameworkElement);
          }
        }
      }
      if (error != null)
      {
        FrameworkElement frameworkElement2 = this.ErrorRow(error);
        base.Children.Add(frameworkElement2);
        if (!Anim.Instant)
        {
          frameworkElement2.Opacity = 0.0;
          frameworkElement2.Translation = new Vector3(0f, -3f, 0f);
          Anim.OpacitySpring(frameworkElement2, 1f, new SwiftSpring(0.35, 0.75));
          Anim.TranslationSpring(frameworkElement2, Vector3.Zero, new SwiftSpring(0.35, 0.75));
        }
      }
      this.host.ContentResized();
    }

    // Token: 0x06002613 RID: 9747 RVA: 0x0063F1CC File Offset: 0x0063F1CC
    private FrameworkElement ErrorRow(string text)
    {
      TextBlock textBlock = Icons.Glyph("\ue7ba", 10.0, this.pal.ErrorText);
      textBlock.VerticalAlignment = VerticalAlignment.Top;
      textBlock.Margin = new Thickness(0.0, 2.0, 0.0, 0.0);
      TextBlock textBlock2 = Ui.Text(text, 11.0, FontWeights.Normal, this.pal.ErrorText, TextAlignment.Left, 0, true, false);
      Grid grid = new Grid();
      grid.ColumnSpacing = 6.0;
      grid.Tag = "error";
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid.Children.Add(textBlock);
      Grid.SetColumn(textBlock2, 1);
      grid.Children.Add(textBlock2);
      return grid;
    }

    // Token: 0x06002614 RID: 9748 RVA: 0x0063F2D0 File Offset: 0x0063F2D0
    private FrameworkElement MaxedOutSection()
    {
      return this.BenefitRow(this.payload.Body, "Your agent messages refresh each month");
    }

    // Token: 0x06002615 RID: 9749 RVA: 0x0063F2E8 File Offset: 0x0063F2E8
    private FrameworkElement OfferStack()
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 8.0
      };
      if (this.payload.IsPaymentProblem)
      {
        stackPanel.Children.Add(this.BenefitRow(this.payload.Body, "Your subscription is paused until payment succeeds"));
      }
      else
      {
        stackPanel.Children.Add(this.BenefitRow("Unlimited talk to HeyClicky.", "Ask, point and talk without watching the meter"));
        stackPanel.Children.Add(this.BenefitRow("Unlimited dictation.", "Hold to dictate as much as you want, everywhere"));
        stackPanel.Children.Add(this.BenefitRow(this.payload.Body, "Handoff your work, build and automate work"));
      }
      return stackPanel;
    }

    // Token: 0x06002616 RID: 9750 RVA: 0x0063F394 File Offset: 0x0063F394
    private FrameworkElement BenefitRow(string title, string subtitle)
    {
      Ellipse ellipse = new Ellipse
      {
        Width = 6.0,
        Height = 6.0,
        Fill = Ui.Brush(this.pal.Accent),
        VerticalAlignment = VerticalAlignment.Top,
        Margin = new Thickness(5.0, 7.0, 5.0, 0.0)
      };
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 2.0
      };
      stackPanel.Children.Add(Ui.Text(title, 14.0, FontWeights.SemiBold, this.pal.Primary, TextAlignment.Left, 2, true, false));
      stackPanel.Children.Add(Ui.Text(subtitle, 11.0, FontWeights.Normal, this.pal.Secondary, TextAlignment.Left, 2, true, false));
      Grid grid = new Grid
      {
        ColumnSpacing = 11.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid.Children.Add(ellipse);
      Grid.SetColumn(stackPanel, 1);
      grid.Children.Add(stackPanel);
      return new Border
      {
        Child = grid,
        Padding = new Thickness(12.0, 10.0, 12.0, 10.0),
        CornerRadius = new CornerRadius(12.0),
        Background = Ui.Brush(this.pal.BenefitSurface),
        BorderBrush = Ui.Brush(this.pal.BenefitBorder),
        BorderThickness = new Thickness(0.5)
      };
    }

    // Token: 0x040011F9 RID: 4601
    private readonly PaywallPayload payload;

    // Token: 0x040011FA RID: 4602
    private readonly PaywallSource source;

    // Token: 0x040011FB RID: 4603
    private readonly PaywallPalette pal;

    // Token: 0x040011FC RID: 4604
    private readonly string currentTier;

    // Token: 0x040011FD RID: 4605
    private readonly IPaywallCardHost host;

    // Token: 0x040011FE RID: 4606
    [Nullable(2)]
    private ReferralPaywallDiscount referral;

    // Token: 0x040011FF RID: 4607
    private string selectedInterval;

    // Token: 0x04001200 RID: 4608
    [Nullable(2)]
    private string launchErrorText;

    // Token: 0x04001201 RID: 4609
    [Nullable(2)]
    private string launchingPlanKey;

    // Token: 0x04001202 RID: 4610
    private bool isLaunchingCheckout;

    // Token: 0x04001203 RID: 4611
    private readonly List<GelButton> planButtons = new List<GelButton>();

    // Token: 0x04001204 RID: 4612
    private const string GiftPath = "M3,8 h18 v4 h-18 z M4,13 h7 v8 h-7 z M13,13 h7 v8 h-7 z M11,8 h2 v13 h-2 z M12,7.5 C10.5,4 7.5,3 6.6,4.6 C5.7,6.2 8,7.6 12,7.5 Z M12,7.5 C13.5,4 16.5,3 17.4,4.6 C18.3,6.2 16,7.6 12,7.5 Z";
  }
}
