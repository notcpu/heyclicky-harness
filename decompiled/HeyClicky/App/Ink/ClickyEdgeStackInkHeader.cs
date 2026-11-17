using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000211 RID: 529
  public sealed class ClickyEdgeStackInkHeader : Grid
  {
    // Token: 0x06001944 RID: 6468 RVA: 0x005CFE10 File Offset: 0x005CFE10
    [NullableContext(1)]
    public ClickyEdgeStackInkHeader(ClickyEdgeStackInkReading reading, double clock, ClickyInkStyle style, double titleSize, double stampSize)
    {
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      ClickyInkLiveNote.Words title = reading.Title;
      if (title != null)
      {
        FrameworkElement frameworkElement = ClickyEdgeStackHighlight.Wrap(ClickyInkWordsView.Make(title, clock, style, ClickyPaperFonts.Hand(titleSize), true, null, null).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
          t.MinimumScale = 0.7;
        }), reading.TitleChanged, 2);
        frameworkElement.HorizontalAlignment = HorizontalAlignment.Left;
        frameworkElement.VerticalAlignment = VerticalAlignment.Center;
        base.Children.Add(frameworkElement);
      }
      ClickyPaperStamp clickyPaperStamp = new ClickyPaperStamp(reading.CurrentState.Status(), null, stampSize, -5.0)
      {
        Margin = new Thickness(22.0, 0.0, 0.0, 0.0),
        VerticalAlignment = VerticalAlignment.Center
      };
      Grid.SetColumn(clickyPaperStamp, 1);
      base.Children.Add(clickyPaperStamp);
    }
  }
}
