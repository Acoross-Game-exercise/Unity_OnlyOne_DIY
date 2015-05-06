using UnityEngine;
using System;
using System.Collections;

using Random = UnityEngine.Random;

public class GameManagerScript : MonoBehaviour 
{
	public int col = 8;
	public int row = 8;
	public GameObject[] m_Tiles;

	private Transform m_BoardHolder;

	void Awake () 
	{
		// board setting
		m_BoardHolder = (new GameObject("Board")).transform;

		for (int x = 0; x < col; ++x)
		{
			for (int y = 0; y < row; ++y)
			{
				GameObject toInstantiate = m_Tiles[Random.Range (0, m_Tiles.Length)]; 	// 하드코딩
				GameObject instance = (GameObject)(Instantiate (toInstantiate, new Vector3(x, y, 0f), Quaternion.identity));
				instance.transform.SetParent(m_BoardHolder);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
