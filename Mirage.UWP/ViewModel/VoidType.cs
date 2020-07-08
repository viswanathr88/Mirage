namespace Mirage.ViewModel
{
    /// <summary>
    /// Represents an Empty type that can be passed as command arguments
    /// </summary>
    public class VoidType
    {
        /// <summary>
        /// Creates an empty type
        /// </summary>
        public static VoidType Empty
        {
            get
            {
                return new VoidType();
            }
        }
    }
}
