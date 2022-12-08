using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManage : MonoBehaviour
{
    public static BattleManage Instance { get; private set; }
    // Start is called before the first frame update
    public HealthBar playerHealthBar;
    public float playerHealth = 100;

    public HealthBar dashBar;
    public HealthBar dashBar2;
    public GameObject enchantSaber;

    public HealthBar enemyHealthBar;
    public float enemyHealth = 100;
    public GameObject enemyCap;

    public ScreenFlash screenFlash;
    public ScreenFlash changeStateScreenFlash;

    public enchantBar enchant;
    public int chargeN = 0;

    public GameObject snk;
    private float chargeTime = 0;
    public OVRPlayerController mainControl;
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
    }

    // Update is called once per frame
    void Update()
    {
        dashBar.SetHealth(mainControl.second * 20);
        dashBar2.SetHealth(mainControl.second * 20);
        chargeTime += Time.deltaTime;
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
    }

    public void charge()
    {
        if (chargeTime > 3) {
            if (chargeN < 3)
            {
                chargeN++;
                enchant.updateCharge(chargeN);
            }
            if (chargeN == 3)
            {
                enchantSaber.SetActive(true);
            }
            
            chargeTime = 0;
        }
    }

    public bool spendCharge()
    {
        if (chargeN != 3) return false;
        chargeN = 0;
        enchant.updateCharge(chargeN);
        enchantSaber.SetActive(false);
        return true;
    }
}   

