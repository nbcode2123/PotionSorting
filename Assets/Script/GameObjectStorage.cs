using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameObjectStorage : MonoBehaviour
{
    public static GameObjectStorage Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else { Instance = this; }
    }
    public PotionStorage PotionStorage;
    public PotionShelfStorage PotionShelfStorage;
    public GameObject DisappearEffect;
    public GameObject CoinLootTrigger;
    public GameObject Coin;
    public GameObject MiddleSceneCamera;
}
