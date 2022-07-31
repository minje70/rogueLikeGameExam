using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour
{
  private Camera mainCamera;
  [SerializeField]
  public UnityEvent<Vector2> OnMovementKeyPressed;
  [SerializeField]
  public UnityEvent<Vector2> OnPointerPositionChange;

  private void Awake()
  {
    mainCamera = Camera.main;
  }
  private void Update()
  {
    GetMovementInput();
    GetPointerInput();
  }

  private void GetPointerInput()
  {
    Vector3 mousePos = Input.mousePosition;
    mousePos.z = mainCamera.nearClipPlane;
    var mouseInWorldSpace = mainCamera.ScreenToWorldPoint(mousePos);
    OnPointerPositionChange?.Invoke(mouseInWorldSpace);
  }

  private void GetMovementInput()
  {
    OnMovementKeyPressed?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
  }
}
