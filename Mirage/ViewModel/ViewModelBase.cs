using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Mirage.ViewModel
{
    /// <summary>
    /// Represents a base view model that all viewmodels should derive from
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        private readonly string name;

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Create a new instance of <see cref="ViewModelBase"/>
        /// </summary>
        public ViewModelBase()
        {
            this.name = GetType().ToString();
        }
        /// <summary>
        /// Set value for a property on the ViewModel
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        /// <param name="value">Value for the property to be set</param>
        public void SetValue(string propertyName, object value)
        {
            PropertyInfo info = GetProperty(propertyName);
            
            if (info == null)
                return;

            Type propertyType = info.PropertyType;
            object typedValue = Convert.ChangeType(value, propertyType, System.Globalization.CultureInfo.CurrentCulture);
            info.SetValue(this, typedValue, null);
        }
        /// <summary>
        /// Set a value for a member property of the ViewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">Reference to the field member</param>
        /// <param name="value">Value of the field</param>
        /// <param name="member">Name of the function used to raise changed event</param>
        /// <returns></returns>
        public bool SetProperty<T>(ref T field, T value, [CallerMemberName] string member = null)
        {
            bool fResult = false;
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                RaisePropertyChanged(member);
                fResult = true;
            }

            return fResult;
        }
        /// <summary>
        /// Get the name of the ViewModel
        /// </summary>
        /// <returns></returns>
        protected string GetName()
        {
            return this.name;
        }
        /// <summary>
        /// Raise an event to indicate that a property value has changed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr">Expression for raising type safe property changed events</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> expr)
        {
            string propertyName = GetProperty(expr).Name;
            RaisePropertyChanged(propertyName);
        }
        /// <summary>
        /// Raise a property changed event. The calling property name will be used to fire
        /// </summary>
        /// <param name="propertyName">Name of the property whose value changed</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// Get the <see cref="PropertyInfo"/> for a property on the ViewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr">Strongly typed property expression</param>
        /// <returns></returns>
        private PropertyInfo GetProperty<T>(Expression<Func<T>> expr)
        {
            var member = expr.Body as MemberExpression;
            if (member == null)
                throw new InvalidOperationException("Expression is not a member access expression.");
            var property = member.Member as PropertyInfo;
            if (property == null)
                throw new InvalidOperationException("Member in expression is not a property.");
            return property;
        }
        /// <summary>
        /// Get the <see cref="PropertyInfo"/> for a property given it's name
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        /// <returns></returns>
        private PropertyInfo GetProperty(string propertyName)
        {
            PropertyInfo pInfo = this.GetType().GetRuntimeProperty(propertyName);
            return pInfo;
        }
        /// <summary>
        /// Dispose the ViewModel
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}
