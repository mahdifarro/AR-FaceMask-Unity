using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARFaceMaskManager : MonoBehaviour
{
    [SerializeField]
    ARFaceManager _ARFaceManager;

    [SerializeField]
    GameObject[] facePrefabs;
    [SerializeField]
    Button changeFaceMaskButton;

    int _activeMaskIndex;

    // Start is called before the first frame update
    void Awake()
    {
        AssignButtons();

        _activeMaskIndex = 0; 
        SetARFaceMask(_activeMaskIndex);
    }

    private void AssignButtons()
    {
        changeFaceMaskButton.onClick.AddListener(() =>
        {
            _activeMaskIndex = (_activeMaskIndex + 1) % facePrefabs.Length;
            SetARFaceMask(_activeMaskIndex);
        });
    }

    private void SetARFaceMask(int activeMaskIndex)
    {
        _ARFaceManager.gameObject.SetActive(false);
        _ARFaceManager.facePrefab = facePrefabs[activeMaskIndex];
        _ARFaceManager.gameObject.SetActive(true);
    }
}
