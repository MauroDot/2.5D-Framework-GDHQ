using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        { 
            other.gameObject.TryGetComponent(out Player player);
            player.UpdateCoinsCollected();
            Destroy(this.gameObject);
        }
    }
}
    