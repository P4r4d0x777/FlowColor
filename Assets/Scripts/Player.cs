using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject _obstacle;
    public GameObject _colorSwitch;

    public string _currentColor;

    public float _jumpForce = 9f;

    public Rigidbody2D _circle;

    public SpriteRenderer _spriteRenderer;

    public Color _blue;
    public Color _yellow;
    public Color _pink;
    public Color _purple;

    public static int _score = 0;
    public Text _scoreText;
    private void Awake()
    {
        _circle.isKinematic = true;
        SetRandomColor();
        _scoreText.text = _score.ToString();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            _circle.isKinematic = false;
            _circle.velocity = Vector2.up * _jumpForce;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Scored")
        {
            _score++;
            _scoreText.text = _score.ToString();
            collision.gameObject.SetActive(false);

            Instantiate(_obstacle, new Vector2(0f, transform.position.y + 7f), _obstacle.transform.rotation);
            Instantiate(_colorSwitch, new Vector2(0f, transform.position.y + 10f), Quaternion.identity);
            return;
        }
        if (collision.gameObject.tag == "ColorChanger")
        {
            SetRandomColor();
            Destroy(collision.gameObject);
            return;
        }
        if(collision.gameObject.tag != _currentColor)
        {
            Debug.Log("Death");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _score = 0;
        }
    }

    void SetRandomColor()
    {
        int randomize = Random.Range(0, 3);

        switch(randomize)
        {
            case 0: _currentColor = "Blue";_spriteRenderer.color = _blue; break;
            case 1: _currentColor = "Yellow"; _spriteRenderer.color = _yellow; break;
            case 2: _currentColor = "Pink"; _spriteRenderer.color = _pink; break;
            case 3: _currentColor = "Purple"; _spriteRenderer.color = _purple; break;
        }
    }
}
