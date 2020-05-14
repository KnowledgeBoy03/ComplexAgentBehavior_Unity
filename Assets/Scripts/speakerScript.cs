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
    public bool reveal;
    public bool warning;
    // Start is called before the first frame update
    void Start()
    {
        range = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Reveal()
    {
        reveal = true;
        StartCoroutine(ActualReveal());
    }

    public void Warning()
    {
        warning = true;
        StartCoroutine(ActualWarning());
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

    IEnumerator ActualWarning()
    {
        range.enabled = true;
        BackUp();
        yield return new WaitForSeconds(5);
        range.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("1") || other.CompareTag("2") || other.CompareTag("3") || other.CompareTag("4"))
        {
            if (warning)
            {
                other.GetComponent<pursuerScript>().suspect = true;
            }
            if (reveal)
            {
                other.GetComponent<pursuerScript>().found = true;
            }
        }
    }
}
