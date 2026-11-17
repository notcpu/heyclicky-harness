using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x02000232 RID: 562
[CompilerGenerated]
internal sealed class <>z__ReadOnlyArray<T> : IEnumerable, ICollection, IList, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
  // Token: 0x06002094 RID: 8340 RVA: 0x003F6864 File Offset: 0x003F6864
  public <>z__ReadOnlyArray(T[] items)
  {
    this._items = items;
  }

  // Token: 0x06002095 RID: 8341 RVA: 0x003F6874 File Offset: 0x003F6874
  [return: Nullable(1)]
  IEnumerator IEnumerable.GetEnumerator()
  {
    return this._items.GetEnumerator();
  }

  // Token: 0x17000750 RID: 1872
  // (get) Token: 0x06002096 RID: 8342 RVA: 0x003F6884 File Offset: 0x003F6884
  int ICollection.Count
  {
    get
    {
      return this._items.Length;
    }
  }

  // Token: 0x17000751 RID: 1873
  // (get) Token: 0x06002097 RID: 8343 RVA: 0x003F6890 File Offset: 0x003F6890
  bool ICollection.IsSynchronized
  {
    get
    {
      return false;
    }
  }

  // Token: 0x17000752 RID: 1874
  // (get) Token: 0x06002098 RID: 8344 RVA: 0x003F6894 File Offset: 0x003F6894
  object ICollection.SyncRoot
  {
    [return: Nullable(1)]
    get
    {
      return this;
    }
  }

  // Token: 0x06002099 RID: 8345 RVA: 0x003F6898 File Offset: 0x003F6898
  void ICollection.CopyTo([Nullable(1)] Array array, int index)
  {
    this._items.CopyTo(array, index);
  }

  // Token: 0x17000753 RID: 1875
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

  // Token: 0x17000754 RID: 1876
  // (get) Token: 0x0600209C RID: 8348 RVA: 0x003F68C4 File Offset: 0x003F68C4
  bool IList.IsFixedSize
  {
    get
    {
      return true;
    }
  }

  // Token: 0x17000755 RID: 1877
  // (get) Token: 0x0600209D RID: 8349 RVA: 0x003F68C8 File Offset: 0x003F68C8
  bool IList.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x0600209E RID: 8350 RVA: 0x003F68CC File Offset: 0x003F68CC
  int IList.Add([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x0600209F RID: 8351 RVA: 0x003F68D4 File Offset: 0x003F68D4
  void IList.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020A0 RID: 8352 RVA: 0x003F68DC File Offset: 0x003F68DC
  bool IList.Contains([Nullable(2)] object value)
  {
    return this._items.Contains(value);
  }

  // Token: 0x060020A1 RID: 8353 RVA: 0x003F68EC File Offset: 0x003F68EC
  int IList.IndexOf([Nullable(2)] object value)
  {
    return this._items.IndexOf(value);
  }

  // Token: 0x060020A2 RID: 8354 RVA: 0x003F68FC File Offset: 0x003F68FC
  void IList.Insert(int index, [Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020A3 RID: 8355 RVA: 0x003F6904 File Offset: 0x003F6904
  void IList.Remove([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020A4 RID: 8356 RVA: 0x003F690C File Offset: 0x003F690C
  void IList.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020A5 RID: 8357 RVA: 0x003F6914 File Offset: 0x003F6914
  [return: Nullable(new byte[] { 1, 0 })]
  IEnumerator<T> IEnumerable<T>.GetEnumerator()
  {
    return this._items.GetEnumerator();
  }

  // Token: 0x17000756 RID: 1878
  // (get) Token: 0x060020A6 RID: 8358 RVA: 0x003F6924 File Offset: 0x003F6924
  int IReadOnlyCollection<T>.Count
  {
    get
    {
      return this._items.Length;
    }
  }

  // Token: 0x17000757 RID: 1879
  T IReadOnlyList<T>.this[int index]
  {
    get
    {
      return this._items[index];
    }
  }

  // Token: 0x17000758 RID: 1880
  // (get) Token: 0x060020A8 RID: 8360 RVA: 0x003F6940 File Offset: 0x003F6940
  int ICollection<T>.Count
  {
    get
    {
      return this._items.Length;
    }
  }

  // Token: 0x17000759 RID: 1881
  // (get) Token: 0x060020A9 RID: 8361 RVA: 0x003F694C File Offset: 0x003F694C
  bool ICollection<T>.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x060020AA RID: 8362 RVA: 0x003F6950 File Offset: 0x003F6950
  void ICollection<T>.Add(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020AB RID: 8363 RVA: 0x003F6958 File Offset: 0x003F6958
  void ICollection<T>.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020AC RID: 8364 RVA: 0x003F6960 File Offset: 0x003F6960
  bool ICollection<T>.Contains(T item)
  {
    return this._items.Contains(item);
  }

  // Token: 0x060020AD RID: 8365 RVA: 0x003F6970 File Offset: 0x003F6970
  void ICollection<T>.CopyTo([Nullable(new byte[] { 1, 0 })] T[] array, int arrayIndex)
  {
    this._items.CopyTo(array, arrayIndex);
  }

  // Token: 0x060020AE RID: 8366 RVA: 0x003F6980 File Offset: 0x003F6980
  bool ICollection<T>.Remove(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x1700075A RID: 1882
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

  // Token: 0x060020B1 RID: 8369 RVA: 0x003F69A0 File Offset: 0x003F69A0
  int IList<T>.IndexOf(T item)
  {
    return this._items.IndexOf(item);
  }

  // Token: 0x060020B2 RID: 8370 RVA: 0x003F69B0 File Offset: 0x003F69B0
  void IList<T>.Insert(int index, T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020B3 RID: 8371 RVA: 0x003F69B8 File Offset: 0x003F69B8
  void IList<T>.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x04000DF6 RID: 3574
  [CompilerGenerated]
  private readonly T[] _items;
}
