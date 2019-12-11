using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	public float length = 30;
	public int startIndex = 0;
	public int endIndex = 0;
    public float[] lanes;

	public Platform(int length, int startIndex, int endIndex)
	{
		this.length = length;
		this.startIndex = startIndex;
		this.endIndex = endIndex;
	}
}