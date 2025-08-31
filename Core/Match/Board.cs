namespace Core.Match
{
    public class Board
    {
        // 8x8 grid of tiles
        private readonly Tile[] tiles = new Tile[64];

        public Board(BoardData boardData)
        {
            for(int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new Tile();
            }
            if(!(boardData?.Characters is {Length: 64}))
            {
                return;
            }
            for(int i = 0; i < tiles.Length; i++)
            {
                SetCharacter(GetPosition(i), boardData.Characters[i]);
            }
        }
        
        private void SetCharacter(Position pos, ICharacter character)
        {
            var index = GetIndex(pos.X, pos.Y);
            tiles[index].Character = character;
        }

        
        public static int GetIndex(int x, int y)
        {
            return y * 8 + x;
        }

        public static Position GetPosition(int index)
        {
            return new Position()
            {
                X = index % 8,
                Y = index / 8
            };
        }
        
        #region API
        
        public CharacterMoved ExecuteStep(Position from, int stepIndex, int stepCount)
        {
            var fromIndex = GetIndex(from.X, from.Y);
            var character = tiles[fromIndex].Character;
            if (character == null) return null;

            var characterStep = character.Data.Steps[stepIndex];
            var toX = from.X + characterStep.X * stepCount;
            var toY = from.Y + characterStep.Y * stepCount;
            if (toX < 0 || toX >= 8 || toY < 0 || toY >= 8)
                return null; // Out of bounds
            
            var toIndex = GetIndex(toX, toY);
            var targetTile = tiles[toIndex];
            var capturedCharacter = targetTile.Character;
            targetTile.Character = character;
            tiles[fromIndex].Character = null;
            return new CharacterMoved()
            {
                From = from,
                To = new Position {X = toX, Y = toY},
                Target = capturedCharacter
            };
        }
        
        public int GetMaxStepCount(Position startPos, CharacterData characterData, int stepIndex)
        {
            var step = characterData.Steps[stepIndex];
            for (var i = 1; i <= characterData.MaxSteps; i++)
            {
                var newX = startPos.X + step.X * i;
                var newY = startPos.Y + step.Y * i;

                if (newX < 0 || newX >= 8 || newY < 0 || newY >= 8)
                    return i - 1;

                var targetTile = tiles[GetIndex(newX, newY)];
                if (targetTile.Character == null) continue;
                if (targetTile.Character.Data.Team != characterData.Team)
                    return i; // Can capture opposing piece
                return i - 1; // Blocked by own piece
            }
            return characterData.MaxSteps;
        }
        
        public void ClearBoard()
        {
            for (var i = 0; i < tiles.Length; i++)
            {
                tiles[i].Character = null;
            }
        }
        
        #endregion
    }
}