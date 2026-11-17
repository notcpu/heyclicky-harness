using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000130 RID: 304
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkDashboardKinds
  {
    // Token: 0x040008C5 RID: 2245
    public static readonly IReadOnlySet<ClickyInkDocument.Kind> TileKinds = new HashSet<ClickyInkDocument.Kind>
    {
      ClickyInkDocument.Kind.Figure,
      ClickyInkDocument.Kind.Spark,
      ClickyInkDocument.Kind.Status,
      ClickyInkDocument.Kind.Progress,
      ClickyInkDocument.Kind.Tally,
      ClickyInkDocument.Kind.Streak,
      ClickyInkDocument.Kind.Countdown
    };

    // Token: 0x040008C6 RID: 2246
    public static readonly IReadOnlySet<ClickyInkDocument.Kind> DashboardKinds = new HashSet<ClickyInkDocument.Kind>
    {
      ClickyInkDocument.Kind.Spark,
      ClickyInkDocument.Kind.Status,
      ClickyInkDocument.Kind.Progress,
      ClickyInkDocument.Kind.Tally,
      ClickyInkDocument.Kind.Streak,
      ClickyInkDocument.Kind.Countdown,
      ClickyInkDocument.Kind.Pipeline,
      ClickyInkDocument.Kind.Run,
      ClickyInkDocument.Kind.Quiet
    };
  }
}
