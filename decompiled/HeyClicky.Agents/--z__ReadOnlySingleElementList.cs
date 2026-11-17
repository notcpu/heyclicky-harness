using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x0200007A RID: 122
[CompilerGenerated]
internal sealed class <>z__ReadOnlySingleElementList<T> : IEnumerable, ICollection, IList, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
  // Token: 0x06000953 RID: 2387 RVA: 0x00104150 File Offset: 0x00104150
  public <>z__ReadOnlySingleElementList(T item)
  {
    this._item = item;
  }

  // Token: 0x06000954 RID: 2388 RVA: 0x00104160 File Offset: 0x00104160
  [return: Nullable(1)]
  IEnumerator IEnumerable.GetEnumerator()
  {
    return new <>z__ReadOnlySingleElementList<T>.Enumerator(this._item);
  }

  // Token: 0x1700025B RID: 603
  // (get) Token: 0x06000955 RID: 2389 RVA: 0x00104170 File Offset: 0x00104170
  int ICollection.Count
  {
    get
    {
      return 1;
    }
  }

  // Token: 0x1700025C RID: 604
  // (get) Token: 0x06000956 RID: 2390 RVA: 0x00104174 File Offset: 0x00104174
  bool ICollection.IsSynchronized
  {
    get
    {
      return false;
    }
  }

  // Token: 0x1700025D RID: 605
  // (get) Token: 0x06000957 RID: 2391 RVA: 0x00104178 File Offset: 0x00104178
  object ICollection.SyncRoot
  {
    [return: Nullable(1)]
    get
    {
      return this;
    }
  }

  // Token: 0x06000958 RID: 2392 RVA: 0x0010417C File Offset: 0x0010417C
  void ICollection.CopyTo([Nullable(1)] Array array, int index)
  {
    array.SetValue(this._item, index);
  }

  // Token: 0x1700025E RID: 606
  object IList.this[int index]
  {
    [return: Nullable(2)]
    get
    {
      if (index != 0)
      {
        throw new IndexOutOfRangeException();
      }
      return this._item;
    }
    [param: Nullable(2)]
    set
    {
      throw new NotSupportedException();
    }
  }

  // Token: 0x1700025F RID: 607
  // (get) Token: 0x0600095B RID: 2395 RVA: 0x001041B0 File Offset: 0x001041B0
  bool IList.IsFixedSize
  {
    get
    {
      return true;
    }
  }

  // Token: 0x17000260 RID: 608
  // (get) Token: 0x0600095C RID: 2396 RVA: 0x001041B4 File Offset: 0x001041B4
  bool IList.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x0600095D RID: 2397 RVA: 0x001041B8 File Offset: 0x001041B8
  int IList.Add([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x0600095E RID: 2398 RVA: 0x001041C0 File Offset: 0x001041C0
  void IList.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x0600095F RID: 2399 RVA: 0x001041C8 File Offset: 0x001041C8
  bool IList.Contains([Nullable(2)] object value)
  {
    return EqualityComparer<T>.Default.Equals(this._item, (T)((object)value));
  }

  // Token: 0x06000960 RID: 2400 RVA: 0x001041E0 File Offset: 0x001041E0
  int IList.IndexOf([Nullable(2)] object value)
  {
    if (!EqualityComparer<T>.Default.Equals(this._item, (T)((object)value)))
    {
      return -1;
    }
    return 0;
  }

  // Token: 0x06000961 RID: 2401 RVA: 0x00104200 File Offset: 0x00104200
  void IList.Insert(int index, [Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06000962 RID: 2402 RVA: 0x00104208 File Offset: 0x00104208
  void IList.Remove([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06000963 RID: 2403 RVA: 0x00104210 File Offset: 0x00104210
  void IList.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06000964 RID: 2404 RVA: 0x00104218 File Offset: 0x00104218
  [return: Nullable(new byte[] { 1, 0 })]
  IEnumerator<T> IEnumerable<T>.GetEnumerator()
  {
    return new <>z__ReadOnlySingleElementList<T>.Enumerator(this._item);
  }

  // Token: 0x17000261 RID: 609
  // (get) Token: 0x06000965 RID: 2405 RVA: 0x00104228 File Offset: 0x00104228
  int IReadOnlyCollection<T>.Count
  {
    get
    {
      return 1;
    }
  }

  // Token: 0x17000262 RID: 610
  T IReadOnlyList<T>.this[int index]
  {
    get
    {
      if (index != 0)
      {
        throw new IndexOutOfRangeException();
      }
      return this._item;
    }
  }

  // Token: 0x17000263 RID: 611
  // (get) Token: 0x06000967 RID: 2407 RVA: 0x00104240 File Offset: 0x00104240
  int ICollection<T>.Count
  {
    get
    {
      return 1;
    }
  }

  // Token: 0x17000264 RID: 612
  // (get) Token: 0x06000968 RID: 2408 RVA: 0x00104244 File Offset: 0x00104244
  bool ICollection<T>.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x06000969 RID: 2409 RVA: 0x00104248 File Offset: 0x00104248
  void ICollection<T>.Add(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x0600096A RID: 2410 RVA: 0x00104250 File Offset: 0x00104250
  void ICollection<T>.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x0600096B RID: 2411 RVA: 0x00104258 File Offset: 0x00104258
  bool ICollection<T>.Contains(T item)
  {
    return EqualityComparer<T>.Default.Equals(this._item, item);
  }

  // Token: 0x0600096C RID: 2412 RVA: 0x0010426C File Offset: 0x0010426C
  void ICollection<T>.CopyTo([Nullable(new byte[] { 1, 0 })] T[] array, int arrayIndex)
  {
    array[arrayIndex] = this._item;
  }

  // Token: 0x0600096D RID: 2413 RVA: 0x0010427C File Offset: 0x0010427C
  bool ICollection<T>.Remove(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x17000265 RID: 613
  T IList<T>.this[int index]
  {
    get
    {
      if (index != 0)
      {
        throw new IndexOutOfRangeException();
      }
      return this._item;
    }
    set
    {
      throw new NotSupportedException();
    }
  }

  // Token: 0x06000970 RID: 2416 RVA: 0x001042A0 File Offset: 0x001042A0
  int IList<T>.IndexOf(T item)
  {
    if (!EqualityComparer<T>.Default.Equals(this._item, item))
    {
      return -1;
    }
    return 0;
  }

  // Token: 0x06000971 RID: 2417 RVA: 0x001042B8 File Offset: 0x001042B8
  void IList<T>.Insert(int index, T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06000972 RID: 2418 RVA: 0x001042C0 File Offset: 0x001042C0
  void IList<T>.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x0400034C RID: 844
  [CompilerGenerated]
  private readonly T _item;

  // Token: 0x020001EF RID: 495
  private sealed class Enumerator : IDisposable, IEnumerator, IEnumerator<T>
  {
    // Token: 0x06000FB9 RID: 4025 RVA: 0x0011D4E4 File Offset: 0x0011D4E4
    public Enumerator(T item)
    {
      this.System.Collections.Generic.IEnumerator<T>.Current = item;
    }

    // Token: 0x17000309 RID: 777
    // (get) Token: 0x06000FBA RID: 4026 RVA: 0x0011D4F4 File Offset: 0x0011D4F4
    object IEnumerator.Current
    {
      get
      {
        return this._item;
      }
    }

    // Token: 0x1700030A RID: 778
    // (get) Token: 0x06000FBB RID: 4027 RVA: 0x0011D504 File Offset: 0x0011D504
    T IEnumerator<T>.Current
    {
      get
      {
        return this._item;
      }
    }

    // Token: 0x06000FBC RID: 4028 RVA: 0x0011D50C File Offset: 0x0011D50C
    bool IEnumerator.MoveNext()
    {
      return !this._moveNextCalled && (this._moveNextCalled = true);
    }

    // Token: 0x06000FBD RID: 4029 RVA: 0x0011D530 File Offset: 0x0011D530
    void IEnumerator.Reset()
    {
      this._moveNextCalled = false;
    }

    // Token: 0x06000FBE RID: 4030 RVA: 0x0011D53C File Offset: 0x0011D53C
    void IDisposable.Dispose()
    {
    }

    // Token: 0x04000920 RID: 2336
    [CompilerGenerated]
    private readonly T _item;

    // Token: 0x04000921 RID: 2337
    [CompilerGenerated]
    private bool _moveNextCalled;
  }
}
