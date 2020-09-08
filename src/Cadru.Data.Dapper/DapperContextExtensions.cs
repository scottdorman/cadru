namespace Cadru.Data.Dapper
{
    internal static class DapperContextExtensions
    {
        internal static IObjectMap MapObject<T>(this IDapperContext dapperContext, DatabaseObjectType databaseObjectType) where T : class
        {
            return dapperContext.Mappings.GetOrAdd(typeof(T), v => ObjectMap<T>.CreateMap(databaseObjectType, dapperContext.CommandAdapter));
        }
    }
}
