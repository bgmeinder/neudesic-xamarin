using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Neudesic.Android.Fragments;

namespace Neudesic.Android
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IFragmentListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "Manage Users";
            SetFragment();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            SetFragment(item.ItemId);
            return base.OnOptionsItemSelected(item);
        }

        public void SetFragment(int MenuId = Resource.Id.menu_list) 
        {
            BaseFragment view = new UserListFragment();
            if (MenuId != Resource.Id.menu_list) 
            {
                view = new AddUserFragment();
            }

            var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
            var transaction = this.FragmentManager.BeginTransaction();
            if (fragment != null)
            {
                transaction.Remove(fragment);
            }
            transaction.Add(Resource.Id.fragmentContainer, view);
            transaction.Commit();
        }
    }
}