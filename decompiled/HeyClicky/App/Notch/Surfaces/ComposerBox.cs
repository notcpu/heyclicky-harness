using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Input;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using WinRT.Interop;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x0200015B RID: 347
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ComposerBox : Grid
  {
    // Token: 0x17000379 RID: 889
    // (get) Token: 0x06001322 RID: 4898 RVA: 0x0059E8F0 File Offset: 0x0059E8F0
    public TextBox Input { get; }

    // Token: 0x1400004F RID: 79
    // (add) Token: 0x06001323 RID: 4899 RVA: 0x0059E8F8 File Offset: 0x0059E8F8
    // (remove) Token: 0x06001324 RID: 4900 RVA: 0x0059E930 File Offset: 0x0059E930
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    [field: Nullable(new byte[] { 2, 1, 1, 1 })]
    public event Action<string, IReadOnlyList<string>> Submitted;

    // Token: 0x14000050 RID: 80
    // (add) Token: 0x06001325 RID: 4901 RVA: 0x0059E968 File Offset: 0x0059E968
    // (remove) Token: 0x06001326 RID: 4902 RVA: 0x0059E9A0 File Offset: 0x0059E9A0
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Cancelled;

    // Token: 0x14000051 RID: 81
    // (add) Token: 0x06001327 RID: 4903 RVA: 0x0059E9D8 File Offset: 0x0059E9D8
    // (remove) Token: 0x06001328 RID: 4904 RVA: 0x0059EA10 File Offset: 0x0059EA10
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<int, int> MetricsChanged;

    // Token: 0x14000052 RID: 82
    // (add) Token: 0x06001329 RID: 4905 RVA: 0x0059EA48 File Offset: 0x0059EA48
    // (remove) Token: 0x0600132A RID: 4906 RVA: 0x0059EA80 File Offset: 0x0059EA80
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<bool> FocusChanged;

    // Token: 0x1700037A RID: 890
    // (get) Token: 0x0600132B RID: 4907 RVA: 0x0059EAB8 File Offset: 0x0059EAB8
    public IReadOnlyList<string> Attachments
    {
      get
      {
        return this.attachments;
      }
    }

    // Token: 0x1700037B RID: 891
    // (get) Token: 0x0600132C RID: 4908 RVA: 0x0059EAC0 File Offset: 0x0059EAC0
    // (set) Token: 0x0600132D RID: 4909 RVA: 0x0059EAC8 File Offset: 0x0059EAC8
    [Nullable(2)]
    public Func<IntPtr> WindowHandle
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x0600132E RID: 4910 RVA: 0x0059EAD4 File Offset: 0x0059EAD4
    public void RemoveAttachButton()
    {
      if (this.attach == null || !this.row.Children.Contains(this.attach))
      {
        return;
      }
      int num = Grid.GetColumn(this.attach);
      this.row.Children.Remove(this.attach);
      this.row.ColumnDefinitions.RemoveAt(num);
      Grid.SetColumn(this.send, num);
    }

    // Token: 0x0600132F RID: 4911 RVA: 0x0059EB44 File Offset: 0x0059EB44
    public ComposerBox(string placeholderText, bool showAttach)
    {
      base.Margin = new Thickness(8.0, 0.0, 8.0, 0.0);
      Border border = new Border
      {
        CornerRadius = new CornerRadius(14.0),
        Background = Ui.Brush(DS.Colors.NotchCardSurface),
        BorderBrush = Ui.Brush(DS.White(0.14)),
        BorderThickness = new Thickness(1.0)
      };
      base.Children.Add(border);
      this.Input = new TextBox
      {
        FontSize = 13.0,
        FontFamily = DS.Fonts.Text,
        Foreground = Ui.Brush(Colors.White),
        Background = Ui.Transparent,
        BorderThickness = new Thickness(0.0),
        Padding = new Thickness(0.0),
        MinHeight = 0.0,
        AcceptsReturn = true,
        TextWrapping = TextWrapping.Wrap,
        IsSpellCheckEnabled = false,
        IsTextScaleFactorEnabled = false,
        SelectionHighlightColor = Ui.Brush(DS.Colors.CursorBlue),
        VerticalAlignment = VerticalAlignment.Center
      };
      ComposerBox.StripChrome(this.Input);
      AutomationProperties.SetName(this.Input, "Message HeyClicky");
      ScrollViewer.SetVerticalScrollBarVisibility(this.Input, ScrollBarVisibility.Auto);
      this.placeholderText = placeholderText;
      this.placeholder = Ui.Text(placeholderText, 13.0, FontWeights.Normal, DS.White(0.45), TextAlignment.Left, 1, false, false);
      this.placeholder.IsHitTestVisible = false;
      this.placeholder.VerticalAlignment = VerticalAlignment.Center;
      this.measurer = new TextBlock
      {
        FontSize = 13.0,
        FontFamily = DS.Fonts.Text,
        TextWrapping = TextWrapping.Wrap,
        IsTextScaleFactorEnabled = false
      };
      this.fieldHost = new Grid
      {
        VerticalAlignment = VerticalAlignment.Center
      };
      this.fieldHost.Children.Add(this.Input);
      this.fieldHost.Children.Add(this.placeholder);
      this.send = new Pressable(true)
      {
        Width = 24.0,
        Height = 24.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      this.sendGel = new GelSurface(GelAppearance.PrimaryBlue, 12.0);
      this.sendGlyph = Icons.Glyph("\ue74a", 10.5, Colors.Black);
      this.sendGlyph.FontWeight = FontWeights.Bold;
      this.send.Children.Add(this.sendGel);
      this.send.Children.Add(this.sendGlyph);
      this.send.Click += this.Submit;
      this.send.Tip("Send");
      this.row = new Grid
      {
        ColumnSpacing = 10.0,
        Margin = new Thickness(12.0, 0.0, 8.0, 0.0)
      };
      this.row.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      this.row.Children.Add(this.fieldHost);
      int num = 1;
      if (showAttach)
      {
        this.attach = new Pressable(true)
        {
          Width = 24.0,
          Height = 24.0,
          VerticalAlignment = VerticalAlignment.Center
        };
        TextBlock clip = Icons.Glyph("\ue723", 11.5, DS.White(0.65));
        clip.FontWeight = FontWeights.Medium;
        this.attach.Children.Add(clip);
        this.attach.WithPressScale(0.94f, 0.12, true);
        this.attach.PressedChanged += delegate(bool p)
        {
          Anim.Opacity(clip, p ? 0.85f : 1f, 0.12, null, 0.0);
        };
        this.attach.Click += this.PickFiles;
        this.attach.Tip("Attach files");
        this.row.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        Grid.SetColumn(this.attach, num++);
        this.row.Children.Add(this.attach);
      }
      this.row.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetColumn(this.send, num);
      this.row.Children.Add(this.send);
      this.chipRow = new ScrollViewer
      {
        Content = this.chips,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
        VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
        HorizontalScrollMode = ScrollMode.Enabled,
        VerticalScrollMode = ScrollMode.Disabled,
        Height = 46.0,
        Margin = new Thickness(0.0, 4.0, 0.0, 0.0),
        Visibility = Visibility.Collapsed
      };
      this.chips.Margin = new Thickness(12.0, 0.0, 12.0, 0.0);
      this.column = new StackPanel
      {
        Spacing = 4.0,
        Margin = new Thickness(0.0, 8.0, 0.0, 8.0)
      };
      this.column.Children.Add(this.chipRow);
      this.column.Children.Add(this.row);
      base.Children.Add(this.column);
      this.dropZone = this.BuildDropZone();
      base.Children.Add(this.dropZone);
      this.Input.TextChanged += delegate(object _, TextChangedEventArgs _)
      {
        this.placeholder.Visibility = ((this.Input.Text.Length == 0) ? Visibility.Visible : Visibility.Collapsed);
        this.Remeasure();
        this.UpdateSend();
      };
      this.Input.PreviewKeyDown += this.OnKey;
      this.Input.GotFocus += delegate(object _, RoutedEventArgs _)
      {
        Action<bool> focusChanged = this.FocusChanged;
        if (focusChanged == null)
        {
          return;
        }
        focusChanged(true);
      };
      this.Input.LostFocus += delegate(object _, RoutedEventArgs _)
      {
        Action<bool> focusChanged2 = this.FocusChanged;
        if (focusChanged2 == null)
        {
          return;
        }
        focusChanged2(false);
      };
      this.Input.Paste += this.OnPaste;
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        this.Remeasure();
      };
      this.UpdateSend();
      this.Input.Loaded += delegate(object _, RoutedEventArgs _)
      {
        this.placeholder.Visibility = ((this.Input.Text.Length == 0) ? Visibility.Visible : Visibility.Collapsed);
        this.UpdateSend();
      };
      this.ApplyFieldHeight();
    }

    // Token: 0x06001330 RID: 4912 RVA: 0x0059F260 File Offset: 0x0059F260
    public static void StripChrome(TextBox t)
    {
      ResourceDictionary resources = t.Resources;
      foreach (string text in new string[] { "TextControlBackground", "TextControlBackgroundPointerOver", "TextControlBackgroundFocused", "TextControlBackgroundDisabled", "TextControlBorderBrush", "TextControlBorderBrushPointerOver", "TextControlBorderBrushFocused", "TextControlBorderBrushDisabled" })
      {
        resources[text] = Ui.Transparent;
      }
      foreach (string text2 in new string[] { "TextControlForeground", "TextControlForegroundPointerOver", "TextControlForegroundFocused" })
      {
        resources[text2] = Ui.Brush(Colors.White);
      }
      resources["TextControlPlaceholderForeground"] = Ui.Brush(DS.White(0.45));
      resources["TextControlBorderThemeThickness"] = new Thickness(0.0);
      resources["TextControlBorderThemeThicknessFocused"] = new Thickness(0.0);
      resources["TextControlThemePadding"] = new Thickness(0.0);
      resources["TextControlThemeMinHeight"] = 0.0;
    }

    // Token: 0x1700037C RID: 892
    // (get) Token: 0x06001331 RID: 4913 RVA: 0x0059F3B0 File Offset: 0x0059F3B0
    // (set) Token: 0x06001332 RID: 4914 RVA: 0x0059F3B8 File Offset: 0x0059F3B8
    public static double LineHeight { get; private set; } = 16.0;

    // Token: 0x06001333 RID: 4915 RVA: 0x0059F3C0 File Offset: 0x0059F3C0
    public static void CalibrateLineHeight()
    {
      TextBlock textBlock = new TextBlock();
      textBlock.FontSize = 13.0;
      textBlock.FontFamily = DS.Fonts.Text;
      textBlock.Text = "Ag";
      textBlock.IsTextScaleFactorEnabled = false;
      textBlock.Measure(new Size(1000f, 1000f));
      ComposerBox.LineHeight = Math.Ceiling(textBlock.DesiredSize.Height);
      NotchGeometry.InputLineHeight = ComposerBox.LineHeight;
    }

    // Token: 0x06001334 RID: 4916 RVA: 0x0059F434 File Offset: 0x0059F434
    private void Remeasure()
    {
      double actualWidth = this.fieldHost.ActualWidth;
      if (actualWidth <= 0.0)
      {
        return;
      }
      string text = this.Input.Text;
      if (text.Length == 0)
      {
        text = " ";
      }
      if (text.EndsWith('\r') || text.EndsWith('\n'))
      {
        text += " ";
      }
      this.measurer.Width = actualWidth;
      this.measurer.Text = text;
      this.measurer.Measure(new Size(actualWidth, double.PositiveInfinity));
      int num = Math.Max(1, (int)Math.Round(this.measurer.DesiredSize.Height / ComposerBox.LineHeight));
      if (num != this.lines)
      {
        this.lines = num;
        this.ApplyFieldHeight();
        Action<int, int> metricsChanged = this.MetricsChanged;
        if (metricsChanged == null)
        {
          return;
        }
        metricsChanged(Math.Max(0, this.lines - 1), this.attachments.Count);
      }
    }

    // Token: 0x06001335 RID: 4917 RVA: 0x0059F52C File Offset: 0x0059F52C
    private void ApplyFieldHeight()
    {
      double num = (double)Math.Clamp(this.lines, 1, 8) * ComposerBox.LineHeight + 6.0;
      this.Input.Height = num;
      this.fieldHost.Height = num;
      double num2 = Math.Max(24.0, num);
      this.row.Height = num2;
      VerticalAlignment verticalAlignment = ((this.lines <= 1) ? VerticalAlignment.Center : VerticalAlignment.Bottom);
      this.send.VerticalAlignment = verticalAlignment;
      if (this.attach != null)
      {
        this.attach.VerticalAlignment = verticalAlignment;
      }
      this.placeholder.VerticalAlignment = VerticalAlignment.Center;
    }

    // Token: 0x06001336 RID: 4918 RVA: 0x0059F5C8 File Offset: 0x0059F5C8
    private void OnKey(object sender, KeyRoutedEventArgs e)
    {
      bool flag = InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Shift).HasFlag(CoreVirtualKeyStates.Down);
      if (e.Key == VirtualKey.Enter && !flag)
      {
        e.Handled = true;
        this.Submit();
        return;
      }
      if (e.Key == VirtualKey.Escape)
      {
        e.Handled = true;
        Action cancelled = this.Cancelled;
        if (cancelled == null)
        {
          return;
        }
        cancelled();
      }
    }

    // Token: 0x1700037D RID: 893
    // (get) Token: 0x06001337 RID: 4919 RVA: 0x0059F62C File Offset: 0x0059F62C
    private bool HasContent
    {
      get
      {
        return this.Input.Text.Trim().Length > 0 || this.attachments.Count > 0;
      }
    }

    // Token: 0x06001338 RID: 4920 RVA: 0x0059F658 File Offset: 0x0059F658
    private void UpdateSend()
    {
      bool hasContent = this.HasContent;
      this.sendGel.Enabled = hasContent;
      this.sendGlyph.Foreground = Ui.Brush(hasContent ? Colors.Black : DS.Colors.GelDisabledContent);
      this.send.Enabled = hasContent;
    }

    // Token: 0x06001339 RID: 4921 RVA: 0x0059F6A4 File Offset: 0x0059F6A4
    private void Submit()
    {
      if (!this.HasContent)
      {
        return;
      }
      string text = this.Input.Text.Trim();
      List<string> list = this.attachments.ToList<string>();
      this.Input.Text = "";
      this.attachments.Clear();
      this.chips.Children.Clear();
      this.chipRow.Visibility = Visibility.Collapsed;
      this.UpdateSend();
      this.UpdateAttachButton();
      Action<string, IReadOnlyList<string>> submitted = this.Submitted;
      if (submitted == null)
      {
        return;
      }
      submitted(text, list);
    }

    // Token: 0x0600133A RID: 4922 RVA: 0x0059F72C File Offset: 0x0059F72C
    public void FocusInput()
    {
      this.Input.Focus(FocusState.Programmatic);
    }

    // Token: 0x0600133B RID: 4923 RVA: 0x0059F73C File Offset: 0x0059F73C
    private async void PickFiles()
    {
      try
      {
        FileOpenPicker fileOpenPicker = new FileOpenPicker();
        fileOpenPicker.FileTypeFilter.Add("*");
        Func<IntPtr> windowHandle = this.WindowHandle;
        IntPtr? intPtr = ((windowHandle != null) ? new IntPtr?(windowHandle()) : null);
        if (intPtr != null)
        {
          IntPtr valueOrDefault = intPtr.GetValueOrDefault();
          if (valueOrDefault != 0)
          {
            InitializeWithWindow.Initialize(fileOpenPicker, valueOrDefault);
          }
        }
        IReadOnlyList<StorageFile> readOnlyList = await fileOpenPicker.PickMultipleFilesAsync();
        this.AddAttachments(readOnlyList.Select<StorageFile, string>((StorageFile f) => f.Path));
      }
      catch (Exception ex)
      {
        Log.Warn("composer", "file picker failed: " + ex.Message);
      }
    }

    // Token: 0x0600133C RID: 4924 RVA: 0x0059F774 File Offset: 0x0059F774
    private async void OnPaste(object sender, TextControlPasteEventArgs e)
    {
      if (this.attach != null)
      {
        DataPackageView content = Clipboard.GetContent();
        if (content.Contains(StandardDataFormats.StorageItems))
        {
          e.Handled = true;
          try
          {
            IReadOnlyList<IStorageItem> readOnlyList = await content.GetStorageItemsAsync();
            IReadOnlyList<string> readOnlyList2 = ComposerAttachmentPolicy.PastableImageFiles(readOnlyList.Select<IStorageItem, string>((IStorageItem i) => i.Path));
            if (readOnlyList2.Count > 0)
            {
              this.AddAttachments(readOnlyList2);
            }
            string text = "composer";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 2);
            defaultInterpolatedStringHandler.AppendLiteral("pasted ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(readOnlyList.Count);
            defaultInterpolatedStringHandler.AppendLiteral(" copied file(s): ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(readOnlyList2.Count);
            defaultInterpolatedStringHandler.AppendLiteral(" image(s) attached");
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
          }
          catch (Exception ex)
          {
            Log.Warn("composer", "paste files failed: " + ex.Message);
          }
        }
        else if (content.Contains(StandardDataFormats.Bitmap))
        {
          e.Handled = true;
          try
          {
            RandomAccessStreamReference randomAccessStreamReference = await content.GetBitmapAsync();
            string tempPath = Path.GetTempPath();
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 1);
            defaultInterpolatedStringHandler.AppendLiteral("clicky-paste-");
            defaultInterpolatedStringHandler.AppendFormatted<long>(DateTimeOffset.Now.ToUnixTimeMilliseconds());
            defaultInterpolatedStringHandler.AppendLiteral(".png");
            string path = Path.Combine(tempPath, defaultInterpolatedStringHandler.ToStringAndClear());
            using (IRandomAccessStreamWithContentType src = await randomAccessStreamReference.OpenReadAsync())
            {
              using (FileStream dst = File.Create(path))
              {
                await src.AsStreamForRead().CopyToAsync(dst);
              }
              FileStream dst = null;
            }
            IRandomAccessStreamWithContentType src = null;
            this.AddAttachment(path);
            path = null;
          }
          catch (Exception ex2)
          {
            Log.Warn("composer", "paste image failed: " + ex2.Message);
          }
        }
      }
    }

    // Token: 0x0600133D RID: 4925 RVA: 0x0059F7B4 File Offset: 0x0059F7B4
    public void AddAttachment(string path)
    {
      this.AddAttachments(new <>z__ReadOnlySingleElementList<string>(path));
    }

    // Token: 0x0600133E RID: 4926 RVA: 0x0059F7C4 File Offset: 0x0059F7C4
    public int AddAttachments(IEnumerable<string> paths)
    {
      ValueTuple<IReadOnlyList<string>, bool> valueTuple = ComposerAttachmentPolicy.Admit(this.attachments.Count, paths);
      IReadOnlyList<string> item = valueTuple.Item1;
      bool item2 = valueTuple.Item2;
      foreach (string text in item)
      {
        this.AddChip(text);
      }
      if (item2)
      {
        this.ShowCapNotice();
      }
      if (item.Count == 0)
      {
        return 0;
      }
      this.chipRow.Visibility = Visibility.Visible;
      this.AttachmentsDidChange();
      return item.Count;
    }

    // Token: 0x0600133F RID: 4927 RVA: 0x0059F858 File Offset: 0x0059F858
    private void AddChip(string path)
    {
      this.attachments.Add(path);
      Grid tile = new Grid
      {
        Width = 46.0,
        Height = 46.0
      };
      tile.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(11.0),
        Background = Ui.Brush(DS.White(0.3))
      });
      Border border = new Border
      {
        Margin = new Thickness(3.0),
        CornerRadius = new CornerRadius(8.0),
        Background = Ui.Brush(DS.Colors.Surface2)
      };
      string text = Path.GetExtension(path).ToLowerInvariant();
      bool flag;
      if (text != null)
      {
        int length = text.Length;
        if (length != 4)
        {
          if (length != 5)
          {
            goto IL_01B6;
          }
          char c = text[1];
          if (c != 'j')
          {
            if (c != 't')
            {
              if (c != 'w')
              {
                goto IL_01B6;
              }
              if (!(text == ".webp"))
              {
                goto IL_01B6;
              }
            }
            else if (!(text == ".tiff"))
            {
              goto IL_01B6;
            }
          }
          else if (!(text == ".jpeg"))
          {
            goto IL_01B6;
          }
        }
        else
        {
          char c = text[1];
          if (c <= 'g')
          {
            if (c != 'b')
            {
              if (c != 'g')
              {
                goto IL_01B6;
              }
              if (!(text == ".gif"))
              {
                goto IL_01B6;
              }
            }
            else if (!(text == ".bmp"))
            {
              goto IL_01B6;
            }
          }
          else if (c != 'j')
          {
            if (c != 'p')
            {
              goto IL_01B6;
            }
            if (!(text == ".png"))
            {
              goto IL_01B6;
            }
          }
          else if (!(text == ".jpg"))
          {
            goto IL_01B6;
          }
        }
        flag = true;
        goto IL_01B9;
      }
      IL_01B6:
      flag = false;
      IL_01B9:
      if (flag)
      {
        border.Background = new ImageBrush
        {
          ImageSource = new BitmapImage(new Uri(path))
          {
            DecodePixelWidth = 120
          },
          Stretch = Stretch.UniformToFill
        };
      }
      else
      {
        border.Background = Ui.Transparent;
        ComposerBox.LoadShellIconAsync(border, path);
      }
      tile.Children.Add(border);
      Pressable remove = new Pressable(true)
      {
        Width = 13.0,
        Height = 13.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Top,
        Margin = new Thickness(0.0, 3.0, 3.0, 0.0),
        Opacity = 0.0,
        IsHitTestVisible = false
      }.Named("Remove attachment");
      remove.Children.Add(new Ellipse
      {
        Fill = Ui.Brush(DS.Black(0.6))
      });
      TextBlock textBlock = Icons.Glyph("\ue711", 7.0, Colors.White);
      textBlock.FontWeight = FontWeights.Bold;
      textBlock.HorizontalAlignment = HorizontalAlignment.Center;
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      remove.Children.Add(textBlock);
      remove.Click += delegate
      {
        this.RemoveAttachment(path, tile);
      };
      tile.Children.Add(remove);
      tile.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        remove.IsHitTestVisible = true;
        Anim.Opacity(remove, 1f, 0.12, null, 0.0);
      };
      tile.PointerExited += delegate(object _, PointerRoutedEventArgs e)
      {
        if (PointerFrames.IsStale(e))
        {
          return;
        }
        remove.IsHitTestVisible = false;
        Anim.Opacity(remove, 0f, 0.12, null, 0.0);
      };
      ToolTipService.SetToolTip(tile, Path.GetFileName(path));
      this.chips.Children.Add(tile);
    }

    // Token: 0x06001340 RID: 4928 RVA: 0x0059FBFC File Offset: 0x0059FBFC
    public static async Task LoadShellIconAsync(Border inner, string path)
    {
      try
      {
        BitmapImage bmp;
        using (StorageItemThumbnail thumb = await (await StorageFile.GetFileFromPathAsync(path)).GetThumbnailAsync(ThumbnailMode.SingleItem, 80U))
        {
          if (thumb == null)
          {
            throw new InvalidOperationException("no icon");
          }
          bmp = new BitmapImage();
          await bmp.SetSourceAsync(thumb);
          inner.Child = new Image
          {
            Source = bmp,
            Width = 40.0,
            Height = 40.0,
            Stretch = Stretch.Uniform
          };
        }
        StorageItemThumbnail thumb = null;
        bmp = null;
      }
      catch
      {
        inner.Background = Ui.Brush(DS.Colors.Surface2);
        inner.Child = Icons.Glyph("\ue8a5", 16.0, DS.Colors.TextSecondary);
      }
    }

    // Token: 0x06001341 RID: 4929 RVA: 0x0059FC48 File Offset: 0x0059FC48
    private void RemoveAttachment(string path, FrameworkElement tile)
    {
      this.attachments.Remove(path);
      this.chips.Children.Remove(tile);
      if (this.attachments.Count == 0)
      {
        this.chipRow.Visibility = Visibility.Collapsed;
      }
      this.AttachmentsDidChange();
    }

    // Token: 0x06001342 RID: 4930 RVA: 0x0059FC88 File Offset: 0x0059FC88
    internal bool RemoveAttachmentAt(int index)
    {
      if (index < 0 || index >= this.chips.Children.Count || index >= this.attachments.Count)
      {
        return false;
      }
      this.RemoveAttachment(this.attachments[index], (FrameworkElement)this.chips.Children[index]);
      return true;
    }

    // Token: 0x06001343 RID: 4931 RVA: 0x0059FCE8 File Offset: 0x0059FCE8
    private void AttachmentsDidChange()
    {
      this.UpdateSend();
      this.UpdateAttachButton();
      Action<int, int> metricsChanged = this.MetricsChanged;
      if (metricsChanged != null)
      {
        metricsChanged(Math.Max(0, this.lines - 1), this.attachments.Count);
      }
      Action attachmentsChanged = this.AttachmentsChanged;
      if (attachmentsChanged == null)
      {
        return;
      }
      attachmentsChanged();
    }

    // Token: 0x06001344 RID: 4932 RVA: 0x0059FD3C File Offset: 0x0059FD3C
    private void UpdateAttachButton()
    {
      if (this.attach == null)
      {
        return;
      }
      bool flag = this.attachments.Count >= 10;
      this.attach.Enabled = !flag;
      this.attach.Opacity = (flag ? 0.4615384615384615 : 1.0);
      Pressable pressable = this.attach;
      string text;
      if (!flag)
      {
        text = "Attach files";
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
        defaultInterpolatedStringHandler.AppendLiteral("Up to ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(10);
        defaultInterpolatedStringHandler.AppendLiteral(" files");
        text = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      pressable.Tip(text);
    }

    // Token: 0x06001345 RID: 4933 RVA: 0x0059FDE0 File Offset: 0x0059FDE0
    private static Grid WarningTriangleFill(double size, Color knockout)
    {
      TextBlock textBlock = Icons.Glyph("\uf139", size, Colors.White);
      TextBlock textBlock2 = Ui.Text("!", size * 0.72, FontWeights.Black, knockout, TextAlignment.Center, 0, false, false);
      textBlock2.Margin = new Thickness(0.0, size * 0.18, 0.0, 0.0);
      textBlock2.VerticalAlignment = VerticalAlignment.Center;
      return new Grid
      {
        VerticalAlignment = VerticalAlignment.Center,
        Children = { textBlock, textBlock2 }
      };
    }

    // Token: 0x06001346 RID: 4934 RVA: 0x0059FE7C File Offset: 0x0059FE7C
    private void ShowCapNotice()
    {
      if (this.capNotice == null)
      {
        Grid grid = ComposerBox.WarningTriangleFill(9.0, DS.Colors.Warning);
        TextBlock textBlock = Ui.Text(ComposerAttachmentPolicy.CapNotice, 10.5, FontWeights.SemiBold, Colors.White, TextAlignment.Left, 0, false, false);
        textBlock.VerticalAlignment = VerticalAlignment.Center;
        Border border = new Border
        {
          CornerRadius = new CornerRadius(100.0),
          Background = Ui.Brush(DS.Colors.Warning),
          Padding = new Thickness(9.0, 3.0, 9.0, 3.0),
          Child = Ui.H(4.0, new UIElement[] { grid, textBlock })
        }.Capsule();
        ShadowLayer shadowLayer = new ShadowLayer(100.0, DS.Black(0.3), 4.0, 1.0, 0.0);
        this.capNotice = new Grid
        {
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Top,
          Margin = new Thickness(0.0, 5.0, 0.0, 0.0),
          IsHitTestVisible = false,
          Opacity = 0.0,
          Tag = "attachment-cap-notice",
          Children = { shadowLayer, border }
        };
        base.Children.Add(this.capNotice);
        this.capNoticeTimer = base.DispatcherQueue.CreateTimer();
        this.capNoticeTimer.IsRepeating = false;
        this.capNoticeTimer.Interval = ComposerAttachmentPolicy.CapNoticeDuration;
        this.capNoticeTimer.Tick += delegate(Microsoft.UI.Dispatching.DispatcherQueueTimer _, object _)
        {
          this.capNoticeShowing = false;
          if (this.capNotice != null)
          {
            Anim.Opacity(this.capNotice, 0f, 0.18, null, 0.0);
          }
        };
      }
      Log.Info("composer", "attachment cap reached: notice shown");
      this.capNoticeShowing = true;
      Anim.Opacity(this.capNotice, 1f, 0.18, null, 0.0);
      this.capNoticeTimer.Stop();
      this.capNoticeTimer.Start();
    }

    // Token: 0x1700037E RID: 894
    // (get) Token: 0x06001347 RID: 4935 RVA: 0x005A00B8 File Offset: 0x005A00B8
    internal bool IsCapNoticeShowing
    {
      get
      {
        return this.capNoticeShowing;
      }
    }

    // Token: 0x06001348 RID: 4936 RVA: 0x005A00C0 File Offset: 0x005A00C0
    public void SetDropTargeting(bool on, Color tint)
    {
      Border border = base.Children[0] as Border;
      if (border != null)
      {
        border.BorderBrush = Ui.Brush(on ? tint : DS.White(0.14));
        border.BorderThickness = new Thickness(on ? 1.5 : 1.0);
      }
      this.dropTargeting = on;
      this.dropGlyph.Foreground = Ui.Brush(tint);
      this.dropBadgePlus.Foreground = Ui.Brush(tint);
      Anim.Opacity(this.column, !on, 0.15, null, 0.0);
      Anim.Opacity(this.dropZone, on > false, 0.15, null, 0.0);
      this.column.IsHitTestVisible = !on;
    }

    // Token: 0x06001349 RID: 4937 RVA: 0x005A01A4 File Offset: 0x005A01A4
    private Grid BuildDropZone()
    {
      this.dropGlyph = Icons.Glyph("\ue7c3", 16.0, DS.Colors.CursorBlue);
      this.dropGlyph.FontWeight = FontWeights.SemiBold;
      this.dropBadgePlus = Icons.Glyph("\ue710", 6.5, DS.Colors.CursorBlue);
      this.dropBadgePlus.FontWeight = FontWeights.Bold;
      Grid grid = new Grid
      {
        Width = 10.0,
        Height = 10.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Bottom,
        Margin = new Thickness(0.0, 0.0, -3.0, -2.0),
        Children = 
        {
          new Ellipse
          {
            Fill = Ui.Brush(DS.Colors.NotchCardSurface)
          },
          this.dropBadgePlus
        }
      };
      Grid grid2 = new Grid
      {
        Width = 16.0,
        Height = 16.0,
        Children = { this.dropGlyph, grid }
      };
      TextBlock textBlock = Ui.Text("Drop files here to attach", 13.0, FontWeights.SemiBold, DS.White(0.92), TextAlignment.Left, 0, false, false);
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      StackPanel stackPanel = Ui.H(8.0, new UIElement[] { grid2, textBlock });
      stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
      stackPanel.VerticalAlignment = VerticalAlignment.Center;
      return new Grid
      {
        Opacity = 0.0,
        IsHitTestVisible = false,
        MinHeight = 24.0,
        Children = { stackPanel }
      };
    }

    // Token: 0x0600134A RID: 4938 RVA: 0x005A0374 File Offset: 0x005A0374
    public void SetPlaceholder(string text)
    {
      this.placeholderText = text;
      if (!this.dropTargeting)
      {
        this.placeholder.Text = text;
      }
    }

    // Token: 0x14000053 RID: 83
    // (add) Token: 0x0600134B RID: 4939 RVA: 0x005A0394 File Offset: 0x005A0394
    // (remove) Token: 0x0600134C RID: 4940 RVA: 0x005A03CC File Offset: 0x005A03CC
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action AttachmentsChanged;

    // Token: 0x1700037F RID: 895
    // (get) Token: 0x0600134D RID: 4941 RVA: 0x005A0404 File Offset: 0x005A0404
    public bool HasDraft
    {
      get
      {
        return this.HasContent;
      }
    }

    // Token: 0x040008CF RID: 2255
    public const int MaxVisibleLines = 8;

    // Token: 0x040008D0 RID: 2256
    private const double LeadingPadding = 12.0;

    // Token: 0x040008D1 RID: 2257
    private const double TrailingPadding = 8.0;

    // Token: 0x040008D2 RID: 2258
    private const double Spacing = 10.0;

    // Token: 0x040008D3 RID: 2259
    private const double VerticalPadding = 8.0;

    // Token: 0x040008D4 RID: 2260
    private const double CornerRadiusValue = 14.0;

    // Token: 0x040008D6 RID: 2262
    private readonly TextBlock placeholder;

    // Token: 0x040008D7 RID: 2263
    private readonly TextBlock measurer;

    // Token: 0x040008D8 RID: 2264
    private readonly Grid fieldHost;

    // Token: 0x040008D9 RID: 2265
    private readonly Pressable send;

    // Token: 0x040008DA RID: 2266
    private readonly GelSurface sendGel;

    // Token: 0x040008DB RID: 2267
    private readonly TextBlock sendGlyph;

    // Token: 0x040008DC RID: 2268
    [Nullable(2)]
    private readonly Pressable attach;

    // Token: 0x040008DD RID: 2269
    private readonly StackPanel chips = new StackPanel
    {
      Orientation = Orientation.Horizontal,
      Spacing = 4.0
    };

    // Token: 0x040008DE RID: 2270
    private readonly ScrollViewer chipRow;

    // Token: 0x040008DF RID: 2271
    private readonly Grid row;

    // Token: 0x040008E0 RID: 2272
    private readonly List<string> attachments = new List<string>();

    // Token: 0x040008E1 RID: 2273
    private int lines = 1;

    // Token: 0x040008E8 RID: 2280
    [Nullable(2)]
    private Grid capNotice;

    // Token: 0x040008E9 RID: 2281
    [Nullable(2)]
    private Microsoft.UI.Dispatching.DispatcherQueueTimer capNoticeTimer;

    // Token: 0x040008EA RID: 2282
    private bool capNoticeShowing;

    // Token: 0x040008EB RID: 2283
    private readonly StackPanel column;

    // Token: 0x040008EC RID: 2284
    private readonly Grid dropZone;

    // Token: 0x040008ED RID: 2285
    private TextBlock dropGlyph;

    // Token: 0x040008EE RID: 2286
    private TextBlock dropBadgePlus;

    // Token: 0x040008EF RID: 2287
    private bool dropTargeting;

    // Token: 0x040008F0 RID: 2288
    private string placeholderText;
  }
}
