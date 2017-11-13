using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObjectManager : MonoBehaviour {

    public GameObject soundObjectPrefab;
    private List<GameObject> soundObjectsCache;

    public void Start()
    {
        soundObjectsCache = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            GameObject temp = Instantiate(soundObjectPrefab, this.transform);          
            if (temp.GetComponent<SoundObject>())
            {
                temp.gameObject.SetActive(false);
                temp.GetComponent<SoundObject>().soundTriggerRange = 0;
                temp.GetComponent<SoundObject>().soundObjectManager = this;
                temp.transform.position = this.transform.position;
                soundObjectsCache.Add(temp);
            }
            else
            {
                Debug.LogWarning("No SoundObject component attached to SoundObject prefab");
            }
        }        
    }

    public GameObject RequestSoundObject(float triggerRange)
    {
        GameObject temp;
        if (soundObjectsCache.Count > 0)
        {
            temp = soundObjectsCache[0];
            soundObjectsCache.Remove(temp);           
        }
        else
        {
            temp = Instantiate(soundObjectPrefab, this.transform);            
        }
        temp.SetActive(true);
        temp.GetComponent<SoundObject>().soundObjectManager = this;
        temp.GetComponent<SoundObject>().soundTriggerRange = triggerRange;
        return temp;
    }

    public void ReturnSoundObject(GameObject soundObject)
    {
        if (soundObject.GetComponent<SoundObject>())
        {
            soundObject.SetActive(false);
            soundObject.GetComponent<SoundObject>().soundTriggerRange = 0;
            soundObjectsCache.Add(soundObject);
        }
        else
        {
            Debug.LogWarning("Returned object does not have a SoundObject component attached");
        }
    }
}
