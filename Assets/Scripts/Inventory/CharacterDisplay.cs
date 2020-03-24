
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    private const int LAYER_DEFAULT = 0; // Player
    private const int LAYER_DISPLAY = 12; // Display
    private const float TURN = 360;

    [SerializeField] private Transform _target;
    [SerializeField] private Transform _displayAnchor;
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _display;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private LayerMask _layer;
    private RaycastHit _hit;
    private Transform _pivot;
    private Vector2 _textureDimension;

    [SerializeField] private float _rotationSensitivity = 15;
    [SerializeField] private float _minRotationX = -TURN;
    [SerializeField] private float _maxRotationX = TURN;
    [SerializeField] private float _minRotationY = -30;
    [SerializeField] private float _maxRotationY = 30;

    private float _rotationX;
    private float _rotationY;
    private Quaternion _originalRotation;

    private bool _isRotating;
    private bool _mouseHovering;

    public Transform Target
    {
        set
        {
            if (_target != null)
                ChangeLayers(_target, LAYER_DEFAULT);
            _target = value;
            bool isNull = _target == null;
            _pivot.gameObject.SetActive(!isNull);
            _displayAnchor.SetParent(_target);
            _displayAnchor.localPosition = Vector3.zero;
            if (!isNull)
            {
                ChangeLayers(_target, LAYER_DISPLAY);
            }
        }
    }

    public bool IsDisplaying
    {
        get { return _canvas.enabled; }
        set { _canvas.enabled = value; }
    }

    public bool IsRotating
    {
        set { _isRotating = value; }
    }

    public bool MouseHovering
    {
        set { _mouseHovering = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        _pivot = _displayAnchor.GetChild(0);

        _textureDimension = new Vector2(_display.GetComponent<RawImage>().texture.width,
                                        _display.GetComponent<RawImage>().texture.height);
        _originalRotation = _pivot.localRotation;
        
        Target = _target;
        


        Debug.Log("Target -> " + _target);
    }

    // Update is called once per frame
    void Update()
    {
        GetTargetByTag("Player");
        
        //       if (InputManager.INVENTORY_DOWN)
        //            IsDisplaying = !IsDisplaying;

        if (IsDisplaying)
        {
            if (_mouseHovering)
            {
                Vector3 scaledSizeDelta = _display.sizeDelta * _canvas.scaleFactor;
                Vector3 bottomLeftCorner = new Vector3(_display.position.x - (scaledSizeDelta.x / 2),
                                                       _display.position.y - (scaledSizeDelta.y / 2));
                _hit = CastRay(_camera, bottomLeftCorner, scaledSizeDelta, _textureDimension, _layer);

            }
            if (_isRotating)
                RotatePivot(_pivot);
        }
    }

    private RaycastHit CastRay(Camera camera, Vector3 bottomLeftCorner, Vector3 sizeDelta, Vector2 textureDimensions, LayerMask layer)
    {
        Vector3 relativeMousePosition = Input.mousePosition - bottomLeftCorner;
        relativeMousePosition.x = (relativeMousePosition.x / sizeDelta.x) * textureDimensions.x;
        relativeMousePosition.y = (relativeMousePosition.y / sizeDelta.y) * textureDimensions.y;

        RaycastHit hit;

        Ray ray = camera.ScreenPointToRay(relativeMousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
 //           Debug.DrawLine(camera.transform.position, hit.point, Color.blue);
 //           marker.position = hit.point;
        }
        return hit;
    }


    private void RotatePivot(Transform pivot)
    {
        _rotationX += Input.GetAxis("Mouse X") * _rotationSensitivity;
        _rotationY += Input.GetAxis("Mouse Y") * _rotationSensitivity;
        _rotationX = ClampAngle(_rotationX, _minRotationX, _maxRotationX);
        _rotationY = ClampAngle(_rotationY, _minRotationY, _maxRotationY);

        Quaternion xQuaternion = Quaternion.AngleAxis(_rotationX, Vector2.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(_rotationY, Vector2.left);

        pivot.localRotation = _originalRotation * xQuaternion * yQuaternion;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle > TURN)
            angle -= TURN;
        if (angle < -TURN)
            angle += TURN;
        return Mathf.Clamp(angle, min, max);
    }

    public static void ChangeLayers(Transform target, int layer)
    {
        target.gameObject.layer = layer;
        foreach (Transform child in target)
        {
            ChangeLayers(child, layer);
        }
    }

    void ChangeTarget(Transform target)
    {
        _target = target;
    }
    void GetTargetByTag(string _tag)
    {
        GameObject obj = GameObject.FindGameObjectWithTag(_tag);
        if (obj)
        {
            ChangeTarget(obj.transform);
        }
        else
        {
 //           Debug.Log("Cant find object with tag " + _tag);
        }
    }
}
