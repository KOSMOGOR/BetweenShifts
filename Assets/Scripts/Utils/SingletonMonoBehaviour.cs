using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T: SingletonMonoBehaviour<T>
{
    public static T I { get; private set; }

    void Awake() {
        if (I == null) I = (T)this;
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject.transform.root.gameObject);
        AwakeNew();
    }

    /// <summary>
    /// Use this instead of Awake()
    /// </summary>
    virtual protected void AwakeNew() {}
}
