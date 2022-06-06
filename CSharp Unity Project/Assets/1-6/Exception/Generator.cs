using System;
using UnityEngine;

/// <summary>
/// �v���n�u�� GameObnect �Ƃ��ďo���|�C���g�ɐ�������
/// </summary>
public class Generator : MonoBehaviour
{
    /// <summary>�o���|�C���g</summary>
    [SerializeField] Transform[] _spawnPoints;
    /// <summary>�o��������v���n�u</summary>
    [SerializeField] GameObject[] _prefabs;

    void Start()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            var prefab = _prefabs[i % _prefabs.Length]; // �o��������v���n�u���擾����
            var spawnPoint = _spawnPoints[i];   // �o���|�C���g���擾����
            // �v���n�u���� GameObject �𐶐�����i��O���X���[����\�������鏈���j
            var go = Instantiate(prefab);
            // �������� GameObject ���o���|�C���g�Ɉړ�����i��O���X���[����\�������鏈���j
            go.transform.position = spawnPoint.position;
        }
    }
}
