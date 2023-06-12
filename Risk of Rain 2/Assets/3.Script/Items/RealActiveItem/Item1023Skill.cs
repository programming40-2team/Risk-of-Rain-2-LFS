using System.Collections;
using UnityEngine;

public class Item1023Skill : PrimitiveActiveItem
{
    public float radius = 5f; // 플레이어와의 일정 거리
    public float rotationSpeed = 50f; // 회전 속도

    private Transform playerTransform;


    private void Start()
    {
        Init();
        radius = Random.Range(5, 11);
        StartCoroutine(nameof(StartFire_co));
        Managers.Resource.Destroy(gameObject, 30f);
    }

    public override void Init()
    {
        base.Init();

        playerTransform = Player.transform;

    }

    private void Update()
    {
        RotateAroundPlayer();
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(StartFire_co));
    }
    private void RotateAroundPlayer()
    {
        transform.RotateAround(playerTransform.position, Vector3.up, rotationSpeed * Time.deltaTime);

        // 플레이어와의 거리를 일정하게 유지
        Vector3 directionToPlayer = transform.position - playerTransform.position;
        float distanceToPlayer = directionToPlayer.magnitude;


        //회전 반경을 벗어날 경우  같은 방향을 유지한 반경으로 옮겨줌
        if (distanceToPlayer > radius)
        {
            Vector3 targetPosition = playerTransform.position + directionToPlayer.normalized * radius;
            targetPosition.y += 0.015f;
            transform.position = targetPosition;
        }
    }

    private IEnumerator StartFire_co()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject go = Managers.Resource.Instantiate("TurretBullet");
                go.transform.position = gameObject.transform.position;
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(2f);
        }


    }
}
