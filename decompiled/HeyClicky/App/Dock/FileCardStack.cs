using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000335 RID: 821
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class FileCardStack : Grid
  {
    // Token: 0x060022FB RID: 8955 RVA: 0x00629B5C File Offset: 0x00629B5C
    [return: Nullable(2)]
    public static FileCardStack Make(IReadOnlyList<string> entries, [Nullable(2)] string workspace, Action<string> open)
    {
      if (entries.Count != 0)
      {
        return new FileCardStack(entries, workspace, open);
      }
      return null;
    }

    // Token: 0x060022FC RID: 8956 RVA: 0x00629B70 File Offset: 0x00629B70
    private FileCardStack(IReadOnlyList<string> entries, [Nullable(2)] string workspace, Action<string> open)
    {
      FileCardStack <>4__this = this;
      this.open = open;
      base.Width = 152.0;
      base.HorizontalAlignment = HorizontalAlignment.Right;
      List<string> list = entries.Take<string>(3).ToList<string>();
      string arrivalKey = string.Join("\n", entries);
      DateTimeOffset dateTimeOffset;
      bool flag = (FileCardStack.arrivals.TryGetValue(arrivalKey, out dateTimeOffset) && DateTimeOffset.Now - dateTimeOffset < TimeSpan.FromSeconds(3L)) || Anim.ReduceMotion;
      for (int i = 0; i < list.Count; i++)
      {
        string entry = list[i];
        FileCard fileCard = new FileCard(entry, workspace, FileCardStack.CardReadyTitle(entry, workspace), delegate
        {
          <>4__this.Open(entry);
        })
        {
          IsHitTestVisible = false
        };
        double num = FileCardStack.VerticalOffsets[i] * (list.Count != 1);
        Grid grid = new Grid
        {
          Width = 138.0,
          Height = 166.0,
          HorizontalAlignment = HorizontalAlignment.Right,
          VerticalAlignment = VerticalAlignment.Center,
          Margin = new Thickness(0.0, 0.0, -FileCardStack.HorizontalOffsets[i], 0.0),
          RenderTransform = new TranslateTransform
          {
            Y = num
          }
        };
        fileCard.CenterPoint = new Vector3(69f, 83f, 0f);
        fileCard.Rotation = (float)(FileCardStack.TiltDegrees[i] + (double)(flag ? 0 : 10));
        fileCard.Translation = new Vector3(flag ? 0f : 178f, 0f, 0f);
        grid.Children.Add(fileCard);
        Pressable pressable = new Pressable(true)
        {
          Width = Math.Max(0.0, 138.0 - FileCardStack.HorizontalOffsets[i]),
          Height = 166.0,
          HorizontalAlignment = HorizontalAlignment.Left
        };
        pressable.Click += delegate
        {
          <>4__this.Open(entry);
        };
        pressable.HoverChanged += delegate(bool h)
        {
          <>4__this.Hover(entry, h);
        };
        AutomationProperties.SetName(pressable, FileCard.AccessibilityText(entry, workspace));
        pressable.ContextFlyout = FileCardStack.Menu(entry, workspace, null);
        grid.Children.Add(pressable);
        Canvas.SetZIndex(grid, list.Count - i);
        base.Children.Add(grid);
        this.cards.Add(new ValueTuple<FileCard, Grid, int>(fileCard, grid, i));
      }
      if (entries.Count > 3)
      {
        Border border = new Border();
        border.Background = Ui.Brush(Colors.White);
        border.Padding = new Thickness(7.0, 0.0, 7.0, 0.0);
        border.Height = 20.0;
        border.IsHitTestVisible = false;
        border.HorizontalAlignment = HorizontalAlignment.Right;
        border.VerticalAlignment = VerticalAlignment.Center;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
        defaultInterpolatedStringHandler.AppendLiteral("+");
        defaultInterpolatedStringHandler.AppendFormatted<int>(entries.Count - 3);
        border.Child = Ui.Text(defaultInterpolatedStringHandler.ToStringAndClear(), 11.0, FontWeights.Bold, ArcColors.Ink.WithAlpha(0.7), TextAlignment.Left, 0, false, false);
        border.Margin = new Thickness(0.0, 140.0, 108.0, 0.0);
        Border border2 = border.Capsule();
        Canvas.SetZIndex(border2, 200);
        base.Children.Add(border2);
      }
      if (flag)
      {
        FileCardStack.arrivals[arrivalKey] = DateTimeOffset.Now;
        return;
      }
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        foreach (ValueTuple<FileCard, Grid, int> valueTuple in <>4__this.cards)
        {
          FileCard card = valueTuple.Item1;
          int item = valueTuple.Item3;
          int p = item;
          DockTimer.After(0.096 + (double)p * 0.07, delegate
          {
            SwiftSpring swiftSpring = new SwiftSpring(0.55, 0.82);
            Anim.TranslationSpring(card, Vector3.Zero, swiftSpring);
            FileCardStack.RotateSpring(card, FileCardStack.TiltDegrees[p], swiftSpring);
          });
        }
        FileCardStack.arrivals[arrivalKey] = DateTimeOffset.Now;
        if (FileCardStack.arrivals.Count > 40)
        {
          foreach (string text in (from a in FileCardStack.arrivals
            where DateTimeOffset.Now - a.Value > TimeSpan.FromSeconds(30L)
            select a.Key).ToList<string>())
          {
            FileCardStack.arrivals.Remove(text);
          }
        }
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        FileCardStack.arrivals[arrivalKey] = DateTimeOffset.Now;
      };
    }

    // Token: 0x060022FD RID: 8957 RVA: 0x00629F9C File Offset: 0x00629F9C
    private static void RotateSpring(UIElement e, double degrees, SwiftSpring spring)
    {
      if (Anim.ReduceMotion || Anim.Instant)
      {
        e.Rotation = (float)degrees;
        return;
      }
      SpringScalarNaturalMotionAnimation springScalarNaturalMotionAnimation = spring.Scalar(Ui.Compositor, (float)degrees);
      springScalarNaturalMotionAnimation.Target = "Rotation";
      e.StartAnimation(springScalarNaturalMotionAnimation);
    }

    // Token: 0x060022FE RID: 8958 RVA: 0x00629FE4 File Offset: 0x00629FE4
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
      foreach (ValueTuple<FileCard, Grid, int> valueTuple in this.cards)
      {
        FileCard item = valueTuple.Item1;
        Grid item2 = valueTuple.Item2;
        int item3 = valueTuple.Item3;
        bool flag = item.Entry == this.hovered;
        item.SetHovered(flag);
        Canvas.SetZIndex(item2, flag ? 100 : (this.cards.Count - item3));
        if (!Anim.ReduceMotion)
        {
          Anim.TranslationSpring(item, new Vector3((float)(flag ? (-18) : 0), 0f, 0f), swiftSpring);
          Anim.ScaleSpring(item, flag ? 1.05f : 1f, swiftSpring, null);
          FileCardStack.RotateSpring(item, flag ? 0.0 : FileCardStack.TiltDegrees[item3], swiftSpring);
        }
      }
    }

    // Token: 0x060022FF RID: 8959 RVA: 0x0062A138 File Offset: 0x0062A138
    private void Open(string entry)
    {
      this.open(entry);
    }

    // Token: 0x06002300 RID: 8960 RVA: 0x0062A148 File Offset: 0x0062A148
    internal static MenuFlyout Menu(string entry, [Nullable(2)] string workspace, [Nullable(2)] Action open = null)
    {
      MenuFlyout menuFlyout = new MenuFlyout();
      MenuFlyoutItem menuFlyoutItem = new MenuFlyoutItem
      {
        Text = "Open"
      };
      menuFlyoutItem.Click += delegate(object _, RoutedEventArgs _)
      {
        if (open != null)
        {
          open();
          return;
        }
        DockFiles.Open(entry, workspace);
      };
      menuFlyout.Items.Add(menuFlyoutItem);
      if (!CardReadyOutput.IsWebLink(entry))
      {
        MenuFlyoutItem menuFlyoutItem2 = new MenuFlyoutItem
        {
          Text = "Show in File Explorer"
        };
        menuFlyoutItem2.Click += delegate(object _, RoutedEventArgs _)
        {
          DockFiles.Reveal(entry, workspace);
        };
        menuFlyout.Items.Add(menuFlyoutItem2);
      }
      return menuFlyout;
    }

    // Token: 0x06002301 RID: 8961 RVA: 0x0062A1E4 File Offset: 0x0062A1E4
    internal static string CardReadyTitle(string entry, [Nullable(2)] string workspace)
    {
      IReadOnlyList<string> readOnlyList = new <>z__ReadOnlySingleElementList<string>(entry);
      string text = null;
      string text2 = null;
      Func<IReadOnlyList<string>, IReadOnlyList<string>> func = (IReadOnlyList<string> e) => e;
      Func<string, string> func2 = (string e) => DockFiles.Resolve(e, workspace);
      Func<string, ValueTuple<string, string, uint>> func3;
      if ((func3 = FileCardStack.<>O.<0>__FileKind) == null)
      {
        func3 = (FileCardStack.<>O.<0>__FileKind = new Func<string, ValueTuple<string, string, uint>>(FileCardStack.FileKind));
      }
      return CardReadyOutput.CardButtonTitle(CardReadyOutput.Make(readOnlyList, text, text2, func, func2, func3));
    }

    // Token: 0x06002302 RID: 8962 RVA: 0x0062A258 File Offset: 0x0062A258
    [return: TupleElementNames(new string[] { "KindLabel", "Symbol", "Tint" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public static ValueTuple<string, string, uint> FileKind(string path)
    {
      DockFileStyle dockFileStyle = DockFileStyle.For(path);
      uint num = (uint)(((int)dockFileStyle.Tint.R << 16) | ((int)dockFileStyle.Tint.G << 8) | (int)dockFileStyle.Tint.B);
      string kind = dockFileStyle.Kind;
      string text;
      if (kind != null)
      {
        switch (kind.Length)
        {
        case 3:
          if (kind == "PDF")
          {
            text = "doc.richtext.fill";
            goto IL_030F;
          }
          break;
        case 4:
        {
          char c = kind[0];
          if (c <= 'D')
          {
            if (c != 'C')
            {
              if (c == 'D')
              {
                if (kind == "Data")
                {
                  text = "curlybraces";
                  goto IL_030F;
                }
              }
            }
            else if (kind == "Code")
            {
              text = "chevron.left.forwardslash.chevron.right";
              goto IL_030F;
            }
          }
          else if (c != 'L')
          {
            if (c == 'T')
            {
              if (kind == "Text")
              {
                text = "doc.plaintext.fill";
                goto IL_030F;
              }
            }
          }
          else if (kind == "Link")
          {
            text = "link";
            goto IL_030F;
          }
          break;
        }
        case 5:
        {
          char c = kind[0];
          if (c != 'A')
          {
            if (c != 'I')
            {
              if (c == 'V')
              {
                if (kind == "Video")
                {
                  text = "film.fill";
                  goto IL_030F;
                }
              }
            }
            else if (kind == "Image")
            {
              text = "photo.fill";
              goto IL_030F;
            }
          }
          else if (kind == "Audio")
          {
            text = "waveform";
            goto IL_030F;
          }
          break;
        }
        case 6:
        {
          char c = kind[0];
          if (c != 'F')
          {
            if (c == 'S')
            {
              if (kind == "Slides")
              {
                text = "rectangle.on.rectangle.fill";
                goto IL_030F;
              }
            }
          }
          else if (kind == "Folder")
          {
            text = "folder.fill";
            goto IL_030F;
          }
          break;
        }
        case 7:
          if (kind == "Archive")
          {
            text = "doc.zipper";
            goto IL_030F;
          }
          break;
        case 8:
        {
          char c = kind[0];
          if (c != 'D')
          {
            if (c != 'M')
            {
              if (c == 'W')
              {
                if (kind == "Web page")
                {
                  text = "globe";
                  goto IL_030F;
                }
              }
            }
            else if (kind == "Markdown")
            {
              text = "text.alignleft";
              goto IL_030F;
            }
          }
          else if (kind == "Document")
          {
            text = "doc.text.fill";
            goto IL_030F;
          }
          break;
        }
        case 11:
          if (kind == "Spreadsheet")
          {
            text = "tablecells.fill";
            goto IL_030F;
          }
          break;
        }
      }
      text = "doc.fill";
      IL_030F:
      string text2 = text;
      return new ValueTuple<string, string, uint>(dockFileStyle.Kind, text2, num);
    }

    // Token: 0x04001104 RID: 4356
    public const int ShownCardCount = 3;

    // Token: 0x04001105 RID: 4357
    public const double NoteRoomTaken = 132.0;

    // Token: 0x04001106 RID: 4358
    private static readonly double[] TiltDegrees = new double[] { 5.0, -6.0, 2.5 };

    // Token: 0x04001107 RID: 4359
    private static readonly double[] VerticalOffsets = new double[] { -12.0, 22.0, -36.0 };

    // Token: 0x04001108 RID: 4360
    private static readonly double[] HorizontalOffsets = new double[] { 26.0, 40.0, 52.0 };

    // Token: 0x04001109 RID: 4361
    private const double ArrivalDistance = 178.0;

    // Token: 0x0400110A RID: 4362
    private static readonly Dictionary<string, DateTimeOffset> arrivals = new Dictionary<string, DateTimeOffset>();

    // Token: 0x0400110B RID: 4363
    [TupleElementNames(new string[] { "Card", "Slot", "Position" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private readonly List<ValueTuple<FileCard, Grid, int>> cards = new List<ValueTuple<FileCard, Grid, int>>();

    // Token: 0x0400110C RID: 4364
    private readonly Action<string> open;

    // Token: 0x0400110D RID: 4365
    [Nullable(2)]
    private string hovered;

    // Token: 0x0200097E RID: 2430
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040027CB RID: 10187
      [TupleElementNames(new string[] { "KindLabel", "Symbol", "Tint" })]
      [Nullable(new byte[] { 0, 1, 0, 1, 1 })]
      public static Func<string, ValueTuple<string, string, uint>> <0>__FileKind;
    }
  }
}
