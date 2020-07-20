using System.Collections.Generic;

/// <summary>
/// Constant data for coin store items.
/// </summary>
public static class CoinList
{
    public class Coin
    {
        public Coin(int amount, float price, string productId)
        {
            Amount = amount;
            Price = price;
            ProductId = productId;
        }

        public int Amount { get; }

        public float Price { get; }

        public string ProductId { get; }
    }

    public static readonly Coin FiveCoins = new Coin(5, 0.99f, "five_coins");
    public static readonly Coin TenCoins = new Coin(10, 1.99f, "ten_coins");
    public static readonly Coin TwentyCoins = new Coin(20, 2.49f, "twenty_coins");
    public static readonly Coin FiftyCoins = new Coin(50, 4.99f, "fifty_coins");
    public static readonly List<Coin> List = new List<Coin>() {FiveCoins, TenCoins, TwentyCoins, FiftyCoins};
}