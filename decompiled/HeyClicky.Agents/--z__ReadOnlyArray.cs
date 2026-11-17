using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x02000079 RID: 121
[CompilerGenerated]
internal sealed class <>z__ReadOnlyArray<T> : IEnumerable, ICollection, IList, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
  // Token: 0x06000933 RID: 2355 RVA: 0x00103FF4 File Offset: 0x00103FF4
  public <>z__ReadOnlyArray(T[] items)
  {
    this._items = items;
  }

  // Token: 0x06000934 RID: 2356 RVA: 0x00104004 File Offset: 0x00104004
  [return: Nullable(1)]
  IEnumerator IEnumerable.GetEnumerator()
  {
    return this._items.GetEnumerator();
  }

  // Token: 0x17000250 RID: 592
  // (get) Token: 0x06000935 RID: 2357 RVA: 0x00104014 File Offset: 0x00104014
  int ICollection.Count
  {
    get
    {
      return this._items.Length;
    }
  }

  // Token: 0x17000251 RID: 593
  // (get) Token: 0x06000936 RID: 2358 RVA: 0x00104020 File Offset: 0x00104020
  bool ICollection.IsSynchronized
  {
    get
    {
      return false;
    }
  }

  // Token: 0x17000252 RID: 594
  // (get) Token: 0x06000937 RID: 2359 RVA: 0x00104024 File Offset: 0x00104024
  object ICollection.SyncRoot
  {
    [return: Nullable(1)]
    get
    {
      return this;
    }
  }

  // Token: 0x06000938 RID: 2360 RVA: 0x00104028 File Offset: 0x00104028
  void ICollection.CopyTo([Nullable(1)] Array array, int index)
  {
    this._items.CopyTo(array, index);
  }

  // Token: 0x17000253 RID: 595
  object IList.this[int index]
  {
    [return: Nullable(2)]
    get
    {
      return this._items[index];
    }
    [param: Nullable(2)]
    set
    {
      throw new NotSupportedException();
    }
  }

  // Token: 0x17000254 RID: 596
  // (get) Token: 0x0600093B RID: 2363 RVA: 0x00104054 File Offset: 0x00104054
  bool IList.IsFixedSize
  {
    get
    {
      return true;
    }
  }

  // Token: 0x17000255 RID: 597
  // (get) Token: 0x0600093C RID: 2364 RVA: 0x00104058 File Offset: 0x00104058
  bool IList.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x0600093D RID: 2365 RVA: 0x0010405C File Offset: 0x0010405C
  int IList.Add([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x0600093E RID: 2366 RVA: 0x00104064 File Offset: 0x00104064
  void IList.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x0600093F RID: 2367 RVA: 0x0010406C File Offset: 0x0010406C
  bool IList.Contains([Nullable(2)] object value)
  {
    return this._items.Contains(value);
  }

  // Token: 0x06000940 RID: 2368 RVA: 0x0010407C File Offset: 0x0010407C
  int IList.IndexOf([Nullable(2)] object value)
  {
    return this._items.IndexOf(value);
  }

  // Token: 0x06000941 RID: 2369 RVA: 0x0010408C File Offset: 0x0010408C
  void IList.Insert(int index, [Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06000942 RID: 2370 RVA: 0x00104094 File Offset: 0x00104094
  void IList.Remove([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06000943 RID: 2371 RVA: 0x0010409C File Offset: 0x0010409C
  void IList.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06000944 RID: 2372 RVA: 0x001040A4 File Offset: 0x001040A4
  [return: Nullable(new byte[] { 1, 0 })]
  IEnumerator<T> IEnumerable<T>.GetEnumerator()
  {
    return this._items.GetEnumerator();
  }

  // Token: 0x17000256 RID: 598
  // (get) Token: 0x06000945 RID: 2373 RVA: 0x001040B4 File Offset: 0x001040B4
  int IReadOnlyCollection<T>.Count
  {
    get
    {
      return this._items.Length;
    }
  }

  // Token: 0x17000257 RID: 599
  T IReadOnlyList<T>.this[int index]
  {
    get
    {
      return this._items[index];
    }
  }

  // Token: 0x17000258 RID: 600
  // (get) Token: 0x06000947 RID: 2375 RVA: 0x001040D0 File Offset: 0x001040D0
  int ICollection<T>.Count
  {
    get
    {
      return this._items.Length;
    }
  }

  // Token: 0x17000259 RID: 601
  // (get) Token: 0x06000948 RID: 2376 RVA: 0x001040DC File Offset: 0x001040DC
  bool ICollection<T>.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x06000949 RID: 2377 RVA: 0x001040E0 File Offset: 0x001040E0
  void ICollection<T>.Add(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x0600094A RID: 2378 RVA: 0x001040E8 File Offset: 0x001040E8
  void ICollection<T>.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x0600094B RID: 2379 RVA: 0x001040F0 File Offset: 0x001040F0
  bool ICollection<T>.Contains(T item)
  {
    return this._items.Contains(item);
  }

  // Token: 0x0600094C RID: 2380 RVA: 0x00104100 File Offset: 0x00104100
  void ICollection<T>.CopyTo([Nullable(new byte[] { 1, 0 })] T[] array, int arrayIndex)
  {
    this._items.CopyTo(array, arrayIndex);
  }

  // Token: 0x0600094D RID: 2381 RVA: 0x00104110 File Offset: 0x00104110
  bool ICollection<T>.Remove(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x1700025A RID: 602
  T IList<T>.this[int index]
  {
    get
    {
      return this._items[index];
    }
    set
    {
      throw new NotSupportedException();
    }
  }

  // Token: 0x06000950 RID: 2384 RVA: 0x00104130 File Offset: 0x00104130
  int IList<T>.IndexOf(T item)
  {
    return this._items.IndexOf(item);
  }

  // Token: 0x06000951 RID: 2385 RVA: 0x00104140 File Offset: 0x00104140
  void IList<T>.Insert(int index, T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x06000952 RID: 2386 RVA: 0x00104148 File Offset: 0x00104148
  void IList<T>.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x0400034B RID: 843
  [CompilerGenerated]
  private readonly T[] _items;
}
