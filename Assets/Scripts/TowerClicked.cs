using UnityEngine;
using UnityEngine.EventSystems;

public class TowerClicked : MonoBehaviour
{
    public GameObject towerMenu; // The menu that appears when a tower is clicked
    public LayerMask towerLayer; // The layer on which towers are placed

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (towerMenu != null)
        {
            towerMenu.SetActive(false); // Initially, the menu is not visible
        }
    }

    public void ShowMenu()
    {
        if (towerMenu.activeSelf)
        {
            towerMenu.SetActive(false);
        }
        else
        {
            towerMenu.SetActive(true);
        }
    }
}
