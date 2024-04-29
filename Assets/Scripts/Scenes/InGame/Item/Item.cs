using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Scenes.InGame.Manager;

namespace Scenes.InGame.Item
{
    public class Item : MonoBehaviour
    {
        [Header("アイテムのパラメータ")]
        [SerializeField, Tooltip("落下速度")] private float _power = 0f;
        [SerializeField, Tooltip("得点")] private int _score = 0;

        private Rigidbody2D _rigidbody2d;
        private Vector2 _prevVelocity;

        void Start()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();

            _rigidbody2d.AddForce(Vector2.down * _power, ForceMode2D.Impulse);

            InGameManager.Instance.OnPause
                .Subscribe(_ => Pause())
                .AddTo(this);

            InGameManager.Instance.OnRestart
                .Subscribe(_ => Restart())
                .AddTo(this);

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(x => judgeObject(x));
        }

        private void judgeObject(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                InGameManager.Instance.ChangeScore(_score);
                Destroy(this.gameObject);
            }
            else if (collider.gameObject.CompareTag("DeadFrame"))
            {
                Destroy(this.gameObject);
            }
        }

        private void Pause()
        {
            _prevVelocity = _rigidbody2d.velocity;
            _rigidbody2d.velocity = Vector2.zero;
        }

        private void Restart()
        {
            _rigidbody2d.AddForce(_prevVelocity.normalized * _power, ForceMode2D.Impulse);
        }
    }
}
