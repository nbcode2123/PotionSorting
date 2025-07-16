using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PotionStorage", menuName = "ScriptableObject/Potion")]
public class PotionStorage : ScriptableObject
{
    [SerializeField] public List<GameObject> ListPotion = new List<GameObject>();
    [SerializeField] public List<GameObject> ListBigPotion = new List<GameObject>();
    [SerializeField] public List<GameObject> ListSmallPotion = new List<GameObject>();

    [SerializeField] public List<GameObject> ListGreenPotion = new List<GameObject>();

    [SerializeField] public List<GameObject> ListYellowPotion = new List<GameObject>();

    [SerializeField] public List<GameObject> ListBluePotion = new List<GameObject>();

    [SerializeField] public List<GameObject> ListRedPotion = new List<GameObject>();


    [SerializeField] public List<GameObject> ListBlackPotion = new List<GameObject>();

    [SerializeField] public List<GameObject> ListPurplePotion = new List<GameObject>();

    [SerializeField] public List<GameObject> ListMintGreenPotion = new List<GameObject>();
    [SerializeField] public List<GameObject> ListNavyBluePotion = new List<GameObject>();
    [SerializeField] public List<GameObject> ListVioletPotion = new List<GameObject>();
    [SerializeField] public List<GameObject> ListRainbowPotion = new List<GameObject>();
    [SerializeField] public Dictionary<string, List<GameObject>> DicColor = new Dictionary<string, List<GameObject>>();
    [SerializeField] public Dictionary<string, List<GameObject>> DicShape = new Dictionary<string, List<GameObject>>();

    public void CreateDicColor()
    {
        DicColor = new Dictionary<string, List<GameObject>>()
    {
        { "Green", ListGreenPotion },
        { "Yellow", ListYellowPotion },
        { "Blue", ListBluePotion },
        { "Red", ListRedPotion },
        { "Black", ListBlackPotion },
        { "Purple", ListPurplePotion },
        { "MintGreen", ListMintGreenPotion },
        { "NavyBlue", ListNavyBluePotion },
        { "Violet", ListVioletPotion },
        { "Rainbow", ListRainbowPotion }
    };
    }
    public void CreateDicShape()
    {
        DicColor = new Dictionary<string, List<GameObject>>()
    {
        { "Big", ListBigPotion },
        { "Small", ListSmallPotion },

    };
    }
    public GameObject GetRandomPotion()
    {
        int _temp = Random.Range(0, ListPotion.Count);
        return ListPotion[_temp];

    }








}
