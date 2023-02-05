using UnityEngine;

namespace _5_Scripts.Player
{
    public class PlayerSpriteHandler : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer faceSpriteRenderer;

        [SerializeField] public Sprite FaceSad;
        [SerializeField] public Sprite FaceHappy;
        [SerializeField] public Sprite FaceSunglasses;
        [SerializeField] public Sprite FaceSweaty;

        void SetFaceSad() => faceSpriteRenderer.sprite = FaceSad;
        void SetFaceHappy() => faceSpriteRenderer.sprite = FaceHappy;
        void SetFaceSunglasses() => faceSpriteRenderer.sprite = FaceSunglasses;
        void SetFaceSweaty() => faceSpriteRenderer.sprite = FaceSweaty;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
