using LilMage.Units;

namespace LilMage
{
    /// <summary>
    /// A unit that can be controller by some controller, e.g. a <see cref="PlayerController"/>
    /// </summary>
    public interface IControllableUnit : IUnit
    {
        IController CurrentController { get; }

        void PossessByController(IController controller);
    }
}