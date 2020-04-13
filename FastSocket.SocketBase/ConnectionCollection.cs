using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Sodao.FastSocket.SocketBase
{
    /// <summary>
    /// socket connection collection
    /// </summary>
    public sealed class ConnectionCollection
    {
        #region Private Members
        private System.Timers.Timer timer = new System.Timers.Timer();

        /// <summary>
        /// key:ConnectionID
        /// </summary>
        private readonly ConcurrentDictionary<long, IConnection> _dic = new ConcurrentDictionary<long, IConnection>();
        #endregion

        #region Construction
        public ConnectionCollection()
        {
            timer.AutoReset = true;
            timer.Interval = 60 * 1000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var connections = this.ToArray();
            foreach (var conn in connections)
            {
                if (conn == null) continue;
                if (conn.Active == false)
                    Remove(conn.ConnectionID);
                conn.UnRecTime++;
                if (conn.UnRecTime > 20)
                {
                    try
                    {
                        conn.BeginDisconnect();
                    }
                    catch { }
                    finally 
                    {
                        Remove(conn.ConnectionID);
                    }
                }
            }

            //throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// add
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">connection is null</exception>
        public bool Add(SocketBase.IConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            return this._dic.TryAdd(connection.ConnectionID, connection);
        }
        /// <summary>
        /// remove connection by id.
        /// </summary>
        /// <param name="connectionID"></param>
        /// <returns></returns>
        public bool Remove(long connectionID)
        {
            IConnection connection;
            return this._dic.TryRemove(connectionID, out connection);
        }
        /// <summary>
        /// get by connection id
        /// </summary>
        /// <param name="connectionID"></param>
        /// <returns></returns>
        public SocketBase.IConnection Get(long connectionID)
        {
            IConnection connection;
            this._dic.TryGetValue(connectionID, out connection);
            return connection;
        }
        /// <summary>
        /// to array
        /// </summary>
        /// <returns></returns>
        public SocketBase.IConnection[] ToArray()
        {
            return this._dic.ToArray().Select(c => c.Value).ToArray();
        }
        /// <summary>
        /// count.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return this._dic.Count;
        }
        /// <summary>
        /// 断开所有连接
        /// </summary>
        public void DisconnectAll()
        {
            var connections = this.ToArray();
            foreach (var conn in connections) conn.BeginDisconnect();
        }
        #endregion
    }
}