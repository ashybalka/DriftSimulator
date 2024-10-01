using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExchangeController : MonoBehaviour
{
    [SerializeField] GameObject ExchangePanel;
    [SerializeField] GameObject InsufficientPopup;
    private SaveLoadController saveload;
    private CurrenciesController currencyController;

    private void Start()
    {
        saveload = GameObject.Find("SaveLoader").GetComponent<SaveLoadController>();
        currencyController = GameObject.Find("Currencies").GetComponent<CurrenciesController>();
    }


    public void OpenExchangePanel()
    { 
        ExchangePanel.SetActive(!ExchangePanel.activeInHierarchy);
    }
    public void MoneyToGold()
    {
        if (saveload._currencies.Money >= 1000)
        {
            saveload._currencies.Money -= 1000;
            saveload._currencies.Gold += 10;
            saveload.SaveToJson();
            currencyController.UpdateCurrencies();
        }
        else
        {
            StartCoroutine(Insufficient("$"));
        }
    }

    public void GoldToMoney()
    {
        if (saveload._currencies.Gold >= 10)
        {
            saveload._currencies.Gold -= 10;
            saveload._currencies.Money += 1000;
            saveload.SaveToJson();
            currencyController.UpdateCurrencies();
        }
        else
        {
            StartCoroutine(Insufficient("gold"));
        }
    }

    IEnumerator Insufficient(string value)
    {
        InsufficientPopup.GetComponentInChildren<TMP_Text>().text = "Insufficient " + value + " to exchange";
        InsufficientPopup.SetActive(true);
        yield return new WaitForSeconds(2f);
        InsufficientPopup.SetActive(false);
    }
}
