using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents
{
  // Token: 0x02000006 RID: 6
  [NullableContext(1)]
  [Nullable(0)]
  public static class AgentLaunchPalette
  {
    // Token: 0x17000001 RID: 1
    // (get) Token: 0x06000014 RID: 20 RVA: 0x000D6240 File Offset: 0x000D6240
    public static int SlotCount
    {
      get
      {
        return AgentLaunchPalette.SwatchRgb.Count;
      }
    }

    // Token: 0x06000015 RID: 21 RVA: 0x000D624C File Offset: 0x000D624C
    public static int NextIndex(IReadOnlyList<int> assignedMostRecentFirst, IReadOnlySet<int> activeSlots)
    {
      int slotCount = AgentLaunchPalette.SlotCount;
      List<int> list = assignedMostRecentFirst.Where<int>((int i) => i >= 0).ToList<int>();
      int num = (((list.Count > 0) ? list[0] : (-1)) + 1) % slotCount;
      HashSet<int> hashSet = new HashSet<int>(activeSlots.Where<int>((int i) => i >= 0));
      hashSet.UnionWith(list.Take<int>(Math.Max(0, slotCount - 1)));
      for (int k = 0; k < slotCount; k++)
      {
        int num2 = (num + k) % slotCount;
        if (!hashSet.Contains(num2))
        {
          return num2;
        }
      }
      for (int j = 0; j < slotCount; j++)
      {
        int num3 = (num + j) % slotCount;
        if (!activeSlots.Contains(num3))
        {
          return num3;
        }
      }
      return num;
    }

    // Token: 0x0400000A RID: 10
    public static readonly IReadOnlyList<uint> SwatchRgb = new <>z__ReadOnlyArray<uint>(new uint[] { 3375359U, 16731212U, 13934615U, 2540161U });
  }
}
