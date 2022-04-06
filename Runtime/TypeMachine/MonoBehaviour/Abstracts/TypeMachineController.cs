using System;
using UnityEngine;

namespace StypeMachine
{
	public abstract class TypeMachineController<T> : MonoBehaviour, IHoldTM
	{

		#region Variables

		[Header("TypeMachineController")]

		[SerializeField]
		protected bool apceptValuesNotIncluded = false;

		[SerializeField]
		protected bool canReenterSameState = false;

		[SerializeField]
		protected T startState = default;

		public TypeMachine TM { get; private set; }

		protected bool hasStarted = false;

		#endregion

		#region State

		public virtual Type State
		{
			get => TM.State;

			set => TM.State = value;
		}

		#endregion

		#region Init

		protected virtual void Awake()
		{
			CreateTypeMachine();

#if UNITY_EDITOR
			TM.useDebug = useDebug;
#endif
		}

		protected virtual void Start()
		{
			hasStarted = true;
			ToStartState();
		}

		protected virtual void CreateTypeMachine()
		{
			TM = new TypeMachine(typeof(T), apceptValuesNotIncluded, canReenterSameState);
		}

		protected virtual void OnEnable()
		{
			ToStartState();
		}

		protected virtual void OnDisable()
		{
			TM.ToDefault();
		}

		protected virtual void ToStartState()
		{
			if (!hasStarted || startState == null)
			{
				return;
			}

			this.SetState(startState);
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

