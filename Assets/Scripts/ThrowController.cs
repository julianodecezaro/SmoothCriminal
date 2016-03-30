using UnityEngine;
using System.Collections;

public class ThrowController : GenericController
{
    public float Force = 7.0f * Time.deltaTime;
    public float DestroyDelay = 1.5f;

    // Use this for initialization
    void Start()
    {

    }    
    
    protected void Throw()
    {
        transform.Translate(new Vector3(Force, 0.0f, 0.0f));
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject, DestroyDelay);
    }
}
