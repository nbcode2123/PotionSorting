using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{

    private Tween TweenEffect;

    // Start is called before the first frame update
    void Start()
    {
        FlyToCoinCounter();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FlyToCoinCounter()
    {
        TweenEffect = gameObject.transform
        .DOMove(GameObjectStorage.Instance.CoinLootTrigger.transform.position, 0.5f)
        .SetEase(Ease.InBack)
        .OnComplete(() => Destroy(gameObject));

    }
    private void OnDestroy()
    {
        // TweenEffect.Kill();
    }
}
