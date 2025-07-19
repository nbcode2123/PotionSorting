using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { private set; get; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public float DurationTime;
    public TextMeshProUGUI timerText;
    public Tween TweenTimer;
    public float CurrentTime;
    public Action OnTimerCounting;


    public void Start()
    {
        OnTimerCounting += TimeCounting;
        UpdateTimerUI(DurationTime);

        ObserverManager.AddListener("Start", StartTimer);
        ObserverManager.AddListener("Pause", PauseTimer);
        ObserverManager.AddListener("Continues", ContinuesTimer);
        ObserverManager.AddListener("Level Complete", StopTimer);
        ObserverManager.AddListener("New Level", SetUpNewLevel);
        ObserverManager.AddListener("BackToMenu", SetUpNewLevel);







    }
    private void OnDisable()
    {
        ObserverManager.RemoveListener("Start", StartTimer);
        ObserverManager.RemoveListener("Pause", PauseTimer);
        ObserverManager.RemoveListener("Continues", PauseTimer);
        ObserverManager.RemoveListener("Level Complete", StopTimer);



    }
    private void OnDestroy()
    {
        ObserverManager.RemoveListener("Start", StartTimer);
        ObserverManager.RemoveListener("Pause", PauseTimer);
        ObserverManager.RemoveListener("Continues", PauseTimer);
        ObserverManager.RemoveListener("Level Complete", StopTimer);




    }
    public void SetUpNewLevel()
    {
        Debug.Log("SetUpNewLevel");

        if (TweenTimer != null)
        {
            TweenTimer.Kill();
            TweenTimer = null;
        }
        CurrentTime = DurationTime;
        UpdateTimerUI(CurrentTime);
        OnTimerCounting -= TimeCounting;
        OnTimerCounting += TimeCounting;
    }
    public void StartTimer()
    {
        OnTimerCounting?.Invoke();
        OnTimerCounting -= TimeCounting;

    }

    public void TimeCounting()
    {
        CurrentTime = DurationTime;
        TweenTimer = DOTween.To(() => CurrentTime, x =>
           {
               CurrentTime = x;
               UpdateTimerUI(CurrentTime);
           }, 0f, DurationTime)
           .SetEase(Ease.Linear)
           .OnComplete(OnTimerEnd);
    }

    public void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnTimerEnd()
    {
        Debug.Log("Hết giờ!");
    }
    public void PauseTimer()
    {
        TweenTimer.Pause();
    }

    public void ContinuesTimer()
    {
        TweenTimer.Play();
    }

    public void StopTimer()
    {
        TweenTimer.Kill();
    }

}
