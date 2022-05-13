using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore_NhibernateApp.Models
{
    public class OpenSessionNHibernate
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();

            var configurationPath = HttpContext.Current.Server.MapPath(@"~\DAL\nh.configuration.xml");

            configuration.Configure(configurationPath);

            var userConfigurationFile = HttpContext.Current.Server.MapPath(@"~\DAL\user.mapping.xml");

            configuration.AddFile(userConfigurationFile);

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }
}