using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakerScript : MonoBehaviour
{
    BoxCollider range;
    public Transform evaderPos;
    // Start is called before the first frame update
    void Start()
    {
        range = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reveal()
    {
        StartCoroutine(ActualReveal());
    }

    IEnumerator ActualReveal()
    {
        range.enabled = true;
        yield return new WaitForSeconds(5);
        range.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Evader"))
        {
            evaderPos = other.transform;
        }
    }
}
