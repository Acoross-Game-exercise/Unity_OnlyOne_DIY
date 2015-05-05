using UnityEngine;
using System.Collections;

public class PlayerScript2 : MonoBehaviour
{
	public WalkingState m_WalkingState = WalkingState.Idle;
	public Direction m_Direction = Direction.Right;

	private Animator m_Animator;

	int m_tmpCounter = 0;

	void Start ()
	{
		m_Animator = GetComponent<Animator>();
	}

	public void AttackAnimationCallback()
	{
		m_WalkingState = WalkingState.Idle;

		string cmd = "";

		switch(m_Direction)
		{
		case Direction.Right:
			cmd = "toRight";
			break;
		case Direction.Left:
			cmd = "toLeft";
			break;
		case Direction.Down:
			cmd = "toDown";
			break;
		case Direction.Up:
			cmd = "toUp";
			break;
		default:
			return;
		}

		m_Animator.SetTrigger(cmd);
	}

	void Update ()
	{
		if (m_WalkingState == WalkingState.Action)
		{
			return;
		}

		int jump = (int) Input.GetAxisRaw("Jump");
		if (jump > 0)
		{
			m_WalkingState = WalkingState.Action;
			m_Animator.SetTrigger("triggerAttack");
			return;
		}

		int horizontal = 0;
		int vertical = 0;

		// GetAxisRaw: 누르고 있으면 1 or -1 지속됨. 떼고 있으면 0
		horizontal = (int) Input.GetAxisRaw("Horizontal");
		vertical = (int) Input.GetAxisRaw ("Vertical");

		// 현재 방향과 다른 방향으로 가도록 명령
		ChangeDirection(horizontal, vertical);

		// idle 상태에서 방향키 눌르면 walk 로
		if (m_WalkingState == WalkingState.Idle)
		{
			// 걷기 상태로 변경
			if (horizontal != 0 || vertical != 0)
			{
				m_WalkingState = WalkingState.Walking;
				m_Animator.SetBool("isWalking", true);
			}
		}
		else if (m_WalkingState == WalkingState.Walking)
		{
			if (horizontal == 0 && vertical == 0)
			{
				m_WalkingState = WalkingState.Idle;
				m_Animator.SetBool("isWalking", false);
			}
		}
	}

	public bool ChangeDirection(int horizontal, int vertical)
	{
		if (horizontal == 0 && vertical == 0)
			return false;

		Direction newDirection;
		string cmd = "";

		// 4방향 버전이다.
		if (horizontal > 0)
		{
			newDirection = Direction.Right;
			cmd = "toRight";
		}
		else if (horizontal < 0)
		{
			newDirection = Direction.Left;
			cmd = "toLeft";
		}
		else if (vertical > 0)
		{
			newDirection = Direction.Up;
			cmd = "toUp";
		}
		else if (vertical < 0)
		{
			newDirection = Direction.Down;
			cmd = "toDown";
		}
		else
		{
			// default:
			return false;
		}

		if (m_Direction == newDirection)
			return false;

		m_Direction = newDirection;
		m_Animator.SetTrigger(cmd);

		return true;
	}
}
