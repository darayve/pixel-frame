using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpaquePanelToggleActive : MonoBehaviour
{
    [SerializeField] GameObject menuUI;

    private void Update()
    {
        if (menuUI.activeSelf) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
