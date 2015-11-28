using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ITTWEB_Opgave2_Protein.Startup))]

namespace ITTWEB_Opgave2_Protein
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}