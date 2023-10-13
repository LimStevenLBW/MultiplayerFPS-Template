using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHud : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;

    public void UpdateHealthText(int health)
    {
        healthText.SetText(health + " / 100");
    }

    public void UpdateAmmoText(int ammo)
    {
        ammoText.SetText(ammo + " / 30");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
