// Copyright (C) 2021 SansDevs.

using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int _dotsNumber;
    [SerializeField] Transform _dotsParent;
    [SerializeField] GameObject _dotPrefab;
    [SerializeField] float _dotSpacing;
    [SerializeField] float _gravityScale;
    [SerializeField] [Range(0.01f, 0.3f)] float _dotMinScale;
    [SerializeField] [Range(0.3f, 1f)] float _dotMaxScale;

    private Transform[] _dotsList;

    private Vector2 pos;

    //dot pos
    private float timeStamp;

    private void Start()
    {
        Hide();

        PrepareDots();
    }

    public void UpdateDots(Vector3 ballPos, Vector2 forceApplied)
    {
        timeStamp = _dotSpacing;
        for (int i = 0; i < _dotsNumber; i++)
        {
            pos = (Vector2)ballPos + (forceApplied * timeStamp) + (.5f * _gravityScale * timeStamp * timeStamp * Physics2D.gravity);

            _dotsList[i].position = pos;
            timeStamp += _dotSpacing;
        }
    }

    public Vector2 PointPosition(Vector2 startingPos, Vector2 direction, float launchForce, float t)
    {
        Vector2 position = startingPos + (direction * launchForce * t) + (.5f * (Physics2D.gravity * (t * t)));
        return position;
    }

    public void Show()
    {
        _dotsParent.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _dotsParent.gameObject.SetActive(false);
    }

    private void PrepareDots()
    {
        _dotsList = new Transform[_dotsNumber];
        _dotPrefab.transform.localScale = Vector3.one * _dotMaxScale;

        float scale = _dotMaxScale;
        float scaleFactor = scale / _dotsNumber;

        for (int i = 0; i < _dotsNumber; i++)
        {
            _dotsList[i] = Instantiate(_dotPrefab, _dotsParent).transform;

            _dotsList[i].localScale = Vector3.one * scale;
            if (scale > _dotMinScale) scale -= scaleFactor;
        }
    }
}
