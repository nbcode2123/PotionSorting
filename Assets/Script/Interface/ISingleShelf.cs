using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISingleShelf
{
    List<IShelfSlot> ListShelfSlots { set; get; }
    List<IShelfSlot> GetListShelfSlot();
    IPotionShelf PotionShelf { set; get; }
    void AddShelfSlot(IShelfSlot shelfSlot);
    void CheckMatch();
    void CheckEmpty();
}
