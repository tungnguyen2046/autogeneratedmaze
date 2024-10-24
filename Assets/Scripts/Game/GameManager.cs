using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;} 

    private static int level = 1; public int Level{ get { return level; }}
    public float timer {get; private set;} 
    bool isWin;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        Observer.AddObserver(GameEvents.onWinning, IncreaseLevel);

        timer = 10 + level * 10 + 1;
    }

    private void OnDisable() 
    {
        Observer.RemoveObserver(GameEvents.onWinning, IncreaseLevel);
    }

    private void Update()
    {
        if(!isWin)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Max(0, timer);

            if(timer <= 0) Observer.Notify(GameEvents.onLosing);
        }
    }

    private void IncreaseLevel()
    {
        isWin = true;
        level++;
    }
}
