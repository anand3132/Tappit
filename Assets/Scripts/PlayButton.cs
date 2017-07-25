using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour {

	[Header("Max scale")]
	public Vector3 mMinScale;
	[Header("Min scale")]
	public Vector3 mMaxScale;
	[Header("Scaling Speed")]
	public Vector3 mScaleSpeed;

	private Vector3 currentScaleSpeed;

	void Start()
	{
		currentScaleSpeed = mScaleSpeed;
	}

	void Update()
	{
		if (transform.localScale.x <= mMinScale.x || transform.localScale.x >= mMaxScale.x)
		{
			currentScaleSpeed.x *= -1; //invert it
		}

		if (transform.localScale.y <= mMinScale.y || transform.localScale.y >= mMaxScale.y)
		{
			currentScaleSpeed.y *= -1; //invert it
		}

		if (transform.localScale.z <= mMinScale.z || transform.localScale.z >= mMaxScale.z)
		{
			currentScaleSpeed.z *= -1; //invert it
		}

		transform.localScale = transform.localScale + currentScaleSpeed * Time.deltaTime;
	}
}
