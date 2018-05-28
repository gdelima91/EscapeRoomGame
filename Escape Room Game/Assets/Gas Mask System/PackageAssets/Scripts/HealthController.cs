using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float playerHealth = 100.0f;
    public float healthFall = 2;
    public float maxHealth = 100.0f;

    [SerializeField] private Text healthUI;

    public static HealthController instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateHealth()
    {
        healthUI.text = playerHealth.ToString("0");
    }

    public void Death()
    {
        //DO SOMETHING HERE!
        Debug.Log("I DIED"); //Delete this and replace with your own code for dying!
    }
}
