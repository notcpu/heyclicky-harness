using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000004 RID: 4
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BareUrlPattern_2 : Regex
  {
    // Token: 0x06000005 RID: 5 RVA: 0x003314A4 File Offset: 0x003314A4
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BareUrlPattern_2()
    {
      this.pattern = "\\s*https?://[^\\s\"'),]+";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BareUrlPattern_2.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x04000003 RID: 3
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BareUrlPattern_2 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BareUrlPattern_2();

    // Token: 0x02000237 RID: 567
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x060020F8 RID: 8440 RVA: 0x003F6CC0 File Offset: 0x003F6CC0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BareUrlPattern_2.RunnerFactory.Runner();
      }

      // Token: 0x020006A1 RID: 1697
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A13 RID: 14867 RVA: 0x0045A63C File Offset: 0x0045A63C
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          while (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan) && this.runtextpos != inputSpan.Length)
          {
            this.runtextpos++;
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
          }
        }

        // Token: 0x06003A14 RID: 14868 RVA: 0x0045A67C File Offset: 0x0045A67C
        private unsafe bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 8)
          {
            ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(runtextpos);
            int num = readOnlySpan.IndexOf<char>("http");
            if (num >= 0)
            {
              int num2 = num - 1;
              while (num2 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num2])))
              {
                num2--;
              }
              this.runtextpos = runtextpos + num2 + 1;
              this.runtrackpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A15 RID: 14869 RVA: 0x0045A704 File Offset: 0x0045A704
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          num = this.runtrackpos;
          readOnlySpan = inputSpan.Slice(num);
          if (this.runtextpos < num)
          {
            this.runtextpos = num;
          }
          if (!readOnlySpan.StartsWith<char>("http"))
          {
            return false;
          }
          if (readOnlySpan.Length > 4 && *readOnlySpan[4] == 115)
          {
            readOnlySpan = readOnlySpan.Slice(1);
            num++;
          }
          if (!readOnlySpan.Slice(4).StartsWith<char>("://"))
          {
            return false;
          }
          num += 7;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = 0;
          char c;
          while (num3 < readOnlySpan.Length && (((c = (char)(*readOnlySpan[num3])) < '\u0080') ? (((int)"쇿\uffff\ued7a\uffff\uffff\uffff\uffff\uffff"[(int)(c >> 4)] & (1 << (int)(c & '\u000f'))) != 0) : RegexRunner.CharInClass(c, "\u0001\b\u0001\"#'()*,-d")))
          {
            num3++;
          }
          if (num3 == 0)
          {
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
