using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinCalculator : MonoBehaviour
{
    public static CoinCalculator Instance { private set; get; }
    public TextMeshProUGUI CoinCounterText;
    public int CoinCounter;
    public int CoinDefault;
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
        CoinCounter = PlayerPrefs.GetInt("Coin", CoinCounter);

        CoinCounterText.text = CoinCounter.ToString();



    }
    public void DecreaseCoin()
    {

    }
    public void IncreaseCoin()
    {
        var _currentCombo = ComboController.Instance.ComboCounter;
        CoinCounter = CoinCounter + 1 + _currentCombo;
        CoinCounterText.text = CoinCounter.ToString();
        PlayerPrefs.SetInt("Coin", CoinCounter);


        // var _tempCoinCounter = CoinCounter + 3 + _currentCombo;

        // DOTween.To(() => CoinCounter, x =>
        // {
        //     CoinCounter = x;
        //     CoinCounterText.text = CoinCounter.ToString();
        // }, _tempCoinCounter, 0.5f).SetEase(Ease.Linear);
    }
    private void OnDestroy()
    {
        ObserverManager.RemoveListener("BestMatch", IncreaseCoin);
    }
    private void OnDisable()
    {
        ObserverManager.RemoveListener("BestMatch", IncreaseCoin);
    }


}
