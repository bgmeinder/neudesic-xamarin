using Android.App;
using Neudesic.Core.Repository;

namespace Neudesic.Android.Fragments
{
    public class BaseFragment : Fragment
    {
        /// <summary>
        /// The user repository.
        /// </summary>
        protected UserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Neudesic.Android.Fragments.BaseFragment"/> class.
        /// </summary>
        public BaseFragment()
        {
            this.userRepository = new UserRepository();
        }
    }
}
