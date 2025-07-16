using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShelfSlot
{
    Potion CurrentPotion { get; set; }
    ISingleShelf SingleShelf { get; set; }

    void SetPotion(Potion potion);
    void UnSetPotion();
    Potion GetPotion();
    Vector3 GetPivotPosition();
    void PushPotionToCollection(Potion potion);
    void FirstPotionInCollectionSetUp();
    void SecondPotionInCollectionSetUp();
    void NotifyToCheckMatch();

    void NotifyToCheckEmpty();
    List<Potion> GetPotionInStack();





}
