using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LensSummoner : MonoBehaviour
{
    public GameObject lensPrefab;
    public Transform summonLocation;

    public Transform lensParent;

    public Text summoningText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartSummoning();
        }
    }

    private void StartSummoning()
    {
        Debug.Log("Summon");

        summoningText.text = "1. Red, 2. Green, 3 Blue";

        SummonLens();
    }

    private void SummonLens()
    {
        GameObject lens = Instantiate(lensPrefab, summonLocation);
            
        lens.transform.SetParent(lensParent);
    }
}
