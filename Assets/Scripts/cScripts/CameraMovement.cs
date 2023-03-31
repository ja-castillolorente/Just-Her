using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    [HideInInspector] public GameObject pressedObject;
    ObjectInteraction objectInteractionScript;
    Camera camera;
    // Variable para la rotación de cámara
    Vector3 targetDirection;
    Quaternion defaultRotation;
    Quaternion nextRotation;
    public float rotationSpeed = 5;
    // Variables para el zoom
    float defaultFovValue;
    public float zoom_InDuration = 1.5f;
    float zoom_InTime;
    public float zoom_OutDuration = 1.5f;
    float zoom_OutTime;
    // Bool para indicar el fin del zoom-in
    [HideInInspector] public bool hasZoom_InEnded;
    void Start()
    {
        camera = GetComponent<Camera>();
        defaultRotation = transform.rotation;
        defaultFovValue = camera.fieldOfView;
    }
    public void SetDirection()
    {
        objectInteractionScript = pressedObject.GetComponent<ObjectInteraction>();
        targetDirection = pressedObject.transform.position - transform.position;
        nextRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        StartCoroutine("ZoomIn");
    }
    IEnumerator ZoomIn()
    {
        while (zoom_InTime < zoom_InDuration)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, objectInteractionScript.objectFOV_Value, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, rotationSpeed * Time.deltaTime);
            zoom_InTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        hasZoom_InEnded = true;
        zoom_InTime = 0;
    }
    IEnumerator ZoomOut()
    {
        while (zoom_OutTime < zoom_OutDuration)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, defaultFovValue, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultRotation, rotationSpeed * Time.deltaTime);
            zoom_OutTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        zoom_OutTime = 0;
    }
}
