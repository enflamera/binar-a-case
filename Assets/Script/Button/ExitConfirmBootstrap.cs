using UnityEngine;

public static class ExitConfirmBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        if (ExitConfirmManager.Instance == null)
        {
            var prefab = Resources.Load<GameObject>("ExitConfirmCanvas");
            if (prefab != null)
                Object.Instantiate(prefab);
            else
                Debug.LogWarning("ExitConfirmCanvas prefab tidak ditemukan di Resources/");
        }
    }
}