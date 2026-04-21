using UnityEngine;

public class MonsterTimer : MonoBehaviour
{
    [SerializeField] private float MonsterWaitMax = 50f;
    [SerializeField] private float MonsterPaticenceDrain = 4f;
    [SerializeField] private float CurrentMonsterTime;
    [SerializeField] MonsterDetect MonsterA;

    private void Start()
    {
        CurrentMonsterTime = MonsterWaitMax;
    }

    private void Update()
    {
        HandleMonsterPaticence();
    }
    public void HandleMonsterPaticence()
    {
        CurrentMonsterTime -= MonsterPaticenceDrain * Time.deltaTime;

        if(CurrentMonsterTime <= 0)
        {
            MonsterA.playAnimation();
            CurrentMonsterTime = MonsterWaitMax;
        }
    }
}
