using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsEmptyChecker : MonoBehaviour
{
    public static SlotsEmptyChecker Instance { private set; get; }
    public List<IShelfSlot> ListShelfSlot;
    public List<Potion> ListPotionLeft;


    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }
    private void Start()
    {
        ListShelfSlot = LevelManager.Instance.ListShelfSlots;
    }
    public void CheckEmptySlot()
    {
        Debug.Log(ListShelfSlot.Count);
        for (int i = 0; i < ListShelfSlot.Count; i++)
        {
            if (ListShelfSlot[i].GetPotion() == null)
            {

                return;
            }
        }
        Debug.Log("All slot have potion");
        for (int i = 0; i < ListShelfSlot.Count; i++)
        {
            ListPotionLeft.Add(ListShelfSlot[i].GetPotion());
            ListPotionLeft.AddRange(ListShelfSlot[i].GetPotionInStack());
        }
        for (int i = 0; i < ListPotionLeft.Count; i++)
        {
            ListPotionLeft[i].gameObject.SetActive(false);
        }

    }

}
