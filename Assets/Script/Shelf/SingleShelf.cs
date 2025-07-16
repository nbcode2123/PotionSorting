using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class SingleShelf : MonoBehaviour, ISingleShelf
{
    public List<IShelfSlot> ListShelfSlots { get; set; }
    public List<Potion> ListPotions { get; set; }
    public IPotionShelf PotionShelf { set; get; }
    private IMatchChecker matchChecker;
    public List<GameObject> ListPotionInLevel;
    [SerializeField] public List<GameObject> ListShelfSlotsObj;



    private void Awake()
    {
        matchChecker = new NormalMatch();
        ListShelfSlots = new List<IShelfSlot>();
        ListPotions = new List<Potion>();
        PotionShelf = gameObject.transform.parent.GetComponent<IPotionShelf>();
        PotionShelf.AddSingleShelf(this);

        ListShelfSlotsObj = new List<GameObject>();
    }
    private void Start()
    {


    }

    public void AddShelfSlot(IShelfSlot shelfSlot)
    {
        ListShelfSlots.Add(shelfSlot);
        ListShelfSlotsObj.Add(((MonoBehaviour)shelfSlot).gameObject);

    }
    public List<IShelfSlot> GetListShelfSlot()
    {

        return ListShelfSlots;
    }
    public void CheckMatch()
    {
        ListPotions.Clear();
        for (int i = 0; i < ListShelfSlots.Count; i++)
        {
            if (ListShelfSlots[i].GetPotion() == null)
            {
                ListPotions.Clear();
                Debug.Log(gameObject.name + "Single shelf not match");
                return;
            }
            else
            {
                ListPotions.Add(ListShelfSlots[i].GetPotion());
                ListPotionInLevel.Add(((MonoBehaviour)ListShelfSlots[i]).gameObject);
            }


        }

        matchChecker.CheckMatch(ListPotions);
        Debug.Log(gameObject.name + "Single shelf not match");

        if (ListPotions.Count == 0)
        {
            for (int i = 0; i < ListShelfSlots.Count; i++)
            {
                // GameObject _tempObj = ((MonoBehaviour)ListShelfSlots[i]).gameObject;
                // _tempObj.GetComponent<Collider2D>().enabled = true;
                // ListShelfSlots[i].UnSetPotion();
                // // Debug.Log(_tempObj.GetComponent<Collider2D>().enabled);
                ListShelfSlots[i].FirstPotionInCollectionSetUp();
                ListShelfSlots[i].SecondPotionInCollectionSetUp();
            }
        }

    }
    public void CheckEmpty()
    {
        for (int i = 0; i < ListShelfSlots.Count; i++)
        {
            if (ListShelfSlots[i].GetPotion() != null)
            {
                // Debug.Log(gameObject.name + "SingleShelf NotEmpty");

                return;
            }
        }
        for (int i = 0; i < ListShelfSlots.Count; i++)
        {

            ListShelfSlots[i].FirstPotionInCollectionSetUp();
            ListShelfSlots[i].SecondPotionInCollectionSetUp();

        }




    }

}
