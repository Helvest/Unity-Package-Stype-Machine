using System.Collections.Generic;
using StypeMachine;
using UnityEngine;

[DefaultExecutionOrder(-9999)]
public class MonoFlypeMachine : FlypeMachineController<MonoBehaviour>
{

	#region Fields

	[Header("MonoTypeMachine")]

	[SerializeField]
	protected List<MonoBehaviour> states = default;

	[Header("Prefabs")]
	[SerializeField]
	private Transform _transformParentForPrefabs = default;

	[SerializeField]
	protected List<MonoBehaviour> statesPrefab = default;

	#endregion

	#region CreateTypeMachine

	protected override void CreateFlypeMachine()
	{
		base.CreateFlypeMachine();

		foreach (var state in states)
		{
			if (state != null)
			{
				FlypeMachine.AddState(
					state,
					enterAction: (Flag) => state.gameObject.SetActive(true),
					exitAction: (Flag) => state.gameObject.SetActive(false)
				);

				state.gameObject.SetActive(false);
			}
		}

		foreach (var state in statesPrefab)
		{
			if (state == null)
			{
				continue;
			}

			var prefab = state;
			MonoBehaviour instance = null;

			FlypeMachine.AddState(state,
				enterAction: (P) =>
				{
					if (instance != null)
					{
						instance.gameObject.SetActive(true);
					}
					else if (prefab != null)
					{
						instance = Instantiate(prefab, _transformParentForPrefabs);
					}
				},
				exitAction: (P) =>
				{
					if (instance != null)
					{
						Destroy(instance.gameObject);
					}
				}
			);
		}
	}

	#endregion

}
