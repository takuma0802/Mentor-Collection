using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomTransformOfCell {

	public static Transform SetParentWithReset(this Transform self,Transform parent)
	{
		self.SetParent(parent);
		self.transform.localPosition = Vector3.zero;
		self.transform.localEulerAngles = Vector3.zero;
		self.transform.localScale = Vector3.one;
		return self;
	}
}