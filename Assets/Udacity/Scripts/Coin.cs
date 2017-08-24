using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour 
{
    //Create a reference to the CoinPoofPrefab
    public GameObject coinPoof;

    private float _xRotation = -90;

    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("PlayerUI");
    }

    public void OnCoinClicked() {
        // Instantiate the CoinPoof Prefab where this coin is located
        // Make sure the poof animates vertically
        // Destroy this coin. Check the Unity documentation on how to use Destroy
        Instantiate(coinPoof, transform.position, Quaternion.Euler(_xRotation,0,0));

        _player.GetComponent<GameManager>().getOneCoin();

        Destroy(gameObject);
    }

}
