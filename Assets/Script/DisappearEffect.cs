using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearEffect : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
