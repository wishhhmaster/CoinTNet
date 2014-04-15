
namespace CoinTNet.UI.Common
{
    /// <summary>
    /// Generic object used in list controls
    /// This object encapsulates another object, and returns
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ListControlItem<T>
    {
        /// <summary>
        /// Initialises a new instance of the class
        /// </summary>
        /// <param name="item">The item</param>
        /// <param name="description"></param>
        public ListControlItem(T item, string description)
        {
            Item = item;
            Description = description;
        }
        /// <summary>
        /// Gets or sets the item to encapsulate
        /// </summary>
        public T Item { get; set; }
        /// <summary>
        /// Gets or sets the description to display when the item is displayed in a list control
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Returns the object's description
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Description;
        }

    }
}
