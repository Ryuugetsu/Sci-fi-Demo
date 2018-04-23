using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour {

    [SerializeField]
    private AudioClip _clip;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)                
            {
                if (Input.GetKeyDown(KeyCode.E) && player.hasCoin == true)
                {
                    player.hasCoin = false;
                    UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                    uiManager.HideCoin();
                    AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 0.5f);
                    player._enableWeapon();
                }
                else
                {
                    Debug.Log("Get out of here! ");
                }                    
            }
        }
    }

}
