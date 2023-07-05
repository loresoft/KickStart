using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EntityChange;
using FluentAssertions;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.EntityChange.Tests
{
    public class EntityCompareTests
    {
        private readonly ITestOutputHelper _output;

        public EntityCompareTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Configure()
        {
            Kick.Start(config => config
                .LogTo(_output.WriteLine)
                .IncludeAssemblyFor<OrderProfile>()
                .UseEntityChange()
            );


            var original = new Order
            {
                Id = Guid.NewGuid().ToString(),
                OrderNumber = 1000,
                Total = 10000,
            };

            var current = new Order
            {
                Id = Guid.NewGuid().ToString(),
                OrderNumber = 1000,
                Total = 11000,
                Items = new List<OrderLine>
                {
                    new OrderLine { Sku = "abc-123", Quanity = 1, UnitPrice = 5000 },
                }
            };


            var comparer = new EntityComparer();
            var changes = comparer.Compare(original, current);

            changes.Should().NotBeNull();
            changes.Count.Should().Be(3);

            var total = changes.FirstOrDefault(c => c.Path == "Total");
            total.Should().NotBeNull();
            total.Path.Should().Be("Total");
            total.CurrentFormatted.Should().Be("$11,000.00");

            var items = changes.FirstOrDefault(c => c.Path == "Items[0]");
            items.Should().NotBeNull();
            items.Path.Should().Be("Items[0]");
            items.CurrentFormatted.Should().Be("abc-123");
            items.Operation.Should().Be(ChangeOperation.Add);


            WriteMarkdown(changes);
        }



        private void WriteMarkdown(IReadOnlyList<ChangeRecord> changes)
        {
            var formatter = new MarkdownFormatter();
            var markdown = formatter.Format(changes);

            _output.WriteLine(markdown);
        }

    }
}
