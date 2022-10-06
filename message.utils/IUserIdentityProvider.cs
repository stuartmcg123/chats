namespace message.utils
{
    public interface IUserIdentityProvider {

        /// <summary>
        /// Get the users id.
        /// </summary>
        /// <returns></returns>
        string Get();
    }
}