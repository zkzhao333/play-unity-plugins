public static class CoinList
{
    public class Coin
    {
        public Coin(int amount, float price)
        {
            Amount = amount;
            Price = price;
        }

        public int Amount { get; }

        public float Price { get; }
    }

    public static readonly Coin FiveCoins = new Coin(5, 0.99f);
    public static readonly Coin TenCoins = new Coin(10, 1.99f);
    public static readonly Coin TwentyCoins = new Coin(20, 2.99f);
    public static readonly Coin FiftyCoins = new Coin(50, 4.99f);
}