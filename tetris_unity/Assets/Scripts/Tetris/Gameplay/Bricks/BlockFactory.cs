using Tools;
using UnityEngine;

namespace Tetris.Gameplay.Bricks
{
    public class BlockFactory : IFactory<Transform>
    {
        private readonly GameObject _blockPrefab;

        public BlockFactory(GameObject blockPrefab)
        {
            _blockPrefab = blockPrefab;
        }
        
        public Transform GetNext()
        {
            return GameObject.Instantiate(_blockPrefab).transform;
        }
    }
}