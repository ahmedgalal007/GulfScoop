using Lib.Net.Http.WebPush;
using System;
using System.Collections.Generic;
using System.Text;

namespace gulfsoccer.utilities.PushNotifications
{
    public class PushMessage
    {
        #region Fields
        private int? _timeToLive;
        #endregion

        #region Properties
        public string Content { get; set; }
        public PushMessageUrgency Urgency { get; set; }
        public string Topic { get; set; }
        public int? TimeToLive
        {
            get { return _timeToLive; }

            set
            {
                if (value.HasValue && (value.Value < 0))
                {
                    throw new ArgumentOutOfRangeException(nameof(TimeToLive), "The TTL must be a non-negative integer");
                }

                _timeToLive = value;
            }
        }
        #endregion

        #region Constructors
        public PushMessage(string content)
        {
            Content = content;
            Urgency = PushMessageUrgency.Normal;
        }
        #endregion
    }
}
