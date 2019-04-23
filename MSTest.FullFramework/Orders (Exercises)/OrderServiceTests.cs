using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductionCode.Orders;
using ProductionCode.Orders.Models;

namespace MSTest.FullFramework.Orders
{
    [TestClass]
    public class OrderServiceTests
    {
        [TestMethod]
        public void TestValidOrder_ThrowsWhenAmountIsNegative()
        {
            //
            // Arrange
            //
            var order = new Order();
            order.OrderId = 123456;
            order.Amount = -1;
            order.ClientId = 200;
            order.ClientName = "Bob's FoodMart";
            order.BookedDate = new DateTime(2019, 01, 12);
            order.CreatedDate = new DateTime(2019, 01, 01);
            order.CurrencyIsoCode = "USD";
            order.Adjustments.Add(new Adjustment(){Amount=-20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01)});

            var orderService = new OrderService();

            //
            // Act
            //
            var ex = Assert.ThrowsException<Exception>(() => orderService.ValidateOrder(order));

            //
            // Assert
            //
            Assert.AreEqual("Order amount cannot be negative", ex.Message);
        }

        [TestMethod]
        public void TestValidOrder_ThrowsWhenAmountWithAdjustmentsIsNegative()
        {
            //
            // Arrange
            //
            var order = new Order();
            order.OrderId = 123456;
            order.Amount = 100;
            order.ClientId = 200;
            order.ClientName = "Bob's FoodMart";
            order.BookedDate = new DateTime(2019, 01, 12);
            order.CreatedDate = new DateTime(2019, 01, 01);
            order.CurrencyIsoCode = "USD";
            order.Adjustments.Add(new Adjustment() {Amount = -120, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01)});

            var orderService = new OrderService();

            //
            // Act
            //
            var ex = Assert.ThrowsException<Exception>(() => orderService.ValidateOrder(order));

            //
            // Assert
            //
            Assert.AreEqual("Order adjustments make total amount negative", ex.Message);
        }

        [TestMethod]
        public void TestValidOrder_ThrowsWhenOrderIsDeleted()
        {
            //
            // Arrange
            //
            var order = new Order();
            order.OrderId = 123456;
            order.Amount = 100;
            order.ClientId = 200;
            order.ClientName = "Bob's FoodMart";
            order.BookedDate = new DateTime(2019, 01, 12);
            order.CreatedDate = new DateTime(2019, 01, 01);
            order.CurrencyIsoCode = "USD";
            order.Adjustments.Add(new Adjustment() { Amount = -20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01) });
            order.IsDeleted = true;

            var orderService = new OrderService();

            //
            // Act
            //
            var ex = Assert.ThrowsException<Exception>(() => orderService.ValidateOrder(order));

            //
            // Assert
            //
            Assert.AreEqual("Order has been deleted", ex.Message);

        }


        #region Project 5


        //[TestMethod]
        //public void TestOrder_AmountPaidIsZeroWhenZeroDollarPaymentsMade()
        //{
        //    //
        //    // Arrange
        //    //
        //    var order = new Order();
        //    order.OrderId = 123456;
        //    order.Amount = 100;
        //    order.ClientId = 200;
        //    order.ClientName = "Bob's FoodMart";
        //    order.BookedDate = new DateTime(2019, 01, 12);
        //    order.CreatedDate = new DateTime(2019, 01, 01);
        //    order.CurrencyIsoCode = "USD";
        //    order.Adjustments.Add(new Adjustment() { Amount = -20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01) });
        //    order.Invoices.Add(new Invoice() { Amount = 0, OrderId = 123456, InvoiceId = 1, CreatedDate = DateTime.Now });

        //    var orderService = new OrderService();

        //    //
        //    // Act
        //    //
        //    var payment = orderService.PayInvoice(order, 1);

        //    //
        //    // Assert
        //    //
        //    Assert.AreEqual(0, order.TotalPayments);
        //}


        //[TestMethod]
        //public void TestPayInvoice_CreatesPaymentOfInvoiceAmount()
        //{
        //    //
        //    // Arrange
        //    //
        //    var order = new Order();
        //    order.OrderId = 123456;
        //    order.Amount = 100;
        //    order.ClientId = 200;
        //    order.ClientName = "Bob's FoodMart";
        //    order.BookedDate = new DateTime(2019, 01, 12);
        //    order.CreatedDate = new DateTime(2019, 01, 01);
        //    order.CurrencyIsoCode = "USD";
        //    order.Adjustments.Add(new Adjustment() { Amount = -20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01) });
        //    order.Invoices.Add(new Invoice() { Amount = 10, OrderId = 123456, InvoiceId = 1, CreatedDate = DateTime.Now });

        //    var orderService = new OrderService();

        //    //
        //    // Act
        //    //
        //    var payment = orderService.PayInvoice(order, 1);

        //    //
        //    // Assert
        //    //
        //    Assert.AreEqual(100, payment.Amount);
        //}


        //[TestMethod]
        //public void TestPayInvoice_ThrowsExceptionWhenInvoiceNotFound()
        //{
        //    //
        //    // Arrange
        //    //
        //    var order = new Order();
        //    order.OrderId = 123456;
        //    order.Amount = 100;
        //    order.ClientId = 200;
        //    order.ClientName = "Bob's FoodMart";
        //    order.BookedDate = new DateTime(2019, 01, 12);
        //    order.CreatedDate = new DateTime(2019, 01, 01);
        //    order.CurrencyIsoCode = "USD";
        //    order.Adjustments.Add(new Adjustment() { Amount = -20, OrderId = 123456, AdjustmentId = 1, CreatedDate = new DateTime(2019, 01, 01) });

        //    var orderService = new OrderService();

        //    //
        //    // Act
        //    //
        //    var ex = Assert.ThrowsException<Exception>(() => orderService.PayInvoice(order, 1));

        //    //
        //    // Assert
        //    //
        //    Assert.AreEqual("Invoice InvoiceId", ex.Message);

        //}


        #endregion Project 5

    }
}
