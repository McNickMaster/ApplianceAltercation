using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 1000f, damage = 20, knockbackForce = 0;

    public ParticleSystem OnHitParticleSystem;
    public SpriteRenderer fireSprite;

    private bool destroyOnEnemyHit = false;

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

    public void DestroyOnEnemyHit(bool state)
    {
        destroyOnEnemyHit = state;
    }

    public void SetKnockback(float force)
    {
        knockbackForce = force;
    }

    private void Explode(bool die)
    {
        OnHitParticleSystem.Play();
        
        if (die)
        {
            active = false;
            fireSprite.enabled = false;
            Invoke("Die", 0.5f);
        }
        
        
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
            enemy.KnockBack(knockbackForce);
            Explode(destroyOnEnemyHit);
        }

         if(collision.CompareTag("Environment"))
        {
            Explode(true);
        }

        //print(collision.tag);
    }
}
