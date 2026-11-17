using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x02000233 RID: 563
[CompilerGenerated]
internal sealed class <>z__ReadOnlyList<T> : IEnumerable, ICollection, IList, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
  // Token: 0x060020B4 RID: 8372 RVA: 0x003F69C0 File Offset: 0x003F69C0
  public <>z__ReadOnlyList(List<T> items)
  {
    this._items = items;
  }

  // Token: 0x060020B5 RID: 8373 RVA: 0x003F69D0 File Offset: 0x003F69D0
  [return: Nullable(1)]
  IEnumerator IEnumerable.GetEnumerator()
  {
    return ((IEnumerable)this._items).GetEnumerator();
  }

  // Token: 0x1700075B RID: 1883
  // (get) Token: 0x060020B6 RID: 8374 RVA: 0x003F69E0 File Offset: 0x003F69E0
  int ICollection.Count
  {
    get
    {
      return this._items.Count;
    }
  }

  // Token: 0x1700075C RID: 1884
  // (get) Token: 0x060020B7 RID: 8375 RVA: 0x003F69F0 File Offset: 0x003F69F0
  bool ICollection.IsSynchronized
  {
    get
    {
      return false;
    }
  }

  // Token: 0x1700075D RID: 1885
  // (get) Token: 0x060020B8 RID: 8376 RVA: 0x003F69F4 File Offset: 0x003F69F4
  object ICollection.SyncRoot
  {
    [return: Nullable(1)]
    get
    {
      return this;
    }
  }

  // Token: 0x060020B9 RID: 8377 RVA: 0x003F69F8 File Offset: 0x003F69F8
  void ICollection.CopyTo([Nullable(1)] Array array, int index)
  {
    ((ICollection)this._items).CopyTo(array, index);
  }

  // Token: 0x1700075E RID: 1886
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

  // Token: 0x1700075F RID: 1887
  // (get) Token: 0x060020BC RID: 8380 RVA: 0x003F6A24 File Offset: 0x003F6A24
  bool IList.IsFixedSize
  {
    get
    {
      return true;
    }
  }

  // Token: 0x17000760 RID: 1888
  // (get) Token: 0x060020BD RID: 8381 RVA: 0x003F6A28 File Offset: 0x003F6A28
  bool IList.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x060020BE RID: 8382 RVA: 0x003F6A2C File Offset: 0x003F6A2C
  int IList.Add([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020BF RID: 8383 RVA: 0x003F6A34 File Offset: 0x003F6A34
  void IList.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020C0 RID: 8384 RVA: 0x003F6A3C File Offset: 0x003F6A3C
  bool IList.Contains([Nullable(2)] object value)
  {
    return ((IList)this._items).Contains(value);
  }

  // Token: 0x060020C1 RID: 8385 RVA: 0x003F6A4C File Offset: 0x003F6A4C
  int IList.IndexOf([Nullable(2)] object value)
  {
    return ((IList)this._items).IndexOf(value);
  }

  // Token: 0x060020C2 RID: 8386 RVA: 0x003F6A5C File Offset: 0x003F6A5C
  void IList.Insert(int index, [Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020C3 RID: 8387 RVA: 0x003F6A64 File Offset: 0x003F6A64
  void IList.Remove([Nullable(2)] object value)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020C4 RID: 8388 RVA: 0x003F6A6C File Offset: 0x003F6A6C
  void IList.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020C5 RID: 8389 RVA: 0x003F6A74 File Offset: 0x003F6A74
  [return: Nullable(new byte[] { 1, 0 })]
  IEnumerator<T> IEnumerable<T>.GetEnumerator()
  {
    return ((IEnumerable<T>)this._items).GetEnumerator();
  }

  // Token: 0x17000761 RID: 1889
  // (get) Token: 0x060020C6 RID: 8390 RVA: 0x003F6A84 File Offset: 0x003F6A84
  int IReadOnlyCollection<T>.Count
  {
    get
    {
      return this._items.Count;
    }
  }

  // Token: 0x17000762 RID: 1890
  T IReadOnlyList<T>.this[int index]
  {
    get
    {
      return this._items[index];
    }
  }

  // Token: 0x17000763 RID: 1891
  // (get) Token: 0x060020C8 RID: 8392 RVA: 0x003F6AA4 File Offset: 0x003F6AA4
  int ICollection<T>.Count
  {
    get
    {
      return this._items.Count;
    }
  }

  // Token: 0x17000764 RID: 1892
  // (get) Token: 0x060020C9 RID: 8393 RVA: 0x003F6AB4 File Offset: 0x003F6AB4
  bool ICollection<T>.IsReadOnly
  {
    get
    {
      return true;
    }
  }

  // Token: 0x060020CA RID: 8394 RVA: 0x003F6AB8 File Offset: 0x003F6AB8
  void ICollection<T>.Add(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020CB RID: 8395 RVA: 0x003F6AC0 File Offset: 0x003F6AC0
  void ICollection<T>.Clear()
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020CC RID: 8396 RVA: 0x003F6AC8 File Offset: 0x003F6AC8
  bool ICollection<T>.Contains(T item)
  {
    return this._items.Contains(item);
  }

  // Token: 0x060020CD RID: 8397 RVA: 0x003F6AD8 File Offset: 0x003F6AD8
  void ICollection<T>.CopyTo([Nullable(new byte[] { 1, 0 })] T[] array, int arrayIndex)
  {
    this._items.CopyTo(array, arrayIndex);
  }

  // Token: 0x060020CE RID: 8398 RVA: 0x003F6AE8 File Offset: 0x003F6AE8
  bool ICollection<T>.Remove(T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x17000765 RID: 1893
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

  // Token: 0x060020D1 RID: 8401 RVA: 0x003F6B08 File Offset: 0x003F6B08
  int IList<T>.IndexOf(T item)
  {
    return this._items.IndexOf(item);
  }

  // Token: 0x060020D2 RID: 8402 RVA: 0x003F6B18 File Offset: 0x003F6B18
  void IList<T>.Insert(int index, T item)
  {
    throw new NotSupportedException();
  }

  // Token: 0x060020D3 RID: 8403 RVA: 0x003F6B20 File Offset: 0x003F6B20
  void IList<T>.RemoveAt(int index)
  {
    throw new NotSupportedException();
  }

  // Token: 0x04000DF7 RID: 3575
  [CompilerGenerated]
  private readonly List<T> _items;
}
