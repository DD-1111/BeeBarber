using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManage : MonoBehaviour
{
    public static BattleManage Instance { get; private set; }
    // Start is called before the first frame update
    public HealthBar playerHealthBar;
    public float playerHealth = 100;

    public HealthBar enemyHealthBar;
    public float enemyHealth = 100;
    public GameObject enemyCap;

    public ScreenFlash screenFlash;
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemeyTakeDamage(float damage)
    {
        enemyHealth = Mathf.Max(0, enemyHealth - damage);
        enemyHealthBar.SetHealth(enemyHealth);
        if (enemyHealth <= 50f & enemyCap.GetComponent<hairMoving>().enabled)
        {
            changeState();
        }
    }

    public void playerTakeDamage(float damage)
    {
        playerHealth = Mathf.Max(0, playerHealth - damage);
        playerHealthBar.SetHealth(playerHealth);
        Debug.Log("here");
        if (screenFlash)
        {
            Debug.Log("Here");
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
    }
}   

