using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : Appliance
{
    public Transform firePoint;

    public GameObject projectile;

    public Animator animator;

    [SerializeField]
    private float projectileDamage = 50f, projectileScale = 1f, firerate = 0.5f;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        base.GetInput();
        base.Translate();
        base.Rotate();

        

        if (base.IsActionPressed())
        {
            StartFiring();

        }
        else if (base.IsStopActionPressed())
        {
            StopFiring();
        }

    }

    void StartFiring()
    {
        InvokeRepeating("Fire", 0, firerate);
        animator.SetBool("shoot", true);
        animator.SetBool("stopShoot", false);
    }

    void StopFiring()
    {
        CancelInvoke("Fire");
        animator.SetBool("shoot", false);
        animator.SetBool("stopShoot", true);
    }

    void Fire()
    {
        
        GameObject temp;
        for (int i = 0; i < 5; i++)
        {
            temp = Instantiate(projectile, firePoint.position, firePoint.rotation * Quaternion.AngleAxis((15 * (i - (5 / 2))), transform.forward));
            temp.GetComponent<FireProjectile>().setDamage(projectileDamage);
            temp.transform.localScale *= projectileScale;
        }

    //Instantiate(projectile, firePoint.position, firePoint.rotation);
        
        //Instantiate(projectile, firePoint.position, firePoint.rotation);
      //  Instantiate(projectile, firePoint.position, Quaternion.Euler(0, 0, firePoint.rotation.z + transform.rotation.z + 30));
    }
    
}
