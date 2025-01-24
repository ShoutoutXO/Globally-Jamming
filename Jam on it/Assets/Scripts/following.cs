using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class following : MonoBehaviour
{

    public Transform person;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() => transform.position = new Vector3(person.transform.position.x, person.transform.position.y, transform.position.z);
}
