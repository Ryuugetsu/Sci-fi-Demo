using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private CharacterController _characterController;
    [SerializeField]
    private float _speed = 3.5f;
    private float _gravity = 9.81f;


    [SerializeField]
    private GameObject _weapon;
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    [SerializeField]
    private int _currentAmmo;
    private int _maxAmmo = 50;
    private bool _isReloading = false;
    private UIManager _uiManager;

    public bool hasCoin;


	// Use this for initialization
	void Start ()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _currentAmmo = _maxAmmo;

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        hasCoin = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CalculateMovement();

        //esconder o cursor do mouse e trava-lo no centro da tela do jogo
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        //Start Coroutine Reload
        if (Input.GetKeyDown(KeyCode.R) && _isReloading == false)
        {
            _isReloading = true;
            StartCoroutine(Reload());
        }

        //atrirar
        if (Input.GetMouseButton(0) && _currentAmmo > 0)
        {
            Shoot();
        }
        else
        {
            _muzzleFlash.SetActive(false);
        }
                       
	}

    private void Shoot()
    {
        _muzzleFlash.SetActive(true);
        _currentAmmo--;
        _uiManager.UpdateAmmo(_currentAmmo);

        //atirar o ray trace apartir do centro da tela
        Ray originRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(originRay, out hitInfo))
        {
            Debug.Log("raytrace hit " + hitInfo.transform.name);
            GameObject hitMarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(hitMarker, 5f);

            //destroi objetos como caixas e garrafas
            

            Destroyer destroyer = hitInfo.transform.GetComponent<Destroyer>();
            if(destroyer != null)
            {
                destroyer.DestroyCrate();
                
            }
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        _currentAmmo = _maxAmmo;
        _isReloading = false;
        _uiManager.UpdateAmmo(_currentAmmo);
    }

    private void CalculateMovement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

        //reatribui o conteudo da variave velocity convertido de local space to world space
        velocity = transform.transform.TransformDirection(velocity);

        _characterController.Move(velocity * Time.deltaTime);
    }

    public void _enableWeapon()
    {
        _weapon.SetActive(true);
    }

}
