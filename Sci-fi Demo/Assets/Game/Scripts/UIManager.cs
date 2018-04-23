﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private GameObject _coin;

    public void ShowCoin()
    {
        _coin.SetActive(true);
    }

    public void HideCoin()
    {
        _coin.SetActive(false);
    }

    public void UpdateAmmo(int count)
    {
        _ammoText.text = "Ammo: " + count;        
    }

}
