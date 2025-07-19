using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public int Level;
    public List<GameObject> ListPotionInLevel;
    public List<GameObject> ListPotionMatch;
    public List<GameObject> ListPotionInShelf;

    public List<IShelfSlot> ListShelfSlots;
    public List<ISingleShelf> ListSingleShelf;
    [SerializeField] public GameObject CurrentPotionShelf;
    ///
    // [SerializeField] public List<GameObject> ListShelfSlot;
    private ISortingAlgorithm sortingAlgorithm;


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
        ListShelfSlots = new List<IShelfSlot>();
        ListSingleShelf = new List<ISingleShelf>();
        Level = 0;

    }
    public void Start()
    {
        ObserverManager.AddListener("New Level", CreateNewLevel);
        // CreateNewLevel();



    }
    public void OnDestroy()
    {
        ObserverManager.RemoveListener("New Level", CreateNewLevel);

    }
    public void OnDisable()
    {
        ObserverManager.RemoveListener("New Level", CreateNewLevel);

    }
    public void CreateNewLevel()
    {
        ListPotionMatch.Clear();
        ListPotionInLevel.Clear();
        ListPotionInShelf.Clear();
        ListShelfSlots.Clear();
        ListSingleShelf.Clear();
        if (CurrentPotionShelf != null)
        {
            Destroy(CurrentPotionShelf);
        }
        List<GameObject> _potionShelf = GameObjectStorage.Instance.PotionShelfStorage.ListPotionShelf;
        // CurrentPotionShelf = Instantiate(GameObjectStorage.Instance.PotionShelfStorage.ListPotionShelf[Random.Range(0, _potionShelf.Count)]);
        CurrentPotionShelf = Instantiate(GameObjectStorage.Instance.PotionShelfStorage.ListPotionShelf[0]);

        GameObject UIProperties = CurrentPotionShelf.transform.GetChild(0).gameObject;
        UIPropertiesPotionShelf _uIProperties = UIProperties.GetComponent<UIPropertiesPotionShelf>();
        CameraController.Instance.SetPotionShelfToCamera(_uIProperties.MiddlePivot, _uIProperties.CameraSize);
        IPotionShelf potionShelf = CurrentPotionShelf.GetComponent<IPotionShelf>();
        // var _multiple = Random.Range(1, 4);
        sortingAlgorithm = new EasySortingAlgorithm(potionShelf, 1);
        sortingAlgorithm.SortingAlgorithm();
        // TurnOffObj();


    }
    public void TurnOffObj()
    {
        CurrentPotionShelf.SetActive(false);
        foreach (var item in ListPotionInLevel)
        {
            item.SetActive(false);
        }

    }
    public void TurnOnObj()
    {
        CurrentPotionShelf.SetActive(true);
        foreach (var item in ListPotionInLevel)
        {
            item.SetActive(true);
        }

    }



}
