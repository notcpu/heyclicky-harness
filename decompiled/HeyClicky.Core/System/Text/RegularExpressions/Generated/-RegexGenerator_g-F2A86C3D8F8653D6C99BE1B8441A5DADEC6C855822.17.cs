using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000012 RID: 18
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StyleBlock_16 : Regex
  {
    // Token: 0x06000021 RID: 33 RVA: 0x00331980 File Offset: 0x00331980
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StyleBlock_16()
    {
      this.pattern = "<style.*?</style>";
      this.roptions = RegexOptions.IgnoreCase | RegexOptions.Singleline;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StyleBlock_16.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x04000011 RID: 17
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StyleBlock_16 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StyleBlock_16();

    // Token: 0x02000245 RID: 581
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002114 RID: 8468 RVA: 0x003F6DA0 File Offset: 0x003F6DA0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StyleBlock_16.RunnerFactory.Runner();
      }

      // Token: 0x020006AF RID: 1711
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A51 RID: 14929 RVA: 0x0045BFE0 File Offset: 0x0045BFE0
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

        // Token: 0x06003A52 RID: 14930 RVA: 0x0045C020 File Offset: 0x0045C020
        private unsafe bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 14)
          {
            ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(runtextpos);
            for (int i = 0; i < readOnlySpan.Length - 13; i++)
            {
              int num = readOnlySpan.Slice(i).IndexOf('<');
              if (num < 0)
              {
                break;
              }
              i += num;
              if (i + 4 >= readOnlySpan.Length)
              {
                break;
              }
              if ((*readOnlySpan[i + 3] | 32) == 121 && (*readOnlySpan[i + 4] | 32) == 108)
              {
                this.runtextpos = runtextpos + i;
                return true;
              }
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A53 RID: 14931 RVA: 0x0045C0C0 File Offset: 0x0045C0C0
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (readOnlySpan.Length < 6 || *readOnlySpan[0] != 60 || !readOnlySpan.Slice(1).StartsWith("style", StringComparison.OrdinalIgnoreCase))
          {
            return false;
          }
          num += 6;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          while (readOnlySpan.Length < 8 || !readOnlySpan.StartsWith<char>("</") || !readOnlySpan.Slice(2).StartsWith("style", StringComparison.OrdinalIgnoreCase) || *readOnlySpan[7] != 62)
          {
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            num = num3;
            if (inputSpan.Slice(num).IsEmpty)
            {
              return false;
            }
            num++;
            readOnlySpan = inputSpan.Slice(num);
            num3 = readOnlySpan.IndexOf<char>("</");
            if (num3 < 0)
            {
              return false;
            }
            num += num3;
            readOnlySpan = inputSpan.Slice(num);
            num3 = num;
          }
          num += 8;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
