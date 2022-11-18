using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JC.Layout
{
	/// <summary>
	/// used to align things into single or multi-dimensional arrays.
	/// You can align either by interval, or evenly distributing across a range
	/// </summary>
	public class ArrayLayout : MonoBehaviour
	{
		public enum ReferenceMode { StartingPoint, CenterPoint }
		public enum Axis
		{
			X,
			Y,
			Z
		}

		[SerializeField]
		Axis axis;

		[SerializeField]
		Transform[] transformsToAlign;

		[SerializeField]
		Transform reference;

		[SerializeField]
		ReferenceMode mode;

		[SerializeField]
		float distBetweenItems;

		[SerializeField]
		float totalDistance;

		[SerializeField]
		bool localOperation;

		[Header("Visualization Variables")]
		[SerializeField] bool previewDistributive = true;
		[Range(0,1)]
		[SerializeField] float previewSize = 0.01f;

		void Align(float interval, bool distributive)
		{
			Vector3 direction = GetDirection();
			Vector3 position = GetStartPosition(direction, distributive);

			for (int i = 0; i < transformsToAlign.Length; i++)
			{
				if (localOperation) transformsToAlign[i].transform.localPosition = GetPointForIndex(i, position, direction, interval);
				else transformsToAlign[i].position = GetPointForIndex(i, position, direction, interval);
			}
		}

		float GetIntervalDistributive()
		{
			return (transformsToAlign.Length > 0) ? totalDistance / Mathf.Max(0, transformsToAlign.Length - 1) : 0;
		}

		Vector3 GetPointForIndex(int i, Vector3 startPosition, Vector3 direction, float interval)
		{
			return startPosition + ((direction * interval) * i);
		}

		Vector3 GetDirection()
		{
			switch (axis)
			{
				case Axis.X:
					return (localOperation) ? Vector3.right : reference.TransformDirection(Vector3.right);

				case Axis.Y:
					return (localOperation) ? Vector3.up : reference.TransformDirection(Vector3.up);

				case Axis.Z:
					return Vector3.forward;

				default:
					return Vector3.zero;
			}
		}

		Vector3 GetStartPosition(Vector3 direction, bool distributive)
		{
			Vector3 position = Vector3.zero;

			if (mode == ReferenceMode.CenterPoint)
			{
				if (localOperation) position = -direction * (GetDistance(distributive) * 0.5f);
				else position = reference.TransformPoint(-direction * (GetDistance(distributive) * 0.5f));
			}
			else
			{
				if (localOperation) position = Vector3.zero;
				else position = reference.position;
			}

			return position;
		}

		float GetDistance(bool distributive)
		{
			if (distributive) return totalDistance;
			else
			{
				if (transformsToAlign == null || transformsToAlign.Length == 0) return 0;
				else return distBetweenItems * (transformsToAlign.Length - 1);
			}
		}

		#region Executive Functions
		public void EvenlyDistribute()
		{
			Align(GetIntervalDistributive(), true);
		}

		public void Interval()
		{
			Align(distBetweenItems, false);
		}
		#endregion

		private void OnDrawGizmosSelected()
		{
			Vector3 direction = GetDirection();
			Vector3 startPosition = (localOperation) ? reference.transform.TransformPoint(GetStartPosition(direction, previewDistributive)) :
				GetStartPosition(direction, previewDistributive);

			Gizmos.color = Color.green;
			for(int i=0; i < transformsToAlign.Length; i++)
			{
				Gizmos.DrawWireSphere(GetPointForIndex(i, startPosition, direction,
					((previewDistributive) ? GetIntervalDistributive() :
					distBetweenItems)), previewSize);
			}

			Gizmos.DrawLine(startPosition, startPosition + direction * GetDistance(previewDistributive));
		}
	}
}