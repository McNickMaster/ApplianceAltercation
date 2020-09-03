using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Appliance
{
    public Transform firePoint;

    public GameObject projectile;

    public Animator animator;

    [SerializeField]
    private float projectileDamage = 90f, projectileScale = 2f, firerate = 0.6f;

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
    }

    void StopFiring()
    {
        CancelInvoke("Fire");
    }

    void Fire()
    {
        GameObject temp;
        int num = 3;
        for (int i = 0; i < num; i++)
        {
          
            temp = Instantiate(projectile, firePoint.position, firePoint.rotation * Quaternion.AngleAxis((15 * (i - (num / 2))), transform.forward));
            temp.GetComponent<FireProjectile>().setDamage(projectileDamage);
            temp.transform.localScale *= projectileScale;
        }

        //Instantiate(projectile, firePoint.position, firePoint.rotation);

        //Instantiate(projectile, firePoint.position, firePoint.rotation);
        //  Instantiate(projectile, firePoint.position, Quaternion.Euler(0, 0, firePoint.rotation.z + transform.rotation.z + 30));
    }

}
