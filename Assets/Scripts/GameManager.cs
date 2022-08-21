using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        None,
        Game,
        Success,
        Fail
    }

    public TimeBar TimeBar;
    public Slider AmountSlider;
    public Slider DifficultySlider;

    public RectTransform PlaygroundField;

    public Number ButtonPrefab;

    public TextMeshProUGUI DifficultyTimeText;
    public TextMeshProUGUI AmountText;
    public TextMeshProUGUI TimeLeft;
    public TextMeshProUGUI StartButtonText;

    public GameObject SuccessMessage;
    public GameObject FailMessage;

    private float _timer;

    private int _amount;
    private int _x, _y, _sizeX, _sizeY;
    private int _lastPressedButton;
    private int _expectedButton = 0;

    private GameState _currentGameState = GameState.None;

    private void Start()
    {
        var rect = PlaygroundField.rect;
        _sizeX = (int)rect.size.x;
        _sizeY = (int)rect.size.y;
    }

    public void StartGame()
    {
        if (_currentGameState == GameState.None)
        {
            _currentGameState = GameState.Game;
            DifficultySlider.interactable = false;
            AmountSlider.interactable = false;

            StartButtonText.text = "Stop";

            _timer = DifficultySlider.value;
            _amount = (int)AmountSlider.value;

            TimeBar.SetMaxTime(_timer);

            var counter = 0;

            while (counter <= _amount)
            {
                _x = Random.Range(50, _sizeX - 50);
                _y = Random.Range(50, _sizeY - 50);

                var spawnedButton = Instantiate(ButtonPrefab, PlaygroundField.transform);
                spawnedButton.transform.localPosition = new Vector2(_x, _y);
                spawnedButton.Initialize(counter, NumberClicked);

                counter++;
            }
        }
        else
        {
            _currentGameState = GameState.None;
            _timer = DifficultySlider.value;
            StartButtonText.text = "Start";

            TimeBar.SetTime(_timer);
            TimeBar.SetMaxTime(_timer);
            TimeLeft.text = "Time left: " + _timer.ToString("0") + "s";

            DifficultySlider.interactable = true;
            AmountSlider.interactable = true;

            FailMessage.SetActive(false);
            SuccessMessage.SetActive(false);
            _expectedButton = 0;
        }
    }

    public void AmountChange()
    {
        _amount = (int)AmountSlider.value;
        AmountText.text = _amount + 1 + " numbers";
    }
    public void DifficultyChange()
    {
        _timer = DifficultySlider.value;
        DifficultyTimeText.text = DifficultySlider.value.ToString() + " seconds";
        TimeLeft.text = "Time left: " + DifficultySlider.value;
    }

    private void NumberClicked(int number)
    {
        if (number != _expectedButton)
        {
            Fail();
        }
        ++_expectedButton;
        if (_expectedButton == _amount + 1)
        {
            Success();
        }
    }

    private void None()
    {
        DifficultySlider.interactable = true;
        AmountSlider.interactable = true;

        Destroy(GameObject.FindWithTag("SpawnedButtons"));

        SuccessMessage.SetActive(false);
        FailMessage.SetActive(false);
    }
    private void Fail()
    {
        _currentGameState = GameState.Fail;
        FailMessage.SetActive(true);
        _expectedButton = 0;
    }
    private void Success()
    {
        _currentGameState = GameState.Success;
        SuccessMessage.SetActive(true);
        _expectedButton = 0;
    }

    private void Update()
    {
        if (_currentGameState == GameState.Game)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                TimeBar.SetTime(_timer);
                TimeLeft.text = "Time left: " + _timer.ToString("0") + "s";
                if (_timer <= 0)
                {
                    Fail();
                }
            }
        }
        else if (_currentGameState == GameState.None)
        {
            None();
        }
    }
}
