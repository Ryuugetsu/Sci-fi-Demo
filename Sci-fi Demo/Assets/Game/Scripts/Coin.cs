using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField]
    private AudioClip _coinAudio;
    //check for collision
    //check if collider is the player
    //Check if 'E' key press
    //give player the coin
    //player sound
    //destroy the coin

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
           
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    player.hasCoin =true;
                    UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                    if(uiManager != null)
                    {
                        uiManager.ShowCoin();
                    }
                    Destroy(this.gameObject);
                    AudioSource.PlayClipAtPoint(_coinAudio, Camera.main.transform.position, 0.5f);                    
                }
            }
        }
    }


}
