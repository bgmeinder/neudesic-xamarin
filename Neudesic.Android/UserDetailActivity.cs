using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Neudesic.Core.Repository;

namespace Neudesic.Android
{
    [Activity(Label = "@string/user_detail_title", Icon = "@drawable/icon")]
    public class UserDetailActivity : Activity
    {
        /// <summary>
        /// The user image view.
        /// </summary>
        private ImageView userImageView;

        /// <summary>
        /// The username.
        /// </summary>
        private TextView username;

        /// <summary>
        /// The first name.
        /// </summary>
        private TextView firstName;

        /// <summary>
        /// The last name.
        /// </summary>
        private TextView lastName;

        /// <summary>
        /// The delete button.
        /// </summary>
        private Button deleteButton;

        /// <summary>
        /// The user identifier.
        /// </summary>
        private string UserId;

        /// <summary>
        /// The user repository.
        /// </summary>
        private UserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Neudesic.Android.UserDetailActivity"/> class.
        /// </summary>
        public UserDetailActivity()
        {
            this.userRepository = new UserRepository();
        }

        /// <summary>
        /// Ons the create.
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.UserId = Intent.Extras.GetString("userId");
            SetContentView(Resource.Layout.UserDetailView);
            SetViews();
            BindEventHandlers();
            LoadData();
        }

        private void SetViews() 
        {
            this.userImageView = FindViewById<ImageView>(Resource.Id.userImageView);
            this.username = FindViewById<TextView>(Resource.Id.userNameText);
            this.firstName = FindViewById<TextView>(Resource.Id.firstNameText);
            this.lastName = FindViewById<TextView>(Resource.Id.lastNameText);
            this.deleteButton = FindViewById<Button>(Resource.Id.deleteUserButton);
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        private async void LoadData()
        {
            var user = await this.userRepository.GetByIdAsync(this.UserId);
            this.userImageView.SetImageResource(Resource.Drawable.user);
            this.username.Text = user.Username;
            this.firstName.Text = user.FirstName;
            this.lastName.Text = user.LastName;
        }

        /// <summary>
        /// Binds the event handlers.
        /// </summary>
        private void BindEventHandlers() => this.deleteButton.Click += DeleteButton_Click;

        /// <summary>
        /// The delete button event handler
        /// </summary>
        /// <returns>The button click.</returns>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The click event e</param>
        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            await this.userRepository.DeleteAsync(this.UserId);
            this.NavigateToMain();
        }

        /// <summary>
        /// Navigates to main.
        /// </summary>
        private void NavigateToMain()
        {
            var intent = new Intent();
            intent.SetClass(this, typeof(MainActivity));
            StartActivityForResult(intent, 100); 
        }
    }
}
