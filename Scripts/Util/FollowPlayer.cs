using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = EvolutionSystem.instance.GetCurrentPlayerTransform().position + new Vector3(0, 0, -10);
        //transform.position = player.transform.position + new Vector3(0, 0, -10);
    }
}
