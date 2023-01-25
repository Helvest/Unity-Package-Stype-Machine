﻿using System.Collections.Generic;
using UnityEngine;

namespace StypeMachine
{
	[DefaultExecutionOrder(-9999)]
	public abstract class FlypeMachineController<T> : MonoBehaviour, IHoldFlypeMachine where T : class
	{

		#region Fields

		[Header("FlypeMachineController")]

		[SerializeField]
		protected bool apceptFlagsNotIncluded = false;

		[SerializeField]
		protected bool canReenterSameFlag = false;

		[SerializeField]
		protected List<T> startFlags = default;

		public FlypeMachine FlypeMachine { get; private set; }

		protected bool hasStarted = false;

		#endregion

		#region Init

		protected virtual void Awake()
		{
			CreateFlypeMachine();
		}

		protected virtual void CreateFlypeMachine()
		{
			FlypeMachine = new FlypeMachine(apceptFlagsNotIncluded, canReenterSameFlag);
#if UNITY_EDITOR || DEVELOPMENT_BUILD
			FlypeMachine.useDebugLog = _useDebugLog;
#endif
		}

		protected virtual void OnEnable()
		{
			ToStartFlags();
		}

		protected virtual void OnDisable()
		{
			FlypeMachine.UnsetAllFlags();
			hasStarted = false;
		}

		protected virtual void ToStartFlags()
		{
			if (hasStarted)
			{
				return;
			}

			hasStarted = true;

			this.SetFlags(startFlags);
		}

		#endregion

		#region Debug

#if UNITY_EDITOR || DEVELOPMENT_BUILD

		[Header("Debug")]
		[SerializeField]
		protected bool _useDebugLog = false;

		public bool UseDebugLog
		{
			get { return FlypeMachine != null ? FlypeMachine.useDebugLog : _useDebugLog; }
			set { 
				_useDebugLog = value;

				if (FlypeMachine != null)
				{
					FlypeMachine.useDebugLog = _useDebugLog;
				}
			}
		}

#endif

		#endregion

	}
}
