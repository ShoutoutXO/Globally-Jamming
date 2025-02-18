using UnityEngine;

public class InvisibleItem : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().enabled = false; // Hides the object but keeps it active
    }
}