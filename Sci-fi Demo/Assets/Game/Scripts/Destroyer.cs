using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    [SerializeField]
    private GameObject _destructable;

    public void DestroyCrate()
    {
        Instantiate(_destructable, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
