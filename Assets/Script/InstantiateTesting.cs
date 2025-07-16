using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject CreateObj(GameObject obj)
    {
        var _tempObj = Instantiate(obj);
        return _tempObj;
    }
}
