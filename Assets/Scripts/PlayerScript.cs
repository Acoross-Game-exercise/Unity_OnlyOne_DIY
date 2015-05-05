using UnityEngine;
using System.Collections;

public enum Direction
{
	Down,
	Up,
	Left,
	Right
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
		
		if (vertical != 0)
		{
			horizontal = 0;
			if (vertical > 0 && m_Direction != Direction.Up)
			{
				m_animator.SetTrigger("upTrigger");
				m_Direction = Direction.Up;
			}
			else if (vertical < 0 && m_Direction != Direction.Down)
			{
				m_animator.SetTrigger("downTrigger");
				m_Direction = Direction.Down;
			}
		}
		else if (horizontal != 0)
		{
			vertical = 0;
			if (horizontal > 0 && m_Direction != Direction.Right)
			{
				m_animator.SetTrigger("rightTrigger");
				m_Direction = Direction.Right;
			}
			else if (horizontal < 0 && m_Direction != Direction.Left)
			{
				m_animator.SetTrigger("leftTrigger");
				m_Direction = Direction.Left;
			}
		}
	}
}
