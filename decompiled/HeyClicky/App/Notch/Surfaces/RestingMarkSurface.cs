using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000165 RID: 357
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class RestingMarkSurface : NotchSurface
  {
    // Token: 0x17000392 RID: 914
    // (get) Token: 0x0600139C RID: 5020 RVA: 0x005A346C File Offset: 0x005A346C
    public override FrameworkElement View
    {
      get
      {
        return this.root;
      }
    }

    // Token: 0x17000393 RID: 915
    // (get) Token: 0x0600139D RID: 5021 RVA: 0x005A3474 File Offset: 0x005A3474
    public override VerticalAlignment ContentAlignment
    {
      get
      {
        return VerticalAlignment.Center;
      }
    }

    // Token: 0x0600139E RID: 5022 RVA: 0x005A3478 File Offset: 0x005A3478
    public RestingMarkSurface()
    {
      this.root.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      this.root.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star),
        MinWidth = 6.0
      });
      this.root.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      this.label = Ui.Semibold("Working", 11.0, DS.Colors.TextPrimary);
      this.label.VerticalAlignment = VerticalAlignment.Center;
      Typography.SetNumeralAlignment(this.label, FontNumeralAlignment.Tabular);
      this.root.Children.Add(this.label);
      Grid.SetColumn(this.mark, 2);
      this.root.Children.Add(this.mark);
    }

    // Token: 0x0600139F RID: 5023 RVA: 0x005A35D4 File Offset: 0x005A35D4
    public override bool CanUpdate(NotchPresentation p)
    {
      NotchPresentation.LiveEvent liveEvent = p as NotchPresentation.LiveEvent;
      return liveEvent != null && liveEvent.Event is NotchLiveEvent.RestingMark;
    }

    // Token: 0x060013A0 RID: 5024 RVA: 0x005A35FC File Offset: 0x005A35FC
    public override void Update(NotchPresentation p)
    {
      this.Refresh();
    }

    // Token: 0x060013A1 RID: 5025 RVA: 0x005A3604 File Offset: 0x005A3604
    protected override void OnAttached()
    {
      base.Host.BindToEdges(this.label, -1.0, 0.5);
      base.Host.BindToEdges(this.mark, 1.0, 0.5);
      NotchExtensions.RestingMarkChanged += this.OnChanged;
      this.Refresh();
    }

    // Token: 0x060013A2 RID: 5026 RVA: 0x005A3670 File Offset: 0x005A3670
    public override void Detached()
    {
      NotchExtensions.RestingMarkChanged -= this.OnChanged;
    }

    // Token: 0x060013A3 RID: 5027 RVA: 0x005A3684 File Offset: 0x005A3684
    private void OnChanged()
    {
      this.root.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.Refresh));
    }

    // Token: 0x060013A4 RID: 5028 RVA: 0x005A36A4 File Offset: 0x005A36A4
    private void Refresh()
    {
      Func<NotchRestingMarkState> restingMarkState = NotchExtensions.RestingMarkState;
      NotchRestingMarkState notchRestingMarkState = ((restingMarkState != null) ? restingMarkState() : null) ?? new NotchRestingMarkState(1, "idle");
      TextBlock textBlock = this.label;
      string text;
      if (notchRestingMarkState.WorkingCount <= 1)
      {
        text = "Working";
      }
      else
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(notchRestingMarkState.WorkingCount);
        defaultInterpolatedStringHandler.AppendLiteral(" working");
        text = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      textBlock.Text = text;
      ComputerMark computerMark = this.mark;
      bool flag = notchRestingMarkState.WorkingCount > 0;
      string alertPhase = notchRestingMarkState.AlertPhase;
      string text2;
      if (!(alertPhase == "needsYou"))
      {
        if (!(alertPhase == "failed"))
        {
          text2 = null;
        }
        else
        {
          text2 = "warning";
        }
      }
      else
      {
        text2 = "hand";
      }
      computerMark.Set(flag, text2);
      List<string> list = new List<string> { "HeyClicky" };
      if (notchRestingMarkState.WorkingCount > 0)
      {
        List<string> list2 = list;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(notchRestingMarkState.WorkingCount);
        defaultInterpolatedStringHandler.AppendLiteral(" working");
        list2.Add(defaultInterpolatedStringHandler.ToStringAndClear());
      }
      AutomationProperties.SetName(this.root, string.Join(", ", list));
    }

    // Token: 0x04000910 RID: 2320
    private readonly Grid root = new Grid
    {
      Margin = new Thickness(14.0, 0.0, 14.0, 0.0),
      ColumnSpacing = 8.0,
      IsHitTestVisible = false
    };

    // Token: 0x04000911 RID: 2321
    private readonly TextBlock label;

    // Token: 0x04000912 RID: 2322
    private readonly ComputerMark mark = new ComputerMark
    {
      VerticalAlignment = VerticalAlignment.Center
    };
  }
}
