namespace EntityFrameworkEmCamadas
{
    public class Ajudante
    {
        public static void limparControlesEFocar(System.Windows.Forms.Form formulario, System.Windows.Forms.TextBox campoFocar = null)
        {
            foreach(var control in formulario.Controls)
                if(control is System.Windows.Forms.TextBox)
                    ((System.Windows.Forms.TextBox)control).Text = "";

            if (campoFocar!=null)
                campoFocar.Focus();
        }
        
        public static void CamposHabilitados(System.Windows.Forms.Form formulario, bool ativar)
        {
            foreach (var control in formulario.Controls)
                if (control is System.Windows.Forms.TextBox)
                {
                    ((System.Windows.Forms.TextBox)control).Enabled = ativar;
                    ((System.Windows.Forms.TextBox)control).BackColor = 
                        !ativar ? System.Drawing.Color.Gray : System.Drawing.Color.White;
                }
        }

    }
}
