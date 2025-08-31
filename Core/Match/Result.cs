# nullable disable

namespace Core.Match
{
    public abstract class Output
    {
        
    }

    public class CharacterMoved : Output
    {
        public Position From;
        public Position To;
        public ICharacter Target;
    }
}