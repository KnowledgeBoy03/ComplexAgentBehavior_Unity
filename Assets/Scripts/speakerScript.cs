using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakerScript : MonoBehaviour
{
    BoxCollider range;
    public Transform evaderPos;
    public AudioSource audioSource;
    public AudioClip found;
    public AudioClip suspect;
    // Start is called before the first frame update
    void Start()
    {
        range = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reveal()
    {
        StartCoroutine(ActualReveal());
    }

    public void BackUp()
    {
        audioSource.PlayOneShot(suspect);
    }

    public void Alarm()
    {
        audioSource.PlayOneShot(found);
    }

    IEnumerator ActualReveal()
    {
        range.enabled = true;
        Alarm();
        yield return new WaitForSeconds(5);
        range.enabled = false;
    }
}
