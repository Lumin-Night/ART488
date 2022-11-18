using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JC.Layout
{
	[CustomEditor(typeof(ArrayLayout))]
	public class ArrayLayoutEditor : Editor
	{
		ArrayLayout m_instance;

		// Use this for initialization
		void Start()
		{
			m_instance = target as ArrayLayout;
		}

		private void OnEnable()
		{
			m_instance = target as ArrayLayout;
		}


		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if(GUILayout.Button("Evenly Distribute"))
			{
				m_instance.EvenlyDistribute();
			}

			if(GUILayout.Button("Distance Interval"))
			{
				m_instance.Interval();
			}
		}
	}
}