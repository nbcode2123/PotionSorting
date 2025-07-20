using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public int ShufflePrice;
    public int FreezeTimePrice;
    public void ShufflePotion()
    {
        if (CoinCalculator.Instance.CoinCounter >= ShufflePrice)
        {
            CoinCalculator.Instance.DecreaseCoin(ShufflePrice);

            var _listPotionInShelf = LevelManager.Instance.ListPotionInShelf;
            var _listShelfSlot = LevelManager.Instance.ListShelfSlots;
            var _listSingleShelf = LevelManager.Instance.ListSingleShelf;
            for (int i = 0; i < _listShelfSlot.Count; i++)
            {
                _listShelfSlot[i].ClearStack();
            }
            PutPotionToShelf(_listShelfSlot, _listPotionInShelf);
            CheckForMatchWhenInit(_listSingleShelf);
        }


    }
    public void PutPotionToShelf(List<IShelfSlot> listShelfSlot, List<GameObject> listPotionInShelf)
    {

        int _listShelfSlot = listShelfSlot.Count;
        List<IShelfSlot> _tempShelfSlot = new List<IShelfSlot>(listShelfSlot);
        Debug.Log(_tempShelfSlot.Count);
        for (int i = 0; i < listPotionInShelf.Count; i++)
        {
            listPotionInShelf[i].SetActive(false);
            listPotionInShelf[i].gameObject.GetComponent<DragAndDropController>().DisableThisPotion();
            if (listPotionInShelf.Count - i == _tempShelfSlot.Count)
            {
                int _tempRemoveRandomShelf1 = Random.Range(0, _tempShelfSlot.Count);
                _tempShelfSlot.RemoveAt(_tempRemoveRandomShelf1);
                int _tempRemoveRandomShelf2 = Random.Range(0, _tempShelfSlot.Count);
                _tempShelfSlot.RemoveAt(_tempRemoveRandomShelf2);
                int _tempRemoveRandomShelf3 = Random.Range(0, _tempShelfSlot.Count);
                _tempShelfSlot.RemoveAt(_tempRemoveRandomShelf3);
            }
            int _tempRandomShelf = Random.Range(0, _tempShelfSlot.Count);
            listShelfSlot[_tempRandomShelf].PushPotionToCollection(listPotionInShelf[i].GetComponent<Potion>());
        }
        for (int i = 0; i < listShelfSlot.Count; i++)
        {
            listShelfSlot[i].SecondPotionInCollectionSetUp();
            listShelfSlot[i].FirstPotionInCollectionSetUp();
        }





    }
    public void CheckForMatchWhenInit(List<ISingleShelf> listSingleShelf)
    {
        for (int i = 0; i < listSingleShelf.Count; i++)
        {
            listSingleShelf[i].CheckMatch();
        }
    }
    public void FreezeTime()
    {
        if (CoinCalculator.Instance.CoinCounter >= FreezeTimePrice)
        {
            CoinCalculator.Instance.DecreaseCoin(FreezeTimePrice);

            Timer.Instance.PauseTimer();
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(5f);
            sequence.AppendCallback(() => Timer.Instance.ContinuesTimer()).OnComplete(() =>
            {
                sequence.Kill();

            });

        }


    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
