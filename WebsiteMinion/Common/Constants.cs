using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteMinion.Common
{
    public class Constants
    {
        public class Schemas
        {
            public const string Main = "main";
        }

        public class Tables
        {
            public const string WebsiteInfo = "website_info";
            public const string WebsiteStatusHistory = "website_status_history";
        }

        public class ConnectionStringKeys
        {
            public const string MinionSiteDb = "ConnectionStrings:MINION_SITE_DB";
        }
    }
}
