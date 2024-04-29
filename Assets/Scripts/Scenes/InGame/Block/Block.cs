using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;
namespace Scenes.InGame.Block
{
    public class Block : MonoBehaviour, IDamagable
    {
        [Header("�u���b�N�̃p�����[�^")]
        [SerializeField,Tooltip("�u���b�N�̑ϋv�x")]
        private int _hp = 1;
        [Header("���̑�")]
        [SerializeField, Tooltip("�j�󎞂ɃX�|�[������I�u�W�F�N�g")]
        private GameObject _spawnObject;

        public void Break()
        {
            Instantiate(_spawnObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        public void Damange(int damage)
        {
            if (damage < 0) return;//�_���[�W�����̏ꍇ�͏�����Ԃ�
            _hp = _hp - damage;
            if(_hp <= 0)
            {
                Manager.InGameManager.Instance.BlockDestroy();
                Break();
            }
        }
    }
}