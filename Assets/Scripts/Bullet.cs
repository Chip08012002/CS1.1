
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectWeHit)
    {
        if (objectWeHit.gameObject.CompareTag("Target"))
        {
            CreateBulletEffect(objectWeHit);
            Destroy(gameObject);
        }


        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            CreateBulletEffect(objectWeHit);
            Destroy(gameObject);
        }
    }

    void CreateBulletEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GobalReferences.Instance.bulletEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
