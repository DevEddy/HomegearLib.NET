﻿using HomegearLib.RPC;
using System;
using System.Collections.Generic;

namespace HomegearLib
{
    public class Links : ReadOnlyDictionary<Int32, ReadOnlyDictionary<Int32, Link>>, IDisposable
    {
        RPCController _rpc = null;

        Int32 _peerId = 0;
        public Int32 PeerID { get { return _peerId; } }

        Int32 _channel = -1;
        public Int32 Channel { get { return _channel; } }

        public Links(RPCController rpc, Int32 peerId, Int32 channel) : base()
        {
            _rpc = rpc;
            _peerId = peerId;
            _channel = channel;
        }

        public void Dispose()
        {
            _rpc = null;
        }

        public void Reload()
        {
            List<Link> allLinks = _rpc.GetLinks(_peerId, _channel);
            Dictionary<Int32, Dictionary<Int32, Link>> links = new Dictionary<Int32, Dictionary<Int32, Link>>();
            foreach (Link link in allLinks)
            {
                if (!links.ContainsKey(link.RemotePeerID)) links.Add(link.RemotePeerID, new Dictionary<Int32, Link>());
                if (links[link.RemotePeerID].ContainsKey(link.RemoteChannel)) continue;
                links[link.RemotePeerID].Add(link.RemoteChannel, link);
            }
            Dictionary<Int32, ReadOnlyDictionary<Int32, Link>> links2 = new Dictionary<Int32, ReadOnlyDictionary<Int32, Link>>();
            foreach (KeyValuePair<Int32, Dictionary<Int32, Link>> pair in links)
            {
                links2.Add(pair.Key, new ReadOnlyDictionary<Int32, Link>(pair.Value));
            }
            _dictionary = links2;
        }

        public void Add(Int32 remoteID, Int32 remoteChannel, bool isSender)
        {
            if (isSender) _rpc.AddLink(_peerId, _channel, remoteID, remoteChannel);
            else _rpc.AddLink(remoteID, remoteChannel, _peerId, _channel);
        }
    }
}
