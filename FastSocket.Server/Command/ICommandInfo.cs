
namespace Sodao.FastSocket.Server.Command
{
    /// <summary>
    /// command info interface.
    /// </summary>
    public interface ICommandInfo
    {
        /// <summary>
        /// get the command name
        /// </summary>
        string CMD_ID { get; }
    }
}