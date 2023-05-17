﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;
using Object = UnityEngine.Object;

public class ResourceManager
{
	Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();

	public T Load<T>(string key) where T : Object
	{
		if (_resources.TryGetValue(key, out Object resource))
			return resource as T;

		return null;
	}

	public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
	{
		GameObject prefab = Load<GameObject>($"{key}");
		if (prefab == null)
		{
			Debug.Log($"Failed to load prefab : {key}");
			return null;
		}
		GameObject go = Object.Instantiate(prefab, parent);
		go.name = prefab.name;
		return go;
	}

	public void Destroy(GameObject go)
	{
		if (go == null)
			return;

		Object.Destroy(go);
	}

	#region 어드레서블
	public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
	{
		// 확인.
		if (_resources.TryGetValue(key, out Object resource))
		{
			callback?.Invoke(resource as T);
			return;
		}

		string loadKey = key;
		if (key.Contains(".sprite"))
			loadKey = $"{key}[{key.Replace(".sprite", "")}]";

		// 리소스 비동기 로딩 시작.
		var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
		asyncOperation.Completed += (op) =>
		{
			_resources.Add(key, op.Result);
			callback?.Invoke(op.Result);
		};
	}

	public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
	{
		var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
		opHandle.Completed += (op) =>
		{
			int loadCount = 0;
			int totalCount = op.Result.Count;

			foreach (var result in op.Result)
			{
				LoadAsync<T>(result.PrimaryKey, (obj) =>
				{
					loadCount++;
					callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
				});
			}
		};
	}
	#endregion
}