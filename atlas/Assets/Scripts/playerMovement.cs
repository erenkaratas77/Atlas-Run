using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class playerMovement : MonoBehaviour
{ 
public GameObject Earth;

public bool isFinished = false;
public bool isDead = false;

public LevelManager levelManager;

public float speed;
public float backForce;


float worldSize = 0.2f;
float doorValue;

Vector3 mousePosition;
Collider m_Collider;
Vector3 m_Size;

void Start()
{
    //Fetch the Collider from the GameObject
    m_Collider = GetComponent<Collider>();

    //Fetch the size of the Collider volume
    m_Size = m_Collider.bounds.size;

    //Output to the console the size of the Collider volume
    Debug.Log("Collider Size : " + m_Size);
}

// Update is called once per frame
private void Update()
{
    if (!isFinished && !isDead)
    {
        float horizontal = 0;
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            horizontal = (Input.mousePosition.x - mousePosition.x) / Screen.width * 1.5f;
            mousePosition = Input.mousePosition;
        }
        backForce = Mathf.Clamp(backForce + Time.deltaTime * 3.25f, -1, 1);

        transform.position += new Vector3(1, 0, 0) * Time.deltaTime * -speed * backForce + new Vector3(0, 0, 1) * horizontal * 5;
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -4f, 4f));
    }

}
private void FixedUpdate()
{
    if (worldSize <= 0)
    {
        GetComponentInChildren<Animator>().SetBool("isDead", true);
        levelManager.fail();
        isDead = true;
    }
}

private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.tag == "Finish")
    {
        isFinished = true;
        GetComponent<Shoot>().enabled = true;
        GetComponentInChildren<Animator>().SetBool("isFinished", true);
        this.enabled = false;
    }
    else if (other.gameObject.CompareTag("addDoor"))
    {
        Debug.Log("Bu büyüten kapı");
        doorValue = other.gameObject.GetComponent<Doors>().operationValue;
        worldSize += doorValue;
        Earth.transform.DOScale((worldSize), 1);
    }

    else if (other.gameObject.CompareTag("subDoor"))
    {
        Debug.Log("Bu kücülten kapı");
        doorValue = other.gameObject.GetComponent<Doors>().operationValue;
        worldSize -= doorValue;
        Earth.transform.DOScale((worldSize), 1);

    }

    if (other.gameObject.tag == "MainDoor" || (other.gameObject.tag == "blue") || (other.gameObject.tag == "green"))
    {
        StartCoroutine(destroyDoor(other.gameObject));
    }

}
IEnumerator destroyDoor(GameObject g)
{
    yield return new WaitForSeconds(0.6f);
    Destroy(g.gameObject);
}




}



