using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LensSummoner : MonoBehaviour
{
    // the lenses must always be in the RGB order in here
    public GameObject[] lensPrefabs;
    public Transform summonLocation;

    public Transform lensParent;

    public float lensLifetime = 5f;

    private ActionController _ac;
    
    // -1 to show there is no selected prefab
    private int _selectedPrefab = -1;

    private void Start()
    {
        _ac = GetComponent<ActionController>();
    }

    // Update is called once per frame
    void Update()
    {
        // is this extra click really necessary?
        // not if I stick with only 3 options in the summoning menu
        // if I add more modifiers it will still be necesary
        
        // would it be an idea to consider a radial menu?
        // might be a bit too much tbh
        if (Input.GetMouseButtonDown(0))
        {
            StartSummoning();
        }
    }

    public void SelectLens(int lensId)
    {
        _selectedPrefab = lensId;
    }

    private void StartSummoning()
    {
        Debug.Log("Summon");

        SummonLens();
        
        _ac.EndSummoning();
    }

    private void SummonLens()
    {
        // only if there is prefab selected
        if (_selectedPrefab < 0) return;
        
        GameObject lens = Instantiate(lensPrefabs[_selectedPrefab], summonLocation);
            
        lens.transform.SetParent(lensParent);
        
        Destroy(lens, lensLifetime);
    }
}
