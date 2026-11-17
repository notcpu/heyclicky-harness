using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200032A RID: 810
  public sealed class DeckMoreButton : Pressable
  {
    // Token: 0x0600229A RID: 8858 RVA: 0x00626814 File Offset: 0x00626814
    [NullableContext(1)]
    public DeckMoreButton(int count, bool pointsLeft, string help, Action action)
    {
      DeckMoreButton.<>c__DisplayClass0_0 CS$<>8__locals1 = new DeckMoreButton.<>c__DisplayClass0_0();
      base..ctor(true);
      CS$<>8__locals1.<>4__this = this;
      base.Height = 26.0;
      CS$<>8__locals1.plate = new Border
      {
        Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Rgb(0.19, 0.19, 0.19, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Rgb(0.1, 0.1, 0.1, 1.0))
        }),
        BorderBrush = Ui.Brush(DS.White(0.16)),
        BorderThickness = new Thickness(1.0)
      }.Capsule();
      base.Children.Add(new ShadowLayer(13.0, DS.Black(0.35), 8.0, 3.0, 0.0));
      base.Children.Add(CS$<>8__locals1.plate);
      CS$<>8__locals1.arrow = Icons.Glyph(pointsLeft ? "\ue76b" : "\ue76c", 10.0, DS.White(0.86));
      CS$<>8__locals1.arrow.FontWeight = FontWeights.Bold;
      DeckMoreButton.<>c__DisplayClass0_0 CS$<>8__locals2 = CS$<>8__locals1;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
      defaultInterpolatedStringHandler.AppendFormatted<int>(count);
      defaultInterpolatedStringHandler.AppendLiteral(" more");
      CS$<>8__locals2.words = Ui.Text(defaultInterpolatedStringHandler.ToStringAndClear(), 12.0, FontWeights.SemiBold, DS.White(0.86), TextAlignment.Left, 0, false, false);
      Typography.SetNumeralAlignment(CS$<>8__locals1.words, FontNumeralAlignment.Tabular);
      StackPanel stackPanel = Ui.H(5.0, new UIElement[] { CS$<>8__locals1.arrow, CS$<>8__locals1.words });
      stackPanel.VerticalAlignment = VerticalAlignment.Center;
      stackPanel.Padding = new Thickness(12.0, 0.0, 12.0, 0.0);
      base.Children.Add(stackPanel);
      base.HoverChanged += delegate(bool h)
      {
        CS$<>8__locals1.plate.Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Rgb(h ? 0.24 : 0.19, h ? 0.24 : 0.19, h ? 0.24 : 0.19, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Rgb(0.1, 0.1, 0.1, 1.0))
        });
        CS$<>8__locals1.plate.BorderBrush = Ui.Brush(DS.White(h ? 0.28 : 0.16));
        CS$<>8__locals1.arrow.Foreground = (CS$<>8__locals1.words.Foreground = Ui.Brush(DS.White(h ? 1.0 : 0.86)));
        CS$<>8__locals1.<>4__this.CenterPoint = new Vector3((float)CS$<>8__locals1.<>4__this.ActualWidth / 2f, (float)CS$<>8__locals1.<>4__this.ActualHeight / 2f, 0f);
        Anim.Scale(CS$<>8__locals1.<>4__this, h ? 1.05f : 1f, 0.14, null, null);
      };
      base.WithPressScale(0.97f, 0.14, false);
      base.Click += action;
      ToolTipService.SetToolTip(this, help);
      AutomationProperties.SetName(this, help);
    }
  }
}
