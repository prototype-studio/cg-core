#nullable disable
using System;

namespace Core.Match
{
    [Serializable]
    public struct CharacterData
    {
        public string Id;
        public Team Team;
        public Position[] Steps;
        public int MaxSteps;

        public CharacterData(string id, Team team, Position[] steps, int maxSteps)
        {
            Id = id;
            Team = team;
            Steps = steps;
            MaxSteps = maxSteps;
        }
    }

    public interface ICharacter
    {
        CharacterData Data { get; }
    }
}