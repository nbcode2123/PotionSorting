using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboController : MonoBehaviour
{
    public static ComboController Instance { get; private set; }
    public Slider Slide;
    public GameObject ComboTextObj;
    public TextMeshProUGUI ComboText;
    public int ComboCounter;
    public GameObject FillArea;
    public float DurationComboTime;
    public float DurationDecreaseEachCombo;
    public float CurrentComboTime;
    public Tween TweenComboTime;



    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void Start()
    {
        ComboCounter = 0;
        ComboText.text = "";
        Slide.value = 0;
        FillArea.SetActive(false);
        ObserverManager.AddListener("BestMatch", ComboActive);
        ObserverManager.AddListener("New Level", ResetCombo);
        ObserverManager.AddListener("BackToMenu", ResetCombo);

    }
    public void ComboActive()
    {
        FillArea.SetActive(true);

        ComboCounter++;
        ComboTextObj.SetActive(true);
        ComboText.text = $"Combo X{ComboCounter}";
        StartTimerCombo();
        DurationComboTime -= DurationDecreaseEachCombo;




    }
    public void ResetCombo()
    {
        FillArea.SetActive(false);

        ComboCounter = 0;
        ComboTextObj.SetActive(false);
        // ComboText.text = $"Combo X{ComboCounter}";
        // StartTimerCombo();
        // DurationComboTime -= DurationDecreaseEachCombo;




    }
    public void StartTimerCombo()
    {
        if (TweenComboTime != null)
        {
            TweenComboTime.Kill();

        }

        Slide.maxValue = DurationComboTime;
        Slide.value = DurationComboTime;


        CurrentComboTime = DurationComboTime;
        TweenComboTime = DOTween.To(() => CurrentComboTime, x =>
        {
            CurrentComboTime = x;
            Slide.value = x;


        }, 0, DurationComboTime).SetEase(Ease.Linear).OnComplete(OverTime);



    }
    public void OverTime()
    {
        DurationComboTime = 20;
        ComboCounter = 0;
        ComboTextObj.SetActive(false);
        ComboText.text = "";
        Slide.value = 0;
        FillArea.SetActive(false);




    }
}
