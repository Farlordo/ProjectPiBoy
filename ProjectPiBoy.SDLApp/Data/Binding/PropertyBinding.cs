using ProjectPiBoy.Common.Utilities;
using ProjectPiBoy.SDLApp.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.SDLApp.Data.Binding
{
    /// <summary>
    /// Creates a binding between two properties
    /// </summary>
    public class PropertyBinding<TViewData, TModelData> : CustomBindableBase, IBinding<TViewData, TModelData>
    {
        /// <summary>
        /// Creates a new <see cref="PropertyBinding{TViewData, TModelData}"/> from property expressions
        /// </summary>
        /// <typeparam name="TView">The type of the view</typeparam>
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <param name="viewPropLambda">The view property lambda expression</param>
        /// <param name="modelPropLambda">The model property lambda expression</param>
        /// <returns>The new <see cref="PropertyBinding{TViewData, TModelData}"/></returns>
        public static PropertyBinding<TViewData, TModelData> NewBinding<TView, TModel>(TView view, Expression<Func<TView, TViewData>> viewPropLambda, TModel model, Expression<Func<TModel, TModelData>> modelPropLambda)
        {
            PropertyInfo viewProp = ReflectionUtil.GetPropertyInfo(viewPropLambda);
            PropertyInfo modelProp = ReflectionUtil.GetPropertyInfo(modelPropLambda);

            return new PropertyBinding<TViewData, TModelData>(view, viewProp, model, modelProp);
        }

        public PropertyBinding(object view, PropertyInfo viewProperty, object model, PropertyInfo modelProperty)
        {
            this.ViewProperty = viewProperty;
            this.ModelProperty = modelProperty;

            this.View = view;
            this.Model = model;

            this.ViewWatcher = (s, e) => this.OnViewChanged();
            this.ModelWatcher = (s, e) => this.OnModelChanged();

            //TODO: Find out how to unregister these when needed, to avoid memory leaks!
            //Note that these are the only things holding onto this object!

            if (view is INotifyPropertyChanged viewNotifier)
                viewNotifier.PropertyChanged += this.ViewWatcher;

            if (model is INotifyPropertyChanged modelNotifier)
                modelNotifier.PropertyChanged += this.ModelWatcher;
        }

        protected object View { get; }
        protected object Model { get; }

        protected PropertyInfo ViewProperty { get; }
        protected PropertyInfo ModelProperty { get; }

        protected PropertyChangedEventHandler ViewWatcher;
        protected PropertyChangedEventHandler ModelWatcher;

        public TViewData ViewData
        {
            get => (TViewData) this.ViewProperty.GetMethod.Invoke(this.View, new object[0]);
            set
            {
                //Only set the property if it has changed
                if (!Equals(this.ViewData, value))
                {
                    this.ViewProperty.SetMethod.Invoke(this.View, new object[] { value });
                    this.OnPropertyChanged(nameof(ViewData));
                }
            }
        }

        public TModelData ModelData
        {
            get => (TModelData) this.ModelProperty.GetMethod.Invoke(this.Model, new object[0]);
            set
            {
                //Only set the property if it has changed
                if (!Equals(this.ModelData, value))
                {
                    this.ModelProperty.SetMethod.Invoke(this.Model, new object[] { value });
                    this.OnPropertyChanged(nameof(ModelData));
                }
            }
        }

        //TODO: These properties aren't used yet

        private string _StringFormat;
        public string StringFormat
        {
            get => this._StringFormat;
            set => this.SetProperty(ref this._StringFormat, value);
        }

        private object _FallbackValue;
        public object FallbackValue
        {
            get => this._FallbackValue;
            set => this.SetProperty(ref this._FallbackValue, value);
        }

        private object _TargetNullValue;
        public object TargetNullValue
        {
            get => this._TargetNullValue;
            set => this.SetProperty(ref this._TargetNullValue, value);
        }

        public void Recalculate()
        {
            this.OnModelChanged();
        }

        //TODO: Expand binding support

        protected void OnModelChanged()
        {
            //If the types are the same, update the binding
            if (typeof(TViewData) == typeof(TModelData))
                this.ViewData = (TViewData)(object)this.ModelData;
            else
                throw new InvalidOperationException($"Can't assign {typeof(TModelData)} to {typeof(TViewData)}!");

            this.BindingUpdated?.Invoke();
        }

        protected void OnViewChanged()
        {
            //If the types are the same, update the binding
            if (typeof(TViewData) == typeof(TModelData))
                this.ModelData = (TModelData)(object)this.ViewData;
            else
                throw new InvalidOperationException($"Can't assign {typeof(TViewData)} to {typeof(TModelData)}!");

            this.BindingUpdated?.Invoke();
        }

        public event Action BindingUpdated;
    }
}
