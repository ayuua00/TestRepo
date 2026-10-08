using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{
    public GameObject BagPanel;
    public KeyCode BagtoggleKey=KeyCode.B;
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
            if (BagPanel != null)
            {
                bool isActive = BagPanel.gameObject.activeSelf;
                BagPanel.SetActive(!isActive);
            }

        }
    }
}
