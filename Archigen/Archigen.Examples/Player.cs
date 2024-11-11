namespace Archigen.Examples
{
    /// <summary>
    /// Nested POCO
    /// </summary>
    public class Player
    {
        public string PlayerName { get; set; }

        public override string ToString() => this.PlayerName;

    }
}
