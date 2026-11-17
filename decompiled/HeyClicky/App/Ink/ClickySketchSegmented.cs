using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001FA RID: 506
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickySketchSegmented<TOption> : StackPanel
  {
    // Token: 0x0600185D RID: 6237 RVA: 0x005CAD54 File Offset: 0x005CAD54
    public static double ChoiceWidth(string title, double fontSize)
    {
      return Math.Max(Math.Floor((double)PaperText.CharacterCount(title) * fontSize * 0.48 + 22.0), Math.Ceiling(ClickyHandLineBreaker.Width(title, ClickyPaperFonts.Hand(fontSize)) + 22.0));
    }

    // Token: 0x0600185E RID: 6238 RVA: 0x005CADA4 File Offset: 0x005CADA4
    public ClickySketchSegmented(IReadOnlyList<TOption> options, [Nullable(2)] TOption selection, Func<TOption, string> title, Action<TOption> onSelect, double fontSize = 17.0, long seed = 9L)
    {
      base.Orientation = Orientation.Horizontal;
      base.Spacing = 6.0;
      base.HorizontalAlignment = HorizontalAlignment.Left;
      for (int i = 0; i < options.Count; i++)
      {
        TOption option = options[i];
        bool flag = EqualityComparer<TOption>.Default.Equals(option, selection);
        Pressable pressable = new Pressable(true)
        {
          Width = ClickySketchSegmented<TOption>.ChoiceWidth(title(option), fontSize),
          Height = 30.0
        };
        pressable.Children.Add(new ClickySketchShapeView(flag ? ClickySketchInk.Teal : PaperColor.White.Opacity(0.55), flag ? ClickySketchInk.TealLine : ClickySketchInk.TealLine.Opacity(0.4), 3.0, seed + (long)i, 1.1, false, false));
        pressable.Children.Add(new ClickyHandText(title(option), fontSize, 1.0, new int?(1), 1.0, TextAlignment.Left)
        {
          Margin = new Thickness(0.0, 2.0, 0.0, 0.0),
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center
        }.WithForeground(flag ? ClickySketchInk.TealWords : PaperColor.Hex("#4A6488")));
        pressable.Click += delegate
        {
          onSelect(option);
        };
        ClickySketchPressStyle.Apply(pressable);
        AutomationProperties.SetName(pressable, title(option));
        base.Children.Add(pressable);
      }
    }
  }
}
