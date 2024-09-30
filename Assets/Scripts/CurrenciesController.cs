using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class CurrenciesController : MonoBehaviour
{
    [SerializeField] TMP_Text MoneyText, GoldText;
    private SaveLoadController saveload;

    void Start()
    {
        saveload = GameObject.Find("SaveLoader").GetComponent<SaveLoadController>();
        UpdateCurrencies();
    }

    public void UpdateCurrencies()
    {
        MoneyText.text = saveload._currencies.Money.ToString()  + " $";
        GoldText.text = saveload._currencies.Gold.ToString() + " GOLD";
    }
}
