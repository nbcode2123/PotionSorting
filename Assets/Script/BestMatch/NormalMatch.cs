using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMatch : MonoBehaviour, IMatchChecker
{


    public void CheckMatch(List<Potion> listPotion)
    {

        ColorType colorType = listPotion[0].GetColor();
        ShapeType shapeType = listPotion[0].GetShape();

        for (int i = 1; i < listPotion.Count; i++)
        {
            if (listPotion[i].GetColor() != colorType)
            {
                Debug.Log("Single shelf not match");

                return;

            }
            if (listPotion[i].GetShape() != shapeType)
            {
                Debug.Log("Single shelf not match");

                return;

            }
        }
        for (int i = 0; i < listPotion.Count; i++)
        {
            listPotion[i].gameObject.GetComponent<DragAndDropController>().DisableThisPotion();
            listPotion[i].gameObject.SetActive(false);
            ObserverManager.Notify("BestMatch", listPotion[i].gameObject);

        }
        listPotion.Clear();
        // if (potion1 == null || potion2 == null || potion3 == null)
        // {
        //     return;
        // }
        // if (potion1.GetColor() == potion2.GetColor() && potion2.GetColor() == potion3.GetColor())
        // {
        //     if (potion1.GetShape() == potion2.GetShape() && potion2.GetShape() == potion3.GetShape())
        //     {
        //         Debug.Log("Match");
        //     }
        // }



    }


}
