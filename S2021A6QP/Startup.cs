using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(S2021A6QP.Startup))]

namespace S2021A6QP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
