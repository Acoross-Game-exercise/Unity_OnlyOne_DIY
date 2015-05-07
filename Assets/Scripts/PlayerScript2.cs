using UnityEngine;
using System.Collections;

public class PlayerScript2 : MonoBehaviour
{
	public WalkingState m_WalkingState = WalkingState.Idle;
	public Direction m_Direction = Direction.Right;
	public GameObject m_Sword;
	public GameObject m_SwordInstance;

	private Animator m_Animator;

	void Start ()
	{
		m_Animator = GetComponent<Animator>();

		// 칼 생성
		m_SwordInstance = Instantiate(m_Sword);
		m_SwordInstance.transform.Translate(new Vector2(0f, 0f));
		m_SwordInstance.transform.SetParent(transform);
		m_SwordInstance.gameObject.SetActive(false);
	}

	public void AttackAnimationCallback()
	{
		//(m_SwordInstance.GetComponent<BoxCollider2D>() as BoxCollider2D).isTrigger = false;
		m_SwordInstance.gameObject.SetActive(false);
		StopCoroutine("SwingSword");
		m_SwordInstance.transform.rotation = Quaternion.identity;


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

	public void StartSwing()
	{
		m_SwordInstance.gameObject.SetActive(true);
		StartCoroutine("SwingSword");
	}

	IEnumerator SwingSword()
	{
		while(true)
		{
			// 90도 swing
			m_SwordInstance.transform.Rotate(new Vector3(0f, 0f, 1f), -Time.deltaTime*600, Space.World);
			
			yield return null;
		}
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

			SwordColliderListner swordScript = m_SwordInstance.GetComponent<SwordColliderListner>();
			//(m_SwordInstance.GetComponent<BoxCollider2D>() as BoxCollider2D).isTrigger = true;

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
			//Move (horizontal, vertical);	// 모든방향 버전
			Move ();	// 4방향 only

			if (horizontal == 0 && vertical == 0)
			{
				m_WalkingState = WalkingState.Idle;
				m_Animator.SetBool("isWalking", false);
			}
		}

	}

	private void Move()
	{
		int horizontal = 0;
		int vertical = 0;

		switch (m_Direction)
		{
		case Direction.Down:
			vertical = -1;
			break;
		case Direction.Up:
			vertical = 1;
			break;
		case Direction.Right:
			horizontal = 1;
			break;
		case Direction.Left:
			horizontal = -1;
			break;
		default:
			break;
		}

		Move (horizontal, vertical);
	}

	private void Move(int horizontal, int vertical)
	{
		if (Input.GetKey(KeyCode.LeftControl))
			transform.Translate (new Vector3(horizontal, vertical, vertical * 10f) * Time.deltaTime * 3f);	// left control 누르면 3배 빠름.
		else
			transform.Translate (new Vector3(horizontal, vertical, vertical * 10f) * Time.deltaTime);
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
