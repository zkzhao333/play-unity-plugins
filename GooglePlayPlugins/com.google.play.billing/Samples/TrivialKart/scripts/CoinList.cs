public static class CoinList
{
    public class Coin
    {
        public readonly int Amount;
        public readonly float Price;

        public Coin(int amount, float price)
        {
            Amount = amount;
            Price = price;
        }
    }

    public static readonly Coin FiveCoins = new Coin(5, 0.99f);
    public static readonly Coin TenCoins = new Coin(10, 1.99f);
    public static readonly Coin TwentyCoins = new Coin(20, 2.99f);
    public static readonly Coin FiftyCoins = new Coin(50, 4.99f);
}