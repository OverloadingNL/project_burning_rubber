using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
	public GameObject prefab;
	public int amount;
	public bool expandable;
}

public class Pool : MonoBehaviour
{
	public static Pool singleton;
	public List<PoolItem> items;
	public List<GameObject> pooledItems;

	private void Awake()
	{
		singleton = this;
		pooledItems = new List<GameObject>();
		foreach (PoolItem item in items)
		{
			for (int i = 0; i < item.amount; i++)
			{
				GameObject obj = Instantiate(item.prefab);
				obj.SetActive(false);
				pooledItems.Add(obj);
			}

		}
	}

	public GameObject GetRandom(int index, bool useIndex, int prevIndex)
	{
		Utils.Shuffle(pooledItems);

		for (int i = 0; i < pooledItems.Count; i++)
		{
			if (!pooledItems[i].activeInHierarchy)
			{
                Platform settings = pooledItems[i].GetComponent<Platform>();

                if (useIndex && (settings.startIndex != index || settings.endIndex != index))
                {
                    continue;
                }
                if (settings.startIndex != prevIndex)
                {
                    continue;
                }
				return pooledItems[i];
			}
		}

		foreach (var item in items)
		{
			if (item.expandable)
			{
				GameObject obj = Instantiate(item.prefab);
				obj.SetActive(false);
				pooledItems.Add(obj);
				return obj;
			}
		}

		return null;
	}
}

public static class Utils
{
	public static System.Random r = new System.Random();
	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = r.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}