using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour {

    public SphereCollider soundTrigger;
    public SoundObjectManager soundObjectManager;
    public float soundTriggerRange;

    private float timer;

    public void OnEnable()
    {
        timer = Time.time;
        Debug.Log("Sound object \"spawned\"");
    }
    public void Update()
    {
        soundTrigger.radius = soundTriggerRange;
        if (Time.time - timer > 2f)
        {
            timer = Time.time;
            Debug.Log("Sound Object sent back");
            soundObjectManager.ReturnSoundObject(this.gameObject);

        }   
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("thing in trigger name: " + other.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("ENemy triggered soundObject");
            other.gameObject.GetComponent<StateController>().investigateTarget = this.gameObject.transform;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, soundTriggerRange);
    }
}
