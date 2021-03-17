using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Core.Utils
{
    public static class ReadOnlyCollectionReplaceItem
    {
        public static IImmutableList<TItem> WithReplaced<TItem>(this IReadOnlyCollection<TItem> collection,
                                                                TItem newItem, Predicate<TItem> predicate)
        {
            return ListWithReplacedItem(collection, newItem, predicate).ToImmutableList();
        }


        private static IList<TItem> ListWithReplacedItem<TItem>(IReadOnlyCollection<TItem> itemCollection,
                                                                TItem newItem, Predicate<TItem> predicate)
        {
            var itemsList = itemCollection.ToList();
            itemsList[itemsList.FindIndex(predicate)] = newItem;
            return itemsList;
        }
    }
}