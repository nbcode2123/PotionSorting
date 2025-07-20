using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropController : MonoBehaviour
{
    [SerializeField] private Collider2D Collider;
    public Vector3 LastPosition;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    private Potion Potion;
    private IShelfSlot LastShelfSlot;
    private void Awake()
    {
        Collider = gameObject.GetComponent<Collider2D>();
        Potion = GetComponent<Potion>();
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {


    }
    private void OnMouseDown()
    {

        ObserverManager.Notify("Start");



        gameObject.transform.position = GetMousePositionInWorldSpace();
        SpriteRenderer.sortingOrder = 1;




    }
    private void OnMouseDrag()
    {
        gameObject.transform.position = GetMousePositionInWorldSpace();

    }
    private void OnMouseUp()
    {
        ObserverManager.Notify("Drop", "DropPotion");
        SquashAndStretch();
        SpriteRenderer.sortingOrder = 0;
        Collider2D hit = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("ShelfSlot"));
        if (hit != null && hit.gameObject.TryGetComponent(out IShelfSlot NewShelfSlot))
        {
            ISingleShelf _lastShelfSlot = LastShelfSlot.SingleShelf;
            ISingleShelf _newShelfSLot = NewShelfSlot.SingleShelf;
            if (_lastShelfSlot == _newShelfSLot)
            {
                if (NewShelfSlot.GetPotion() == null)
                {
                    LastShelfSlot.UnSetPotion();
                    NewShelfSlot.SetPotion(Potion);
                    LastShelfSlot = NewShelfSlot;
                    LastPosition = NewShelfSlot.GetPivotPosition();
                    SlotsEmptyChecker.Instance.CheckEmptySlot();

                }
                else
                {
                    gameObject.transform.position = LastPosition;

                }
            }
            else if (_lastShelfSlot != _newShelfSLot)
            {

                if (NewShelfSlot.GetPotion() == null)
                {
                    LastShelfSlot.UnSetPotion();
                    LastShelfSlot.NotifyToCheckEmpty();
                    NewShelfSlot.SetPotion(Potion);
                    LastShelfSlot = NewShelfSlot;
                    LastPosition = NewShelfSlot.GetPivotPosition();
                    NewShelfSlot.NotifyToCheckMatch();
                    SlotsEmptyChecker.Instance.CheckEmptySlot();



                }
                else
                {
                    gameObject.transform.position = LastPosition;

                }
            }
        }
        else
        {
            gameObject.transform.position = LastPosition;

        }


    }
    public void SquashAndStretch()
    {
        transform.DOScale(new Vector3(1.2f, 0.8f, 1f), 0.2f).SetEase(Ease.InOutQuad).SetLoops(2, LoopType.Yoyo);
        SpriteRenderer.sortingOrder = 0;
    }
    public void SetLastShelfSlot(IShelfSlot lastShelfSlot)
    {
        LastShelfSlot = lastShelfSlot;

    }
    public void DisableThisPotion()
    {
        LastShelfSlot = null;
        gameObject.GetComponent<Collider2D>().enabled = false;




    }
    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 _pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _pos.z = 0;
        return _pos;
    }
    public void SetLastPosition(Vector3 position)
    {
        LastPosition = position;
    }


}
