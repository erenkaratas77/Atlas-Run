using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earth : MonoBehaviour
{
    public float speed = 1.0f;
    public Color startColor;
    public Color blueSmoke;
    public Color greenSmoke;

    public Color HairstartColor;
    public Color blueHair;
    public Color greenHair;

    public Material water;
    public Material land;
    public Material hair;

    public ParticleSystem smokeBlast;
    public LevelManager levelManager;

    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        water.color = startColor;
        land.color = startColor;
        hair.color = HairstartColor;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<Zeus>())
        {

            collision.transform.GetComponentInParent<Zeus>().health = 0;
            levelManager.win();
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        float t = (Time.time - startTime) * speed;
        if (other.gameObject.tag == "blue")
        {
            water.color = Color.Lerp(startColor, blueSmoke, t);
            hair.color = Color.Lerp(hair.color, blueHair, t);

            smokeBlast.Play();

        }
        else if (other.gameObject.tag == "green")
        {
            land.color = Color.Lerp(startColor, greenSmoke, t);
            hair.color = Color.Lerp(hair.color, greenHair, t);

            smokeBlast.Play();

        }
    }
}
