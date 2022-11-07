namespace PetStore.Common.Models
{
    /// <summary>
    /// Wrapper around properties used in patch requests
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    public class PatchProperty<T>
    {
        public bool DoUpdate { get; set; }
        public T Value { get; set; }
    }
}
