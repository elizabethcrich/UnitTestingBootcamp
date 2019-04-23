using System;
using System.Collections.Generic;
using ProductionCode.Orders.Models;
using System.Linq;

namespace xUnit.FullFramework.Orders.Builders
{
    class OrderBuilder
    {
    

        private decimal? _amount = null;
        private int? _id = null;
        private DateTime? _bookedDate = new DateTime(2019, 02, 13);
       
        private List<Adjustment> _adjustments = new List<Adjustment>();
        private List<Refund> _refunds = new List<Refund>();
        private bool _deleted = false;
        private string _currencyCode = null;

        public OrderBuilder WithBookedDate(DateTime? bookedDate)
        {
            _bookedDate = bookedDate;
            return this;
        }

        public OrderBuilder WithOrderId(int id)
        {
            _id = id;
            return this;
        }

        public OrderBuilder WithAmount(decimal amount)
        {
            _amount = amount;
            return this;
        }

        public OrderBuilder WithCredit(decimal creditAmount)
        {
            _adjustments.Add(new Adjustment() { Amount = creditAmount * -1 });
            return this;
        }

        public OrderBuilder WithDebit(decimal debitAmount)
        {
            _adjustments.Add(new Adjustment() { Amount = debitAmount });
            return this;
        }


        public OrderBuilder WithRefund(decimal refundAmount)
        {
            _refunds.Add(new Refund() { Amount = refundAmount });
            return this;
        }

        public OrderBuilder AsDeleted()
        {
            _deleted = true;
            return this;
        }

        public OrderBuilder WithCurrency(string currencyCode)
        {
            _currencyCode = currencyCode;
            return this;
        }

        public Order Build()
        {
            if (!_amount.HasValue)
            {
                _amount = 1000;
            }

            if (!_id.HasValue)
            {
                _id = new Random(DateTime.UtcNow.Millisecond).Next();
            }


            var order = new Order()
            {
                OrderId = _id.Value,
                Amount = _amount.Value,
                Subtotal = _amount.Value,
                BookedDate = _bookedDate,
                
                IsDeleted = _deleted,
                CurrencyIsoCode = _currencyCode ?? "USD"

            };

            order.Adjustments.AddRange(_adjustments);
            order.Refunds.AddRange(_refunds);

            return order;
        }
    }
}
