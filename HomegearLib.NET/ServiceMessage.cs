﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomegearLib
{
    public class ServiceMessage
    {
        private Int32 _peerId = 0;
        public Int32 PeerID { get { return _peerId; } internal set { _peerId = value; } }

        private Int32 _channel = -1;
        public Int32 Channel { get { return _channel; } internal set { _channel = value; } }

        private String _type;
        public String Type { get { return _type; } internal set { _type = value; } }

        private Int32 _value = 0;
        public Int32 Value { get { return _value; } internal set { _value = value; } }

        public ServiceMessage(Int32 peerId, Int32 channel, String type)
        {
            _peerId = peerId;
            _channel = channel;
            _type = type;
        }
    }
}
