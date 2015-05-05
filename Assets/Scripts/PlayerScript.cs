using UnityEngine;
using System.Collections;

public enum Direction
{
	Down,
	Up,
	Left,
	Right,
	UR,
	UL,
	DR,
	DL
}

public class PlayerScript : MonoBehaviour 
{
	public Direction m_Direction = Direction.Down;
	private Animator m_animator;
	
	// Use this for initialization
	void Start () 
	{
		m_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		int horizontal = 0;
		int vertical = 0;
		
		horizontal = (int) Input.GetAxisRaw ("Horizontal");
		vertical = (int) Input.GetAxisRaw ("Vertical");

		Move (horizontal, vertical);
	}

	private void Move(int horizontal, int vertical)
	{
		if (horizontal == 0)
		{
			if (vertical > 0)
			{
				TurnTo(Direction.Up);
			}
			else if (vertical < 0)
			{
				TurnTo (Direction.Down);
			}
		}
		// 대각선 이동 - Right
		else if (horizontal > 0)
		{
			// UR
			if (vertical > 0)
			{
				TurnTo (Direction.UR);
			}
			// DR
			else if (vertical < 0)
			{
				TurnTo (Direction.DR);
			}
			// right
			else 
			{
				TurnTo (Direction.Right);
			}
		}
		// 대각선 이동 - Left
		else if (horizontal < 0)
		{
			// UL
			if (vertical > 0)
			{
				TurnTo (Direction.UL);
			}
			// DL
			else if (vertical < 0)
			{
				TurnTo (Direction.DL);
			}
			// left
			else
			{
				TurnTo (Direction.Left);
			}
		}
	}

	#region coroutine test...
	private IEnumerator CoTurnTo(Direction dir)
	{
		if (m_Direction == dir)
			yield break;
		
		m_Direction = dir;

		switch (dir)
		{
		case Direction.Up:
			m_animator.SetTrigger("upTrigger");
			yield return new WaitForSeconds(0.1f);
			break;
		case Direction.Down:
			m_animator.SetTrigger("downTrigger");
			yield return new WaitForSeconds(0.1f);
			break;
		case Direction.Right:
			m_animator.SetTrigger("rightTrigger");
			yield return new WaitForSeconds(0.1f);
			break;
		case Direction.Left:
			m_animator.SetTrigger("leftTrigger");
			yield return new WaitForSeconds(0.1f);
			break;
		case Direction.UR:
			m_animator.SetTrigger("URTrigger");
			yield return new WaitForSeconds(0.1f);
			break;
		case Direction.UL:
			m_animator.SetTrigger("ULTrigger");
			yield return new WaitForSeconds(0.1f);
			break;
		case Direction.DR:
			m_animator.SetTrigger("DRTrigger");
			yield return new WaitForSeconds(0.1f);
			break;
		case Direction.DL:
			m_animator.SetTrigger("DLTrigger");
			yield return new WaitForSeconds(0.1f);
			break;
		default:
			break;
		}

		yield break;
	}
	#endregion

	private void TurnTo(Direction dir)
	{
		if (m_Direction == dir)
			return;

		switch (dir)
		{
		case Direction.Up:
			m_animator.SetTrigger("upTrigger");
			break;
		case Direction.Down:
			m_animator.SetTrigger("downTrigger");
			break;
		case Direction.Right:
			m_animator.SetTrigger("rightTrigger");
			break;
		case Direction.Left:
			m_animator.SetTrigger("leftTrigger");
			break;
		case Direction.UR:
			m_animator.SetTrigger("URTrigger");
			break;
		case Direction.UL:
			m_animator.SetTrigger("ULTrigger");
			break;
		case Direction.DR:
			m_animator.SetTrigger("DRTrigger");
			break;
		case Direction.DL:
			m_animator.SetTrigger("DLTrigger");
			break;
		default:
			break;
		}

		m_Direction = dir;
	}
}
