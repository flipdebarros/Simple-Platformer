using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

[EditorTool("DrawRectTool", typeof(MonoBehaviour))]
public class DrawRectTool : EditorTool, IDrawSelectedHandles
{

	public void OnDrawHandles()
	{
		foreach (MonoBehaviour targetObj in targets.OfType<MonoBehaviour>())
		{
			foreach ((object obj, FieldInfo fieldInfo) in FindRects(targetObj))
			{
				EditorGUI.BeginChangeCheck();
				Rect rect = (Rect)fieldInfo.GetValue(obj);
				DrawRect(ref rect, targetObj.transform.position);
				if (!EditorGUI.EndChangeCheck())
					continue;
				Undo.RecordObject(targetObj, "Change Rect");
				fieldInfo.SetValue(obj, rect);
			}
		}
	}

	void DrawRect(ref Rect rect, Vector2 position)
	{
		Rect displayRect = rect;
		displayRect.position += position;
		Handles.DrawSolidRectangleWithOutline(displayRect, new Color(0f, 0f, 1f, 0.1f), Color.blue);
		Vector2 newPos = Handles.PositionHandle(
			displayRect.center,
			Quaternion.identity
		);
		displayRect.center = newPos;

		
		Vector2[] corners = GetCorners(displayRect);
		for (int i = 0; i < corners.Length; i++)
		{
			EditorGUI.BeginChangeCheck();
			corners[i] = Handles.FreeMoveHandle(
				corners[i],
				HandleUtility.GetHandleSize(corners[i]) * 0.1f,
				Vector3.one * 0.01f,
				Handles.CircleHandleCap
			);
			if (EditorGUI.EndChangeCheck())
				displayRect = UpdateRect(i, corners);
		}

		displayRect.position -= position;
		rect = displayRect;
	}

	Vector2[] GetCorners(Rect rect)
	{
		Vector2[] corners = new Vector2[4];

		corners[0] = rect.position;
		corners[1] = rect.position + Vector2.Scale(rect.size, Vector3.up);
		corners[2] = rect.position + rect.size;
		corners[3] = rect.position + Vector2.Scale(rect.size, Vector3.right);

		return corners;
	}

	Rect UpdateRect(int index, Vector2[] corners)
	{
		int nextCornerIndex = (index + 1) % 4;
		int previousCornerIndex = (index + 3) % 4;

		if (index % 2 == 0)
		{
			corners[nextCornerIndex].x = corners[index].x;
			corners[previousCornerIndex].y = corners[index].y;
		}
		else
		{
			corners[previousCornerIndex].x = corners[index].x;
			corners[nextCornerIndex].y = corners[index].y;
		}

		return new Rect { min = corners[0], max = corners[2] };
	}

	IEnumerable<(object, FieldInfo)> FindRects(object obj)
	{
		HashSet<object> drawableObjects = new() { obj };
		Queue<object> objectsQueue = new();
		List<(object, FieldInfo)> rects = new();
		objectsQueue.Enqueue(obj);

		while (objectsQueue.Count > 0)
		{
			object current = objectsQueue.Dequeue();
			IEnumerable<FieldInfo> drawables = FindDrawables(current.GetType(), out IEnumerable<FieldInfo> rectFields);
			rects.AddRange(rectFields.Select(x => (current, x)));
			foreach (FieldInfo drawableField in drawables)
			{
				object drawable = drawableField.GetValue(current);
				if (targets.Contains(drawable) || !drawableObjects.Add(drawable))
					continue;
				objectsQueue.Enqueue(drawable);
			}
		}

		return rects;
	}

	static IEnumerable<FieldInfo> FindDrawables(Type type, out IEnumerable<FieldInfo> rectFields)
	{
		FieldInfo[] fields = type.GetFields(
			BindingFlags.Default |
			BindingFlags.Instance |
			BindingFlags.Static |
			BindingFlags.Public |
			BindingFlags.NonPublic
		);

		rectFields = fields.Where(x => x.GetCustomAttribute<DrawRectAttribute>() != null)
			.Where(x => x.FieldType == typeof(Rect));

		return fields.Where(x => x.FieldType.GetCustomAttribute<DrawableAttribute>() != null);
	}
}