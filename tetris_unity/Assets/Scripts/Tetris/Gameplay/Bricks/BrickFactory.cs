using System;
using System.Collections.Generic;
using Tetris.Gameplay.Bricks.Components;
using Tetris.LevelManagement;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tetris.Gameplay.Bricks
{
    public class BrickFactory : IFactory<Brick>, IObserver<LevelData>
    {
        private readonly GameObject _brickPrefab;
        private readonly IFactory<Transform> _blockFactory;
        
        private BrickData[] _brickPresets;

        public BrickFactory(GameObject brickPrefab, IFactory<Transform> blockFactory)
        {
            _brickPrefab = brickPrefab;
            _blockFactory = blockFactory;
        }

        public Brick GetNext()
        {
            var brick = GameObject.Instantiate(_brickPrefab).GetComponent<Brick>();

            var brickData = _brickPresets[Random.Range(0, _brickPresets.Length)];

            var blocks = new List<Transform>();
            for (int i = 0; i < brickData.Cells.Length; i++)
            {
                blocks.Add(_blockFactory.GetNext());
            }
            
            brick.Init(brickData, blocks.ToArray());

            return brick;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(LevelData value)
        {
            _brickPresets = value.BricksPresets;
        }
    }
}