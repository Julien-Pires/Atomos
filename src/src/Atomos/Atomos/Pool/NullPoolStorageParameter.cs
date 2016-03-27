namespace Atomos
{
    public class NullPoolStorageParameter
    {
        #region Properties

        public bool IsNull { get; } = true;

        #endregion

        #region Constructors

        public NullPoolStorageParameter()
        {
        }

        protected NullPoolStorageParameter(bool isNull)
        {
            IsNull = isNull;
        }

        #endregion
    }
}