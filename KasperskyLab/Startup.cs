using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KasperskyLab.Startup))]
namespace KasperskyLab
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
