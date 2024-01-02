using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float health = 100;

    [Header("Score")]
    [SerializeField] int scoreValue = 150;
    
    [Header("Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 1f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float particleLife = 1f;
   
    [Header("Prefabs")]
    [SerializeField] GameObject enemyLaser;
    [SerializeField] GameObject particlePrefab;
    
    [Header("Shooting Audio")]
    [SerializeField] AudioClip shooting;
    [SerializeField] [Range(0, 1)] float shootingVolume = 0.2f;

    [Header("Dying Audio")]
    [SerializeField] AudioClip dying;
    [SerializeField] [Range(0, 1)] float dyingVolume = 1f;
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();       

    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(enemyLaser, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shooting, Camera.main.transform.position, shootingVolume);
    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();

        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        GameObject currentParticle = Instantiate(particlePrefab, transform.position, transform.rotation) as GameObject;
        AudioSource.PlayClipAtPoint(dying, Camera.main.transform.position, dyingVolume);
        Destroy(gameObject);
        Destroy(currentParticle, particleLife);
    }
}
