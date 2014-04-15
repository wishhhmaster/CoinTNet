using System;
using System.Collections.Generic;

namespace CoinTNet.DO
{
    /// <summary>
    /// Generic tree node
    /// http://stackoverflow.com/questions/66893/tree-data-structure-in-c-sharp
    /// 
    /// </summary>
    /// <typeparam name="T">The type of data held by the node</typeparam>
    class NTree<T>
    {
        /// <summary>
        /// The node's children
        /// </summary>
        private LinkedList<NTree<T>> children;

        /// <summary>
        /// Initialises a new instance of the class with the specified data
        /// </summary>
        /// <param name="data">The data</param>
        public NTree(T data)
        {
            this.Data = data;
            children = new LinkedList<NTree<T>>();
        }

        #region Public properties

        /// <summary>
        /// Gets the node's data
        /// </summary>
        public T Data { get; private set; }
        /// <summary>
        /// The node's parent
        /// </summary>
        public NTree<T> Parent { get; private set; }

        /// <summary>
        /// Gets the number of children
        /// </summary>
        public int ChildrenCount
        {
            get { return children.Count; }
        }

        #endregion

        /// <summary>
        /// Adds a child to the current node
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public NTree<T> AddChild(T data)
        {
            var newNode = new NTree<T>(data);
            newNode.Parent = this;
            children.AddFirst(newNode);
            return newNode;
        }

        /// <summary>
        /// Applies an action to the node and all of its descendants
        /// </summary>
        /// <param name="action"></param>
        public void Traverse(Action<NTree<T>> action)
        {
            action(this);
            foreach (NTree<T> kid in children)
                kid.Traverse(action);
        }

        /// <summary>
        /// Gets the chain from the tree's root down to the current node
        /// </summary>
        /// <returns>A chain</returns>
        public List<NTree<T>> GetTree()
        {
            var list = new List<NTree<T>>();

            var p = this;
            while (p != null)
            {
                list.Insert(0, p);
                p = p.Parent;
            }

            return list;
        }
    }
}
