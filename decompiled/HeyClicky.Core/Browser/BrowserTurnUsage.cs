using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Browser
{
  // Token: 0x020001E0 RID: 480
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BrowserTurnUsage
  {
    // Token: 0x06001B94 RID: 7060 RVA: 0x003DCA14 File Offset: 0x003DCA14
    public BrowserTurnUsage(string thread, DateTime firstCallAt)
    {
    }

    // Token: 0x17000612 RID: 1554
    // (get) Token: 0x06001B95 RID: 7061 RVA: 0x003DCA68 File Offset: 0x003DCA68
    public string Thread { get; } = thread;

    // Token: 0x17000613 RID: 1555
    // (get) Token: 0x06001B96 RID: 7062 RVA: 0x003DCA70 File Offset: 0x003DCA70
    public DateTime FirstCallAt { get; } = firstCallAt;

    // Token: 0x17000614 RID: 1556
    // (get) Token: 0x06001B97 RID: 7063 RVA: 0x003DCA78 File Offset: 0x003DCA78
    // (set) Token: 0x06001B98 RID: 7064 RVA: 0x003DCA80 File Offset: 0x003DCA80
    public int OperationCount { get; set; }

    // Token: 0x17000615 RID: 1557
    // (get) Token: 0x06001B99 RID: 7065 RVA: 0x003DCA8C File Offset: 0x003DCA8C
    public Dictionary<string, int> OperationCounts { get; } = new Dictionary<string, int>();

    // Token: 0x17000616 RID: 1558
    // (get) Token: 0x06001B9A RID: 7066 RVA: 0x003DCA94 File Offset: 0x003DCA94
    // (set) Token: 0x06001B9B RID: 7067 RVA: 0x003DCA9C File Offset: 0x003DCA9C
    public int BatchStepCount { get; set; }

    // Token: 0x17000617 RID: 1559
    // (get) Token: 0x06001B9C RID: 7068 RVA: 0x003DCAA8 File Offset: 0x003DCAA8
    // (set) Token: 0x06001B9D RID: 7069 RVA: 0x003DCAB0 File Offset: 0x003DCAB0
    public int CommandsSentToBrowser { get; set; }

    // Token: 0x17000618 RID: 1560
    // (get) Token: 0x06001B9E RID: 7070 RVA: 0x003DCABC File Offset: 0x003DCABC
    // (set) Token: 0x06001B9F RID: 7071 RVA: 0x003DCAC4 File Offset: 0x003DCAC4
    public int ErrorCount { get; set; }

    // Token: 0x17000619 RID: 1561
    // (get) Token: 0x06001BA0 RID: 7072 RVA: 0x003DCAD0 File Offset: 0x003DCAD0
    public Dictionary<string, int> ErrorKinds { get; } = new Dictionary<string, int>();

    // Token: 0x1700061A RID: 1562
    // (get) Token: 0x06001BA1 RID: 7073 RVA: 0x003DCAD8 File Offset: 0x003DCAD8
    public SortedSet<string> DetachReasons { get; } = new SortedSet<string>(StringComparer.Ordinal);

    // Token: 0x1700061B RID: 1563
    // (get) Token: 0x06001BA2 RID: 7074 RVA: 0x003DCAE0 File Offset: 0x003DCAE0
    // (set) Token: 0x06001BA3 RID: 7075 RVA: 0x003DCAE8 File Offset: 0x003DCAE8
    public int TimeoutCount { get; set; }

    // Token: 0x1700061C RID: 1564
    // (get) Token: 0x06001BA4 RID: 7076 RVA: 0x003DCAF4 File Offset: 0x003DCAF4
    public Dictionary<string, int> TimeoutOperations { get; } = new Dictionary<string, int>();

    // Token: 0x1700061D RID: 1565
    // (get) Token: 0x06001BA5 RID: 7077 RVA: 0x003DCAFC File Offset: 0x003DCAFC
    // (set) Token: 0x06001BA6 RID: 7078 RVA: 0x003DCB04 File Offset: 0x003DCB04
    public int TabsKept { get; set; }

    // Token: 0x1700061E RID: 1566
    // (get) Token: 0x06001BA7 RID: 7079 RVA: 0x003DCB10 File Offset: 0x003DCB10
    // (set) Token: 0x06001BA8 RID: 7080 RVA: 0x003DCB18 File Offset: 0x003DCB18
    public int ScreenshotsSaved { get; set; }

    // Token: 0x1700061F RID: 1567
    // (get) Token: 0x06001BA9 RID: 7081 RVA: 0x003DCB24 File Offset: 0x003DCB24
    // (set) Token: 0x06001BAA RID: 7082 RVA: 0x003DCB2C File Offset: 0x003DCB2C
    public bool WasStoppedByUser { get; set; }

    // Token: 0x17000620 RID: 1568
    // (get) Token: 0x06001BAB RID: 7083 RVA: 0x003DCB38 File Offset: 0x003DCB38
    // (set) Token: 0x06001BAC RID: 7084 RVA: 0x003DCB40 File Offset: 0x003DCB40
    public bool AccessEndedByTimeout { get; set; }

    // Token: 0x17000621 RID: 1569
    // (get) Token: 0x06001BAD RID: 7085 RVA: 0x003DCB4C File Offset: 0x003DCB4C
    // (set) Token: 0x06001BAE RID: 7086 RVA: 0x003DCB54 File Offset: 0x003DCB54
    public DateTime? EndedAt { get; set; }
  }
}
