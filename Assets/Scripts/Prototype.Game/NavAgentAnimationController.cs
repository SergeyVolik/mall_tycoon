using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Prototype
{
    public class NavAgentAnimationController : MonoBehaviour
    {
        private static int s_WalkAnimParam = Animator.StringToHash("Walk");
        private static int s_MoveSpeedParam = Animator.StringToHash("MoveSpeed");
        private NavMeshAgent m_Agent;
        private Animator m_Animator;

        internal void SetMoveSpeed(float speed)
        {
            m_Animator.SetFloat(s_MoveSpeedParam, speed);
        }

        private void Awake()
        {
            m_Agent = GetComponentInParent<NavMeshAgent>();
            m_Animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!m_Agent)
                return;

            m_Animator.SetFloat(s_WalkAnimParam, m_Agent.velocity.magnitude);
        }
    }
}
