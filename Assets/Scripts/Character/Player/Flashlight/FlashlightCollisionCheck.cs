using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlashlightController))]
public class FlashlightCollisionCheck : MonoBehaviour {

    public FlashlightController flashlightHandler;
    public float sphereRadius;
    public float endOfLightOffset;

    public void Update()
    {
        RaycastHit hit;
        Physics.SphereCast(flashlightHandler.flashLight.transform.position, sphereRadius, flashlightHandler.flashLight.transform.forward, out hit, flashlightHandler.flashLight.range - endOfLightOffset);

        //Draws the sphere that will be cast when checking for enemies
        Debug.DrawRay(flashlightHandler.flashLight.transform.position, flashlightHandler.flashLight.transform.forward.normalized * (flashlightHandler.flashLight.range - endOfLightOffset), Color.green);
        Debug.DrawRay(new Vector3(flashlightHandler.flashLight.transform.position.x, flashlightHandler.flashLight.transform.position.y, flashlightHandler.flashLight.transform.position.z + sphereRadius), flashlightHandler.flashLight.transform.forward.normalized * (flashlightHandler.flashLight.range - endOfLightOffset), Color.green);
        Debug.DrawRay(new Vector3(flashlightHandler.flashLight.transform.position.x, flashlightHandler.flashLight.transform.position.y, flashlightHandler.flashLight.transform.position.z - sphereRadius), flashlightHandler.flashLight.transform.forward.normalized * (flashlightHandler.flashLight.range - endOfLightOffset), Color.green);
        Debug.DrawRay(new Vector3(flashlightHandler.flashLight.transform.position.x, flashlightHandler.flashLight.transform.position.y + sphereRadius, flashlightHandler.flashLight.transform.position.z), flashlightHandler.flashLight.transform.forward.normalized * (flashlightHandler.flashLight.range - endOfLightOffset), Color.green);
        Debug.DrawRay(new Vector3(flashlightHandler.flashLight.transform.position.x, flashlightHandler.flashLight.transform.position.y - sphereRadius, flashlightHandler.flashLight.transform.position.z), flashlightHandler.flashLight.transform.forward.normalized * (flashlightHandler.flashLight.range - endOfLightOffset), Color.green);

        if ((hit.collider != null) && (hit.collider.CompareTag("Enemy")))
        {
            StateController enemyStateController = hit.collider.GetComponent<StateController>();
            //Adds time to the detected enemies currentTimeInFlashlight. This is used for decision making when this value exceeds the enemies threshold.
            if (enemyStateController)
            {
                enemyStateController.flashlightHoldingPlayer = this.gameObject;
                enemyStateController.currentTimeInFlashlight += Time.deltaTime;
            }
        }
    }

    //Draws the sphere that will be cast when checking for enemies
    public void OnDrawGizmos()
    {
        if (flashlightHandler != null)
            Gizmos.DrawWireSphere(flashlightHandler.flashLight.transform.position, sphereRadius);
    }
}
