using UnityEngine;

public class Gun : MonoBehaviour
{
    float fireRate = 10;
    float nextTimeToFire = 0;
    float accuracyModifier = 0.1f; // this needs to be adjusted to get higher with distance
    float bulletSpeed = 500f;
    Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    

    void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                Vector3 adjustedHitPoint = new Vector3(hit.point.x + Random.Range(-accuracyModifier, accuracyModifier), hit.point.y + Random.Range(-accuracyModifier, accuracyModifier), hit.point.z + Random.Range(-accuracyModifier, accuracyModifier));
                var hitParticlesGO = Instantiate(Resources.Load("Prefabs/HitParticles"), adjustedHitPoint, Quaternion.LookRotation(hit.normal)) as GameObject;
                Destroy(hitParticlesGO, 1.0f);

                if (hit.rigidbody)
                {
                    hit.rigidbody.AddForceAtPosition(-hit.normal * bulletSpeed, hit.point);
                }
            }
        }
    }

    public void Fire()
    {
        Shoot();
    }
}