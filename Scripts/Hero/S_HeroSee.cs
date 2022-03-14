using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HeroSee : MonoBehaviour
{
    [SerializeField] private CircleCollider2D CircleRadiusSee;
    [SerializeField] private List<GameObject> ISeeIts = new List<GameObject>();

    public GameObject Target;

    private void Start()
    {
        StartCoroutine(UpdateListOfEnemy());
    }

    IEnumerator UpdateListOfEnemy()
    {
        while (true)
        {
            float minDistance = float.MaxValue;

            foreach (var item in ISeeIts)
            {
                float checkDistance = Vector2.Distance(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z), item.transform.position);

                item.GetComponent<SpriteRenderer>().color = Color.white;

                if (checkDistance < minDistance)
                {
                    minDistance = checkDistance;
                    Target = item;
                }
            }

            if (ISeeIts.Count == 0)
                Target = null;

            if (Target != null)
                Target.GetComponent<SpriteRenderer>().color = Color.red;

            yield return new WaitForSeconds(0.1f);
        }
    }
        private void RemoveEnemyFromList(GameObject Enemy) => ISeeIts.Remove(Enemy);


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            ISeeIts.Add(collision.gameObject);
            collision.GetComponent<S_moveEnemy>().event_DeadEnemy += RemoveEnemyFromList;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            ISeeIts.Remove(collision.gameObject);
            collision.GetComponent<S_moveEnemy>().event_DeadEnemy -= RemoveEnemyFromList;
        }
    }


}
