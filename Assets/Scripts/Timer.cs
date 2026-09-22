using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] double scoreTime = 0.0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreTime += Time.deltaTime;
        scoreTime = Math.Round(scoreTime, 3);
        //Debug.Log("Time Take: " + scoreTime);
    }
}
