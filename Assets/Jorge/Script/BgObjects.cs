using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObjects : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private float _lengthX, _lengthY, _startPosX, _startPosY;
    [SerializeField] private float _bgSpeed = 0.2f;

    void Start()
    {
        _startPosX = transform.position.x;
        _startPosY = transform.position.y;
        _lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        _lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float tempX = (_player.position.x * (1 - _bgSpeed));
        float distX = _player.position.x * _bgSpeed;
        float tempY = (_player.position.y * (1 - _bgSpeed));
        float distY = _player.position.y * _bgSpeed;

        transform.position = new Vector2(_startPosX + distX, _startPosY + distY);

        if (tempX > _startPosX + _lengthX)
            _startPosX += _lengthX*2;
        else if (tempX < _startPosX - _lengthX)
            _startPosX -= _lengthX*2;

        if (tempY > _startPosY + _lengthY)
            _startPosY += _lengthY;
        else if (tempY < _startPosY - _lengthY)
            _startPosY -= _lengthY;
    }
}
