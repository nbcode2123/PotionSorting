using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteChecker : MonoBehaviour
{
    public static CompleteChecker Instance { get; private set; }
    public List<GameObject> ListPotionInLevel;
    public List<GameObject> ListPotionMatch;
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
        ListPotionInLevel = new List<GameObject>();
        ListPotionMatch = new List<GameObject>();
    }
    private void Start()
    {
        ObserverManager.AddListener<GameObject>("BestMatch", AddPotionToMatchList);

    }
    private void OnDestroy()
    {
        ObserverManager.RemoveListener<GameObject>("BestMatch", AddPotionToMatchList);


    }
    private void OnDisable()
    {
        ObserverManager.RemoveListener<GameObject>("BestMatch", AddPotionToMatchList);


    }
    public void AddPotionInLevel(GameObject potion)
    {
        ListPotionInLevel.Add(potion);
    }
    public void AddPotionToMatchList(GameObject potion)
    {
        ListPotionMatch.Add(potion);
        CheckGameComplete();
    }
    public void CheckGameComplete()
    {
        if (ListPotionInLevel.Count != ListPotionMatch.Count)
        {
            return;
        }
        else
        {
            Debug.Log("Level Complete");
            ObserverManager.Notify("Level Complete");

        }
    }

}
