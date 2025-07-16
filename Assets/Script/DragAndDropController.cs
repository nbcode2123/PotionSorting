using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropController : MonoBehaviour
{
    [SerializeField] private Collider2D Collider;
    [SerializeField] private Vector3 LastPosition;
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
        LastPosition = gameObject.transform.position;
        // Bóp: Thu nhỏ Y, tăng X một chút


    }
    private void OnMouseDown()
    {
        // LastPosition = transform.position;
        gameObject.transform.position = GetMousePositionInWorldSpace();
        SpriteRenderer.sortingOrder = 1;




    }
    private void OnMouseDrag()
    {
        gameObject.transform.position = GetMousePositionInWorldSpace();

    }
    private void OnMouseUp()
    {
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
                    gameObject.transform.position = NewShelfSlot.GetPivotPosition();
                    LastPosition = NewShelfSlot.GetPivotPosition();
                    SlotsEmptyChecker.Instance.CheckEmptySlot();

                }
                else
                {
                    transform.position = LastPosition;

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
                    gameObject.transform.position = NewShelfSlot.GetPivotPosition();
                    LastPosition = NewShelfSlot.GetPivotPosition();
                    NewShelfSlot.NotifyToCheckMatch();
                    SlotsEmptyChecker.Instance.CheckEmptySlot();



                }
                else
                {
                    transform.position = LastPosition;

                }
            }
        }
        else
        {
            transform.position = LastPosition;

        }


    }
    public void SquashAndStretch()
    {
        transform.DOScale(new Vector3(1.2f, 0.8f, 1f), 0.2f).SetEase(Ease.InOutQuad).SetLoops(2, LoopType.Yoyo);// quay lại kích thước gốc sau 1 giây
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


}
