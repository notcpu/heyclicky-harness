using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000214 RID: 532
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkActedOn
  {
    // Token: 0x06001951 RID: 6481 RVA: 0x005D0C34 File Offset: 0x005D0C34
    public static FrameworkElement Wrap(FrameworkElement content, ClickyInkGroup group, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      if (!ClickyInkActedOn.OperableKinds.Contains(group.Kind))
      {
        return content;
      }
      ClickyInkActionFeedback feedback = actions.Feedback;
      List<ClickyInkDocument.Block> list = (from b in @group.Blocks
        where b.GoneAt == null
        select b.Source).ToList<ClickyInkDocument.Block>();
      ClickyInkDocument.Block block = ((list.Count > 0) ? list[0] : null);
      ValueTuple<string, bool>? valueTuple = null;
      ClickyInkSentAction pending = feedback.Pending;
      ClickyInkDocument.Kind kind;
      bool flag;
      if (pending != null && list.Any<ClickyInkDocument.Block>((ClickyInkDocument.Block s) => ClickyInkObjectKey.Of(s) == pending.ObjectKey))
      {
        valueTuple = new ValueTuple<string, bool>?(new ValueTuple<string, bool>(pending.MarginWords, true));
      }
      else
      {
        kind = group.Kind;
        if (kind <= ClickyInkDocument.Kind.Question)
        {
          if (kind != ClickyInkDocument.Kind.Letter && kind != ClickyInkDocument.Kind.Question)
          {
            goto IL_0101;
          }
        }
        else if (kind != ClickyInkDocument.Kind.Form && kind != ClickyInkDocument.Kind.Gallery)
        {
          goto IL_0101;
        }
        flag = true;
        goto IL_0104;
        IL_0101:
        flag = false;
        IL_0104:
        if (flag && block != null)
        {
          ClickyInkSentAction clickyInkSentAction = feedback.AnswerFor(block);
          if (clickyInkSentAction != null)
          {
            valueTuple = new ValueTuple<string, bool>?(new ValueTuple<string, bool>(clickyInkSentAction.MarginWords, false));
          }
        }
      }
      bool flag2 = group.Kind == ClickyInkDocument.Kind.Question && block != null && feedback.AnswerFor(block) != null;
      kind = group.Kind;
      flag = kind == ClickyInkDocument.Kind.Question || kind == ClickyInkDocument.Kind.Form || kind == ClickyInkDocument.Kind.Gallery;
      bool flag3 = flag && block != null && feedback.AnswerFor(block) != null;
      bool flag4 = group.Kind == ClickyInkDocument.Kind.Letter && block != null && feedback.AnswerFor(block) != null;
      kind = group.Kind;
      double num;
      if (kind > ClickyInkDocument.Kind.Form)
      {
        if (kind != ClickyInkDocument.Kind.Connect)
        {
          if (kind == ClickyInkDocument.Kind.Gallery)
          {
            goto IL_0239;
          }
          if (kind != ClickyInkDocument.Kind.Criteria)
          {
            goto IL_0288;
          }
        }
        num = (feedback.IsLocked ? 0.7 : 1.0);
        goto IL_0293;
      }
      if (kind == ClickyInkDocument.Kind.Ask)
      {
        num = (feedback.IsLocked ? 0.45 : 1.0);
        goto IL_0293;
      }
      if (kind == ClickyInkDocument.Kind.Question)
      {
        num = (flag2 ? 0.55 : (feedback.IsLocked ? 0.7 : 1.0));
        goto IL_0293;
      }
      if (kind != ClickyInkDocument.Kind.Form)
      {
        goto IL_0288;
      }
      IL_0239:
      num = (flag3 ? 0.6 : (feedback.IsLocked ? 0.7 : 1.0));
      goto IL_0293;
      IL_0288:
      num = 1.0;
      IL_0293:
      double num2 = num;
      if (group.Kind != ClickyInkDocument.Kind.Letter && (feedback.IsLocked || flag3))
      {
        content.IsHitTestVisible = false;
        Control control = content as Control;
        if (control != null)
        {
          control.IsEnabled = false;
        }
      }
      content.Opacity *= num2;
      List<UIElement> list2 = new List<UIElement>();
      if (flag2)
      {
        ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyInkCrossOut(group.Id.CharacterCount()), new PaperStroke(style.Fresh.Opacity(0.8), 2.0, PenLineCap.Round, PenLineJoin.Miter, null));
        clickyShapeView.Margin = new Thickness(10.0, 8.0, 10.0, 8.0);
        list2.Add(clickyShapeView);
      }
      if (flag4)
      {
        FrameworkElement frameworkElement = ClickyInkApprovedStamp.Make("OK TO SEND");
        frameworkElement.HorizontalAlignment = HorizontalAlignment.Right;
        frameworkElement.VerticalAlignment = VerticalAlignment.Top;
        frameworkElement.Transformed(-14.0, 30.0, 0.0, 1.0, null);
        frameworkElement.IsHitTestVisible = false;
        list2.Add(frameworkElement);
      }
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 5.0
      };
      stackPanel.Children.Add((list2.Count > 0) ? new ClickyInkOverlay(content, list2.ToArray()) : content);
      if (valueTuple != null)
      {
        ValueTuple<string, bool> valueOrDefault = valueTuple.GetValueOrDefault();
        stackPanel.Children.Add(ClickyInkYouSaidSlip.Make(valueOrDefault.Item1, valueOrDefault.Item2, clock, style));
      }
      return stackPanel;
    }

    // Token: 0x04000C51 RID: 3153
    private static readonly HashSet<ClickyInkDocument.Kind> OperableKinds = new HashSet<ClickyInkDocument.Kind>
    {
      ClickyInkDocument.Kind.Ask,
      ClickyInkDocument.Kind.Question,
      ClickyInkDocument.Kind.Letter,
      ClickyInkDocument.Kind.Form,
      ClickyInkDocument.Kind.Connect,
      ClickyInkDocument.Kind.Gallery,
      ClickyInkDocument.Kind.Criteria
    };
  }
}
