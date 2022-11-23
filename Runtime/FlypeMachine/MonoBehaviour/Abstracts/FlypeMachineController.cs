﻿using UnityEngine;

namespace StypeMachine
{
	[DefaultExecutionOrder(-9999)]
	public abstract class FlypeMachineController<T> : MonoBehaviour, IHoldFlypeMachine where T : class
	{

		#region Fields

		[Header("FlypeMachineController")]

		[SerializeField]
		protected bool apceptValuesNotIncluded = false;

		[SerializeField]
		protected bool canReenterSameState = false;

		[SerializeField]
		protected T[] startFlags = default;

		public FlypeMachine FlypeMachine { get; private set; }

		protected bool hasStarted = false;

		#endregion

		#region Init

		protected virtual void Awake()
		{
			CreateFlypeMachine();

#if UNITY_EDITOR
			FlypeMachine.useDebug = useDebug;
#endif
		}

		protected virtual void Start()
		{
			hasStarted = true;
			ToStartFlags();
		}

		protected virtual void CreateFlypeMachine()
		{
			FlypeMachine = new FlypeMachine(apceptValuesNotIncluded, canReenterSameState);
		}

		protected virtual void OnEnable()
		{
			ToStartFlags();
		}

		protected virtual void OnDisable()
		{
			FlypeMachine.RemoveAllFlags();
		}

		protected virtual void ToStartFlags()
		{
			if (!hasStarted)
			{
				return;
			}

			this.AddFlags(startFlags);
		}

		#endregion

		#region Debug

#if UNITY_EDITOR

		[Header("Debug")]
		[SerializeField]
		protected bool useDebug = false;

#endif

		#endregion

	}
}
