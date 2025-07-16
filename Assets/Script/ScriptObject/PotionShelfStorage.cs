using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PotionShelf", menuName = "ScriptableObject/PotionShelfStorage")]
public class PotionShelfStorage : ScriptableObject
{
    [SerializeField] public List<GameObject> ListPotionShelf = new List<GameObject>();


}
