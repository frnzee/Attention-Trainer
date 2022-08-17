using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Number : MonoBehaviour
{

    public GameObject _spawnedButton;
    public TextMeshProUGUI _currentNumberText;
//    public GameManager.NumberDelegate numberDelegate;
//    public GameManager.NumberDelegate pressedNumber;
    public void Initialize(int CurrentNumber)
    {
//        pressedNumber = numberDelegate;
//        numberDelegate(CurrentNumber);
        _spawnedButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = CurrentNumber.ToString();
    }
    public void OnClick()
    {
//        pressedNumber(int.Parse(GetComponentInChildren<TMPro.TextMeshProUGUI>().text));
        Destroy(_spawnedButton);
    }
}
