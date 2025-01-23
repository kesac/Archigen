namespace Archigen.Examples
{
    /// <summary>
    /// Example player that belongs to a <see cref="Team"/>.
    /// </summary>
    public class Player
    {
        public string Name { get; set; }
        public override string ToString() => this.Name;

    }
}
