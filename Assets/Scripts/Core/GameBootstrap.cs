using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private Family startingFamily;

    void Start()
    {
        GameManager.Instance.SetActiveFamily(startingFamily);
    }
}
