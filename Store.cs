namespace Inventory
{
    public enum SortOrder
    {
        ASC,
        DESC
    }

    class Store
    {
        private List<Item> items = new List<Item>();
        readonly int maxCapacity;

        public Store(int max)
        {
            maxCapacity = max;
        }

        public void AddItem(Item item)
        {
            try
            {
                //check if item exist in store
                bool itemThere = items.Any(currentItem => currentItem.Name == item.Name);
                //check if item is not empty
                if (string.IsNullOrWhiteSpace(item.Name))
                {
                    throw new Exception("Cannot add Empty item  ..!");
                }
                if (itemThere)
                {
                    throw new Exception("Item already There !");
                }
                CheckCapacity(item);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        public void CheckCapacity(Item item)
        {
            if ((GetCurrentVolume() + item.Quantity) <= maxCapacity)
            {
                items.Add(item);
                Console.WriteLine("Item added");
                return;
            }
            throw new Exception("Add item failed, it is out of store capacity!");
        }

        public void DeleteItem(Item item)
        {
            Item? itemIsFound = items.FirstOrDefault(currentItem => currentItem.Name == item.Name);
            //check if item there
            try
            {
                if (itemIsFound != null)
                {
                    items.Remove(item);
                    Console.WriteLine($"item {item.Name} is deleted successfully!");
                    return;
                }
                throw new Exception($"item not exist, unsuccessful process!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        // compute the total amount of items in the store
        public int GetCurrentVolume()
        {
            //check if list not empty
            if (items.Count > 0)
            {
                return items.Sum(item => item.Quantity);
            }
            return 0;
        }

        //find an item by name
        public Item? FindItemByName(string itemName)
        {
            Item? itemIsFound = items.FirstOrDefault(currentItem => currentItem.Name == itemName);
                if (itemIsFound != null)
                {
                    return itemIsFound;
                }
            return null;
        }

        //get the sorted collection by name in ascending order
        public void SortByNameAsc()
        {
            try
            {
                if (items.Count > 0)
                {
                    var sortItem = items.OrderBy(currentItem => currentItem.Name);
                    foreach (var item in sortItem)
                    {
                        Console.WriteLine(
                            $"Name: {item.Name}, Quantity: {item.Quantity}, Created Date: {item.CreatedDate}"
                        );
                    }
                    return;
                }
                throw new Exception("Cannot sort Empty List");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        //order by date asc or desc
        public List<Item> SortByDate(SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.ASC:
                    return items.OrderBy(item => item.CreatedDate).ToList();
                case SortOrder.DESC:
                    return items.OrderByDescending(item => item.CreatedDate).ToList();
                default:
                    return items;
            }
        }

        public Dictionary<string, List<Item>> GroupByDate()
        {
            Dictionary<string, List<Item>> itemDic = new Dictionary<string, List<Item>>();
            List<Item> old = new List<Item>();
            List<Item> newArrival = new List<Item>();

            foreach (var item in items)
            {
                if (item.CreatedDate > DateTime.Now.AddMonths(-3))
                    newArrival.Add(item);
                else
                    old.Add(item);
            }
            itemDic.Add("New Arrival", newArrival);
            itemDic.Add("Old", old);
            return itemDic;
        }

        //print all items list
        public void DisplayItems()
        {
            if (items.Count > 0)
            {
                items.ForEach(item =>
                    Console.WriteLine(
                        $"Name: {item.Name}, Quantity: {item.Quantity}, Created Date: {item.CreatedDate}"
                    )
                );
                return;
            }
            Console.WriteLine("Empty List");
        }
    }
}
