using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.SDLApp.Data.Binding
{
    /// <summary>
    /// Represents a binding between a model data object, and a view data object
    /// </summary>
    /// <typeparam name="TViewData">The type of view data</typeparam>
    /// <typeparam name="TModelData">The type of model data</typeparam>
    public interface IBinding<TViewData, TModelData> : INotifyPropertyChanged
    {
        /// <summary>
        /// A delegate to the bound view data
        /// </summary>
        TViewData ViewData { get; set; }

        /// <summary>
        /// A delegate to the bound model data
        /// </summary>
        TModelData ModelData { get; set; }

        /// <summary>
        /// The format to use when binding a string
        /// </summary>
        string StringFormat { get; set; }

        /// <summary>
        /// The value to use when a binding fails
        /// </summary>
        object FallbackValue { get; set; }

        /// <summary>
        /// The value to use when the model data is null
        /// </summary>
        object TargetNullValue { get; set; }

        /// <summary>
        /// Recalculates the binding. Model data takes priority.
        /// </summary>
        void Recalculate();

        /// <summary>
        /// An event that gets fired whenever the binding is updated
        /// </summary>
        event Action BindingUpdated;
    }
}
