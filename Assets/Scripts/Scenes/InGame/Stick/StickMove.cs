using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Scenes.InGame.Stick
{
    [RequireComponent(typeof(StickStatus),typeof(Rigidbody2D))]
    public class StickMove : MonoBehaviour
    {
        private StickStatus _stickStatus;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _velocity;
        private Vector2 _moveVelocity;
        private const int CORRECTIONVALUE = 10;//���l�𒲐����邽�߂̕␳�l�ł�
        void Start()
        {
            _stickStatus = GetComponent<StickStatus>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            //�l�̕ύX���X�g���[���ŊĎ�
            this.ObserveEveryValueChanged(x => x._stickStatus.IsMovable)
                .Where(x => x == false)
                .Subscribe(_ => _rigidbody2D.velocity = Vector2.zero);

            this.ObserveEveryValueChanged(x => x._velocity)
                .Subscribe(_ => _moveVelocity = _velocity * _stickStatus.MoveSpeed);
        }

        void FixedUpdate()
        {
            _velocity = Vector2.zero;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _velocity.x--;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _velocity.x++;
            }

            _rigidbody2D.velocity = _moveVelocity * Time.fixedDeltaTime * CORRECTIONVALUE;
        }
    }
}