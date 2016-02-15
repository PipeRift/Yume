using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
using CrabEditor;
#endif

public class DissolveEffect : MonoBehaviour {
	public float speed = 3f;
	public List<DissolveEffect> connectedEffects = new List<DissolveEffect>();

	private bool disolving = false;
	private float amount = 0;
	private new Renderer renderer;
	private int layer;

	void Start() {
		//Shader.Find("Transparent/Diffuse")
		renderer = GetComponent<Renderer>();
		layer = gameObject.layer;
	}

	public void Dissolve()
	{
		if (disolving)
			return;

		layer = gameObject.layer;
		gameObject.layer = LayerMask.NameToLayer("Dissolved");
		disolving = true;

		for (int i = 0, len = connectedEffects.Count; i < len; i++) {
			connectedEffects[i].Dissolve();
		}
	}

	public void Constitute()
	{
		if (!disolving)
			return;

		gameObject.layer = layer;
		disolving = false;

		for (int i = 0, len = connectedEffects.Count; i < len; i++)
		{
			connectedEffects[i].Constitute();
		}
	}

	void Update () {
		if (disolving && amount < 0.75f)
		{
			amount = Mathf.Lerp(amount, 0.76f, speed * Time.deltaTime);
			renderer.material.SetFloat("_DissolveAmount", amount);
		}
		else if (!disolving && amount > 0)
		{
			amount = Mathf.Lerp(amount, -0.01f, speed * Time.deltaTime);
			renderer.material.SetFloat("_DissolveAmount", amount);
		}
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(DissolveEffect))]
public class DissolveEffectEditor : Editor
{
	protected DissolveEffect t;
	private ReorderableList connectedEffects;

	private void OnEnable()
	{
		connectedEffects = new ReorderableList(serializedObject,
				serializedObject.FindProperty("connectedEffects"),
				true, true, true, true);
		connectedEffects.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField(rect, "Connected Effects");
		};
		connectedEffects.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
			var element = connectedEffects.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
				element, GUIContent.none);
		};
	}

	void Awake()
	{
		t = target as DissolveEffect;
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		t.speed = EditorGUILayout.Slider("Speed", t.speed, 0.5f, 5);
		connectedEffects.DoLayoutList();

		serializedObject.ApplyModifiedProperties();

		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}
}
#endif
