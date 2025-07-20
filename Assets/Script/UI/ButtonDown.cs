
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite Idle;
    public Sprite MouseDown;
    public GameObject Btn;
    public Vector3 scale;
    private void Start()
    {
        scale = Btn.transform.localScale;

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Btn.GetComponent<Image>().sprite = MouseDown;
        Btn.transform.DOScale(new Vector3(scale.x, scale.y, scale.z) * 0.75f, 0.1f).SetEase(Ease.OutQuad);
        ObserverManager.Notify("ClickBtn", "ClickBtn");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Btn.GetComponent<Image>().sprite = Idle;
        Btn.transform.DOScale(new Vector3(scale.x, scale.y, scale.z), 0.1f).SetEase(Ease.OutQuad);
    }
}
