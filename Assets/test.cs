using Spine;
using Spine.Unity;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _archer;

    [SpineBone(dataField = "_archer")]
    [SerializeField] private string _boneName;

    private Bone _target;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _target = _archer.Skeleton.FindBone(_boneName);
    }

    private void Update()
    {
        var mousePosition = Input.mousePosition;
        var worldMousePosition = _camera.ScreenToWorldPoint(mousePosition);
        var skeletonSpacePoint = _archer.transform.InverseTransformPoint(worldMousePosition);
        skeletonSpacePoint.x *= _archer.Skeleton.ScaleX;
        skeletonSpacePoint.y *= _archer.Skeleton.ScaleY;
        _target.Rotation = Mathf.Clamp(-Vector2.Angle(_target.GetLocalPosition(), skeletonSpacePoint) + 60, -30, 35);

        if (Input.GetMouseButtonDown(1))
        {
            var track = _archer.AnimationState.AddAnimation(1, "attack_start", false, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _archer.AnimationState.AddAnimation(2, "attack_finish", false, 0);
        }

        if (Input.GetKey(KeyCode.R))
        {
            _archer.AnimationState.ClearTracks();
        }
    }
}
