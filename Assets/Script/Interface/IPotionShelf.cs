using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPotionShelf
{
    List<ISingleShelf> ListSingleShelf { set; get; }
    List<ISingleShelf> GetListSingleShelf();
    void AddSingleShelf(ISingleShelf singleShelf);





}
