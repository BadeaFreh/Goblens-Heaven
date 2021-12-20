using UnityEngine;
using UnityEngine.UI; // to be able to import Slider


public class UIController : MonoBehaviour
{

    public Image blackScreen;
    public float fadeSpeed = 1.5f; // 1.5 sec is the perfect speed for fade in and fade out effects

    public static UIController instance; // to access it in PlayerHealthController
    public Slider healthSlider;
    public Text healthText;

    public Slider ammoSlider;
    public Text ammoText;
    public Image damageEffect;
    public float damageAlpha = 0.25f;
    public float damagedFadeSpeed = 2f;
    public GameObject pauseScreen;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (damageEffect.color.a != 0) // alpha value
        {
            // using Mathf.MoveTowards to get fading effect for red screen

            damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b,
                            Mathf.MoveTowards(damageEffect.color.a, 0f, damagedFadeSpeed * Time.deltaTime));
        }

        if (!GameManager.instance.levelEnding) // if game not ended (scene started)=> fade in black screen
        {
            // the 4th param (.MoveTwoards) makes fading effect. range: [current opacity -> 0f opacity]
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b
                , Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        }
        else // won -> go towards 1f opacity on black screen
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b
                , Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
    }

    public void ShowDamage()
    {
        damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, 0.25f);
    }
}
