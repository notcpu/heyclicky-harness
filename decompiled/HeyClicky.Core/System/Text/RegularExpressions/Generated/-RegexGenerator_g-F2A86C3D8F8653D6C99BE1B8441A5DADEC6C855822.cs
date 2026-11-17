using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000002 RID: 2
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__CitationPattern_0 : Regex
  {
    // Token: 0x06000001 RID: 1 RVA: 0x003313F4 File Offset: 0x003313F4
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__CitationPattern_0()
    {
      this.pattern = "\\s*\\(\\[[^\\]\\n]{1,80}\\]\\(https?://[^)\\s\"]+\\)\\)";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__CitationPattern_0.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x04000001 RID: 1
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__CitationPattern_0 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__CitationPattern_0();

    // Token: 0x02000235 RID: 565
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x060020F4 RID: 8436 RVA: 0x003F6CA0 File Offset: 0x003F6CA0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__CitationPattern_0.RunnerFactory.Runner();
      }

      // Token: 0x0200069F RID: 1695
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A0A RID: 14858 RVA: 0x0045A108 File Offset: 0x0045A108
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

        // Token: 0x06003A0B RID: 14859 RVA: 0x0045A148 File Offset: 0x0045A148
        private unsafe bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 15)
          {
            ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(runtextpos);
            int num = readOnlySpan.IndexOf<char>("([");
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

        // Token: 0x06003A0C RID: 14860 RVA: 0x0045A1D0 File Offset: 0x0045A1D0
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
          if (!readOnlySpan.StartsWith<char>("(["))
          {
            return false;
          }
          num += 2;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = 0;
          char c;
          while (num3 < 80 && num3 < readOnlySpan.Length && (((c = (char)(*readOnlySpan[num3])) != '\n') & (c != ']')))
          {
            num3++;
          }
          if (num3 == 0)
          {
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          if (!readOnlySpan.StartsWith<char>("](http"))
          {
            return false;
          }
          if (readOnlySpan.Length > 6 && *readOnlySpan[6] == 115)
          {
            readOnlySpan = readOnlySpan.Slice(1);
            num++;
          }
          if (!readOnlySpan.Slice(6).StartsWith<char>("://"))
          {
            return false;
          }
          num += 9;
          readOnlySpan = inputSpan.Slice(num);
          int num4 = 0;
          while (num4 < readOnlySpan.Length && (((c = (char)(*readOnlySpan[num4])) < '\u0080') ? (((int)"쇿\uffffﷺ\uffff\uffff\uffff\uffff\uffff"[(int)(c >> 4)] & (1 << (int)(c & '\u000f'))) != 0) : RegexRunner.CharInClass(c, "\u0001\u0004\u0001\"#)*d")))
          {
            num4++;
          }
          if (num4 == 0)
          {
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num4);
          num += num4;
          if (!readOnlySpan.StartsWith<char>("))"))
          {
            return false;
          }
          num += 2;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
