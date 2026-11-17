using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200029B RID: 667
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkSketch
  {
    // Token: 0x06001B96 RID: 7062 RVA: 0x005F2474 File Offset: 0x005F2474
    public static FrameworkElement Make(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style)
    {
      ClickyInkSketch.<>c__DisplayClass1_0 CS$<>8__locals1 = new ClickyInkSketch.<>c__DisplayClass1_0();
      CS$<>8__locals1.clock = clock;
      CS$<>8__locals1.block = block;
      CS$<>8__locals1.width = ClickyInkEnvironment.Current.ContentWidth;
      string text2;
      string text = (CS$<>8__locals1.block.Source.Attributes.TryGetValue("sketch", out text2) ? text2 : "");
      CS$<>8__locals1.model = ClickyInkSketchModel.Cached(text);
      double valueOrDefault = InkText.ParseDouble(CS$<>8__locals1.block.Source["h"]).GetValueOrDefault(160.0);
      CS$<>8__locals1.canvasHeight = Math.Min(320.0, Math.Max(50.0, valueOrDefault));
      CS$<>8__locals1.scale = CS$<>8__locals1.width / 380.0;
      string text3 = CS$<>8__locals1.block.Source.Text.TrimWsNl();
      CS$<>8__locals1.seedBase = 0;
      foreach (Rune rune in CS$<>8__locals1.block.Id.EnumerateRunes())
      {
        CS$<>8__locals1.seedBase = (CS$<>8__locals1.seedBase * 31 + rune.Value) & 16777215;
      }
      CS$<>8__locals1.step = ClickyInkSketchModel.Step(CS$<>8__locals1.model.Pieces.Count);
      CS$<>8__locals1.totalSeconds = ClickyInkSketchModel.DrawingSeconds(text);
      CS$<>8__locals1.ink = style.Ink;
      string text4 = CS$<>8__locals1.block.Id + "\u001f" + text;
      Dictionary<string, DateTimeOffset> shownAt = ClickyInkSketch.ShownAt;
      lock (shownAt)
      {
        if (!ClickyInkSketch.ShownAt.TryGetValue(text4, out CS$<>8__locals1.shownAt))
        {
          ClickyInkSketch.ShownAt[text4] = (CS$<>8__locals1.shownAt = DateTimeOffset.Now);
        }
        if (ClickyInkSketch.ShownAt.Count > 200)
        {
          ClickyInkSketch.ShownAt.Clear();
        }
      }
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 4.0,
        Width = CS$<>8__locals1.width,
        HorizontalAlignment = HorizontalAlignment.Left
      };
      if (text3.Length > 0)
      {
        stackPanel.Children.Add(InkTextView.Plain(text3, ClickyPaperFonts.Typed(9.5, new FontWeight?(FontWeights.Bold)), CS$<>8__locals1.ink.Opacity(0.45)).With(delegate(InkTextView v)
        {
          v.Tracking = 0.7;
          v.Uppercase = true;
        }));
      }
      CS$<>8__locals1.timeline = new ClickyInkTimeline(new Func<DateTimeOffset, FrameworkElement>(CS$<>8__locals1.<Make>g__Drawing|2), (Anim.ReduceMotion || CS$<>8__locals1.<Make>g__isDrawnIn|1() || ClickyHomeRendering.IsOffscreen) ? null : new TimeSpan?(TimeSpan.FromSeconds(0.03333333333333333)));
      if (CS$<>8__locals1.timeline.MinimumInterval != null)
      {
        DispatcherQueue forCurrentThread = DispatcherQueue.GetForCurrentThread();
        Once.After(Math.Max(0.0, CS$<>8__locals1.totalSeconds + 0.2 - (DateTimeOffset.Now - CS$<>8__locals1.shownAt).TotalSeconds), delegate
        {
          CS$<>8__locals1.timeline.MinimumInterval = null;
        }, forCurrentThread);
      }
      stackPanel.Children.Add(CS$<>8__locals1.timeline);
      AutomationProperties.SetName(stackPanel, (text3.Length == 0) ? "Sketch" : text3);
      return stackPanel;
    }

    // Token: 0x04000D48 RID: 3400
    private static readonly Dictionary<string, DateTimeOffset> ShownAt = new Dictionary<string, DateTimeOffset>();
  }
}
