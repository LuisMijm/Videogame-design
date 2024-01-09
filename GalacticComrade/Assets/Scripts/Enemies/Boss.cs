using System.Collections;
using System.Collections.Generic;
// using System.Threading;
using UnityEngine;
using System.Timers;

/// <summary>
/// This script defines 'Boss's' health and behavior. 
/// </summary>
public class Boss : MonoBehaviour
{

    #region FIELDS
    [Tooltip("Health points in integer")]
    public int health;

    [Tooltip("Boss's projectile prefab")]
    public GameObject Projectile;
    public GameObject Projectile_beam;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;

    public float shotChance; //probability of 'Boss' shooting during tha path

    public float atk1ShotChance_ = 10;
    [HideInInspector] public float shotTimeMin, shotTimeMax; //max and min time for shooting from the beginning of the path
    public Timer difficultyTimer_; 
    public int difficultyDelay_ = 5000;
    #endregion

    private void Start()
    {
        // Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));
        // difficultyTimer_ = new Timer(difficultyDelay_);
        difficultyTimer_.Elapsed += IncreaseDifficulty;
        difficultyTimer_.Interval = difficultyDelay_;
        // difficultyTimer_.Start();
    }

    //coroutine making a shot
    void ActivateShooting()
    {

        float randomAngle = Random.Range(270f, 360f);
        Quaternion randomQuaternion = Quaternion.Euler(0, 0, randomAngle);
        GameObject shot1 = Instantiate(Projectile, gameObject.transform.position, randomQuaternion);
        randomQuaternion = Quaternion.Euler(0, 0, randomAngle + Random.Range(5, 15));
        GameObject shot2 = Instantiate(Projectile, gameObject.transform.position, randomQuaternion);
        // GameObject beam = Instantiate(Projectile_beam, gameObject.transform.position, randomQuaternion);
    }

    private void Update()
    {
        if (Random.value < (float)atk1ShotChance_ / 100)                             //if random value less than shot probability, making a shot
        // if(1 == 2)
        {
            ActivateShooting();
        }
    }

    public void IncreaseDifficulty(object source, ElapsedEventArgs e)
    {

        if (atk1ShotChance_ < 2.5f)
        // if(true)
        {
            atk1ShotChance_ += 0.05f;
            // atk1ShotChance_ += 23.0f;
        }
        else
        {
            difficultyTimer_.Stop();
            Debug.Log("stop");
        }
        Debug.Log("Difficulty++ " + atk1ShotChance_ + "Atk1 shotchance");
    }

    //method of getting damage for the 'Boss'
    public void GetDamage(int damage)
    {
        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
            Destruction();
        else
            Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
    }

    //if 'Boss' collides 'Player', 'Player' gets the damage equal to projectile's damage value
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Projectile.GetComponent<Projectile>() != null)
                Player.instance.GetDamage(Projectile.GetComponent<Projectile>().damage);
            else
                Player.instance.GetDamage(1);
        }
    }

    //method of destroying the 'Boss'
    void Destruction()
    {
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
