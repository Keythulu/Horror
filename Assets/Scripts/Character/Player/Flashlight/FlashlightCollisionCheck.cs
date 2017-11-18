using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlashlightController))]
public class FlashlightCollisionCheck : MonoBehaviour {

    FlashlightController flashlightController;
    [SerializeField] float sphereRadius;
    [SerializeField] float endOfLightOffset;
    [SerializeField] LayerMask layerMask;

    public void Awake()
    {
        flashlightController = GetComponent<FlashlightController>();
        if (flashlightController == null)
            throw new UnassignedReferenceException();
    }

    public void Update()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(flashlightController.flashLight.transform.position, sphereRadius, flashlightController.flashLight.transform.forward, flashlightController.flashLight.range - endOfLightOffset, layerMask);

        //Draws the sphere that will be cast when checking for enemies
        Debug.DrawRay(flashlightController.flashLight.transform.position, flashlightController.flashLight.transform.forward.normalized * (flashlightController.flashLight.range - endOfLightOffset), Color.green);
        Debug.DrawRay(new Vector3(flashlightController.flashLight.transform.position.x, flashlightController.flashLight.transform.position.y, flashlightController.flashLight.transform.position.z + sphereRadius), flashlightController.flashLight.transform.forward.normalized * (flashlightController.flashLight.range - endOfLightOffset), Color.green);
        Debug.DrawRay(new Vector3(flashlightController.flashLight.transform.position.x, flashlightController.flashLight.transform.position.y, flashlightController.flashLight.transform.position.z - sphereRadius), flashlightController.flashLight.transform.forward.normalized * (flashlightController.flashLight.range - endOfLightOffset), Color.green);
        Debug.DrawRay(new Vector3(flashlightController.flashLight.transform.position.x, flashlightController.flashLight.transform.position.y + sphereRadius, flashlightController.flashLight.transform.position.z), flashlightController.flashLight.transform.forward.normalized * (flashlightController.flashLight.range - endOfLightOffset), Color.green);
        Debug.DrawRay(new Vector3(flashlightController.flashLight.transform.position.x, flashlightController.flashLight.transform.position.y - sphereRadius, flashlightController.flashLight.transform.position.z), flashlightController.flashLight.transform.forward.normalized * (flashlightController.flashLight.range - endOfLightOffset), Color.green);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                StateController enemyStateController = hit.collider.GetComponent<StateController>();
                //Adds time to the detected enemies currentTimeInFlashlight. This is used for decision making when this value exceeds the enemies threshold.
                if (enemyStateController)
                {
                    enemyStateController.flashlightHoldingPlayer = gameObject;
                    enemyStateController.currentTimeInFlashlight += Time.deltaTime;
                }
            }
        }
    }

    //Draws the sphere that will be cast when checking for enemies
    public void OnDrawGizmos()
    {
        if (flashlightController != null)
            Gizmos.DrawWireSphere(flashlightController.flashLight.transform.position, sphereRadius);
    }
}
