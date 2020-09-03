using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;


public class Enemy : MonoBehaviour
{

    public Rigidbody2D rb;
    [SerializeField]
    private Transform player;
    public Animator animator;
    public SpriteRenderer sprite;

    public ParticleSystem onDeath;

    public GameObject deathSkull;
    
    

    [SerializeField]
    private Color color;

    [SerializeField]
    private float maxSpeed = 100f, speed = 0, health = 100, xp = 10;

    private bool dead = false;


    private Vector3 knockback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //sprite = GetComponent<SpriteRenderer>();
        speed = maxSpeed;

        

    }

    // Update is called once per frame
    void Update()
    {
        color = sprite.color;

        if (!dead && !EvolutionSystem.instance.isEvolving)
        {
            player = EvolutionSystem.instance.GetCurrentPlayerTransform();
            //MoveToTarget();
            GetComponent<AIDestinationSetter>().enabled = true;
            GetComponent<Seeker>().enabled = true;
            GetComponent<AIPath>().enabled = true;
            GetComponent<AIDestinationSetter>().SetDestination(player);
            RotateToTarget();
        } else
        {
            GetComponent<AIDestinationSetter>().enabled = false;
            GetComponent<Seeker>().enabled = false;
            GetComponent<AIPath>().enabled = false;
            rb.velocity = Vector3.zero;
        }
        

        if(health <= 0)
        {
            Die();
        }
    }

    void MoveToTarget()
    {

        //rb.velocity = ((player.position -transform.position).normalized * speed) + knockback;


       // agent.SetDestination(player.position);

    }

    public void KnockBack(float force)
    {
        knockback = force * transform.up;
        Invoke("ResetKnockback", 0.5f);
    }

    private void ResetKnockback()
    {
        knockback = Vector2.zero;
    }

    void RotateToTarget()
    {

        Vector3 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        SetColor(new Color(0.945098f, 0.7098039f, 0.7098039f), 0.3f);
        print(sprite.color);
        //print(this.gameObject.name + " just took " + damage + " damage");
    }

    void SetColor(Color color, float time)
    {
        //sprite = EvolutionSystem.instance.playerSpriteRenderer;
        sprite.color = color;
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        sprite.color = Color.white;
    }

    void Die()
    {
        //animator.SetBool("dead", true);
        SetColor(new Color(0.645f, 0.4844191f, 0.4844191f), 0.5f);
        dead = true;
        Invoke("DestroyThisObject", 0.5f);
        rb.velocity = Vector3.zero;

    }

    void DestroyThisObject()
    {
        
        EvolutionSystem.instance.AddXP(xp);

        EnemySpawner.instance.currentEnemyCount--;
        Destroy(this.gameObject);
    }

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }

    private void SetSpeed()
    {
        speed = maxSpeed;
    }

    public void SetSpeed(float modifier, float duration)
    {
        speed *= modifier;
        Invoke("SetSpeed", duration);
    }

    private void OnDestroy()
    {
        Instantiate(deathSkull, transform.position, transform.rotation);
    }

}
