using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000011 RID: 17
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ScriptBlock_15 : Regex
  {
    // Token: 0x0600001F RID: 31 RVA: 0x00331924 File Offset: 0x00331924
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ScriptBlock_15()
    {
      this.pattern = "<script.*?</script>";
      this.roptions = RegexOptions.IgnoreCase | RegexOptions.Singleline;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ScriptBlock_15.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x04000010 RID: 16
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ScriptBlock_15 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ScriptBlock_15();

    // Token: 0x02000244 RID: 580
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002112 RID: 8466 RVA: 0x003F6D90 File Offset: 0x003F6D90
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ScriptBlock_15.RunnerFactory.Runner();
      }

      // Token: 0x020006AE RID: 1710
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A4D RID: 14925 RVA: 0x0045BDDC File Offset: 0x0045BDDC
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

        // Token: 0x06003A4E RID: 14926 RVA: 0x0045BE1C File Offset: 0x0045BE1C
        private unsafe bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 16)
          {
            ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(runtextpos);
            for (int i = 0; i < readOnlySpan.Length - 15; i++)
            {
              int num = readOnlySpan.Slice(i).IndexOf('<');
              if (num < 0)
              {
                break;
              }
              i += num;
              if (i + 5 >= readOnlySpan.Length)
              {
                break;
              }
              if ((*readOnlySpan[i + 5] | 32) == 112 && (*readOnlySpan[i + 2] | 32) == 99)
              {
                this.runtextpos = runtextpos + i;
                return true;
              }
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A4F RID: 14927 RVA: 0x0045BEBC File Offset: 0x0045BEBC
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (readOnlySpan.Length < 7 || *readOnlySpan[0] != 60 || !readOnlySpan.Slice(1).StartsWith("script", StringComparison.OrdinalIgnoreCase))
          {
            return false;
          }
          num += 7;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          while (readOnlySpan.Length < 9 || !readOnlySpan.StartsWith<char>("</") || !readOnlySpan.Slice(2).StartsWith("script", StringComparison.OrdinalIgnoreCase) || *readOnlySpan[8] != 62)
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
          num += 9;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
