using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss01 : MonoBehaviour
{
    [Tooltip("Health points in integer")]
    public int health;

    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;
    public GameObject Projectile_beam;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;

    public float shotChance;
    [HideInInspector] public float shotTimeMin, shotTimeMax;

    private bool canShoot_;
    public float random;
    public float actualshotchance;

    // Start is called before the first frame update
    void Start()
    {

    }

    void ActivateShooting()
    {
        float randomAngle = Random.Range(270f, 360f);
        Quaternion randomQuaternion = Quaternion.Euler(0, 0, randomAngle);
        GameObject shot1 = Instantiate(Projectile, gameObject.transform.position, randomQuaternion);
        randomQuaternion = Quaternion.Euler(0, 0, randomAngle + Random.Range(10, 20));
        GameObject shot2 = Instantiate(Projectile, gameObject.transform.position, randomQuaternion);
        GameObject beam = Instantiate(Projectile_beam, gameObject.transform.position, randomQuaternion);

    }

    // Update is called once per frame
    void Update()
    {
        if ((Random.value < shotChance / 100))
        {
            ActivateShooting();
        }
    }
}
