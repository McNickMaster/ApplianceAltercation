using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuzzy : MonoBehaviour
{
    [SerializeField]
    private float speed = 750f, damage = 10;

    public ParticleSystem OnHitParticleSystem;
    public SpriteRenderer fireSprite;

    private bool active = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    private void Explode()
    {
        OnHitParticleSystem.Play();
        active = false;
        fireSprite.enabled = false;
        Invoke("Die", 0.5f);

    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Explode();
        }

        if (collision.CompareTag("Environment"))
        {
            Explode();
        }

        //print(collision.tag);
    }
}
