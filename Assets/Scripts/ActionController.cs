using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{   
    public Text summoningText;
    public GameObject summoningMenu;

    public float slowdownFactor = 0.05f;
    public float fixedDeltaTimeFactor = 0.02f;
    
    private Vector3 _aimDirection;

    private Shooting _shooting;
    private LensSummoner _lensSummoner;
    private SummoningMenu _summoningMenu;
    private GameController _gameController;

    private bool _isShooting = true;
    private bool _isSummoning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _shooting = GetComponent<Shooting>();
        _lensSummoner = GetComponent<LensSummoner>();
        _summoningMenu = summoningMenu.GetComponent<SummoningMenu>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
        // turn off the summoning at the beginning of the game
        SetSummoningStatus(false);
    }

    // Update is called once per frame
    void Update()
    {
        // don't allow actions if game paused
        if (_gameController.IsPaused())
        {
            return;
        }
        
        Aim();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_isShooting)
            {
                // if can shoot when E is pressed turn off shooting and enable summoning
                
                StartSummoning();
            }
            else 
            {
                // the opposite
                
                EndSummoning();
            }
        }

        if (!_isSummoning) return;
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectLens(0); // RED
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectLens(1); // GREEN
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectLens(2); // BLUE
        }
    }
    
    private void Aim()
    {
        // Get the position of the mouse in screen coordinates
        Vector3 mousePosition = Input.mousePosition;
        
        // Convert the screen point to a point in world coordinates
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));

        // Calculate the direction from the current object's position to the mouse position
        _aimDirection = mouseWorldPosition - transform.position;
        _aimDirection.y = 0; // Optional: if you want to restrict rotation to the X-Z plane

        // Rotate the object to face the mouse direction
        if (_aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_aimDirection);
        }
    }
    
    public void StartSummoning()
    {
        SetShootingStatus(false);
        SetSummoningStatus(true);
                
        summoningText.text = "1. Red, 2. Green, 3 Blue";
                
        // slow down the game while summoning
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = slowdownFactor * fixedDeltaTimeFactor;
        
        summoningMenu.SetActive(true);
    }

    public void EndSummoning()
    {
        SetShootingStatus(true);
        SetSummoningStatus(false);

        summoningText.text = "Press \"E\" to summon";
                
        Time.timeScale = 1f;
        
        _summoningMenu.Reset();
        summoningMenu.SetActive(false);
    }
    
    public Vector3 GetDirection()
    {
        return _aimDirection;
    }

    private void SetShootingStatus(bool value)
    {
        _isShooting = value;
        _shooting.enabled = value;
    }

    private void SetSummoningStatus(bool value)
    {
        _isSummoning = value;
        _lensSummoner.enabled = value;
    }

    private void SelectLens(int lensId)
    {
        _lensSummoner.SelectLens(lensId);
        _summoningMenu.SelectOption(lensId);
    }
}
