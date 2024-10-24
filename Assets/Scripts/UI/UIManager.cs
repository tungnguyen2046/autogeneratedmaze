using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelTxt, timerTxt;
    [SerializeField] Button buttonNext, buttonAgain;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;

    public static UIManager instance {get; private set;}

    void Awake()
    {
        if(instance != null & instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
        Observer.AddObserver(GameEvents.onWinning, EnableWinScreen);
        Observer.AddObserver(GameEvents.onLosing, EnableLoseScreen);
    }

    private void OnDisable() 
    {
        Observer.RemoveObserver(GameEvents.onWinning, EnableWinScreen);
        Observer.RemoveObserver(GameEvents.onLosing, EnableLoseScreen);
    }

    private void Start()
    {
        levelTxt.text = "Level: " + GameManager.instance.Level;
        buttonNext.onClick.AddListener(ReloadScene);
        buttonAgain.onClick.AddListener(ReloadScene);
    }

    private void Update()
    {
        float minutes = Mathf.FloorToInt(GameManager.instance.timer / 60);
        float seconds = Mathf.FloorToInt(GameManager.instance.timer % 60);

        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void EnableWinScreen()
    {
        winScreen.SetActive(true);
    }

    private void EnableLoseScreen()
    {
        loseScreen.SetActive(true);
    }
}
