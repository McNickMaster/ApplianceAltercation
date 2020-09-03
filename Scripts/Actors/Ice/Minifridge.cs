﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minifridge : Appliance
{
    public Transform firePoint;

    public GameObject projectile;

    public Animator animator;

    [SerializeField]
    private float projectileDamage = 20f, projectileScale = 1f, firerate = 0.1f, particleAmount = 6, projectileRange = 15, knockbackForce = 3 ;

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

        //animator.SetBool("shoot", base.IsActionPressed());

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
        float num = particleAmount;
        for (int i = 0; i < num; i++)
        {
            temp = Instantiate(projectile, firePoint.position, firePoint.rotation * Quaternion.AngleAxis((projectileRange * (i - (num / 2))), transform.forward));
            temp.GetComponent<FireProjectile>().setDamage(projectileDamage);
            temp.GetComponent<FireProjectile>().DestroyOnEnemyHit(destroyProjectileOnEnemyHit);
            temp.GetComponent<FireProjectile>().SetKnockback(knockbackForce);
            temp.transform.localScale *= projectileScale;
        }

        //Instantiate(projectile, firePoint.position, firePoint.rotation);

        //Instantiate(projectile, firePoint.position, firePoint.rotation);
        //  Instantiate(projectile, firePoint.position, Quaternion.Euler(0, 0, firePoint.rotation.z + transform.rotation.z + 30));
    }

}
