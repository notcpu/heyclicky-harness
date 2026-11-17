using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001F7 RID: 503
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickySketchToggleRow : Grid
  {
    // Token: 0x1700044C RID: 1100
    // (get) Token: 0x06001858 RID: 6232 RVA: 0x005CAA10 File Offset: 0x005CAA10
    public ClickySketchToggle Toggle { get; }

    // Token: 0x06001859 RID: 6233 RVA: 0x005CAA18 File Offset: 0x005CAA18
    public ClickySketchToggleRow(string title, bool isOn, ClickySketchTone tone = ClickySketchTone.Blue2)
    {
      this.Toggle = new ClickySketchToggle(isOn, (long)PaperText.CharacterCount(title), title);
      base.ColumnSpacing = 10.0;
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      base.Children.Add(this.Toggle);
      Pressable pressable = new Pressable(false)
      {
        Margin = new Thickness(0.0, 2.0, 0.0, 0.0),
        VerticalAlignment = VerticalAlignment.Top
      };
      pressable.Children.Add(new ClickyHandText(title, 19.0, 1.15, null, 1.0, TextAlignment.Left).WithForeground(tone.Words()));
      pressable.Click += delegate
      {
        this.Toggle.IsOn = !this.Toggle.IsOn;
      };
      AutomationProperties.SetAccessibilityView(pressable, AccessibilityView.Raw);
      Grid.SetColumn(pressable, 1);
      base.Children.Add(pressable);
    }
  }
}
