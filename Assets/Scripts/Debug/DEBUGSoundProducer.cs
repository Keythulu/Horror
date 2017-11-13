using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGSoundProducer : MonoBehaviour {

    public SoundObjectManager soundObjectManager;
    public float range;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject temp = soundObjectManager.RequestSoundObject(range);
            temp.transform.position =  this.transform.position;
        }
    }
}
