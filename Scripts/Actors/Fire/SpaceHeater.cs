using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceHeater : Appliance
{
    public Transform firePoint;

    public GameObject projectile;

    public Animator animator;

    [SerializeField]
    private float projectileDamage = 55f, projectileScale = .9f, firerate = 0.2f, burstDelay = 1.5f, burstNumber = 3;


    [SerializeField]
    private bool destroyProjectileOnEnemyHit = false;

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

        animator.SetBool("shoot", base.IsActionPressed());

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
        InvokeRepeating("FireBurst", 0, burstDelay);
    }

    void StopFiring()
    {
        CancelInvoke("Fire");
        CancelInvoke("FireBurst");
    }

    void FireBurst()
    {
        for(int i = 0; i < burstNumber; i++)
        {
            Invoke("Fire", firerate * i);
        }
        
    }

    void Fire()
    {
        GameObject temp;
        int num = 11;
        for (int i = 0; i < num; i++)
        {
            temp = Instantiate(projectile, firePoint.position, firePoint.rotation * Quaternion.AngleAxis((15 * (i - (num / 2))), transform.forward));
            temp.GetComponent<FireProjectile>().setDamage(projectileDamage);
            temp.GetComponent<FireProjectile>().DestroyOnEnemyHit(destroyProjectileOnEnemyHit);
            temp.transform.localScale *= projectileScale;
        }

        //Instantiate(projectile, firePoint.position, firePoint.rotation);

        //Instantiate(projectile, firePoint.position, firePoint.rotation);
        //  Instantiate(projectile, firePoint.position, Quaternion.Euler(0, 0, firePoint.rotation.z + transform.rotation.z + 30));
    }

}
