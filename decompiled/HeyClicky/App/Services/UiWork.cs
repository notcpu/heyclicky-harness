using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HeyClicky.App.Services
{
  // Token: 0x020000EA RID: 234
  [NullableContext(1)]
  [Nullable(0)]
  public static class UiWork
  {
    // Token: 0x06000B5A RID: 2906 RVA: 0x0055B190 File Offset: 0x0055B190
    public static object Pick(SendOrPostCallback callback, [Nullable(2)] object state)
    {
      bool flag = state is Delegate || state is IAsyncStateMachine;
      if (!flag && (state == null || !state.GetType().IsGenericType))
      {
        return callback;
      }
      return state;
    }

    // Token: 0x06000B5B RID: 2907 RVA: 0x0055B1CC File Offset: 0x0055B1CC
    [return: TupleElementNames(new string[] { "Work", "Since" })]
    [return: Nullable(new byte[] { 0, 2 })]
    public static ValueTuple<object, long> Enter(object work)
    {
      ValueTuple<object, long> valueTuple = new ValueTuple<object, long>(Volatile.Read<object>(ref UiWork.current), Volatile.Read(ref UiWork.currentSince));
      Volatile.Write(ref UiWork.currentSince, Stopwatch.GetTimestamp());
      Volatile.Write<object>(ref UiWork.current, work);
      Interlocked.Increment(ref UiWork.started);
      return valueTuple;
    }

    // Token: 0x06000B5C RID: 2908 RVA: 0x0055B20C File Offset: 0x0055B20C
    public static void Exit([TupleElementNames(new string[] { "Work", "Since" })] [Nullable(new byte[] { 0, 2 })] ValueTuple<object, long> previous)
    {
      Volatile.Write<object>(ref UiWork.current, previous.Item1);
      Volatile.Write(ref UiWork.currentSince, previous.Item2);
    }

    // Token: 0x170001F1 RID: 497
    // (get) Token: 0x06000B5D RID: 2909 RVA: 0x0055B230 File Offset: 0x0055B230
    public static long Started
    {
      get
      {
        return Interlocked.Read(ref UiWork.started);
      }
    }

    // Token: 0x06000B5E RID: 2910 RVA: 0x0055B23C File Offset: 0x0055B23C
    [return: TupleElementNames(new string[] { "Work", "RunningMs" })]
    [return: Nullable(new byte[] { 0, 2 })]
    public static ValueTuple<object, int> Snapshot()
    {
      object obj = Volatile.Read<object>(ref UiWork.current);
      long num = Volatile.Read(ref UiWork.currentSince);
      if (obj == null || num == 0L)
      {
        return new ValueTuple<object, int>(null, 0);
      }
      long num2 = (Stopwatch.GetTimestamp() - num) * 1000L / Stopwatch.Frequency;
      return new ValueTuple<object, int>(obj, (int)Math.Clamp(num2, 0L, 2147483647L));
    }

    // Token: 0x06000B5F RID: 2911 RVA: 0x0055B298 File Offset: 0x0055B298
    public static string Describe([Nullable(2)] object work)
    {
      if (work == null)
      {
        return "none";
      }
      string text2;
      try
      {
        Delegate @delegate = work as Delegate;
        if (@delegate != null)
        {
          object target = @delegate.Target;
          if (target != null)
          {
            string text = UiWork.StateMachineName(target.GetType());
            if (text != null)
            {
              return text;
            }
          }
          IAsyncStateMachine asyncStateMachine = @delegate.Target as IAsyncStateMachine;
          if (asyncStateMachine != null)
          {
            text2 = UiWork.NameOf(asyncStateMachine.GetType());
          }
          else
          {
            text2 = UiWork.MethodName(@delegate.Method.DeclaringType, @delegate.Method.Name);
          }
        }
        else
        {
          IAsyncStateMachine asyncStateMachine2 = work as IAsyncStateMachine;
          if (asyncStateMachine2 != null)
          {
            text2 = UiWork.NameOf(asyncStateMachine2.GetType());
          }
          else
          {
            text2 = UiWork.StateMachineName(work.GetType()) ?? UiWork.NameOf(work.GetType());
          }
        }
      }
      catch
      {
        text2 = "unknown";
      }
      return text2;
    }

    // Token: 0x06000B60 RID: 2912 RVA: 0x0055B370 File Offset: 0x0055B370
    [return: Nullable(2)]
    private static string StateMachineName(Type type)
    {
      if (!type.IsGenericType)
      {
        return null;
      }
      Type[] genericArguments = type.GetGenericArguments();
      for (int i = genericArguments.Length - 1; i >= 0; i--)
      {
        if (typeof(IAsyncStateMachine).IsAssignableFrom(genericArguments[i]))
        {
          return UiWork.NameOf(genericArguments[i]);
        }
      }
      return null;
    }

    // Token: 0x06000B61 RID: 2913 RVA: 0x0055B3BC File Offset: 0x0055B3BC
    private static string NameOf(Type type)
    {
      return UiWork.MethodName(type.DeclaringType, type.Name);
    }

    // Token: 0x06000B62 RID: 2914 RVA: 0x0055B3D0 File Offset: 0x0055B3D0
    public static string MethodName([Nullable(2)] Type owner, string name)
    {
      string text = name;
      if (text.StartsWith('<'))
      {
        text = text.TrimStart('<');
        int num = text.IndexOf('>');
        if (num > 0)
        {
          text = text.Substring(0, num);
        }
      }
      while (owner != null && owner.Name.StartsWith('<'))
      {
        owner = owner.DeclaringType;
      }
      string text2 = ((owner != null) ? owner.Name : null) ?? "?";
      int num2 = text2.IndexOf('`');
      if (num2 > 0)
      {
        text2 = text2.Substring(0, num2);
      }
      return text2 + "." + text;
    }

    // Token: 0x04000418 RID: 1048
    [Nullable(2)]
    private static object current;

    // Token: 0x04000419 RID: 1049
    private static long currentSince;

    // Token: 0x0400041A RID: 1050
    private static long started;
  }
}
