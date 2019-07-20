using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ModuloGestorNotas.Startup))]
namespace ModuloGestorNotas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
