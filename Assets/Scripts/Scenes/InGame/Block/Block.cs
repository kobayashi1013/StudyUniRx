using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;
namespace Scenes.InGame.Block
{
    public class Block : MonoBehaviour, IDamagable
    {
        [Header("ブロックのパラメータ")]
        [SerializeField,Tooltip("ブロックの耐久度")]
        private int _hp = 1;
        [Header("その他")]
        [SerializeField, Tooltip("破壊時にスポーンするオブジェクト")]
        private GameObject _spawnObject;

        public void Break()
        {
            Instantiate(_spawnObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        public void Damange(int damage)
        {
            if (damage < 0) return;//ダメージが負の場合は処理を返す
            _hp = _hp - damage;
            if(_hp <= 0)
            {
                Manager.InGameManager.Instance.BlockDestroy();
                Break();
            }
        }
    }
}