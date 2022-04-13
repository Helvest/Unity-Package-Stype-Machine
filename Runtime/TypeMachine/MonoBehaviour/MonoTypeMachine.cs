using System.Collections.Generic;
using UnityEngine;
using StypeMachine;

public class MonoTypeMachine : TypeMachineController<MonoBehaviour>
{

	#region Variables

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

	protected override void CreateTypeMachine()
	{
		base.CreateTypeMachine();

		foreach (var state in states)
		{
			if (state != null)
			{
				TypeMachine.AddState(state,
					enterAction: (P, N) => state.gameObject.SetActive(true),
					exitAction: (P, N) => state.gameObject.SetActive(false),
					updateAction: null
				);

				state.gameObject.SetActive(false);
			}
		}

		foreach (var state in statesPrefab)
		{
			if (state != null)
			{
				var prefab = state;
				MonoBehaviour instance = null;

				TypeMachine.AddState(state,
					enterAction: (P, N) =>
					{
						if (P == N && instance != null)
						{
							instance.gameObject.SetActive(true);
						}
						else if (prefab != null)
						{
							instance = Instantiate(prefab, _transformParentForPrefabs);
						}
					},
					exitAction: (P, N) =>
					{
						if (instance != null)
						{
							if (P == N)
							{
								instance.gameObject.SetActive(false);
							}
							else
							{
								Destroy(instance);
							}
						}
					}
				);
			}
		}
	}

	#endregion

}
