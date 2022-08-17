using System;
using UnityEngine;
using TMPro;

public class Number : MonoBehaviour
{
    public TextMeshProUGUI CurrentNumberText;
    private Action<int> pressedNumber;
    private int _currentNumber;
    public void Initialize(int CurrentNumber, Action<int> numberDelegate)
    {
        _currentNumber = CurrentNumber;
        pressedNumber = numberDelegate;
        CurrentNumberText.text = CurrentNumber.ToString();
    }
    public void OnClick()
    {
        pressedNumber(_currentNumber);
        Destroy(gameObject);
    }
}
