using System;
using System.Collections.Generic;

namespace ExchangeSimulation
{
    public abstract class Order
    {
        public int OrderId { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public abstract void Execute();
    }

    public class LimitOrder : Order
    {
        public double LimitPrice { get; set; }

        public LimitOrder(int orderId, string symbol, int quantity, double limitPrice)
        {
            OrderId = orderId;
            Symbol = symbol;
            Quantity = quantity;
            LimitPrice = limitPrice;
        }

        public override void Execute()
        {
            Console.WriteLine($"Executing Limit Order {OrderId}: {Quantity} units of {Symbol} at {LimitPrice}.");
        }
    }

    public class MarketOrder : Order
    {
        public MarketOrder(int orderId, string symbol, int quantity)
        {
            OrderId = orderId;
            Symbol = symbol;
            Quantity = quantity;
            Price = 0; // Market orders do not have a price until execution
        }

        public override void Execute()
        {
            Console.WriteLine($"Executing Market Order {OrderId}: {Quantity} units of {Symbol} at market price.");
        }
    }

    public class Exchange
    {
        private List<Order> orderBook = new List<Order>();

        public void AddOrder(Order order)
        {
            orderBook.Add(order);
            Console.WriteLine($"Order {order.OrderId} added: {order.Quantity} units of {order.Symbol}");
        }


        public void ProcessOrders()
        {
            Console.WriteLine("\nProcessing Orders...");
            foreach (var order in orderBook)
            {
                order.Execute();
            }
        }

        public void GetOrdersBySymbol(string symbol)
        {
            Console.WriteLine($"\nOrders for {symbol}:");
            foreach (var order in orderBook)
            {
                if (order.Symbol == symbol)
                {
                    Console.WriteLine($"- Order {order.OrderId}: {order.Quantity} units at {order.Price}.");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create the exchange
            var exchange = new Exchange();

            // Create some orders
            var limitOrder1 = new LimitOrder(1, "AAPL", 100, 150.00);
            var marketOrder1 = new MarketOrder(2, "GOOG", 50);
            var limitOrder2 = new LimitOrder(3, "AAPL", 200, 148.50);
            var marketOrder2 = new MarketOrder(4, "GOOG", 30);

            // Add orders to the exchange
            exchange.AddOrder(limitOrder1);
            exchange.AddOrder(marketOrder1);
            exchange.AddOrder(limitOrder2);
            exchange.AddOrder(marketOrder2);

            // Process the orders (execute them)
            exchange.ProcessOrders();

            // Get all orders for a specific symbol (e.g., AAPL)
            exchange.GetOrdersBySymbol("AAPL");
        }
    }
}
