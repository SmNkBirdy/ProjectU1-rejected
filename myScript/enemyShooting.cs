using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    [Header("Importing")]
    public GameObject bullet;
    [Header("Variables")]
    public float bulletSpeed = 10;
    public float weaponDamage = 1;
    public float fireRate = 1;

    float fireTime;
    public void shoot()
    {
        if (fireTime + 1 / fireRate < Time.time || fireTime == 0)
        {
            fireTime = Time.time;
            GameObject currentBullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            currentBullet.GetComponent<Rigidbody>().velocity = currentBullet.transform.forward * bulletSpeed;
            bulletMechanics bulletMechanics = currentBullet.GetComponent<bulletMechanics>();
            bulletMechanics.weaponDamage = weaponDamage;
            bulletMechanics.enemyBullet = true;
        }
    }

    void Update()
    {
        shoot();
    }
}
