public class PlayerWinner
{
    public string PlayerName;
    public double winAmount;
    public string winningType;
    public string dateOfWin;

    public PlayerWinner(string PlayerName, double winAmount, string winningType,string dateOfWin)
    {
        this.PlayerName = PlayerName;
        this.winAmount = winAmount;
        this.winningType = winningType;
        this.dateOfWin = dateOfWin ;
    }
}