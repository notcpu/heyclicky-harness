using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.UI
{
  // Token: 0x020000A2 RID: 162
  [NullableContext(1)]
  [Nullable(0)]
  public static class Accessibility
  {
    // Token: 0x060006BC RID: 1724 RVA: 0x00531FAC File Offset: 0x00531FAC
    public static string TextWithin(DependencyObject root, int maxLength = 120)
    {
      Accessibility.<>c__DisplayClass0_0 CS$<>8__locals1;
      CS$<>8__locals1.maxLength = maxLength;
      CS$<>8__locals1.parts = new List<string>();
      Accessibility.<TextWithin>g__Walk|0_0(root, ref CS$<>8__locals1);
      string text = string.Join(" ", CS$<>8__locals1.parts.Where<string>((string p) => p.Length > 0));
      if (text.Length > CS$<>8__locals1.maxLength)
      {
        return text.Substring(0, CS$<>8__locals1.maxLength);
      }
      return text;
    }

    // Token: 0x060006BD RID: 1725 RVA: 0x00532028 File Offset: 0x00532028
    private static bool IsIconFont(TextBlock block)
    {
      return block.Text.All<char>((char c) => c >= '\ue000' && c <= '\uf8ff');
    }

    // Token: 0x060006BE RID: 1726 RVA: 0x00532054 File Offset: 0x00532054
    public static T Named<[Nullable(0)] T>(this T element, string name) where T : DependencyObject
    {
      AutomationProperties.SetName(element, name);
      return element;
    }

    // Token: 0x060006BF RID: 1727 RVA: 0x00532064 File Offset: 0x00532064
    [CompilerGenerated]
    internal static void <TextWithin>g__Walk|0_0(DependencyObject node, ref Accessibility.<>c__DisplayClass0_0 A_1)
    {
      if (A_1.parts.Sum<string>((string p) => p.Length) > A_1.maxLength)
      {
        return;
      }
      TextBlock textBlock = node as TextBlock;
      if (textBlock != null)
      {
        string text = textBlock.Text;
        if (text != null && text.Length > 0 && textBlock.Visibility == Visibility.Visible && !Accessibility.IsIconFont(textBlock))
        {
          A_1.parts.Add(text.Trim());
        }
      }
      int childrenCount = VisualTreeHelper.GetChildrenCount(node);
      for (int i = 0; i < childrenCount; i++)
      {
        Accessibility.<TextWithin>g__Walk|0_0(VisualTreeHelper.GetChild(node, i), ref A_1);
      }
      if (childrenCount == 0)
      {
        Panel panel = node as Panel;
        if (panel != null)
        {
          foreach (UIElement uielement in panel.Children)
          {
            Accessibility.<TextWithin>g__Walk|0_0(uielement, ref A_1);
          }
        }
      }
    }
  }
}
