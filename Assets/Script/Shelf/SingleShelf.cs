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
                return;
            }
            else
            {
                ListPotions.Add(ListShelfSlots[i].GetPotion());
                ListPotionInLevel.Add(((MonoBehaviour)ListShelfSlots[i]).gameObject);
            }


        }

        matchChecker.CheckMatch(ListPotions);


        if (ListPotions.Count == 0)
        {
            CoinCreateEffect.Instance.CreateCoin(CoinCalculator.Instance.CoinDefault, gameObject.transform.position);
            for (int i = 0; i < ListShelfSlots.Count; i++)
            {
                ListShelfSlots[i].SecondPotionInCollectionSetUp();
                ListShelfSlots[i].FirstPotionInCollectionSetUp();

            }
        }
        CheckEmpty();

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
            ListShelfSlots[i].SecondPotionInCollectionSetUp();
            ListShelfSlots[i].FirstPotionInCollectionSetUp();

        }




    }

}
