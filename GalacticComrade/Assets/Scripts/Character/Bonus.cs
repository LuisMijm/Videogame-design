using UnityEngine;

public class Bonus : MonoBehaviour {

    int MaxPowerUps = 2;
    float maxFireRate_ = 9.0f;


    private void GetPowerUp(int count = 0)
    {
        bool PW_obtained = true;

        switch (Random.Range(0, MaxPowerUps))
        {
        case 0:
            if (PlayerShooting.instance.weaponPower < PlayerShooting.instance.maxweaponPower)
            {
                PlayerShooting.instance.weaponPower++;
                Player.instance.life_++;
            }
            else { PW_obtained = false; }
            break;
        case 1:
            if (PlayerShooting.instance.fireRate < maxFireRate_)
            {
                PlayerShooting.instance.fireRate += 0.5f;
                Player.instance.life_++;
            }else { PW_obtained = false;}
            break;
        }

        if (!PW_obtained && count < 7)
        {
            GetPowerUp(count + 1);
        }
    }

    //when colliding with another object, if another objct is 'Player', sending command to the 'Player'
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player") 
        {
            GetPowerUp();
            Destroy(gameObject);
        }
    }
}
