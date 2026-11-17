using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Input;

namespace HeyClicky.App.UI
{
  // Token: 0x020000B7 RID: 183
  [NullableContext(1)]
  [Nullable(0)]
  public static class PointerFrames
  {
    // Token: 0x06000777 RID: 1911 RVA: 0x00538954 File Offset: 0x00538954
    public static void Note(PointerRoutedEventArgs e)
    {
      uint pointerId = e.Pointer.PointerId;
      uint frameId = e.GetCurrentPoint(null).FrameId;
      uint num;
      if (!PointerFrames.newest.TryGetValue(pointerId, out num) || frameId > num)
      {
        PointerFrames.newest[pointerId] = frameId;
      }
    }

    // Token: 0x06000778 RID: 1912 RVA: 0x0053899C File Offset: 0x0053899C
    public static bool IsStale(PointerRoutedEventArgs e)
    {
      uint frameId = e.GetCurrentPoint(null).FrameId;
      uint num;
      return PointerFrames.newest.TryGetValue(e.Pointer.PointerId, out num) && frameId < num;
    }

    // Token: 0x0400023B RID: 571
    private static readonly Dictionary<uint, uint> newest = new Dictionary<uint, uint>();
  }
}
