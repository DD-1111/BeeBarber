using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManage : MonoBehaviour
{
    public static BattleManage Instance { get; private set; }
    // Start is called before the first frame update
    public HealthBar playerHealthBar;
    public float playerHealth = 100;

    public HealthBar dashBar;
    public HealthBar dashBar2;
    public GameObject enchantSaber;
    public GameObject theSaber;
    public GameObject finalSaber;
    public Canvas textUI;
    public float UIsecond = 0f;

    public HealthBar enemyHealthBar;
    public float enemyHealth = 100;
    public GameObject enemyCap;

    public ScreenFlash screenFlash;
    public ScreenFlash changeStateScreenFlash;

    public enchantBar enchant;
    public int chargeN = 0;
    public int cutedTimes = 20;

    public GameObject snk;
    private float chargeTime = 0;
    public OVRPlayerController mainControl;
    public bool executionMode = false;
    private bool enchantAvailable;

    public float minimumChargeInterval = 2f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }


    void Start()
    {
        enchant.updateCharge(chargeN);
        theSaber.SetActive(true) ;
        
    }

    // Update is called once per frame
    void Update()
    {
        dashBar.SetHealth(mainControl.second * 20);
        dashBar2.SetHealth(mainControl.second * 20);
        chargeTime += Time.deltaTime;
        if (textUI.enabled)
        {
            UIsecond += Time.deltaTime;
            if (UIsecond > 3)
            {
                textUI.enabled = false;
            }
        }

        if (enchantAvailable & OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.1f)
        {
            spendCharge();
        }
    }

    public void EnemeyTakeDamage(float damage)
    {
        enemyHealth = Mathf.Max(0, enemyHealth - damage);
        enemyHealthBar.SetHealth(enemyHealth);
        if (enemyHealth <= 50f & enemyCap.GetComponent<hairMoving>().enabled)
        {
            if (changeStateScreenFlash)
            {
                StartCoroutine(changeStateScreenFlash.Fade());
            }
            changeState();
        }
        if (enemyHealth <= 0f)
        {
            finish();
        }
    }

    public void playerTakeDamage(float damage)
    {
        playerHealth = Mathf.Max(0, playerHealth - damage);
        playerHealthBar.SetHealth(playerHealth);
        if (screenFlash)
        {
            StartCoroutine(screenFlash.Flash());
        }
    }

    public void changeState()
    {
        enemyCap.GetComponent<hairMoving>().enabled = false;
        enemyCap.GetComponent<ChasingRobot>().enabled = true;
        enemyHealth = 100f;
        enemyHealthBar.SetHealth(enemyHealth);
        GameObject.FindGameObjectWithTag("disappear").SetActive(false);
        snk.SetActive(true);
        TextSetter("This customer seems to be mad, oh, she is THE MEDUSA");
    }

    public void charge()
    {
        if (chargeTime > minimumChargeInterval) {
            if (chargeN < 3)
            {
                chargeN++;
                enchant.updateCharge(chargeN);
            }
            if (chargeN == 3)
            {
                enchantAvailable = true;
                TextSetter("Press right index finger trgger to enchant the saber!!");
            }
            
            chargeTime = 0;
        }
    }

    public bool spendCharge()
    {
        if (chargeN != 3) return false;
        chargeN = 0;
        enchant.updateCharge(chargeN);
        enchantSaber.SetActive(true);
        return true;
    }

    public void finish()
    {
        detach("Hair");
        detach("Part");
        detach("Hard");
        enchantSaber.SetActive(false);
        theSaber.SetActive(false);
        finalSaber.SetActive(true);
        executionMode = true;
        
    }

    private void detach(string tag)
    {
        var temp = GameObject.FindGameObjectsWithTag(tag);
        foreach(var part in temp)
        {
            var rb = part.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
    }

    public void exe()
    {
        if (cutedTimes != 0)
        {
            cutedTimes--;
            TextSetter("Remaining slicing : " + cutedTimes);
        } else
        {
            TextSetter("Medusa neutralized, congrats and GG!!");
        }
    }
    public void TextSetter(string s)
    {
        UIsecond = 0f;
        textUI.enabled = true;
        textUI.GetComponent<Text>().text = s;
    }
}   

