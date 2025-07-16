using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShelf : MonoBehaviour, IPotionShelf
{
    public List<ISingleShelf> ListSingleShelf { get; set; }

    private void Awake()
    {
        ListSingleShelf = new List<ISingleShelf>();

    }
    private void Start()
    {
    }
    public void AddSingleShelf(ISingleShelf singleShelf)
    {
        ListSingleShelf.Add(singleShelf);

    }

    public List<ISingleShelf> GetListSingleShelf()
    {
        return ListSingleShelf;

    }


}
