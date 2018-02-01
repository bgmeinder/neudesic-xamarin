using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Neudesic.Android.Adapters;
using Neudesic.Core.Models;

namespace Neudesic.Android.Fragments
{
    public class UserListFragment : BaseFragment
    {
        /// <summary>
        /// The list view.
        /// </summary>
        protected ListView listView;

        /// <summary>
        /// The users.
        /// </summary>
        private List<User> users;

        /// <summary>
        /// Ons the activity created.
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            BindEventHandlers();
            LoadUsers();
        }

        /// <summary>
        /// Ons the create view.
        /// </summary>
        /// <returns>The create view.</returns>
        /// <param name="inflater">Inflater.</param>
        /// <param name="container">Container.</param>
        /// <param name="savedInstanceState">Saved instance state.</param>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.UserListFragment, container, false);
        }

        /// <summary>
        /// Loads the users.
        /// </summary>
        private async void LoadUsers() {
            var allUsers = await this.userRepository.GetAsync();
            this.users = allUsers.ToList();

            listView.Adapter = new UserListAdapter(this.Activity, this.users);
        }

        /// <summary>
        /// Binds the event handlers.
        /// </summary>
        private void BindEventHandlers()
        {
            this.listView = this.View.FindViewById<ListView>(Resource.Id.userListView);
            listView.ItemClick += ListView_ItemClick;
        }

        /// <summary>
        /// Lists the view item click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">The event args</param>
        protected void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e) {
            var user = this.users[e.Position];
            var intent = new Intent();
            intent.SetClass(this.Activity, typeof(UserDetailActivity));
            intent.PutExtra("userId", user.Id);

            StartActivityForResult(intent, 100);
        }
    }
}