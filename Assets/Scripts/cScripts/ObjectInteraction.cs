using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ObjectInteraction : MonoBehaviour
{
    public string scriptName;
    public Texture2D magnifierTexture;
    [HideInInspector] public GameObject pressedObject;
    // Variables para el movimiento de cámara
    public float objectFOV_Value = 25;
    CameraMovement cameraScript;
    // Variables para el highlight
    public List<GameObject> children;
    public List<GameObject> grandchildren;
    Material[] parentMaterials;
    Material[] childMaterials;
    Material[] grandchildMaterials;
    public List<Material> materialList;
    [Header("Rim Values")]
    public float minimumRimValue;
    public float maximumRimValue;
    // Otras variables
    delegate void ClickedAction();
    event ClickedAction clickEvents;
    void Start()
    {
        cameraScript = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        // Si el padre tiene renderer, se buscan los materiales del padre
        if (gameObject.GetComponent<Renderer>() != null)
        {
            parentMaterials = gameObject.GetComponent<Renderer>().materials;
            for (int i = 0; i < parentMaterials.Length; i++)
            {
                materialList.Add(gameObject.GetComponent<Renderer>().materials[i]);
            }
        }
        // Si hay hijos, se buscan los hijos
        if (transform.childCount > 0)
        {
            children = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                children.Add(transform.GetChild(i).gameObject);
            }
            // Se buscan los materiales de los hijos
            FindChildrenMaterials();
            // Si los hijos tienen hijos, se buscan los hijos de los hijos o nietos (en este juego las relaciones no pueden ser más profundas)
            foreach (GameObject child in children)
            {
                if (child.transform.childCount > 0)
                {
                    grandchildren.Add(child.transform.GetChild(0).gameObject);
                }
                // Si hay nietos, se buscan sus materiales
                if (grandchildren.Count > 0)
                {
                    FindGrandchildrenMaterials();
                }
            }
        }
        clickEvents += DirectorScript.directorSingleton.DisableObjectInteraction;
        clickEvents += cameraScript.SetDirection;
    }
    void FindChildrenMaterials()
    {
        // Se buscan los materiales de cada hijo
        for (int childrenIndex = 0; childrenIndex < children.Count; childrenIndex++)
        {
            // Es posible que el hijo no tenga materiales, por lo que hay que comprobar que los tenga
            if (children[childrenIndex].GetComponent<Renderer>() != null)
            {
                childMaterials = children[childrenIndex].GetComponent<Renderer>().materials;
                // Se mete cada material del hijo en la lista de materiales
                foreach (Material material in childMaterials)
                {
                    materialList.Add(material);
                }
            }
        }
    }
    void FindGrandchildrenMaterials()
    {
        // Se buscan los materiales de cada nieto
        for (int grandchildrenIndex = 0; grandchildrenIndex < grandchildren.Count; grandchildrenIndex++)
        {
            // En este caso, los nietos siempre tienen materiales así que no hace falta una comprobación
            grandchildMaterials = grandchildren[grandchildrenIndex].GetComponent<Renderer>().materials;
            // Se mete cada material del nieto en la lista de materiales
            foreach (Material material in grandchildMaterials)
            {
                materialList.Add(material);
            }
        }
    }
    public void OnMouseDown()
    {
        cameraScript.pressedObject = gameObject;
        DirectorScript.directorSingleton.scriptName = scriptName;
        clickEvents();
    }
    void OnMouseEnter()
    {
        Cursor.SetCursor(magnifierTexture, Vector2.zero, CursorMode.Auto);
        foreach (Material material in materialList)
        {
            material.SetFloat("_RimMin", minimumRimValue);
            material.SetFloat("_RimMax", maximumRimValue);
        }
    }
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        foreach (Material material in materialList)
        {
            material.SetFloat("_RimMin", 1);
            material.SetFloat("_RimMax", 1);
        }
    }
}
