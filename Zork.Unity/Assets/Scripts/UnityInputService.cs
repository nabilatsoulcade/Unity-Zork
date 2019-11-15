using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zork.Common;
using TMPro;
public class UnityInputService : MonoBehaviour, IInputService
{
    public event EventHandler<string> InputRecieved;
    public TMP_InputField InputField;
    public void ProcessInput()
    {
        InputRecieved?.Invoke(this, InputField.text);
    }
}
