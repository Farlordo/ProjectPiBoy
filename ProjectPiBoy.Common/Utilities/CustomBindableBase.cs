using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.Common.Utilities
{
    /// <summary>
    /// An alternative to Prism's BindableBase that is cross platform.
    /// </summary>
    /// <remarks>
    /// The source code for <see cref="BindableBase"/> is pasted into this class.
    /// The OnPropertyChanged(Expression) method has been removed, as it relies on Prism.
    /// </remarks>
    public abstract class CustomBindableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Similar to <see cref="SetProperty{T}(ref T, T, string)"/>, but also fires <see cref="PropertyChanged"/>
        /// for the other listed properties as well.
        /// </summary>
        /// <remarks>
        /// This isn't in Prism's BindableBase class, it's custom.
        /// WARNING: You must pass the name of the main property, as it can't be passed by the compiler!
        /// </remarks>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyNames">A list of property names to notify of changes for</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected virtual bool SetPropertyWithDependencies<T>(ref T storage, T value, params string[] propertyNames)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;

            //All of these properties have changed
            foreach (string propertyName in propertyNames)
                this.OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
