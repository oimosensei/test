using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor.Animations;
using UnityEngine;


public class FanGimmick : MonoBehaviour
{
    public float force = 10f; // 風力の強さ
    public Vector2 direction = Vector2.right; // 風の向き
    private bool isActive = false; // ギミックが動作中かどうかを示すフラグ

    public ReactiveProperty<bool> IsClickedForTutorial  = new ReactiveProperty<bool>(false);

    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // タップされたときの処理
        if (Input.GetMouseButtonDown(0))
        {
            isActive = true;
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isActive = true; // ギミックを有効にする
            }
            
            IsClickedForTutorial.Value = true;

            animator.SetBool("isOn", true);
        }

        // タップを離したときの処理
        if (Input.GetMouseButtonUp(0))
        {
            isActive = false; // ギミックを無効にする

            IsClickedForTutorial.Value = false;
            animator.SetBool("isOn", false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isActive)
        {
            // 玉（HeroBall）が風に当たっている場合
            if(other.gameObject.tag == "Ball")
            {
                Rigidbody2D ballRigidbody = other.GetComponent<Rigidbody2D>();
                if (ballRigidbody != null)
                {
                    // 風の向きと強さに基づいて力を加える
                    ballRigidbody.AddForce(direction * force);
                }
            }
        }
    }
}
