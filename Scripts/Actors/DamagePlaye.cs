using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlaye : MonoBehaviour
{
    [SerializeField]
    private float damage = 5, attackRate = 1;

    public Enemy parentEnemy;
    private Appliance player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponentInParent<Appliance>();
            InvokeRepeating("Damage", 0, attackRate);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
            CancelInvoke("Damage");
        }
    }

    void Damage()
    {
        player.TakeDamage(damage);
        parentEnemy.KnockBack(0);
    }
}
