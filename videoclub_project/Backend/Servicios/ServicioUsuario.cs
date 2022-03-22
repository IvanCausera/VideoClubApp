using NLog;
using System;
using System.Data.Entity;
using System.Linq;
using videoclub_project.Backend.Modelo;

namespace videoclub_project.Backend.Servicios {
    /// <summary>
    /// Class containing the rules specific to the user table.
    /// </summary>
    class ServicioUsuario : ServicioGenerico<usuarios> {
        private static Logger log = LogManager.GetCurrentClassLogger();
        private DbContext context;

        public usuarios usuLogin { get; set; }

        public ServicioUsuario(DbContext context) : base(context) {
            this.context = context;
        }

        public bool login(string user, string pass) {
            bool correct = true;
            try {
                usuLogin = context.Set<usuarios>().Where(u => u.user == user).FirstOrDefault();
            } catch (Exception e) {
                log.Info("Database connection failed");
                log.Error(e.InnerException);
                log.Error(e.StackTrace);
            }

            if (usuLogin == null) {
                correct = false;
            } else if (!usuLogin.user.Equals(user) || !usuLogin.password.Equals(pass)) {
                correct = false;
            }

            return correct;
        }
    }
}
