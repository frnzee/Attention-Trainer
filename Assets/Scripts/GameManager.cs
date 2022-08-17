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

    public TimeBar timeBar;
    public Slider amountSlider;
    public Slider difficultySlider;

    public RectTransform playgroundField;

    public Number ButtonPrefab;

    public TextMeshProUGUI difficultyTimeText;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI timeLeft;
    public TextMeshProUGUI startButtonText;

    public GameObject successMessage;
    public GameObject failMessage;

    private float _timer;
    private int _amount;
    private int _x, _y, _sizeX, _sizeY;
    private int _lastPressedButton;

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
            _amount = (int)amountSlider.value;
            var counter = 0;
            timeBar.SetMaxTime(_timer);
            while (counter <= _amount)
            {
                _x = Random.Range(50, _sizeX - 50);
                _y = Random.Range(50, _sizeY - 50);
                var spawnedButton = Instantiate(ButtonPrefab, new Vector2(_x, _y), Quaternion.identity, playgroundField.transform);
                spawnedButton.transform.localPosition = new Vector2(_x, _y);
                spawnedButton.Initialize(counter, NumberClicked);
                counter++;
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
            failMessage.SetActive(false);
            successMessage.SetActive(false);
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
        if (number == 0)
        {
            _lastPressedButton = number;
        }
        else if (number == _lastPressedButton + 1)
        {
            _lastPressedButton = number;
            if (number == _amount)
            {
                _currentGameState = GameState.Success;
            }
        }
        else
        {
            _currentGameState = GameState.Fail;
        }
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
        else if (_currentGameState == GameState.Fail)
        {
            difficultySlider.interactable = true;
            amountSlider.interactable = true;
            failMessage.SetActive(true);
            Destroy(GameObject.FindWithTag("SpawnedButtons"));
        }
        else if (_currentGameState == GameState.Success)
        {
            difficultySlider.interactable = true;
            amountSlider.interactable = true;
            successMessage.SetActive(true);
            Destroy(GameObject.FindWithTag("SpawnedButtons"));
        }
        else if (_currentGameState == GameState.None)
        {
            difficultySlider.interactable = true;
            amountSlider.interactable = true;
            Destroy(GameObject.FindWithTag("SpawnedButtons"));
        }
        else
        {
            _currentGameState = GameState.None;
        }
    }
}
