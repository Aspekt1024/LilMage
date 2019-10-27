namespace LilMage.Units
{
    public enum CastResult
    {
        /// <summary>
        /// The response from requesting an ability to be cast, used with <see cref="IAbilitiesComponent"/>
        /// </summary>
        Success,
        ErrorNotEnoughMana,
        ErrorNoTarget,
        ErrorInvalidTarget,
        ErrorAlreadyCasting,
        ErrorMoving,
        ErrorNotFound,
    }
}