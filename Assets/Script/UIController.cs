using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Slider playerEXPSlider;
    public GameObject gameover;
    public GameObject pausepanel;
    //[SerializeField] private TMP_Text timerText;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this); 
        }
        else 
        {
            Instance = this;
        }
    }

    public void UpdateHealthSlider()
    {
        playerHealthSlider.maxValue = Controller.Instance.playerMaxHealth;
        playerHealthSlider.value = Controller.Instance.playerHealth;
    }
    public void UpdateEXPSlider()
    {
        playerEXPSlider.maxValue = Controller.Instance.playerMaxEXP;
        playerEXPSlider.value = Controller.Instance.playerEXP;
    }
    void Update()
    {
        
    }
}
