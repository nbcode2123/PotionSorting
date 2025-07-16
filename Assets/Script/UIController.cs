using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject ObjDisable;
    public GameObject PlayBtnObj;
    public Image CircleMaskObj;
    public GameObject PauseCanvas;
    public GameObject SoundBtn;
    public GameObject MusicBtn;
    public GameObject ContinuesBtn;
    public void Start()
    {
        PauseCanvas.SetActive(false);

    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseCanvas.SetActive(true);
    }
    public void Continues()
    {
        Time.timeScale = 1;
    }
    public void PlayBtn()
    {
        CircleMaskZoomToLevel();


    }
    public void CircleMaskZoomToLevel()
    {

        RectTransform rt = CircleMaskObj.GetComponent<RectTransform>();
        Vector2 _default = rt.sizeDelta;
        Sequence seq = DOTween.Sequence();

        seq.Append(rt.DOSizeDelta(Vector2.zero, 1)
               .SetEase(Ease.OutQuad)

               .OnComplete(() =>
               {
                   // 👈 Chạy sau mỗi vòng (đi hoặc về)
                   if (Vector2.Distance(rt.sizeDelta, Vector2.zero) < 0.01f)
                   {
                       ObserverManager.Notify("New Level");
                       ObjDisable.SetActive(false);
                       PlayBtnObj.SetActive(false);
                   }

               }));
        seq.AppendInterval(1f);
        seq.Append(rt.DOSizeDelta(_default, 1)
                    .SetEase(Ease.OutQuad))
;




    }
}
