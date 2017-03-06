using System;
using EntityChange;
using Test.Core;

namespace KickStart.EntityChange.Tests
{
    public class OrderProfile : EntityProfile<Order>
    {
        public override void Configure()
        {
            Property(p => p.Total).Formatter(StringFormatter.Currency);

            Collection(p => p.Items).ElementFormatter(v =>
            {
                var orderLine = v as OrderLine;
                return orderLine?.Sku;
            });
        }
    }
}