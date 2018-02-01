using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Neudesic.Core.Models;

namespace Neudesic.Android.Adapters
{
    public class UserListAdapter : BaseAdapter<User>
    {
        /// <summary>
        /// The items.
        /// </summary>
        List<User> items;

        /// <summary>
        /// The context.
        /// </summary>
        Activity context;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Neudesic.Android.Adapters.UserListAdapter"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="items">Items.</param>
        public UserListAdapter(Activity context, List<User> items)
        {
            this.context = context;
            this.items = items;
        }

        /// <summary>
        /// Gets the item identifier.
        /// </summary>
        /// <returns>The item identifier.</returns>
        /// <param name="position">Position.</param>
        public override long GetItemId(int position)
        {
            return position;
        }

        /// <summary>
        /// Gets the <see cref="T:Neudesic.Android.Adapters.UserListAdapter"/> with the specified position.
        /// </summary>
        /// <param name="position">Position.</param>
        public override User this[int position]
        {
            get
            {
                return items[position];
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <returns>The view.</returns>
        /// <param name="position">Position.</param>
        /// <param name="convertView">Convert view.</param>
        /// <param name="parent">Parent.</param>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.UserRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.userNameText).Text = $"{item.FirstName} {item.LastName}";
            convertView.FindViewById<ImageView>(Resource.Id.userImageView).SetImageResource(Resource.Drawable.user);

            return convertView;
        }

    }
}