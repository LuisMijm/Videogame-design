using UnityEngine;

public class Bonus : MonoBehaviour {

    int MaxPowerUps = 2;
    float maxFireRate_ = 9.0f;
    
    //when colliding with another object, if another objct is 'Player', sending command to the 'Player'
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player") 
        {
            switch (Random.Range(0, MaxPowerUps))
            {
                case 1:
                    if (PlayerShooting.instance.fireRate < maxFireRate_)
                    {
                        PlayerShooting.instance.fireRate += 0.5f;
                    }
                    break;
                default:
                    if (PlayerShooting.instance.weaponPower < PlayerShooting.instance.maxweaponPower)
                    {
                        PlayerShooting.instance.weaponPower++;
                    }
                    break;
            }

            Destroy(gameObject);
        }
    }
}
