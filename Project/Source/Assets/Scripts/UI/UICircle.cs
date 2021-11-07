using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UICircle : Graphic
{
	private void Update()
	{
		this.Thickness = (int)Mathf.Clamp((float)this.Thickness, 0f, base.rectTransform.rect.width / 2f);
	}

    protected override void OnPopulateMesh(VertexHelper vbo)
    {
        //base.OnPopulateMesh(vbo);

		float num = -base.rectTransform.pivot.x * base.rectTransform.rect.width;
		float num2 = -base.rectTransform.pivot.x * base.rectTransform.rect.width + (float)this.Thickness;
		vbo.Clear();
		UIVertex simpleVert = UIVertex.simpleVert;
		Vector2 zero = Vector2.zero;
		Vector2 zero2 = Vector2.zero;
		float num3 = (float)this.FillPercent / 100f;
		int num4 = (int)(361f * num3);
		for (int i = 0; i < num4; i++)
		{
			float f = 0.017453292f * (float)i;
			float num5 = Mathf.Cos(f);
			float num6 = Mathf.Sin(f);
			simpleVert.color = this.color;
			simpleVert.position = zero;
			vbo.AddVert(simpleVert);
			zero = new Vector2(num * num5, num * num6);
			simpleVert.position = zero;
			vbo.AddVert(simpleVert);
			if (this.Fill)
			{
				simpleVert.position = Vector2.zero;
				vbo.AddVert(simpleVert);
				vbo.AddVert(simpleVert);
			}
			else
			{
				simpleVert.position = new Vector2(num2 * num5, num2 * num6);
				vbo.AddVert(simpleVert);
				simpleVert.position = zero2;
				vbo.AddVert(simpleVert);
				zero2 = new Vector2(num2 * num5, num2 * num6);
			}
		}
	}

    //protected override void OnFillVBO(List<UIVertex> vbo)
	//{
	//	float num = -base.rectTransform.pivot.x * base.rectTransform.rect.width;
	//	float num2 = -base.rectTransform.pivot.x * base.rectTransform.rect.width + (float)this.Thickness;
	//	vbo.Clear();
	//	UIVertex simpleVert = UIVertex.simpleVert;
	//	Vector2 zero = Vector2.zero;
	//	Vector2 zero2 = Vector2.zero;
	//	float num3 = (float)this.FillPercent / 100f;
	//	int num4 = (int)(361f * num3);
	//	for (int i = 0; i < num4; i++)
	//	{
	//		float f = 0.017453292f * (float)i;
	//		float num5 = Mathf.Cos(f);
	//		float num6 = Mathf.Sin(f);
	//		simpleVert.color = this.color;
	//		simpleVert.position = zero;
	//		vbo.Add(simpleVert);
	//		zero = new Vector2(num * num5, num * num6);
	//		simpleVert.position = zero;
	//		vbo.Add(simpleVert);
	//		if (this.Fill)
	//		{
	//			simpleVert.position = Vector2.zero;
	//			vbo.Add(simpleVert);
	//			vbo.Add(simpleVert);
	//		}
	//		else
	//		{
	//			simpleVert.position = new Vector2(num2 * num5, num2 * num6);
	//			vbo.Add(simpleVert);
	//			simpleVert.position = zero2;
	//			vbo.Add(simpleVert);
	//			zero2 = new Vector2(num2 * num5, num2 * num6);
	//		}
	//	}
	//}

	public int Thickness = 5;

	public bool Fill = true;

	[Range(0f, 100f)]
	public int FillPercent;
}
