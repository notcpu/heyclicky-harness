using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000306 RID: 774
  public sealed class DockMembershipWatch
  {
    // Token: 0x060021C8 RID: 8648 RVA: 0x0061DBF8 File Offset: 0x0061DBF8
    [NullableContext(2)]
    public bool Note(bool showsDock, IEnumerable<Guid> liveSuggestionCardIds)
    {
      string text = ((liveSuggestionCardIds == null) ? "-" : string.Join<Guid>(",", liveSuggestionCardIds.Order<Guid>()));
      ValueTuple<bool, string> valueTuple = new ValueTuple<bool, string>(showsDock, text);
      ValueTuple<bool, string>? valueTuple2 = this.last;
      ValueTuple<bool, string> valueTuple3 = valueTuple;
      bool flag = valueTuple2 != null;
      bool flag2;
      if (!flag)
      {
        flag2 = false;
      }
      else if (!flag)
      {
        flag2 = true;
      }
      else
      {
        ValueTuple<bool, string> valueOrDefault = valueTuple2.GetValueOrDefault();
        flag2 = valueOrDefault.Item1 == valueTuple3.Item1 && valueOrDefault.Item2 == valueTuple3.Item2;
      }
      if (flag2)
      {
        return false;
      }
      this.last = new ValueTuple<bool, string>?(valueTuple);
      return true;
    }

    // Token: 0x0400103B RID: 4155
    [TupleElementNames(new string[] { "ShowsDock", "Cards" })]
    [Nullable(new byte[] { 0, 1 })]
    private ValueTuple<bool, string>? last;
  }
}
