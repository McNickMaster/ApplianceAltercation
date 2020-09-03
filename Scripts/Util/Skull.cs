using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {

        Invoke("DestroyObject", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
