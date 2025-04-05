using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarsController : MonoBehaviour
{
    [SerializeField] private Stats playerStats;
    [SerializeField] private Image hpBar, shieldBar, mpBar;
    [SerializeField] private TextMeshProUGUI hpShield, mpText;
    
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStats == null)
            playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();
        
        hpBar.fillAmount = Mathf.Clamp(1f * playerStats.hp / playerStats.maxHp, 0, 1);
        shieldBar.fillAmount = Mathf.Clamp(1f * playerStats.shield / playerStats.maxShield, 0, 1);
        mpBar.fillAmount = Mathf.Clamp(1f * playerStats.mp / playerStats.maxMp, 0, 1);
        
        if(playerStats.shield == 0)
            hpShield.text = playerStats.hp + "/" + playerStats.maxHp;
        else
            hpShield.text = playerStats.shield + "/" + playerStats.maxShield;
        
        mpText.text = playerStats.mp + "/" + playerStats.maxMp;
    }
}
