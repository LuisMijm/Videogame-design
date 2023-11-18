using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Beam : MonoBehaviour
{
    [Tooltip("Damage which a projectile deals to another object. Integer")]
    public int damage;

    [Tooltip("Whether the projectile belongs to the ‘Enemy’ or to the ‘Player’")]
    public bool enemyBullet;

    [Tooltip("Whether the projectile is destroyed in the collision, or not")]
    public bool destroyedByCollision;

    public GameObject bulletOrigin_;
    public GameObject bulletDestiny_;


    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        if (collision.tag == "Player") //if anoter object is 'player' or 'enemy sending the command of receiving the damage
        {
            Player.instance.GetDamage(damage);
            if (destroyedByCollision)
                Destruction();
        }
        else if (!enemyBullet && collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().GetDamage(damage);
            if (destroyedByCollision)
                Destruction();
        }
    }

    void Destruction()
    {
        Destroy(gameObject);
        Destroy(bulletOrigin_.GetComponent<GameObject>());
        Destroy(bulletOrigin_.GetComponent<GameObject>());
    }
}
