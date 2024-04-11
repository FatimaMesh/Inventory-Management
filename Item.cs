namespace Inventory
{
    class Item
    {
        private string? _name;
        private int _quantity;
        private DateTime _createdDate;
        public string? Name
        {
            get { return _name; }
        }
        public int Quantity
        {
            //cannot be negative
            get { return _quantity; }
            set { _quantity = value; }
        }
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public Item(string name, int quantity, DateTime? createdDate = null)
        {
            try
            {
                if (quantity <= 0)
                {
                    throw new Exception("Quantity must be positive number");
                }
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new Exception("You should add name for item");
                }
                _name = name;
                _quantity = quantity;
                _createdDate = createdDate ?? DateTime.Now;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return;
            }
        }
    }
}
