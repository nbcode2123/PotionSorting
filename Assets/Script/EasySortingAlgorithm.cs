using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasySortingAlgorithm : MonoBehaviour, ISortingAlgorithm
{
    public IPotionShelf PotionShelf { get; set; }
    public List<ISingleShelf> ListSingleShelf { get; set; }
    public List<IShelfSlot> ListShelfSlot { get; set; }
    public List<GameObject> ListPotion;
    public int MultiplesPotion;

    public EasySortingAlgorithm(IPotionShelf potionShelf, int multiplesPotion)
    {
        PotionShelf = potionShelf;
        MultiplesPotion = multiplesPotion;
    }

    public void CreateListSingleShelf()
    {
        ListSingleShelf = new List<ISingleShelf>();
        ListSingleShelf = PotionShelf.GetListSingleShelf();
        LevelManager.Instance.ListSingleShelf.AddRange(ListSingleShelf);
        // RemoveRandomSingleShelf();

    }

    public void CreateListShelfSlot()
    {
        ListShelfSlot = new List<IShelfSlot>();
        for (int i = 0; i < ListSingleShelf.Count; i++)
        {
            ListShelfSlot.AddRange(ListSingleShelf[i].GetListShelfSlot());
        }
        LevelManager.Instance.ListShelfSlots.AddRange(ListShelfSlot);




    }
    // public void RemoveRandomSingleShelf()
    // {
    //     int _temp = Random.Range(0, ListSingleShelf.Count);
    //     ListSingleShelf.Remove(ListSingleShelf[_temp]);
    // }
    // public void RemoveRandomSingleShelf()
    // {
    //     int _temp = Random.Range(0, ListSingleShelf.Count);
    //     ListSingleShelf.Remove(ListSingleShelf[_temp]);
    // }
    public void CreatePoolPotion() //! ve sau neu co the chuyen sang object pooling 
    {
        ListPotion = new List<GameObject>();
        int _singleShelfCount = ListSingleShelf.Count;
        GameObject _currentPotionPrefab;
        for (int i = 0; i < _singleShelfCount * MultiplesPotion; i++)
        {
            _currentPotionPrefab = GameObjectStorage.Instance.PotionStorage.GetRandomPotion();
            for (int j = 0; j < 3; j++)
            {
                // GameObject _potion = instantiateTesting.CreateObj(_currentPotionPrefab);
                GameObject _potion = Instantiate(_currentPotionPrefab);
                CompleteChecker.Instance.AddPotionInLevel(_potion);
                ListPotion.Add(_potion);
                LevelManager.Instance.ListPotionInLevel.Add(_potion);


            }



        }

    }
    // public void ShufflePotion()
    // {
    //     int n = ListPotion.Count;
    //     for (int i = 0; i < n; i++)
    //     {
    //         var _temp1 = Random.Range(0, n);
    //         var _temp2 = Random.Range(0, n);

    //         (ListPotion[_temp1], ListPotion[_temp2]) = (ListPotion[_temp2], ListPotion[_temp1]);
    //     }
    // }
    public void PutPotionToShelf()
    {
        // List<IShelfSlot> _copyListShelfSlot = new List<IShelfSlot>(ListShelfSlot);
        // List<IShelfSlot> _listShuffleShelfSlot = new List<IShelfSlot>();
        // IShelfSlot _tempIShelfSlot;
        // int _randomShelfSlot;

        // for (int i = 0; i < ListPotion.Count; i++)
        // {
        //     _randomShelfSlot = Random.Range(0, _copyListShelfSlot.Count + 1);
        //     _tempIShelfSlot = _copyListShelfSlot[_randomShelfSlot];
        //     _listShuffleShelfSlot.Add(_tempIShelfSlot);
        //     _copyListShelfSlot.Remove(_tempIShelfSlot);


        // }
        // for (int i = 0; i < _listShelfSlotHavePotion.Count; i++)
        // {
        //     _listShelfSlotHavePotion[i].SetPotion(ListPotion[i].GetComponent<Potion>());
        // }
        // for (int i = 0; i < _listShelfSlotHavePotion.Count; i++)
        // {
        //     _listShelfSlotHavePotion[0].PushPotionToCollection(ListPotion[i].GetComponent<Potion>());

        // }
        // for (int i = 0; i < _listShuffleShelfSlot.Count; i++)
        // {
        //     // int _potionInStack = Random.Range(1, MultiplesPotion + 1);
        //     for (int j = 0; j < 3; j++)
        //     {
        //         _listShuffleShelfSlot[i].PushPotionToCollection(ListPotion[j].GetComponent<Potion>());

        //     }

        // }
        int _ListShelfSlot = ListShelfSlot.Count;
        for (int i = 0; i < ListPotion.Count; i++)
        {

            if (ListPotion.Count - i == _ListShelfSlot)
            {
                int _tempRemoveRandomShelf = Random.Range(0, ListShelfSlot.Count);
                ListShelfSlot.RemoveAt(_tempRemoveRandomShelf);


            }
            int _tempRandomShelf = Random.Range(0, ListShelfSlot.Count);


            ListShelfSlot[_tempRandomShelf].PushPotionToCollection(ListPotion[i].GetComponent<Potion>());





        }
        for (int i = 0; i < ListShelfSlot.Count; i++)
        {
            ListShelfSlot[i].FirstPotionInCollectionSetUp();
            ListShelfSlot[i].SecondPotionInCollectionSetUp();
        }




    }
    public void CheckForMatchWhenInit()
    {
        for (int i = 0; i < ListSingleShelf.Count; i++)
        {
            ListSingleShelf[i].CheckMatch();
        }
    }
    public void SortingAlgorithm()
    {
        CreateListSingleShelf();
        CreateListShelfSlot();
        CreatePoolPotion();
        // ShufflePotion();
        PutPotionToShelf();



    }
}
