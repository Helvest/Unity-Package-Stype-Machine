﻿using System;

namespace StypeMachine
{
	[Serializable]
	public class TypeMachine : StateMachine<Type>
	{

		#region Constructor

		public TypeMachine(Type defaultValue = default, bool apceptValuesNotIncluded = true, bool canReenterSameState = false) : base(defaultValue, apceptValuesNotIncluded, canReenterSameState) { }

		#endregion

		#region SetState

		public void SetState<t>()
		{
			State = typeof(t);
		}

		public void SetState<t>(t instance)
		{
			State = instance.GetType();
		}

		public void SetState(Type type)
		{
			State = type;
		}

		#endregion

		#region Add State

		public void AddState<t>(Action<Type, Type> enterAction = default, Action<Type, Type> exitAction = default, Action updateAction = default)
		{
			base.AddState(typeof(t), enterAction, exitAction, updateAction);
		}

		public void AddState<t>(t type, Action<Type, Type> enterAction = default, Action<Type, Type> exitAction = default, Action updateAction = default)
		{
			base.AddState(type.GetType(), enterAction, exitAction, updateAction);
		}

		#endregion

		#region Remove State

		public void RemoveState<t>()
		{
			base.RemoveState(typeof(t));
		}

		public void RemoveState<t>(t type)
		{
			base.RemoveState(type.GetType());
		}

		#endregion

		#region Default State

		public void SetDefaultState<t>()
		{
			base.SetDefaultState(typeof(t));
		}

		public void SetDefaultState<t>(t type)
		{
			base.SetDefaultState(type.GetType());
		}

		#endregion

	}
}

