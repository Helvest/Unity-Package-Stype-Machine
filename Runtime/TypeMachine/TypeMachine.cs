using System;

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

		public void SetState<t>(t type)
		{
			State = type.GetType();
		}

		#endregion

		#region Add State

		public void AddState<t>(Action enterAction = default, Action exitAction = default, Action updateAction = default)
		{
			base.AddState(typeof(t), enterAction, exitAction, updateAction);
		}

		public void AddState<t>(t type, Action enterAction = default, Action exitAction = default, Action updateAction = default)
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

