using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputController _inputController;

    private void Start()
    {
        _inputController = new InputController(this);
    }

    private void Update()
    {
        _inputController.MyUpdate();
    }
}