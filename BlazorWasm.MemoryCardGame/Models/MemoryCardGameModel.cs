namespace MemoryCardGame.Models;

public class MemoryCardGameModel
{
    public int Id { get; set; }
    public string Image { get; set; }
    public string Value { get; set; }
    public bool IsSelected { get; set; }
    public bool IsMatched { get; set; }
    public string CssFlipped { get { return IsSelected ? "flipped" : string.Empty; } }
    public string CssSelected => IsSelected || IsMatched ? "selected" : string.Empty;
}