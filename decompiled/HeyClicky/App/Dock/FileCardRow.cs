using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002F3 RID: 755
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class FileCardRow : Grid
  {
    // Token: 0x0600205C RID: 8284 RVA: 0x00616C00 File Offset: 0x00616C00
    public FileCardRow(IReadOnlyList<string> entries, Action<string> open)
    {
      FileCardRow <>4__this = this;
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.Padding = new Thickness(4.0, 6.0, 0.0, 6.0);
      HashSet<string> seen = new HashSet<string>();
      IReadOnlyList<string> readOnlyList = ArcCardContentSource.MainEntries(entries.Where<string>((string e) => e.Length > 0 && seen.Add(e)).ToList<string>(), null);
      DockFlow dockFlow = new DockFlow
      {
        HorizontalSpacing = 14.0,
        VerticalSpacing = 14.0
      };
      for (int i = 0; i < readOnlyList.Count; i++)
      {
        string entry = readOnlyList[i];
        double num = FileCardRow.TiltDegrees[i % FileCardRow.TiltDegrees.Length];
        FileCard fileCard = new FileCard(entry, null, FileCardStack.CardReadyTitle(entry, null), delegate
        {
          open(entry);
        })
        {
          CenterPoint = new Vector3(69f, 83f, 0f),
          Rotation = (Anim.ReduceMotion ? 0f : ((float)num))
        };
        Pressable pressable = new Pressable(true)
        {
          Width = 138.0,
          Height = 166.0
        };
        pressable.Click += delegate
        {
          open(entry);
        };
        pressable.HoverChanged += delegate(bool h)
        {
          <>4__this.Hover(entry, h);
        };
        pressable.ContextFlyout = FileCardStack.Menu(entry, null, delegate
        {
          open(entry);
        });
        AutomationProperties.SetName(pressable, FileCard.AccessibilityText(entry, null));
        dockFlow.Children.Add(new Grid
        {
          Children = { fileCard, pressable }
        });
        this.cards.Add(new ValueTuple<FileCard, double>(fileCard, num));
      }
      base.Children.Add(dockFlow);
    }

    // Token: 0x0600205D RID: 8285 RVA: 0x00616E24 File Offset: 0x00616E24
    private void Hover(string entry, bool hovering)
    {
      if (hovering && this.hovered != entry)
      {
        FileCardSound.PlayLift();
      }
      if (hovering)
      {
        this.hovered = entry;
      }
      else if (this.hovered == entry)
      {
        this.hovered = null;
      }
      SwiftSpring swiftSpring = new SwiftSpring(0.28, 0.8);
      foreach (ValueTuple<FileCard, double> valueTuple in this.cards)
      {
        FileCard item = valueTuple.Item1;
        double item2 = valueTuple.Item2;
        bool flag = item.Entry == this.hovered;
        item.SetHovered(flag);
        UIElement uielement = item.Parent as UIElement;
        if (uielement != null)
        {
          Canvas.SetZIndex(uielement, (flag > false) ? 1 : 0);
        }
        if (!Anim.ReduceMotion)
        {
          Anim.ScaleSpring(item, flag ? 1.04f : 1f, swiftSpring, null);
          SpringScalarNaturalMotionAnimation springScalarNaturalMotionAnimation = swiftSpring.Scalar(Ui.Compositor, flag ? 0f : ((float)item2));
          springScalarNaturalMotionAnimation.Target = "Rotation";
          item.StartAnimation(springScalarNaturalMotionAnimation);
        }
      }
    }

    // Token: 0x04000FAF RID: 4015
    private static readonly double[] TiltDegrees = new double[] { -3.0, 2.5, -1.5, 3.5 };

    // Token: 0x04000FB0 RID: 4016
    [TupleElementNames(new string[] { "Card", "Tilt" })]
    [Nullable(new byte[] { 1, 0, 1 })]
    private readonly List<ValueTuple<FileCard, double>> cards = new List<ValueTuple<FileCard, double>>();

    // Token: 0x04000FB1 RID: 4017
    [Nullable(2)]
    private string hovered;
  }
}
