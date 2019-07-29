using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour {

  public Vector3 TargetPoint { 
    get { return targetPoint;} 
    set { 
      if (value.x > targetPoint.x && value.y > targetPoint.y) 
          targetPoint = value;
        } 
    }
  public float speed = 1.0f;
  public float arriveDistance = 1.0f;
  public float distance => Vector3.Distance(
      this.transform.position, TargetPoint);
  public bool arriveTarget => distance <= arriveDistance;
  
  [SerializeField]
  private Vector3 targetPoint;
  private Animator animator => this.gameObject.GetComponent<Animator>();

  // Start is called before the first frame update
  void Start() { 
    targetPoint = this.transform.position;
  }

  // Update is called once per frame
  void Update() {
    animator.SetBool("walk", !arriveTarget);
    if (!arriveTarget) WalkToTarget();
  }

  void WalkToTarget() {
    this.transform.position = Vector3.Lerp(
      this.transform.position,
      TargetPoint, speed * Time.deltaTime / distance
    );
  }
}