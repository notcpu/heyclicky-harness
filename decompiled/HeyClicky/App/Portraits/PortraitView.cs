using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.Graphics.Canvas;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Portraits
{
  // Token: 0x0200010C RID: 268
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PortraitView : Grid
  {
    // Token: 0x17000288 RID: 648
    // (get) Token: 0x06000E0D RID: 3597 RVA: 0x0056F94C File Offset: 0x0056F94C
    private static Compositor C
    {
      get
      {
        return CompositionTarget.GetCompositorForCurrentThread();
      }
    }

    // Token: 0x17000289 RID: 649
    // (get) Token: 0x06000E0E RID: 3598 RVA: 0x0056F954 File Offset: 0x0056F954
    // (set) Token: 0x06000E0F RID: 3599 RVA: 0x0056F95C File Offset: 0x0056F95C
    public bool AnimatesWhileIdle { get; set; }

    // Token: 0x1700028A RID: 650
    // (get) Token: 0x06000E10 RID: 3600 RVA: 0x0056F968 File Offset: 0x0056F968
    private bool IdleLifeEnabled
    {
      get
      {
        return this.AnimatesWhileIdle;
      }
    }

    // Token: 0x1700028B RID: 651
    // (get) Token: 0x06000E11 RID: 3601 RVA: 0x0056F970 File Offset: 0x0056F970
    // (set) Token: 0x06000E12 RID: 3602 RVA: 0x0056F978 File Offset: 0x0056F978
    public bool TracksPointer { get; set; } = true;

    // Token: 0x1700028C RID: 652
    // (get) Token: 0x06000E13 RID: 3603 RVA: 0x0056F984 File Offset: 0x0056F984
    // (set) Token: 0x06000E14 RID: 3604 RVA: 0x0056F98C File Offset: 0x0056F98C
    public bool BlinksOnPointerEntry { get; set; } = true;

    // Token: 0x1700028D RID: 653
    // (get) Token: 0x06000E15 RID: 3605 RVA: 0x0056F998 File Offset: 0x0056F998
    public PortraitAppearance Appearance
    {
      get
      {
        return this.a;
      }
    }

    // Token: 0x06000E16 RID: 3606 RVA: 0x0056F9A0 File Offset: 0x0056F9A0
    public PortraitView(PortraitAppearance appearance, double width, double height, PortraitExpression expression = PortraitExpression.Curious, bool showsAvatarBackground = true, Color? glow = null, double glowRadius = 0.0)
    {
      this.a = appearance;
      this.pack = appearance.Pack;
      this.caretEyes = this.pack == PortraitPack.ClassicClouds;
      this.expression = expression;
      base.Width = width;
      base.Height = height;
      base.IsHitTestVisible = true;
      this.AutomationProperties_SetName("HeyClicky");
      double num = Math.Min(width, height);
      if (showsAvatarBackground)
      {
        this.plate = new Ellipse
        {
          Width = num,
          Height = num,
          Fill = Ui.Brush(appearance.Background),
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center,
          IsHitTestVisible = false
        };
        this.art.Children.Add(this.plate);
      }
      if (glow != null)
      {
        Color valueOrDefault = glow.GetValueOrDefault();
        if (glowRadius > 0.0)
        {
          base.Children.Add(this.GlowHost(valueOrDefault, glowRadius));
        }
      }
      this.art.Children.Add(this.stage);
      base.Children.Add(this.art);
      this.stage.Children.Add(this.breath);
      this.breath.Children.Add(this.hairBack);
      this.breath.Children.Add(this.head);
      UIElement[] array = new UIElement[] { this.hairBack, this.head };
      for (int i = 0; i < array.Length; i++)
      {
        array[i].CenterPoint = new Vector3(160f, 192f, 0f);
      }
      this.BuildArtwork(showsAvatarBackground);
      this.Layout(width, height);
      this.ApplyExpression(false);
      this.Look(expression.RestingGaze(), false);
      if (showsAvatarBackground)
      {
        CompositionEllipseGeometry compositionEllipseGeometry = PortraitView.C.CreateEllipseGeometry();
        compositionEllipseGeometry.Center = new Vector2((float)width / 2f, (float)height / 2f);
        compositionEllipseGeometry.Radius = new Vector2((float)num / 2f);
        ElementCompositionPreview.GetElementVisual(this.art).Clip = PortraitView.C.CreateGeometricClip(compositionEllipseGeometry);
      }
      base.Background = Ui.Transparent;
      base.PointerEntered += this.OnPointerEntered;
      base.PointerMoved += this.OnPointerMoved;
      base.PointerExited += this.OnPointerExited;
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        this.loaded = true;
        this.live = true;
        base.IsHitTestVisible = this.TracksPointer;
        this.UpdateActivity();
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        this.loaded = false;
        this.UpdateActivity();
      };
    }

    // Token: 0x06000E17 RID: 3607 RVA: 0x0056FD74 File Offset: 0x0056FD74
    private void AutomationProperties_SetName(string name)
    {
      AutomationProperties.SetName(this, name);
    }

    // Token: 0x1700028E RID: 654
    // (get) Token: 0x06000E18 RID: 3608 RVA: 0x0056FD80 File Offset: 0x0056FD80
    [TupleElementNames(new string[] { "X", "Y", "W", "H" })]
    [Nullable(0)]
    private ValueTuple<double, double, double, double> Viewport
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "X", "Y", "W", "H" })]
      get
      {
        PortraitPack portraitPack = this.pack;
        ValueTuple<int, int, int, int> valueTuple;
        if (portraitPack != PortraitPack.CloudClub)
        {
          if (portraitPack == PortraitPack.ClassicClouds)
          {
            valueTuple = new ValueTuple<int, int, int, int>(-32, -28, 384, 384);
          }
          else
          {
            valueTuple = new ValueTuple<int, int, int, int>(0, -20, 320, 320);
          }
        }
        else
        {
          valueTuple = new ValueTuple<int, int, int, int>(0, 4, 320, 320);
        }
        ValueTuple<int, int, int, int> valueTuple2 = valueTuple;
        return new ValueTuple<double, double, double, double>((double)valueTuple2.Item1, (double)valueTuple2.Item2, (double)valueTuple2.Item3, (double)valueTuple2.Item4);
      }
    }

    // Token: 0x06000E19 RID: 3609 RVA: 0x0056FDFC File Offset: 0x0056FDFC
    private void Layout(double w, double h)
    {
      ValueTuple<double, double, double, double> viewport = this.Viewport;
      this.scale = Math.Max(0.01, Math.Min(w / viewport.Item3, h / viewport.Item4));
      this.stage.RenderTransform = new MatrixTransform
      {
        Matrix = new Matrix(this.scale, 0.0, 0.0, this.scale, w / 2.0 - (viewport.Item1 + viewport.Item3 / 2.0) * this.scale, h / 2.0 - (viewport.Item2 + viewport.Item4 / 2.0) * this.scale)
      };
    }

    // Token: 0x06000E1A RID: 3610 RVA: 0x0056FEC8 File Offset: 0x0056FEC8
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "X", "Y" })]
    private ValueTuple<double, double> DrawingPoint(Point p)
    {
      ValueTuple<double, double, double, double> viewport = this.Viewport;
      return new ValueTuple<double, double>(viewport.Item1 + (p.X - (base.Width - viewport.Item3 * this.scale) / 2.0) / this.scale, viewport.Item2 + (p.Y - (base.Height - viewport.Item4 * this.scale) / 2.0) / this.scale);
    }

    // Token: 0x06000E1B RID: 3611 RVA: 0x0056FF4C File Offset: 0x0056FF4C
    private void BuildArtwork(bool avatarBackground)
    {
      if (this.pack == PortraitPack.ClassicClouds)
      {
        this.BuildClassicCloud(avatarBackground);
        return;
      }
      if (this.pack == PortraitPack.CloudClub)
      {
        this.BuildCloudClub();
        return;
      }
      foreach (HairPiece hairPiece in PortraitArt.Back(this.a.Hairstyle, true))
      {
        this.AddPiece(hairPiece, this.hairBack);
      }
      PortraitPack portraitPack = this.pack;
      bool flag = portraitPack == PortraitPack.ArcadeAllStars || portraitPack == PortraitPack.StorybookSpirits;
      double num = (double)(flag ? 116 : ((this.a.Hairstyle == Hairstyle.Topknot) ? 82 : ((this.a.Hairstyle == Hairstyle.TwinBuns) ? 72 : ((this.a.Hairstyle == Hairstyle.Braids) ? 68 : 58))));
      this.head.Children.Add(PortraitView.Shape(PortraitArt.FacePath(num), new Color?(this.a.Skin), null, 3.2));
      this.head.Children.Add(PortraitView.Shape(PortraitArt.JawPath(), null, new Color?(PortraitData.Ink), 3.2));
      foreach (double num2 in new double[] { 74.0, 225.0 })
      {
        ValueTuple<double, int, int, int> valueTuple = ((this.pack == PortraitPack.StorybookSpirits) ? new ValueTuple<double, int, int, int>((double)((num2 == 74.0) ? 82 : 209), 191, 36, 20) : new ValueTuple<double, int, int, int>(num2, 200, 28, 12));
        Path path = PortraitView.Shape(PPath.Oval(valueTuple.Item1, (double)valueTuple.Item2, (double)valueTuple.Item3, (double)valueTuple.Item4), new Color?(PortraitData.Rgb(15172239U)), null, 3.2);
        path.Opacity = 0.0;
        this.head.Children.Add(path);
        this.cheeks.Add(path);
      }
      if (this.pack == PortraitPack.KiCrew)
      {
        this.head.Children.Add(PortraitView.Shape(new PPath().M(162.0, 185.0).L(157.0, 197.0).Q(158.0, 200.0, 164.0, 198.0), null, new Color?(PortraitData.Ink), 3.2));
      }
      else
      {
        Hairstyle hairstyle = this.a.Hairstyle;
        flag = hairstyle == Hairstyle.FoxHood || hairstyle == Hairstyle.BearHood;
        if (flag)
        {
          this.head.Children.Add(PortraitView.Shape(PPath.Oval(125.0, 194.0, 76.0, 41.0), new Color?(this.a.Background), null, 3.2));
        }
      }
      this.MakeEye(108.0, 164.0);
      this.MakeEye(217.0, 164.0);
      Color color = ((this.pack == PortraitPack.StorybookSpirits) ? PortraitData.StorybookFaceInk : PortraitData.Ink);
      this.mouth = new PortraitView.MorphPath(new Color?(color), new Color?(color), 2.3, PortraitArt.MouthPath(this.expression));
      if (this.a.Hairstyle == Hairstyle.Robot)
      {
        this.mouth.Root.Translation = new Vector3(0f, -12f, 0f);
      }
      this.head.Children.Add(this.mouth.Root);
      this.head.Children.Add(this.hairFront);
      foreach (HairPiece hairPiece2 in PortraitArt.Front(this.a.Hairstyle))
      {
        this.AddPiece(hairPiece2, this.hairFront);
      }
    }

    // Token: 0x06000E1C RID: 3612 RVA: 0x005703E4 File Offset: 0x005703E4
    private void BuildClassicCloud(bool avatarBackground)
    {
      PPath ppath = PortraitArt.ClassicSilhouette(this.a);
      ValueTuple<double, double, double, double> valueTuple = ppath.Bounds();
      if (avatarBackground)
      {
        this.head.Children.Add(this.HeadShadow(ppath));
      }
      this.paint = new Path
      {
        Data = ppath.ToXaml(),
        Fill = Ui.Brush(this.a.Hair)
      };
      WriteableBitmap writeableBitmap = PortraitWash.For(this.a, new ValueTuple<double, double, double, double>?(valueTuple));
      if (writeableBitmap != null)
      {
        this.paint.Fill = new ImageBrush
        {
          ImageSource = writeableBitmap,
          Stretch = Stretch.Fill
        };
      }
      this.head.Children.Add(this.paint);
      for (int i = 0; i < 2; i++)
      {
        this.MakeCaretEye(PortraitArt.ClassicEyeCenter(i, this.a));
      }
    }

    // Token: 0x06000E1D RID: 3613 RVA: 0x005704B4 File Offset: 0x005704B4
    private void BuildCloudClub()
    {
      PPath ppath = PortraitArt.CloudClubSilhouette(this.a.Hairstyle);
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush
      {
        MappingMode = BrushMappingMode.Absolute,
        StartPoint = new Point(112.0, 52.0),
        EndPoint = new Point(208.0, 264.0)
      };
      linearGradientBrush.GradientStops.Add(new GradientStop
      {
        Color = this.a.HairColor.CloudFill(0.87),
        Offset = 0.0
      });
      linearGradientBrush.GradientStops.Add(new GradientStop
      {
        Color = this.a.HairColor.CloudFill(0.55),
        Offset = 0.5
      });
      linearGradientBrush.GradientStops.Add(new GradientStop
      {
        Color = this.a.HairColor.CloudFill(0.12),
        Offset = 1.0
      });
      this.head.Children.Add(new Path
      {
        Data = ppath.ToXaml(),
        Fill = linearGradientBrush,
        Stroke = Ui.Brush(PortraitData.Ink),
        StrokeThickness = 3.2,
        StrokeLineJoin = PenLineJoin.Round
      });
      double[] array = new double[] { 74.0, 225.0 };
      for (int i = 0; i < array.Length; i++)
      {
        Path path = PortraitView.Shape(PPath.Oval(array[i], 204.0, 28.0, 12.0), new Color?(PortraitData.Rgb(14650524U)), null, 3.2);
        this.head.Children.Add(path);
        this.cheeks.Add(path);
      }
      this.MakeEye(108.0, 164.0);
      this.MakeEye(217.0, 164.0);
      this.mouth = new PortraitView.MorphPath(new Color?(PortraitData.Ink), new Color?(PortraitData.Ink), 2.3, PortraitArt.MouthPath(this.expression));
      this.head.Children.Add(this.mouth.Root);
    }

    // Token: 0x06000E1E RID: 3614 RVA: 0x00570738 File Offset: 0x00570738
    private void AddPiece(HairPiece piece, Canvas parent)
    {
      PieceFill? fill = piece.Fill;
      Color? color;
      if (fill != null)
      {
        switch (fill.GetValueOrDefault())
        {
        case PieceFill.Hair:
          color = new Color?(this.a.Hair);
          goto IL_00D5;
        case PieceFill.HairShadow:
          color = new Color?(PortraitView.Tinted(this.a.Hair, 0.0, 0.23));
          goto IL_00D5;
        case PieceFill.HairHighlight:
          color = new Color?(PortraitView.Tinted(this.a.Hair, 1.0, 0.16));
          goto IL_00D5;
        case PieceFill.Outfit:
          color = new Color?(this.a.Outfit);
          goto IL_00D5;
        case PieceFill.Accent:
          color = new Color?(this.a.Accent);
          goto IL_00D5;
        }
      }
      color = null;
      IL_00D5:
      Color? color2 = color;
      parent.Children.Add(PortraitView.Shape(piece.Path, color2, piece.Stroked ? new Color?(PortraitData.Ink) : null, 3.2));
    }

    // Token: 0x06000E1F RID: 3615 RVA: 0x0057085C File Offset: 0x0057085C
    private static Color Tinted(Color c, double toward, double amount)
    {
      PortraitView.<>c__DisplayClass61_0 CS$<>8__locals1;
      CS$<>8__locals1.toward = toward;
      CS$<>8__locals1.amount = amount;
      return PortraitData.Rgb(PortraitView.<Tinted>g__F|61_0(c.R, ref CS$<>8__locals1), PortraitView.<Tinted>g__F|61_0(c.G, ref CS$<>8__locals1), PortraitView.<Tinted>g__F|61_0(c.B, ref CS$<>8__locals1), 1.0);
    }

    // Token: 0x06000E20 RID: 3616 RVA: 0x005708B4 File Offset: 0x005708B4
    private static Path Shape(PPath path, Color? fill, Color? stroke, double lineWidth = 3.2)
    {
      Path path2 = new Path();
      path2.Data = path.ToXaml();
      Brush brush;
      if (fill != null)
      {
        Color valueOrDefault = fill.GetValueOrDefault();
        brush = Ui.Brush(valueOrDefault);
      }
      else
      {
        brush = null;
      }
      path2.Fill = brush;
      Brush brush2;
      if (stroke != null)
      {
        Color valueOrDefault2 = stroke.GetValueOrDefault();
        brush2 = Ui.Brush(valueOrDefault2);
      }
      else
      {
        brush2 = null;
      }
      path2.Stroke = brush2;
      path2.StrokeThickness = lineWidth;
      path2.StrokeLineJoin = PenLineJoin.Round;
      path2.StrokeStartLineCap = PenLineCap.Round;
      path2.StrokeEndLineCap = PenLineCap.Round;
      return path2;
    }

    // Token: 0x06000E21 RID: 3617 RVA: 0x00570934 File Offset: 0x00570934
    private void MakeEye(double cx, double cy)
    {
      bool flag = this.pack == PortraitPack.KiCrew;
      bool flag2 = this.pack == PortraitPack.ArcadeAllStars;
      bool flag3 = this.pack == PortraitPack.StorybookSpirits;
      bool flag4 = this.pack.IsCloud();
      double num = (double)(flag4 ? 72 : (flag ? 86 : (flag3 ? 44 : (flag2 ? 82 : 90))));
      double num2 = (double)(flag4 ? 86 : (flag ? 64 : (flag3 ? 54 : (flag2 ? 92 : 112))));
      Canvas canvas = new Canvas
      {
        Width = num,
        Height = num2,
        CenterPoint = new Vector3((float)num / 2f, (float)num2 / 2f, 0f)
      };
      Canvas.SetLeft(canvas, cx - num / 2.0);
      Canvas.SetTop(canvas, cy - num2 / 2.0);
      PPath ppath;
      if (flag3)
      {
        ppath = PortraitArt.StorybookEyePath();
      }
      else if (flag)
      {
        PPath ppath2 = new PPath().M(2.0, 9.0).Q(43.0, 11.0, 84.0, 24.0).Q(83.0, 51.0, 72.0, 59.0)
          .Q(41.0, 65.0, 15.0, 58.0)
          .Q(4.0, 46.0, 2.0, 9.0)
          .Z();
        ppath = ((cx > 162.0) ? ppath2.Transformed(new Matrix3x2(-1f, 0f, 0f, 1f, (float)num, 0f)) : ppath2);
      }
      else if (this.a.Hairstyle == Hairstyle.Robot)
      {
        ppath = PPath.Rounded(0.0, 0.0, num, num2, 22.0);
      }
      else
      {
        ppath = PPath.Oval(0.0, 0.0, num, num2);
      }
      Canvas canvas2 = new Canvas
      {
        Width = num,
        Height = num2
      };
      Color color = (flag3 ? PortraitData.StorybookFaceInk : PortraitData.Rgb(0.99, 0.99, 0.98, 1.0));
      canvas2.Children.Add(PortraitView.Shape(ppath, new Color?(color), null, 3.2));
      Canvas canvas3 = new Canvas
      {
        Width = num,
        Height = num2
      };
      ValueTuple<double, double, double, double> valueTuple2;
      if (!flag3)
      {
        ValueTuple<int, int, double, double> valueTuple = (flag ? new ValueTuple<int, int, double, double>(25, 9, 38.0, 54.0) : new ValueTuple<int, int, double, double>(6, 1, num - 12.0, num2 - 6.0));
        valueTuple2 = new ValueTuple<double, double, double, double>((double)valueTuple.Item1, (double)valueTuple.Item2, valueTuple.Item3, valueTuple.Item4);
      }
      else
      {
        valueTuple2 = new ValueTuple<double, double, double, double>(3.0, 6.0, 38.0, 47.0);
      }
      ValueTuple<double, double, double, double> valueTuple3 = valueTuple2;
      ValueTuple<double, double, double, double> valueTuple4;
      if (!flag3)
      {
        ValueTuple<int, int, double, double> valueTuple = (flag ? new ValueTuple<int, int, double, double>(35, 11, 22.0, 46.0) : new ValueTuple<int, int, double, double>(16, 4, num - 25.0, num2 - 16.0));
        valueTuple4 = new ValueTuple<double, double, double, double>((double)valueTuple.Item1, (double)valueTuple.Item2, valueTuple.Item3, valueTuple.Item4);
      }
      else
      {
        valueTuple4 = new ValueTuple<double, double, double, double>(2.0, -3.0, 36.0, 49.0);
      }
      ValueTuple<double, double, double, double> valueTuple5 = valueTuple4;
      ValueTuple<double, double, double, double> valueTuple7;
      if (!flag4)
      {
        ValueTuple<int, int, int, int> valueTuple6 = (flag ? new ValueTuple<int, int, int, int>(46, 17, 8, 12) : (flag3 ? new ValueTuple<int, int, int, int>(25, 11, 6, 8) : (flag2 ? new ValueTuple<int, int, int, int>(44, 16, 18, 22) : new ValueTuple<int, int, int, int>(49, 18, 20, 24))));
        valueTuple7 = new ValueTuple<double, double, double, double>((double)valueTuple6.Item1, (double)valueTuple6.Item2, (double)valueTuple6.Item3, (double)valueTuple6.Item4);
      }
      else
      {
        valueTuple7 = new ValueTuple<double, double, double, double>(38.0, 15.0, 15.0, 18.0);
      }
      ValueTuple<double, double, double, double> valueTuple8 = valueTuple7;
      canvas3.Children.Add(PortraitView.Shape(PPath.Oval(valueTuple3.Item1, valueTuple3.Item2, valueTuple3.Item3, valueTuple3.Item4), new Color?(this.a.Eyes), null, 3.2));
      canvas3.Children.Add(PortraitView.Shape(PPath.Oval(valueTuple5.Item1, valueTuple5.Item2, valueTuple5.Item3, valueTuple5.Item4), new Color?(flag3 ? PortraitData.StorybookFaceInk : PortraitData.Ink), null, 3.2));
      canvas3.Children.Add(PortraitView.Shape(PPath.Oval(valueTuple8.Item1, valueTuple8.Item2, valueTuple8.Item3, valueTuple8.Item4), new Color?(PortraitData.Rgb(1.0, 1.0, 1.0, 1.0)), null, 3.2));
      Path path = PortraitView.Shape(flag ? PortraitArt.HeartPath(0.65, 0.55, 17.0, 13.0) : PortraitArt.HeartPath(num / 78.0, num2 / 100.0, 0.0, 0.0), new Color?(PortraitData.Rgb(14836871U)), null, 3.2);
      path.Opacity = 0.0;
      canvas2.Children.Add(canvas3);
      canvas2.Children.Add(path);
      PortraitView.ClipTo(canvas2, ppath);
      canvas.Children.Add(canvas2);
      if (!flag3)
      {
        canvas.Children.Add(PortraitView.Shape(ppath, null, new Color?(PortraitData.Ink), 3.2));
      }
      this.head.Children.Add(canvas);
      this.eyes.Add(canvas);
      this.pupils.Add(canvas3);
      this.hearts.Add(path);
      this.eyeCenters.Add(new ValueTuple<double, double>(cx, cy));
      double num3 = (double)(flag3 ? 18 : 30);
      PortraitView.MorphPath morphPath = new PortraitView.MorphPath(null, new Color?(PortraitData.Ink), 3.2, new PPath().M(cx - num3, cy + 3.0).Q(cx, cy + 17.0, cx + num3, cy + 3.0));
      morphPath.Root.Opacity = 0.0;
      this.head.Children.Add(morphPath.Root);
      this.lids.Add(morphPath);
      Canvas canvas4 = new Canvas
      {
        Width = 0.0,
        Height = 0.0,
        Opacity = 0.0
      };
      Canvas.SetLeft(canvas4, cx);
      Canvas.SetTop(canvas4, cy - num2 / 2.0 - 2.0);
      canvas4.Children.Add(PortraitView.Shape(new PPath().M(-17.0, 0.0).Q(0.0, -4.0, 17.0, 0.0), null, new Color?(PortraitData.Ink), 3.4));
      this.head.Children.Add(canvas4);
      this.brows.Add(canvas4);
    }

    // Token: 0x06000E22 RID: 3618 RVA: 0x005711B8 File Offset: 0x005711B8
    [NullableContext(0)]
    private void MakeCaretEye([TupleElementNames(new string[] { "X", "Y" })] ValueTuple<double, double> center)
    {
      Color color = ((this.pack == PortraitPack.ClassicClouds) ? PortraitData.ClassicCloudEyeColor : this.a.HairColor.CloudFeatureInk());
      Canvas canvas = new Canvas
      {
        Width = 72.0,
        Height = 86.0,
        CenterPoint = new Vector3(36f, 43f, 0f)
      };
      Canvas.SetLeft(canvas, center.Item1 - 36.0);
      Canvas.SetTop(canvas, center.Item2 - 43.0);
      PortraitView.MorphPath morphPath = new PortraitView.MorphPath(null, new Color?(color), 22.400000000000002, PortraitArt.CaretEyePath(this.expression, this.eyes.Count));
      canvas.Children.Add(morphPath.Root);
      this.head.Children.Add(canvas);
      this.eyes.Add(canvas);
      this.carets.Add(morphPath);
      this.eyeCenters.Add(center);
      PortraitView.MorphPath morphPath2 = new PortraitView.MorphPath(null, new Color?(color), 22.400000000000002, this.LidPath(this.eyes.Count - 1, this.expression));
      morphPath2.Root.Opacity = 0.0;
      this.head.Children.Add(morphPath2.Root);
      this.lids.Add(morphPath2);
    }

    // Token: 0x06000E23 RID: 3619 RVA: 0x0057133C File Offset: 0x0057133C
    private PPath LidPath(int index, PortraitExpression e)
    {
      ValueTuple<double, double> valueTuple = this.eyeCenters[index];
      if (this.pack == PortraitPack.ClassicClouds)
      {
        return PortraitArt.ClassicClosedEyePath(e, valueTuple);
      }
      double num = (double)((this.pack == PortraitPack.StorybookSpirits) ? 18 : 30);
      double num2 = (double)((index == 0) ? 108 : 217);
      return new PPath().M(num2 - num, 167.0).Q(num2, (double)((e == PortraitExpression.Laughing) ? 142 : 181), num2 + num, 167.0);
    }

    // Token: 0x06000E24 RID: 3620 RVA: 0x005713C0 File Offset: 0x005713C0
    private static void ClipTo(UIElement element, PPath path)
    {
      try
      {
        CompositionPathGeometry compositionPathGeometry = PortraitView.C.CreatePathGeometry(new CompositionPath(path.ToWin2D(PortraitView.Device)));
        ElementCompositionPreview.GetElementVisual(element).Clip = PortraitView.C.CreateGeometricClip(compositionPathGeometry);
      }
      catch (Exception ex)
      {
        Log.Warn("portrait", "eye clip: " + ex.Message);
      }
    }

    // Token: 0x06000E25 RID: 3621 RVA: 0x00571430 File Offset: 0x00571430
    private FrameworkElement HeadShadow(PPath silhouette)
    {
      Canvas canvas = new Canvas
      {
        Width = 320.0,
        Height = 400.0,
        IsHitTestVisible = false
      };
      try
      {
        CompositionPathGeometry compositionPathGeometry = PortraitView.C.CreatePathGeometry(new CompositionPath(silhouette.ToWin2D(PortraitView.Device)));
        CompositionSpriteShape compositionSpriteShape = PortraitView.C.CreateSpriteShape(compositionPathGeometry);
        compositionSpriteShape.FillBrush = PortraitView.C.CreateColorBrush(Colors.Black);
        ShapeVisual shapeVisual = PortraitView.C.CreateShapeVisual();
        shapeVisual.Size = new Vector2(320f, 400f);
        shapeVisual.Shapes.Add(compositionSpriteShape);
        CompositionVisualSurface compositionVisualSurface = PortraitView.C.CreateVisualSurface();
        compositionVisualSurface.SourceVisual = shapeVisual;
        compositionVisualSurface.SourceSize = new Vector2(320f, 400f);
        CompositionSurfaceBrush compositionSurfaceBrush = PortraitView.C.CreateSurfaceBrush(compositionVisualSurface);
        compositionSurfaceBrush.Stretch = CompositionStretch.None;
        DropShadow dropShadow = PortraitView.C.CreateDropShadow();
        dropShadow.Color = PortraitData.Rgb(0.16, 0.18, 0.23, 1.0);
        dropShadow.Opacity = 0.2f;
        dropShadow.BlurRadius = 14f;
        dropShadow.Offset = new Vector3(0f, 4f, 0f);
        dropShadow.Mask = compositionSurfaceBrush;
        SpriteVisual spriteVisual = PortraitView.C.CreateSpriteVisual();
        spriteVisual.Size = new Vector2(320f, 400f);
        spriteVisual.Shadow = dropShadow;
        ElementCompositionPreview.SetElementChildVisual(canvas, spriteVisual);
      }
      catch (Exception ex)
      {
        Log.Warn("portrait", "head shadow: " + ex.Message);
      }
      return canvas;
    }

    // Token: 0x06000E26 RID: 3622 RVA: 0x005715FC File Offset: 0x005715FC
    private FrameworkElement GlowHost(Color color, double radius)
    {
      Grid grid = new Grid
      {
        IsHitTestVisible = false
      };
      try
      {
        CompositionVisualSurface compositionVisualSurface = PortraitView.C.CreateVisualSurface();
        compositionVisualSurface.SourceVisual = ElementCompositionPreview.GetElementVisual(this.art);
        Vector2 vector = new Vector2((float)base.Width, (float)base.Height);
        compositionVisualSurface.SourceSize = vector;
        CompositionSurfaceBrush compositionSurfaceBrush = PortraitView.C.CreateSurfaceBrush(compositionVisualSurface);
        compositionSurfaceBrush.Stretch = CompositionStretch.None;
        DropShadow dropShadow = PortraitView.C.CreateDropShadow();
        dropShadow.Color = color;
        dropShadow.Opacity = 0.55f;
        dropShadow.BlurRadius = (float)(radius * 2.0);
        dropShadow.Offset = Vector3.Zero;
        dropShadow.Mask = compositionSurfaceBrush;
        SpriteVisual spriteVisual = PortraitView.C.CreateSpriteVisual();
        spriteVisual.Size = vector;
        spriteVisual.Shadow = dropShadow;
        ElementCompositionPreview.SetElementChildVisual(grid, spriteVisual);
      }
      catch (Exception ex)
      {
        Log.Warn("portrait", "glow: " + ex.Message);
      }
      return grid;
    }

    // Token: 0x1700028F RID: 655
    // (get) Token: 0x06000E27 RID: 3623 RVA: 0x00571700 File Offset: 0x00571700
    private bool ClosedEyes
    {
      get
      {
        return this.expression.HasClosedEyes() || (this.pack == PortraitPack.ClassicClouds && this.expression == PortraitExpression.Sad);
      }
    }

    // Token: 0x17000290 RID: 656
    // (get) Token: 0x06000E28 RID: 3624 RVA: 0x00571728 File Offset: 0x00571728
    // (set) Token: 0x06000E29 RID: 3625 RVA: 0x00571730 File Offset: 0x00571730
    public PortraitExpression Expression
    {
      get
      {
        return this.expression;
      }
      set
      {
        if (value == this.expression)
        {
          return;
        }
        this.expression = value;
        this.ApplyExpression(this.CanAnimate);
        ValueTuple<double, double>? valueTuple = this.gazeTarget;
        if (valueTuple == null && !this.pointerInside)
        {
          this.Look(this.expression.RestingGaze(), this.CanAnimate);
        }
      }
    }

    // Token: 0x17000291 RID: 657
    // (get) Token: 0x06000E2A RID: 3626 RVA: 0x0057178C File Offset: 0x0057178C
    private bool CanAnimate
    {
      get
      {
        return this.live && this.loaded && !Anim.Instant;
      }
    }

    // Token: 0x06000E2B RID: 3627 RVA: 0x005717A8 File Offset: 0x005717A8
    private void ApplyExpression(bool animated)
    {
      bool closedEyes = this.ClosedEyes;
      for (int i = 0; i < this.eyes.Count; i++)
      {
        PortraitView.ScaleTo(this.eyes[i], new Vector3(1f, (float)this.expression.EyeOpenness(i), 1f), animated, 0.2);
        PortraitView.Fade(this.eyes[i], !closedEyes, animated, 0.2);
      }
      for (int j = 0; j < this.lids.Count; j++)
      {
        this.lids[j].Set(this.LidPath(j, this.expression), animated);
        PortraitView.Fade(this.lids[j].Root, closedEyes ? ((this.pack == PortraitPack.ClassicClouds && this.expression != PortraitExpression.Laughing) ? 0.58 : 1.0) : 0.0, animated, 0.2);
      }
      foreach (Path path in this.hearts)
      {
        PortraitView.Fade(path, this.expression == PortraitExpression.Loving, animated, 0.2);
      }
      if (this.caretEyes)
      {
        for (int k = 0; k < this.carets.Count; k++)
        {
          this.carets[k].Set(PortraitArt.CaretEyePath(this.expression, k), animated);
        }
      }
      else
      {
        foreach (UIElement uielement in this.pupils)
        {
          PortraitView.Fade(uielement, this.expression != PortraitExpression.Loving, animated, 0.2);
        }
      }
      foreach (Path path2 in this.cheeks)
      {
        bool flag;
        switch (this.expression)
        {
        case PortraitExpression.Shy:
        case PortraitExpression.Laughing:
        case PortraitExpression.Loving:
          flag = true;
          break;
        case PortraitExpression.Excited:
        case PortraitExpression.Determined:
          goto IL_021F;
        default:
          goto IL_021F;
        }
        IL_0222:
        double num = (flag ? 0.6 : ((this.expression == PortraitExpression.Happy) ? 0.22 : 0.0));
        double num2 = ((this.pack == PortraitPack.StorybookSpirits) ? 0.32 : 0.0);
        PortraitView.Fade(path2, Math.Max(num, num2), animated, 0.2);
        continue;
        IL_021F:
        flag = false;
        goto IL_0222;
      }
      for (int l = 0; l < this.brows.Count; l++)
      {
        bool flag = this.pack == PortraitPack.KiCrew;
        if (!flag)
        {
          PortraitExpression portraitExpression = this.expression;
          bool flag2 = portraitExpression - PortraitExpression.Surprised <= 1 || portraitExpression == PortraitExpression.Determined;
          flag = flag2;
        }
        bool flag3 = flag;
        PortraitView.Fade(this.brows[l], flag3 > false, animated, 0.2);
        double num3 = ((this.expression == PortraitExpression.Determined) ? ((l == 0) ? 0.28 : (-0.28)) : ((l == 0) ? (-0.12) : 0.08));
        PortraitView.Rotate(this.brows[l], PortraitView.Deg(num3), animated, 0.2);
        PortraitView.TranslateTo(this.brows[l], new Vector3(0f, (float)((this.expression == PortraitExpression.Surprised) ? (-8) : 5), 0f), animated, 0.2);
      }
      PortraitView.MorphPath morphPath = this.mouth;
      if (morphPath != null)
      {
        morphPath.Set(PortraitArt.MouthPath(this.expression), animated);
      }
      this.UpdateHead(animated);
    }

    // Token: 0x06000E2C RID: 3628 RVA: 0x00571BB8 File Offset: 0x00571BB8
    [NullableContext(0)]
    public void Look([TupleElementNames(new string[] { "X", "Y" })] ValueTuple<double, double> target, bool animated = true)
    {
      if (!double.IsFinite(target.Item1) || !double.IsFinite(target.Item2))
      {
        return;
      }
      double num = Math.Max(1.0, Math.Sqrt(target.Item1 * target.Item1 + target.Item2 * target.Item2));
      ValueTuple<double, double> valueTuple = new ValueTuple<double, double>(target.Item1 / num, target.Item2 / num);
      animated &= this.CanAnimate;
      if (animated && Math.Abs(valueTuple.Item1 - this.gaze.Item1) + Math.Abs(valueTuple.Item2 - this.gaze.Item2) <= 0.008)
      {
        return;
      }
      this.gaze = valueTuple;
      double num2 = ((this.pack == PortraitPack.ClassicClouds) ? 19.2 : 6.0);
      double num3 = ((this.pack == PortraitPack.ClassicClouds) ? 11.52 : 4.5);
      Vector3 vector = new Vector3((float)(valueTuple.Item1 * num2), (float)(valueTuple.Item2 * num3), 0f);
      if (this.caretEyes)
      {
        foreach (Canvas canvas in this.eyes)
        {
          PortraitView.TranslateTo(canvas, vector, animated, 0.2);
        }
        using (List<PortraitView.MorphPath>.Enumerator enumerator2 = this.lids.GetEnumerator())
        {
          while (enumerator2.MoveNext())
          {
            PortraitView.MorphPath morphPath = enumerator2.Current;
            PortraitView.TranslateTo(morphPath.Root, vector, animated, 0.2);
          }
          goto IL_01DA;
        }
      }
      foreach (UIElement uielement in this.pupils)
      {
        PortraitView.TranslateTo(uielement, vector, animated, 0.2);
      }
      IL_01DA:
      this.hairShiftX = valueTuple.Item1 * 0.9;
      PortraitView.TranslateTo(this.hairBack, new Vector3((float)this.hairShiftX, 0f, 0f), animated, 0.28);
      PortraitView.TranslateTo(this.hairFront, new Vector3((float)this.hairShiftX, 0f, 0f), animated, 0.28);
      this.UpdateHead(animated);
    }

    // Token: 0x06000E2D RID: 3629 RVA: 0x00571E3C File Offset: 0x00571E3C
    [NullableContext(0)]
    public void SetGazeTarget([TupleElementNames(new string[] { "X", "Y" })] ValueTuple<double, double>? target)
    {
      this.gazeTarget = target;
      this.Look(target ?? this.expression.RestingGaze(), this.CanAnimate);
    }

    // Token: 0x06000E2E RID: 3630 RVA: 0x00571E7C File Offset: 0x00571E7C
    private void UpdateHead(bool animated)
    {
      this.rotationDeg = PortraitView.Deg(this.expression.HeadTilt() + this.gaze.Item1 * 0.035);
      PortraitView.Rotate(this.head, this.rotationDeg, animated, 0.2);
      PortraitView.Rotate(this.hairBack, this.rotationDeg, animated, 0.2);
    }

    // Token: 0x06000E2F RID: 3631 RVA: 0x00571EEC File Offset: 0x00571EEC
    public void Blink()
    {
      if (this.ClosedEyes || !this.CanAnimate)
      {
        return;
      }
      for (int i = 0; i < this.eyes.Count; i++)
      {
        float num = (float)this.expression.EyeOpenness(i);
        PortraitView.KeysV3(this.eyes[i], "Scale", new float[] { num, 0.025f, 0.025f, num }, new float[] { 0f, 0.26f, 0.38f, 1f }, 0.19, (float v) => new Vector3(1f, v, 1f));
        PortraitView.Keys(this.eyes[i], "Opacity", new float[] { 1f, 1f, 0f, 0f, 1f, 1f }, new float[] { 0f, 0.16f, 0.26f, 0.38f, 0.65f, 1f }, 0.19);
        UIElement root = this.lids[i].Root;
        string text = "Opacity";
        float[] array = new float[6];
        array[2] = 1f;
        array[3] = 1f;
        PortraitView.Keys(root, text, array, new float[] { 0f, 0.16f, 0.26f, 0.38f, 0.65f, 1f }, 0.19);
      }
    }

    // Token: 0x06000E30 RID: 3632 RVA: 0x00572030 File Offset: 0x00572030
    public void Perform(PortraitReaction reaction)
    {
      if (!this.CanAnimate)
      {
        return;
      }
      float num = (float)PortraitView.Deg(this.expression.HeadTilt());
      UIElement[] array3;
      switch (reaction)
      {
      case PortraitReaction.Wave:
        if (this.pack.IsCloud())
        {
          this.SmileBriefly(PortraitExpression.Happy);
          PortraitView.Keys(this.head, "Rotation", new float[]
          {
            num,
            (float)PortraitView.Deg(-0.09),
            (float)PortraitView.Deg(0.08),
            (float)PortraitView.Deg(-0.04),
            num
          }, null, 1.15);
          UIElement uielement = this.head;
          string text = "Translation";
          float[] array = new float[4];
          array[1] = -6f;
          array[2] = -3f;
          PortraitView.KeysV3(uielement, text, array, null, 1.15, (float v) => new Vector3(0f, v, 0f));
          return;
        }
        this.Nod(-0.04);
        return;
      case PortraitReaction.Wink:
        if (!this.ClosedEyes && this.eyes.Count > 0)
        {
          PortraitView.Keys(this.eyes[0], "Opacity", new float[] { 1f, 0f, 0f, 1f }, new float[] { 0f, 0.18f, 0.65f, 1f }, 0.6);
          UIElement root = this.lids[0].Root;
          string text2 = "Opacity";
          float[] array2 = new float[4];
          array2[1] = 1f;
          array2[2] = 1f;
          PortraitView.Keys(root, text2, array2, new float[] { 0f, 0.18f, 0.65f, 1f }, 0.6);
        }
        this.Nod(-0.065);
        return;
      case PortraitReaction.Boop:
      {
        this.Blink();
        array3 = new UIElement[] { this.head, this.hairBack };
        for (int i = 0; i < array3.Length; i++)
        {
          PortraitView.KeysV3(array3[i], "Scale", new float[] { 1f, 0.96f, 1f }, null, 0.38, (float v) => new Vector3(v, v, 1f));
        }
        using (List<Path>.Enumerator enumerator = this.cheeks.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            Path path = enumerator.Current;
            PortraitView.Keys(path, "Opacity", new float[]
            {
              (float)path.Opacity,
              0.8f,
              (float)this.CheekRest
            }, null, 0.65);
          }
          return;
        }
        break;
      }
      case PortraitReaction.Giggle:
        break;
      case PortraitReaction.Celebrate:
      {
        this.SmileBriefly(PortraitExpression.Excited);
        array3 = new UIElement[] { this.head, this.hairBack };
        for (int i = 0; i < array3.Length; i++)
        {
          UIElement part = array3[i];
          UIElement part2 = part;
          string text3 = "Translation";
          float[] array4 = new float[4];
          array4[1] = 1f;
          array4[2] = -6f;
          PortraitView.KeysV3(part2, text3, array4, new float[] { 0f, 0.12f, 0.4f, 1f }, 0.92, (float v) => new Vector3((part == this.hairBack) ? ((float)this.hairShiftX) : 0f, v, 0f));
        }
        return;
      }
      case PortraitReaction.Dance:
      {
        array3 = new UIElement[] { this.head, this.hairBack };
        for (int i = 0; i < array3.Length; i++)
        {
          PortraitView.Keys(array3[i], "Rotation", new float[]
          {
            num,
            (float)PortraitView.Deg(-0.06),
            (float)PortraitView.Deg(0.06),
            (float)PortraitView.Deg(-0.06),
            (float)PortraitView.Deg(0.06),
            num
          }, null, 1.35);
        }
        return;
      }
      default:
        return;
      }
      this.SmileBriefly(PortraitExpression.Laughing);
      this.Blink();
      array3 = new UIElement[] { this.head, this.hairBack };
      for (int i = 0; i < array3.Length; i++)
      {
        PortraitView.Keys(array3[i], "Rotation", new float[]
        {
          num,
          (float)PortraitView.Deg(-0.055),
          (float)PortraitView.Deg(0.055),
          (float)PortraitView.Deg(-0.035),
          num
        }, null, 0.65);
      }
    }

    // Token: 0x17000292 RID: 658
    // (get) Token: 0x06000E31 RID: 3633 RVA: 0x005724B4 File Offset: 0x005724B4
    private double CheekRest
    {
      get
      {
        bool flag;
        switch (this.expression)
        {
        case PortraitExpression.Shy:
        case PortraitExpression.Laughing:
        case PortraitExpression.Loving:
          flag = true;
          goto IL_002B;
        }
        flag = false;
        IL_002B:
        return Math.Max(flag ? 0.6 : ((this.expression == PortraitExpression.Happy) ? 0.22 : 0.0), (this.pack == PortraitPack.StorybookSpirits) ? 0.32 : 0.0);
      }
    }

    // Token: 0x06000E32 RID: 3634 RVA: 0x0057253C File Offset: 0x0057253C
    private void SmileBriefly(PortraitExpression mood)
    {
      PortraitView.MorphPath morphPath = this.mouth;
      if (morphPath == null)
      {
        return;
      }
      morphPath.Pulse(PortraitArt.MouthPath(mood), 0.75);
    }

    // Token: 0x06000E33 RID: 3635 RVA: 0x00572560 File Offset: 0x00572560
    private void Nod(double amount)
    {
      float num = (float)this.rotationDeg;
      PortraitView.Keys(this.head, "Rotation", new float[]
      {
        num,
        num + (float)PortraitView.Deg(amount),
        num
      }, new float[] { 0f, 0.38f, 1f }, 0.48);
      PortraitView.Keys(this.hairBack, "Rotation", new float[]
      {
        num,
        num + (float)PortraitView.Deg(amount * 0.85),
        num
      }, new float[] { 0f, 0.48f, 1f }, 0.56);
      UIElement uielement = this.hairFront;
      string text = "Rotation";
      float[] array = new float[3];
      array[1] = (float)PortraitView.Deg(amount * 0.2);
      PortraitView.Keys(uielement, text, array, new float[] { 0f, 0.48f, 1f }, 0.56);
    }

    // Token: 0x06000E34 RID: 3636 RVA: 0x0057265C File Offset: 0x0057265C
    private void UpdateActivity()
    {
      if (!this.loaded || !this.CanAnimate)
      {
        DispatcherQueueTimer dispatcherQueueTimer = this.idleTimer;
        if (dispatcherQueueTimer != null)
        {
          dispatcherQueueTimer.Stop();
        }
        this.pointerInside = false;
        return;
      }
      if (!this.IdleLifeEnabled)
      {
        return;
      }
      this.StartBreathing();
      if (this.idleTimer == null)
      {
        this.idleTimer = base.DispatcherQueue.CreateTimer();
        this.idleTimer.IsRepeating = false;
        this.idleTimer.Tick += delegate(DispatcherQueueTimer _, object _)
        {
          this.IdleBeat();
          this.ScheduleIdle(3.4, 6.5);
        };
      }
      this.ScheduleIdle(1.1, 2.8);
    }

    // Token: 0x06000E35 RID: 3637 RVA: 0x005726F8 File Offset: 0x005726F8
    private void ScheduleIdle(double min, double max)
    {
      if (this.idleTimer == null || !this.loaded)
      {
        return;
      }
      this.idleTimer.Interval = TimeSpan.FromSeconds(min + this.rng.NextDouble() * (max - min));
      this.idleTimer.Start();
    }

    // Token: 0x06000E36 RID: 3638 RVA: 0x00572738 File Offset: 0x00572738
    private void IdleBeat()
    {
      if (!this.CanAnimate)
      {
        return;
      }
      if (!this.ClosedEyes)
      {
        this.Blink();
      }
      if (!this.pointerInside)
      {
        ValueTuple<double, double>? valueTuple = this.gazeTarget;
        if (valueTuple == null)
        {
          ValueTuple<double, double> valueTuple2 = this.expression.RestingGaze();
          this.Look(new ValueTuple<double, double>(valueTuple2.Item1 + (this.rng.NextDouble() * 0.44 - 0.22), valueTuple2.Item2 + (this.rng.NextDouble() * 0.24 - 0.12)), true);
        }
      }
    }

    // Token: 0x06000E37 RID: 3639 RVA: 0x005727D8 File Offset: 0x005727D8
    private void StartBreathing()
    {
      if (this.breathing)
      {
        return;
      }
      this.breathing = true;
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = PortraitView.C.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation.InsertKeyFrame(0f, Vector3.Zero);
      vector3KeyFrameAnimation.InsertKeyFrame(1f, new Vector3(0f, -1.4f, 0f), Easing.EaseInOut(PortraitView.C));
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(2.5);
      vector3KeyFrameAnimation.Direction = AnimationDirection.Alternate;
      vector3KeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
      vector3KeyFrameAnimation.Target = "Translation";
      this.breath.StartAnimation(vector3KeyFrameAnimation);
    }

    // Token: 0x06000E38 RID: 3640 RVA: 0x00572874 File Offset: 0x00572874
    private void OnPointerEntered(object sender, PointerRoutedEventArgs e)
    {
      if (!this.TracksPointer || !this.CanAnimate)
      {
        return;
      }
      this.pointerInside = true;
      if (this.BlinksOnPointerEntry)
      {
        ValueTuple<double, double>? valueTuple = this.gazeTarget;
        if (valueTuple == null)
        {
          this.Blink();
        }
      }
      this.OnPointerMoved(sender, e);
    }

    // Token: 0x06000E39 RID: 3641 RVA: 0x005728C0 File Offset: 0x005728C0
    private void OnPointerMoved(object sender, PointerRoutedEventArgs e)
    {
      if (this.TracksPointer && this.CanAnimate)
      {
        ValueTuple<double, double>? valueTuple = this.gazeTarget;
        if (valueTuple == null && this.expression != PortraitExpression.Sleepy)
        {
          this.pointerInside = true;
          ValueTuple<double, double> valueTuple2 = this.DrawingPoint(e.GetCurrentPoint(this).Position);
          double num = (valueTuple2.Item1 - 162.0) / 140.0;
          double num2 = (valueTuple2.Item2 - 164.0) / 145.0;
          double num3 = Math.Max(1.0, Math.Sqrt(num * num + num2 * num2));
          this.Look(new ValueTuple<double, double>(num / num3, num2 / num3), true);
          return;
        }
      }
    }

    // Token: 0x06000E3A RID: 3642 RVA: 0x00572974 File Offset: 0x00572974
    private void OnPointerExited(object sender, PointerRoutedEventArgs e)
    {
      if (!this.TracksPointer || PointerFrames.IsStale(e))
      {
        return;
      }
      this.pointerInside = false;
      this.Look(this.gazeTarget ?? this.expression.RestingGaze(), this.CanAnimate);
    }

    // Token: 0x06000E3B RID: 3643 RVA: 0x005729CC File Offset: 0x005729CC
    private static double Deg(double radians)
    {
      return radians * 180.0 / 3.141592653589793;
    }

    // Token: 0x06000E3C RID: 3644 RVA: 0x005729E4 File Offset: 0x005729E4
    private static void Fade(UIElement e, double to, bool animated, double seconds = 0.2)
    {
      if (!animated)
      {
        e.Opacity = to;
        return;
      }
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = PortraitView.C.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(1f, (float)to, Easing.EaseOut(PortraitView.C));
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(seconds);
      scalarKeyFrameAnimation.Target = "Opacity";
      e.StartAnimation(scalarKeyFrameAnimation);
    }

    // Token: 0x06000E3D RID: 3645 RVA: 0x00572A3C File Offset: 0x00572A3C
    private static void Rotate(UIElement e, double degrees, bool animated, double seconds = 0.2)
    {
      if (!animated)
      {
        e.Rotation = (float)degrees;
        return;
      }
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = PortraitView.C.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(1f, (float)degrees, Easing.EaseOut(PortraitView.C));
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(seconds);
      scalarKeyFrameAnimation.Target = "Rotation";
      e.StartAnimation(scalarKeyFrameAnimation);
    }

    // Token: 0x06000E3E RID: 3646 RVA: 0x00572A98 File Offset: 0x00572A98
    private static void TranslateTo(UIElement e, Vector3 to, bool animated, double seconds = 0.2)
    {
      if (!animated)
      {
        e.Translation = to;
        return;
      }
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = PortraitView.C.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation.InsertKeyFrame(1f, to, Easing.EaseOut(PortraitView.C));
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(seconds);
      vector3KeyFrameAnimation.Target = "Translation";
      e.StartAnimation(vector3KeyFrameAnimation);
    }

    // Token: 0x06000E3F RID: 3647 RVA: 0x00572AF0 File Offset: 0x00572AF0
    private static void ScaleTo(UIElement e, Vector3 to, bool animated, double seconds = 0.2)
    {
      if (!animated)
      {
        e.Scale = to;
        return;
      }
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = PortraitView.C.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation.InsertKeyFrame(1f, to, Easing.EaseOut(PortraitView.C));
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(seconds);
      vector3KeyFrameAnimation.Target = "Scale";
      e.StartAnimation(vector3KeyFrameAnimation);
    }

    // Token: 0x06000E40 RID: 3648 RVA: 0x00572B48 File Offset: 0x00572B48
    private static void Keys(UIElement e, string target, float[] values, [Nullable(2)] float[] times, double seconds)
    {
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = PortraitView.C.CreateScalarKeyFrameAnimation();
      CubicBezierEasingFunction cubicBezierEasingFunction = Easing.EaseInOut(PortraitView.C);
      scalarKeyFrameAnimation.InsertExpressionKeyFrame(0f, "this.StartingValue");
      for (int i = 1; i < values.Length; i++)
      {
        scalarKeyFrameAnimation.InsertKeyFrame((times != null) ? times[i] : ((float)i / (float)(values.Length - 1)), values[i], cubicBezierEasingFunction);
      }
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(seconds);
      scalarKeyFrameAnimation.Target = target;
      e.StartAnimation(scalarKeyFrameAnimation);
    }

    // Token: 0x06000E41 RID: 3649 RVA: 0x00572BC0 File Offset: 0x00572BC0
    private static void KeysV3(UIElement e, string target, float[] values, [Nullable(2)] float[] times, double seconds, Func<float, Vector3> map)
    {
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = PortraitView.C.CreateVector3KeyFrameAnimation();
      CubicBezierEasingFunction cubicBezierEasingFunction = Easing.EaseInOut(PortraitView.C);
      vector3KeyFrameAnimation.InsertExpressionKeyFrame(0f, "this.StartingValue");
      for (int i = 1; i < values.Length; i++)
      {
        vector3KeyFrameAnimation.InsertKeyFrame((times != null) ? times[i] : ((float)i / (float)(values.Length - 1)), map(values[i]), cubicBezierEasingFunction);
      }
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(seconds);
      vector3KeyFrameAnimation.Target = target;
      e.StartAnimation(vector3KeyFrameAnimation);
    }

    // Token: 0x06000E45 RID: 3653 RVA: 0x00572C80 File Offset: 0x00572C80
    [CompilerGenerated]
    internal static double <Tinted>g__F|61_0(byte v, ref PortraitView.<>c__DisplayClass61_0 A_1)
    {
      double num = (double)v / 255.0;
      return num + (A_1.toward - num) * A_1.amount;
    }

    // Token: 0x04000589 RID: 1417
    private readonly PortraitAppearance a;

    // Token: 0x0400058A RID: 1418
    private readonly PortraitPack pack;

    // Token: 0x0400058B RID: 1419
    private readonly bool caretEyes;

    // Token: 0x0400058C RID: 1420
    private readonly Canvas stage = new Canvas
    {
      Width = 320.0,
      Height = 400.0,
      IsHitTestVisible = false
    };

    // Token: 0x0400058D RID: 1421
    private readonly Canvas breath = new Canvas
    {
      Width = 320.0,
      Height = 400.0
    };

    // Token: 0x0400058E RID: 1422
    private readonly Canvas hairBack = new Canvas
    {
      Width = 320.0,
      Height = 400.0
    };

    // Token: 0x0400058F RID: 1423
    private readonly Canvas head = new Canvas
    {
      Width = 320.0,
      Height = 400.0
    };

    // Token: 0x04000590 RID: 1424
    private readonly Canvas hairFront = new Canvas
    {
      Width = 320.0,
      Height = 400.0
    };

    // Token: 0x04000591 RID: 1425
    private readonly Grid art = new Grid();

    // Token: 0x04000592 RID: 1426
    private readonly List<Canvas> eyes = new List<Canvas>();

    // Token: 0x04000593 RID: 1427
    private readonly List<UIElement> pupils = new List<UIElement>();

    // Token: 0x04000594 RID: 1428
    private readonly List<PortraitView.MorphPath> carets = new List<PortraitView.MorphPath>();

    // Token: 0x04000595 RID: 1429
    private readonly List<PortraitView.MorphPath> lids = new List<PortraitView.MorphPath>();

    // Token: 0x04000596 RID: 1430
    private readonly List<Path> hearts = new List<Path>();

    // Token: 0x04000597 RID: 1431
    private readonly List<Path> cheeks = new List<Path>();

    // Token: 0x04000598 RID: 1432
    private readonly List<Canvas> brows = new List<Canvas>();

    // Token: 0x04000599 RID: 1433
    [TupleElementNames(new string[] { "X", "Y" })]
    [Nullable(new byte[] { 1, 0 })]
    private readonly List<ValueTuple<double, double>> eyeCenters = new List<ValueTuple<double, double>>();

    // Token: 0x0400059A RID: 1434
    [Nullable(2)]
    private PortraitView.MorphPath mouth;

    // Token: 0x0400059B RID: 1435
    [Nullable(2)]
    private Path paint;

    // Token: 0x0400059C RID: 1436
    [Nullable(2)]
    private Ellipse plate;

    // Token: 0x0400059D RID: 1437
    private PortraitExpression expression;

    // Token: 0x0400059E RID: 1438
    [TupleElementNames(new string[] { "X", "Y" })]
    [Nullable(0)]
    private ValueTuple<double, double> gaze;

    // Token: 0x0400059F RID: 1439
    [TupleElementNames(new string[] { "X", "Y" })]
    [Nullable(0)]
    private ValueTuple<double, double>? gazeTarget;

    // Token: 0x040005A0 RID: 1440
    private double rotationDeg;

    // Token: 0x040005A1 RID: 1441
    private double hairShiftX;

    // Token: 0x040005A2 RID: 1442
    private bool live;

    // Token: 0x040005A3 RID: 1443
    private bool loaded;

    // Token: 0x040005A4 RID: 1444
    private bool pointerInside;

    // Token: 0x040005A5 RID: 1445
    private bool breathing;

    // Token: 0x040005A6 RID: 1446
    [Nullable(2)]
    private DispatcherQueueTimer idleTimer;

    // Token: 0x040005A7 RID: 1447
    private readonly Random rng = new Random();

    // Token: 0x040005A8 RID: 1448
    private static readonly CanvasDevice Device = CanvasDevice.GetSharedDevice();

    // Token: 0x040005AC RID: 1452
    private double scale;

    // Token: 0x020005E8 RID: 1512
    [Nullable(0)]
    private sealed class MorphPath
    {
      // Token: 0x06003011 RID: 12305 RVA: 0x006668D8 File Offset: 0x006668D8
      public MorphPath(Color? fill, Color? stroke, double lineWidth, PPath initial)
      {
        for (int i = 0; i < 2; i++)
        {
          this.paths[i] = PortraitView.Shape(initial, fill, stroke, lineWidth);
          this.paths[i].Opacity = i == 0;
          this.Root.Children.Add(this.paths[i]);
        }
      }

      // Token: 0x06003012 RID: 12306 RVA: 0x00666968 File Offset: 0x00666968
      public void Set(PPath path, bool animated)
      {
        this.current = path;
        if (!animated)
        {
          this.paths[this.active].Data = path.ToXaml();
          this.paths[this.active].Opacity = 1.0;
          this.paths[1 - this.active].Opacity = 0.0;
          return;
        }
        Path path2 = this.paths[1 - this.active];
        path2.Data = path.ToXaml();
        PortraitView.Fade(path2, 1.0, true, 0.2);
        PortraitView.Fade(this.paths[this.active], 0.0, true, 0.2);
        this.active = 1 - this.active;
      }

      // Token: 0x06003013 RID: 12307 RVA: 0x00666A38 File Offset: 0x00666A38
      public void Pulse(PPath mood, double seconds)
      {
        Path path = this.paths[this.active];
        Path path2 = this.paths[1 - this.active];
        path2.Data = mood.ToXaml();
        float[] array = new float[] { 0f, 0.18f, 0.7f, 1f };
        PortraitView.Keys(path, "Opacity", new float[] { 1f, 0f, 0f, 1f }, array, seconds);
        UIElement uielement = path2;
        string text = "Opacity";
        float[] array2 = new float[4];
        array2[1] = 1f;
        array2[2] = 1f;
        PortraitView.Keys(uielement, text, array2, array, seconds);
      }

      // Token: 0x04001B34 RID: 6964
      public readonly Canvas Root = new Canvas
      {
        Width = 320.0,
        Height = 400.0
      };

      // Token: 0x04001B35 RID: 6965
      private readonly Path[] paths = new Path[2];

      // Token: 0x04001B36 RID: 6966
      private int active;

      // Token: 0x04001B37 RID: 6967
      [Nullable(2)]
      private PPath current;
    }
  }
}
