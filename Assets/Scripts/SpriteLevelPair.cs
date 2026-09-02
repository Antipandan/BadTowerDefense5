using UnityEngine;


    [System.Serializable]
    public sealed class SpriteLevelPair
    {
        [SerializeField] private Sprite inGameSprite;
        [SerializeField] private Sprite levelSprite;
        
        public Sprite InGameSprite
        {
            get => inGameSprite;
        }

        public Sprite LevelSprite
        {
            get => levelSprite;
        }
    }
