using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShatterAnimations : MonoBehaviour
{
    [SerializeField]
    private float _animationTime = 4;
    private float _animationTimeCurrent = 0;
    [SerializeField]
    private AnimationCurve _positionCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField]
    private AnimationCurve _scaleCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField]
    private AnimationCurve _rotationCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField]
    private float _distanceMultiplier = 1;
    [SerializeField]
    [Range(0f, 2f)]
    private float _rotationMultiplier = 1;
    [SerializeField]
    [Range(0f, 2f)]
    private float _scaleMultiplier = 1;
    [SerializeField]
    private bool _isAnimating = false;

    [Header("Auto-Filled after Init")]
    [SerializeField]
    private List<Transform> _children = new List<Transform>();
    [SerializeField]
    private List<Vector3> _positions = new List<Vector3>();
    [SerializeField]
    private List<Quaternion> _rotations = new List<Quaternion>();
    [SerializeField]
    private List<Renderer> _renderers = new List<Renderer>();

    public Action CreationAnimationFinished;

    [Button]
    public void Init() // may be manually called before game in editor to fill renderers
    {
        _children.Clear();
        _positions.Clear();
        _rotations.Clear();
        _renderers.Clear();

        int childCount = this.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = this.transform.GetChild(i);
            _children.Add(childTransform);
            _positions.Add(childTransform.position);
            _rotations.Add(childTransform.rotation);
            _renderers.Add(childTransform.GetComponent<Renderer>());
        }
    }

    public void SetMaterial(Material material)
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].sharedMaterial = material;
        }
    }

    [Button]
    public void StartCreationAnimation()
    {
        this.gameObject.SetActive(true);
        _animationTimeCurrent = 0;
        _isAnimating = true;

        UpdatePositions();
    }

    private void FinishCreationAnimation()
    {
        _isAnimating = false;
        this.gameObject.SetActive(false);
        CreationAnimationFinished?.Invoke();
    }

    private void UpdatePositions()
    {
        float progress = _animationTimeCurrent / _animationTime;
        float positionProgress = _positionCurve.Evaluate(Mathf.Clamp(progress, 0f, 1f));
        float scaleProgress = _scaleCurve.Evaluate(Mathf.Clamp(progress, 0f, 1f));
        float rotationProgress = _rotationCurve.Evaluate(Mathf.Clamp(progress, 0f, 1f));

        Vector3 centerPosition = this.transform.position;
        
        for (int i = 0; i < _children.Count; i++)
        {
            Vector3 offset = (centerPosition - _positions[i]).normalized * _distanceMultiplier;
            Quaternion rotationAngle = Quaternion.Euler(offset.normalized * 90 * _rotationMultiplier);
            _children[i].transform.position = Vector3.Lerp(_positions[i] - offset, _positions[i], positionProgress);
            _children[i].transform.localScale = Vector3.Lerp(Vector3.one - Vector3.one * _scaleMultiplier, Vector3.one, scaleProgress);
            _children[i].transform.localRotation = Quaternion.Lerp(_rotations[i] * rotationAngle, _rotations[i], rotationProgress);
        }
    }

    private void Update()
    {
        if (_isAnimating)
        {
            _animationTimeCurrent += Time.deltaTime;
            UpdatePositions();
            if (_animationTimeCurrent >= _animationTime)
            {
                FinishCreationAnimation();
            }
        }
    }


}
