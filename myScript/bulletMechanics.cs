using UnityEngine;

public class bulletMechanics : MonoBehaviour
{
    [Header("Variables")]
    public float weaponDamage = 1;
    public bool enemyBullet = false;
    public float lifeTime = 5f;

    float bornTime;

    void Start()
    {
        bornTime = Time.time;
    }

    void Update()
    {
        if (Time.time > bornTime + lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && enemyBullet)
        {
            objectHealth health = other.GetComponent<objectHealth>();
            if (health != null)
            {
                health.healthPoint -= weaponDamage;
            }
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Enemy" && !enemyBullet)
        {
            objectHealth health = other.GetComponent<objectHealth>();
            if (health != null)
            {
                health.healthPoint -= weaponDamage;
            }
            Destroy(gameObject);
        }
    }
}