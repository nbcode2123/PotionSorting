using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISortingAlgorithm
{
    IPotionShelf PotionShelf { get; set; }
    List<ISingleShelf> ListSingleShelf { get; set; }
    List<IShelfSlot> ListShelfSlot { get; set; }
    void CreateListSingleShelf();
    void CreateListShelfSlot();
    void SortingAlgorithm();

}
