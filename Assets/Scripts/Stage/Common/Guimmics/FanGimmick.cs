using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor.Animations;
using UnityEngine;


public class FanGimmick : MonoBehaviour
{
    public float force = 10f; // ���͂̋���
    public Vector2 direction = Vector2.right; // ���̌���
    private bool isActive = false; // �M�~�b�N�����쒆���ǂ����������t���O

    public ReactiveProperty<bool> IsClickedForTutorial  = new ReactiveProperty<bool>(false);

    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // �^�b�v���ꂽ�Ƃ��̏���
        if (Input.GetMouseButtonDown(0))
        {
            isActive = true;
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isActive = true; // �M�~�b�N��L���ɂ���
            }
            
            IsClickedForTutorial.Value = true;

            animator.SetBool("isOn", true);
        }

        // �^�b�v�𗣂����Ƃ��̏���
        if (Input.GetMouseButtonUp(0))
        {
            isActive = false; // �M�~�b�N�𖳌��ɂ���

            IsClickedForTutorial.Value = false;
            animator.SetBool("isOn", false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isActive)
        {
            // �ʁiHeroBall�j�����ɓ������Ă���ꍇ
            if(other.gameObject.tag == "Ball")
            {
                Rigidbody2D ballRigidbody = other.GetComponent<Rigidbody2D>();
                if (ballRigidbody != null)
                {
                    // ���̌����Ƌ����Ɋ�Â��ė͂�������
                    ballRigidbody.AddForce(direction * force);
                }
            }
        }
    }
}
