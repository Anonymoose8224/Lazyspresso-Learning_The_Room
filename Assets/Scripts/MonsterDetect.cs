using UnityEngine;

public class MonsterDetect : MonoBehaviour
{
    [SerializeField] Animation anim;
    [SerializeField] PlayerHead player;
    private void OnTriggerEnter(Collider other)
    {
        if (player.CompareTag("Player"))
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
                anim.Play("NewMonsterMove");
                break;
            case 1:
                anim["NewMonsterMove"].speed = -1.0f;
                anim["NewMonsterMove"].time = anim["NewMonsterMove"].length;
                anim.Play("NewMonsterMove");
                break;
            default:
                Debug.Log("Your Lucky");
                break;
        }

    }


}
