using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour 
{
	public class CAcceleration
	{
		public float Ts;			// 적용시간 (s)
		public float m_dTs;		// 기 적용시간
		public Vector2 m_Acc;	// 가속도

		public CAcceleration(float t, Vector2 ac)
		{
			Ts = t;
			m_Acc = ac;
			m_dTs = 0f;
		}
	}

	private CAcceleration m_Acceleration = null;
	private Vector2 m_Velocity;

	void Awake()
	{
		m_Velocity = new Vector2(0f, 0f);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collidedObject)
	{
		// is kinematic 이라 물리엔진 못 쓴다.
		// 맞으면 밀려나게 하고 싶다.

		print ("ouch");

		// t 시간동안 a 의 가속도를 적용하기
		m_Acceleration = new CAcceleration(0.05f, new Vector2(120f, 0f));
	}

	void LateUpdate()
	{
		// 위치 update
		if (m_Velocity.sqrMagnitude > 0.1f)
		{
			transform.Translate (m_Velocity * Time.deltaTime);

			// 마찰력 (속도가 0보다 클 때만 작용) + 가속도 없을 때만
			Vector2 tmpVel = m_Velocity - m_Velocity.normalized * 10f * Time.deltaTime;
			
			if (tmpVel.sqrMagnitude > m_Velocity.sqrMagnitude)
				m_Velocity = Vector2.zero;
			else
				m_Velocity = tmpVel;
			//print ("rocation update: " + m_Velocity.sqrMagnitude + ", " + m_Velocity.ToString());
		}

		// 속도 update
		if (m_Acceleration != null)
		{
			print ("vel update");
			m_Velocity += m_Acceleration.m_Acc * Time.deltaTime;

			m_Acceleration.m_dTs += Time.deltaTime;
			if (m_Acceleration.Ts < m_Acceleration.m_dTs)
			{
				m_Acceleration = null;
			}
		}
	}
}
