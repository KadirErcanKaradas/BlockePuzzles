using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeColor : MonoBehaviour
{
    public List<GameObject> cubes = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            cubes.Add(other.gameObject);         
        }
    }
    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            GameObject current = cubes[i];
            current.transform.DOScale(new Vector3(current.transform.localScale.x / 2, current.transform.localScale.y, current.transform.localScale.z), 0.2f).OnComplete(() =>
            {
                current.transform.DOScale(new Vector3(current.transform.localScale.x*2, current.transform.localScale.y, current.transform.localScale.z), 0.2f);
            });
            cubes.RemoveAt(i);
        }
    }
}
