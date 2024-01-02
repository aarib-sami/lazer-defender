using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float padding = 0.7f;
    [SerializeField] public int health = 200;

    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] GameObject Laserprefab;

    [Header("Shooting Audio")]
    [SerializeField] AudioClip shooting;
    [SerializeField] [Range(0, 1)] float shootingVolume = 0.5f;

    [Header("Dying Audio")]
    [SerializeField] AudioClip dying;
    [SerializeField] [Range(0, 1)] float dyingVolume = 0.5f;

    [Header("Coroutine")]
    Coroutine firingCoroutine;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    [Header("Damage")]
    [SerializeField] DamageDealer damageDealer;
     
    void Start()
    {
        SetUpMoveBoundaries();
    }


    void Update()
    {
        Move();
        Fire();
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
        FindObjectOfType<GameSession>().alive = false;
        AudioSource.PlayClipAtPoint(dying, Camera.main.transform.position, dyingVolume);
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject,0.1f);
    }
    public int GetHealth()
    {
        return health;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           
           firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject laser = Instantiate(Laserprefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shooting, Camera.main.transform.position, shootingVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
        
    }


    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
       
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin + padding, xMax - padding);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin + padding - 0.2f, yMax - padding + 0.2f);

        transform.position = new Vector2(newXPos, newYPos);

    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        
    }

    
}
