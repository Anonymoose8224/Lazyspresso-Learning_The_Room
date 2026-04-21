using System;
using UnityEngine;

public class CaterpillarPuzzleThrowing : MonoBehaviour
{
    [SerializeField] private GameObject[] rings;
    [SerializeField] private GameObject startingRing;
    [SerializeField] private int counter = 0;

    [SerializeField] private RingPuzzleData switchingRings;

    private void Awake()
    {
        counter = 0;
        rings = new GameObject[4];
        startingRing = rings[0];
    }
    private void Start()
    {
        switchingRings.onReachedArea += SwitchRings;
    }

    private void SwitchRings(bool reached)
    {
        if(reached == true)
        {
            counter++;
            startingRing = rings[counter];
        }
    }
}
