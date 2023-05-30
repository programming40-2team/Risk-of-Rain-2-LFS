using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    private MeshRenderer objectMesh;
    private Material mat;
    [SerializeField] Material highlightMaterial;

    [SerializeField] GameObject bossPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] ParticleSystem bossRazer;

    private void Awake()
    {
        //bossRazer = GetComponent<ParticleSystem>();
        objectMesh = GetComponent<MeshRenderer>();
        mat = objectMesh.material;
    }
    private void Start()
    {
        if (bossRazer.isPlaying)
        {
            bossRazer.Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Transform bossSpawnPoint = spawnPoint;

        if (other.CompareTag("Player"))
        {
            Debug.Log("입장");
            Highlight();

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("입력");
                Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);  //제단에서 e키를 누르면 보스가 소환될 지점
                BossRazer();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetHighlight();
        }
    }

    private void BossRazer()
    {
        if (!bossRazer.isPlaying)
        {
            bossRazer.Play();
        }
    }

    private void Highlight()
    {
        objectMesh.material = highlightMaterial;
    }

    private void ResetHighlight()
    {
        objectMesh.material = mat;
    }
}



