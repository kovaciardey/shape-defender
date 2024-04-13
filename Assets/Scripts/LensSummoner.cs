using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LensSummoner : MonoBehaviour
{
    public GameObject lensPrefab;
    public Transform summonLocation;

    public Transform lensParent;

    private ActionController _ac;

    private void Start()
    {
        _ac = GetComponent<ActionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSummoning();
        }
    }

    private void StartSummoning()
    {
        Debug.Log("Summon");

        SummonLens();
        
        _ac.EndSummoning();
    }

    private void SummonLens()
    {
        GameObject lens = Instantiate(lensPrefab, summonLocation);
            
        lens.transform.SetParent(lensParent);
    }
}
