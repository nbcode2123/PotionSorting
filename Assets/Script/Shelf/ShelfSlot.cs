using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ShelfSlot : MonoBehaviour, IShelfSlot
{

    public Potion CurrentPotion { get; set; }
    public ISingleShelf SingleShelf { get; set; }
    public GameObject MiddlePivot;
    public GameObject BehindPivot;
    public Stack<Potion> StackPotionInSlot;
    public bool isEmpty;
    public Tween TweenEffectColor;
    public Tween TweenEffectTransform;
    private void Awake()
    {

        SingleShelf = gameObject.transform.parent.GetComponent<ISingleShelf>();
        SingleShelf.AddShelfSlot(this);
        CurrentPotion = null;
        StackPotionInSlot = new Stack<Potion>();
    }
    private void Start()
    {
    }


    public Vector3 GetPivotPosition()
    {
        return MiddlePivot.transform.position;
    }
    public void SetPotion(Potion potion)
    {
        CurrentPotion = potion;
        isEmpty = false;


        GameObject _potionObj = CurrentPotion.gameObject;
        _potionObj.GetComponent<DragAndDropController>().SetLastShelfSlot(this);
        _potionObj.transform.position = MiddlePivot.transform.position;
        _potionObj.GetComponent<DragAndDropController>().SetLastPosition(MiddlePivot.transform.position);

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        // NotifyToCheckMatch();


    }
    public void UnSetPotion() // 
    {
        CurrentPotion = null;
        isEmpty = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        // FirstPotionInCollectionSetUp();
        // SecondPotionInCollectionSetUp();
        // NotifyToCheckEmpty();
    }

    public Potion GetPotion()
    {
        if (CurrentPotion != null)
        {
            return CurrentPotion;
        }
        else return null;
    }

    public void NotifyToCheckMatch()
    {
        SingleShelf.CheckMatch();
    }
    public void NotifyToCheckEmpty()
    {
        SingleShelf.CheckEmpty();
    }

    public void PushPotionToCollection(Potion potion)
    {
        StackPotionInSlot.Push(potion);
        GameObject _potionObj = potion.gameObject;
        _potionObj.SetActive(false);


    }

    public void FirstPotionInCollectionSetUp()
    {
        if (StackPotionInSlot.Count == 0)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            CurrentPotion = null;
            isEmpty = true;

        }
        else
        {
            Potion _tempPotion = StackPotionInSlot.Pop();
            CurrentPotion = _tempPotion;
            GameObject _potionObj = CurrentPotion.gameObject;
            _potionObj.GetComponent<Collider2D>().enabled = false;
            _potionObj.GetComponent<DragAndDropController>().SetLastShelfSlot(this);
            _potionObj.GetComponent<DragAndDropController>().SetLastPosition(MiddlePivot.transform.position);

            _potionObj.SetActive(true);

            // _potionObj.transform.position = MiddlePivot.transform.position;
            // _potionObj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

            TweenEffectColor = _potionObj.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 1f), 0.5f).OnComplete(() => { TweenEffectColor.Kill(); });
            TweenEffectTransform = _potionObj.transform.DOMove(MiddlePivot.transform.position, 0.5f)
             .OnComplete(() =>
                 {
                     _potionObj.GetComponent<Collider2D>().enabled = true;
                     TweenEffectTransform.Kill();
                 }
             );


            _potionObj.GetComponent<SpriteRenderer>().sortingOrder = 0;
            _potionObj.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            isEmpty = false;


        }
        SecondPotionInCollectionSetUp();
    }

    public void SecondPotionInCollectionSetUp()
    {
        if (StackPotionInSlot.Count == 0)
        {
            return;
            // gameObject.GetComponent<Collider2D>().enabled = true;
            // CurrentPotion = null;


        }
        else
        {
            Potion _tempPotion = StackPotionInSlot.Peek();
            GameObject _potionObj = _tempPotion.gameObject;
            _potionObj.SetActive(true);
            _potionObj.GetComponent<DragAndDropController>().SetLastShelfSlot(this);
            _potionObj.GetComponent<SpriteRenderer>().color = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);
            _potionObj.GetComponent<SpriteRenderer>().sortingOrder = -1;
            _potionObj.GetComponent<BoxCollider2D>().enabled = false;
            _potionObj.transform.position = BehindPivot.transform.position;
            _potionObj.GetComponent<DragAndDropController>().SetLastPosition(MiddlePivot.transform.position);



        }
    }
    public List<Potion> GetPotionInStack()
    {
        List<Potion> _listPotion = new List<Potion>(StackPotionInSlot.Reverse());
        return _listPotion;



    }
    public void ClearStack()
    {
        CurrentPotion = null;
        StackPotionInSlot = new Stack<Potion>();

    }

}
