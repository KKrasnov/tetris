using UnityEngine;

namespace Tetris.Gameplay.Bricks.Components
{
    public class Brick : MonoBehaviour
    {
        private BrickData _data;

        public BrickData Data => _data;
        
        [HideInInspector]
        public Transform[] Blocks;

        public void Init(BrickData data, Transform[] blocks)
        {
            _data = data;
            Blocks = blocks;
            
            for(int i = 0; i < _data.Cells.Length; i++)
            {
                var block = Blocks[i];
                block.SetParent(this.transform);
                block.localPosition = _data.Cells[i];
            }
        }
        
        public void DoMove(Vector2 direction)
        {
            transform.Translate(direction.normalized);
        }

        public void DoRotate(float direction)
        {
            Debug.LogError("Not implemented!");
        }

        public void SetPosition(int x, int y)
        {
            this.transform.position = new Vector2(x, y);
        }

        public Vector2 GetPosition()
        {
            return this.transform.position;
        }

        public void Deinit()
        {
            foreach (var block in Blocks)
            {
                block.SetParent(null);
            }
            GameObject.Destroy(this.gameObject);
        }
    }
}