using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Window
{
  // Token: 0x02000143 RID: 323
  internal static class UIElementCollectionExtensions
  {
    // Token: 0x06001247 RID: 4679 RVA: 0x00595C7C File Offset: 0x00595C7C
    [NullableContext(1)]
    public static void AddRange(this UIElementCollection children, IEnumerable<FrameworkElement> elements)
    {
      foreach (FrameworkElement frameworkElement in elements)
      {
        children.Add(frameworkElement);
      }
    }
  }
}
