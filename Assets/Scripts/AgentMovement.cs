using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Rigidbody2D가 없으면 만들어서 에러를 없애기 위해 사용하는 키워드
[RequireComponent(typeof(Rigidbody2D))]
public class AgentMovement : MonoBehaviour
{
  protected Rigidbody2D rb2d;

  [SerializeField]
  public MovementDataSO MovementData;

  [SerializeField]
  protected float currentVelocity = 3;
  protected Vector2 movementDirection;

  [SerializeField]
  public UnityEvent<float> OnVelocityChange;

  private void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
  }

  public void MoveAgent(Vector2 movementInput)
  {
    if (movementInput.magnitude > 0)
    {
      // 내적값이 음수면 서로 반대 방향을 가리킨다는 의미
      if (Vector2.Dot(movementInput.normalized, movementDirection) < 0)
      {
        currentVelocity = 0;
      }
      movementDirection = movementInput.normalized;
    }
    currentVelocity = CalculateSpeed(movementInput);
  }

  private float CalculateSpeed(Vector2 movementInput)
  {
    if (movementInput.magnitude > 0)
    {
      currentVelocity += MovementData.acceleration * Time.deltaTime;
    }
    else
    {
      currentVelocity -= MovementData.deacceleration * Time.deltaTime;
    }
    return Mathf.Clamp(currentVelocity, 0, MovementData.maxSpeed);
  }

  // rigidbody를 움직일 때는 FixedUpdate를 쓰는게 현재 가장 좋은 방법이라고 한다.
  private void FixedUpdate()
  {
    OnVelocityChange?.Invoke(currentVelocity);
    rb2d.velocity = currentVelocity * movementDirection.normalized;

  }
}
