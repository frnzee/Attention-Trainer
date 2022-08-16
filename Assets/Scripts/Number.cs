using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Number : MonoBehaviour
{

    public GameObject _spawnedButton;
    private int currentNumberCallback;
    public void Initialize(int CurrentNumber)
    {
        currentNumberCallback = CurrentNumber;
        Debug.Log(CurrentNumber);
        _spawnedButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = CurrentNumber.ToString();
    }
    public void NumberClicked(int number)
    {

    }
    public void OnClick()
    {
        Debug.Log(currentNumberCallback);
        Destroy(_spawnedButton);
    }
}
