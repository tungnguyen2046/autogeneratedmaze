using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private void Awake() 
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.name == "TriggerEnd")
        {
            Observer.Notify(GameEvents.onWinning);
        }
        
    }
}
