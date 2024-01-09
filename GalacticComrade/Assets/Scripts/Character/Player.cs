using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    public GameObject destructionFX;

    public static Player instance;

    public int life_ = 0;

    private float minFireRate_ = 3.5f;

    private float totalInvencibleTime_ = 3.0f;
    private float currentInvencibleTime_ = 0.0f;



    private void Awake()
    {
        if (instance == null) 
            instance = this;

        for (int i = 0; i < 15; i++)
        {
            //GameObject spriteVida = Instantiate(spriteVidaPrefab, contenedorVida);
            //spriteVida.transform.position = new Vector3(i * espacioEntreSprites, 0, 0);
        }
    }

    private void LosePowerUp()
    {
        int powerup_selector = Random.Range(0, 2);
        bool PW_lost = true;
        
        switch (powerup_selector)
        {
            case 0: // fireratio
                if (PlayerShooting.instance.fireRate > minFireRate_){
                    PlayerShooting.instance.fireRate -= 0.5f;
                }else{
                    PW_lost = false;
                }
                break; 

            case 1: // weaponpower
                if (PlayerShooting.instance.weaponPower > 1){
                    PlayerShooting.instance.weaponPower--;
                }else{
                    PW_lost = false;
                }
                break;
        }

        if (!PW_lost)
        {
            LosePowerUp();
        }
    }

    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)   
    {
        //Lose a perk until 0 perks and 0 shield
        if (currentInvencibleTime_ <= 0.0f)
        {
            if (life_ > 0){
                life_--;
                LosePowerUp();
                SetInvencible();
            }else{
                Destruction();
            }
        }
    }

    private void SetInvencible() { 
        currentInvencibleTime_ = totalInvencibleTime_;
    }


    private void Update()
    {
        if (currentInvencibleTime_ > 0.0f)
        {
            currentInvencibleTime_ -= Time.deltaTime;
            SpriteRenderer render = this.GetComponent<SpriteRenderer>();
            render.enabled = !render.enabled;
        }
        else
        {
            SpriteRenderer render = this.GetComponent<SpriteRenderer>();
            render.enabled = true;
        }
    }

    //'Player's' destruction procedure
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        Destroy(gameObject);
    }
}
















