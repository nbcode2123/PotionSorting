using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;
    public class Pool
    {
        public string Name;
        public GameObject Prefab;
        public List<GameObject> ListGameObject;
        public Queue<GameObject> QueueGameObject;
    }
    public List<string> ListPoolName = new List<string>();
    public List<Pool> ListPool = new List<Pool>();
    void Awake()
    {
        if (Instance != null && Instance != this)
        {

            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        ListPool = new List<Pool>();
    }
    public void CreatePool(GameObject poolObject)
    {
        // CheckForPoolExistence(poolObject.name);
        if (!ListPoolName.Contains(poolObject.name) && CheckForPoolExistence(poolObject.name))
        {
            var _tempPool = new Pool();
            _tempPool.Name = poolObject.name;
            _tempPool.Prefab = poolObject;
            _tempPool.ListGameObject = new List<GameObject>();

            ListPool.Add(_tempPool);
            ListPoolName.Add(poolObject.name);
        }
        else
        {
            // ClearPoolObject(poolObject.name);
            return;
        }
    }
    public void SpawnPool(string namePool, int poolSize) // tao ra pool object tu pool
    {
        var _pool = ListPool.Find(p => p.Name == namePool);
        if (_pool.ListGameObject.Count == 0 || _pool == null)
        {
            GameObject _tempParentObject = new GameObject(namePool + "Pool");
            for (int i = 0; i < poolSize; i++)
            {
                var _tempObject = Instantiate(_pool.Prefab);
                _tempObject.name = _pool.Prefab.name + i;
                _tempObject.SetActive(false);
                _tempObject.transform.parent = _tempParentObject.transform;
                _pool.ListGameObject.Add(_tempObject);
            }
        }
        _pool.QueueGameObject = new Queue<GameObject>(_pool.ListGameObject);
    }



    public bool CheckForPoolExistence(string namePool)
    {
        GameObject _tempParentObject = GameObject.Find(namePool + "Pool");
        if (_tempParentObject == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public GameObject GetObjectFromPool(string namePool)

    {
        GameObject _tempObject = null;
        var _tempPool = ListPool.Find(p => p.Name == namePool);
        _tempObject = _tempPool.QueueGameObject.Dequeue(); // Lấy object đầu tiên ra
        _tempPool.QueueGameObject.Enqueue(_tempObject);
        return _tempObject;
    }


    public void DeletePoolObj(string namePool)
    {
        GameObject _tempParentObject = GameObject.Find(namePool + "Pool");
        if (_tempParentObject != null)
        {
            Destroy(_tempParentObject);
        }
        var _tempPool = ListPool.Find(p => p.Name == namePool);
        ListPoolName.Remove(_tempPool.Name);
        ListPool.Remove(_tempPool);
    }
    // public void SpawnThePool(string namePool, int poolSize, GameObject gameobjectParent)// tao ra pool object tu pool nma duoc gan cho 1 object lam parent 
    // {
    //     var _pool = ListPool.Find(p => p.Name == namePool);
    //     _pool.ListGameObject.Clear();
    //     for (int i = 0; i < poolSize; i++)
    //     {
    //         var _tempObject = Instantiate(_pool.Prefab);
    //         _tempObject.name = _pool.Prefab.name + i;
    //         _tempObject.SetActive(false);
    //         _tempObject.transform.SetParent(gameobjectParent.transform);
    //         _pool.ListGameObject.Add(_tempObject);

    //     }
    //     _pool.QueueGameObject = new Queue<GameObject>(_pool.ListGameObject);
    // }
    // public void ClearPoolObject(string namePool)
    // {
    //     var _tempPool = ListPool.Find(p => p.Name == namePool);
    //     _tempPool.ListGameObject.Clear();
    //     // CheckForPoolExistence(namePool);

    // }




}
