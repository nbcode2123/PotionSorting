using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinCreateEffect : MonoBehaviour
{
    public static CoinCreateEffect Instance { get; private set; }
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
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateCoin(int number, Vector3 position)
    {
        Sequence _sequence = DOTween.Sequence();
        float _radius;
        for (int i = 0; i < number; i++)
        {
            _radius = Random.Range(0, 2);
            Vector3 _tempPosition = new Vector3(Random.Range(position.x - _radius, position.x + _radius), Random.Range(position.y - _radius, position.y + _radius), 0);
            _sequence.AppendCallback(() =>
            {
                Instantiate(GameObjectStorage.Instance.Coin, _tempPosition, Quaternion.identity);

            });
            _sequence.AppendInterval(0.1f);
        }

    }
}
