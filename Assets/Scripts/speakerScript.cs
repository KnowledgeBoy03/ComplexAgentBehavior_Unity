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
    public AudioClip transmission;
    public AudioClip ambush;
    public bool reveal;
    public bool warning;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    // Start is called before the first frame update
    void Start()
    {
        range = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        p1 = GameObject.FindGameObjectWithTag("1");
        p2 = GameObject.FindGameObjectWithTag("2");
        p3 = GameObject.FindGameObjectWithTag("3");
        p4 = GameObject.FindGameObjectWithTag("4");
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

    public void Respond()
    {
        audioSource.PlayOneShot(transmission);
    }

    public void Ready()
    {
        audioSource.PlayOneShot(ambush);
    }

    IEnumerator ActualReveal()
    {
        range.enabled = true;
        this.gameObject.SetActive(true);
        Alarm();
        yield return new WaitForSeconds(15);
        if (!p1.GetComponent<pursuerScript>().found)
        {
            p1.GetComponent<pursuerScript>().ambush = true;
        }
        yield return new WaitForSeconds(5f);
        if (!p2.GetComponent<pursuerScript>().found)
        {
            p2.GetComponent<pursuerScript>().ambush = true;
        }
        yield return new WaitForSeconds(5f);
        if (!p3.GetComponent<pursuerScript>().found)
        {
            p3.GetComponent<pursuerScript>().ambush = true;
        }
        yield return new WaitForSeconds(5f);
        if (!p4.GetComponent<pursuerScript>().found)
        {
            p4.GetComponent<pursuerScript>().ambush = true;
        }
        range.enabled = false;
    }

    IEnumerator ActualWarning()
    {
        range.enabled = true;
        this.gameObject.SetActive(true);
        BackUp();
        yield return new WaitForSeconds(5);
        if (!p1.GetComponent<pursuerScript>().suspect)
        {
            p1.GetComponent<pursuerScript>().transmission = true;
        }
        yield return new WaitForSeconds(2.5f);
        if (!p2.GetComponent<pursuerScript>().suspect)
        {
            p2.GetComponent<pursuerScript>().transmission = true;
        }
        yield return new WaitForSeconds(2.5f);
        if (!p3.GetComponent<pursuerScript>().suspect)
        {
            p3.GetComponent<pursuerScript>().transmission = true;
        }
        yield return new WaitForSeconds(2.5f);
        if (!p4.GetComponent<pursuerScript>().suspect)
        {
            p4.GetComponent<pursuerScript>().transmission = true;
        }
        range.enabled = false;
    }
}
