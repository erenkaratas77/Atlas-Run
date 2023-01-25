using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthquake : MonoBehaviour
{
    [Header("Info")]
    private Vector3 _startPos;
    private Vector3 _randomPos;

    [Header("Settings")]
    [Range(0f, 2f)]
    public float _time = 0.2f;
    [Range(0f, 2f)]
    public float _distance = 0.1f;
    [Range(0f, 0.1f)]
    public float _delayBetweenShakes = 0f;

    private void start()
    {
        _startPos = transform.position;
    }

    private void OnValidate()
    {
        if (_delayBetweenShakes > _time)
            _delayBetweenShakes = _time;
    }

    public void Begin()
    {
    }
    private void FixedUpdate()
    {
        StartCoroutine(Shake());


    }

    private IEnumerator Shake()
    {



        _randomPos = _startPos + (Random.insideUnitSphere * _distance);
        _randomPos.x = transform.position.x;
        _randomPos.y = transform.position.y;

        transform.position = _randomPos;

        if (_delayBetweenShakes > 0f)
        {
            yield return new WaitForSeconds(_delayBetweenShakes);
        }
        else
        {
            yield return null;
        }


        transform.position = new Vector3(transform.position.x, transform.position.y, _startPos.z);


    }
}