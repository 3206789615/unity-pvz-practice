using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpawnState
{
    NotStart,
    Spawning,
    End
}
public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance { get; private set; }

    private SpawnState spawnState = SpawnState.NotStart;

    public Transform[] spawnPointList;
    public GameObject ZombiePrefab;
    public GameObject ConeheadZombie;
    public GameObject BucketheadZombie;
    public FinalWaveUI finalWaveUI;
    private List<Zombie> zombies = new List<Zombie>();
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //StartSpawn();
    }

    private void Update()
    {
        Debug.Log("½©Ê¬ÊýÁ¿£º"+zombies.Count);
        if (spawnState==SpawnState.End&&zombies.Count==0)
        {           
            GameManager.Instance.GameEndSuccess();
        }
    }
    public void StartSpawn()
    {
        spawnState = SpawnState.Spawning;
        StartCoroutine(SpawnZombie());
    }

    public void Pause()
    {
        spawnState = SpawnState.End;
        foreach (Zombie zombie in zombies)
        {
            if (zombie == null) continue;
            zombie.TransitionToPause();
        }
    }

    IEnumerator SpawnZombie()
    {
        int level =50;
        yield return new WaitForSeconds(21);
        AudioManager.Instance.Play("awooga");
        //1²¨
        for (int i = 0; i <3; i++)
        {
            SpawnZombies(ZombiePrefab,i+level);
            yield return new WaitForSeconds(13);
        }
        yield return new WaitForSeconds(30);
        level -= 10;
        //2²¨
        for (int i = 0; i < 10; i++)
        {
            if (i<=7)
            {
                SpawnZombies(ZombiePrefab,i+level);
            }
            else
            {
                SpawnZombies(ConeheadZombie,i+level);
            }
            yield return new WaitForSeconds(8);
        }
        yield return new WaitForSeconds(30);
        level -= 10;
        //3²¨
        for (int i = 0; i < 30; i++)
        {
            int r = Random.Range(0, 9);

            if (r <=6)
                SpawnZombies(ZombiePrefab,i+level);
            else if(r==7)
                SpawnZombies(ConeheadZombie,i+level);
            else
                SpawnZombies(BucketheadZombie,i+level);
            yield return new WaitForSeconds(2);

        }
        yield return new WaitForSeconds(40);
        AudioManager.Instance.Play("finalwave");
        level -= 10;
        //4²¨
        for (int i = 0; i < 50; i++)
        {
            int r = Random.Range(0, 7);

            if (r <= 4)
                SpawnZombies(ZombiePrefab, i + level);
            else if (r == 5)
                SpawnZombies(ConeheadZombie, i + level);
            else
                SpawnZombies(BucketheadZombie, i + level);
            yield return new WaitForSeconds(1);

        }
        yield return new WaitForSeconds(47);
        finalWaveUI.FWShow();
        AudioManager.Instance.Play("hugewave");
        yield return new WaitForSeconds(3);
        AudioManager.Instance.StopBGM("bgm1");
        AudioManager.Instance.PlayBGM("bgm2");
        level -= 10;
        //5²¨
        for (int i = 0; i < 100; i++)
        {
            int r = Random.Range(0, 5);

            if (r <= 2)
                SpawnZombies(ZombiePrefab, i + level);
            else if (r == 3)
                SpawnZombies(ConeheadZombie, i + level);
            else
                SpawnZombies(BucketheadZombie, i + level);
            yield return new WaitForSeconds(1f);

        }
        Debug.Log(spawnState);
        spawnState = SpawnState.End;
    }
    private void SpawnZombies(GameObject zombiePrefab,int i)
    {
        if (spawnState == SpawnState.Spawning)
        {
            int index = Random.Range(0, spawnPointList.Length);
            Vector3 pos = spawnPointList[index].position;
            pos.z = i + (spawnPointList.Length - index) * 100;
            GameObject go=GameObject.Instantiate(zombiePrefab, spawnPointList[index].position, Quaternion.identity);
            zombies.Add(go.GetComponent<Zombie>());           
        }        
    }

    public void RemoveZombie(Zombie zombie)
    {
        zombies.Remove(zombie);
    }
}
