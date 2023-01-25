using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shoot : MonoBehaviour
{
    public GameObject atlas;
    public RectTransform crosshair;

    Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            crosshair.DOScale(new Vector3(1, 1, 1), 0.25f);

        }
        else if (Input.GetMouseButton(0))
        {
            crosshair.position += Input.mousePosition - mousePosition;
            crosshair.position = new Vector3(Mathf.Clamp(crosshair.position.x, 0, Screen.width), Mathf.Clamp(crosshair.position.y, 0, Screen.height), 0);

            mousePosition = Input.mousePosition;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            atlas.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            atlas.transform.parent = null;

            atlas.transform.position = Camera.main.transform.position + Camera.main.ScreenPointToRay(crosshair.position).direction * 1.5f;
            atlas.transform.GetComponent<Rigidbody>().AddForce(Camera.main.ScreenPointToRay(crosshair.position).direction * 2500);
            crosshair.DOScale(new Vector3(0, 0, 0), 0.25f);

        }
    }
}
