using System.Data;
using System.Threading;

using Cadru.Data.Dapper.Predicates;

using Dapper;

namespace Cadru.Data.Dapper
{
    public interface IDapperCommandBuilder
    {
        CommandDefinition GetDeleteCommand(IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        CommandDefinition GetInsertCommand(object data, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        CommandDefinition GetSelectCommand(IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        CommandDefinition GetSelectTopCommand(int count = 1, IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
        CommandDefinition GetUpdateCommand(object data, IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    }
}