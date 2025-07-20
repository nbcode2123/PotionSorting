using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTriggerEffect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            ObserverManager.Notify("CoinLoot", "CoinLoot");
            Destroy(other.gameObject);
            CoinCalculator.Instance.IncreaseCoin();
        }


    }
}
