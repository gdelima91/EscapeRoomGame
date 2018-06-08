using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System.Linq;

public class GasMaskController : MonoBehaviour
{
    public enum GASMASKSTATES
    {
        GasMaskOn,
        GasMaskOffInSmoke,
        GasMaskOffOutOfSmoke
    }

    #region Mask Filter Variables
    //Serialize some of these fields or remove [HideInInspector] if you want to debug
    [Header("Gas Mask Features")] 
    [SerializeField] private float maxEquipMaskTimer = 1f; //The maximum time you want to wait before putting on or taking off the mask. Same as "maskTimer".

    [HideInInspector] private float maskBeforeTimer = 0.99f; //Just a millisecond before the max timer so we can stop this looping, it will autofill from the start function if edited
    [HideInInspector] public bool hasGasMask = false;
    [HideInInspector] public bool gasMaskOn = false;
    private float equipMaskTimer = 1f;
    private bool canUse = false;
    [HideInInspector] public bool puttingOn = false, puttingOff = false;
    [HideInInspector] private GASMASKSTATES currentState;
    public float gasDamage = 4.0f;
    #endregion 

    #region Movement Speeds
    [Header("Movement Speeds")]
    [SerializeField] private float walkNorm = 5;
    [SerializeField] private float walkGas = 2;
    [SerializeField] private float runNorm = 10;
    [SerializeField] private float runGas = 4;
    #endregion

    #region Filter Variables   
    //[Header("Filter Options")]    
   //[SerializeField] private float maxFilterTimer = 100f; //Keep this at 100.
    //[SerializeField] private float filterFallRate = 2f; //Increase this to make the filter deplete faster
    //[SerializeField] private int warningPercentage; //The percentage the system will give a warning
    //[SerializeField] private float defaultVignette = 0.28f; //Default value of the vignette image effect

    //[HideInInspector] private float filterTimer = 100f; //Set the same as your max value, do not change!
    //private bool hasFilter = true; //Whether you have a filter or not
    //private bool filterChanged = false; //Has the filter changed
    //public int maskFilters = 0; //How many filters does your player currently have? Increase this value at the start to give them more!
    #endregion

    #region References for Inspector
    [Header("References")]
    //[SerializeField] private Image maskUI;
    //[SerializeField] private Image filterMainUI;
    //[SerializeField] private Text filterUI;
    //[SerializeField] private Text FilterLevelUI;

    private GameObject mainCamera;
    private GameObject player;
    private VignetteAndChromaticAberration vignetteEffect;
    private BlurOptimized blurEffect;
    private ScreenOverlay screenOverlayEffect;

    [SerializeField] private bool canBreath;
    [HideInInspector] private bool playOnce = false;
    [HideInInspector] private bool playOnceChoke = false;

    public static GasMaskController instance;
    #endregion

    void Awake()
    {
        #region Singleton Code
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion
    }

    void Start()
    {
        #region Filling Variables / Filter/Mask Timer start values
       // filterTimer = maxFilterTimer;
       // equipMaskTimer = maxEquipMaskTimer;
       // maskBeforeTimer = maxEquipMaskTimer - 0.01f;
        mainCamera = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
        vignetteEffect = mainCamera.GetComponent<VignetteAndChromaticAberration>();
        screenOverlayEffect = mainCamera.GetComponent<ScreenOverlay>();
        blurEffect = mainCamera.GetComponent<BlurOptimized>();
        #endregion
    }

    void Update()
    {
        #region Equipped Gas 
        if (Input.GetButtonDown("GasMask"))
        {
            //gasdamage = false
            
            hasGasMask = true;
            Debug.Log("Pressed Gas, " + (hasGasMask ? "has mask, " : "no mask, ") + (gasMaskOn ? "mask on, " : "mask off, " + (puttingOn ? "putting on" : "not putting on")));
            if (hasGasMask && !gasMaskOn && !puttingOn)
            {
                Debug.Log("Put On");
                gasMaskOn = true;
                screenOverlayEffect.enabled = true;
                /*equipMaskTimer -= Time.deltaTime;
                if (equipMaskTimer <= 0)
                {
                    canUse = true;
                }

                if (canUse)
                {
                    equipMaskTimer = 1f;
                    StartCoroutine(MaskOn());
                    StartCoroutine(Wait());
                }*/
            }

            else if(hasGasMask && gasMaskOn && !puttingOn)
            {
                Debug.Log("Take Off");
                gasMaskOn = false;
                screenOverlayEffect.enabled = false;
                /*equipMaskTimer -= Time.deltaTime;
                if (equipMaskTimer <= 0)
                {
                    canUse = false;
                }

                if (!canUse)
                {
                    equipMaskTimer = maxEquipMaskTimer;
                    MaskOff();
                    StartCoroutine(Wait());
                }*/
            }
            else
            {
                if (equipMaskTimer <= maskBeforeTimer)
                {
                    equipMaskTimer = maxEquipMaskTimer;
                }
            }
        }

        
        #endregion

        /*#region Filter Control
        if (hasGasMask && gasMaskOn)
        {
            filterTimer -= Time.deltaTime * filterFallRate;
            UpdateFilterUI("FilterValue");
            if (vignetteEffect.intensity <= 0.5)
            {
                vignetteEffect.intensity += Time.deltaTime/(200*10);
            }

            if (filterTimer <= 1)
            {
                if (maskFilters >= 1)
                {
                    ReplaceFilter();
                }
                else
                {
                    filterTimer = 0;
                    UpdateFilterUI("FilterValue");
                    hasFilter = false;
                    MaskOff();
                }
            }

            if (filterTimer <= ((maxFilterTimer / 100) * warningPercentage) && !filterChanged)
            {
                UpdateFilterUI("RedFilter");
                AudioManager.instance.Play("Warning_Alarm");
                filterChanged = true;
            }
        }

        if (hasGasMask)
        {
            if (Input.GetKeyDown("z") && maskFilters >= 1)
            {
                ReplaceFilter();
            }
        }
        #endregion */

        #region Taking Damage / Breathing Section

        if (!canBreath)
        {
            if (gasMaskOn) currentState = GASMASKSTATES.GasMaskOn;
            else currentState = GASMASKSTATES.GasMaskOffInSmoke;
        }
        else
        {
            if (gasMaskOn) currentState = GASMASKSTATES.GasMaskOn;
            else currentState = GASMASKSTATES.GasMaskOffOutOfSmoke;
        }

        switch (currentState)
        {
            case GASMASKSTATES.GasMaskOffOutOfSmoke:
                if (playOnce)
                {
                    AudioManager.instance.StopPlaying("ChokingGas");
                    AudioManager.instance.Play("DeepBreath");
                    playOnce = false;
                }
                break;
            case GASMASKSTATES.GasMaskOffInSmoke:
                if (!playOnce)
                {
                    AudioManager.instance.StopPlaying("DeepBreath");
                    AudioManager.instance.StopPlaying("ChokingGas");
                    AudioManager.instance.Play("ChokingGas");
                    playOnce = true;
                }

                PlayerHealth.instance.TakeDamage(gasDamage * Time.deltaTime);

                break;
            case GASMASKSTATES.GasMaskOn:
                AudioManager.instance.StopPlaying("ChokingGas");
                AudioManager.instance.StopPlaying("DeepBreath");
                break;
        }

        return;

        //if (!canBreath && (puttingOn || puttingOff) && playOnce)
        //{
        //    Debug.Log("Stop all sounds");
        //    PlayerHealth.instance.playerHealth = PlayerHealth.instance.maxHealth;
        //    AudioManager.instance.StopPlaying("ChokingGas");
        //    AudioManager.instance.StopPlaying("DeepBreath");
        //    PlayerHealth.instance.UpdateHealth();
        //}

        //else if (!canBreath && !playOnce && (!gasMaskOn || puttingOff))
        //{
        //    Debug.Log("Choking Sound");
        //    AudioManager.instance.Play("ChokingGas");
        //    playOnce = true;
        //}

        //else if (canBreath && playOnce && (!puttingOn || puttingOff) && !gasMaskOn)
        //{
        //    if (canBreath)
        //    {
        //        Debug.Log("Deep breath");
        //        PlayerHealth.instance.playerHealth = PlayerHealth.instance.maxHealth;
        //        AudioManager.instance.StopPlaying("ChokingGas");
        //        AudioManager.instance.Play("DeepBreath");
        //        PlayerHealth.instance.UpdateHealth();
        //        playOnce = false;
        //    }
        //}

        //if (!canBreath && PlayerHealth.instance.playerHealth <= 0)
        //{
        //    PlayerHealth.instance.Death();
        //}
        #endregion
    }

   /* void ReplaceFilter()
    {
        #region Replace Filter Code
        AudioManager.instance.Play("ReplaceFilter");
        filterTimer = maxFilterTimer;
        maskFilters--;
        vignetteEffect.intensity = defaultVignette;
        hasFilter = true;
        UpdateFilterUI("WhiteFilter");
        UpdateFilterUI("FilterNumber");
        UpdateFilterUI("FilterValue");
        filterChanged = false;
        #endregion
    }*/

    public void DamageGas()
    {
        #region Damaging Gas Section
        canBreath = false;
        player.GetComponent<FirstPersonController>().m_WalkSpeed = walkGas;
        player.GetComponent<FirstPersonController>().m_RunSpeed = runGas;
        blurEffect.enabled = true;
        #endregion
    }

    public void CanBreath()
    {
        #region Can Breath Region
        canBreath = true;
        player.GetComponent<FirstPersonController>().m_WalkSpeed = walkNorm;
        player.GetComponent<FirstPersonController>().m_RunSpeed = runNorm;
        blurEffect.enabled = false;
        #endregion
    }

   /* public void UpdateFilterUI(string filterType)
    {
        #region Update Filter UI Section
        if (filterType == "FilterNumber")
        {
            filterUI.text = maskFilters.ToString("0");
        }

        else if (filterType == "RedFilter")
        {
            filterMainUI.color = Color.red;
        }

        else if (filterType == "WhiteFilter")
        {
            filterMainUI.color = Color.white;
        }

        else if (filterType == "FilterValue")
        {
            FilterLevelUI.text = filterTimer.ToString("0");
        }
        #endregion
    }*/

   /* public void UpdateMaskUI(string maskType)
    {
        #region Update Mask UI Section        
        if (maskType == "MaskWhite")
        {
            maskUI.color = Color.white;
        }
        else if(maskType == "MaskGreen")
        {
            maskUI.color = Color.green;
        }
        #endregion
    }*/

    IEnumerator Wait()
    {
        #region Waiting while putting on
        if (!gasMaskOn) puttingOff = true;
        else puttingOn = true;
        yield return new WaitForSeconds(2.5f);
        puttingOn = puttingOff = false;
        #endregion
    }

    IEnumerator MaskOn()
    {
        #region Putting Mask On Section

        gasMaskOn = true;
        AudioManager.instance.Play("BreathIn");
        yield return new WaitForSeconds(1.5f);
       // UpdateMaskUI("MaskGreen");
        AudioManager.instance.Play("GasMask_Breathing");
        screenOverlayEffect.enabled = true;
        vignetteEffect.enabled = true;

        #endregion
    }

    void MaskOff()
    {
        #region Putting Mask On Section
        gasMaskOn = false;
       // UpdateMaskUI("MaskWhite");
        AudioManager.instance.StopPlaying("GasMask_Breathing");
        AudioManager.instance.StopPlaying("DeepBreath");
        screenOverlayEffect.enabled = false;
        vignetteEffect.enabled = false;
        #endregion
    }
}
