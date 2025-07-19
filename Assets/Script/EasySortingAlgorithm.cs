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
                LevelManager.Instance.ListPotionInShelf.Add(_potion);



            }



        }

    }

    public void PutPotionToShelf()
    {

        int _ListShelfSlot = ListShelfSlot.Count;
        for (int i = 0; i < ListPotion.Count; i++)
        {

            if (ListPotion.Count - i == _ListShelfSlot)
            {
                int _tempRemoveRandomShelf1 = Random.Range(0, ListShelfSlot.Count);
                ListShelfSlot.RemoveAt(_tempRemoveRandomShelf1);
                int _tempRemoveRandomShelf2 = Random.Range(0, ListShelfSlot.Count);
                ListShelfSlot.RemoveAt(_tempRemoveRandomShelf2);
                int _tempRemoveRandomShelf3 = Random.Range(0, ListShelfSlot.Count);
                ListShelfSlot.RemoveAt(_tempRemoveRandomShelf3);



            }
            int _tempRandomShelf = Random.Range(0, ListShelfSlot.Count);


            ListShelfSlot[_tempRandomShelf].PushPotionToCollection(ListPotion[i].GetComponent<Potion>());





        }
        for (int i = 0; i < ListShelfSlot.Count; i++)
        {
            ListShelfSlot[i].SecondPotionInCollectionSetUp();
            ListShelfSlot[i].FirstPotionInCollectionSetUp();
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
        CheckForMatchWhenInit();



    }
}
