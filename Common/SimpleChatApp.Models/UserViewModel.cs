using System;

namespace SimpleChatApp.Models
{
    /// <summary>
    /// Viewmodel for User
    /// </summary>
    /// <seealso cref="SimpleChatApp.Models.ObservableObject" />
    public class UserViewModel : ObservableObject
    {
        private Guid _id;
        private string _name;
        private string _action;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id
        {
            get { return _id; }
            set { SetAndNotify(ref _id, value, () => Id); }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return _name; }
            set { SetAndNotify(ref _name, value, () => Name); }
        }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action
        {
            get { return _action; }
            set { SetAndNotify(ref _action, value, () => Action); }
        }
    }
}
