using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        None,
        Game,
        Success,
        Fail
    }
    /*    public enum NumberClicked
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9
        } */

    public delegate void NumberDelegate(int number);
    public NumberDelegate numberDelegate;

    public TimeBar timeBar;
    public Slider amountSlider;
    public Slider difficultySlider;

    public RectTransform playgroundField;

    public Number ButtonPrefab;
    private Number _spawnedButton;

    public TextMeshProUGUI difficultyTimeText;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI timeLeft;
    public TextMeshProUGUI startButtonText;

    public GameObject successMessage;
    public GameObject failMessage;

    private GameObject[] numbers;

    private float _timer;
    private int _amount;
    private int _x, _y, _sizeX, _sizeY;

    private GameState _currentGameState = GameState.None;

    private void Start()
    {
        _sizeX = (int)playgroundField.rect.size.x;
        _sizeY = (int)playgroundField.rect.size.y;
    }
    public void StartGame()
    {
        if (_currentGameState == GameState.None)
        {
            _currentGameState = GameState.Game;
            startButtonText.text = "Stop";
            _timer = difficultySlider.value;
            _amount = (int)amountSlider.value; ;
            timeBar.SetMaxTime(_timer);
            numberDelegate = NumberClicked;
            while (_amount >= 0)
            {
                _x = Random.Range(50, _sizeX - 50);
                _y = Random.Range(50, _sizeY - 50);
                _spawnedButton = Instantiate(ButtonPrefab, new Vector2(_x, _y), Quaternion.identity, playgroundField.transform);
                _spawnedButton.transform.localPosition = new Vector2(_x, _y);
                _spawnedButton.Initialize(_amount);
                _amount--;
                difficultySlider.interactable = false;
                amountSlider.interactable = false;
            }
        }
        else
        {
            _currentGameState = GameState.None;
            startButtonText.text = "Start";
            _timer = difficultySlider.value;
            timeBar.SetTime(_timer);
            timeLeft.text = "Time left: " + _timer.ToString("0") + "s";
            difficultySlider.interactable = true;
            amountSlider.interactable = true;
        }
    }
    public void AmountChange()
    {
        _amount = (int)amountSlider.value;
        amountText.text = (_amount + 1).ToString() + " numbers";
    }
    public void DifficultyChange()
    {
        _timer = difficultySlider.value;
        difficultyTimeText.text = difficultySlider.value.ToString() + " seconds";
    }
    public void NumberClicked(int number)
    {
        Debug.Log(number);
    }
    private void Update()
    {
        if (_currentGameState == GameState.Game)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                timeBar.SetTime(_timer);
                timeLeft.text = "Time left: " + _timer.ToString("0") + "s";
                if (_timer <= 0)
                {
                    difficultySlider.interactable = true;
                    amountSlider.interactable = true;
                    _currentGameState = GameState.Fail;
                    failMessage.SetActive(true);
                }
            }
        }
        else
        {

        }
    }
}
