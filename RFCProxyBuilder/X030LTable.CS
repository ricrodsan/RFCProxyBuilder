using System;
//using System.Text;
//using System.Collections;
//using System.Runtime.InteropServices;
//using System.Xml.Serialization;
//using System.Web.Services;
//using System.Web.Services.Description;
//using System.Web.Services.Protocols;
using SAP.Connector;
using SAP.Connector.Rfc;

namespace RFCProxyBuilder
{
    /// <summary>
    /// A typed collection of X030L elements.
    /// </summary>
    [Serializable]
    public class X030LTable : SAPTable
    {

        /// <summary>
        /// Returns the element type X030L.
        /// </summary>
        /// <returns>The type X030L.</returns>
        public override Type GetElementType()
        {
            return (typeof(X030L));
        }

        /// <summary>
        /// Creates an empty new row of type X030L.
        /// </summary>
        /// <returns>The newX030L.</returns>
        public override object CreateNewRow()
        {
            return new X030L();
        }

        /// <summary>
        /// The indexer of the collection.
        /// </summary>
        public X030L this[int index]
        {
            get
            {
                return ((X030L)(List[index]));
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// Adds a X030L to the end of the collection.
        /// </summary>
        /// <param name="value">The X030L to be added to the end of the collection.</param>
        /// <returns>The index of the newX030L.</returns>
        public int Add(X030L value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// Inserts a X030L into the collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which value should be inserted.</param>
        /// <param name="value">The X030L to insert.</param>
        public void Insert(int index, X030L value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        /// Searches for the specified X030L and returnes the zero-based index of the first occurrence in the collection.
        /// </summary>
        /// <param name="value">The X030L to locate in the collection.</param>
        /// <returns>The index of the object found or -1.</returns>
        public int IndexOf(X030L value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// Determines wheter an element is in the collection.
        /// </summary>
        /// <param name="value">The X030L to locate in the collection.</param>
        /// <returns>True if found; else false.</returns>
        public bool Contains(X030L value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// Removes the first occurrence of the specified X030L from the collection.
        /// </summary>
        /// <param name="value">The X030L to remove from the collection.</param>
        public void Remove(X030L value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Copies the contents of the X030LTable to the specified one-dimensional array starting at the specified index in the target array.
        /// </summary>
        /// <param name="array">The one-dimensional destination array.</param>           
        /// <param name="index">The zero-based index in array at which copying begins.</param>           
        public void CopyTo(X030L[] array, int index)
        {
            List.CopyTo(array, index);
        }
    }
}
