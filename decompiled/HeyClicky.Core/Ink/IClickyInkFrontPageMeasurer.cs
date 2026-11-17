using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200014C RID: 332
  [NullableContext(1)]
  public interface IClickyInkFrontPageMeasurer
  {
    // Token: 0x170004D7 RID: 1239
    // (get) Token: 0x060014C0 RID: 5312
    double ContentWidth { get; }

    // Token: 0x060014C1 RID: 5313
    double Group(ClickyInkGroup group);

    // Token: 0x060014C2 RID: 5314
    double Table(ClickyInkGroup group, double width);

    // Token: 0x060014C3 RID: 5315
    double Headline(ClickyInkDashboardBoard board, double width, bool isMission);

    // Token: 0x060014C4 RID: 5316
    double Tiles(IReadOnlyList<ClickyInkLiveNote.Block> blocks, double width);

    // Token: 0x060014C5 RID: 5317
    double MissionTileHeight(IReadOnlyList<ClickyInkDocument.Block> blocks);
  }
}
