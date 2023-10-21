using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompraVentaCarrosApi.Tools
{
    public static class EmailBody
    {
        public static string EmailStringBody( string email, string emailToken)
        {
            //return $@"<html>
            //            <head>
            //            </head>
            //            <body style=""margin:0;padding:0;font-family: Arial, Helvetica, sans-serif;"">
            //                <div style="" height: auto;background: Linear-gradient(to top, #c9c9ff 50%, #6e6ef6 90%) no-repeat;width:400px;padding:30px >
            //                    <h1>Recuperar tu contraseña</h1>
            //                    <hr>
            //                    <p>Haz recivido este Email por tu solicitud para recuperar tu contraseña.</p>
            //                    <p>Por favor da click en el link para continuar el proceso.</p>
            //                    <a href=""http://Localhost:4200/reset?email={email}&code={emailToken}"" target=""_blank="" style="" background:#0d6efd;padding:10px;border:none;color:white; border-radius:4px;display:block; margin:0 auto;width:50%;text-aling:center;text-decoration:none"">Cambiar Contraseña</a><br>
            //                    <p>Cordial saludo, <br><br>
            //                    Administrador</p>
            //                </div>
            //            </body>

            //        </html>";

            return $@"<p>Correo para confirmación de la cuenta<p/><br><a href=""http://Localhost:4200/reset?email={email}&code={emailToken}"">Click para RECUPERAR LA CONTRASEÑA</a>";

        }
    }
}
