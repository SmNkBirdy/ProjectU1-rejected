using UnityEngine;

public class weaponScript : MonoBehaviour
{
    [Header("Importing")]
    public GameObject spawnPoint;
    public GameObject bullet;
    public GameObject item;
    [Header("Variables")]
    public float weaponDamage = 1;
    public float fireRate = 1;
    public float fireScatter = 1;
    public float bulletSpeed = 10;
    public int burstMode = 0;
    public float burstDelay = 0;
    public int bulletsAmountPerShot = 1;
    public float bulletLifeTime = 5;
    public int ammo;
    public bool infiniteAmmo;
    public bool enemyBullet = false;
    [Header("Choose one")]
    public bool primaryFire = true;
    public bool secondaryFire = false;

    int burstShots = 0;
    float burstTime = 0;
    float fireTime = 0;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (primaryFire) 
            {
                Fire();
            }
        }

        if (Input.GetMouseButton(1))
        {
            if (secondaryFire)
            {
                Fire();
            }
        }
    }

    public void Fire()
    {
        if (ammo > 0 || infiniteAmmo)
        {
            if (burstMode == 0)
            {
                if (fireTime + 1 / fireRate < Time.time || fireTime == 0)
                {
                    for (int i = 0; i < bulletsAmountPerShot; i++)
                    {
                        createBullet();
                    }
                    ammo--;
                }
            }
            else
            {
                if (burstTime + burstDelay < Time.time || burstTime == 0)
                {
                    if (fireTime + 1 / fireRate < Time.time || fireTime == 0)
                    {
                        for (int i = 0; i < bulletsAmountPerShot; i++)
                        {
                            createBullet();
                        }
                        ammo--;
                        burstShots++;
                    }
                    if (burstShots == burstMode)
                    {
                        burstTime = Time.time;
                        burstShots = 0;
                    }
                }
            }
        }
    }

    public void createBullet()
    {
        //счётчик скорострельности
        fireTime = Time.time;
        //создание пули
        float bulletScatter = Random.Range(fireScatter * -1, fireScatter + 1);
        GameObject currentBullet = Instantiate(bullet, spawnPoint.transform.position, transform.rotation);
        currentBullet.transform.Rotate(new Vector3(0, bulletScatter, 0));
        //предание скорости пуле
        currentBullet.GetComponent<Rigidbody>().velocity = currentBullet.transform.forward * bulletSpeed;
        //установка настроек
        bulletMechanics bulletMechanics = currentBullet.GetComponent<bulletMechanics>();
        bulletMechanics.weaponDamage = weaponDamage;
        bulletMechanics.enemyBullet = enemyBullet;
        bulletMechanics.lifeTime = bulletLifeTime;
    }
}
