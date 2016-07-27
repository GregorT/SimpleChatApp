using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace SimpleChatApp.Models
{
    /// <summary>
    /// Observable object INotifyPropertyChanged implementation
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Sets the and notify.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="property">The property.</param>
        protected virtual void SetAndNotify<T>(ref T field, T value, Expression<Func<T>> property)
        {
            if (!object.ReferenceEquals(field, value))
            {
                field = value;
                this.OnPropertyChanged(property);
            }
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="changedProperty">The changed property.</param>
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> changedProperty)
        {
            if (PropertyChanged != null)
            {
                string name = ((MemberExpression)changedProperty.Body).Member.Name;
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
