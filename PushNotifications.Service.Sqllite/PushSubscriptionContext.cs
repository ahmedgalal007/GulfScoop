
using System.Data.Entity;
using WebPush = Lib.Net.Http.WebPush;

namespace PushNotifications.Service.Sqllite
{
    internal class PushSubscriptionContext : DbContext
    {
        public PushSubscriptionContext(string connectionName = "DefaultConnection")
            : base(connectionName)
        {
        }

        public static PushSubscriptionContext Create(string connectionName = "DefaultConnection")
        {
            return new PushSubscriptionContext(connectionName);
        }
        public class PushSubscription : WebPush.PushSubscription
        {
            public string P256DH
            {
                get { return GetKey(WebPush.PushEncryptionKeyName.P256DH); }

                set { SetKey(WebPush.PushEncryptionKeyName.P256DH, value); }
            }

            public string Auth
            {
                get { return GetKey(WebPush.PushEncryptionKeyName.Auth); }

                set { SetKey(WebPush.PushEncryptionKeyName.Auth, value); }
            }

            public PushSubscription()
            { }

            public PushSubscription(WebPush.PushSubscription subscription)
            {
                Endpoint = subscription.Endpoint;
                Keys = subscription.Keys;
            }
        }

        public DbSet<PushSubscription> Subscriptions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PushSubscription>().HasKey(e => e.Endpoint);
            modelBuilder.Entity<PushSubscription>().Ignore(p => p.Keys);
        }
    }
}
