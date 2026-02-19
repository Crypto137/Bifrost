using Bifrost.Core.Models;

namespace Bifrost.Core.ClientManagement
{
    public class ServerManager
    {
        private readonly List<ServerInfo> _servers = new();

        // Expose server list for serialization, but do not allow GUI implementations to access it.
        internal List<ServerInfo> ServerList { get => _servers; }

        public int ServerCount { get => _servers.Count; }

        public ServerManager()
        {
        }

        public List<ServerInfo>.Enumerator GetEnumerator()
        {
            return _servers.GetEnumerator();
        }

        public ServerInfo GetServer(int index)
        {
            if (index < 0 || index >= _servers.Count)
                return null;

            return _servers[index];
        }

        public void SetServerList(IEnumerable<ServerInfo> serverList)
        {
            _servers.Clear();

            if (serverList != null)
                _servers.AddRange(serverList);
        }

        public ServerInfo AddServer()
        {
            ServerInfo serverInfo = new();
            _servers.Add(serverInfo);
            return serverInfo;
        }

        public bool RemoveServer(int index)
        {
            if (index < 0 || index >= _servers.Count)
                return false;

            _servers.RemoveAt(index);
            return true;
        }

        public bool RemoveServer(ServerInfo serverInfo)
        {
            int index = _servers.IndexOf(serverInfo);
            return RemoveServer(index);
        }
    }
}
