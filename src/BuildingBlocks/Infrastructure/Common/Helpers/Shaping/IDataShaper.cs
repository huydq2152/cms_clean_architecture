namespace Infrastructure.Common.Helpers.Shaping
{
    /// <summary>
    /// Defines methods for shaping data.
    /// </summary>
    /// <typeparam name="T">The type of the objects to be shaped.</typeparam>
    public interface IDataShaper<T>
    {
        /// <summary>
        /// Shapes data for a collection of entities based on the provided fields string.
        /// </summary>
        /// <param name="entities">The entities to shape.</param>
        /// <param name="fieldsString">A string containing the fields to include in the shaped data.</param>
        /// <returns>A collection of shaped entities.</returns>
        IEnumerable<T> ShapeData(IEnumerable<T> entities, string fieldsString);

        /// <summary>
        /// Shapes data for a single entity based on the provided fields string.
        /// </summary>
        /// <param name="entity">The entity to shape.</param>
        /// <param name="fieldsString">A string containing the fields to include in the shaped data.</param>
        /// <returns>A shaped entity.</returns>
        T ShapeData(T entity, string fieldsString);
    }
}