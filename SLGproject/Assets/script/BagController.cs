using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{
    public GameObject BagPanel;
    public KeyCode BagtoggleKey=KeyCode.B;
    public void ToggleBag()
    {
        if (BagPanel != null)
        {
            BagPanel.SetActive(!BagPanel.activeSelf);
        }
    }
    private void Start()
    {
        if (BagPanel != null)
        {
            BagPanel.SetActive(false);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(BagtoggleKey))
        {
            ToggleBag();
        }
    }
}
