using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseController : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController; // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    private void Start()
    {
        // If we haven't set up the Unity Purchasing reference.
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing.
            InitializePurchasing();
        }
    }

    private void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of google play store.
        var builder = ConfigurationBuilder.Instance(Google.Play.Billing.GooglePlayStoreModule.Instance());

        // Add consumable products (coins) to sell / restore by way of its identifier,
        // associating the general identifier with its store-specific identifiers.
        foreach (var coin in CoinList.List)
        {
            builder.AddProduct(coin.ProductId, ProductType.Consumable);
        }

        // Continue adding the non-consumable products (car).
        foreach (var car in CarList.List.Where(car => car.IsPriceInDollar && car.Price > 0))
        {
            builder.AddProduct(car.ProductId, ProductType.NonConsumable);
        }
        // TODO: subscription IAP.

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }

    private static bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public static void BuyProductId(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            var product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously. 
               m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log(
                    "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
        // Check if a consumable (coins) has been purchased by this user
        foreach (var coin in CoinList.List)
        {
            if (!String.Equals(args.purchasedProduct.definition.id, coin.ProductId, StringComparison.Ordinal)) continue;
            GameDataController.GetGameData().IncreaseCoinsOwned(coin.Amount);
            return PurchaseProcessingResult.Complete;
        }

        // Check if a non-consumable (car) has been purchased by this user
        foreach (var car in CarList.List)
        {
            if (!String.Equals(args.purchasedProduct.definition.id, car.ProductId,
                StringComparison.Ordinal)) continue;
            GameDataController.GetGameData().PurchaseCar(car);
            return PurchaseProcessingResult.Complete;
        }
        

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(
            $"OnPurchaseFailed: FAIL. Product: '{product.definition.storeSpecificId}', PurchaseFailureReason: {failureReason}");
    }
}