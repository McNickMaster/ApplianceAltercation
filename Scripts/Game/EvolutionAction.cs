using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionAction : MonoBehaviour
{
    public List<ParticleSystem> systems = new List<ParticleSystem>();

    private GameObject player;

    [SerializeField]
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = EvolutionSystem.instance.GetCurrentPlayerTransform().gameObject;

        if (active)
        {
            MovePlayer();
            PlaySystems();
        } else
        {

        }
    }

    void PlaySystems()
    {
        foreach (ParticleSystem system in systems)
        {
            system.Play();
        }
        active = false;
    }

    void MovePlayer()
    {
        //change to lerp
        player.transform.position = Vector3.zero;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.GetComponent<Appliance>().IsActive(false);
        player.GetComponent<Appliance>().rb.velocity = Vector3.zero;
        //Invoke("ResetPlayer", 1);
    }

    public void StartEffect()
    {
        active = true;
    }
    public void StopEffect()
    {
        foreach (ParticleSystem system in systems)
        {
            system.Stop();
        }
        active = false;
    }

    void ResetPlayer()
    {
        player.GetComponent<Appliance>().IsActive(true);
    }
}
