using UnityEngine;

public class MonsterDetect : MonoBehaviour
{
    [SerializeField] Animation anim;
    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Player"))
            Debug.Log("Player Has enter the monster range");

        else
            Debug.Log("Nothing to detect");
    }

    public void playAnimation()
    {
        int rand = Random.Range(0,2);

        switch (rand)
        {
            case 0:
                anim.Play("MonsterMove");
                break;
            case 1:
                anim["MonsterMove"].speed = -1.0f;
                anim["MonsterMove"].time = anim["MonsterMove"].length;
                anim.Play("MonsterMove");
                break;
            default:
                Debug.Log("Your Lucky");
                break;
        }

    }


}
