namespace MonoGame_Pikachu.Interface
{
    public interface IPlayerMovementInputService
    {
        public bool ShouldGoRight();

        public bool ShouldGoLeft();

        public bool ShouldGoUp();

        public bool ShouldGoDown();
    }
}
